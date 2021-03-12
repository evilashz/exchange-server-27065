using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Serialization;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.ShadowRedundancy;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.Transport.MessageResubmission
{
	// Token: 0x0200013E RID: 318
	internal class ResubmitRequestFacade
	{
		// Token: 0x06000E08 RID: 3592 RVA: 0x00033678 File Offset: 0x00031878
		public ResubmitRequestFacade(Guid correlationId, IEnumerable<PrimaryServerInfo> primaryServers, IMessagingDatabase messagingDatabase, Destination destination, DateTime startTime, DateTime endTime, bool resubmitFromShadow, bool wasCurrentServerUnresponsive, bool isTestRequest = false)
		{
			if (messagingDatabase == null)
			{
				throw new ArgumentNullException("messagingDatabase");
			}
			this.messagingDatabase = messagingDatabase;
			using (Transaction transaction = messagingDatabase.BeginNewTransaction())
			{
				this.primaryRequest = messagingDatabase.NewReplayRequest(correlationId, destination, startTime, endTime, isTestRequest);
				this.primaryRequest.State = ((!resubmitFromShadow || wasCurrentServerUnresponsive) ? ResubmitRequestState.Running : ResubmitRequestState.Completed);
				this.primaryRequest.DiagnosticInformation = "(Primary - " + this.primaryRequest.State + ")";
				this.primaryRequest.Commit();
				this.replaySubRequests.Add(this.primaryRequest);
				this.requestId = this.primaryRequest.PrimaryRequestId;
				this.correlationId = correlationId;
				this.isTestRequest = isTestRequest;
				MessageResubmissionPerfCounters.Instance.UpdateResubmitRequestCount(this.primaryRequest.State, 1);
				foreach (PrimaryServerInfo primaryServerInfo in primaryServers)
				{
					destination = new Destination(Destination.DestinationType.Shadow, primaryServerInfo.DatabaseState);
					IReplayRequest replayRequest = messagingDatabase.NewReplayRequest(correlationId, destination, startTime, this.primaryRequest.DateCreated, isTestRequest);
					replayRequest.State = ResubmitRequestState.Running;
					replayRequest.PrimaryRequestId = this.requestId;
					replayRequest.DiagnosticInformation = "(Shadow - " + primaryServerInfo.ServerFqdn + ")";
					replayRequest.Commit();
					this.replaySubRequests.Add(replayRequest);
					MessageResubmissionPerfCounters.Instance.UpdateResubmitRequestCount(replayRequest.State, 1);
				}
				transaction.Commit(TransactionCommitMode.Lazy);
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003384C File Offset: 0x00031A4C
		public ResubmitRequestFacade(IMessagingDatabase messagingDatabase, long requestId, Guid correlationId, bool isTestRequest = false)
		{
			if (messagingDatabase == null)
			{
				throw new ArgumentNullException("messagingDatabase");
			}
			this.messagingDatabase = messagingDatabase;
			this.requestId = requestId;
			this.correlationId = correlationId;
			this.isTestRequest = isTestRequest;
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x000338A0 File Offset: 0x00031AA0
		public long RequestId
		{
			get
			{
				return this.requestId;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x000338B4 File Offset: 0x00031AB4
		public bool IsCompleted
		{
			get
			{
				bool result;
				using (this.syncLock.AcquireReadLock())
				{
					result = this.replaySubRequests.All((IReplayRequest replayRequest) => replayRequest.State == ResubmitRequestState.Completed);
				}
				return result;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0003391C File Offset: 0x00031B1C
		public bool IsTestRequest
		{
			get
			{
				bool result;
				using (this.syncLock.AcquireReadLock())
				{
					result = this.replaySubRequests.All((IReplayRequest replayRequest) => replayRequest.IsTestRequest);
				}
				return result;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0003397C File Offset: 0x00031B7C
		public DateTime CreationTime
		{
			get
			{
				DateTime result;
				using (this.syncLock.AcquireReadLock())
				{
					if (this.replaySubRequests == null || this.replaySubRequests.Count == 0)
					{
						result = DateTime.MinValue;
					}
					else
					{
						result = this.replaySubRequests[0].DateCreated;
					}
				}
				return result;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x000339EC File Offset: 0x00031BEC
		public int OutstaindingMailItemCount
		{
			get
			{
				int result;
				using (this.syncLock.AcquireReadLock())
				{
					result = this.replaySubRequests.Sum((IReplayRequest req) => req.OutstandingMailItemCount);
				}
				return result;
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00033AAC File Offset: 0x00031CAC
		public bool IsSimilar(Destination destination, DateTime startTime, DateTime endTime, IEnumerable<PrimaryServerInfo> shadowServers)
		{
			bool result;
			using (this.syncLock.AcquireReadLock())
			{
				bool flag;
				if (this.primaryRequest.Destination.Type != Destination.DestinationType.Conditional && this.primaryRequest.Destination.Equals(destination))
				{
					flag = (from p in shadowServers
					select new Destination(Destination.DestinationType.Shadow, p.DatabaseState)).All((Destination p) => this.replaySubRequests.Any((IReplayRequest r) => r.Destination.Equals(p)));
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				bool flag3 = this.primaryRequest.Destination.Type == Destination.DestinationType.Conditional && destination.ToString().Equals(this.primaryRequest.Destination.ToString());
				result = (this.primaryRequest.StartTime == startTime && this.primaryRequest.EndTime == endTime && (flag2 || flag3));
			}
			return result;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00033BB0 File Offset: 0x00031DB0
		public ResubmitRequest GetResubmitRequest()
		{
			ResubmitRequest result;
			using (this.syncLock.AcquireReadLock())
			{
				if (this.replaySubRequests.Count == 0)
				{
					result = null;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					ResubmitRequest resubmitRequest = null;
					ResubmitRequestState state = ResubmitRequestState.Completed;
					foreach (IReplayRequest replayRequest in this.replaySubRequests)
					{
						if (ResubmitRequestFacade.IsPrimary(replayRequest))
						{
							resubmitRequest = ResubmitRequest.Create(replayRequest.PrimaryRequestId, Components.Configuration.LocalServer.TransportServer.Name, replayRequest.StartTime, replayRequest.Destination.ToString(), replayRequest.DiagnosticInformation, replayRequest.EndTime, replayRequest.DateCreated, (int)replayRequest.State);
						}
						if (replayRequest.State == ResubmitRequestState.Running || replayRequest.State == ResubmitRequestState.Paused)
						{
							state = replayRequest.State;
						}
						stringBuilder.Append(replayRequest.DiagnosticInformation).Append(" ");
					}
					if (resubmitRequest != null)
					{
						resubmitRequest.State = state;
						resubmitRequest.DiagnosticInformation = stringBuilder.ToString();
					}
					result = resubmitRequest;
				}
			}
			return result;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00033CE8 File Offset: 0x00031EE8
		public void Add(IReplayRequest replayRequest)
		{
			using (this.syncLock.AcquireWriteLock())
			{
				this.replaySubRequests.Add(replayRequest);
			}
			if (ResubmitRequestFacade.IsPrimary(replayRequest))
			{
				this.primaryRequest = replayRequest;
			}
			this.AddSystemProbeTrace("Replay sub request {0} added to the resubmission request {1}", new object[]
			{
				replayRequest.RequestId,
				this.requestId
			});
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00033D80 File Offset: 0x00031F80
		public bool SetState(ResubmitRequestState newState, out byte[] response)
		{
			if (newState != ResubmitRequestState.Running && newState != ResubmitRequestState.Paused)
			{
				throw new ArgumentException(Strings.InvalidMessageResubmissionState);
			}
			bool flag = false;
			response = null;
			using (this.syncLock.AcquireReadLock())
			{
				using (Transaction transaction = this.messagingDatabase.BeginNewTransaction())
				{
					foreach (IReplayRequest replayRequest2 in from replayRequest in this.replaySubRequests
					where replayRequest.State == ResubmitRequestState.Running || replayRequest.State == ResubmitRequestState.Paused
					select replayRequest)
					{
						ResubmitRequestState state = replayRequest2.State;
						flag = true;
						replayRequest2.State = newState;
						replayRequest2.Materialize(transaction);
						this.AddSystemProbeTrace("Updated state for request: Primary request id = {0}, request id = {1}, state = {2}", new object[]
						{
							replayRequest2.PrimaryRequestId,
							replayRequest2.RequestId,
							newState
						});
						MessageResubmissionPerfCounters.Instance.ChangeResubmitRequestState(state, newState);
					}
					transaction.Commit(TransactionCommitMode.Lazy);
				}
			}
			if (!flag)
			{
				response = Serialization.ObjectToBytes(new ResubmitRequestResponse(ResubmitRequestResponseCode.CannotModifyCompletedRequest, Strings.CannotModifyCompletedRequest(this.RequestId).ToString()));
			}
			return flag;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00033EF4 File Offset: 0x000320F4
		public bool Remove(out byte[] response)
		{
			using (this.syncLock.AcquireWriteLock())
			{
				response = null;
				IReplayRequest replayRequest = this.replaySubRequests.FirstOrDefault((IReplayRequest replaySubRequest) => replaySubRequest.State == ResubmitRequestState.Running);
				if (replayRequest != null)
				{
					response = Serialization.ObjectToBytes(new ResubmitRequestResponse(ResubmitRequestResponseCode.CannotRemoveRequestInRunningState, Strings.CannotRemoveRequestInRunningState(replayRequest.RequestId).ToString()));
					return false;
				}
				List<IReplayRequest> list = new List<IReplayRequest>();
				foreach (IReplayRequest replayRequest2 in this.replaySubRequests)
				{
					ExTraceGlobals.MessageResubmissionTracer.TraceDebug<long, long>((long)this.GetHashCode(), "Deleting sub replay request: primary requestId = {0}, requestId = {1}", replayRequest2.PrimaryRequestId, replayRequest2.RequestId);
					this.AddSystemProbeTrace("Deleting sub replay request: Primary request id = {0}, request id = {1}", new object[]
					{
						replayRequest2.PrimaryRequestId,
						replayRequest2.RequestId
					});
					MessageResubmissionPerfCounters.Instance.UpdateResubmitRequestCount(replayRequest2.State, -1);
					replayRequest2.Delete();
					replayRequest2.Dispose();
					list.Add(replayRequest2);
				}
				foreach (IReplayRequest item in list)
				{
					this.replaySubRequests.Remove(item);
				}
			}
			return true;
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x000340B8 File Offset: 0x000322B8
		public string GetDiagnosticInformationString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (this.syncLock.AcquireReadLock())
			{
				foreach (IReplayRequest replayRequest in this.replaySubRequests)
				{
					stringBuilder.AppendLine(string.Format("SubRequest ID={0} | CreationTime={1} | StartTime={2} | EndTime={3} | State={4} | DiagnosticInfo={5} | DestinationType={6} | Destination={7}", new object[]
					{
						replayRequest.RequestId,
						replayRequest.DateCreated.ToLocalTime(),
						replayRequest.StartTime.ToLocalTime(),
						replayRequest.EndTime.ToLocalTime(),
						replayRequest.State,
						replayRequest.DiagnosticInformation,
						replayRequest.Destination.Type,
						replayRequest.Destination.ToString()
					}));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x000341E8 File Offset: 0x000323E8
		public XElement GetDiagnosticInformation()
		{
			XElement result;
			using (this.syncLock.AcquireReadLock())
			{
				XElement xelement = new XElement("ResubmissionRequest", new XAttribute("RequestId", this.RequestId.ToString()));
				foreach (IReplayRequest replayRequest in this.replaySubRequests)
				{
					xelement.Add(new object[]
					{
						new XElement("SubRequest", new object[]
						{
							new XAttribute("RequestId", replayRequest.RequestId),
							new XAttribute("DestinationType", replayRequest.Destination.Type),
							new XAttribute("DestinationId", replayRequest.Destination.ToString()),
							new XAttribute("DateCreated", replayRequest.DateCreated),
							new XAttribute("StartTime", replayRequest.StartTime),
							new XAttribute("EndTime", replayRequest.EndTime),
							new XAttribute("LastResubmittedMessageOriginalDeliveryTime", replayRequest.LastResubmittedMessageOrginalDeliveryTime),
							new XAttribute("OutstandingMailItemCount", replayRequest.OutstandingMailItemCount),
							new XAttribute("ContinuationToken", replayRequest.ContinuationToken),
							new XAttribute("TotalReplayedMessages", replayRequest.TotalReplayedMessages),
							new XAttribute("State", replayRequest.State)
						}),
						new XText(replayRequest.DiagnosticInformation ?? "Unavailable")
					});
				}
				result = xelement;
			}
			return result;
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0003444C File Offset: 0x0003264C
		public int Resubmit(int maxAllowedMessages)
		{
			int num = 0;
			using (this.syncLock.AcquireReadLock())
			{
				foreach (IReplayRequest replayRequest in from replaySubRequest in this.replaySubRequests
				where replaySubRequest.State == ResubmitRequestState.Running
				select replaySubRequest)
				{
					if (num >= maxAllowedMessages)
					{
						break;
					}
					int num2 = this.ResubmitSubRequest(maxAllowedMessages - num, replayRequest);
					MessageResubmissionPerfCounters.Instance.UpdateResubmissionCount(num2, !ResubmitRequestFacade.IsPrimary(replayRequest));
					if (replayRequest.State == ResubmitRequestState.Completed)
					{
						MessageResubmissionPerfCounters.Instance.ChangeResubmitRequestState(replayRequest.State, ResubmitRequestState.Completed);
					}
					IReplayRequest replayRequest2 = replayRequest;
					replayRequest2.DiagnosticInformation += string.Format("(LastResubmissionTime: {0}; LastResubmittedMessageOriginalDeliveryTime: {1}; MessagesSubmitted: {2}; State: {3})", new object[]
					{
						DateTime.UtcNow,
						replayRequest.LastResubmittedMessageOrginalDeliveryTime,
						num2,
						replayRequest.State
					});
					ExTraceGlobals.MessageResubmissionTracer.TraceDebug<long, long, int>(replayRequest.PrimaryRequestId, "Messages submitted. primary requestId = {0} requestId = {1}, messagesSubmittedFromRequest = {2}", replayRequest.PrimaryRequestId, replayRequest.RequestId, num2);
					this.AddSystemProbeTrace("Messages resubmitted for request. Primary Request Id = {0}, Request Id = {1}, Total messages submitted = {2}, State = {3}", new object[]
					{
						replayRequest.PrimaryRequestId,
						replayRequest.RequestId,
						num2,
						replayRequest.State
					});
					replayRequest.Commit();
					num += num2;
				}
			}
			return num;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00034610 File Offset: 0x00032810
		public void Dispose()
		{
			foreach (IReplayRequest replayRequest in this.replaySubRequests)
			{
				replayRequest.Dispose();
			}
			this.replaySubRequests.Clear();
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0003467B File Offset: 0x0003287B
		public bool IsRunning()
		{
			return this.replaySubRequests.Any((IReplayRequest replayRequest) => replayRequest.State == ResubmitRequestState.Running);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x000346A8 File Offset: 0x000328A8
		internal static void StampResubmittedHeader(TransportMailItem mailItem, Destination primaryDestination, Guid correlationId)
		{
			Header header = Header.Create("X-MS-Exchange-Organization-ResubmittedMessage");
			header.Value = string.Format("CorrelationId={0};Destination=\"{1}\";Type={2}", correlationId, primaryDestination.ToString(), primaryDestination.Type);
			mailItem.RootPart.Headers.PrependChild(header);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x000346FC File Offset: 0x000328FC
		internal static bool TryParseResubmittedHeader(TransportMailItem mailItem, out Destination destination, out Guid correlationId)
		{
			correlationId = Guid.Empty;
			destination = null;
			Header header = mailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-ResubmittedMessage");
			if (header == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(header.Value))
			{
				ExTraceGlobals.MessageResubmissionTracer.TraceWarning<string>(0L, "Header {0} has no value", "X-MS-Exchange-Organization-ResubmittedMessage");
				return false;
			}
			Destination.DestinationType destinationType = Destination.DestinationType.Mdb;
			Guid empty = Guid.Empty;
			string text = string.Empty;
			foreach (object obj in ResubmitRequestFacade.RegexResubmittedHeaderParser.Matches(header.Value))
			{
				Match match = (Match)obj;
				string value = match.Groups["value"].Value;
				string value2;
				if ((value2 = match.Groups["name"].Value) != null)
				{
					if (value2 == "CorrelationId")
					{
						Guid.TryParse(value, out correlationId);
						continue;
					}
					if (value2 == "Destination")
					{
						Guid.TryParse(value, out empty);
						text = value;
						continue;
					}
					if (value2 == "Type")
					{
						Enum.TryParse<Destination.DestinationType>(value, out destinationType);
						continue;
					}
				}
				if (empty != Guid.Empty)
				{
					return false;
				}
				text = string.Format("{0};{1}={2}", text, match.Groups["name"].Value, value);
			}
			if (correlationId == Guid.Empty)
			{
				ExTraceGlobals.MessageResubmissionTracer.TraceWarning<string>(0L, "Header {0} does not contain a correlation id", "X-MS-Exchange-Organization-ResubmittedMessage");
				return false;
			}
			ExTraceGlobals.MessageResubmissionTracer.TraceDebug<string, Destination.DestinationType, Guid>(0L, "Resubmit header present - Destination = {0}; DestinationType = {1} and CorrelationId = {2}", text, destinationType, correlationId);
			if (destinationType == Destination.DestinationType.Conditional)
			{
				destination = new Destination(destinationType, text);
			}
			else if (empty == Guid.Empty)
			{
				destination = new Destination(Destination.DestinationType.Shadow, text);
			}
			else
			{
				destination = new Destination(Destination.DestinationType.Mdb, empty);
			}
			return true;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x000348F8 File Offset: 0x00032AF8
		private static void StampProbeTestResubmissionHeader(TransportMailItem mailItem)
		{
			Header header = Header.Create("X-MS-Exchange-Organization-TestResubmission");
			header.Value = true.ToString();
			mailItem.RootPart.Headers.PrependChild(header);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00034931 File Offset: 0x00032B31
		private static bool IsPrimary(IReplayRequest replaySubRequest)
		{
			return replaySubRequest.PrimaryRequestId == replaySubRequest.RequestId;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00034978 File Offset: 0x00032B78
		private int ResubmitSubRequest(int maxAllowedMessages, IReplayRequest replaySubRequest)
		{
			MailboxResubmitHelper resubmitHelper = new MailboxResubmitHelper(string.Format("{0};MessageResubmit", replaySubRequest.Destination));
			int num = 0;
			while (num < maxAllowedMessages && replaySubRequest.State == ResubmitRequestState.Running)
			{
				foreach (TransportMailItem tmi2 in from tmi in replaySubRequest.GetMessagesForRedelivery(maxAllowedMessages - num)
				where ResubmitRequestFacade.IsPrimary(replaySubRequest) || tmi.DateReceived <= this.primaryRequest.EndTime
				select tmi)
				{
					this.ResubmitMailItem(resubmitHelper, replaySubRequest, tmi2);
					num++;
					replaySubRequest.AddMailItemReference();
				}
			}
			return num;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00034ADC File Offset: 0x00032CDC
		private void ResubmitMailItem(MailboxResubmitHelper resubmitHelper, IReplayRequest replaySubRequest, TransportMailItem tmi)
		{
			ITimerCounter timerCounter = MessageResubmissionPerfCounters.Instance.ResubmitMessagesLatencyCounter();
			timerCounter.Start();
			Stopwatch stopwatch = Stopwatch.StartNew();
			resubmitHelper.Resubmit(tmi, delegate(TransportMailItem clonedTmi)
			{
				clonedTmi.BumpExpirationTime();
				clonedTmi.From = RoutingAddress.NullReversePath;
				clonedTmi.ResetToActive();
				clonedTmi.IsDiscardPending = false;
				clonedTmi.OnReleaseFromActive += delegate(TransportMailItem o)
				{
					replaySubRequest.ReleaseMailItemReference();
				};
				ResubmitRequestFacade.StampResubmittedHeader(clonedTmi, this.primaryRequest.Destination, this.correlationId);
				if (this.isTestRequest)
				{
					ResubmitRequestFacade.StampProbeTestResubmissionHeader(clonedTmi);
				}
			});
			ExTraceGlobals.MessageResubmissionTracer.TracePerformance<long>(tmi.MsgId, "Resubmit of TMI took {0}ms", stopwatch.ElapsedMilliseconds);
			timerCounter.Stop();
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00034B49 File Offset: 0x00032D49
		private void AddSystemProbeTrace(string format, params object[] args)
		{
			if (this.isTestRequest)
			{
				SystemProbeHelper.MessageResubmissionTracer.TracePass(this.correlationId, 0L, format, args);
			}
		}

		// Token: 0x04000603 RID: 1539
		internal const string ResubmittedHeader = "X-MS-Exchange-Organization-ResubmittedMessage";

		// Token: 0x04000604 RID: 1540
		internal const string TestResubmissionHeader = "X-MS-Exchange-Organization-TestResubmission";

		// Token: 0x04000605 RID: 1541
		internal const string ResubmittedHeaderCorrelationIdKey = "CorrelationId";

		// Token: 0x04000606 RID: 1542
		internal const string ResubmittedHeaderDestinationKey = "Destination";

		// Token: 0x04000607 RID: 1543
		internal const string ResubmittedHeaderDestinationTypeKey = "Type";

		// Token: 0x04000608 RID: 1544
		private const string ResubmittedHeaderFormat = "CorrelationId={0};Destination=\"{1}\";Type={2}";

		// Token: 0x04000609 RID: 1545
		private const string ResubmittedHeaderRegex = "(?<name>[^=]+)=(((?<value>[^\"]*?)($|;))|(\"(?<value>.*?)\"($|;)))";

		// Token: 0x0400060A RID: 1546
		private const string DiagnosticInfoUnavailableString = "Unavailable";

		// Token: 0x0400060B RID: 1547
		private readonly List<IReplayRequest> replaySubRequests = new List<IReplayRequest>();

		// Token: 0x0400060C RID: 1548
		private readonly ReaderWriterLockSlim syncLock = new ReaderWriterLockSlim();

		// Token: 0x0400060D RID: 1549
		private static readonly Regex RegexResubmittedHeaderParser = new Regex("(?<name>[^=]+)=(((?<value>[^\"]*?)($|;))|(\"(?<value>.*?)\"($|;)))", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x0400060E RID: 1550
		private readonly long requestId;

		// Token: 0x0400060F RID: 1551
		private readonly Guid correlationId;

		// Token: 0x04000610 RID: 1552
		private readonly bool isTestRequest;

		// Token: 0x04000611 RID: 1553
		private readonly IMessagingDatabase messagingDatabase;

		// Token: 0x04000612 RID: 1554
		private IReplayRequest primaryRequest;
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MailboxTransport.Shared.Providers;
using Microsoft.Exchange.MailboxTransport.Shared.Smtp;
using Microsoft.Exchange.MailboxTransport.Shared.SubmissionItem;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.MailboxTransport.StoreDriver.Shared;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000013 RID: 19
	internal class MailItemSubmitter : IMessageConverter, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000070 RID: 112 RVA: 0x0000523B File Offset: 0x0000343B
		public MailItemSubmitter(ulong submissionConnectionId, SubmissionInfo submissionInfo, SendAsManager sendAsManager, SubmissionPoisonHandler submissionPoisonHandler, SubmissionPoisonContext submissionPoisonContext, StoreDriverSubmission storeDriverSubmission) : this(submissionConnectionId, submissionInfo, submissionPoisonHandler, submissionPoisonContext, storeDriverSubmission.StoreDriverTracer)
		{
			ArgumentValidator.ThrowIfNull("sendAsManager", sendAsManager);
			ArgumentValidator.ThrowIfNull("storeDriverSubmission", storeDriverSubmission);
			this.sendAsManager = sendAsManager;
			this.storeDriverSubmission = storeDriverSubmission;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005278 File Offset: 0x00003478
		protected MailItemSubmitter(ulong submissionConnectionId, SubmissionInfo submissionInfo, SubmissionPoisonHandler submissionPoisonHandler, SubmissionPoisonContext submissionPoisonContext, IStoreDriverTracer storeDriverTracer)
		{
			ArgumentValidator.ThrowIfNull("submissionInfo", submissionInfo);
			ArgumentValidator.ThrowIfNull("submissionPoisonHandler", submissionPoisonHandler);
			ArgumentValidator.ThrowIfNull("submissionPoisonContext", submissionPoisonContext);
			ArgumentValidator.ThrowIfNull("storeDriverTracer", storeDriverTracer);
			this.Thread = Thread.CurrentThread;
			this.disposeTracker = ((IDisposeTrackable)this).GetDisposeTracker();
			this.SubmissionConnectionId = submissionConnectionId;
			this.SubmissionInfo = submissionInfo;
			this.Item = submissionInfo.CreateSubmissionItem(this);
			this.result = new MailSubmissionResult();
			this.traceFilter = default(MailItemTraceFilter);
			this.breadcrumb = new MailItemSubmitter.Breadcrumb(this.result);
			this.breadcrumb.MailboxServer = submissionInfo.MailboxFqdn;
			this.breadcrumb.MailboxDatabase = submissionInfo.MdbGuid;
			this.submissionPoisonHandler = submissionPoisonHandler;
			this.submissionPoisonContext = submissionPoisonContext;
			this.storeDriverTracer = storeDriverTracer;
			MapiSubmissionInfo mapiSubmissionInfo = submissionInfo as MapiSubmissionInfo;
			if (mapiSubmissionInfo != null)
			{
				this.breadcrumb.EventCounter = mapiSubmissionInfo.EventCounter;
				this.breadcrumb.MailboxGuid = mapiSubmissionInfo.MailboxGuid;
			}
			MailItemSubmitter.submitterHistory.Drop(this.breadcrumb);
			this.LatencyTracker = submissionInfo.LatencyTracker;
			this.breadcrumb.LatencyTracker = this.LatencyTracker;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000053B0 File Offset: 0x000035B0
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000053BD File Offset: 0x000035BD
		public uint ErrorCode
		{
			get
			{
				return this.result.ErrorCode;
			}
			set
			{
				this.result.ErrorCode = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000053CB File Offset: 0x000035CB
		public MailSubmissionResult Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000053D3 File Offset: 0x000035D3
		public string Description
		{
			get
			{
				return "Submit";
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000053DA File Offset: 0x000035DA
		public bool IsOutbound
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000053DD File Offset: 0x000035DD
		public long MessageSize
		{
			get
			{
				return this.messageSize;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000053E5 File Offset: 0x000035E5
		public Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MailboxTransportSubmissionServiceTracer;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000053EC File Offset: 0x000035EC
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000053F4 File Offset: 0x000035F4
		public Guid ExternalOrganizationId
		{
			get
			{
				return this.externalOrgId;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000053FC File Offset: 0x000035FC
		public int RecipientCount
		{
			get
			{
				return this.recipientCount;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00005404 File Offset: 0x00003604
		public StoreDriverSubmission StoreDriverSubmission
		{
			get
			{
				return this.storeDriverSubmission;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000540C File Offset: 0x0000360C
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00005414 File Offset: 0x00003614
		internal LatencyTracker LatencyTracker { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000541D File Offset: 0x0000361D
		internal TimeSpan RpcLatency
		{
			get
			{
				return this.rpcLatency;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00005425 File Offset: 0x00003625
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000542D File Offset: 0x0000362D
		internal DateTime StartTime { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00005436 File Offset: 0x00003636
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000543E File Offset: 0x0000363E
		internal SubmissionInfo SubmissionInfo { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00005447 File Offset: 0x00003647
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000544F File Offset: 0x0000364F
		internal SubmissionItem Item { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00005458 File Offset: 0x00003658
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00005460 File Offset: 0x00003660
		internal ulong SubmissionConnectionId { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00005469 File Offset: 0x00003669
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00005471 File Offset: 0x00003671
		internal MailItemSubmitter.SubmissionStage Stage { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000547A File Offset: 0x0000367A
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00005482 File Offset: 0x00003682
		internal Thread Thread { get; private set; }

		// Token: 0x0600008C RID: 140 RVA: 0x0000548C File Offset: 0x0000368C
		public void Dispose()
		{
			if (this.Item != null)
			{
				this.Item.Dispose();
			}
			this.traceFilter.Dispose();
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.mailItemToSubmit != null)
			{
				this.mailItemToSubmit.ReleaseFromActive();
				this.mailItemToSubmit = null;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000054EC File Offset: 0x000036EC
		public void HandlePoisonMessageNdrSubmission()
		{
			if (this.StoreDriverSubmission.IsSubmissionPaused)
			{
				this.ErrorCode = 2214592513U;
				return;
			}
			this.StartTime = DateTime.UtcNow;
			this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.PoisonHandledNdrStart);
			try
			{
				MessageStatus messageStatus = StorageExceptionHandler.RunUnderExceptionHandler(this, new StoreDriverDelegate(this.NdrForPoisonMessageWorker));
				if (messageStatus.Action == MessageAction.Skip)
				{
					StoreDriverSubmissionDatabasePerfCounters.IncrementSkippedSubmission(this.SubmissionInfo.DatabaseName, false);
					this.ErrorCode = 5U;
					this.AppendDiagnosticInfo(messageStatus.Response, this.Stage);
				}
			}
			finally
			{
				this.breadcrumb.Done = ExDateTime.UtcNow - this.breadcrumb.RecordCreation;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000055AC File Offset: 0x000037AC
		public void Submit()
		{
			if (this.StoreDriverSubmission.IsSubmissionPaused)
			{
				this.ErrorCode = 2214592513U;
				return;
			}
			this.StartTime = DateTime.UtcNow;
			this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.Start);
			PerformanceContext performanceContext = new PerformanceContext(PerformanceContext.Current);
			this.submissionExceptionAgentName = string.Empty;
			try
			{
				StoreDriverSubmissionDatabasePerfCounters.IncrementSubmissionAttempt(this.SubmissionInfo.MailboxFqdn, false);
				StoreDriverSubmissionDatabasePerfCounters.IncrementSubmissionAttempt(this.SubmissionInfo.DatabaseName, false);
				this.processingTerminated = false;
				MessageStatus messageStatus = StorageExceptionHandler.RunUnderExceptionHandler(this, new StoreDriverDelegate(this.SubmissionWorker));
				if (!this.processingTerminated)
				{
					StoreDriverSubmission.RecordExceptionForDiagnostics(messageStatus, this);
					ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(33632U, messageStatus.Action.ToString());
					if (messageStatus.Action == MessageAction.NDR)
					{
						ExTraceGlobals.FaultInjectionTracer.TraceTest(64000U);
						this.SubmitNdrForFailedSubmission(messageStatus.Response);
						this.AppendDiagnosticInfo(messageStatus.Response, this.Stage);
						StoreDriverSubmissionDatabasePerfCounters.IncrementSubmissionFailure(this.SubmissionInfo.DatabaseName, false);
					}
					else if (messageStatus.Action == MessageAction.Retry)
					{
						StoreDriverSubmissionDatabasePerfCounters.IncrementSubmissionFailure(this.SubmissionInfo.DatabaseName, false);
						this.ErrorCode = 1090519040U;
						this.AppendDiagnosticInfo(messageStatus.Response, this.Stage);
					}
					else if (messageStatus.Action == MessageAction.RetryQueue)
					{
						StoreDriverSubmissionDatabasePerfCounters.IncrementSubmissionFailure(this.SubmissionInfo.DatabaseName, false);
						this.ErrorCode = 1107296260U;
						this.AppendDiagnosticInfo(messageStatus.Response, this.Stage);
					}
					else if (messageStatus.Action == MessageAction.RetryMailboxServer)
					{
						StoreDriverSubmissionDatabasePerfCounters.IncrementSubmissionFailure(this.SubmissionInfo.DatabaseName, false);
						this.ErrorCode = 1140850693U;
						this.AppendDiagnosticInfo(messageStatus.Response, this.Stage);
					}
					else if (messageStatus.Action == MessageAction.Reroute)
					{
						StoreDriverSubmissionDatabasePerfCounters.IncrementSubmissionFailure(this.SubmissionInfo.DatabaseName, false);
						this.ErrorCode = 2214592516U;
						this.AppendDiagnosticInfo(messageStatus.Response, this.Stage);
					}
					else if (messageStatus.Action == MessageAction.Skip)
					{
						StoreDriverSubmissionDatabasePerfCounters.IncrementSkippedSubmission(this.SubmissionInfo.DatabaseName, false);
						this.ErrorCode = 16U;
						this.AppendDiagnosticInfo(messageStatus.Response, this.Stage);
					}
					else
					{
						if (messageStatus.Action == MessageAction.LogDuplicate)
						{
							throw new InvalidOperationException("LogDuplicate only applies to delivery.");
						}
						if (this.ErrorCode != 2684354560U && !HResult.IsHandled(this.ErrorCode))
						{
							throw new InvalidOperationException("error code is not set up correctly.");
						}
						if (this.ErrorCode != 0U)
						{
							StoreDriverSubmissionDatabasePerfCounters.IncrementSubmissionFailure(this.SubmissionInfo.DatabaseName, false);
						}
					}
					bool flag = this.ErrorCode != 0U && HResult.IsHandled(this.ErrorCode);
					if (flag && !string.IsNullOrEmpty(this.submissionExceptionAgentName))
					{
						StoreDriverSubmissionAgentPerfCounters.IncrementAgentSubmissionAttempt(this.submissionExceptionAgentName);
						StoreDriverSubmissionAgentPerfCounters.IncrementAgentSubmissionFailure(this.submissionExceptionAgentName);
						StoreDriverSubmissionAgentPerfCounters.RefreshAgentSubmissionPercentCounter(this.submissionExceptionAgentName);
					}
				}
			}
			catch (StoreDriverSubmissionRetiredException e)
			{
				this.ErrorCode = 2214592514U;
				this.AppendDiagnosticInfo(e, this.Stage);
			}
			finally
			{
				this.breadcrumb.Done = ExDateTime.UtcNow - this.breadcrumb.RecordCreation;
				TimeSpan timeSpan = PerformanceContext.Current.Latency - performanceContext.Latency;
				LatencyTracker.TrackExternalComponentLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionPerfContextLdap, this.LatencyTracker, timeSpan);
				this.breadcrumb.LdapLatency = timeSpan;
				this.TraceLatency(this.SubmissionInfo, "LDAP", timeSpan);
				if (this.Item != null && this.Item.Session != null)
				{
					CumulativeRPCPerformanceStatistics storeCumulativeRPCStats = this.Item.Session.GetStoreCumulativeRPCStats();
					LatencyTracker.TrackExternalComponentLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionStoreStats, this.LatencyTracker, storeCumulativeRPCStats.timeInServer);
					this.breadcrumb.StoreLatency = storeCumulativeRPCStats.timeInServer;
					this.TraceLatency(this.SubmissionInfo, "Store", storeCumulativeRPCStats.timeInServer);
				}
				if (this.mexSession != null)
				{
					try
					{
						this.agentLatencyTracker.Dispose();
						this.agentLatencyTracker = null;
					}
					finally
					{
						MExEvents.FreeExecutionContext(this.mexSession);
						this.mexSession = null;
					}
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000059EC File Offset: 0x00003BEC
		public void SubmitNdrForFailedSubmission(SmtpResponse response)
		{
			MessageStatus messageStatus = this.FailedSubmissionNdrWorker(response);
			switch (messageStatus.Action)
			{
			case MessageAction.Retry:
				this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass(this.storeDriverTracer.MessageProbeActivityId, 0L, "Failed to submit event due to transient problem. Need to retry mailbox.");
				this.ErrorCode = 1090519041U;
				return;
			case MessageAction.RetryQueue:
				this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass(this.storeDriverTracer.MessageProbeActivityId, 0L, "Failed to submit event due to transient problem. Need to retry mailbox database.");
				this.ErrorCode = 1107296262U;
				return;
			case MessageAction.NDR:
			case MessageAction.Skip:
				this.ErrorCode = 5U;
				this.LogFailedNdrGeneration(messageStatus.Exception);
				break;
			case MessageAction.Reroute:
				this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass(this.storeDriverTracer.MessageProbeActivityId, 0L, "Failed to submit event due to transient problem. Need to retry mailbox server.");
				this.ErrorCode = 1140850695U;
				return;
			case MessageAction.Throw:
			case MessageAction.LogDuplicate:
			case MessageAction.LogProcess:
				break;
			case MessageAction.RetryMailboxServer:
				this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass(this.storeDriverTracer.MessageProbeActivityId, 0L, "Failed to submit event due to transient problem. Need to retry other hub.");
				this.ErrorCode = 2214592517U;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005B00 File Offset: 0x00003D00
		public void LogMessage(Exception exception)
		{
			if (this.Item.HasMessageItem && !this.Item.Done)
			{
				ItemConversion.SaveFailedItem(this.Item.Item, this.Item.ConversionOptions, exception);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005B38 File Offset: 0x00003D38
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MailItemSubmitter>(this);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005B40 File Offset: 0x00003D40
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005B5C File Offset: 0x00003D5C
		internal static void WriteLamNotificationEvent(string stageString, string latency, string messageId, Guid mailboxGuid, TraceHelper.LamNotificationIdParts? lamNotificationIdParts)
		{
			if (lamNotificationIdParts == null)
			{
				StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_ErrorWritingToLamLamNotificationIdNotSet, null, new object[]
				{
					stageString,
					messageId,
					mailboxGuid
				});
				return;
			}
			try
			{
				EventNotificationItem eventNotificationItem = new EventNotificationItem(lamNotificationIdParts.Value.ServiceName, lamNotificationIdParts.Value.ComponentName, null, ResultSeverityLevel.Verbose);
				eventNotificationItem.StateAttribute1 = Convert.ToString(lamNotificationIdParts.Value.LamNotificationIdGuid);
				eventNotificationItem.AddCustomProperty("StateAttribute1", lamNotificationIdParts.Value.LamNotificationSequenceNumber);
				eventNotificationItem.AddCustomProperty("StateAttribute2", latency);
				eventNotificationItem.AddCustomProperty("StateAttribute3", messageId);
				eventNotificationItem.AddCustomProperty("StateAttribute4", mailboxGuid);
				eventNotificationItem.AddCustomProperty("StateAttribute5", stageString);
				eventNotificationItem.Publish(false);
			}
			catch (Exception ex)
			{
				StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_ErrorWritingToLam, null, new object[]
				{
					stageString,
					messageId,
					mailboxGuid,
					ex
				});
				throw;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005C80 File Offset: 0x00003E80
		internal static XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement("SubmissionHistory");
			foreach (MailItemSubmitter.Breadcrumb breadcrumb in ((IEnumerable<MailItemSubmitter.Breadcrumb>)MailItemSubmitter.submitterHistory))
			{
				xelement.Add(breadcrumb.GetDiagnosticInfo());
			}
			return xelement;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005CE4 File Offset: 0x00003EE4
		internal void TraceLatency(SubmissionInfo submissionInfo, string latencyType, TimeSpan latency)
		{
			MapiSubmissionInfo mapiSubmissionInfo = (MapiSubmissionInfo)submissionInfo;
			this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass(this.storeDriverTracer.MessageProbeActivityId, 0L, "Event {0}, Mailbox {1}, Mdb {2}, {3} latency {4}", new object[]
			{
				mapiSubmissionInfo.EventCounter,
				mapiSubmissionInfo.MailboxGuid,
				mapiSubmissionInfo.MdbGuid,
				latencyType,
				latency
			});
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005D5C File Offset: 0x00003F5C
		internal bool ShouldThrottleMessage(MapiSubmissionInfo mapiSubmissionInfo, IMessageThrottlingManager throttlingManager, ThrottlingPolicyCache throttlingPolicyCache)
		{
			if (mapiSubmissionInfo.SenderAdEntry != null)
			{
				switch ((Microsoft.Exchange.Data.Directory.Recipient.RecipientType)mapiSubmissionInfo.SenderAdEntry[ADRecipientSchema.RecipientType])
				{
				case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.PublicFolder:
				case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.SystemAttendantMailbox:
				case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.SystemMailbox:
				case Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MicrosoftExchange:
					return false;
				}
				ADObjectId throttlingPolicyId = (ADObjectId)mapiSubmissionInfo.SenderAdEntry[ADRecipientSchema.ThrottlingPolicy];
				IThrottlingPolicy throttlingPolicy = throttlingPolicyCache.Get(this.organizationId, throttlingPolicyId);
				int num = int.MaxValue;
				if (!throttlingPolicy.MessageRateLimit.IsUnlimited)
				{
					uint value = throttlingPolicy.MessageRateLimit.Value;
					if ((ulong)value < (ulong)((long)num))
					{
						num = (int)value;
					}
				}
				MessageThrottlingReason messageThrottlingReason = throttlingManager.ShouldThrottleMessage(mapiSubmissionInfo.MailboxGuid, num);
				if (messageThrottlingReason != MessageThrottlingReason.NotThrottled)
				{
					this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass<Guid, int>(this.storeDriverTracer.MessageProbeActivityId, 0L, "mailbox {0} submission has exceeded the throttling policy of {1} per minute.", mapiSubmissionInfo.MailboxGuid, num);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005E44 File Offset: 0x00004044
		internal void CreateMailItem(SubmissionInfo submissionInfo)
		{
			if (this.Item != null)
			{
				this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass<string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "create transport mailitem for message class {0}", this.Item.MessageClass);
			}
			MapiSubmissionInfo mapiSubmissionInfo = submissionInfo as MapiSubmissionInfo;
			ADRecipientCache<TransportMiniRecipient> adrecipientCache = null;
			if (mapiSubmissionInfo != null && mapiSubmissionInfo.SenderAdEntry != null)
			{
				SmtpAddress smtpAddress = (SmtpAddress)mapiSubmissionInfo.SenderAdEntry[ADRecipientSchema.PrimarySmtpAddress];
				if (smtpAddress.IsValidAddress)
				{
					ProxyAddress proxyAddress = new SmtpProxyAddress(smtpAddress.ToString(), true);
					Result<TransportMiniRecipient> result = new Result<TransportMiniRecipient>(mapiSubmissionInfo.SenderAdEntry, null);
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.organizationId), 936, "CreateMailItem", "f:\\15.00.1497\\sources\\dev\\MailboxTransport\\src\\MailboxTransportSubmission\\StoreDriverSubmission\\MailItemSubmitter.cs");
					adrecipientCache = new ADRecipientCache<TransportMiniRecipient>(tenantOrRootOrgRecipientSession, TransportMiniRecipientSchema.Properties, 1, this.organizationId);
					adrecipientCache.AddCacheEntry(proxyAddress, result);
				}
			}
			if (adrecipientCache == null)
			{
				adrecipientCache = new ADRecipientCache<TransportMiniRecipient>(TransportMiniRecipientSchema.Properties, 0, this.organizationId);
			}
			this.mailItemToSubmit = TransportMailItem.NewMailItem(adrecipientCache, LatencyComponent.MailboxTransportSubmissionStoreDriverSubmission, MailDirectionality.Originating, this.externalOrgId);
			this.mailItemToSubmit.LatencyTracker = this.LatencyTracker;
			if (this.Item != null)
			{
				if (ObjectClass.IsReport(this.Item.MessageClass, "NDR"))
				{
					this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass(this.storeDriverTracer.MessageProbeActivityId, 0L, "Message is a store ndr request");
					this.mailItemToSubmit.From = RoutingAddress.NullReversePath;
					this.CopyContentFromMapiToTransportMailItem(true);
					RoutingAddress routingAddress;
					if (!SubmissionItemUtils.TryGetRoutingAddressFromParticipant(this.mailItemToSubmit.ADRecipientCache, this.Item.Sender, "Sender", out routingAddress))
					{
						throw new InvalidSenderException(this.Item.Sender);
					}
					this.mailItemToSubmit.Recipients.Add((string)routingAddress);
					this.Item.DecorateMessage(this.mailItemToSubmit);
					Components.DsnGenerator.DecorateStoreNdr(this.mailItemToSubmit, routingAddress);
					MultilevelAuth.EnsureSecurityAttributes(this.mailItemToSubmit, SubmitAuthCategory.Internal, MultilevelAuthMechanism.SecureInternalSubmit, null);
				}
				else if (ObjectClass.IsOfClass(this.Item.MessageClass, "IPM.Note.StorageQuotaWarning"))
				{
					QuotaType quotaType;
					int currentSize;
					int? maxSize;
					int? localeId;
					string folderName;
					bool isPrimaryMailbox;
					bool isPublicFolderMailbox;
					this.GetQuotaProps(out quotaType, out currentSize, out maxSize, out localeId, out folderName, out isPrimaryMailbox, out isPublicFolderMailbox);
					this.mailItemToSubmit.CacheTransportSettings();
					this.mailItemToSubmit.From = RoutingAddress.NullReversePath;
					List<string> list = null;
					List<string> list2 = null;
					this.recipientCount = SubmissionItemUtils.CopyRecipientsTo(this.Item, this.mailItemToSubmit, new SubmissionRecipientHandler(this.RecipientHandler), ref list, ref list2);
					if (list != null)
					{
						this.AppendRecipientsListToDiagnosticInfo("Unresolved", list);
					}
					if (list2 != null)
					{
						this.AppendRecipientsListToDiagnosticInfo("NotResponsible", list2);
					}
					if (this.recipientCount == 0)
					{
						this.ErrorCode = 15U;
					}
					Components.DsnGenerator.CreateStoreQuotaMessageBody(this.mailItemToSubmit, quotaType, this.Item.MessageClass, localeId, currentSize, maxSize, folderName, isPrimaryMailbox, isPublicFolderMailbox);
					this.Item.DecorateMessage(this.mailItemToSubmit);
					MultilevelAuth.EnsureSecurityAttributes(this.mailItemToSubmit, SubmitAuthCategory.Internal, MultilevelAuthMechanism.SecureInternalSubmit, null);
				}
				else
				{
					this.CopyContentFromMapiToTransportMailItem(false);
					this.Item.DecorateMessage(this.mailItemToSubmit);
					this.Item.ApplySecurityAttributesTo(this.mailItemToSubmit);
					string refTypePropValue = this.Item.GetRefTypePropValue<string>(MessageItemSchema.DavSubmitData);
					if (!DavHeader.CopySenderAndRecipientsFromHeaders(refTypePropValue, this.mailItemToSubmit))
					{
						SubmissionItemUtils.CopySenderTo(this.Item, this.mailItemToSubmit);
						List<string> list3 = null;
						List<string> list4 = null;
						List<string> list5 = null;
						this.recipientCount = SubmissionItemUtils.CopyRecipientsTo(this.Item, this.mailItemToSubmit, new SubmissionRecipientHandler(this.RecipientHandler), ref list3, ref list4, this.ShouldUsePrSmtpEmailAddress(), out list5);
						if (list3 != null)
						{
							this.AppendRecipientsListToDiagnosticInfo("Unresolved", list3);
						}
						if (list4 != null)
						{
							this.AppendRecipientsListToDiagnosticInfo("NotResponsible", list4);
						}
						if (list5 != null && list5.Count > 0)
						{
							this.AppendRecipientsListToDiagnosticInfo("EscapedIMCEAAndUsedParticipantSmtpEmailAddress", list5);
						}
						if (this.recipientCount == 0)
						{
							this.ErrorCode = 15U;
						}
					}
					ClassificationUtils.PromoteStoreClassifications(this.mailItemToSubmit.RootPart.Headers);
				}
				SubmissionItemUtils.PatchQuarantineSender(this.mailItemToSubmit, this.Item.QuarantineOriginalSender);
				SmtpResponse response;
				if (!this.mailItemToSubmit.ValidateDeliveryPriority(out response))
				{
					throw new SmtpResponseException(response);
				}
				if (submissionInfo.ShouldDeprioritize)
				{
					Util.EncodeAndSetPriorityAsHeader(this.mailItemToSubmit.RootPart.Headers, DeliveryPriority.Low, "SenderExceededSubmissionRateLimit");
				}
				this.CopyHeaderFirewallExceptions();
				this.mailItemToSubmit.PerfCounterAttribution = "MAPISubmit";
			}
			this.messageSize = this.mailItemToSubmit.RefreshMimeSize();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000062C0 File Offset: 0x000044C0
		internal void SendMailItem(SubmissionReadOnlyMailItem mailItem, ISendAsSource subscription)
		{
			if (mailItem.Recipients.Count == 0)
			{
				this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass<MailItemSubmitter>(this.storeDriverTracer.MessageProbeActivityId, 0L, "No valid recipients for event {0}.", this);
				return;
			}
			string sourceContext;
			if (subscription != null)
			{
				sourceContext = string.Concat(new object[]
				{
					ConnectionLog.SessionIdToString(this.SubmissionConnectionId),
					";",
					subscription.UserEmailAddress,
					";",
					subscription.SourceGuid
				});
			}
			else
			{
				sourceContext = ConnectionLog.SessionIdToString(this.SubmissionConnectionId);
			}
			string text = string.Empty;
			byte[] entryId = MailItemSubmitter.EmptyByteArray;
			string mailboxDatabaseGuid = string.Empty;
			MapiSubmissionInfo mapiSubmissionInfo = this.SubmissionInfo as MapiSubmissionInfo;
			if (mapiSubmissionInfo != null)
			{
				text = mapiSubmissionInfo.GetSenderEmailAddress();
				entryId = mapiSubmissionInfo.EntryId;
				mailboxDatabaseGuid = mapiSubmissionInfo.MdbGuid.ToString();
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3106286909U, mapiSubmissionInfo.MdbGuid.ToString("N"));
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(4180028733U, mapiSubmissionInfo.MailboxFqdn);
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2768645437U, text);
			}
			MsgTrackReceiveInfo msgTrackInfo = new MsgTrackReceiveInfo(this.SubmissionInfo.NetworkAddress, this.SubmissionInfo.MailboxFqdn, StoreDriverSubmission.LocalIPAddress, sourceContext, mailItem.MailItem.MessageTrackingSecurityInfo, mailboxDatabaseGuid, text, entryId);
			LatencyHeaderManager.FinalizeLatencyHeadersOnMailboxTransportSubmission(mailItem.MailItem, this.SubmissionInfo.OriginalCreateTime, this.SubmissionInfo.MailboxFqdn);
			mailItem.MailItem.UpdateCachedHeaders();
			this.result.From = (string)mailItem.From;
			if (!string.IsNullOrEmpty(text))
			{
				this.result.Sender = text;
			}
			else
			{
				this.result.Sender = this.result.From;
			}
			this.result.Subject = mailItem.Subject;
			this.result.NetworkMessageId = mailItem.NetworkMessageId;
			MessageTrackingLog.TrackReceive(MessageTrackingSource.STOREDRIVER, mailItem.MailItem, this.result.MessageId, msgTrackInfo);
			SmtpMailItemResult smtpMailItemResult;
			try
			{
				this.WriteLamNotificationEvent(MailItemSubmitter.SubmissionStage.StartedSMTPOutOperation);
				smtpMailItemResult = OutProvider.SendMessage(mailItem);
				this.WriteLamNotificationEvent(MailItemSubmitter.SubmissionStage.FinishedSMTPOutOperation);
			}
			catch (Exception ex)
			{
				this.WriteLamNotificationEvent(MailItemSubmitter.SubmissionStage.SMTPOutThrewException);
				this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass<Exception>(this.storeDriverTracer.MessageProbeActivityId, 0L, "An exception was thrown when trying to send the message. Exception: {0}", ex);
				this.AppendDiagnosticInfo(ex, this.Stage);
				this.ErrorCode = 2684354560U;
				return;
			}
			if (smtpMailItemResult == null)
			{
				if (OutProvider.OutProviderInstance is PickupFolderOutProvider)
				{
					return;
				}
				this.AppendDiagnosticInfo(SmtpResponse.ConnectionTimedOut, this.Stage);
				this.ErrorCode = 2684354560U;
			}
			else
			{
				this.result.RemoteHostName = smtpMailItemResult.RemoteHostName;
				SmtpResponse smtpResponse = smtpMailItemResult.MessageResponse.SmtpResponse;
				if (smtpResponse.Equals(SmtpResponse.Empty))
				{
					SmtpResponse smtpResponse2 = smtpMailItemResult.ConnectionResponse.SmtpResponse;
					if (smtpResponse2.Equals(SmtpResponse.Empty))
					{
						this.AppendDiagnosticInfo(SmtpResponse.ConnectionTimedOut, this.Stage);
						this.ErrorCode = 2684354560U;
					}
					else
					{
						switch (smtpMailItemResult.ConnectionResponse.AckStatus)
						{
						case AckStatus.Success:
							this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TraceFail<long>(this.storeDriverTracer.MessageProbeActivityId, 0L, "When MessageResponse is empty, ConnectionResponse cannot be Success unless there were no messages in this connection", mailItem.RecordId);
							break;
						case AckStatus.Retry:
							this.AppendDiagnosticInfo(smtpMailItemResult.ConnectionResponse.SmtpResponse, this.Stage);
							this.ErrorCode = 2684354560U;
							break;
						case AckStatus.Fail:
							if ((mailItem.MailItemType & MailItemType.ActualMessage) == MailItemType.ActualMessage)
							{
								SmtpResponse smtpResponse3 = smtpMailItemResult.ConnectionResponse.SmtpResponse;
								if (smtpResponse3.Equals(AckReason.MessageIsPoisonForRemoteServer))
								{
									this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TraceFail<long>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Poison Message for HUB is detected - message's recordId is: {0}", mailItem.RecordId);
									this.ErrorCode = 3U;
								}
								else
								{
									this.SubmitNdrForFailedSubmission(smtpMailItemResult.ConnectionResponse.SmtpResponse);
								}
							}
							else if ((mailItem.MailItemType & MailItemType.MainMessage) == MailItemType.MainMessage)
							{
								this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TraceFail<long>(this.storeDriverTracer.MessageProbeActivityId, 0L, "PermanentNDRGenerationFailure for message with recordId {0}", mailItem.RecordId);
								this.AppendDiagnosticInfo(smtpMailItemResult.ConnectionResponse.SmtpResponse, this.Stage);
								this.ErrorCode = 5U;
							}
							break;
						default:
							throw new InvalidOperationException(string.Format("AckConnection with status: {0} is invalid", smtpMailItemResult.MessageResponse.AckStatus));
						}
					}
				}
				else
				{
					switch (smtpMailItemResult.MessageResponse.AckStatus)
					{
					case AckStatus.Success:
						if ((mailItem.MailItemType & MailItemType.OtherMessage) == MailItemType.OtherMessage)
						{
							this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass<long>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Sent other message {0}", mailItem.RecordId);
						}
						else
						{
							if ((mailItem.MailItemType & MailItemType.ActualMessage) == MailItemType.ActualMessage)
							{
								List<DsnRecipientInfo> list = new List<DsnRecipientInfo>();
								foreach (MailRecipient mailRecipient in smtpMailItemResult.RecipientResponses.Keys)
								{
									if (smtpMailItemResult.RecipientResponses[mailRecipient].AckStatus == AckStatus.Fail)
									{
										list.Add(DsnGenerator.CreateDsnRecipientInfo(null, mailRecipient.Email.ToString(), null, AckReason.OutboundInvalidAddress));
									}
								}
								this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass<long>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Sent actual message {0}", mailItem.RecordId);
								this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePfdPass<int, long>(this.storeDriverTracer.MessageProbeActivityId, 0L, "PFD ESD {0} Submitting mailitem {1} to the submit queue via SMTP", 25499, mailItem.RecordId);
								this.SubmitNdrForInvalidRecipients(mailItem, list);
							}
							else if ((mailItem.MailItemType & MailItemType.MainMessage) == MailItemType.MainMessage)
							{
								this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass<long>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Sent main NDR message {0}", mailItem.RecordId);
								this.ErrorCode = 1U;
							}
							ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(4045811005U, (this.mailItemToSubmit != null) ? this.mailItemToSubmit.Subject : null);
						}
						break;
					case AckStatus.Retry:
					{
						int num = 0;
						int num2 = 0;
						if (smtpMailItemResult.RecipientResponses != null)
						{
							foreach (MailRecipient key in smtpMailItemResult.RecipientResponses.Keys)
							{
								if (smtpMailItemResult.RecipientResponses[key].AckStatus == AckStatus.Fail)
								{
									num++;
								}
								else if (smtpMailItemResult.RecipientResponses[key].AckStatus == AckStatus.Retry)
								{
									num2++;
								}
							}
						}
						ISystemProbeTrace mapiStoreDriverSubmissionTracer = this.storeDriverTracer.MapiStoreDriverSubmissionTracer;
						Guid messageProbeActivityId = this.storeDriverTracer.MessageProbeActivityId;
						long etlTraceId = 0L;
						string formatString = "SMTP returned Retry for the message with RecordId{0}. FailedRecipientCount:{1}; RetryRecipientCount:{2}; Reason{3}";
						object[] array = new object[4];
						array[0] = mailItem.RecordId;
						array[1] = num;
						array[2] = num2;
						object[] array2 = array;
						int num3 = 3;
						SmtpResponse smtpResponse4 = smtpMailItemResult.MessageResponse.SmtpResponse;
						array2[num3] = smtpResponse4.StatusText;
						mapiStoreDriverSubmissionTracer.TracePass(messageProbeActivityId, etlTraceId, formatString, array);
						this.AppendDiagnosticInfo(smtpMailItemResult.MessageResponse.SmtpResponse, string.Format(CultureInfo.InvariantCulture, "FailedRecipientCount:{0}; RetryRecipientCount:{1}", new object[]
						{
							num,
							num2
						}), this.Stage);
						this.ErrorCode = 2684354560U;
						return;
					}
					case AckStatus.Fail:
						if ((mailItem.MailItemType & MailItemType.ActualMessage) == MailItemType.ActualMessage)
						{
							SmtpResponse smtpResponse5 = smtpMailItemResult.MessageResponse.SmtpResponse;
							if (smtpResponse5.Equals(AckReason.MessageIsPoisonForRemoteServer))
							{
								this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TraceFail<long>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Poison Message for HUB is detected - message's recordId is: {0}", mailItem.RecordId);
								this.ErrorCode = 3U;
							}
							else
							{
								this.SubmitNdrForFailedSubmission(smtpMailItemResult.MessageResponse.SmtpResponse);
							}
						}
						else if ((mailItem.MailItemType & MailItemType.MainMessage) == MailItemType.MainMessage)
						{
							this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TraceFail<long>(this.storeDriverTracer.MessageProbeActivityId, 0L, "PermanentNDRGenerationFailure for message with recordId {0}", mailItem.RecordId);
							this.AppendDiagnosticInfo(smtpMailItemResult.MessageResponse.SmtpResponse, this.Stage);
							this.ErrorCode = 5U;
						}
						break;
					default:
						throw new InvalidOperationException(string.Format("AckMessage with status: {0} is invalid", smtpMailItemResult.MessageResponse.AckStatus));
					}
				}
			}
			if (this.ErrorCode == 3U)
			{
				MessageTrackingLog.TrackPoisonMessage(MessageTrackingSource.STOREDRIVER, mailItem.MailItem, this.result.MessageId, msgTrackInfo);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006B5C File Offset: 0x00004D5C
		internal void AddRpcLatency(TimeSpan additionalLatency, string rpcType)
		{
			this.rpcLatency += additionalLatency;
			this.breadcrumb.RpcLatency = this.rpcLatency;
			this.TraceLatency(this.SubmissionInfo, rpcType, additionalLatency);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006B90 File Offset: 0x00004D90
		protected void NdrForPoisonMessageWorker()
		{
			try
			{
				this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.LoadADEntry);
				MapiSubmissionInfo mapiSubmissionInfo = this.SubmissionInfo as MapiSubmissionInfo;
				if (mapiSubmissionInfo == null)
				{
					this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TraceFail(this.storeDriverTracer.MessageProbeActivityId, 0L, "mapiSubmissionInfo is null");
					this.ErrorCode = 5U;
					return;
				}
				this.LoadAdEntry(mapiSubmissionInfo);
				this.organizationId = this.SubmissionInfo.GetOrganizationId();
				this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.LoadItem);
				uint errorCode;
				if (!this.LoadItemFromStore(out errorCode))
				{
					this.ErrorCode = errorCode;
					return;
				}
				this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.GenerateNdr);
				this.SubmitNdrForFailedSubmission(this.submissionPoisonHandler.GetPoisonHandledSmtpResponse(mapiSubmissionInfo.EventCounter));
				this.submissionPoisonHandler.UpdatePoisonNdrSentToTrue(this.submissionPoisonContext);
			}
			catch (Exception ex)
			{
				this.AppendDiagnosticInfo(ex, this.Stage);
				this.SendInformationalWatson(ex, this.result.DiagnosticInfo);
				throw new PoisonHandlerNdrGenerationErrorException(ex);
			}
			this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.DoneWithMessage);
			this.CallDoneWithMessage();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006CB8 File Offset: 0x00004EB8
		protected virtual void SendInformationalWatson(Exception exception, string detailedInformation = null)
		{
			if (string.IsNullOrEmpty(detailedInformation))
			{
				detailedInformation = "Not Applicable";
			}
			StoreDriverUtils.SendInformationalWatson(exception, detailedInformation);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006DA8 File Offset: 0x00004FA8
		protected virtual MessageStatus FailedSubmissionNdrWorker(SmtpResponse failureSmtpResponse)
		{
			MessageStatus messageStatus = StorageExceptionHandler.RunUnderExceptionHandler(this, delegate
			{
				this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass<string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Submitting NDR for failed event {0}", this.SubmissionInfo.ToString());
				if (!this.Item.HasMessageItem)
				{
					this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass<string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "No message item for event {0}. NDR will not be generated.", this.SubmissionInfo.ToString());
					this.ErrorCode = 2U;
					return;
				}
				TransportMailItem ndrMailItem = this.GenerateNdrMailItem();
				ConfigurationProvider.SendNDRForFailedSubmission(ndrMailItem, failureSmtpResponse, new DsnMailOutHandlerDelegate(this.SendMainDsnMailToOutProvider));
			});
			StoreDriverSubmission.RecordExceptionForDiagnostics(messageStatus, this);
			this.AppendDiagnosticInfo(messageStatus.Response, MailItemSubmitter.SubmissionStage.GenerateNdr);
			return messageStatus;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006DF4 File Offset: 0x00004FF4
		protected virtual void LoadAdEntry(MapiSubmissionInfo mapiSubmissionInfo)
		{
			try
			{
				LatencyTracker.BeginTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionAD, this.LatencyTracker);
				mapiSubmissionInfo.LoadAdRawEntry();
			}
			finally
			{
				LatencyTracker.EndTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionAD, this.LatencyTracker);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006E38 File Offset: 0x00005038
		protected virtual bool LoadItemFromStore(out uint loadResult)
		{
			loadResult = this.Item.LoadFromStore();
			if (!this.Item.HasMessageItem)
			{
				this.ErrorCode = loadResult;
				return false;
			}
			return true;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006E5F File Offset: 0x0000505F
		protected virtual void LogFailedNdrGeneration(Exception exception)
		{
			this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass<string, Exception>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Failed to generate NDR for ({0}) due to {1}", this.SubmissionInfo.ToString(), exception);
			this.SubmissionInfo.LogEvent(SubmissionInfo.Event.FailedToGenerateNdrInSubmission, exception);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006E9C File Offset: 0x0000509C
		protected virtual void CallDoneWithMessage()
		{
			try
			{
				Exception ex = this.Item.DoneWithMessage();
				if (ex != null)
				{
					this.AppendDiagnosticInfo(ex, this.Stage);
				}
			}
			catch (Exception e)
			{
				this.AppendDiagnosticInfo(e, this.Stage);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006EE8 File Offset: 0x000050E8
		private static DsnRecipientInfo CreateDsnRecipientInfo(Participant participant)
		{
			if (participant != null)
			{
				return DsnGenerator.CreateDsnRecipientInfo(participant.DisplayName, participant.EmailAddress, participant.RoutingType, AckReason.OutboundInvalidAddress);
			}
			return DsnGenerator.CreateDsnRecipientInfo(string.Empty, string.Empty, string.Empty, AckReason.OutboundInvalidAddress);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006F34 File Offset: 0x00005134
		private void SubmissionWorker()
		{
			this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePfdPass<int, string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "PFD ESD {0} Submitting event {1}", 23451, this.SubmissionInfo.ToString());
			this.StoreDriverSubmission.ThrowIfStopped();
			this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.LoadADEntry);
			MapiSubmissionInfo mapiSubmissionInfo = this.SubmissionInfo as MapiSubmissionInfo;
			this.LoadAdEntry(mapiSubmissionInfo);
			this.organizationId = this.SubmissionInfo.GetOrganizationId();
			this.result.OrganizationId = this.organizationId;
			if (this.SubmissionInfo.TenantHint != null)
			{
				this.externalOrgId = this.SubmissionInfo.TenantHint.GetExternalDirectoryOrganizationId();
			}
			else
			{
				this.externalOrgId = ((this.organizationId == OrganizationId.ForestWideOrgId) ? TenantPartitionHint.ExternalDirectoryOrganizationIdForRootOrg : ADAccountPartitionLocator.GetExternalDirectoryOrganizationIdByTenantName(this.organizationId.OrganizationalUnit.Name, this.organizationId.PartitionId));
			}
			this.result.ExternalOrganizationId = this.externalOrgId;
			this.result.Sender = mapiSubmissionInfo.GetSenderEmailAddress();
			this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.CreateMexSession);
			try
			{
				this.mexSession = MExEvents.GetExecutionContext(StoreDriverServer.GetInstance(this.organizationId));
				this.mexSession.Dispatcher.OnAgentInvokeEnd += new AgentInvokeEndHandler(this.AgentInvokeEndHandler);
			}
			catch (DataSourceTransientException ex)
			{
				this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TraceFail<string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Failed to get agent execution context: {0}", ex.ToString());
				this.ErrorCode = 2214592516U;
				this.AppendDiagnosticInfo(ex, this.Stage);
				this.processingTerminated = true;
				return;
			}
			this.agentLatencyTracker = new AgentLatencyTracker(this.mexSession);
			this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.CheckThrottle);
			if (mapiSubmissionInfo.ShouldThrottle)
			{
				this.ErrorCode = 11U;
				this.result.DiagnosticInfo = "Message throttling";
			}
			else
			{
				this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.LoadItem);
				uint errorCode;
				if (!this.LoadItemFromStore(out errorCode))
				{
					this.ErrorCode = errorCode;
					return;
				}
				this.ExtractLAMNotificationId();
				this.WriteLamNotificationEvent(MailItemSubmitter.SubmissionStage.LoadItem);
				this.result.MessageId = this.Item.Item.InternetMessageId;
				ISendAsSource sendAsSource = null;
				this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.SendAsCheck);
				ExDateTime utcNow = ExDateTime.UtcNow;
				try
				{
					StoreProvider.TryGetSendAsSubscription(this.Item.Item, this.sendAsManager, out sendAsSource);
				}
				finally
				{
					TimeSpan additionalLatency = ExDateTime.UtcNow - utcNow;
					this.AddRpcLatency(additionalLatency, "Send as");
				}
				this.WriteLamNotificationEvent(MailItemSubmitter.SubmissionStage.SendAsCheck);
				try
				{
					this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.CreateMailItem);
					this.StoreDriverSubmission.ThrowIfStopped();
					this.CreateMailItem(this.SubmissionInfo);
					this.WriteLamNotificationEvent(MailItemSubmitter.SubmissionStage.CreateMailItem);
					this.SetSystemProbeIdOnTmi();
					List<string> list = new List<string>(this.mailItemToSubmit.Recipients.CountUnprocessed());
					foreach (MailRecipient mailRecipient in this.mailItemToSubmit.Recipients)
					{
						list.Add(mailRecipient.Email.ToString());
					}
					this.result.RecipientAddresses = list.ToArray();
					if (sendAsSource != null)
					{
						this.sendAsManager.SaveSubscriptionProperties(sendAsSource, this.mailItemToSubmit.ExtendedProperties);
					}
					this.mailItemToSubmit.DateReceived = this.StartTime;
				}
				catch (InvalidSenderException ex2)
				{
					this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TraceFail<InvalidSenderException>(this.storeDriverTracer.MessageProbeActivityId, 0L, "invalid sender {0}. Event will be skipped", ex2);
					this.ErrorCode = 7U;
					this.SubmissionInfo.LogEvent(SubmissionInfo.Event.InvalidSender);
					this.AppendDiagnosticInfo(ex2, this.Stage);
					return;
				}
				catch (InvalidQuotaWarningMessageException ex3)
				{
					this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TraceFail<InvalidQuotaWarningMessageException>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Quota warning message is invalid :{0}.", ex3);
					this.ErrorCode = 10U;
					this.AppendDiagnosticInfo(ex3, this.Stage);
					return;
				}
				catch (NdrItemToTransportItemCopyException ex4)
				{
					this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TraceFail<Exception>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Failed to convert MAPI item to Transport Item during NDR processing:{0}.", ex4.InnerException);
					this.ErrorCode = 5U;
					this.SubmissionInfo.LogEvent(SubmissionInfo.Event.FailedToGenerateNdrInSubmission);
					this.AppendDiagnosticInfo(ex4, this.Stage);
					throw;
				}
				catch (NonNdrItemToTransportItemCopyException ex5)
				{
					this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TraceFail<Exception>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Failed to convert MAPI item to Transport Item during non NDR processing:{0}.", ex5.InnerException);
					this.AppendDiagnosticInfo(ex5, this.Stage);
					throw;
				}
				try
				{
					this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.OnDemotedEvent);
					this.StoreDriverSubmission.ThrowIfStopped();
					this.agentLatencyTracker.BeginTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionOnDemotedMessage, this.mailItemToSubmit.LatencyTracker);
					MExEvents.RaiseEvent(this.mexSession, "OnDemotedMessage", new object[]
					{
						new StoreDriverSubmissionEventSourceImpl(),
						new StoreDriverSubmissionEventArgsImpl(new TransportMailItemWrapper(this.mailItemToSubmit, false), this.Item, this)
					});
					try
					{
						ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3808832829U, "OnDemotedMessage");
					}
					catch (LocalizedException actualAgentException)
					{
						throw new StoreDriverAgentRaisedException("Fault injection.", actualAgentException);
					}
				}
				catch (StoreDriverAgentRaisedException ex6)
				{
					this.submissionExceptionAgentName = ex6.AgentName;
					throw;
				}
				finally
				{
					this.agentLatencyTracker.EndTrackLatency();
				}
				this.WriteLamNotificationEvent(MailItemSubmitter.SubmissionStage.OnDemotedEvent);
				this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.SubmitNdrForInvalidRecipients);
				this.StoreDriverSubmission.ThrowIfStopped();
				SubmissionReadOnlyMailItem submissionReadOnlyMailItem = new SubmissionReadOnlyMailItem(this.mailItemToSubmit, MailItemType.ActualMessage | MailItemType.MainMessage);
				this.SubmitNdrForInvalidRecipients(submissionReadOnlyMailItem, this.invalidRecipients);
				if (this.ErrorCode != 0U && this.ErrorCode != 0U && this.ErrorCode != 15U)
				{
					return;
				}
				this.WriteLamNotificationEvent(MailItemSubmitter.SubmissionStage.SubmitNdrForInvalidRecipients);
				this.StoreDriverSubmission.ThrowIfStopped();
				this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.CommitMailItem);
				this.traceFilter.SetMailItem(this.mailItemToSubmit);
				this.WriteLamNotificationEvent(MailItemSubmitter.SubmissionStage.CommitMailItem);
				this.SendMailItem(submissionReadOnlyMailItem, sendAsSource);
				if (HResult.IsMessageSubmittedOrHasNoRcpts(this.result.ErrorCode))
				{
					this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.SubmitMailItem);
					MSExchangeStoreDriverSubmission.SubmittedMailItems.Increment();
					this.WriteLamNotificationEvent(MailItemSubmitter.SubmissionStage.SubmitMailItem);
					this.Stage = this.breadcrumb.SetStage(MailItemSubmitter.SubmissionStage.DoneWithMessage);
					this.CallDoneWithMessage();
					this.WriteLamNotificationEvent(MailItemSubmitter.SubmissionStage.DoneWithMessage);
				}
				return;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000075EC File Offset: 0x000057EC
		private TimeSpan CopyContentFromMapiToTransportMailItem(bool isNdr)
		{
			TimeSpan timeSpan;
			try
			{
				LatencyTracker.BeginTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionContentConversion, this.LatencyTracker);
				this.Item.ConversionOptions.EnableCalendarHeaderGeneration = SubmissionConfiguration.Instance.App.EnableCalendarHeaderCreation;
				timeSpan = this.Item.CopyContentTo(this.mailItemToSubmit);
			}
			catch (NotSupportedException innerException)
			{
				if (isNdr)
				{
					throw new NdrItemToTransportItemCopyException(innerException);
				}
				throw new NonNdrItemToTransportItemCopyException(innerException);
			}
			finally
			{
				LatencyTracker.EndTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionContentConversion, this.LatencyTracker);
			}
			return timeSpan;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007678 File Offset: 0x00005878
		private void AgentInvokeEndHandler(object dispatcher, IMExSession session)
		{
			StoreDriverSubmissionAgentPerfCounters.IncrementAgentSubmissionAttempt(session.CurrentAgent.Name);
			StoreDriverSubmissionAgentPerfCounters.RefreshAgentSubmissionPercentCounter(session.CurrentAgent.Name);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000769C File Offset: 0x0000589C
		private void SetSystemProbeIdOnTmi()
		{
			if (this.storeDriverTracer.IsMessageAMapiSubmitSystemProbe)
			{
				this.mailItemToSubmit.SystemProbeId = this.storeDriverTracer.MessageProbeActivityId;
				return;
			}
			if (this.storeDriverTracer.IsMessageAMapiSubmitLAMProbe && this.storeDriverTracer.MessageProbeLamNotificationIdParts != null)
			{
				this.mailItemToSubmit.SystemProbeId = this.storeDriverTracer.MessageProbeLamNotificationIdParts.Value.LamNotificationIdGuid;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007718 File Offset: 0x00005918
		private void ExtractLAMNotificationId()
		{
			try
			{
				if (ObjectClass.IsOfClass(this.Item.Item.ClassName, "IPM.Note.MapiSubmitLAMProbe"))
				{
					object obj = this.Item.Item.TryGetProperty(MessageItemSchema.MapiSubmitLamNotificationId);
					if (obj != null && !(obj is PropertyError))
					{
						string lamNotificationId = obj.ToString();
						this.storeDriverTracer.MessageProbeLamNotificationIdParts = new TraceHelper.LamNotificationIdParts?(this.storeDriverTracer.Parse(lamNotificationId));
					}
					else
					{
						StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_LamNotificationIdNotSetOnMessage, null, new object[]
						{
							this.Item.Item.InternetMessageId,
							((MapiSubmissionInfo)this.SubmissionInfo).MailboxGuid
						});
					}
				}
			}
			catch (Exception ex)
			{
				StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_ErrorParsingLamNotificationId, null, new object[]
				{
					(this.Item == null) ? string.Empty : ((this.Item.Item == null) ? string.Empty : this.Item.Item.InternetMessageId),
					((MapiSubmissionInfo)this.SubmissionInfo).MailboxGuid,
					ex
				});
				throw;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000784C File Offset: 0x00005A4C
		private void WriteLamNotificationEvent(MailItemSubmitter.SubmissionStage stage)
		{
			if (this.storeDriverTracer.IsMessageAMapiSubmitLAMProbe)
			{
				MailItemSubmitter.WriteLamNotificationEvent(stage.ToString(), string.Empty, (this.mailItemToSubmit == null) ? string.Empty : this.mailItemToSubmit.InternetMessageId, ((MapiSubmissionInfo)this.SubmissionInfo).MailboxGuid, this.storeDriverTracer.MessageProbeLamNotificationIdParts);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000078B0 File Offset: 0x00005AB0
		private void SubmitNdrForInvalidRecipients(SubmissionReadOnlyMailItem mainMessage, List<DsnRecipientInfo> invalidRecipients)
		{
			if (invalidRecipients.Count == 0)
			{
				return;
			}
			this.storeDriverTracer.MapiStoreDriverSubmissionTracer.TracePass<int>(this.storeDriverTracer.MessageProbeActivityId, 0L, "create NDR for {0} invalid recipients", invalidRecipients.Count);
			ConfigurationProvider.SendNDRForInvalidAddresses(mainMessage.MailItem, invalidRecipients, new DsnMailOutHandlerDelegate(this.SendOtherDsnMailToOutProvider));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00007908 File Offset: 0x00005B08
		private long CalculateContentIntegrityHash()
		{
			long num = (long)this.Item.Item.Subject.Length << 48;
			if (this.Item.ConversionResult != null && 0L < this.Item.ConversionResult.BodySize)
			{
				num += this.Item.ConversionResult.AccumulatedAttachmentSize;
				num += (long)this.Item.ConversionResult.AttachmentCount << 56;
				num += this.Item.ConversionResult.BodySize << 24;
				num += (long)this.Item.ConversionResult.RecipientCount << 40;
			}
			return num;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000079A8 File Offset: 0x00005BA8
		private void SendOtherDsnMailToOutProvider(TransportMailItem ndrTransportMailItem)
		{
			this.SendMailItem(new SubmissionReadOnlyMailItem(ndrTransportMailItem, MailItemType.OtherMessage), null);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000079B8 File Offset: 0x00005BB8
		private void SendMainDsnMailToOutProvider(TransportMailItem ndrTransportMailItem)
		{
			this.SendMailItem(new SubmissionReadOnlyMailItem(ndrTransportMailItem, MailItemType.MainMessage), null);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000079C8 File Offset: 0x00005BC8
		private TransportMailItem GenerateNdrMailItem()
		{
			TransportMailItem transportMailItem;
			if (this.mailItemToSubmit != null)
			{
				transportMailItem = TransportMailItem.NewSideEffectMailItem(this.mailItemToSubmit, this.organizationId, LatencyComponent.MailboxTransportSubmissionStoreDriverSubmission, MailDirectionality.Originating, this.ExternalOrganizationId);
			}
			else
			{
				transportMailItem = TransportMailItem.NewMailItem(this.organizationId, LatencyComponent.MailboxTransportSubmissionStoreDriverSubmission, MailDirectionality.Originating, this.ExternalOrganizationId);
			}
			transportMailItem.PerfCounterAttribution = "MAPISubmitNdr";
			Stream stream = transportMailItem.OpenMimeWriteStream();
			stream.Close();
			SubmissionItemUtils.CopySenderTo(this.Item, transportMailItem);
			this.Item.DecorateMessage(transportMailItem);
			string refTypePropValue = this.Item.GetRefTypePropValue<string>(ItemSchema.InternetMessageId);
			if (!string.IsNullOrEmpty(refTypePropValue))
			{
				Header header = Header.Create(HeaderId.MessageId);
				header.Value = refTypePropValue;
				transportMailItem.RootPart.Headers.AppendChild(header);
				if (refTypePropValue != null && refTypePropValue.Length <= 100)
				{
					transportMailItem.EnvId = refTypePropValue;
				}
			}
			string refTypePropValue2 = this.Item.GetRefTypePropValue<string>(ItemSchema.Subject);
			if (refTypePropValue2 != null)
			{
				transportMailItem.Message.Subject = refTypePropValue2;
				transportMailItem.Subject = refTypePropValue2;
			}
			transportMailItem.CacheTransportSettings();
			return transportMailItem;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00007AC0 File Offset: 0x00005CC0
		private void GetQuotaProps(out QuotaType quotaType, out int currentSize, out int? mailboxMax, out int? localeId, out string folderName, out bool isPrimaryMailbox, out bool isPublicFolderMailbox)
		{
			currentSize = 0;
			mailboxMax = null;
			folderName = null;
			localeId = null;
			isPrimaryMailbox = true;
			isPublicFolderMailbox = false;
			int? valueTypePropValue = this.Item.GetValueTypePropValue<int>(ItemSchema.QuotaType);
			if (valueTypePropValue == null)
			{
				InvalidQuotaWarningMessageException.ThrowExceptionForMissingProp(ItemSchema.QuotaType.Name);
			}
			quotaType = (QuotaType)valueTypePropValue.Value;
			bool? valueTypePropValue2 = this.Item.GetValueTypePropValue<bool>(ItemSchema.IsPublicFolderQuotaMessage);
			if (valueTypePropValue2 == null)
			{
				InvalidQuotaWarningMessageException.ThrowExceptionForMissingProp(ItemSchema.IsPublicFolderQuotaMessage.Name);
			}
			isPublicFolderMailbox = valueTypePropValue2.Value;
			bool? valueTypePropValue3 = this.Item.GetValueTypePropValue<bool>(ItemSchema.PrimaryMbxOverQuota);
			if (valueTypePropValue3 == null)
			{
				InvalidQuotaWarningMessageException.ThrowExceptionForMissingProp(ItemSchema.PrimaryMbxOverQuota.Name);
			}
			isPrimaryMailbox = valueTypePropValue3.Value;
			int? valueTypePropValue4 = this.Item.GetValueTypePropValue<int>(ItemSchema.ExcessStorageUsed);
			if (valueTypePropValue4 == null)
			{
				InvalidQuotaWarningMessageException.ThrowExceptionForMissingProp(ItemSchema.ExcessStorageUsed.Name);
			}
			int? valueTypePropValue5 = this.Item.GetValueTypePropValue<int>(ItemSchema.StorageQuotaLimit);
			if (valueTypePropValue5 == null)
			{
				InvalidQuotaWarningMessageException.ThrowExceptionForMissingProp(ItemSchema.StorageQuotaLimit.Name);
			}
			if (this.Item.Sender != null)
			{
				InvalidQuotaWarningMessageException.ThrowExceptionForUnexpectedProp("Sender");
			}
			currentSize = valueTypePropValue4.Value + valueTypePropValue5.Value;
			int? valueTypePropValue6 = this.Item.GetValueTypePropValue<int>(ItemSchema.QuotaProhibitSend);
			int? valueTypePropValue7 = this.Item.GetValueTypePropValue<int>(ItemSchema.QuotaProhibitReceive);
			localeId = this.Item.GetValueTypePropValue<int>(MessageItemSchema.MessageLocaleId);
			switch (quotaType)
			{
			case QuotaType.StorageWarningLimit:
			case QuotaType.StorageOverQuotaLimit:
			case QuotaType.StorageShutoff:
				if (valueTypePropValue6 != null && valueTypePropValue7 != null)
				{
					mailboxMax = new int?(Math.Min(valueTypePropValue6.Value, valueTypePropValue7.Value));
					goto IL_221;
				}
				if (valueTypePropValue6 != null)
				{
					mailboxMax = valueTypePropValue6;
					goto IL_221;
				}
				mailboxMax = valueTypePropValue7;
				goto IL_221;
			case QuotaType.MailboxMessagesPerFolderCountWarningQuota:
			case QuotaType.MailboxMessagesPerFolderCountReceiveQuota:
			case QuotaType.FolderHierarchyChildrenCountWarningQuota:
			case QuotaType.FolderHierarchyChildrenCountReceiveQuota:
			case QuotaType.FolderHierarchyDepthWarningQuota:
			case QuotaType.FolderHierarchyDepthReceiveQuota:
			case QuotaType.FoldersCountWarningQuota:
			case QuotaType.FoldersCountReceiveQuota:
				mailboxMax = valueTypePropValue7;
				goto IL_221;
			}
			throw new NotSupportedException("Invalid QuotaType value.");
			IL_221:
			string refTypePropValue = this.Item.GetRefTypePropValue<string>(ItemSchema.SvrGeneratingQuotaMsg);
			int length;
			if (!string.IsNullOrEmpty(refTypePropValue) && (length = refTypePropValue.LastIndexOf(' ')) != -1)
			{
				folderName = refTypePropValue.Substring(0, length);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00007D25 File Offset: 0x00005F25
		private void RecipientHandler(int? recipientType, Recipient recipient, TransportMailItem mailItem, MailRecipient mailRecipient)
		{
			if (mailRecipient == null)
			{
				this.invalidRecipients.Add(MailItemSubmitter.CreateDsnRecipientInfo(recipient.Participant));
				return;
			}
			MSExchangeStoreDriverSubmission.TotalRecipients.Increment();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00007D50 File Offset: 0x00005F50
		private bool ShouldUsePrSmtpEmailAddress()
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			return snapshot != null && snapshot.MailboxTransport.UseParticipantSmtpEmailAddress.Enabled;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00007D84 File Offset: 0x00005F84
		private void CopyHeaderFirewallExceptions()
		{
			string refTypePropValue = this.Item.GetRefTypePropValue<string>(MessageItemSchema.XMsExchOrganizationOriginalClientIPAddress);
			HeaderList headers = this.mailItemToSubmit.MimeDocument.RootPart.Headers;
			if (!string.IsNullOrEmpty(refTypePropValue))
			{
				headers.AppendChild(new TextHeader("X-MS-Exchange-Organization-OriginalClientIPAddress", refTypePropValue));
				headers.RemoveAll("X-Originating-IP");
				headers.AppendChild(new AsciiTextHeader("X-Originating-IP", string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
				{
					refTypePropValue
				})));
				IPAddress originalClientIPAddress;
				if (IPAddress.TryParse(refTypePropValue, out originalClientIPAddress))
				{
					this.result.OriginalClientIPAddress = originalClientIPAddress;
				}
				else
				{
					MailItemSubmitter.diag.TraceError<string>(0L, "Unable to parse original IP address <{0}>", refTypePropValue);
				}
			}
			string refTypePropValue2 = this.Item.GetRefTypePropValue<string>(MessageItemSchema.XMsExchOrganizationOriginalServerIPAddress);
			if (!string.IsNullOrEmpty(refTypePropValue2))
			{
				headers.AppendChild(new TextHeader("X-MS-Exchange-Organization-OriginalServerIPAddress", refTypePropValue2));
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00007E64 File Offset: 0x00006064
		private void AppendDiagnosticInfo(Exception e, MailItemSubmitter.SubmissionStage submissionStage)
		{
			string text;
			if (submissionStage == MailItemSubmitter.SubmissionStage.OnDemotedEvent && this.mexSession != null && !string.IsNullOrEmpty(this.mexSession.LastAgentName))
			{
				text = string.Format(CultureInfo.InvariantCulture, "Stage:{0}, Agent:{1}, Exception:{2}:{3}", new object[]
				{
					submissionStage,
					this.mexSession.LastAgentName,
					e.GetType().Name,
					e.Message
				});
			}
			else
			{
				text = string.Format(CultureInfo.InvariantCulture, "Stage:{0}, Exception:{1}:{2}", new object[]
				{
					submissionStage,
					e.GetType().Name,
					e.Message
				});
			}
			Exception innerException = e.InnerException;
			int num = 0;
			while (num < 10 && innerException != null)
			{
				MapiPermanentException ex = innerException as MapiPermanentException;
				MapiRetryableException ex2 = innerException as MapiRetryableException;
				string text2 = innerException.Message;
				if (ex != null)
				{
					text2 = ex.DiagCtx.ToCompactString();
				}
				else if (ex2 != null)
				{
					text2 = ex2.DiagCtx.ToCompactString();
				}
				text = string.Format(CultureInfo.InvariantCulture, "{0} InnerException:{1}:{2}", new object[]
				{
					text,
					innerException.GetType().Name,
					text2
				});
				num++;
				innerException = innerException.InnerException;
			}
			text = text.Replace('\n', ' ');
			if (string.IsNullOrEmpty(this.result.DiagnosticInfo))
			{
				this.result.DiagnosticInfo = text;
				return;
			}
			MailSubmissionResult mailSubmissionResult = this.result;
			mailSubmissionResult.DiagnosticInfo = mailSubmissionResult.DiagnosticInfo + "; " + text;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00007FF0 File Offset: 0x000061F0
		private void AppendDiagnosticInfo(SmtpResponse response, MailItemSubmitter.SubmissionStage submissionStage)
		{
			this.AppendDiagnosticInfo(response, null, submissionStage);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00007FFC File Offset: 0x000061FC
		private void AppendDiagnosticInfo(SmtpResponse response, string details, MailItemSubmitter.SubmissionStage submissionStage)
		{
			string text;
			if (submissionStage == MailItemSubmitter.SubmissionStage.OnDemotedEvent && this.mexSession != null && !string.IsNullOrEmpty(this.mexSession.LastAgentName))
			{
				text = string.Format(CultureInfo.InvariantCulture, "Stage:{0}, Agent:{1}, SmtpResponse:{2}", new object[]
				{
					submissionStage,
					this.mexSession.LastAgentName,
					response
				});
			}
			else
			{
				text = string.Format(CultureInfo.InvariantCulture, "Stage:{0}, SmtpResponse:{1}", new object[]
				{
					submissionStage,
					response
				});
			}
			if (!string.IsNullOrEmpty(details))
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0}, details:{1}", new object[]
				{
					text,
					details
				});
			}
			text = text.Replace('\n', ' ');
			if (string.IsNullOrEmpty(this.result.DiagnosticInfo))
			{
				this.result.DiagnosticInfo = text;
				return;
			}
			MailSubmissionResult mailSubmissionResult = this.result;
			mailSubmissionResult.DiagnosticInfo = mailSubmissionResult.DiagnosticInfo + "; " + text;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000080FC File Offset: 0x000062FC
		private void AppendRecipientsListToDiagnosticInfo(string listName, List<string> recipientsList)
		{
			string text = string.Join("|", recipientsList.ToArray());
			string text2 = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				listName,
				text
			});
			if (string.IsNullOrEmpty(this.result.DiagnosticInfo))
			{
				this.result.DiagnosticInfo = text2;
				return;
			}
			this.result.DiagnosticInfo = string.Format(CultureInfo.InvariantCulture, "{0};{1}", new object[]
			{
				this.result.DiagnosticInfo,
				text2
			});
		}

		// Token: 0x04000029 RID: 41
		private const int SubmitterRecords = 128;

		// Token: 0x0400002A RID: 42
		private static readonly Trace diag = ExTraceGlobals.MapiStoreDriverSubmissionTracer;

		// Token: 0x0400002B RID: 43
		private static readonly byte[] EmptyByteArray = new byte[0];

		// Token: 0x0400002C RID: 44
		private static Breadcrumbs<MailItemSubmitter.Breadcrumb> submitterHistory = new Breadcrumbs<MailItemSubmitter.Breadcrumb>(128);

		// Token: 0x0400002D RID: 45
		private SendAsManager sendAsManager;

		// Token: 0x0400002E RID: 46
		private SubmissionPoisonHandler submissionPoisonHandler;

		// Token: 0x0400002F RID: 47
		private SubmissionPoisonContext submissionPoisonContext;

		// Token: 0x04000030 RID: 48
		private IStoreDriverTracer storeDriverTracer;

		// Token: 0x04000031 RID: 49
		private StoreDriverSubmission storeDriverSubmission;

		// Token: 0x04000032 RID: 50
		private TransportMailItem mailItemToSubmit;

		// Token: 0x04000033 RID: 51
		private List<DsnRecipientInfo> invalidRecipients = new List<DsnRecipientInfo>();

		// Token: 0x04000034 RID: 52
		private MailSubmissionResult result;

		// Token: 0x04000035 RID: 53
		private MailItemTraceFilter traceFilter;

		// Token: 0x04000036 RID: 54
		private OrganizationId organizationId;

		// Token: 0x04000037 RID: 55
		private Guid externalOrgId;

		// Token: 0x04000038 RID: 56
		private MailItemSubmitter.Breadcrumb breadcrumb;

		// Token: 0x04000039 RID: 57
		private IMExSession mexSession;

		// Token: 0x0400003A RID: 58
		private AgentLatencyTracker agentLatencyTracker;

		// Token: 0x0400003B RID: 59
		private DisposeTracker disposeTracker;

		// Token: 0x0400003C RID: 60
		private long messageSize;

		// Token: 0x0400003D RID: 61
		private int recipientCount;

		// Token: 0x0400003E RID: 62
		private TimeSpan rpcLatency;

		// Token: 0x0400003F RID: 63
		private bool processingTerminated;

		// Token: 0x04000040 RID: 64
		private string submissionExceptionAgentName;

		// Token: 0x02000014 RID: 20
		internal enum SubmissionStage
		{
			// Token: 0x04000049 RID: 73
			Start,
			// Token: 0x0400004A RID: 74
			LoadADEntry,
			// Token: 0x0400004B RID: 75
			CreateMexSession,
			// Token: 0x0400004C RID: 76
			CheckThrottle,
			// Token: 0x0400004D RID: 77
			LoadItem,
			// Token: 0x0400004E RID: 78
			SendAsCheck,
			// Token: 0x0400004F RID: 79
			CreateMailItem,
			// Token: 0x04000050 RID: 80
			OnDemotedEvent,
			// Token: 0x04000051 RID: 81
			SubmitNdrForInvalidRecipients,
			// Token: 0x04000052 RID: 82
			CommitMailItem,
			// Token: 0x04000053 RID: 83
			StartedSMTPOutOperation,
			// Token: 0x04000054 RID: 84
			SubmitMailItem,
			// Token: 0x04000055 RID: 85
			FinishedSMTPOutOperation,
			// Token: 0x04000056 RID: 86
			SMTPOutThrewException,
			// Token: 0x04000057 RID: 87
			DoneWithMessage,
			// Token: 0x04000058 RID: 88
			GenerateNdr,
			// Token: 0x04000059 RID: 89
			PoisonHandledNdrStart
		}

		// Token: 0x02000015 RID: 21
		internal class Breadcrumb
		{
			// Token: 0x060000B6 RID: 182 RVA: 0x000081B3 File Offset: 0x000063B3
			internal Breadcrumb(MailSubmissionResult result)
			{
				this.RecordCreation = ExDateTime.UtcNow;
				this.result = result;
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000B7 RID: 183 RVA: 0x000081D8 File Offset: 0x000063D8
			// (set) Token: 0x060000B8 RID: 184 RVA: 0x000081E0 File Offset: 0x000063E0
			internal TimeSpan Done
			{
				get
				{
					return this.done;
				}
				set
				{
					this.done = value;
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000B9 RID: 185 RVA: 0x000081E9 File Offset: 0x000063E9
			// (set) Token: 0x060000BA RID: 186 RVA: 0x000081F1 File Offset: 0x000063F1
			internal ExDateTime RecordCreation { get; set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060000BB RID: 187 RVA: 0x000081FA File Offset: 0x000063FA
			// (set) Token: 0x060000BC RID: 188 RVA: 0x00008202 File Offset: 0x00006402
			internal LatencyTracker LatencyTracker { get; set; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x060000BD RID: 189 RVA: 0x0000820B File Offset: 0x0000640B
			// (set) Token: 0x060000BE RID: 190 RVA: 0x00008213 File Offset: 0x00006413
			internal TimeSpan RpcLatency { get; set; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x060000BF RID: 191 RVA: 0x0000821C File Offset: 0x0000641C
			// (set) Token: 0x060000C0 RID: 192 RVA: 0x00008224 File Offset: 0x00006424
			internal TimeSpan StoreLatency { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000822D File Offset: 0x0000642D
			// (set) Token: 0x060000C2 RID: 194 RVA: 0x00008235 File Offset: 0x00006435
			internal TimeSpan LdapLatency { get; set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000823E File Offset: 0x0000643E
			// (set) Token: 0x060000C4 RID: 196 RVA: 0x00008246 File Offset: 0x00006446
			internal Guid MailboxDatabase { get; set; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000824F File Offset: 0x0000644F
			// (set) Token: 0x060000C6 RID: 198 RVA: 0x00008257 File Offset: 0x00006457
			internal Guid MailboxGuid { get; set; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000C7 RID: 199 RVA: 0x00008260 File Offset: 0x00006460
			// (set) Token: 0x060000C8 RID: 200 RVA: 0x00008268 File Offset: 0x00006468
			internal string MailboxServer { get; set; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x00008271 File Offset: 0x00006471
			// (set) Token: 0x060000CA RID: 202 RVA: 0x00008279 File Offset: 0x00006479
			internal long EventCounter { get; set; }

			// Token: 0x060000CB RID: 203 RVA: 0x00008284 File Offset: 0x00006484
			public override string ToString()
			{
				return string.Format("Created: {0}, EventCounter: {1}, MDB: {2}, MbxServer: {3}, MbxGuid: {4}, ErrorCode: {5}, Sender: {6}, From: {7}, MessageId: {8}, DiagnosticInfo: {9}, Subject: {10}, SubmissionStage: {11}, Completed: {12}, Elapsed: {13}, RpcLatency: {14}, StoreLatency: {15}, LdapLatency: {16}", new object[]
				{
					this.RecordCreation,
					this.EventCounter,
					this.MailboxDatabase.ToString(),
					this.MailboxServer,
					this.MailboxGuid,
					HResult.GetStringForErrorCode(this.result.ErrorCode),
					this.result.Sender,
					this.result.From,
					this.result.MessageId,
					this.result.DiagnosticInfo,
					this.result.Subject,
					Int32EnumFormatter<MailItemSubmitter.SubmissionStage>.Format((int)this.submissionStage),
					(this.done == TimeSpan.MinValue) ? "No" : "Yes",
					(this.done == TimeSpan.MinValue) ? ExDateTime.UtcNow.Subtract(this.RecordCreation) : this.done,
					this.RpcLatency,
					this.StoreLatency,
					this.LdapLatency
				});
			}

			// Token: 0x060000CC RID: 204 RVA: 0x000083E0 File Offset: 0x000065E0
			public XElement GetDiagnosticInfo()
			{
				XElement xelement = new XElement("Submission", new object[]
				{
					new XElement("creationTimestamp", this.RecordCreation),
					new XElement("eventCounter", this.EventCounter),
					new XElement("mailboxDatabase", this.MailboxDatabase),
					new XElement("mailboxServer", this.MailboxServer),
					new XElement("mailboxGuid", this.MailboxGuid),
					new XElement("errorCode", HResult.GetStringForErrorCode(this.result.ErrorCode)),
					new XElement("sender", this.result.Sender),
					new XElement("from", this.result.From),
					new XElement("diagnosticInfo", this.result.DiagnosticInfo),
					new XElement("subject", this.result.Subject),
					new XElement("submissionStage", Int32EnumFormatter<MailItemSubmitter.SubmissionStage>.Format((int)this.submissionStage)),
					new XElement("completed", this.done != TimeSpan.MinValue),
					new XElement("elapsed", (this.done == TimeSpan.MinValue) ? ExDateTime.UtcNow.Subtract(this.RecordCreation) : this.done),
					new XElement("rpcLatency", this.RpcLatency),
					new XElement("storeLatency", this.StoreLatency),
					new XElement("ldapLatency", this.LdapLatency)
				});
				xelement.Add(LatencyFormatter.GetDiagnosticInfo(this.LatencyTracker));
				xelement.SetAttributeValue("messageId", this.result.MessageId);
				return xelement;
			}

			// Token: 0x060000CD RID: 205 RVA: 0x0000863B File Offset: 0x0000683B
			internal MailItemSubmitter.SubmissionStage SetStage(MailItemSubmitter.SubmissionStage submissionStage)
			{
				this.submissionStage = submissionStage;
				return submissionStage;
			}

			// Token: 0x0400005A RID: 90
			private MailSubmissionResult result;

			// Token: 0x0400005B RID: 91
			private TimeSpan done = TimeSpan.MinValue;

			// Token: 0x0400005C RID: 92
			private MailItemSubmitter.SubmissionStage submissionStage;
		}
	}
}

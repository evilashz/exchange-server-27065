using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Mapi;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxTransport.Submission
{
	// Token: 0x02000005 RID: 5
	internal class MailboxTransportSubmissionAssistant : IEventBasedAssistant, IAssistantBase, IEventSkipNotification
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002FA4 File Offset: 0x000011A4
		public MailboxTransportSubmissionAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName)
		{
			if (databaseInfo == null)
			{
				throw new ArgumentNullException("databaseInfo");
			}
			this.databaseInfo = databaseInfo;
			this.mdbGuidString = this.databaseInfo.Guid.ToString();
			this.Name = name;
			this.NonLocalizedName = nonLocalizedName;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00003047 File Offset: 0x00001247
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000304F File Offset: 0x0000124F
		public LocalizedString Name { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00003058 File Offset: 0x00001258
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00003060 File Offset: 0x00001260
		public string NonLocalizedName { get; private set; }

		// Token: 0x06000021 RID: 33 RVA: 0x00003069 File Offset: 0x00001269
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.MailSubmitted) == MapiEventTypeFlags.MailSubmitted;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000307E File Offset: 0x0000127E
		public virtual void OnStart(EventBasedStartInfo startInfo)
		{
			MailboxTransportSubmissionAssistant.InitializePerformanceCounterMaintenance();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003088 File Offset: 0x00001288
		public virtual void OnShutdown()
		{
			Interlocked.Exchange(ref this.shutdownInProgress, 1);
			lock (MailboxTransportSubmissionService.MailboxTransportSubmissionAssistantInstances)
			{
				MailboxTransportSubmissionAssistant.ShutdownPerformanceCounterMaintenance();
				MailboxTransportSubmissionService.MailboxTransportSubmissionAssistantInstances.Remove(this);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000030E0 File Offset: 0x000012E0
		public void HandleEvent(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item)
		{
			MailboxTransportSubmissionAssistant.IdentifyProbeMessage(mapiEvent);
			Thread currentThread = Thread.CurrentThread;
			try
			{
				Interlocked.Increment(ref this.concurrentEvents);
				this.SubmissionsInProgress[currentThread] = new SubmissionsInProgress.Entry(ExDateTime.UtcNow, this.databaseInfo.Guid, mapiEvent);
				using (new SenderGuidTraceFilter(this.databaseInfo.Guid, mapiEvent.MailboxGuid))
				{
					bool flag = true;
					MailboxTransportSubmissionAssistant.LatencyRecord latencyRecord;
					LatencyTracker latencyTracker;
					if (MailboxTransportSubmissionAssistant.eventCounterToLatencyMap.TryGetValue(mapiEvent.EventCounter, out latencyRecord))
					{
						MailboxTransportSubmissionAssistant.eventCounterToLatencyMap.Remove(mapiEvent.EventCounter);
						latencyTracker = latencyRecord.LatencyTracker;
						LatencyTracker.EndTrackLatency(latencyRecord.LatencyComponent, latencyTracker, true);
						flag = false;
					}
					else
					{
						latencyTracker = LatencyTracker.CreateInstance(LatencyComponent.MailboxTransportSubmissionService);
						LatencyTracker.TrackPreProcessLatency(LatencyComponent.SubmissionAssistant, latencyTracker, mapiEvent.CreateTime);
					}
					LatencyTracker.BeginTrackLatency(LatencyComponent.MailboxTransportSubmissionService, latencyTracker);
					string text = this.FormatMapiEventInfo(mapiEvent);
					this.LogMapiEventIntoCrimsonChannelPeriodically(text);
					this.SubmissionsInProgress[Thread.CurrentThread].LatencyTracker = latencyTracker;
					if (MailboxTransportSubmissionAssistant.ShouldLogNotifyEvents && (flag || MailboxTransportSubmissionAssistant.ShouldLogTemporaryFailures))
					{
						LatencyFormatter latencyFormatter = new LatencyFormatter(latencyTracker, Components.Configuration.LocalServer.TransportServer.Fqdn, mapiEvent.CreateTime, mapiEvent.CreateTime, true, false, false);
						MsgTrackMapiSubmitInfo msgTrackInfo = new MsgTrackMapiSubmitInfo(text, null, string.Empty, StoreDriverSubmission.LocalIPAddress, Components.Configuration.LocalServer.TransportServer.Name, string.Empty, string.Empty, MailboxTransportSubmissionAssistant.ItemEntryId(mapiEvent), latencyFormatter.FormatAndUpdatePerfCounters(), null, false, true);
						MessageTrackingLog.TrackNotify(msgTrackInfo, false);
					}
					MSExchangeSubmission.PendingSubmissions.Increment();
					new HashSet<string>();
					DateTime createTime = mapiEvent.CreateTime;
					MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TracePfdPass<int, Guid, long>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "PFD EMS {0} SubmitMail for mailbox {1} at entry {2}", 22427, mapiEvent.MailboxGuid, mapiEvent.EventCounter);
					LatencyFormatter latencyFormatter2 = new LatencyFormatter(latencyTracker, Components.Configuration.LocalServer.TransportServer.Fqdn, createTime, createTime, false, true, false);
					bool isPublicFolder = mapiEvent.ExtendedEventFlags.HasFlag(MapiExtendedEventFlags.PublicFolderMailbox);
					LatencyTracker.BeginTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmission, latencyTracker);
					ISubmissionProvider submissionProvider = (ISubmissionProvider)Components.StoreDriverSubmission;
					MailSubmissionResult mailSubmissionResult = submissionProvider.SubmitMessage(Components.Configuration.LocalServer.TransportServer.ExchangeLegacyDN, mapiEvent.MailboxGuid, this.databaseInfo.Guid, this.databaseInfo.DatabaseName, mapiEvent.EventCounter, mapiEvent.ItemEntryId, mapiEvent.ParentEntryId, Components.Configuration.LocalServer.TransportServer.Fqdn, new IPAddress(StoreDriverSubmission.LocalIPAddress.GetAddressBytes()), mapiEvent.CreateTime, isPublicFolder, (mapiEvent.TenantHint == null) ? null : TenantPartitionHint.FromPersistablePartitionHint(mapiEvent.TenantHint), latencyFormatter2.FormatAndUpdatePerfCounters(), MailboxTransportSubmissionService.QuarantineHandler, MailboxTransportSubmissionService.SubmissionPoisonHandler, latencyTracker);
					MailSubmissionResult result = mailSubmissionResult;
					LatencyTracker.EndTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmission, latencyTracker);
					LatencyTracker.EndTrackLatency(LatencyComponent.MailboxTransportSubmissionService, latencyTracker);
					this.LogAndUpdateCounters(mapiEvent, latencyTracker, createTime, result);
					this.HandleEventError(mapiEvent, result, latencyTracker, text);
				}
			}
			finally
			{
				MSExchangeSubmission.PendingSubmissions.Decrement();
				Interlocked.Decrement(ref this.concurrentEvents);
				this.SubmissionsInProgress.Remove(currentThread);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003428 File Offset: 0x00001628
		public void OnSkipEvent(MapiEvent mapiEvent, Exception exception)
		{
			try
			{
				MailboxTransportSubmissionAssistant.eventCounterToLatencyMap.Remove(mapiEvent.EventCounter);
				string text = this.FormatMapiEventInfo(mapiEvent);
				Exception ex = exception;
				if (ex != null)
				{
					while (ex.InnerException != null)
					{
						ex = ex.InnerException;
					}
				}
				string text2 = "Error: SkipEvent";
				if (ex != null)
				{
					text2 = text2 + ", Diagnostic Information: " + ex.GetType().Name;
				}
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TraceFail<string, string>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "{0} will be skipped due to error {1}", text, text2);
				MsgTrackMapiSubmitInfo msgTrackInfo = new MsgTrackMapiSubmitInfo(text, null, string.Empty, StoreDriverSubmission.LocalIPAddress, Components.Configuration.LocalServer.TransportServer.Name, string.Empty, string.Empty, mapiEvent.ItemEntryId, string.Empty, text2, true, false);
				MessageTrackingLog.TrackMapiSubmit(msgTrackInfo);
			}
			catch (Exception arg)
			{
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TraceFail<Exception>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "Unexpected exception {0} during skipped event logging", arg);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000352C File Offset: 0x0000172C
		public override int GetHashCode()
		{
			return this.hashCode;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003534 File Offset: 0x00001734
		public XElement GetDiagnosticInfo(string argument)
		{
			XElement xelement = new XElement("MailboxTransportSubmissionAssistant", new object[]
			{
				new XElement("concurrentEvents", this.concurrentEvents),
				new XElement("messagesInRetry", MailboxTransportSubmissionAssistant.eventCounterToLatencyMap.Count),
				new XElement("shutdownInProgress", this.shutdownInProgress),
				new XElement("Database", new object[]
				{
					new XElement("guid", this.databaseInfo.Guid),
					new XElement("name", this.databaseInfo.DatabaseName),
					new XElement("public", this.databaseInfo.IsPublic),
					new XElement("systemMailboxGuid", this.databaseInfo.SystemMailboxGuid)
				})
			});
			if (!string.IsNullOrEmpty(argument) && -1 != argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase))
			{
				XElement xelement2 = new XElement("SuccessHistory");
				xelement.Add(xelement2);
				int num = 0;
				foreach (SubmissionRecord.Success success in ((IEnumerable<SubmissionRecord.Success>)this.successHistory))
				{
					xelement2.Add(success.GetDiagnosticInfo());
					num++;
				}
				xelement2.AddFirst(new XElement("count", num));
				XElement xelement3 = new XElement("FailureHistory");
				xelement.Add(xelement3);
				num = 0;
				foreach (SubmissionRecord.Failure failure in ((IEnumerable<SubmissionRecord.Failure>)this.failureHistory))
				{
					xelement3.Add(failure.GetDiagnosticInfo());
				}
				xelement3.AddFirst(new XElement("count", num));
			}
			if (!string.IsNullOrEmpty(argument) && -1 != argument.IndexOf("currentThreads", StringComparison.OrdinalIgnoreCase))
			{
				xelement.Add(this.SubmissionsInProgress.GetDiagnosticInfo());
			}
			return xelement;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000037A8 File Offset: 0x000019A8
		internal static TimeSpan GetConfigTimeSpan(string label, TimeSpan minimumValue, TimeSpan maximumValue, TimeSpan defaultValue)
		{
			TimeSpan result;
			try
			{
				result = TransportAppConfig.GetConfigTimeSpan(label, minimumValue, maximumValue, defaultValue);
			}
			catch (ConfigurationErrorsException)
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000037D8 File Offset: 0x000019D8
		internal bool DetectSubmissionHang()
		{
			return this.SubmissionsInProgress.DetectHangAndLog(MailboxTransportSubmissionAssistant.HangThreshold);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000037EA File Offset: 0x000019EA
		private static byte[] ItemEntryId(MapiEvent mapiEvent)
		{
			return mapiEvent.ItemEntryId;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000037F4 File Offset: 0x000019F4
		private static void BeginTrackLatency(MapiEvent mapiEvent, LatencyTracker latencyTracker, LatencyComponent latencyComponent)
		{
			MailboxTransportSubmissionAssistant.LatencyRecord value = new MailboxTransportSubmissionAssistant.LatencyRecord(latencyTracker, latencyComponent);
			MailboxTransportSubmissionAssistant.eventCounterToLatencyMap[mapiEvent.EventCounter] = value;
			LatencyTracker.BeginTrackLatency(latencyComponent, latencyTracker);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003822 File Offset: 0x00001A22
		private static void IncrementSuccessfulSubmissionPerfmon()
		{
			MSExchangeSubmission.SuccessfulSubmissions.Increment();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000382F File Offset: 0x00001A2F
		private static void IncrementSuccessfulPoisonNdrSubmissionPerfmon()
		{
			MSExchangeSubmission.SuccessfulPoisonNdrSubmissions.Increment();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000383C File Offset: 0x00001A3C
		private static void IncrementFailedSubmissionPerfmon()
		{
			MSExchangeSubmission.FailedSubmissions.Increment();
			lock (MailboxTransportSubmissionService.PercentPermanentFailures)
			{
				MailboxTransportSubmissionService.PercentPermanentFailures.AddNumerator(1L);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003890 File Offset: 0x00001A90
		private static void IncrementNonActionableFailedSubmissionPerfmon()
		{
			MSExchangeSubmission.NonActionableFailedSubmissions.Increment();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000389D File Offset: 0x00001A9D
		private static void IncrementPermanentFailedPoisonNdrSubmissionPerfmon()
		{
			MSExchangeSubmission.PermanentFailedPoisonNdrSubmissions.Increment();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000038AC File Offset: 0x00001AAC
		private static void ComputePercentSubmissionsPerfmon(bool calculateOnly = false)
		{
			lock (MailboxTransportSubmissionService.PercentPermanentFailures)
			{
				if (!calculateOnly)
				{
					MailboxTransportSubmissionService.PercentPermanentFailures.AddDenominator(1L);
				}
				MSExchangeSubmission.PercentFailedSubmissions.RawValue = (long)((int)MailboxTransportSubmissionService.PercentPermanentFailures.GetSlidingPercentage());
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000390C File Offset: 0x00001B0C
		private static void InitializePerformanceCounterMaintenance()
		{
			if (MailboxTransportSubmissionAssistant.performanceCounterMaintenanceTimer == null)
			{
				MailboxTransportSubmissionAssistant.performanceCounterMaintenanceTimer = new GuardedTimer(new TimerCallback(MailboxTransportSubmissionAssistant.RefreshPerformanceCounters), null, TimeSpan.FromSeconds(60.0));
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000393A File Offset: 0x00001B3A
		private static void ShutdownPerformanceCounterMaintenance()
		{
			if (MailboxTransportSubmissionAssistant.performanceCounterMaintenanceTimer != null)
			{
				MailboxTransportSubmissionAssistant.performanceCounterMaintenanceTimer.Dispose(true);
				MailboxTransportSubmissionAssistant.performanceCounterMaintenanceTimer = null;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003954 File Offset: 0x00001B54
		private static void RefreshPerformanceCounters(object state)
		{
			MailboxTransportSubmissionAssistant.ComputePercentSubmissionsPerfmon(true);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000395C File Offset: 0x00001B5C
		private static void IncrementTemporarySubmissionFailuresPerfmon()
		{
			MSExchangeSubmission.TemporarySubmissionFailures.Increment();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003969 File Offset: 0x00001B69
		private static void IncrementTemporaryPoisonNdrSubmissionFailuresPerfmon()
		{
			MSExchangeSubmission.TemporaryPoisonNdrSubmissionFailures.Increment();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003978 File Offset: 0x00001B78
		private static void IdentifyProbeMessage(MapiEvent mapiEvent)
		{
			MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeLamNotificationIdParts = null;
			if (ObjectClass.IsOfClass(mapiEvent.ObjectClass, "IPM.Note.MapiSubmitSystemProbe"))
			{
				string arg = string.Format("{0:X8}", mapiEvent.ItemEntryIdString.GetHashCode());
				string text = mapiEvent.MailboxGuid.ToString();
				int startIndex = text.IndexOf('-');
				MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId = new Guid(string.Format("{0}{1}", arg, text.Substring(startIndex)));
				return;
			}
			MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId = Guid.Empty;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003A15 File Offset: 0x00001C15
		private void CheckForShutdown()
		{
			if (this.shutdownInProgress == 1)
			{
				throw new TransientMailboxException(MailboxTransportSubmissionAssistant.RetryScheduleGeneric);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003A30 File Offset: 0x00001C30
		private void LogAndUpdateCounters(MapiEvent mapiEvent, LatencyTracker latencyTracker, DateTime arrivalTime, MailSubmissionResult result)
		{
			LatencyFormatter latencyFormatter = new LatencyFormatter(latencyTracker, Components.Configuration.LocalServer.TransportServer.Fqdn, arrivalTime, arrivalTime, true, false, true);
			string text = latencyFormatter.FormatAndUpdatePerfCounters();
			this.LogMessageTrackingInfo(result.RemoteHostName, StoreDriverSubmission.LocalIPAddress, Components.Configuration.LocalServer.TransportServer.Name, mapiEvent, result, text);
			if (result.ErrorCode == 0U)
			{
				MailboxTransportSubmissionAssistant.IncrementSuccessfulSubmissionPerfmon();
				if (MailboxTransportSubmissionService.StoreDriverTracer.IsMessageAMapiSubmitLAMProbe)
				{
					MailItemSubmitter.WriteLamNotificationEvent("EventHandled", text, result.MessageId, mapiEvent.MailboxGuid, MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeLamNotificationIdParts);
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003ACC File Offset: 0x00001CCC
		private void LogMessageTrackingInfo(string remoteHostName, IPAddress localIP, string localServerName, MapiEvent mapiEvent, MailSubmissionResult result, string latencyString)
		{
			SubmissionRecord.Drop(this.successHistory, this.failureHistory, mapiEvent, result);
			string text = this.FormatMapiEventInfo(mapiEvent);
			MsgTrackMapiSubmitInfo msgTrackInfo;
			if (result.ErrorCode == 0U)
			{
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TracePass<string>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "Submission succeeded for {0}", text);
				msgTrackInfo = new MsgTrackMapiSubmitInfo(text, null, remoteHostName, localIP, localServerName, result.Sender, result.From, result.MessageId, MailboxTransportSubmissionAssistant.ItemEntryId(mapiEvent), Components.Configuration.LocalServer.TransportServer.MessageTrackingLogSubjectLoggingEnabled ? result.Subject : "[Undisclosed]", latencyString, result.DiagnosticInfo, null != mapiEvent, result.ExternalOrganizationId, result.OrganizationId, result.RecipientAddresses, MailDirectionality.Originating, result.NetworkMessageId, result.OriginalClientIPAddress);
			}
			else
			{
				string text2 = HResult.GetStringForErrorCode(result.ErrorCode);
				if (!string.IsNullOrEmpty(result.DiagnosticInfo))
				{
					text2 = "Error: " + text2 + ", Diagnostic Information: " + result.DiagnosticInfo;
				}
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TracePass<string, string>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "Submission failed for {0}, error: {1}", text, text2);
				if (!HResult.IsHandled(result.ErrorCode) && !MailboxTransportSubmissionAssistant.ShouldLogTemporaryFailures)
				{
					return;
				}
				msgTrackInfo = new MsgTrackMapiSubmitInfo(text, null, remoteHostName, localIP, localServerName, result.Sender, result.MessageId, MailboxTransportSubmissionAssistant.ItemEntryId(mapiEvent), latencyString, text2, HResult.IsHandled(result.ErrorCode), null != mapiEvent, result.ExternalOrganizationId, result.OrganizationId, result.RecipientAddresses, MailDirectionality.Originating, result.NetworkMessageId);
			}
			if (11U == result.ErrorCode)
			{
				MessageTrackingLog.TrackThrottle(MessageTrackingSource.STOREDRIVER, msgTrackInfo, result.Sender, result.MessageId, MailDirectionality.Originating);
				return;
			}
			MessageTrackingLog.TrackMapiSubmit(msgTrackInfo);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003C9C File Offset: 0x00001E9C
		private void LogMapiEventIntoCrimsonChannelPeriodically(string eventIdentifier)
		{
			this.eventNotificationItem.PublishPeriodic(ExchangeComponent.MailboxTransport.Name, "MBTSubmissionServiceNotifyMapiLogger", string.Empty, "NotifyMapi Heartbeat. Event is: " + eventIdentifier, "MBTSubmissionServiceNotifyMapiLogger", SubmissionConfiguration.Instance.App.ServiceHeartbeatPeriodicLoggingInterval, ResultSeverityLevel.Informational, false);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003CEC File Offset: 0x00001EEC
		private string FormatMapiEventInfo(MapiEvent mapiEvent)
		{
			return string.Format(CultureInfo.InvariantCulture, "MDB:{0}, Mailbox:{1}, Event:{2}, MessageClass:{3}, CreationTime:{4}, ClientType:{5}", new object[]
			{
				this.mdbGuidString,
				mapiEvent.MailboxGuid,
				mapiEvent.EventCounter,
				mapiEvent.ObjectClass,
				mapiEvent.CreateTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo),
				Int32EnumFormatter<MapiEventClientTypes>.Format((int)mapiEvent.ClientType)
			});
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003D68 File Offset: 0x00001F68
		private void HandleEventError(MapiEvent mapiEvent, MailSubmissionResult result, LatencyTracker latencyTracker, string eventContext)
		{
			uint errorCode = result.ErrorCode;
			if (errorCode == 22U)
			{
				MailboxTransportSubmissionAssistant.IncrementSuccessfulPoisonNdrSubmissionPerfmon();
			}
			else if (errorCode == 23U)
			{
				MailboxTransportSubmissionAssistant.IncrementPermanentFailedPoisonNdrSubmissionPerfmon();
			}
			else if (HResult.IsHandled(errorCode))
			{
				if (HResult.IsNonActionable(errorCode))
				{
					MailboxTransportSubmissionAssistant.IncrementNonActionableFailedSubmissionPerfmon();
				}
				else if (!HResult.IsMessageSubmittedOrHasNoRcpts(errorCode))
				{
					MailboxTransportSubmissionAssistant.IncrementFailedSubmissionPerfmon();
				}
			}
			else if (HResult.IsRetryableAtCurrentHub(errorCode))
			{
				MailboxTransportSubmissionAssistant.IncrementTemporarySubmissionFailuresPerfmon();
			}
			MailboxTransportSubmissionAssistant.ComputePercentSubmissionsPerfmon(false);
			if (11U == errorCode)
			{
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TraceFail<string>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "Submission for message {0} was throttled, retry happens in 1 minute.", mapiEvent.ItemEntryIdString);
				MailboxTransportSubmissionAssistant.BeginTrackLatency(mapiEvent, latencyTracker, LatencyComponent.SubmissionAssistantThrottling);
				throw new TransientMailboxException(MailboxTransportSubmissionAssistant.RetryScheduleMessageThrottling);
			}
			if (errorCode == 1140850696U)
			{
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TraceFail<uint, string>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "Resource is in Quarantined State. Error Code: {0}; Event Context: {1}", errorCode, eventContext);
				throw new TransientMailboxException(new RetrySchedule(FinalAction.RetryForever, TimeSpan.MaxValue, new TimeSpan[]
				{
					result.QuarantineTimeSpan
				}));
			}
			if (24U == errorCode)
			{
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TraceFail<uint, string>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "NDR for Poison Message was not successful. Retry. Error Code: {0}, Event context: {1}.", errorCode, eventContext);
				MailboxTransportSubmissionAssistant.IncrementTemporaryPoisonNdrSubmissionFailuresPerfmon();
				throw new TransientServerException(MailboxTransportSubmissionAssistant.RetrySchedulePoisonNdr);
			}
			if (HResult.IsHandled(errorCode))
			{
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TraceFail<uint, string>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "Submission was handled.  Error code {0}, event context {1}.", errorCode, eventContext);
				return;
			}
			if (2684354560U == errorCode)
			{
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TraceFail<string>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "Submission for message {0} was throttled at smtp, retry happens in 1 minute.", mapiEvent.ItemEntryIdString);
				throw new TransientMailboxException(MailboxTransportSubmissionAssistant.RetryScheduleGeneric);
			}
			if (HResult.IsRetryableAtCurrentHub(errorCode))
			{
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TraceFail<uint, string>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "Submission was not successful. Retry at current hub error code {0}, event context {1}.", errorCode, eventContext);
				if (HResult.IsRetryMailbox(errorCode))
				{
					throw new TransientMailboxException(MailboxTransportSubmissionAssistant.RetryScheduleGeneric);
				}
				if (HResult.IsRetryMailboxDatabase(errorCode))
				{
					throw new TransientDatabaseException(MailboxTransportSubmissionAssistant.RetryScheduleGeneric);
				}
				if (HResult.IsRetryMailboxServer(errorCode))
				{
					throw new TransientServerException(MailboxTransportSubmissionAssistant.RetryScheduleGeneric);
				}
				return;
			}
			else
			{
				if (HResult.IsRetryableAtOtherHub(errorCode))
				{
					MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TraceFail<uint, string>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "Submission was not successful. Retry other hub error code {0}, event context {1}.", errorCode, eventContext);
					throw new TransientServerException(MailboxTransportSubmissionAssistant.RetryScheduleGeneric);
				}
				throw new InvalidOperationException("Internal error. ErrorCode is: " + errorCode);
			}
		}

		// Token: 0x0400003B RID: 59
		private const int SubmissionRecords = 64;

		// Token: 0x0400003C RID: 60
		private const string EventHandledLamEntryName = "EventHandled";

		// Token: 0x0400003D RID: 61
		public static readonly Guid MailboxTransportSubmissionServiceComponentGuid = new Guid("{9409FDD0-E7E0-4D6B-AF02-F49A36C10FD4}");

		// Token: 0x0400003E RID: 62
		public static readonly int MaxConcurrentSubmissions = Environment.ProcessorCount * SubmissionConfiguration.Instance.App.MaxConcurrentSubmissions;

		// Token: 0x0400003F RID: 63
		internal readonly SubmissionsInProgress SubmissionsInProgress = new SubmissionsInProgress(MailboxTransportSubmissionAssistant.MaxConcurrentSubmissions);

		// Token: 0x04000040 RID: 64
		private static readonly bool ShouldLogTemporaryFailures = SubmissionConfiguration.Instance.App.ShouldLogTemporaryFailures;

		// Token: 0x04000041 RID: 65
		private static readonly bool ShouldLogNotifyEvents = SubmissionConfiguration.Instance.App.ShouldLogNotifyEvents;

		// Token: 0x04000042 RID: 66
		private static readonly RetrySchedule RetryScheduleMessageThrottling = new RetrySchedule(FinalAction.RetryForever, TimeSpan.MaxValue, new TimeSpan[]
		{
			SubmissionConfiguration.Instance.App.SenderRateThrottlingRetryInterval
		});

		// Token: 0x04000043 RID: 67
		private static readonly RetrySchedule RetryScheduleGeneric = new RetrySchedule(FinalAction.Skip, TimeSpan.FromDays(1.0), new TimeSpan[]
		{
			TimeSpan.FromSeconds(5.0)
		});

		// Token: 0x04000044 RID: 68
		private static readonly RetrySchedule RetrySchedulePoisonNdr = new RetrySchedule(FinalAction.Skip, TimeSpan.FromMinutes(1.0), new TimeSpan[]
		{
			TimeSpan.FromSeconds(30.0)
		});

		// Token: 0x04000045 RID: 69
		private static readonly TimeSpan HangThreshold = MailboxTransportSubmissionAssistant.GetConfigTimeSpan("HangThreshold", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromMinutes(20.0));

		// Token: 0x04000046 RID: 70
		private static SynchronizedDictionary<long, MailboxTransportSubmissionAssistant.LatencyRecord> eventCounterToLatencyMap = new SynchronizedDictionary<long, MailboxTransportSubmissionAssistant.LatencyRecord>();

		// Token: 0x04000047 RID: 71
		private static object hubPickerLock = new object();

		// Token: 0x04000048 RID: 72
		private static GuardedTimer performanceCounterMaintenanceTimer;

		// Token: 0x04000049 RID: 73
		private readonly IEventNotificationItem eventNotificationItem = new EventNotificationItemWrapper();

		// Token: 0x0400004A RID: 74
		private readonly string mdbGuidString;

		// Token: 0x0400004B RID: 75
		private readonly int hashCode = Guid.NewGuid().GetHashCode();

		// Token: 0x0400004C RID: 76
		private readonly DatabaseInfo databaseInfo;

		// Token: 0x0400004D RID: 77
		private volatile int shutdownInProgress;

		// Token: 0x0400004E RID: 78
		private int concurrentEvents;

		// Token: 0x0400004F RID: 79
		private Breadcrumbs<SubmissionRecord.Success> successHistory = new Breadcrumbs<SubmissionRecord.Success>(64);

		// Token: 0x04000050 RID: 80
		private Breadcrumbs<SubmissionRecord.Failure> failureHistory = new Breadcrumbs<SubmissionRecord.Failure>(64);

		// Token: 0x02000006 RID: 6
		internal struct LatencyRecord
		{
			// Token: 0x0600003F RID: 63 RVA: 0x00004116 File Offset: 0x00002316
			internal LatencyRecord(LatencyTracker latencyTracker, LatencyComponent latencyComponent)
			{
				this.latencyTracker = latencyTracker;
				this.latencyComponent = latencyComponent;
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000040 RID: 64 RVA: 0x00004126 File Offset: 0x00002326
			internal LatencyTracker LatencyTracker
			{
				get
				{
					return this.latencyTracker;
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000041 RID: 65 RVA: 0x0000412E File Offset: 0x0000232E
			internal LatencyComponent LatencyComponent
			{
				get
				{
					return this.latencyComponent;
				}
			}

			// Token: 0x04000053 RID: 83
			private LatencyTracker latencyTracker;

			// Token: 0x04000054 RID: 84
			private LatencyComponent latencyComponent;
		}
	}
}

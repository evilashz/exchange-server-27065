using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.MailboxTransport.StoreDriver.Shared;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000027 RID: 39
	internal class StoreDriverSubmission : IStoreDriverSubmission, IStartableTransportComponent, ITransportComponent, ISubmissionProvider, IDiagnosable
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00009B98 File Offset: 0x00007D98
		public StoreDriverSubmission(IStoreDriverTracer storeDriverTracer)
		{
			ArgumentValidator.ThrowIfNull("storeDriverTracer", storeDriverTracer);
			this.storeDriverTracer = storeDriverTracer;
			ExTraceGlobals.FaultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(StoreDriverSubmission.FaultInjectionCallback));
			ResourceHealthMonitorManager.Initialize(ResourceHealthComponent.Transport);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00009C3E File Offset: 0x00007E3E
		public static IPHostEntry LocalIP
		{
			get
			{
				return StoreDriverSubmission.localIp;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00009C45 File Offset: 0x00007E45
		public static IPAddress LocalIPAddress
		{
			get
			{
				return StoreDriverSubmission.localIp.AddressList[0];
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00009C53 File Offset: 0x00007E53
		public static string ReceivedHeaderTcpInfo
		{
			get
			{
				return StoreDriverSubmission.receivedHeaderTcpInfo;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00009C5A File Offset: 0x00007E5A
		public virtual bool PoisonMessageDectionEnabled
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer.PoisonMessageDetectionEnabled;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00009C70 File Offset: 0x00007E70
		public bool IsSubmissionPaused
		{
			get
			{
				return this.paused;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00009C78 File Offset: 0x00007E78
		public bool Retired
		{
			get
			{
				return this.stopped;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00009C82 File Offset: 0x00007E82
		public IStoreDriverTracer StoreDriverTracer
		{
			get
			{
				return this.storeDriverTracer;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00009C8C File Offset: 0x00007E8C
		public string CurrentState
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				stringBuilder.Append("Submission thread count=");
				stringBuilder.AppendLine(SubmissionThreadLimiter.ConcurrentSubmissions.ToString());
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00009CCA File Offset: 0x00007ECA
		private SubmissionsInProgress SubmissionsInProgress
		{
			get
			{
				if (this.submissionsInProgress == null)
				{
					this.submissionsInProgress = new SubmissionsInProgress(Components.Configuration.LocalServer.MaxConcurrentMailboxSubmissions);
				}
				return this.submissionsInProgress;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009CF4 File Offset: 0x00007EF4
		public static void InitializePerformanceCounterMaintenance()
		{
			StoreDriverSubmission.performanceCounterMaintenanceTimer = new GuardedTimer(new TimerCallback(StoreDriverSubmission.RefreshPerformanceCounters), null, TimeSpan.FromSeconds(60.0));
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009D1B File Offset: 0x00007F1B
		public static void ShutdownPerformanceCounterMaintenance()
		{
			if (StoreDriverSubmission.performanceCounterMaintenanceTimer != null)
			{
				StoreDriverSubmission.performanceCounterMaintenanceTimer.Dispose(true);
				StoreDriverSubmission.performanceCounterMaintenanceTimer = null;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009D35 File Offset: 0x00007F35
		public static void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			StoreDriverSubmission.eventLogger.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009D45 File Offset: 0x00007F45
		public static string FormatIPAddress(IPAddress address)
		{
			return "[" + address.ToString() + "]";
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009D5C File Offset: 0x00007F5C
		public void Continue()
		{
			lock (this.syncObject)
			{
				this.paused = false;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009DA0 File Offset: 0x00007FA0
		public void ExpireOldSubmissionConnections()
		{
			SubmissionConnectionPool.ExpireOldConnections();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009DA8 File Offset: 0x00007FA8
		public void Pause()
		{
			lock (this.syncObject)
			{
				this.paused = true;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00009EA8 File Offset: 0x000080A8
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
			lock (this.syncObject)
			{
				this.paused = initiallyPaused;
				ADNotificationAdapter.RunADOperation(delegate()
				{
					try
					{
						StoreDriverSubmission.localIp = Dns.GetHostEntry(Dns.GetHostName());
					}
					catch (SocketException ex)
					{
						this.storeDriverTracer.StoreDriverSubmissionTracer.TraceFail<string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Start failed: {0}", ex.ToString());
						StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_StoreDriverSubmissionGetLocalIPFailure, null, new object[]
						{
							ex
						});
						throw new TransportComponentLoadFailedException(ex.Message, ex);
					}
					StoreDriverSubmission.receivedHeaderTcpInfo = StoreDriverSubmission.FormatIPAddress(StoreDriverSubmission.localIp.AddressList[0]);
					this.storeDriverTracer.StoreDriverSubmissionTracer.TracePass(this.storeDriverTracer.MessageProbeActivityId, 0L, "Start submission");
					this.StartSubmission();
				}, 1);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009F04 File Offset: 0x00008104
		public void Stop()
		{
			this.storeDriverTracer.StoreDriverSubmissionTracer.TracePass(this.storeDriverTracer.MessageProbeActivityId, 0L, "Stop StoreDriverSubmission");
			if (!this.stopped)
			{
				this.stopped = true;
			}
			lock (this.syncObject)
			{
				this.StopSubmission();
				ProcessAccessManager.UnregisterComponent(this);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009F80 File Offset: 0x00008180
		public void Retire()
		{
			this.Stop();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009F88 File Offset: 0x00008188
		public void Load()
		{
			lock (this.syncObject)
			{
				ProcessAccessManager.RegisterComponent(this);
			}
			try
			{
				MExEvents.Initialize(Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\Shared\\agents.config"), ProcessTransportRole.MailboxSubmission, LatencyAgentGroup.MailboxTransportSubmissionStoreDriverSubmission, "Microsoft.Exchange.Data.Transport.StoreDriver.StoreDriverAgent");
				StoreDriverSubmission.InitializePerformanceCounterMaintenance();
			}
			catch (ExchangeConfigurationException ex)
			{
				this.storeDriverTracer.StoreDriverSubmissionTracer.TraceFail(this.storeDriverTracer.MessageProbeActivityId, (long)this.GetHashCode(), "StoreDriversubmission.Load threw ExchangeConfigurationException: shutting down service.");
				StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_CannotStartAgents, null, new object[]
				{
					ex.LocalizedString,
					ex
				});
				this.Stop();
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000A04C File Offset: 0x0000824C
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000A050 File Offset: 0x00008250
		public void Unload()
		{
			lock (this.syncObject)
			{
				ProcessAccessManager.UnregisterComponent(this);
			}
			MExEvents.Shutdown();
			StoreDriverSubmission.ShutdownPerformanceCounterMaintenance();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000A0A8 File Offset: 0x000082A8
		public MailSubmissionResult SubmitMessage(string serverDN, Guid mailboxGuid, Guid mdbGuid, string databaseName, long eventCounter, byte[] entryId, byte[] parentEntryId, string serverFqdn, IPAddress networkAddressBytes, DateTime originalCreateTime, bool isPublicFolder, TenantPartitionHint tenantHint, string mailboxHopLatency, QuarantineHandler quarantineHandler, SubmissionPoisonHandler submissionPoisonHandler, LatencyTracker latencyTracker)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("serverDN", serverDN);
			ArgumentValidator.ThrowIfEmpty("mailboxGuid", mailboxGuid);
			ArgumentValidator.ThrowIfEmpty("mdbGuid", mdbGuid);
			ArgumentValidator.ThrowIfNullOrEmpty("databaseName", databaseName);
			ArgumentValidator.ThrowIfNull("entryId", entryId);
			ArgumentValidator.ThrowIfInvalidValue<int>("entryId", entryId.Length, (int value) => value > 0);
			ArgumentValidator.ThrowIfNull("parentEntryId", parentEntryId);
			ArgumentValidator.ThrowIfInvalidValue<int>("parentEntryId", parentEntryId.Length, (int value) => value > 0);
			ArgumentValidator.ThrowIfNullOrEmpty("serverFqdn", serverFqdn);
			ArgumentValidator.ThrowIfNull("networkAddressBytes", networkAddressBytes);
			ArgumentValidator.ThrowIfNullOrEmpty("mailboxHopLatency", mailboxHopLatency);
			ArgumentValidator.ThrowIfNull("quarantineHandler", quarantineHandler);
			ArgumentValidator.ThrowIfNull("submissionPoisonHandler", submissionPoisonHandler);
			bool shouldDeprioritize = false;
			if (SubmissionConfiguration.Instance.App.SenderRateDeprioritizationEnabled)
			{
				long num = this.rateTrackerForDeprioritization.IncrementSenderRate(mailboxGuid, originalCreateTime);
				if (num > (long)SubmissionConfiguration.Instance.App.SenderRateDeprioritizationThreshold)
				{
					shouldDeprioritize = true;
				}
			}
			bool shouldThrottle = false;
			if (SubmissionConfiguration.Instance.App.SenderRateThrottlingEnabled)
			{
				long num2 = this.rateTrackerForThrottling.IncrementSenderRate(mailboxGuid, originalCreateTime);
				if (num2 > (long)SubmissionConfiguration.Instance.App.SenderRateThrottlingThreshold)
				{
					shouldThrottle = true;
					this.rateTrackerForThrottling.ResetSenderRate(mailboxGuid, originalCreateTime);
				}
			}
			MapiSubmissionInfo submissionInfo = new MapiSubmissionInfo(serverDN, mailboxGuid, entryId, parentEntryId, eventCounter, serverFqdn, networkAddressBytes, mdbGuid, databaseName, originalCreateTime, isPublicFolder, tenantHint, mailboxHopLatency, latencyTracker, shouldDeprioritize, shouldThrottle, this.storeDriverTracer);
			return this.SubmitMessageImpl(submissionInfo, submissionPoisonHandler, quarantineHandler);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000A23C File Offset: 0x0000843C
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "StoreDriverSubmission";
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000A244 File Offset: 0x00008444
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			bool flag = parameters.Argument.IndexOf("exceptions", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("callstacks", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag3 = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag4 = flag3 || parameters.Argument.IndexOf("basic", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag5 = parameters.Argument.IndexOf("currentThreads", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag6 = flag4 || parameters.Argument.IndexOf("config", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag7 = !flag6 || parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
			if (flag7)
			{
				xelement.Add(new XElement("help", "Supported arguments: config, basic, verbose, exceptions, callstacks, currentThreads, help."));
			}
			if (flag6)
			{
				SubmissionConfiguration.Instance.App.AddDiagnosticInfo(xelement);
			}
			if (flag4)
			{
				xelement.Add(new XElement("submittingThreads", SubmissionThreadLimiter.ConcurrentSubmissions));
				xelement.Add(SubmissionThreadLimiter.DatabaseThreadMap.GetDiagnosticInfo(new XElement("SubmissionDatabaseThreadMap")));
			}
			if (flag5 && this.SubmissionsInProgress != null)
			{
				xelement.Add(this.SubmissionsInProgress.GetDiagnosticInfo());
			}
			if (flag3)
			{
				xelement.Add(MailItemSubmitter.GetDiagnosticInfo());
			}
			if (flag)
			{
				StoreDriverSubmission.DumpExceptionStatistics(xelement);
			}
			if (flag2)
			{
				StoreDriverSubmission.DumpExceptionCallstacks(xelement);
			}
			return xelement;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000A3E4 File Offset: 0x000085E4
		internal static void DumpExceptionStatistics(XElement storeDriverElement)
		{
			XElement xelement = new XElement("ExceptionStatisticRecords");
			lock (StoreDriverSubmission.submissionExceptionStatisticRecords)
			{
				XElement xelement2 = new XElement("SubmissionExceptionStatistics");
				foreach (KeyValuePair<string, StoreDriverSubmission.ExceptionStatisticRecord<StoreDriverSubmission.SubmissionOccurrenceRecord>> keyValuePair in StoreDriverSubmission.submissionExceptionStatisticRecords)
				{
					XElement xelement3 = new XElement("Exception");
					xelement3.Add(new XAttribute("HitCount", keyValuePair.Value.CountSinceServiceStart));
					xelement3.Add(new XAttribute("Type", keyValuePair.Key));
					foreach (StoreDriverSubmission.SubmissionOccurrenceRecord submissionOccurrenceRecord in keyValuePair.Value.LastOccurrences)
					{
						xelement3.Add(submissionOccurrenceRecord.GetDiagnosticInfo());
					}
					xelement2.Add(xelement3);
				}
				xelement.Add(xelement2);
			}
			storeDriverElement.Add(xelement);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000A53C File Offset: 0x0000873C
		internal static void DumpExceptionCallstacks(XElement storeDriverElement)
		{
			XElement xelement = new XElement("ExceptionCallstackRecords");
			lock (StoreDriverSubmission.exceptionSubmissionCallstackRecords)
			{
				XElement xelement2 = new XElement("SubmissionCallstackRecords");
				foreach (KeyValuePair<MessageAction, Dictionary<string, StoreDriverSubmission.SubmissionOccurrenceRecord>> keyValuePair in StoreDriverSubmission.exceptionSubmissionCallstackRecords)
				{
					XElement xelement3 = new XElement(keyValuePair.Key.ToString());
					foreach (KeyValuePair<string, StoreDriverSubmission.SubmissionOccurrenceRecord> keyValuePair2 in keyValuePair.Value)
					{
						XElement diagnosticInfo = keyValuePair2.Value.GetDiagnosticInfo();
						XElement xelement4 = new XElement("Callstack", keyValuePair2.Key);
						xelement4.Add(new XAttribute("Length", keyValuePair2.Key.Length));
						diagnosticInfo.Add(xelement4);
						xelement3.Add(diagnosticInfo);
					}
					xelement2.Add(xelement3);
				}
				xelement.Add(xelement2);
			}
			storeDriverElement.Add(xelement);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A6D0 File Offset: 0x000088D0
		internal static void RecordExceptionForDiagnostics(MessageStatus messageStatus, IMessageConverter messageConverter)
		{
			if (messageStatus.Exception != null && (messageStatus.Action == MessageAction.NDR || messageStatus.Action == MessageAction.Retry || messageStatus.Action == MessageAction.RetryQueue || messageStatus.Action == MessageAction.Reroute || messageStatus.Action == MessageAction.RetryMailboxServer || messageStatus.Action == MessageAction.Skip) && messageConverter.IsOutbound)
			{
				StoreDriverSubmission.UpdateSubmissionExceptionStatisticRecords(messageStatus, Components.TransportAppConfig.MapiSubmission.MaxStoreDriverSubmissionExceptionOccurrenceHistoryPerException, Components.Configuration.AppConfig.MapiSubmission.MaxStoreDriverSubmissionExceptionCallstackHistoryPerBucket, (MailItemSubmitter)messageConverter);
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000A754 File Offset: 0x00008954
		internal static void ClearExceptionRecords()
		{
			lock (StoreDriverSubmission.exceptionSubmissionCallstackRecords)
			{
				StoreDriverSubmission.exceptionSubmissionCallstackRecords.Clear();
			}
			lock (StoreDriverSubmission.submissionExceptionStatisticRecords)
			{
				StoreDriverSubmission.submissionExceptionStatisticRecords.Clear();
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000A7CC File Offset: 0x000089CC
		internal static void UpdateSubmissionExceptionStatisticRecords(MessageStatus messageStatus, int lastOccurrencesPerException, int callstacksPerBucket, MailItemSubmitter mailItemSubmitter)
		{
			MapiSubmissionInfo mapiSubmissionInfo = mailItemSubmitter.SubmissionInfo as MapiSubmissionInfo;
			StoreDriverSubmission.SubmissionOccurrenceRecord submissionOccurrenceRecord = new StoreDriverSubmission.SubmissionOccurrenceRecord(DateTime.UtcNow, mailItemSubmitter.StartTime, mailItemSubmitter.SubmissionInfo.MdbGuid, (mapiSubmissionInfo == null) ? Guid.Empty : mapiSubmissionInfo.MailboxGuid, (mapiSubmissionInfo == null) ? null : mapiSubmissionInfo.EntryId, (mapiSubmissionInfo == null) ? null : mapiSubmissionInfo.ParentEntryId, (mapiSubmissionInfo == null) ? 0L : mapiSubmissionInfo.EventCounter, mailItemSubmitter.OrganizationId, mailItemSubmitter.ErrorCode, mailItemSubmitter.SubmissionInfo.MailboxFqdn, mailItemSubmitter.Item.HasMessageItem ? mailItemSubmitter.Item.Item.InternetMessageId : null, mailItemSubmitter.SubmissionConnectionId, mailItemSubmitter.MessageSize, mailItemSubmitter.RecipientCount, mailItemSubmitter.Result.Sender, mailItemSubmitter.Stage);
			if (lastOccurrencesPerException > 0)
			{
				string key = StoreDriverSubmission.GenerateExceptionKey(messageStatus);
				lock (StoreDriverSubmission.submissionExceptionStatisticRecords)
				{
					StoreDriverSubmission.ExceptionStatisticRecord<StoreDriverSubmission.SubmissionOccurrenceRecord> value;
					if (!StoreDriverSubmission.submissionExceptionStatisticRecords.TryGetValue(key, out value))
					{
						value = default(StoreDriverSubmission.ExceptionStatisticRecord<StoreDriverSubmission.SubmissionOccurrenceRecord>);
						value.LastOccurrences = new Queue<StoreDriverSubmission.SubmissionOccurrenceRecord>(lastOccurrencesPerException);
					}
					if (value.LastOccurrences.Count == lastOccurrencesPerException)
					{
						value.LastOccurrences.Dequeue();
					}
					value.LastOccurrences.Enqueue(submissionOccurrenceRecord);
					value.CountSinceServiceStart++;
					StoreDriverSubmission.submissionExceptionStatisticRecords[key] = value;
				}
			}
			StoreDriverSubmission.UpdateSubmissionExceptionCallstackRecords(messageStatus, callstacksPerBucket, submissionOccurrenceRecord);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000A940 File Offset: 0x00008B40
		internal void ThrowIfStopped()
		{
			if (this.stopped)
			{
				throw new StoreDriverSubmissionRetiredException();
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A954 File Offset: 0x00008B54
		private static Exception FaultInjectionCallback(string exceptionType)
		{
			LocalizedString localizedString = new LocalizedString("Fault injection.");
			if (exceptionType.Equals("Microsoft.Exchange.Data.Storage.StoragePermanentException", StringComparison.OrdinalIgnoreCase))
			{
				return new StoragePermanentException(localizedString);
			}
			if (exceptionType.Equals("Microsoft.Exchange.Data.Storage.StorageTransientException", StringComparison.OrdinalIgnoreCase))
			{
				return new StorageTransientException(localizedString);
			}
			if (exceptionType.Equals("Microsoft.Mapi.MapiExceptionSessionLimit", StringComparison.OrdinalIgnoreCase))
			{
				Exception innerException = MapiExceptionHelper.SessionLimitException(localizedString);
				return new TooManyObjectsOpenedException(localizedString, innerException);
			}
			if (exceptionType.Equals("Microsoft.Mapi.MapiExceptionUnconfigured", StringComparison.OrdinalIgnoreCase))
			{
				Exception innerException2 = MapiExceptionHelper.UnconfiguredException(localizedString);
				return new MailboxUnavailableException(localizedString, innerException2);
			}
			if (exceptionType.Equals("CannotGetSiteInfoException", StringComparison.OrdinalIgnoreCase))
			{
				return new StoragePermanentException(localizedString, new CannotGetSiteInfoException(localizedString));
			}
			if (exceptionType.Equals("System.ArgumentException", StringComparison.OrdinalIgnoreCase))
			{
				return new ArgumentException(localizedString);
			}
			if (exceptionType.StartsWith("Microsoft.Mapi.MapiException", StringComparison.OrdinalIgnoreCase))
			{
				return FaultInjectionHelper.CreateXsoExceptionFromMapiException(exceptionType, localizedString);
			}
			return new ApplicationException(localizedString);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000AA38 File Offset: 0x00008C38
		private static string GenerateExceptionKey(MessageStatus messageStatus)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(messageStatus.Action.ToString());
			stringBuilder.Append(":");
			stringBuilder.Append(messageStatus.Exception.GetType().FullName);
			Exception ex = messageStatus.Exception;
			if (ex is SmtpResponseException)
			{
				stringBuilder.Append(";");
				stringBuilder.Append(messageStatus.Response.StatusCode);
				stringBuilder.Append(";");
				stringBuilder.Append(messageStatus.Response.EnhancedStatusCode);
			}
			while (ex.InnerException != null)
			{
				stringBuilder.Append("~");
				stringBuilder.Append(ex.InnerException.GetType().Name);
				ex = ex.InnerException;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000AB18 File Offset: 0x00008D18
		private static void UpdateSubmissionExceptionCallstackRecords(MessageStatus messageStatus, int callstacksPerBucket, StoreDriverSubmission.SubmissionOccurrenceRecord occurrenceRecord)
		{
			if (callstacksPerBucket > 0)
			{
				string key = messageStatus.Exception.ToString();
				lock (StoreDriverSubmission.exceptionSubmissionCallstackRecords)
				{
					Dictionary<string, StoreDriverSubmission.SubmissionOccurrenceRecord> dictionary;
					if (!StoreDriverSubmission.exceptionSubmissionCallstackRecords.TryGetValue(messageStatus.Action, out dictionary))
					{
						dictionary = new Dictionary<string, StoreDriverSubmission.SubmissionOccurrenceRecord>(callstacksPerBucket);
						StoreDriverSubmission.exceptionSubmissionCallstackRecords[messageStatus.Action] = dictionary;
					}
					StoreDriverSubmission.SubmissionOccurrenceRecord submissionOccurrenceRecord;
					if (!dictionary.TryGetValue(key, out submissionOccurrenceRecord) && dictionary.Count == callstacksPerBucket)
					{
						DateTime t = DateTime.MaxValue;
						string key2 = null;
						foreach (KeyValuePair<string, StoreDriverSubmission.SubmissionOccurrenceRecord> keyValuePair in dictionary)
						{
							if (keyValuePair.Value.Timestamp < t)
							{
								t = keyValuePair.Value.Timestamp;
								key2 = keyValuePair.Key;
							}
						}
						dictionary.Remove(key2);
					}
					dictionary[key] = occurrenceRecord;
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000AC2C File Offset: 0x00008E2C
		private static void RefreshPerformanceCounters(object state)
		{
			StoreDriverSubmissionDatabasePerfCounters.RefreshPerformanceCounters();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000AC34 File Offset: 0x00008E34
		private MailSubmissionResult SubmitMessageImpl(MapiSubmissionInfo submissionInfo, SubmissionPoisonHandler submissionPoisonHandler, QuarantineHandler quarantineHandler)
		{
			MailSubmissionResult mailSubmissionResult = new MailSubmissionResult();
			if (this.Retired)
			{
				mailSubmissionResult.ErrorCode = 2214592514U;
				return mailSubmissionResult;
			}
			string mailboxFqdn = submissionInfo.MailboxFqdn;
			string database = submissionInfo.MdbGuid.ToString();
			MailSubmissionResult result;
			using (submissionInfo.GetTraceFilter())
			{
				using (SubmissionConnectionWrapper connection = SubmissionConnectionPool.GetConnection(mailboxFqdn, database))
				{
					using (SubmissionThreadLimiter submissionThreadLimiter = new SubmissionThreadLimiter())
					{
						SubmissionPoisonContext submissionPoisonContext = null;
						try
						{
							submissionThreadLimiter.BeginSubmission(connection.Id, mailboxFqdn, database);
							if (this.Retired)
							{
								mailSubmissionResult.ErrorCode = 2214592514U;
								connection.SubmissionAborted("Retiring.");
								result = mailSubmissionResult;
							}
							else
							{
								this.storeDriverTracer.StoreDriverSubmissionTracer.TracePfdPass<int, MapiSubmissionInfo>(this.storeDriverTracer.MessageProbeActivityId, 0L, "PFD ESD {0} Processing SubmitMessage for {1}", 27547, submissionInfo);
								submissionPoisonContext = submissionInfo.GetPoisonContext();
								MailItemSubmitter mailItemSubmitter2;
								MailItemSubmitter mailItemSubmitter = mailItemSubmitter2 = new MailItemSubmitter(connection.Id, submissionInfo, this.sendAsManager, submissionPoisonHandler, submissionPoisonContext, this);
								try
								{
									QuarantineInfoContext quarantineInfoContext;
									TimeSpan timeSpan;
									if (SubmissionConfiguration.Instance.App.EnableMailboxQuarantine && quarantineHandler.IsResourceQuarantined(submissionPoisonContext.ResourceGuid, out quarantineInfoContext, out timeSpan))
									{
										mailSubmissionResult.ErrorCode = 1140850696U;
										mailSubmissionResult.QuarantineTimeSpan = timeSpan;
										mailSubmissionResult.DiagnosticInfo = string.Format("{0}:{1}, QuarantineRemainingTimeSpan:{2}", "QuarantineStart", quarantineInfoContext.QuarantineStartTime, timeSpan);
										connection.SubmissionFailed(string.Format("Resource {0} is in quarantined state", submissionPoisonContext.ResourceGuid));
										return mailSubmissionResult;
									}
									if (this.PoisonMessageDectionEnabled && submissionPoisonHandler.VerifyPoisonMessage(submissionPoisonContext))
									{
										this.LogPoisonMessageMTL(submissionInfo, submissionPoisonContext);
										if (SubmissionConfiguration.Instance.App.EnableSendNdrForPoisonMessage)
										{
											if (!submissionPoisonHandler.VerifyPoisonNdrSent(submissionPoisonContext))
											{
												mailItemSubmitter.HandlePoisonMessageNdrSubmission();
												mailSubmissionResult.ErrorCode = StoreDriverSubmissionUtils.MapSubmissionStatusErrorCodeToPoisonErrorCode(mailItemSubmitter.Result.ErrorCode);
											}
										}
										else
										{
											mailSubmissionResult.ErrorCode = 3U;
										}
										connection.SubmissionAborted(string.Format("Poison Context Info: Resource = {0};EventCounter = {1}.", submissionPoisonContext.ResourceGuid, submissionPoisonContext.MapiEventCounter));
										return mailSubmissionResult;
									}
									Thread currentThread = Thread.CurrentThread;
									try
									{
										this.SubmissionsInProgress[currentThread] = mailItemSubmitter;
										mailItemSubmitter.Submit();
										mailSubmissionResult.RemoteHostName = mailItemSubmitter.Result.RemoteHostName;
									}
									finally
									{
										this.SubmissionsInProgress.Remove(currentThread);
									}
								}
								finally
								{
									if (mailItemSubmitter2 != null)
									{
										((IDisposable)mailItemSubmitter2).Dispose();
									}
								}
								if (mailItemSubmitter.Result.ErrorCode == 0U)
								{
									connection.SubmissionSuccessful(mailItemSubmitter.MessageSize, mailItemSubmitter.RecipientCount);
								}
								else
								{
									StringBuilder stringBuilder = new StringBuilder();
									stringBuilder.Append("HResult: ");
									stringBuilder.Append(mailItemSubmitter.Result.ErrorCode.ToString());
									stringBuilder.Append("; DiagnosticInfo: ");
									stringBuilder.Append(mailItemSubmitter.Result.DiagnosticInfo);
									connection.SubmissionFailed(stringBuilder.ToString());
								}
								if (mailItemSubmitter.Result.ErrorCode == 3U)
								{
									submissionInfo.LogEvent(SubmissionInfo.Event.StoreDriverSubmissionPoisonMessageInSubmission);
								}
								result = mailItemSubmitter.Result;
							}
						}
						catch (ThreadLimitExceededException ex)
						{
							connection.SubmissionAborted(ex.Message);
							mailSubmissionResult.ErrorCode = 1090519042U;
							mailSubmissionResult.DiagnosticInfo = ex.Message;
							result = mailSubmissionResult;
						}
						catch (Exception exception)
						{
							try
							{
								submissionPoisonHandler.SavePoisonContext(submissionPoisonContext);
								string exceptionDiagnosticInfo = StorageExceptionHandler.GetExceptionDiagnosticInfo(exception);
								connection.SubmissionFailed("Exception: " + exceptionDiagnosticInfo);
								submissionInfo.LogEvent(SubmissionInfo.Event.StoreDriverSubmissionPoisonMessage, exception);
								FailFast.Fail(exception);
							}
							catch (Exception ex2)
							{
								StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_StoreDriverSubmissionFailFastFailure, null, new object[]
								{
									ex2
								});
							}
							result = mailSubmissionResult;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000B09C File Offset: 0x0000929C
		private void LogPoisonMessageMTL(SubmissionInfo submissionInfo, SubmissionPoisonContext submissionPoisonContext)
		{
			this.storeDriverTracer.StoreDriverSubmissionTracer.TracePass<SubmissionInfo>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Found poison message for {0}", submissionInfo);
			MsgTrackPoisonInfo msgTrackingPoisonInfo = new MsgTrackPoisonInfo(submissionInfo.NetworkAddress, submissionInfo.MailboxFqdn, StoreDriverSubmission.LocalIPAddress, submissionPoisonContext.MapiEventCounter.ToString() + ": " + submissionPoisonContext.ResourceGuid.ToString());
			MessageTrackingLog.TrackPoisonMessage(MessageTrackingSource.STOREDRIVER, msgTrackingPoisonInfo);
			submissionInfo.LogEvent(SubmissionInfo.Event.StoreDriverSubmissionPoisonMessageInSubmission);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B120 File Offset: 0x00009320
		private string GetSubmissionContext(MapiSubmissionInfo submissionInfo)
		{
			return string.Format(CultureInfo.InvariantCulture, "MDB:{0}, Mailbox:{1}, Event:{2}, CreationTime:{3}", new object[]
			{
				submissionInfo.MdbGuid,
				submissionInfo.MailboxGuid,
				submissionInfo.EventCounter,
				submissionInfo.OriginalCreateTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo)
			});
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B18C File Offset: 0x0000938C
		private void StartSubmission()
		{
			try
			{
				ProcessAccessManager.RegisterComponent(this);
			}
			catch (Exception ex)
			{
				this.storeDriverTracer.StoreDriverSubmissionTracer.TraceFail<string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Failed to start Store Driver Submission: {0}", ex.ToString());
				StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_StoreDriverSubmissionStartFailure, null, new object[]
				{
					ex
				});
				throw new TransportComponentLoadFailedException(ex.Message, ex);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000B200 File Offset: 0x00009400
		private void StopSubmission()
		{
			this.storeDriverTracer.StoreDriverSubmissionTracer.TracePass(this.storeDriverTracer.MessageProbeActivityId, 0L, "Stop submission");
			while (SubmissionThreadLimiter.ConcurrentSubmissions > 0)
			{
				Thread.Sleep(500);
			}
		}

		// Token: 0x040000A3 RID: 163
		private const string ProcessAccessManagerComponentName = "StoreDriverSubmission";

		// Token: 0x040000A4 RID: 164
		private static IPHostEntry localIp;

		// Token: 0x040000A5 RID: 165
		private static string receivedHeaderTcpInfo;

		// Token: 0x040000A6 RID: 166
		private static ExEventLog eventLogger = new ExEventLog(new Guid("{597e9983-c5b7-425c-b17f-983e354b783a}"), "MSExchange Store Driver Submission");

		// Token: 0x040000A7 RID: 167
		private static Dictionary<MessageAction, Dictionary<string, StoreDriverSubmission.SubmissionOccurrenceRecord>> exceptionSubmissionCallstackRecords = new Dictionary<MessageAction, Dictionary<string, StoreDriverSubmission.SubmissionOccurrenceRecord>>(6);

		// Token: 0x040000A8 RID: 168
		private static Dictionary<string, StoreDriverSubmission.ExceptionStatisticRecord<StoreDriverSubmission.SubmissionOccurrenceRecord>> submissionExceptionStatisticRecords = new Dictionary<string, StoreDriverSubmission.ExceptionStatisticRecord<StoreDriverSubmission.SubmissionOccurrenceRecord>>(100);

		// Token: 0x040000A9 RID: 169
		private static GuardedTimer performanceCounterMaintenanceTimer;

		// Token: 0x040000AA RID: 170
		private readonly object syncObject = new object();

		// Token: 0x040000AB RID: 171
		private readonly SenderRateTracker rateTrackerForDeprioritization = new SenderRateTracker(TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(10.0));

		// Token: 0x040000AC RID: 172
		private readonly SenderRateTracker rateTrackerForThrottling = new SenderRateTracker(TimeSpan.FromMinutes(1.0), TimeSpan.FromSeconds(10.0));

		// Token: 0x040000AD RID: 173
		private readonly SendAsManager sendAsManager = new SendAsManager();

		// Token: 0x040000AE RID: 174
		private IStoreDriverTracer storeDriverTracer;

		// Token: 0x040000AF RID: 175
		private bool paused;

		// Token: 0x040000B0 RID: 176
		private volatile bool stopped;

		// Token: 0x040000B1 RID: 177
		private SubmissionsInProgress submissionsInProgress;

		// Token: 0x02000028 RID: 40
		private struct ExceptionStatisticRecord<T>
		{
			// Token: 0x040000B4 RID: 180
			internal Queue<T> LastOccurrences;

			// Token: 0x040000B5 RID: 181
			internal int CountSinceServiceStart;
		}

		// Token: 0x02000029 RID: 41
		private struct SubmissionOccurrenceRecord
		{
			// Token: 0x060001C2 RID: 450 RVA: 0x0000B26C File Offset: 0x0000946C
			internal SubmissionOccurrenceRecord(DateTime timestamp, DateTime submissionStart, Guid mdbGuid, Guid mailboxGuid, byte[] entryID, byte[] parentEntryID, long eventCounter, OrganizationId organizationID, uint errorCode, string mailboxServer, string messageID, ulong sessionID, long messageSize, int recipientCount, string sender, MailItemSubmitter.SubmissionStage stage)
			{
				this.timestamp = timestamp;
				this.submissionStart = submissionStart;
				this.mdbGuid = mdbGuid;
				this.mailboxGuid = mailboxGuid;
				this.entryID = entryID;
				this.parentEntryID = parentEntryID;
				this.eventCounter = eventCounter;
				this.organizationID = organizationID;
				this.errorCode = errorCode;
				this.mailboxServer = mailboxServer;
				this.messageID = messageID;
				this.sessionID = sessionID;
				this.messageSize = messageSize;
				this.recipientCount = recipientCount;
				this.sender = sender;
				this.stage = stage;
			}

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000B2F6 File Offset: 0x000094F6
			internal DateTime Timestamp
			{
				get
				{
					return this.timestamp;
				}
			}

			// Token: 0x060001C4 RID: 452 RVA: 0x0000B300 File Offset: 0x00009500
			internal XElement GetDiagnosticInfo()
			{
				return new XElement("Occurrence", new object[]
				{
					new XElement("HitUtc", this.timestamp.ToString(CultureInfo.InvariantCulture)),
					new XElement("SubmissionStart", this.submissionStart.ToString(CultureInfo.InvariantCulture)),
					new XElement("MdbGuid", this.mdbGuid),
					new XElement("MailboxGuid", this.mailboxGuid),
					new XElement("EntryID", this.entryID),
					new XElement("ParentEntryID", this.parentEntryID),
					new XElement("EventCounter", this.eventCounter),
					new XElement("OrgID", this.organizationID),
					new XElement("Stage", this.stage),
					new XElement("ErrorCode", this.errorCode),
					new XElement("MailboxServer", this.mailboxServer),
					new XElement("MessageID", this.messageID),
					new XElement("SessionID", this.sessionID),
					new XElement("MessageSize", this.messageSize),
					new XElement("RecipientCount", this.recipientCount)
				});
			}

			// Token: 0x040000B6 RID: 182
			private DateTime timestamp;

			// Token: 0x040000B7 RID: 183
			private DateTime submissionStart;

			// Token: 0x040000B8 RID: 184
			private Guid mdbGuid;

			// Token: 0x040000B9 RID: 185
			private Guid mailboxGuid;

			// Token: 0x040000BA RID: 186
			private byte[] entryID;

			// Token: 0x040000BB RID: 187
			private byte[] parentEntryID;

			// Token: 0x040000BC RID: 188
			private long eventCounter;

			// Token: 0x040000BD RID: 189
			private OrganizationId organizationID;

			// Token: 0x040000BE RID: 190
			private MailItemSubmitter.SubmissionStage stage;

			// Token: 0x040000BF RID: 191
			private uint errorCode;

			// Token: 0x040000C0 RID: 192
			private string mailboxServer;

			// Token: 0x040000C1 RID: 193
			private string messageID;

			// Token: 0x040000C2 RID: 194
			private ulong sessionID;

			// Token: 0x040000C3 RID: 195
			private long messageSize;

			// Token: 0x040000C4 RID: 196
			private int recipientCount;

			// Token: 0x040000C5 RID: 197
			private string sender;
		}
	}
}

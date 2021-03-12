using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000046 RID: 70
	internal class MRSService : DisposeTrackableBase, IDiagnosable
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00017610 File Offset: 0x00015810
		static MRSService()
		{
			ServerThrottlingResource.InitializeServerThrottlingObjects(true);
			ResourceLoadDelayInfo.Initialize();
			ADSession.DisableAdminTopologyMode();
			MRSService.NextFullScanTime = DateTime.MinValue;
			MRSService.scheduledLogsList = new List<ILogProcessable>();
			MRSService.jobPoller = new PeriodicJobExecuter("PickHeavyJobs", new PeriodicJobExecuter.JobPollerCallback(MRSService.PickHeavyJobs), 0.1);
			MRSService.lightJobPoller = new PeriodicJobExecuter("PickLightJobs", new PeriodicJobExecuter.JobPollerCallback(MRSService.PickLightJobs), 0.1);
			MRSService.logDumper = new PeriodicJobExecuter("DumpLogFiles", new PeriodicJobExecuter.JobPollerCallback(MRSService.DumpLogFiles), 0.0);
			MRSService.issueCache = new MRSIssueCache();
			MailboxReplicationServicePerformanceCounters.LastScanTime.RawValue = CommonUtils.TimestampToPerfcounterLong(DateTime.UtcNow);
			MRSService.serviceStartTime = DateTime.UtcNow;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00017707 File Offset: 0x00015907
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0001770E File Offset: 0x0001590E
		public static MRSService Instance
		{
			get
			{
				return MRSService.instance;
			}
			set
			{
				MRSService.instance = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00017716 File Offset: 0x00015916
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0001771D File Offset: 0x0001591D
		public static DateTime NextFullScanTime { get; private set; }

		// Token: 0x060003B5 RID: 949 RVA: 0x00017725 File Offset: 0x00015925
		public static void LogEvent(ExEventLog.EventTuple tuple, params object[] args)
		{
			CommonUtils.LogEvent(tuple, args);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001772E File Offset: 0x0001592E
		public static bool JobIsActive(Guid mailboxGuid)
		{
			CommonUtils.CheckForServiceStopping();
			return MailboxSyncerJobs.FindJob(mailboxGuid, false) != null;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00017742 File Offset: 0x00015942
		public static void SyncNow(SyncNowNotification notification)
		{
			if (!notification.SyncNowNotificationFlags.HasFlag(SyncNowNotificationFlags.ActivateJob))
			{
				notification.SyncNowNotificationFlags.HasFlag(SyncNowNotificationFlags.Send);
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001777C File Offset: 0x0001597C
		public static void Tickle(Guid mailboxGuid, Guid requestQueueMdbGuid, MoveRequestNotification operation)
		{
			MrsTracer.Service.Debug("Processing tickled mailbox: {0}, requestQueueMdbGuid: {1}, operation: {2}", new object[]
			{
				mailboxGuid,
				requestQueueMdbGuid,
				operation
			});
			if (MailboxSyncerJobs.ProcessJob(mailboxGuid, false, delegate(BaseJob job)
			{
				job.NeedToRefreshRequest = true;
			}))
			{
				return;
			}
			MRSQueue mrsqueue = MRSQueue.Get(requestQueueMdbGuid);
			switch (operation)
			{
			case MoveRequestNotification.Created:
			case MoveRequestNotification.Canceled:
				mrsqueue.Tickle(MRSQueue.ScanType.Heavy);
				return;
			case MoveRequestNotification.Updated:
			case MoveRequestNotification.SuspendResume:
				mrsqueue.Tickle(MRSQueue.ScanType.Light);
				mrsqueue.Tickle(MRSQueue.ScanType.Heavy);
				return;
			default:
				return;
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00017A14 File Offset: 0x00015C14
		public static void ProcessMoveRequestCreatedNotification(Guid requestGuid, Guid mdbGuid)
		{
			using (RequestJobProvider requestJobProvider = new RequestJobProvider(mdbGuid))
			{
				RequestStatisticsBase moveRequest = (RequestStatisticsBase)requestJobProvider.Read<RequestStatisticsBase>(new RequestJobObjectId(requestGuid, mdbGuid, null));
				if (moveRequest != null && !(moveRequest.ValidationResult != RequestJobBase.ValidationResultEnum.Valid))
				{
					if (moveRequest.RequestStyle == RequestStyle.CrossOrg && moveRequest.RequestType == MRSRequestType.Move)
					{
						CommonUtils.CatchKnownExceptions(delegate
						{
							Guid physicalMailboxGuid = moveRequest.ArchiveOnly ? (moveRequest.ArchiveGuid ?? Guid.Empty) : moveRequest.ExchangeGuid;
							ProxyControlFlags proxyControlFlags = ProxyControlFlags.DoNotApplyProxyThrottling | moveRequest.GetProxyControlFlags();
							IMailbox mailbox;
							MailboxType mbxType;
							Guid mdbGuid2;
							if (moveRequest.Direction == RequestDirection.Pull)
							{
								if ((moveRequest.Flags & RequestFlags.RemoteLegacy) != RequestFlags.None)
								{
									mailbox = new MapiSourceMailbox(LocalMailboxFlags.None);
									mailbox.ConfigADConnection(moveRequest.SourceDCName, moveRequest.SourceDCName, moveRequest.SourceCredential);
								}
								else
								{
									mailbox = new RemoteSourceMailbox(moveRequest.RemoteHostName, moveRequest.RemoteOrgName, moveRequest.RemoteCredential, proxyControlFlags, null, true, LocalMailboxFlags.None);
								}
								mbxType = MailboxType.SourceMailbox;
								mdbGuid2 = Guid.Empty;
							}
							else
							{
								if ((moveRequest.Flags & RequestFlags.RemoteLegacy) != RequestFlags.None)
								{
									mailbox = new MapiDestinationMailbox(LocalMailboxFlags.None);
									mailbox.ConfigADConnection(moveRequest.TargetDCName, moveRequest.TargetDCName, moveRequest.TargetCredential);
								}
								else
								{
									mailbox = new RemoteDestinationMailbox(moveRequest.RemoteHostName, moveRequest.RemoteOrgName, moveRequest.RemoteCredential, proxyControlFlags, null, true, LocalMailboxFlags.None);
								}
								mbxType = MailboxType.DestMailboxCrossOrg;
								mdbGuid2 = ((moveRequest.ArchiveOnly ? moveRequest.RemoteArchiveDatabaseGuid : moveRequest.RemoteDatabaseGuid) ?? Guid.Empty);
							}
							using (mailbox)
							{
								mailbox.Config(null, moveRequest.ExchangeGuid, physicalMailboxGuid, null, mdbGuid2, mbxType, null);
								mailbox.Connect(MailboxConnectFlags.None);
								mailbox.UpdateRemoteHostName(moveRequest.UserOrgName);
								mailbox.Disconnect();
							}
						}, null);
					}
				}
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00017AC8 File Offset: 0x00015CC8
		public void StartService()
		{
			MRSService.scheduledLogsList.Add(new MRSSettingsLog());
			MailboxSyncerJobs.StartScheduling();
			MRSService.jobPoller.Start();
			MRSService.lightJobPoller.Start();
			MRSService.issueCache.EnableScanning();
			MRSService.logDumper.Start();
			ProcessAccessManager.RegisterComponent(this);
			ProcessAccessManager.RegisterComponent(MRSService.issueCache);
			ProcessAccessManager.RegisterComponent(ConfigBase<MRSConfigSchema>.Provider);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00017B2C File Offset: 0x00015D2C
		public void StopService()
		{
			MrsTracer.Service.Debug("Stopping service...", new object[0]);
			CommonUtils.ServiceIsStopping = true;
			MRSService.lightJobPoller.WaitForJobToBeDone();
			MRSService.jobPoller.WaitForJobToBeDone();
			MailboxSyncerJobs.WaitForAllJobsToComplete();
			ProcessAccessManager.UnregisterComponent(ConfigBase<MRSConfigSchema>.Provider);
			ProcessAccessManager.UnregisterComponent(MRSService.issueCache);
			ProcessAccessManager.UnregisterComponent(this);
			MailboxSyncerJobs.StopScheduling();
			MRSService.LogEvent(MRSEventLogConstants.Tuple_ServiceStopped, new object[0]);
			MrsTracer.Service.Debug("Service stopped.", new object[0]);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00017DC8 File Offset: 0x00015FC8
		private static TimeSpan PickHeavyJobs()
		{
			TestIntegration.Instance.Barrier("DontPickupJobs", null);
			CommonUtils.CatchKnownExceptions(delegate
			{
				DateTime utcNow = DateTime.UtcNow;
				bool flag = false;
				TimeSpan config = ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("ADInconsistencyCleanUpPeriod");
				if (config != TimeSpan.Zero && MRSService.lastADCleanUpScanFinishTime != null)
				{
					DateTime t = utcNow;
					if (t >= MRSService.lastADCleanUpScanFinishTime + config)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(MRSService.CleanADOprhanAndInconsistency));
					}
				}
				ReservationManager.UpdateHealthState();
				if (utcNow >= MRSService.NextFullScanTime)
				{
					MailboxReplicationServicePerformanceCounters.LastScanTime.RawValue = CommonUtils.TimestampToPerfcounterLong(utcNow);
					MRSService.lastFullScanTime = utcNow;
					flag = true;
					double value = CommonUtils.Randomize(ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("FullScanMoveJobsPollingPeriod").TotalSeconds, 0.2);
					MRSService.NextFullScanTime = utcNow + TimeSpan.FromSeconds(value);
					MrsTracer.Service.Debug("Next full scan at {0}", new object[]
					{
						MRSService.NextFullScanTime
					});
					foreach (Guid mdbGuid in MapiUtils.GetDatabasesOnThisServer())
					{
						MRSQueue.Get(mdbGuid).Tickle(MRSQueue.ScanType.Heavy);
					}
				}
				int num = 0;
				List<Guid> queuesToScan = MRSQueue.GetQueuesToScan(MRSQueue.ScanType.Heavy);
				if (queuesToScan != null)
				{
					foreach (Guid mdbGuid2 in queuesToScan)
					{
						CommonUtils.CheckForServiceStopping();
						bool flag2;
						MRSQueue.Get(mdbGuid2).PickupHeavyJobs(out flag2);
						if (flag2)
						{
							num++;
						}
					}
				}
				if (flag)
				{
					MRSService.lastFullScanDuration = (long)(DateTime.UtcNow - utcNow).TotalMilliseconds;
					MailboxReplicationServicePerformanceCounters.LastScanDuration.RawValue = MRSService.lastFullScanDuration;
					MailboxReplicationServicePerformanceCounters.UnreachableDatabases.RawValue = (long)num;
				}
			}, null);
			return ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("HeavyJobPickupPeriod");
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00017F30 File Offset: 0x00016130
		private static TimeSpan PickLightJobs()
		{
			TestIntegration.Instance.Barrier("DontPickupJobs", null);
			CommonUtils.CatchKnownExceptions(delegate
			{
				DateTime utcNow = DateTime.UtcNow;
				if (utcNow > MRSService.nextLightJobsFullScanTime)
				{
					double num = CommonUtils.Randomize(ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("FullScanLightJobsPollingPeriod").TotalSeconds, 0.2);
					MRSService.nextLightJobsFullScanTime = utcNow + TimeSpan.FromSeconds(num);
					MrsTracer.Service.Debug("Next light job full scan in {0} seconds, at {1}", new object[]
					{
						(int)num,
						MRSService.nextLightJobsFullScanTime
					});
					foreach (Guid mdbGuid in MapiUtils.GetDatabasesOnThisServer())
					{
						MRSQueue.Get(mdbGuid).Tickle(MRSQueue.ScanType.Light);
					}
				}
				List<Guid> queuesToScan = MRSQueue.GetQueuesToScan(MRSQueue.ScanType.Light);
				if (queuesToScan != null)
				{
					foreach (Guid mdbGuid2 in queuesToScan)
					{
						CommonUtils.CheckForServiceStopping();
						MRSQueue.Get(mdbGuid2).PickupLightJobs();
					}
				}
			}, null);
			return ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("LightJobPickupPeriod");
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00017FFC File Offset: 0x000161FC
		private static TimeSpan DumpLogFiles()
		{
			using (List<ILogProcessable>.Enumerator enumerator = MRSService.scheduledLogsList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ILogProcessable scheduledLog = enumerator.Current;
					CommonUtils.CatchKnownExceptions(delegate
					{
						MrsTracer.Service.Debug("Processing {0} MRS scheduled log", new object[]
						{
							scheduledLog.GetType().Name
						});
						scheduledLog.ProcessLogs();
					}, delegate(Exception ex)
					{
						MrsTracer.Service.Debug("Exception occurred while processing {0} MRS scheduled log: {1}", new object[]
						{
							scheduledLog.GetType().Name,
							ex.Message
						});
					});
				}
			}
			return ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("MRSScheduledLogsCheckFrequency");
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00018088 File Offset: 0x00016288
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return MRSService.DiagnosticsComponentName;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000180E0 File Offset: 0x000162E0
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(MRSService.DiagnosticsComponentName);
			MRSDiagnosticArgument arguments;
			try
			{
				arguments = new MRSDiagnosticArgument(parameters.Argument);
			}
			catch (DiagnosticArgumentException ex)
			{
				xelement.Add(new XElement("Error", "Encountered exception: " + ex.Message));
				return xelement;
			}
			xelement.Add(new object[]
			{
				new XElement("ServiceStartTime", MRSService.serviceStartTime),
				new XElement("LastScanDuration", MRSService.lastFullScanDuration),
				new XElement("LastScanTime", MRSService.lastFullScanTime.ToString()),
				new XElement("DurationSinceLastScan", (long)(DateTime.UtcNow - MRSService.lastFullScanTime).TotalMilliseconds),
				new XElement("NextFullScanTime", MRSService.NextFullScanTime.ToString()),
				new XElement("NextLightJobsFullScanTime", MRSService.nextLightJobsFullScanTime.ToString())
			});
			if (arguments.ArgumentCount == 0)
			{
				xelement.Add(new XElement("Help", "Supported arguments: " + arguments.GetSupportedArguments()));
			}
			if (arguments.HasArgument("binaryversions"))
			{
				string assemblyNamePattern = arguments.GetArgument<string>("binaryversions");
				xelement.Add(arguments.RunDiagnosticOperation(() => CommonUtils.GetBinaryVersions(assemblyNamePattern)));
			}
			if (arguments.HasArgument("job"))
			{
				xelement.Add(arguments.RunDiagnosticOperation(() => MailboxSyncerJobs.GetJobsDiagnosticInfo(arguments)));
			}
			if (arguments.HasArgument("reservations"))
			{
				xelement.Add(arguments.RunDiagnosticOperation(() => ReservationManager.GetReservationsDiagnosticInfo(arguments)));
			}
			if (arguments.HasArgument("resources"))
			{
				xelement.Add(arguments.RunDiagnosticOperation(() => ReservationManager.GetResourcesDiagnosticInfo(arguments)));
			}
			if (arguments.HasArgument("queues"))
			{
				xelement.Add(arguments.RunDiagnosticOperation(() => MRSQueue.GetDiagnosticInfo(arguments)));
			}
			if (arguments.HasArgument("workloads"))
			{
				XElement xelement2 = new XElement("Workloads");
				foreach (object obj in Enum.GetValues(typeof(RequestWorkloadType)))
				{
					RequestWorkloadType requestWorkloadType = (RequestWorkloadType)obj;
					if (requestWorkloadType != RequestWorkloadType.None)
					{
						GenericSettingsContext genericSettingsContext = new GenericSettingsContext("RequestWorkloadType", requestWorkloadType.ToString(), null);
						using (genericSettingsContext.Activate())
						{
							bool config = ConfigBase<MRSConfigSchema>.GetConfig<bool>("IsJobPickupEnabled");
							xelement2.Add(new XElement("Workload", new object[]
							{
								new XAttribute("Name", requestWorkloadType.ToString()),
								new XAttribute("IsJobPickupEnabled", config)
							}));
						}
					}
				}
				xelement.Add(xelement2);
			}
			return xelement;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000184F4 File Offset: 0x000166F4
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				MailboxSyncerJobs.StopScheduling();
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000184FE File Offset: 0x000166FE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MRSService>(this);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00018508 File Offset: 0x00016708
		private static void CleanADOprhanAndInconsistency(object obj)
		{
			MRSService.lastADCleanUpScanFinishTime = null;
			try
			{
				CommonUtils.CatchKnownExceptions(new Action(ADInconsistencyCheck.CleanADOrphanAndInconsistency), null);
			}
			finally
			{
				MRSService.lastADCleanUpScanFinishTime = new DateTime?(DateTime.UtcNow);
			}
		}

		// Token: 0x04000179 RID: 377
		private static MRSService instance;

		// Token: 0x0400017A RID: 378
		private static readonly string DiagnosticsComponentName = "MailboxReplicationService";

		// Token: 0x0400017B RID: 379
		private static readonly PeriodicJobExecuter jobPoller;

		// Token: 0x0400017C RID: 380
		private static readonly PeriodicJobExecuter lightJobPoller;

		// Token: 0x0400017D RID: 381
		private static readonly PeriodicJobExecuter logDumper;

		// Token: 0x0400017E RID: 382
		private static readonly MRSIssueCache issueCache;

		// Token: 0x0400017F RID: 383
		private static DateTime nextLightJobsFullScanTime = DateTime.MinValue;

		// Token: 0x04000180 RID: 384
		private static DateTime lastFullScanTime;

		// Token: 0x04000181 RID: 385
		private static DateTime serviceStartTime;

		// Token: 0x04000182 RID: 386
		private static DateTime? lastADCleanUpScanFinishTime = new DateTime?(DateTime.MinValue);

		// Token: 0x04000183 RID: 387
		private static long lastFullScanDuration = 0L;

		// Token: 0x04000184 RID: 388
		private static List<ILogProcessable> scheduledLogsList;
	}
}

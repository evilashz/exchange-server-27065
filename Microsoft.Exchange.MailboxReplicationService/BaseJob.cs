using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000083 RID: 131
	internal abstract class BaseJob : Job, ISettingsContextProvider
	{
		// Token: 0x06000599 RID: 1433 RVA: 0x0002065B File Offset: 0x0001E85B
		public BaseJob()
		{
			this.TestIntegration = new TestIntegration(false);
			this.JobTransferProgress = new BaseJobDiagnosticXml.JobTransferProgress();
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00020690 File Offset: 0x0001E890
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x00020698 File Offset: 0x0001E898
		public Guid RequestJobGuid { get; private set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000206A1 File Offset: 0x0001E8A1
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x000206A9 File Offset: 0x0001E8A9
		protected Exception LastFailure { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x000206B2 File Offset: 0x0001E8B2
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x000206BA File Offset: 0x0001E8BA
		protected Guid RequestKeyGuid { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x000206C3 File Offset: 0x0001E8C3
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x000206CB File Offset: 0x0001E8CB
		public RequestStatisticsBase CachedRequestJob { get; private set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000206D4 File Offset: 0x0001E8D4
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x000206DC File Offset: 0x0001E8DC
		public ADObjectId RequestJobStoringMDB { get; private set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x000206E5 File Offset: 0x0001E8E5
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x000206ED File Offset: 0x0001E8ED
		public string RequestJobIdentity { get; protected set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000206F6 File Offset: 0x0001E8F6
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x000206FE File Offset: 0x0001E8FE
		public TestIntegration TestIntegration { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00020707 File Offset: 0x0001E907
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00020714 File Offset: 0x0001E914
		public int MessagesWritten
		{
			get
			{
				return this.JobTransferProgress.MessagesWritten;
			}
			set
			{
				this.JobTransferProgress.MessagesWritten = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00020722 File Offset: 0x0001E922
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0002072F File Offset: 0x0001E92F
		public ulong MessageSizeWritten
		{
			get
			{
				return this.JobTransferProgress.MessageSizeWritten;
			}
			set
			{
				this.JobTransferProgress.MessageSizeWritten = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0002073D File Offset: 0x0001E93D
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0002074A File Offset: 0x0001E94A
		public int TotalMessages
		{
			get
			{
				return this.JobTransferProgress.TotalMessages;
			}
			set
			{
				this.JobTransferProgress.TotalMessages = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00020758 File Offset: 0x0001E958
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x00020765 File Offset: 0x0001E965
		public ulong TotalMessageByteSize
		{
			get
			{
				return this.JobTransferProgress.TotalMessageByteSize;
			}
			set
			{
				this.JobTransferProgress.TotalMessageByteSize = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00020774 File Offset: 0x0001E974
		public override JobSortKey JobSortKey
		{
			get
			{
				JobSortFlags jobSortFlags = this.IsInteractive ? JobSortFlags.IsInteractive : JobSortFlags.None;
				jobSortFlags |= ((this.Reservation != null) ? JobSortFlags.HasReservations : JobSortFlags.None);
				DateTime lastUpdate = this.TimeTracker.GetTimestamp(RequestJobTimestamp.LastUpdate) ?? DateTime.MinValue;
				return new JobSortKey(this.CachedRequestJob.Priority, jobSortFlags, lastUpdate, this.RequestJobGuid);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x000207DA File Offset: 0x0001E9DA
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x000207E2 File Offset: 0x0001E9E2
		public int TotalFolders { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000207EB File Offset: 0x0001E9EB
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x000207F3 File Offset: 0x0001E9F3
		public int FoldersProcessed { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x000207FC File Offset: 0x0001E9FC
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x00020804 File Offset: 0x0001EA04
		public RequestJobTimeTracker TimeTracker { get; private set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0002080D File Offset: 0x0001EA0D
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x00020815 File Offset: 0x0001EA15
		public SkippedItemCounts SkippedItemCounts { get; protected set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0002081E File Offset: 0x0001EA1E
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x00020826 File Offset: 0x0001EA26
		public FailureHistory FailureHistory { get; private set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0002082F File Offset: 0x0001EA2F
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0002083C File Offset: 0x0001EA3C
		public int OverallProgress
		{
			get
			{
				return this.JobTransferProgress.OverallProgress;
			}
			set
			{
				this.JobTransferProgress.OverallProgress = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0002084A File Offset: 0x0001EA4A
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x00020852 File Offset: 0x0001EA52
		public int RetryCount { get; private set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0002085B File Offset: 0x0001EA5B
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x00020863 File Offset: 0x0001EA63
		public int TotalRetryCount { get; private set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0002086C File Offset: 0x0001EA6C
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x000208E8 File Offset: 0x0001EAE8
		public SyncStage SyncStage
		{
			get
			{
				SyncStage syncStage = SyncStage.None;
				bool flag = true;
				foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
				{
					if (mailboxCopierBase.SyncState == null)
					{
						syncStage = SyncStage.None;
					}
					else if (flag || mailboxCopierBase.SyncState.SyncStage < syncStage)
					{
						syncStage = mailboxCopierBase.SyncState.SyncStage;
					}
					flag = false;
				}
				return syncStage;
			}
			set
			{
				foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
				{
					if (value == SyncStage.None)
					{
						mailboxCopierBase.SyncState = null;
					}
					else if (mailboxCopierBase.SyncState != null)
					{
						mailboxCopierBase.SyncState.SyncStage = value;
					}
				}
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00020954 File Offset: 0x0001EB54
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0002095C File Offset: 0x0001EB5C
		public bool NeedToRefreshRequest
		{
			get
			{
				return this.needToRefreshRequest;
			}
			set
			{
				MrsTracer.Service.Function("BaseJob.set_NeedToRefreshRequest({0})", new object[]
				{
					value
				});
				this.needToRefreshRequest = value;
				if (value)
				{
					base.WakeupJob();
				}
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00020999 File Offset: 0x0001EB99
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x000209A1 File Offset: 0x0001EBA1
		public MDBPerfCounterHelper MDBPerfCounterHelper { get; private set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x000209AA File Offset: 0x0001EBAA
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x000209B2 File Offset: 0x0001EBB2
		public TransferProgressTracker ProgressTracker { get; private set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000209BB File Offset: 0x0001EBBB
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x000209C3 File Offset: 0x0001EBC3
		public ReportData Report { get; private set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x000209CC File Offset: 0x0001EBCC
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x000209D4 File Offset: 0x0001EBD4
		public List<LocalizedString> Warnings { get; private set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x000209DD File Offset: 0x0001EBDD
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x000209E5 File Offset: 0x0001EBE5
		public override ReservationContext Reservation { get; internal set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x000209EE File Offset: 0x0001EBEE
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x000209F6 File Offset: 0x0001EBF6
		public bool AllowInvalidRequest { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000209FF File Offset: 0x0001EBFF
		public bool IsStalledByHigherPriorityJobs
		{
			get
			{
				return this.Reservation != null && ExDateTime.UtcNow - this.lastTaskExecutionTime > this.GetConfig<TimeSpan>("StalledByHigherPriorityJobsTimeout");
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00020A2B File Offset: 0x0001EC2B
		public override WorkloadType WorkloadTypeFromJob
		{
			get
			{
				return CommonUtils.ComputeWlmWorkloadType((int)this.CachedRequestJob.Priority, this.IsInteractive, this.GetConfig<WorkloadType>("WlmWorkloadType"));
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00020A4E File Offset: 0x0001EC4E
		public override bool IsInteractive
		{
			get
			{
				return MoveJob.IsInteractive(this.CachedRequestJob.RequestType, this.CachedRequestJob.WorkloadType);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005D4 RID: 1492
		protected abstract int CopyStartPercentage { get; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005D5 RID: 1493
		protected abstract int CopyEndPercentage { get; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00020A6B File Offset: 0x0001EC6B
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x00020A73 File Offset: 0x0001EC73
		protected DateTime DataGuaranteeCommitTimestamp { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00020A7C File Offset: 0x0001EC7C
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x00020A84 File Offset: 0x0001EC84
		protected TimeSpan IncrementalSyncInterval { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00020A8D File Offset: 0x0001EC8D
		protected bool SkipContentVerification
		{
			get
			{
				return (this.CachedRequestJob != null && this.CachedRequestJob.SkipContentVerification) || this.GetConfig<bool>("DisableContentVerification");
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00020AB1 File Offset: 0x0001ECB1
		protected virtual bool RelinquishAfterOfflineMoveFailure
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00020AB4 File Offset: 0x0001ECB4
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00020ABC File Offset: 0x0001ECBC
		private DateTime DataGuaranteeLastMessageTimestamp { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00020AC5 File Offset: 0x0001ECC5
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x00020ACD File Offset: 0x0001ECCD
		private bool DataGuaranteeWaitLogRolled { get; set; }

		// Token: 0x060005E0 RID: 1504 RVA: 0x00020AD8 File Offset: 0x0001ECD8
		public static void PerformCrashingFailureActions(Guid identifyingGuid, Guid requestGuid, Exception exception, RequestState requestState = RequestState.None, SyncStage syncStage = SyncStage.None)
		{
			QuarantinedJobs.Add(identifyingGuid, exception);
			string text = CommonUtils.FullExceptionMessage(exception, true);
			string arg = CommonUtils.ComputeCallStackHash(exception, 5);
			MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_CrashEvent, new object[]
			{
				CommonUtils.GetFailureType(exception),
				CommonUtils.FullExceptionMessage(exception, true),
				string.Format("Call stack hash: {0}", arg),
				text,
				ExecutionContext.GetDataContext(exception)
			});
			FailureLog.Write(requestGuid, exception, true, requestState, syncStage, null, null);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00020B54 File Offset: 0x0001ED54
		public bool IsMailboxCapabilitySupportedBy(MailboxCapabilities capability, bool isSource)
		{
			foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
			{
				IMailbox mailbox;
				if (!isSource)
				{
					IMailbox destMailbox = mailboxCopierBase.DestMailbox;
					mailbox = destMailbox;
				}
				else
				{
					mailbox = mailboxCopierBase.SourceMailbox;
				}
				IMailbox mailbox2 = mailbox;
				if (mailbox2 == null)
				{
					return false;
				}
				if (!mailbox2.IsMailboxCapabilitySupported(capability))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00020BD0 File Offset: 0x0001EDD0
		protected static DateTime? GetNextScheduledTime(DateTime? startAfter, DateTime? completeAfter, TimeSpan incrementalSyncInterval)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (completeAfter == null)
			{
				return null;
			}
			if (completeAfter.Value < utcNow)
			{
				return null;
			}
			if (startAfter != null && startAfter.Value >= utcNow)
			{
				return new DateTime?(startAfter.Value);
			}
			DateTime dateTime = utcNow.Add(incrementalSyncInterval);
			return new DateTime?((dateTime >= completeAfter.Value) ? completeAfter.Value : dateTime);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00020C5C File Offset: 0x0001EE5C
		public void JobCompletedCallback()
		{
			bool flag = this.Reservation == null && CommonUtils.ServiceIsStopping;
			this.DisposeReservations();
			if (!this.needToRelinquishOnJobCompletion)
			{
				MrsTracer.Service.Debug("Time tracking info at job drop:\n{0}", new object[]
				{
					this.TimeTracker
				});
				return;
			}
			this.Disconnect();
			if (flag)
			{
				return;
			}
			lock (this.syncRoot)
			{
				this.UpdateReportRelinquished(true);
				MrsTracer.Service.Debug("Time tracking info at relinquish:\n{0}", new object[]
				{
					this.TimeTracker
				});
				MRSQueue mrsqueue = MRSQueue.Get(this.RequestJobStoringMDB.ObjectGuid);
				mrsqueue.LastActiveJobFinishTime = DateTime.UtcNow;
				mrsqueue.LastActiveJobFinished = this.GetRequestKeyGuid();
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00020D34 File Offset: 0x0001EF34
		public T GetConfig<T>(string settingName)
		{
			return ConfigBase<MRSConfigSchema>.GetConfig<T>(settingName);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00020D3C File Offset: 0x0001EF3C
		public virtual XElement GetJobDiagnosticInfo(MRSDiagnosticArgument arguments)
		{
			MRSRequestType requestType = this.CachedRequestJob.RequestType;
			BaseJob.BadItemCounts badItemCounts = this.GetBadItemCounts();
			DateTime? dateTime = null;
			if (this.TimeTracker != null)
			{
				dateTime = this.TimeTracker.GetDisplayTimestamp(RequestJobTimestamp.LastProgressCheckpoint);
			}
			this.JobTransferProgress.ThroughputProgressTracker = this.ProgressTracker.GetDiagnosticInfo(arguments);
			BaseJobDiagnosticXml baseJobDiagnosticXml = new BaseJobDiagnosticXml
			{
				RequestGuid = this.GetRequestKeyGuid(),
				RequestQueue = this.RequestJobStoringMDB.ToString(),
				RequestType = requestType.ToString(),
				JobPickupTimestamp = this.jobPickupTimestamp.UniversalTime,
				RetryCount = this.RetryCount,
				TotalRetryCount = this.TotalRetryCount,
				SyncStage = this.SyncStage,
				JobTransferProgressRec = this.JobTransferProgress,
				BadItemsEncountered = badItemCounts.BadItemCount,
				LargeItemsEncountered = badItemCounts.LargeItemCount,
				MissingItemsEncountered = badItemCounts.MissingItemCount,
				LastProgressTimestamp = ((dateTime == null) ? DateTime.MinValue : dateTime.Value),
				TimeTrackerCurrentState = ((this.TimeTracker == null) ? null : this.TimeTracker.CurrentState.ToString())
			};
			if (this.currentlyThrottledResource != null)
			{
				baseJobDiagnosticXml.CurrentlyThrottledResource = this.currentlyThrottledResource.ToString();
				baseJobDiagnosticXml.CurrentlyThrottledResourceMetricType = (int)this.currentlyThrottledResource.MetricType;
				baseJobDiagnosticXml.ThrottledSince = this.throttleStartTimestamp.UniversalTime;
			}
			if (this.Reservation != null)
			{
				baseJobDiagnosticXml.ReservationRecs = new List<BaseJobDiagnosticXml.ReservationRec>();
				foreach (IReservation reservation in this.Reservation.Reservations)
				{
					BaseJobDiagnosticXml.ReservationRec item = new BaseJobDiagnosticXml.ReservationRec
					{
						Id = reservation.Id,
						Flags = reservation.Flags.ToString(),
						ResourceId = reservation.ResourceId
					};
					baseJobDiagnosticXml.ReservationRecs.Add(item);
				}
			}
			if (this.Warnings != null && this.Warnings.Count > 0)
			{
				baseJobDiagnosticXml.Warnings = new List<string>();
				foreach (LocalizedString value in this.Warnings)
				{
					baseJobDiagnosticXml.Warnings.Add(value);
				}
			}
			return baseJobDiagnosticXml.ToDiagnosticInfo(null);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00020FDC File Offset: 0x0001F1DC
		public virtual void Initialize(TransactionalRequestJob rj)
		{
			base.TraceActivityID = rj.RequestGuid.GetHashCode();
			MrsTracer.Service.Function("BaseJob.Initialize: requestGuid={0}, flags=[{1}], BadItemLimit={2}, LargeItemLimit={3}, AllowLargeItems={4}", new object[]
			{
				rj.RequestGuid,
				rj.Flags,
				rj.BadItemLimit,
				rj.LargeItemLimit,
				rj.AllowLargeItems
			});
			this.RequestJobGuid = rj.RequestGuid;
			if (this.RequestKeyGuid == Guid.Empty)
			{
				this.RequestKeyGuid = rj.RequestGuid;
			}
			this.CachedRequestJob = RequestJobProvider.CreateRequestStatistics(rj);
			this.RequestJobIdentity = rj.Identity.ToString();
			this.TotalRetryCount = rj.TotalRetryCount;
			this.requestJobMessageId = rj.MessageId;
			this.RequestJobStoringMDB = rj.WorkItemQueueMdb;
			this.needToRelinquishOnJobCompletion = true;
			this.needToRefreshRequest = false;
			this.jobPickupTimestamp = ExDateTime.UtcNow;
			this.Report = new ReportData(this.GetRequestKeyGuid(), rj.ReportVersion);
			this.MessagesWritten = 0;
			this.MessageSizeWritten = 0UL;
			this.TotalMessages = 0;
			this.TotalMessageByteSize = 0UL;
			this.RetryCount = 0;
			this.lastLazySaveTimestamp = ExDateTime.MinValue;
			this.nextHealthCheckTimestamp = ExDateTime.MinValue;
			this.lastInProgressRequestJobLogCheck = ExDateTime.MinValue;
			this.ProgressTracker = rj.ProgressTracker;
			this.MDBPerfCounterHelper = MDBPerfCounterHelperCollection.GetMDBHelper(this.RequestJobStoringMDB.ObjectGuid, true);
			this.TimeTracker = rj.TimeTracker;
			this.TimeTracker.AttachPerfCounters(this.MDBPerfCounterHelper);
			this.SkippedItemCounts = rj.SkippedItemCounts;
			this.FailureHistory = rj.FailureHistory;
			this.Warnings = new List<LocalizedString>();
			this.TimeTracker.CurrentState = RequestState.InitializingMove;
			if (rj.IsFake)
			{
				return;
			}
			this.ScheduleBeginJob();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000211C0 File Offset: 0x0001F3C0
		public MoveRequestInfo GetMoveRequestInfo()
		{
			MoveRequestInfo moveRequestInfo = null;
			if (this.SyncStage != SyncStage.None)
			{
				BaseJob.BadItemCounts badItemCounts = this.GetBadItemCounts();
				moveRequestInfo = new MoveRequestInfo();
				moveRequestInfo.MailboxGuid = this.GetRequestKeyGuid();
				moveRequestInfo.SyncStage = this.SyncStage;
				moveRequestInfo.PercentComplete = this.OverallProgress;
				moveRequestInfo.BadItemsEncountered = badItemCounts.BadItemCount;
				moveRequestInfo.LargeItemsEncountered = badItemCounts.LargeItemCount;
				moveRequestInfo.ProgressTracker = this.ProgressTracker;
				moveRequestInfo.BytesTransfered = this.ProgressTracker.BytesTransferred;
				moveRequestInfo.BytesPerMinute = this.ProgressTracker.BytesPerMinute;
				moveRequestInfo.ItemsTransfered = this.ProgressTracker.ItemsTransferred;
			}
			return moveRequestInfo;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00021265 File Offset: 0x0001F465
		public IReservation GetReservation(Guid mdbGuid, ReservationFlags flags)
		{
			if (this.Reservation == null)
			{
				return null;
			}
			return this.Reservation.GetReservation(mdbGuid, flags);
		}

		// Token: 0x060005E9 RID: 1513
		public abstract List<MailboxCopierBase> GetAllCopiers();

		// Token: 0x060005EA RID: 1514
		public abstract void ValidateAndPopulateRequestJob(List<ReportEntry> entries);

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00021280 File Offset: 0x0001F480
		protected override IEnumerable<ResourceKey> ResourceDependencies
		{
			get
			{
				HashSet<ResourceKey> hashSet = new HashSet<ResourceKey>();
				hashSet.Add(ProcessorResourceKey.Local);
				foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
				{
					mailboxCopierBase.AddResources(hashSet);
				}
				return hashSet;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000212E8 File Offset: 0x0001F4E8
		public void CheckBadItemCount(bool enforceStrictLimit)
		{
			Unlimited<int> unlimited = this.CachedRequestJob.BadItemLimit;
			Unlimited<int> unlimited2 = this.CachedRequestJob.LargeItemLimit;
			if (!enforceStrictLimit && !this.CachedRequestJob.FailOnFirstBadItem)
			{
				if (!unlimited.IsUnlimited)
				{
					unlimited += 200;
				}
				if (!unlimited2.IsUnlimited)
				{
					unlimited2 += 200;
				}
			}
			BaseJob.BadItemCounts badItemCounts = this.GetBadItemCounts();
			LocalizedException ex = null;
			if (badItemCounts.BadItemCount > unlimited)
			{
				ex = new TooManyBadItemsPermanentException();
			}
			else if (badItemCounts.LargeItemCount > unlimited2)
			{
				ex = new TooManyLargeItemsPermanentException();
			}
			else if (badItemCounts.MissingItemCount > this.GetConfig<int>("ContentVerificationMissingItemThreshold") && badItemCounts.BadItemCount + badItemCounts.MissingItemCount > unlimited)
			{
				ex = new TooManyMissingItemsPermanentException();
			}
			if (ex != null)
			{
				MrsTracer.Service.Error("Too many bad/large/missing items ({0}/{1}/{2}). Bailing.", new object[]
				{
					badItemCounts.BadItemCount,
					badItemCounts.LargeItemCount,
					badItemCounts.MissingItemCount
				});
				this.SaveState(SaveStateFlags.DontSaveRequestJob, null);
				throw ex;
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0002141E File Offset: 0x0001F61E
		public Guid GetRequestKeyGuid()
		{
			return this.RequestKeyGuid;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00021426 File Offset: 0x0001F626
		public RequestJobObjectId GetRequestJobObjectId()
		{
			return new RequestJobObjectId(this.GetRequestKeyGuid(), this.RequestJobStoringMDB.ObjectGuid, this.requestJobMessageId);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00021444 File Offset: 0x0001F644
		public void Disconnect()
		{
			base.CheckDisposed();
			foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
			{
				mailboxCopierBase.Disconnect();
				mailboxCopierBase.UnconfigureProviders();
			}
			this.UnconfigureProviders();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000214A8 File Offset: 0x0001F6A8
		internal void ReportBadItems(MailboxCopierBase mbxCtx, List<BadMessageRec> badItems)
		{
			MrsTracer.Service.Debug("Reporting {0} bad items", new object[]
			{
				badItems.Count
			});
			BadItemClassifier badItemClassifier = new BadItemClassifier();
			foreach (BadMessageRec badMessageRec in badItems)
			{
				if (!mbxCtx.SyncState.BadItems.ContainsKey(badMessageRec.EntryId))
				{
					badItemClassifier.Classify(badMessageRec, this.TestIntegration);
					BadItemMarker badItemMarker = new BadItemMarker(badMessageRec);
					mbxCtx.SyncState.BadItems.Add(badItemMarker.EntryId, badItemMarker);
					if (badMessageRec.RawFailure != null)
					{
						FailureLog.Write(this.RequestJobGuid, badMessageRec.RawFailure, false, this.TimeTracker.CurrentState, SyncStage.BadItem, badMessageRec.FolderName, (badMessageRec.Failure != null) ? badMessageRec.Failure.DataContext : null);
					}
					BadItemLog.Write(this.RequestJobGuid, badMessageRec);
					switch (badMessageRec.Kind)
					{
					case BadItemKind.MissingItem:
					case BadItemKind.MissingFolder:
					case BadItemKind.MisplacedFolder:
						this.Report.Append(MrsStrings.ReportMissingItemEncountered(badMessageRec.ToLocalizedString()), badMessageRec);
						break;
					case BadItemKind.CorruptItem:
					case BadItemKind.CorruptSearchFolderCriteria:
					case BadItemKind.CorruptFolderACL:
					case BadItemKind.CorruptFolderRule:
					case BadItemKind.CorruptFolderProperty:
					case BadItemKind.CorruptInferenceProperties:
					case BadItemKind.CorruptMailboxSetting:
					case BadItemKind.FolderPropertyMismatch:
						this.Report.Append(MrsStrings.ReportBadItemEncountered2(badMessageRec.ToLocalizedString()), badMessageRec);
						break;
					case BadItemKind.LargeItem:
						this.Report.Append(MrsStrings.ReportLargeItemEncountered(badMessageRec.ToLocalizedString()), badMessageRec);
						break;
					}
					if (badMessageRec.Kind != BadItemKind.MissingItem)
					{
						this.GetSkippedItemCounts().AddBadItem(badMessageRec);
					}
				}
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00021668 File Offset: 0x0001F868
		protected void ConfigureMailboxProviders()
		{
			foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
			{
				mailboxCopierBase.ConfigureProviders();
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000216BC File Offset: 0x0001F8BC
		protected void EnumerateAndApplyIncrementalChanges(MailboxCopierBase mbxContext, SyncContext syncContext, MailboxChangesManifest hierarchyChanges)
		{
			MrsTracer.Service.Debug("Applying mailbox changes.", new object[0]);
			mbxContext.ApplyHierarchyChanges(syncContext, hierarchyChanges);
			int num = 0;
			using (IEnumerator<MailboxChanges> enumerator = mbxContext.EnumerateContentChanges(syncContext, hierarchyChanges).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num++;
					MailboxChanges changes = enumerator.Current;
					this.ReportContentChangesEnumerated(mbxContext, changes, num);
					mbxContext.ApplyContentsChanges(syncContext, changes);
				}
			}
			mbxContext.CopyChangedFoldersData();
			mbxContext.ICSSyncState.ProviderState = mbxContext.SourceMailbox.GetMailboxSyncState();
			mbxContext.SaveICSSyncState(true);
			this.Report.Append(MrsStrings.ReportIncrementalSyncCompleted2(mbxContext.TargetTracingID, syncContext.NumberOfHierarchyUpdates, syncContext.CopyMessagesCount.TotalContentCopied));
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00021788 File Offset: 0x0001F988
		protected virtual void UnconfigureProviders()
		{
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0002178A File Offset: 0x0001F98A
		public void CheckServersHealth()
		{
			this.CheckHAandCIHealth();
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00021792 File Offset: 0x0001F992
		protected override void MoveToThrottledState(ResourceKey resource, bool deferDelay)
		{
			this.currentlyThrottledResource = resource;
			this.throttleStartTimestamp = ExDateTime.UtcNow;
			if (deferDelay)
			{
				return;
			}
			this.MoveToThrottledStateNow();
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000217B0 File Offset: 0x0001F9B0
		protected override void StartDeferredDelayIfApplicable()
		{
			if (this.currentlyThrottledResource != null)
			{
				this.MoveToThrottledStateNow();
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000217C0 File Offset: 0x0001F9C0
		public override void RevertToPreviousUnthrottledState()
		{
			if (this.stateBeforeBeingThrottled == null)
			{
				return;
			}
			MrsTracer.Throttling.Debug("Throttle removed by RUBS. Reverting back to previous State: {0} from throttled State: {1}", new object[]
			{
				this.stateBeforeBeingThrottled,
				this.TimeTracker.CurrentState
			});
			this.lastThrottledResource = this.currentlyThrottledResource;
			this.TimeTracker.CurrentState = this.stateBeforeBeingThrottled.Value;
			this.stateBeforeBeingThrottled = null;
			this.currentlyThrottledResource = null;
			this.throttleStartTimestamp = ExDateTime.MinValue;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00021890 File Offset: 0x0001FA90
		public void UpdateReportThrottled()
		{
			lock (this.syncRoot)
			{
				if (this.Reservation != null)
				{
					ExDateTime utcNow = ExDateTime.UtcNow;
					if (!(utcNow - this.lastLazySaveTimestamp < BaseJob.FlushInterval))
					{
						if (!(utcNow - this.lastRJTouchedTimestamp < BaseJob.FlushInterval))
						{
							if (this.TimeTracker != null)
							{
								TimeSpan t = (this.throttleStartTimestamp != ExDateTime.MinValue) ? (ExDateTime.UtcNow - this.throttleStartTimestamp) : TimeSpan.Zero;
								if (t > BaseJob.StallReportingDelay)
								{
									LocalizedString? localizedString = null;
									switch (this.TimeTracker.CurrentState)
									{
									case RequestState.StalledDueToHA:
										localizedString = new LocalizedString?(MrsStrings.MdbReplication);
										break;
									case RequestState.StalledDueToCI:
										localizedString = new LocalizedString?(MrsStrings.ContentIndexing);
										break;
									case RequestState.StalledDueToReadThrottle:
										localizedString = new LocalizedString?(MrsStrings.ReadRpc);
										break;
									case RequestState.StalledDueToWriteThrottle:
										localizedString = new LocalizedString?(MrsStrings.WriteRpc);
										break;
									case RequestState.StalledDueToReadCpu:
										localizedString = new LocalizedString?(MrsStrings.ReadCpu);
										break;
									case RequestState.StalledDueToWriteCpu:
										localizedString = new LocalizedString?(MrsStrings.WriteCpu);
										break;
									case RequestState.StalledDueToReadUnknown:
									case RequestState.StalledDueToWriteUnknown:
										localizedString = new LocalizedString?(MrsStrings.RemoteResource);
										break;
									}
									if (localizedString != null)
									{
										string throttledResource;
										if (this.currentlyThrottledResource != null)
										{
											throttledResource = this.currentlyThrottledResource.ToString();
										}
										else
										{
											throttledResource = this.lastProxyThrottledReason;
										}
										this.Report.Append(MrsStrings.RequestIsStalled(localizedString.Value, throttledResource));
									}
									else if (this.IsStalledByHigherPriorityJobs)
									{
										this.Report.Append(MrsStrings.RequestIsStalledByHigherPriorityJobs);
									}
								}
							}
							this.lastRJTouchedTimestamp = utcNow;
							CommonUtils.CatchKnownExceptions(delegate
							{
								this.SaveRequest(false, null);
							}, delegate(Exception f)
							{
								MrsTracer.Service.Warning("TouchRequestJobPeriodically(): MR-save attempt failed. Error: {0}", new object[]
								{
									f.ToString()
								});
							});
						}
					}
				}
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00021B7C File Offset: 0x0001FD7C
		public void FlushReport(MapiStore systemMbx)
		{
			if (!this.Report.HasNewEntries)
			{
				return;
			}
			CommonUtils.CatchKnownExceptions(delegate
			{
				bool flag = false;
				try
				{
					if (systemMbx == null)
					{
						systemMbx = MapiUtils.GetSystemMailbox(this.RequestJobStoringMDB.ObjectGuid);
						flag = true;
					}
					this.Report.Flush(systemMbx);
				}
				finally
				{
					if (flag)
					{
						systemMbx.Dispose();
					}
				}
			}, delegate(Exception failure)
			{
				MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_ReportFlushFailed, new object[]
				{
					this.RequestJobIdentity,
					this.GetRequestKeyGuid().ToString(),
					this.RequestJobStoringMDB.ToString(),
					CommonUtils.FullExceptionMessage(failure)
				});
			});
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00021BC8 File Offset: 0x0001FDC8
		public MailboxConnectFlags GetConnectFlags(MailboxConnectFlags inFlags)
		{
			MailboxConnectFlags mailboxConnectFlags = inFlags;
			if (this.WorkloadTypeFromJob == WorkloadType.MailboxReplicationServiceHighPriority)
			{
				mailboxConnectFlags |= MailboxConnectFlags.HighPriority;
			}
			if (this.CachedRequestJob.RequestType != MRSRequestType.MailboxRestore && this.GetConfig<bool>("AllowRestoreFromConnectedMailbox"))
			{
				mailboxConnectFlags |= MailboxConnectFlags.AllowRestoreFromConnectedMailbox;
			}
			return mailboxConnectFlags;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00021C07 File Offset: 0x0001FE07
		internal void SaveState(SaveStateFlags flags, BaseJob.UpdateRequestDelegate updateRequestDel)
		{
			this.SaveState(flags, null, updateRequestDel);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00021C14 File Offset: 0x0001FE14
		protected virtual void CalculateCopyingProgress()
		{
			if (this.MessageSizeWritten < this.TotalMessageByteSize)
			{
				this.OverallProgress = this.CopyStartPercentage + (int)((long)(this.CopyEndPercentage - this.CopyStartPercentage) * (long)this.MessageSizeWritten / (long)this.TotalMessageByteSize);
				return;
			}
			this.OverallProgress = this.CopyEndPercentage;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00021CF8 File Offset: 0x0001FEF8
		protected void SaveState(SaveStateFlags flags, MailboxCopierBase currentMailboxContext, BaseJob.UpdateRequestDelegate updateRequestDel)
		{
			if (this.SyncStage == SyncStage.CopyingMessages)
			{
				this.CalculateCopyingProgress();
			}
			ExDateTime utcNow = ExDateTime.UtcNow;
			bool flag = false;
			if (flags.HasFlag(SaveStateFlags.RelinquishLongRunningJob))
			{
				TimeSpan config = this.GetConfig<TimeSpan>("LongRunningJobRelinquishInterval");
				flag = (config > TimeSpan.Zero && utcNow - this.jobPickupTimestamp > config);
			}
			if (flags.HasFlag(SaveStateFlags.Lazy) && !flag)
			{
				if (utcNow < this.lastLazySaveTimestamp + BaseJob.FlushInterval)
				{
					return;
				}
				this.lastLazySaveTimestamp = utcNow;
			}
			else
			{
				this.lastLazySaveTimestamp = ExDateTime.MinValue;
			}
			this.ReportProgress(currentMailboxContext, !flags.HasFlag(SaveStateFlags.DontReportSyncStage));
			this.SaveSyncState();
			this.RetryCount = 0;
			if (flag)
			{
				throw new RelinquishJobLongRunTransientException();
			}
			if (this.SyncStage != SyncStage.SyncFinished && (flags & SaveStateFlags.DontSaveRequestJob) == SaveStateFlags.Regular)
			{
				MrsTracer.Service.Debug("Writing updated move job state to job queue.", new object[0]);
				try
				{
					this.SaveRequest(true, delegate(TransactionalRequestJob rj)
					{
						rj.Status = RequestStatus.InProgress;
						rj.MRSServerName = CommonUtils.LocalComputerName;
						rj.RequestJobState = JobProcessingState.InProgress;
						rj.SyncStage = this.SyncStage;
						rj.PercentComplete = this.OverallProgress;
						rj.TimeTracker.SetTimestamp(RequestJobTimestamp.LastProgressCheckpoint, new DateTime?(this.GetTimeAtWhichLastProgressWasMade()));
						if (updateRequestDel != null)
						{
							updateRequestDel(rj);
						}
						this.UpdateRequestOnSave(rj, UpdateRequestOnSaveType.AllUpdates);
					});
				}
				catch (LocalizedException ex)
				{
					LocalizedString localizedString = CommonUtils.FullExceptionMessage(ex);
					this.Report.Append(MrsStrings.ReportRequestSaveFailed2(CommonUtils.GetFailureType(ex)), ex, ReportEntryFlags.Cleanup);
					MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_RequestSaveFailed, new object[]
					{
						this.GetRequestKeyGuid().ToString(),
						this.RequestJobGuid.ToString(),
						this.RequestJobStoringMDB.ToString(),
						localizedString
					});
					throw;
				}
			}
			this.FlushReport(null);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00021ED4 File Offset: 0x000200D4
		public override IActivityScope GetCurrentActivityScope()
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			currentActivityScope.ClientInfo = CommonUtils.LocalComputerName;
			currentActivityScope.Action = base.GetType().Name;
			if (this.CachedRequestJob != null)
			{
				if (this.CachedRequestJob.User != null)
				{
					currentActivityScope.UserId = "Identity:" + this.CachedRequestJob.User.Identity;
				}
				else if (this.CachedRequestJob.SourceUser != null)
				{
					currentActivityScope.UserId = "Identity:" + this.CachedRequestJob.SourceUser.Identity;
				}
				else if (this.CachedRequestJob.TargetUser != null)
				{
					currentActivityScope.UserId = "Identity:" + this.CachedRequestJob.TargetUser.Identity;
				}
			}
			return currentActivityScope;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00021F9C File Offset: 0x0002019C
		protected void ExamineRequest(RequestJobBase request)
		{
			if (request == null)
			{
				MrsTracer.Service.Warning("Request is corrupt or missing, relinquishing job.", new object[0]);
				throw new RelinquishJobGenericTransientException();
			}
			if (request.RequestGuid != this.CachedRequestJob.RequestGuid)
			{
				MrsTracer.Service.Warning("RequestGuid does not match (overwritten???), abandoning job without cleanup.", new object[0]);
				throw new RelinquishJobGenericTransientException();
			}
			if (request.ValidationResult == null)
			{
				MrsTracer.Service.Debug("Request was not yet validated.", new object[0]);
			}
			else if (request.ValidationResult != RequestJobBase.ValidationResultEnum.Valid)
			{
				MrsTracer.Service.Warning("Request did not validate: {0}, abandoning move without cleanup", new object[]
				{
					request.ValidationMessage.ToString()
				});
				throw new RelinquishJobGenericTransientException();
			}
			if (string.IsNullOrEmpty(request.MRSServerName) || StringComparer.OrdinalIgnoreCase.Compare(CommonUtils.LocalComputerName, request.MRSServerName) != 0)
			{
				this.Report.Append(MrsStrings.ReportRequestProcessedByAnotherMRS(request.MRSServerName));
				throw new RelinquishJobGenericTransientException();
			}
			if (request.CancelRequest && this.CanBeCanceledOrSuspended())
			{
				this.TimeTracker.CurrentState = RequestState.Canceled;
				DateTime dateTime = request.RequestCanceledTimestamp + this.GetConfig<TimeSpan>("CanceledRequestAge");
				if (DateTime.UtcNow < dateTime)
				{
					throw new RelinquishCancelPostponedTransientException(dateTime);
				}
				this.Report.Append(MrsStrings.ReportRequestCanceled);
				this.CleanupCanceledJob();
				throw new JobCanceledPermanentException();
			}
			else
			{
				if (request.Status == RequestStatus.None)
				{
					MrsTracer.Service.Warning("Request did not validate: mailbox is not being moved, abandoning job without cleanup", new object[0]);
					throw new RelinquishJobGenericTransientException();
				}
				if ((request.RehomeRequest || (request.RequestQueue != null && !request.RequestQueue.Equals(request.OptimalRequestQueue))) && this.CachedRequestJob.RequestType != MRSRequestType.Move && this.CachedRequestJob.RequestType != MRSRequestType.MailboxRelocation)
				{
					throw new RelinquishJobRehomeTransientException();
				}
				if (request.Suspend && request.Status != RequestStatus.AutoSuspended && request.Status != RequestStatus.Suspended && request.Status != RequestStatus.Failed && this.CanBeCanceledOrSuspended())
				{
					this.Report.Append(MrsStrings.ReportSuspendingJob);
					throw new RelinquishJobSuspendedTransientException();
				}
				if (request.Suspend && (request.Status == RequestStatus.AutoSuspended || request.Status == RequestStatus.Suspended || request.Status == RequestStatus.Failed) && this.CanBeCanceledOrSuspended())
				{
					throw new RelinquishJobGenericTransientException();
				}
				if (!request.ShouldProcessJob())
				{
					this.Report.Append(MrsStrings.ReportJobProcessingDisabled);
					throw new RelinquishJobGenericTransientException();
				}
				this.UpdateCachedRequestJob(request);
				return;
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0002221F File Offset: 0x0002041F
		protected virtual void UpdateCachedRequestJob(RequestJobBase request)
		{
			if (request is TransactionalRequestJob)
			{
				this.CachedRequestJob = RequestJobProvider.CreateRequestStatistics((TransactionalRequestJob)request);
				return;
			}
			this.CachedRequestJob = (RequestStatisticsBase)request;
			this.MergeTimestamps(this.CachedRequestJob.TimeTracker);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000226A4 File Offset: 0x000208A4
		protected virtual bool ResetAfterFailure(out bool relinquishJobNow)
		{
			Exception failure = this.LastFailure;
			ExceptionSide? failureSide = ExecutionContext.GetExceptionSide(failure);
			if (CommonUtils.ExceptionIs(failure, new WellKnownException[]
			{
				WellKnownException.ResourceReservation
			}))
			{
				failure = new RelinquishJobResourceReservationTransientException(CommonUtils.FullExceptionMessage(failure), failure);
				this.LastFailure = failure;
			}
			relinquishJobNow = false;
			if (failure is JobCanceledPermanentException)
			{
				relinquishJobNow = true;
			}
			if (failure is MRSProxyConnectionLimitReachedTransientException)
			{
				relinquishJobNow = true;
			}
			if (failure is RelinquishJobTransientException || failure is ServiceIsStoppingPermanentException)
			{
				if (this.SyncStage != SyncStage.None)
				{
					bool flag = true;
					if (failure is RelinquishJobDatabaseFailoverTransientException || failure is RelinquishJobMailboxLockoutTransientException || failure is RelinquishJobResourceReservationTransientException || failure is RelinquishJobServerBusyTransientException)
					{
						flag = false;
					}
					if (flag)
					{
						foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
						{
							if (!mailboxCopierBase.IsSourceConnected || !mailboxCopierBase.IsDestinationConnected)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						CommonUtils.CatchKnownExceptions(delegate
						{
							this.SaveState(SaveStateFlags.DontSaveRequestJob, null);
						}, delegate(Exception saveFailure)
						{
							LocalizedString localizedString = CommonUtils.FullExceptionMessage(saveFailure);
							this.Report.Append(MrsStrings.ReportSyncStateSaveFailed2(CommonUtils.GetFailureType(saveFailure)), saveFailure, ReportEntryFlags.Cleanup);
							MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_SyncStateSaveFailed, new object[]
							{
								this.RequestJobIdentity,
								this.GetRequestKeyGuid().ToString(),
								this.RequestJobStoringMDB.ToString(),
								localizedString
							});
						});
					}
				}
				relinquishJobNow = true;
			}
			if (relinquishJobNow)
			{
				FailureLog.Write(this.RequestJobGuid, failure, false, this.TimeTracker.CurrentState, this.SyncStage, null, null);
				return true;
			}
			bool exceptionIsFatal = !CommonUtils.IsTransientException(failure);
			OperationType operationType = ExecutionContext.GetOperationType(failure);
			DateTime utcNow = DateTime.UtcNow;
			if (!exceptionIsFatal && operationType == OperationType.Connect && !CommonUtils.ExceptionIsAny(failure, new WellKnownException[]
			{
				WellKnownException.UnhealthyResource,
				WellKnownException.MRSProxyLimitReached
			}))
			{
				RequestJobTimestamp ts = (failureSide == ExceptionSide.Source) ? RequestJobTimestamp.SourceConnectionFailure : RequestJobTimestamp.TargetConnectionFailure;
				DateTime? timestamp = this.TimeTracker.GetTimestamp(ts);
				if (timestamp == null)
				{
					timestamp = new DateTime?(utcNow);
					this.TimeTracker.SetTimestamp(ts, new DateTime?(utcNow));
				}
				TimeSpan config = this.GetConfig<TimeSpan>("ReconnectAbandonInterval");
				if (config != TimeSpan.Zero && timestamp.Value < utcNow - config)
				{
					if (failureSide == ExceptionSide.Source)
					{
						failure = new SourceMailboxConnectionStalePermanentException(failure);
					}
					else
					{
						failure = new TargetMailboxConnectionStalePermanentException(failure);
					}
					exceptionIsFatal = true;
				}
			}
			if (!exceptionIsFatal && (this.TestIntegration.DisableRetriesOnTransientFailures || ++this.RetryCount > this.GetConfig<int>("MaxRetries") || (double)(++this.TotalRetryCount) > 5.0 / this.GetConfig<TimeSpan>("RetryDelay").TotalHours))
			{
				this.Report.Append(MrsStrings.ReportTooManyTransientFailures(this.TotalRetryCount));
				exceptionIsFatal = true;
			}
			int errorCode = CommonUtils.HrFromException(failure);
			LocalizedString failureMsg = CommonUtils.FullExceptionMessage(failure);
			string dataContext = ExecutionContext.GetDataContext(failure);
			if (CommonUtils.ExceptionIs(failure, new WellKnownException[]
			{
				WellKnownException.MapiMdbOffline
			}))
			{
				this.TimeTracker.CurrentState = (exceptionIsFatal ? RequestState.FailedOther : RequestState.MDBOffline);
			}
			else if (CommonUtils.ExceptionIs(failure, new WellKnownException[]
			{
				WellKnownException.MapiNetworkError
			}))
			{
				this.TimeTracker.CurrentState = (exceptionIsFatal ? RequestState.FailedNetwork : RequestState.NetworkFailure);
			}
			else if (CommonUtils.ExceptionIs(failure, new WellKnownException[]
			{
				WellKnownException.Mapi
			}))
			{
				this.TimeTracker.CurrentState = (exceptionIsFatal ? RequestState.FailedMAPI : RequestState.TransientFailure);
			}
			else if (CommonUtils.ExceptionIs(failure, new WellKnownException[]
			{
				WellKnownException.UnhealthyResource
			}))
			{
				this.TimeTracker.CurrentState = ((this.CachedRequestJob.Direction == RequestDirection.Pull) ? RequestState.StalledDueToReadUnknown : RequestState.StalledDueToWriteUnknown);
				this.lastProxyThrottledReason = failure.Message;
				this.throttleStartTimestamp = ExDateTime.UtcNow;
			}
			else if (failure is JobStalledPermanentException)
			{
				switch (((JobStalledPermanentException)failure).AgentId)
				{
				case 1:
					this.TimeTracker.CurrentState = RequestState.FailedStallDueToHA;
					break;
				case 2:
					this.TimeTracker.CurrentState = RequestState.FailedStallDueToCI;
					break;
				default:
					this.TimeTracker.CurrentState = RequestState.FailedOther;
					break;
				}
			}
			else if (failure is JobStuckPermanentException)
			{
				this.TimeTracker.CurrentState = RequestState.FailedStuck;
			}
			else
			{
				this.TimeTracker.CurrentState = (exceptionIsFatal ? RequestState.FailedOther : RequestState.TransientFailure);
			}
			if (exceptionIsFatal)
			{
				this.Report.Append(MrsStrings.ReportFatalException(CommonUtils.GetFailureType(failure)), failure, ReportEntryFlags.Fatal);
				this.TimeTracker.SetTimestamp(RequestJobTimestamp.Failure, new DateTime?(utcNow));
			}
			else
			{
				LocalizedString msg = MrsStrings.ReportTransientException(CommonUtils.GetFailureType(failure), this.RetryCount, this.GetConfig<int>("MaxRetries"));
				this.Report.Append(msg, failure, ReportEntryFlags.None);
			}
			this.RecordFailure(failure, exceptionIsFatal);
			lock (this.syncRoot)
			{
				CommonUtils.CatchKnownExceptions(delegate
				{
					using (RequestJobProvider rjProvider = new RequestJobProvider(this.RequestJobStoringMDB.ObjectGuid))
					{
						MapiUtils.RetryOnObjectChanged(delegate
						{
							using (TransactionalRequestJob transactionalRequestJob = (TransactionalRequestJob)rjProvider.Read<TransactionalRequestJob>(this.GetRequestJobObjectId()))
							{
								if (transactionalRequestJob != null && transactionalRequestJob.ValidationResult == RequestJobBase.ValidationResultEnum.Valid && transactionalRequestJob.Status != RequestStatus.None)
								{
									if (exceptionIsFatal)
									{
										if (transactionalRequestJob.CancelRequest)
										{
											rjProvider.Delete(transactionalRequestJob);
											CommonUtils.CatchKnownExceptions(delegate
											{
												this.Report.Delete(rjProvider.SystemMailbox);
											}, null);
											transactionalRequestJob.RemoveAsyncNotification();
											return;
										}
										transactionalRequestJob.Suspend = true;
										transactionalRequestJob.Status = RequestStatus.Failed;
										transactionalRequestJob.FailureCode = new int?(errorCode);
										transactionalRequestJob.FailureType = CommonUtils.GetFailureType(failure);
										transactionalRequestJob.FailureSide = failureSide;
										transactionalRequestJob.Message = MrsStrings.MoveRequestMessageError(failureMsg);
									}
									this.UpdateRequestAfterFailure(transactionalRequestJob, failure);
									this.TryUpdateJobFromSyncState(transactionalRequestJob);
									transactionalRequestJob.SyncStage = this.SyncStage;
									transactionalRequestJob.PercentComplete = this.OverallProgress;
									transactionalRequestJob.ProgressTracker = this.ProgressTracker;
									transactionalRequestJob.TotalRetryCount = this.TotalRetryCount;
									transactionalRequestJob.RetryCount = this.RetryCount;
									this.MergeTimestamps(transactionalRequestJob.TimeTracker);
									transactionalRequestJob.TimeTracker = this.TimeTracker;
									transactionalRequestJob.SkippedItemCounts = this.SkippedItemCounts;
									transactionalRequestJob.FailureHistory = this.FailureHistory;
									this.Report.Append(this.TimeTracker, ReportEntryFlags.TargetThrottleDurations | ReportEntryFlags.SourceThrottleDurations);
									rjProvider.Save(transactionalRequestJob);
									this.FlushReport(rjProvider.SystemMailbox);
									transactionalRequestJob.UpdateAsyncNotification(this.Report);
									if (exceptionIsFatal)
									{
										RequestJobLog.Write(transactionalRequestJob);
									}
								}
							}
						});
					}
				}, delegate(Exception saveFailure)
				{
					LocalizedString localizedString = CommonUtils.FullExceptionMessage(saveFailure);
					this.Report.Append(MrsStrings.ReportRequestSaveFailed2(CommonUtils.GetFailureType(saveFailure)), saveFailure, ReportEntryFlags.Cleanup);
					MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_RequestSaveFailed, new object[]
					{
						this.GetRequestKeyGuid().ToString(),
						this.RequestJobGuid.ToString(),
						this.RequestJobStoringMDB.ToString(),
						localizedString
					});
				});
			}
			if (this.RelinquishAfterOfflineMoveFailure)
			{
				this.LastFailure = new RelinquishJobOfflineTransientException(utcNow + ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("OfflineMoveTransientFailureRelinquishPeriod"));
				relinquishJobNow = true;
				return true;
			}
			this.CleanupAfterFailure();
			FailureLog.Write(this.RequestJobGuid, failure, exceptionIsFatal, this.TimeTracker.CurrentState, this.SyncStage, null, null);
			if (exceptionIsFatal)
			{
				MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_RequestFatalFailure, new object[]
				{
					this.RequestJobIdentity,
					this.GetRequestKeyGuid().ToString(),
					errorCode,
					failureMsg,
					dataContext
				});
				return false;
			}
			MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_RequestTransientFailure, new object[]
			{
				this.RequestJobIdentity,
				this.GetRequestKeyGuid().ToString(),
				this.RetryCount,
				this.GetConfig<int>("MaxRetries"),
				errorCode,
				failureMsg,
				dataContext
			});
			MrsTracer.Service.Warning("Retrying on non-fatal failure ({0}/{1}).", new object[]
			{
				this.RetryCount,
				this.GetConfig<int>("MaxRetries")
			});
			return true;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00022E8C File Offset: 0x0002108C
		protected override void ProcessSucceededTask(bool ignoreTaskSuccessfulExecutionTime)
		{
			if (!ignoreTaskSuccessfulExecutionTime)
			{
				this.TimeTracker.SetTimestamp(RequestJobTimestamp.LastUpdate, new DateTime?(DateTime.UtcNow));
				this.lastTaskExecutionTime = ExDateTime.UtcNow;
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00022EB4 File Offset: 0x000210B4
		protected override void ProcessFailedTask(Exception lastFailure, out bool shouldContinueRunningJob)
		{
			this.lastTaskExecutionTime = ExDateTime.UtcNow;
			shouldContinueRunningJob = false;
			this.LastFailure = lastFailure;
			MrsTracer.Service.Error("WorkItem failed:\n{0}", new object[]
			{
				CommonUtils.FullExceptionMessage(this.LastFailure)
			});
			bool flag;
			if (!this.ResetAfterFailure(out flag))
			{
				return;
			}
			if (flag)
			{
				if (!MoveJob.CacheJobQueues)
				{
					return;
				}
				if (this.LastFailure is JobCanceledPermanentException || this.LastFailure is RelinquishJobGenericTransientException || this.LastFailure is RelinquishJobRehomeTransientException || this.LastFailure is RelinquishJobSuspendedTransientException || this.LastFailure is RelinquishJobDatabaseFailoverTransientException || this.LastFailure is ServiceIsStoppingPermanentException)
				{
					return;
				}
				this.DisposeReservations();
				this.UpdateReportRelinquished(false);
			}
			MrsTracer.Service.Debug("Job({0}) will retry.", new object[]
			{
				base.GetType().Name
			});
			this.Disconnect();
			base.ResetWorkItemQueue();
			this.ScheduleBeginJob();
			shouldContinueRunningJob = true;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00022FAC File Offset: 0x000211AC
		protected void StartDataGuaranteeWait()
		{
			this.DataGuaranteeCommitTimestamp = DateTime.UtcNow;
			this.DataGuaranteeLastMessageTimestamp = DateTime.MinValue;
			this.DataGuaranteeWaitLogRolled = false;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00022FCB File Offset: 0x000211CB
		protected void ResetDataGuaranteeWait()
		{
			this.DataGuaranteeCommitTimestamp = DateTime.MinValue;
			this.DataGuaranteeLastMessageTimestamp = DateTime.MinValue;
			this.DataGuaranteeWaitLogRolled = false;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00023004 File Offset: 0x00021204
		protected bool IsDataGuaranteeSatisfied(bool relinquishOnTimeout)
		{
			TimeSpan timeSpan = this.GetConfig<TimeSpan>("DataGuaranteeTimeout");
			int intValue = this.TestIntegration.GetIntValue("DataGuaranteeTimeoutOverrideSecs", 0, 0, 7200);
			if (intValue != 0)
			{
				timeSpan = TimeSpan.FromSeconds((double)intValue);
				MrsTracer.Service.Debug("DataGuaranteeTimeout is overridden in the registry. Using value {0}", new object[]
				{
					timeSpan
				});
			}
			if (timeSpan == TimeSpan.Zero)
			{
				MrsTracer.Service.Warning("DataGuarantee wait is disabled.", new object[0]);
				this.ResetDataGuaranteeWait();
				return true;
			}
			MrsTracer.Service.Debug("Checking DataGuarantee, commit timestamp {0}", new object[]
			{
				this.DataGuaranteeCommitTimestamp
			});
			LocalizedString failureReason;
			bool flag = this.CheckDataGuarantee(this.DataGuaranteeCommitTimestamp, out failureReason);
			bool flag2 = false;
			try
			{
				this.VerifyConnectionsAreAlive();
				flag2 = true;
			}
			finally
			{
				if (!flag2)
				{
					this.ResetDataGuaranteeWait();
				}
			}
			if (flag)
			{
				MrsTracer.Service.Debug("Mailbox changes have replicated around.", new object[0]);
				this.ResetDataGuaranteeWait();
				return true;
			}
			MrsTracer.Service.Warning("Mailbox changes have not yet replicated around", new object[0]);
			DateTime utcNow = DateTime.UtcNow;
			TimeSpan t = utcNow - this.DataGuaranteeCommitTimestamp;
			if (!(t >= timeSpan))
			{
				this.TimeTracker.CurrentState = RequestState.DataReplicationWait;
				if (utcNow - this.DataGuaranteeLastMessageTimestamp > TimeSpan.FromMinutes(1.0))
				{
					this.Report.Append(MrsStrings.ReportWaitingForMailboxDataReplication);
					this.FlushReport(null);
					this.DataGuaranteeLastMessageTimestamp = utcNow;
				}
				if (!this.DataGuaranteeWaitLogRolled && t >= this.GetConfig<TimeSpan>("DataGuaranteeLogRollDelay") && this.GetConfig<TimeSpan>("DataGuaranteeLogRollDelay") != TimeSpan.Zero)
				{
					MrsTracer.Service.Debug("Forcing log roll in target MDB", new object[0]);
					using (List<MailboxCopierBase>.Enumerator enumerator = this.GetAllCopiers().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							MailboxCopierBase mbxCtx = enumerator.Current;
							CommonUtils.CatchKnownExceptions(delegate
							{
								mbxCtx.DestMailbox.ForceLogRoll();
							}, null);
						}
					}
					this.DataGuaranteeWaitLogRolled = true;
				}
				return false;
			}
			DateTime dataGuaranteeCommitTimestamp = this.DataGuaranteeCommitTimestamp;
			this.ResetDataGuaranteeWait();
			Exception ex = new MailboxDataReplicationFailedPermanentException(failureReason);
			if (t >= this.GetConfig<TimeSpan>("DataGuaranteeMaxWait") || !relinquishOnTimeout)
			{
				this.TimeTracker.SetTimestamp(RequestJobTimestamp.FailedDataGuarantee, null);
				throw ex;
			}
			this.TimeTracker.SetTimestamp(RequestJobTimestamp.FailedDataGuarantee, new DateTime?(dataGuaranteeCommitTimestamp));
			DateTime pickupTime = DateTime.UtcNow + this.GetConfig<TimeSpan>("DataGuaranteeRetryInterval");
			throw new RelinquishJobDGTimeoutTransientException(pickupTime, ex);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000232CC File Offset: 0x000214CC
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.TimeTracker.CurrentState = RequestState.None;
			}
			this.DisposeReservations();
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000232EA File Offset: 0x000214EA
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<BaseJob>(this);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000232F2 File Offset: 0x000214F2
		protected virtual void UpdateRequestAfterFailure(RequestJobBase rj, Exception failure)
		{
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000232F4 File Offset: 0x000214F4
		protected virtual void UpdateRequestOnSave(TransactionalRequestJob rj, UpdateRequestOnSaveType updateType)
		{
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000232F6 File Offset: 0x000214F6
		protected virtual void CleanupAfterFailure()
		{
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000232F8 File Offset: 0x000214F8
		protected virtual void SaveSyncState()
		{
			foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
			{
				mailboxCopierBase.SaveSyncState();
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000233B0 File Offset: 0x000215B0
		protected virtual void CleanupCanceledJob()
		{
			CommonUtils.CatchKnownExceptions(delegate
			{
				foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
				{
					if (!mailboxCopierBase.IsDestinationConnected)
					{
						mailboxCopierBase.ConnectDestinationMailbox(MailboxConnectFlags.None);
					}
					mailboxCopierBase.ClearSyncState(SyncStateClearReason.JobCanceled);
				}
			}, null);
			MoveHistoryEntryInternal moveHistoryEntryInternal;
			this.RemoveRequest(false, out moveHistoryEntryInternal);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000233D8 File Offset: 0x000215D8
		protected virtual void BeginJob()
		{
			MrsTracer.Service.Debug("WorkItem: begin job.", new object[0]);
			ConnectivityRec connectivityRec = new ConnectivityRec(ServerKind.MRS, VersionInformation.MRS);
			this.Report.Append(MrsStrings.ReportInitializingJob(CommonUtils.LocalComputerName, VersionInformation.MRS.ToString()), connectivityRec);
			int config = this.GetConfig<int>("PoisonLimit");
			if (config > 0 && this.CachedRequestJob.PoisonCount > config)
			{
				if (!this.CachedRequestJob.CancelRequest)
				{
					MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_RequestIsPoisoned, new object[]
					{
						this.RequestJobIdentity,
						this.GetRequestKeyGuid().ToString(),
						(this.CachedRequestJob.RequestQueue != null) ? this.CachedRequestJob.RequestQueue.ToString() : "null",
						this.CachedRequestJob.PoisonCount
					});
				}
				throw new JobIsPoisonedPermananentException(this.CachedRequestJob.PoisonCount);
			}
			if (this.TestIntegration.InjectCrash)
			{
				MrsTracer.Service.Error("==== INJECTING MRS CRASH due to TestIntegration regkey ====", new object[0]);
				throw new ApplicationException("Injected crash due to TestIntegration regkey");
			}
			this.VerifyRequestIsOnThisMachine();
			base.ScheduleWorkItem(new JobCheck(BaseJob.RunUnthrottledInterval, new Action(this.DoPeriodicChecks)));
			base.ScheduleWorkItem<bool>(new Action<bool>(this.ConfigureProviders), true, WorkloadType.Unknown);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00023538 File Offset: 0x00021738
		protected virtual void ConfigureProviders(bool continueAfterConfiguringProviders)
		{
			this.ConfigureMailboxProviders();
			if (!continueAfterConfiguringProviders)
			{
				return;
			}
			this.CheckRequestIsValid();
			this.TestIntegration.Barrier("BreakpointBeforeConnect", new Action(this.RefreshRequestIfNeeded));
			bool cacheJobQueues = MoveJob.CacheJobQueues;
			if (this.Reservation == null)
			{
				base.ScheduleWorkItem(new Action(this.ObtainReservations), WorkloadType.Unknown);
			}
			base.ScheduleWorkItem(new Action(this.MakeConnections), WorkloadType.Unknown);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000235A6 File Offset: 0x000217A6
		protected virtual void MakeConnections()
		{
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000235A8 File Offset: 0x000217A8
		public override void PerformCrashingFailureActions(Exception exception)
		{
			BaseJob.PerformCrashingFailureActions(this.GetRequestKeyGuid(), this.RequestJobGuid, exception, this.TimeTracker.CurrentState, this.SyncStage);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00023878 File Offset: 0x00021A78
		protected void RemoveRequest(bool createMoveHistoryEntry, out MoveHistoryEntryInternal mhei)
		{
			base.CheckDisposed();
			bool flag = false;
			try
			{
				object obj;
				Monitor.Enter(obj = this.syncRoot, ref flag);
				mhei = null;
				MoveHistoryEntryInternal mheiInt = null;
				MrsTracer.Service.Debug("Deleting job from the job queue.", new object[0]);
				CommonUtils.CatchKnownExceptions(delegate
				{
					using (RequestJobProvider rjProvider = new RequestJobProvider(this.RequestJobStoringMDB.ObjectGuid))
					{
						MapiUtils.RetryOnObjectChanged(delegate
						{
							using (TransactionalRequestJob transactionalRequestJob = (TransactionalRequestJob)rjProvider.Read<TransactionalRequestJob>(this.GetRequestJobObjectId()))
							{
								if (transactionalRequestJob != null)
								{
									if (transactionalRequestJob.Status != RequestStatus.Failed)
									{
										transactionalRequestJob.FailureCode = new int?(-2147220223);
										transactionalRequestJob.FailureType = "JobCanceled";
										transactionalRequestJob.FailureSide = null;
										transactionalRequestJob.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.JobHasBeenCanceled);
									}
									this.TimeTracker.SetTimestamp(RequestJobTimestamp.Completion, new DateTime?(DateTime.UtcNow));
									this.TimeTracker.AddDurationToState(this.SuspendTime, RequestState.Idle);
									this.MergeTimestamps(transactionalRequestJob.TimeTracker);
									transactionalRequestJob.TimeTracker = this.TimeTracker;
									if (createMoveHistoryEntry)
									{
										this.Report.Load(rjProvider.SystemMailbox);
										mheiInt = new MoveHistoryEntryInternal(transactionalRequestJob, this.Report);
									}
									rjProvider.Delete(transactionalRequestJob);
									CommonUtils.CatchKnownExceptions(delegate
									{
										this.Report.Delete(rjProvider.SystemMailbox);
									}, null);
									RequestJobLog.Write(transactionalRequestJob);
								}
							}
						});
					}
				}, delegate(Exception failure)
				{
					LocalizedString localizedString = CommonUtils.FullExceptionMessage(failure);
					MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_FailedToCleanupCanceledRequest, new object[]
					{
						this.RequestJobIdentity,
						this.GetRequestKeyGuid().ToString(),
						this.RequestJobStoringMDB.ToString(),
						localizedString
					});
				});
				mhei = mheiInt;
				this.needToRelinquishOnJobCompletion = false;
				MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_RequestCanceled, new object[]
				{
					this.RequestJobIdentity,
					this.GetRequestKeyGuid().ToString()
				});
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00023E9C File Offset: 0x0002209C
		protected void CompleteRequest(bool createMoveHistoryEntry, out MoveHistoryEntryInternal mhei)
		{
			base.CheckDisposed();
			bool flag = false;
			try
			{
				object obj;
				Monitor.Enter(obj = this.syncRoot, ref flag);
				BaseJob.<>c__DisplayClass3a CS$<>8__locals2 = new BaseJob.<>c__DisplayClass3a();
				mhei = null;
				CS$<>8__locals2.mheiInt = null;
				CS$<>8__locals2.warningsString = null;
				MrsTracer.Service.Debug("Job finished. Saving job.", new object[0]);
				using (RequestJobProvider rjProvider = new RequestJobProvider(this.RequestJobStoringMDB.ObjectGuid))
				{
					MapiUtils.RetryOnObjectChanged(delegate
					{
						using (TransactionalRequestJob rj = (TransactionalRequestJob)rjProvider.Read<TransactionalRequestJob>(this.GetRequestJobObjectId()))
						{
							if (rj != null)
							{
								this.TimeTracker.SetTimestamp(RequestJobTimestamp.Completion, new DateTime?(DateTime.UtcNow));
								this.TimeTracker.AddDurationToState(this.SuspendTime, RequestState.Idle);
								this.MergeTimestamps(rj.TimeTracker);
								rj.TimeTracker = this.TimeTracker;
								this.Report.Append(this.TimeTracker, ReportEntryFlags.TargetThrottleDurations | ReportEntryFlags.SourceThrottleDurations);
								rj.Message = LocalizedString.Empty;
								rj.FailureCode = null;
								rj.FailureType = null;
								rj.FailureSide = null;
								this.TimeTracker.SetTimestamp(RequestJobTimestamp.Failure, null);
								rj.ProgressTracker = this.ProgressTracker;
								rj.SkippedItemCounts = this.SkippedItemCounts;
								rj.FailureHistory = this.FailureHistory;
								this.TryUpdateJobFromSyncState(rj);
								rj.PercentComplete = 100;
								rj.SyncStage = SyncStage.SyncFinished;
								rj.PoisonCount = 0;
								if (this.Warnings.Count == 0)
								{
									rj.Status = RequestStatus.Completed;
									rj.TimeTracker.CurrentState = RequestState.Completed;
								}
								else
								{
									rj.Status = RequestStatus.CompletedWithWarning;
									rj.TimeTracker.CurrentState = RequestState.CompletedWithWarnings;
									LocalizedString localizedString = MrsStrings.MoveRequestMessageWarning(this.Warnings[0]);
									for (int i = 1; i < this.Warnings.Count; i++)
									{
										localizedString = MrsStrings.MoveRequestMessageWarningSeparator(localizedString, this.Warnings[i]);
									}
									rj.Message = localizedString;
									CS$<>8__locals2.warningsString = localizedString.ToString();
								}
								this.Report.Flush(rjProvider.SystemMailbox);
								if (createMoveHistoryEntry)
								{
									ReportData reportData = new ReportData(this.GetRequestKeyGuid(), rj.ReportVersion);
									reportData.Load(rjProvider.SystemMailbox);
									CS$<>8__locals2.mheiInt = new MoveHistoryEntryInternal(rj, reportData);
								}
								bool flag2 = rj.CancelRequest;
								if (rj.Status == RequestStatus.Completed)
								{
									DateTime dateTime = DateTime.UtcNow;
									if (rj.CompletedRequestAgeLimit.IsUnlimited)
									{
										dateTime = DateTime.MaxValue;
									}
									else if (rj.CompletedRequestAgeLimit.Value <= EnhancedTimeSpan.Zero)
									{
										flag2 = true;
									}
									else
									{
										dateTime += rj.CompletedRequestAgeLimit.Value;
									}
									rj.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, new DateTime?(dateTime));
								}
								if (flag2)
								{
									rjProvider.Delete(rj);
									CommonUtils.CatchKnownExceptions(delegate
									{
										this.Report.Delete(rjProvider.SystemMailbox);
									}, null);
									CommonUtils.CatchKnownExceptions(delegate
									{
										rjProvider.DeleteIndexEntries(rj);
									}, null);
									rj.RemoveAsyncNotification();
								}
								else
								{
									rjProvider.Save(rj);
									rj.UpdateAsyncNotification(this.Report);
								}
								RequestJobLog.Write(rj);
							}
						}
					});
				}
				mhei = CS$<>8__locals2.mheiInt;
				this.needToRelinquishOnJobCompletion = false;
				if (CS$<>8__locals2.warningsString != null)
				{
					MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_RequestCompletedWithWarnings, new object[]
					{
						this.RequestJobIdentity,
						this.GetRequestKeyGuid().ToString(),
						CS$<>8__locals2.warningsString
					});
				}
				else
				{
					MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_RequestCompleted, new object[]
					{
						this.RequestJobIdentity,
						this.GetRequestKeyGuid().ToString()
					});
				}
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00024030 File Offset: 0x00022230
		protected void RefreshRequestIfNeeded()
		{
			CommonUtils.CheckForServiceStopping();
			this.CheckForStuckJob();
			this.VerifyTargetDatabaseState();
			if (this.NeedToRefreshRequest)
			{
				this.CheckRequestIsValid();
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00024054 File Offset: 0x00022254
		protected void CheckRequestIsValid()
		{
			this.needToRefreshRequest = false;
			if (this.AllowInvalidRequest)
			{
				return;
			}
			lock (this.syncRoot)
			{
				using (RequestJobProvider requestJobProvider = new RequestJobProvider(this.RequestJobStoringMDB.ObjectGuid))
				{
					RequestStatisticsBase request = (RequestStatisticsBase)requestJobProvider.Read<RequestStatisticsBase>(this.GetRequestJobObjectId());
					this.ExamineRequest(request);
				}
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000240E0 File Offset: 0x000222E0
		protected void CheckForStuckJob()
		{
			if (!TimeSpan.Zero.Equals(this.GetConfig<TimeSpan>("JobStuckDetectionTime")))
			{
				DateTime timeAtWhichLastProgressWasMade = this.GetTimeAtWhichLastProgressWasMade();
				bool flag = DateTime.UtcNow - timeAtWhichLastProgressWasMade > this.GetConfig<TimeSpan>("JobStuckDetectionTime");
				bool flag2 = ExDateTime.UtcNow - this.GetConfig<TimeSpan>("JobStuckDetectionTime") > this.jobPickupTimestamp + this.GetConfig<TimeSpan>("JobStuckDetectionWarmupTime");
				if (flag2 && flag)
				{
					MrsTracer.Service.Debug("Job is stuck. Throwing Failure. Job: {0}", new object[]
					{
						this.RequestJobIdentity
					});
					throw new JobStuckPermanentException(timeAtWhichLastProgressWasMade.ToLocalTime(), ((DateTime)this.jobPickupTimestamp).ToLocalTime());
				}
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000241A8 File Offset: 0x000223A8
		protected void AppendReportEntries(ReportEntry[] entries)
		{
			if (entries != null)
			{
				foreach (ReportEntry reportEntry in entries)
				{
					this.Report.Append(reportEntry);
					if (reportEntry.Type == ReportEntryType.WarningCondition)
					{
						this.Warnings.Add(reportEntry.Message);
						FailureLog.Write(this.RequestJobGuid, new MailboxReplicationTransientException(reportEntry.Message), false, RequestState.Cleanup, SyncStage.CleanupUpdateMovedMailboxWarning, (reportEntry.BadItem != null) ? reportEntry.BadItem.FolderName : null, (reportEntry.Failure != null) ? reportEntry.Failure.DataContext : null);
					}
				}
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0002423F File Offset: 0x0002243F
		protected void ReportProgress(bool reportSyncStage = true)
		{
			this.ReportProgress(null, reportSyncStage);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002424C File Offset: 0x0002244C
		private void ReportProgress(MailboxCopierBase currentMailboxContext, bool reportSyncStage)
		{
			if (this.SyncStage >= SyncStage.Cleanup)
			{
				return;
			}
			if (reportSyncStage)
			{
				this.ReportSessionStatistics(MrsStrings.ReportProgress(this.SyncStage.ToString(), this.OverallProgress));
			}
			if (currentMailboxContext != null)
			{
				switch (this.SyncStage)
				{
				case SyncStage.CreatingFolderHierarchy:
					this.ReportSessionStatistics(MrsStrings.ReportFolderCreationProgress(currentMailboxContext.MailboxSizeTracker.FoldersProcessed, currentMailboxContext.TargetTracingID));
					break;
				case SyncStage.CreatingInitialSyncCheckpoint:
					this.ReportSessionStatistics(MrsStrings.ReportInitialSyncCheckpointCreationProgress(this.FoldersProcessed, this.TotalFolders, currentMailboxContext.TargetTracingID));
					break;
				}
			}
			if (this.SyncStage == SyncStage.CopyingMessages)
			{
				this.Report.Append(MrsStrings.ReportCopyProgress2(this.MessagesWritten, this.TotalMessages, new ByteQuantifiedSize(this.MessageSizeWritten).ToString(), new ByteQuantifiedSize(this.TotalMessageByteSize).ToString(), this.FoldersProcessed, this.TotalFolders));
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00024568 File Offset: 0x00022768
		protected void SaveRequest(bool examineRequest, BaseJob.UpdateRequestDelegate updateRequestDel)
		{
			if (this.AllowInvalidRequest)
			{
				return;
			}
			bool flag = false;
			try
			{
				object obj;
				Monitor.Enter(obj = this.syncRoot, ref flag);
				if (examineRequest)
				{
					this.needToRefreshRequest = false;
				}
				using (RequestJobProvider rjProvider = new RequestJobProvider(this.RequestJobStoringMDB.ObjectGuid))
				{
					MapiUtils.RetryOnObjectChanged(delegate
					{
						using (TransactionalRequestJob transactionalRequestJob = (TransactionalRequestJob)rjProvider.Read<TransactionalRequestJob>(this.GetRequestJobObjectId()))
						{
							ExDateTime utcNow = ExDateTime.UtcNow;
							if (examineRequest)
							{
								this.ExamineRequest(transactionalRequestJob);
							}
							if (transactionalRequestJob != null)
							{
								if (updateRequestDel != null)
								{
									updateRequestDel(transactionalRequestJob);
								}
								transactionalRequestJob.ProgressTracker = this.ProgressTracker;
								transactionalRequestJob.TotalRetryCount = this.TotalRetryCount;
								transactionalRequestJob.RetryCount = this.RetryCount;
								this.TryUpdateJobFromSyncState(transactionalRequestJob);
								this.MergeTimestamps(transactionalRequestJob.TimeTracker);
								transactionalRequestJob.TimeTracker = this.TimeTracker;
								transactionalRequestJob.SkippedItemCounts = this.SkippedItemCounts;
								transactionalRequestJob.FailureHistory = this.FailureHistory;
								this.Report.Append(this.TimeTracker, ReportEntryFlags.TargetThrottleDurations | ReportEntryFlags.SourceThrottleDurations);
								rjProvider.Save(transactionalRequestJob);
								this.lastRJTouchedTimestamp = utcNow;
								this.FlushReport(rjProvider.SystemMailbox);
								if (transactionalRequestJob.Status == RequestStatus.InProgress && this.lastInProgressRequestJobLogCheck + this.GetConfig<TimeSpan>("InProgressRequestJobLogInterval") < utcNow)
								{
									RequestJobLog.Write(transactionalRequestJob);
									this.lastInProgressRequestJobLogCheck = utcNow;
								}
								transactionalRequestJob.UpdateAsyncNotification(this.Report);
							}
						}
					});
				}
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00024690 File Offset: 0x00022890
		protected bool CheckDataGuarantee(DateTime commitTimestamp, out LocalizedString failureReason)
		{
			MrsTracer.Service.Debug("Checking DataGuarantee...", new object[0]);
			bool dataIsGuaranteed = true;
			failureReason = LocalizedString.Empty;
			using (List<MailboxCopierBase>.Enumerator enumerator = this.GetAllCopiers().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BaseJob.<>c__DisplayClass4e CS$<>8__locals2 = new BaseJob.<>c__DisplayClass4e();
					CS$<>8__locals2.mbxCtx = enumerator.Current;
					LocalizedString reason = LocalizedString.Empty;
					ExecutionContext.Create(new DataContext[]
					{
						new SimpleValueDataContext("Mailbox", CS$<>8__locals2.mbxCtx.SourceTracingID)
					}).Execute(delegate
					{
						ConstraintCheckResultType constraintCheckResultType = CS$<>8__locals2.mbxCtx.DestMailbox.CheckDataGuarantee(commitTimestamp, out reason);
						if (constraintCheckResultType != ConstraintCheckResultType.Satisfied)
						{
							dataIsGuaranteed = false;
						}
					});
					if (!dataIsGuaranteed)
					{
						failureReason = reason;
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0002479C File Offset: 0x0002299C
		protected void ReportSessionStatistics(LocalizedString msg)
		{
			this.ReportSessionStatistics(msg, TimeSpan.Zero, TimeSpan.Zero);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000249F8 File Offset: 0x00022BF8
		protected void ReportSessionStatistics(LocalizedString msg, TimeSpan preFinalSyncDataProcessingDuration, TimeSpan archivePreFinalSyncDataProcessingDuration)
		{
			SessionStatistics sessionStatistics = null;
			SessionStatistics archiveSessionStatistics = null;
			CommonUtils.CatchKnownExceptions(delegate
			{
				SessionStatistics sessionStatistics;
				foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
				{
					if (mailboxCopierBase.IsDestinationConnected && mailboxCopierBase.DestMailbox != null)
					{
						sessionStatistics = mailboxCopierBase.DestMailbox.GetSessionStatistics(SessionStatisticsFlags.ContentIndexingWordBreaking);
						if (sessionStatistics != null)
						{
							sessionStatistics.SessionId = mailboxCopierBase.DestMailbox.GetHashCode().ToString();
							sessionStatistics.SourceProviderInfo = mailboxCopierBase.SourceMailboxWrapper.ProviderInfo;
							sessionStatistics.DestinationProviderInfo = mailboxCopierBase.DestMailboxWrapper.ProviderInfo;
							sessionStatistics.SourceLatencyInfo = ((IMailbox)mailboxCopierBase.SourceMailboxWrapper).GetLatencyInfo();
							sessionStatistics.DestinationLatencyInfo = ((IMailbox)mailboxCopierBase.DestMailboxWrapper).GetLatencyInfo();
							if (mailboxCopierBase.Flags.HasFlag(MailboxCopierFlags.TargetIsArchive))
							{
								archiveSessionStatistics = sessionStatistics;
								archiveSessionStatistics.PreFinalSyncDataProcessingDuration = archivePreFinalSyncDataProcessingDuration;
							}
							else
							{
								sessionStatistics = sessionStatistics;
								sessionStatistics.PreFinalSyncDataProcessingDuration = preFinalSyncDataProcessingDuration;
							}
						}
					}
				}
				Comparison<DurationInfo> comparison = (DurationInfo x, DurationInfo y) => y.Duration.CompareTo(x.Duration);
				if (sessionStatistics != null)
				{
					sessionStatistics.SourceProviderInfo.Durations.Sort(comparison);
					sessionStatistics.DestinationProviderInfo.Durations.Sort(comparison);
					sessionStatistics = (sessionStatistics.Clone() as SessionStatistics);
				}
				if (archiveSessionStatistics != null)
				{
					archiveSessionStatistics.SourceProviderInfo.Durations.Sort(comparison);
					archiveSessionStatistics.DestinationProviderInfo.Durations.Sort(comparison);
					archiveSessionStatistics = (archiveSessionStatistics.Clone() as SessionStatistics);
				}
				this.Report.Append(msg, sessionStatistics, archiveSessionStatistics);
				SessionStatisticsLog.Write(this.RequestJobGuid, sessionStatistics, archiveSessionStatistics);
			}, null);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00024A48 File Offset: 0x00022C48
		protected DateTime GetTimeAtWhichLastProgressWasMade()
		{
			DateTime dateTime = DateTime.MinValue;
			foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
			{
				if (mailboxCopierBase.TimestampWhenPersistentProgressWasMade > dateTime)
				{
					dateTime = mailboxCopierBase.TimestampWhenPersistentProgressWasMade;
				}
			}
			return dateTime;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00024AB0 File Offset: 0x00022CB0
		protected void RecordFailure(Exception failure, bool isFatal)
		{
			int config = this.GetConfig<int>("FailureHistoryLength");
			if (failure == null || config <= 0)
			{
				return;
			}
			if (this.FailureHistory == null)
			{
				this.FailureHistory = new FailureHistory();
			}
			this.FailureHistory.Add(failure, isFatal, config);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00024AF2 File Offset: 0x00022CF2
		protected SkippedItemCounts GetSkippedItemCounts()
		{
			if (this.SkippedItemCounts == null)
			{
				this.SkippedItemCounts = new SkippedItemCounts();
			}
			return this.SkippedItemCounts;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00024B10 File Offset: 0x00022D10
		private void MoveToThrottledStateNow()
		{
			this.stateBeforeBeingThrottled = new RequestState?(this.TimeTracker.CurrentState);
			this.TimeTracker.CurrentState = this.GetThrottleStateForResource(this.currentlyThrottledResource);
			MrsTracer.Throttling.Debug("Throttled by RUBS. Previous State: {0}. New State: {1}", new object[]
			{
				this.stateBeforeBeingThrottled,
				this.TimeTracker.CurrentState
			});
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00024B84 File Offset: 0x00022D84
		private RequestState GetThrottleStateForResource(ResourceKey throttledResource)
		{
			bool flag = this.CachedRequestJob.Direction == RequestDirection.Push;
			RequestState requestState;
			if (throttledResource is MdbResourceHealthMonitorKey || throttledResource is DiskLatencyResourceKey)
			{
				requestState = (flag ? RequestState.StalledDueToReadThrottle : RequestState.StalledDueToWriteThrottle);
			}
			else if (throttledResource is ProcessorResourceKey)
			{
				requestState = (flag ? RequestState.StalledDueToReadCpu : RequestState.StalledDueToWriteCpu);
			}
			else if (throttledResource is MdbReplicationResourceHealthMonitorKey || throttledResource is MdbAvailabilityResourceHealthMonitorKey)
			{
				requestState = RequestState.StalledDueToHA;
			}
			else if (throttledResource is CiAgeOfLastNotificationResourceKey)
			{
				requestState = RequestState.StalledDueToCI;
			}
			else if (throttledResource is LegacyResourceHealthMonitorKey)
			{
				ILegacyResourceHealthProvider legacyResourceHealthProvider = (ILegacyResourceHealthProvider)ResourceHealthMonitorManager.Singleton.Get(throttledResource);
				if (legacyResourceHealthProvider.Agent == ConstraintCheckAgent.MailboxDatabaseReplication)
				{
					requestState = RequestState.StalledDueToHA;
				}
				else if (legacyResourceHealthProvider.Agent == ConstraintCheckAgent.ContentIndexing)
				{
					requestState = RequestState.StalledDueToCI;
				}
				else
				{
					MrsTracer.Throttling.Debug("Agent should be set when stall is due to LegacyResourceHealthMonitor. Constraint check result: {0}", new object[]
					{
						legacyResourceHealthProvider.ConstraintResult
					});
					requestState = RequestState.StalledDueToWriteUnknown;
				}
			}
			else
			{
				requestState = RequestState.StalledDueToWriteUnknown;
			}
			MrsTracer.ResourceHealth.Debug("GetThrottleStateForResource(): Key: {0}, ResultState: {1}", new object[]
			{
				throttledResource.ToString(),
				requestState
			});
			return requestState;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00024C88 File Offset: 0x00022E88
		private void VerifyRequestIsOnThisMachine()
		{
			DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(this.RequestJobStoringMDB.ObjectGuid, null, null, FindServerFlags.None);
			if (!databaseInformation.IsOnThisServer)
			{
				this.Report.Append(MrsStrings.ReportRelinquishingJobDueToFailover(databaseInformation.ServerFqdn));
				throw new RelinquishJobDatabaseFailoverTransientException();
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00024CD0 File Offset: 0x00022ED0
		private void VerifyWlmIsNotStalled()
		{
			TimeSpan t = TimeSpan.MaxValue;
			RelinquishJobTransientException ex = new RelinquishJobGenericTransientException();
			if (this.lastThrottledResource != null)
			{
				switch (this.GetThrottleStateForResource(this.lastThrottledResource))
				{
				case RequestState.StalledDueToHA:
					t = this.GetConfig<TimeSpan>("MaxStallRetryPeriod");
					ex = new RelinquishJobHAStallTransientException();
					break;
				case RequestState.StalledDueToCI:
					t = this.GetConfig<TimeSpan>("MaxStallRetryPeriod");
					ex = new RelinquishJobCIStallTransientException();
					break;
				case RequestState.StalledDueToReadThrottle:
				case RequestState.StalledDueToWriteThrottle:
				case RequestState.StalledDueToReadCpu:
				case RequestState.StalledDueToWriteCpu:
				case RequestState.StalledDueToReadUnknown:
				case RequestState.StalledDueToWriteUnknown:
					ex = new RelinquishJobThrottledTransientException();
					t = this.GetConfig<TimeSpan>("WlmThrottlingJobTimeout");
					break;
				}
			}
			DateTime d = this.TimeTracker.GetTimestamp(RequestJobTimestamp.LastUpdate) ?? DateTime.MinValue;
			if (DateTime.UtcNow - d >= t)
			{
				MrsTracer.Service.Debug("Relinquishing job {0} due to WLM: {1}", new object[]
				{
					this.RequestJobIdentity,
					CommonUtils.FullExceptionMessage(ex)
				});
				throw ex;
			}
			if (this.IsStalledByHigherPriorityJobs)
			{
				throw new RelinquishJobThrottledTransientException();
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00024DE8 File Offset: 0x00022FE8
		private void VerifyTargetDatabaseState()
		{
			if (!CommonUtils.ShouldHonorProvisioningSettings())
			{
				return;
			}
			if (this.CachedRequestJob.RequestType != MRSRequestType.Move && this.CachedRequestJob.RequestType != MRSRequestType.MailboxRelocation)
			{
				return;
			}
			if (!this.CachedRequestJob.TargetIsLocal)
			{
				return;
			}
			if (this.CachedRequestJob.SkipProvisioningCheck)
			{
				return;
			}
			MailboxDatabase mailboxDatabase = null;
			if (this.CachedRequestJob.PrimaryIsMoving)
			{
				mailboxDatabase = CommonUtils.FindMdbByGuid(this.CachedRequestJob.TargetMDBGuid, null, null);
				if (mailboxDatabase.IsExcludedFromProvisioning)
				{
					throw new TargetExcludedFromProvisioningPermanentException(this.CachedRequestJob.TargetMDBGuid);
				}
			}
			if (this.CachedRequestJob.ArchiveIsMoving)
			{
				if (!this.CachedRequestJob.TargetArchiveMDBGuid.Equals(this.CachedRequestJob.TargetMDBGuid))
				{
					mailboxDatabase = CommonUtils.FindMdbByGuid(this.CachedRequestJob.TargetArchiveMDBGuid, null, null);
				}
				if (mailboxDatabase.IsExcludedFromProvisioning)
				{
					throw new TargetExcludedFromProvisioningPermanentException(this.CachedRequestJob.TargetArchiveMDBGuid);
				}
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00024ECC File Offset: 0x000230CC
		private void CheckHAandCIHealth()
		{
			this.RefreshRequestIfNeeded();
			ExDateTime utcNow = ExDateTime.UtcNow;
			if (!this.GetConfig<bool>("EnableDataGuaranteeCheck") || (!this.TestIntegration.DisableDataGuaranteeCheckPeriod && utcNow < this.nextHealthCheckTimestamp))
			{
				return;
			}
			ExDateTime exDateTime = utcNow + this.GetConfig<TimeSpan>("MaxStallRetryPeriod");
			ExDateTime exDateTime2 = ExDateTime.MinValue;
			ExDateTime t = ExDateTime.MinValue;
			ServerHealthStatus serverHealthStatus = null;
			ConstraintCheckAgent constraintCheckAgent = ConstraintCheckAgent.None;
			RequestState currentState = this.TimeTracker.CurrentState;
			ExDateTime dt = utcNow;
			for (;;)
			{
				this.RefreshRequestIfNeeded();
				if (utcNow >= t)
				{
					t = utcNow + this.GetConfig<TimeSpan>("DataGuaranteeCheckPeriod");
					foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
					{
						serverHealthStatus = mailboxCopierBase.CheckServersHealth();
						if (serverHealthStatus != null && serverHealthStatus.HealthState != ServerHealthState.Healthy)
						{
							break;
						}
					}
					if (serverHealthStatus == null || serverHealthStatus.HealthState == ServerHealthState.Healthy)
					{
						break;
					}
					if (this.TestIntegration.DisableRetriesOnTransientFailures)
					{
						goto Block_7;
					}
					if (utcNow >= exDateTime)
					{
						goto Block_8;
					}
					this.TestIntegration.Barrier("BreakpointStalledDueToHACIHealth", new Action(this.RefreshRequestIfNeeded));
					switch (serverHealthStatus.Agent)
					{
					case ConstraintCheckAgent.MailboxDatabaseReplication:
						this.TimeTracker.CurrentState = RequestState.StalledDueToHA;
						break;
					case ConstraintCheckAgent.ContentIndexing:
						this.TimeTracker.CurrentState = RequestState.StalledDueToCI;
						break;
					default:
						this.TimeTracker.CurrentState = RequestState.Stalled;
						break;
					}
					if (utcNow - dt > BaseJob.StallReportingDelay)
					{
						if (exDateTime2 == ExDateTime.MinValue || constraintCheckAgent != serverHealthStatus.Agent)
						{
							this.Report.Append(MrsStrings.ReportJobIsStalledWithFailure(serverHealthStatus.FailureReason, ((DateTime)exDateTime).ToLocalTime()));
							this.FlushReport(null);
							exDateTime2 = utcNow;
						}
						else if (utcNow - exDateTime2 > BaseJob.StallReportingFrequency)
						{
							this.Report.Append(MrsStrings.ReportJobIsStillStalled);
							this.FlushReport(null);
							exDateTime2 = utcNow;
						}
					}
					constraintCheckAgent = serverHealthStatus.Agent;
				}
				Thread.Sleep(TimeSpan.FromSeconds(1.0));
				utcNow = ExDateTime.UtcNow;
			}
			if (exDateTime2 != ExDateTime.MinValue)
			{
				this.Report.Append(MrsStrings.ReportJobExitedStalledState);
				this.FlushReport(null);
			}
			this.nextHealthCheckTimestamp = utcNow + this.GetConfig<TimeSpan>("DataGuaranteeCheckPeriod");
			this.TimeTracker.CurrentState = currentState;
			return;
			Block_7:
			throw new JobStalledPermanentException(serverHealthStatus.FailureReason, (int)serverHealthStatus.Agent);
			Block_8:
			RelinquishJobTransientException ex;
			switch (serverHealthStatus.Agent)
			{
			case ConstraintCheckAgent.MailboxDatabaseReplication:
				ex = new RelinquishJobHAStallTransientException();
				break;
			case ConstraintCheckAgent.ContentIndexing:
				ex = new RelinquishJobCIStallTransientException();
				break;
			default:
				MrsTracer.Service.Error("Throttling Agent - HA or CI - was not set by Dumpster API.", new object[0]);
				ex = new RelinquishJobGenericTransientException();
				break;
			}
			throw ex;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000251A0 File Offset: 0x000233A0
		private void VerifyConnectionsAreAlive()
		{
			foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
			{
				mailboxCopierBase.DestMailboxWrapper.Ping();
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000251F8 File Offset: 0x000233F8
		private void TryUpdateJobFromSyncState(TransactionalRequestJob requestJob)
		{
			foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
			{
				if (mailboxCopierBase.SyncState == null)
				{
					return;
				}
			}
			BaseJob.BadItemCounts badItemCounts = this.GetBadItemCounts();
			requestJob.BadItemsEncountered = badItemCounts.BadItemCount;
			requestJob.LargeItemsEncountered = badItemCounts.LargeItemCount;
			requestJob.MissingItemsEncountered = badItemCounts.MissingItemCount;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0002527C File Offset: 0x0002347C
		private BaseJob.BadItemCounts GetBadItemCounts()
		{
			BaseJob.BadItemCounts result = default(BaseJob.BadItemCounts);
			BadItemCounter counter = new BadItemCounter(this.CachedRequestJob.SkipKnownCorruptions);
			foreach (MailboxCopierBase mailboxCopierBase in this.GetAllCopiers())
			{
				result.BadItemCount += mailboxCopierBase.GetBadItemsCountForCounter(counter);
				result.LargeItemCount += mailboxCopierBase.GetLargeItemsCount(counter);
				result.MissingItemCount += mailboxCopierBase.GetMissingItemsCount(counter);
			}
			return result;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00025324 File Offset: 0x00023524
		private void DoPeriodicChecks()
		{
			this.VerifyRequestIsOnThisMachine();
			this.VerifyWlmIsNotStalled();
			this.UpdateReportThrottled();
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00025338 File Offset: 0x00023538
		private void ReportContentChangesEnumerated(MailboxCopierBase mbxCtx, MailboxChanges changes, int countPages)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			changes.GetMessageCounts(out num, out num2, out num3, out num4, out num5);
			int total = num + num2 + num3 + num4 + num5;
			this.Report.Append(mbxCtx.IsIncrementalSyncPaged ? MrsStrings.ReportIncrementalSyncContentChangesPaged2(mbxCtx.SourceTracingID, countPages, num, num2, num3, num4, num5, total) : MrsStrings.ReportIncrementalSyncContentChanges2(mbxCtx.SourceTracingID, num, num2, num3, num4, num5, total));
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000253A0 File Offset: 0x000235A0
		private void ObtainReservations()
		{
			ReservationContext reservation = new ReservationContext();
			MoveJob.ReserveLocalForestResources(reservation, this.WorkloadTypeFromJob, this.CachedRequestJob.RequestType, this.CachedRequestJob.Flags, (this.CachedRequestJob.ArchiveGuid != null) ? this.CachedRequestJob.ArchiveGuid.Value : Guid.Empty, this.CachedRequestJob.ExchangeGuid, this.CachedRequestJob.SourceExchangeGuid, this.CachedRequestJob.TargetExchangeGuid, CommonUtils.GetPartitionHint(this.CachedRequestJob.OrganizationId), this.CachedRequestJob.SourceDatabase, this.CachedRequestJob.SourceArchiveDatabase, this.CachedRequestJob.TargetDatabase, this.CachedRequestJob.TargetArchiveDatabase, (this.CachedRequestJob.SourceDatabase != null) ? this.CachedRequestJob.SourceDatabase.ObjectGuid : Guid.Empty, (this.CachedRequestJob.SourceArchiveDatabase != null) ? this.CachedRequestJob.SourceArchiveDatabase.ObjectGuid : Guid.Empty, (this.CachedRequestJob.TargetDatabase != null) ? this.CachedRequestJob.TargetDatabase.ObjectGuid : Guid.Empty, (this.CachedRequestJob.TargetArchiveDatabase != null) ? this.CachedRequestJob.TargetArchiveDatabase.ObjectGuid : Guid.Empty);
			this.Reservation = reservation;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000254F8 File Offset: 0x000236F8
		[Conditional("DEBUG")]
		private void ValidateCountPages(MailboxCopierBase mbxContext, int count)
		{
			bool isIncrementalSyncPaged = mbxContext.IsIncrementalSyncPaged;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00025501 File Offset: 0x00023701
		private void MergeTimestamps(RequestJobTimeTracker other)
		{
			RequestJobTimeTracker.MergeTimestamps(this.TimeTracker, other);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00025510 File Offset: 0x00023710
		private void ScheduleBeginJob()
		{
			DateTime t = this.CachedRequestJob.NextPickupTime ?? DateTime.MinValue;
			if (DateTime.UtcNow < t && !this.IsInteractive)
			{
				base.ScheduleWorkItem(t.Subtract(DateTime.UtcNow), new Action(this.BeginJob), WorkloadType.Unknown);
				return;
			}
			if (this.LastFailure == null)
			{
				base.ScheduleWorkItem(new Action(this.BeginJob), WorkloadType.Unknown);
				return;
			}
			TimeSpan config = ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("RetryDelay");
			base.ScheduleWorkItem(config, new Action(this.BeginJob), WorkloadType.Unknown);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00025E6C File Offset: 0x0002406C
		private void UpdateReportRelinquished(bool isJobCompleted)
		{
			lock (this.syncRoot)
			{
				CommonUtils.CatchKnownExceptions(delegate
				{
					using (RequestJobProvider rjProvider = new RequestJobProvider(this.RequestJobStoringMDB.ObjectGuid))
					{
						MapiUtils.RetryOnObjectChanged(delegate
						{
							Exception lastFailure = this.LastFailure;
							using (TransactionalRequestJob transactionalRequestJob = (TransactionalRequestJob)rjProvider.Read<TransactionalRequestJob>(this.GetRequestJobObjectId()))
							{
								if (transactionalRequestJob != null && transactionalRequestJob.ValidationResult == RequestJobBase.ValidationResultEnum.Valid && transactionalRequestJob.Status != RequestStatus.None)
								{
									bool flag2 = false;
									transactionalRequestJob.RequestJobState = JobProcessingState.Ready;
									if (isJobCompleted)
									{
										transactionalRequestJob.MRSServerName = null;
									}
									if (lastFailure is MRSProxyConnectionLimitReachedTransientException)
									{
										DateTime dateTime = DateTime.UtcNow + this.GetConfig<TimeSpan>("BackoffIntervalForProxyConnectionLimitReached");
										this.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, new DateTime?(dateTime));
										this.TimeTracker.CurrentState = RequestState.ProxyBackoff;
										MoveJob.AddRemoteHostInProxyBackoff(transactionalRequestJob.RemoteHostName, dateTime);
										transactionalRequestJob.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.JobHasBeenRelinquishedDueToProxyThrottling(dateTime.ToLocalTime()));
										this.Report.Append(MrsStrings.ReportProxyConnectionLimitMet(dateTime.ToLocalTime()), lastFailure, ReportEntryFlags.None);
									}
									else if (lastFailure is ServiceIsStoppingPermanentException)
									{
										this.Report.Append(MrsStrings.ReportRelinquishingJobDueToServiceStop, lastFailure, ReportEntryFlags.None);
										this.TimeTracker.CurrentState = RequestState.Relinquished;
										this.Report.Append(MrsStrings.ReportRelinquishingJob);
									}
									else if (lastFailure is RelinquishJobTransientException)
									{
										this.TestIntegration.Barrier("BreakpointRelinquish", new Action(this.RefreshRequestIfNeeded));
										if (lastFailure is RelinquishJobDatabaseFailoverTransientException)
										{
											transactionalRequestJob.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.JobHasBeenRelinquishedDueToDatabaseFailover);
											this.TimeTracker.CurrentState = RequestState.RelinquishedMDBFailover;
											this.Report.Append(MrsStrings.ReportRelinquishingJob, lastFailure, ReportEntryFlags.None);
										}
										else if (lastFailure is RelinquishJobSuspendedTransientException)
										{
											transactionalRequestJob.Status = RequestStatus.Suspended;
											this.TimeTracker.SetTimestamp(RequestJobTimestamp.Suspended, new DateTime?(DateTime.UtcNow));
											this.TimeTracker.CurrentState = RequestState.Suspended;
											this.Report.Append(MrsStrings.ReportRelinquishingJob);
										}
										else if (lastFailure is RelinquishJobMailboxLockoutTransientException)
										{
											DateTime dateTime = ((RelinquishJobMailboxLockoutTransientException)lastFailure).PickupTime;
											this.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, new DateTime?(dateTime));
											this.TimeTracker.CurrentState = RequestState.StalledDueToMailboxLock;
											transactionalRequestJob.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.JobHasBeenRelinquishedDueToMailboxLockout(dateTime.ToLocalTime()));
											this.Report.Append(MrsStrings.ReportRelinquishBecauseMailboxIsLocked(dateTime.ToLocalTime()));
											flag2 = true;
										}
										else if (lastFailure is RelinquishJobDGTimeoutTransientException)
										{
											DateTime dateTime = ((RelinquishJobDGTimeoutTransientException)lastFailure).PickupTime;
											this.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, new DateTime?(dateTime));
											this.TimeTracker.CurrentState = RequestState.RelinquishedDataGuarantee;
											LocalizedString localizedString = MrsStrings.JobHasBeenRelinquishedDueToDataGuaranteeTimeout(dateTime.ToLocalTime());
											transactionalRequestJob.Message = MrsStrings.MoveRequestMessageInformational(localizedString);
											this.Report.Append(localizedString, lastFailure.InnerException, ReportEntryFlags.None);
										}
										else if (lastFailure is RelinquishJobHAStallTransientException)
										{
											this.TimeTracker.CurrentState = RequestState.RelinquishedHAStall;
											transactionalRequestJob.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.JobHasBeenRelinquishedDueToHAStall);
											this.Report.Append(MrsStrings.ReportRelinquishingJobDueToHAStall);
										}
										else if (lastFailure is RelinquishJobCIStallTransientException)
										{
											this.TimeTracker.CurrentState = RequestState.RelinquishedCIStall;
											transactionalRequestJob.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.JobHasBeenRelinquishedDueToCIStall);
											this.Report.Append(MrsStrings.ReportRelinquishingJobDueToCIStall);
										}
										else if (lastFailure is RelinquishJobThrottledTransientException)
										{
											DateTime dateTime = DateTime.UtcNow + this.GetConfig<TimeSpan>("WlmThrottlingJobRetryInterval");
											this.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, new DateTime?(dateTime));
											this.TimeTracker.CurrentState = RequestState.RelinquishedWlmStall;
											transactionalRequestJob.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.RequestHasBeenPostponedDueToBadHealthOfBackendServers(dateTime.ToLocalTime()));
											this.Report.Append(MrsStrings.ReportRelinquishingJobDueToServerThrottling);
										}
										else if (lastFailure is RelinquishJobRehomeTransientException)
										{
											transactionalRequestJob.RehomeRequest = true;
											this.TimeTracker.CurrentState = RequestState.Relinquished;
											this.Report.Append(MrsStrings.ReportRelinquishingJobDueToNeedForRehome);
										}
										else if (lastFailure is RelinquishJobResourceReservationTransientException)
										{
											this.TimeTracker.CurrentState = RequestState.Relinquished;
											transactionalRequestJob.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.JobHasBeenRelinquishedDueToResourceReservation(CommonUtils.FullExceptionMessage(lastFailure)));
											this.Report.Append(MrsStrings.ReportRelinquishBecauseResourceReservationFailed(CommonUtils.FullExceptionMessage(lastFailure)), lastFailure, ReportEntryFlags.None);
										}
										else if (lastFailure is RelinquishJobLongRunTransientException)
										{
											this.TimeTracker.CurrentState = RequestState.Relinquished;
											transactionalRequestJob.Message = MrsStrings.MoveRequestMessageInformational(MrsStrings.JobHasBeenRelinquishedDueToLongRun);
											this.Report.Append(MrsStrings.JobHasBeenRelinquishedDueToLongRun, lastFailure, ReportEntryFlags.None);
										}
										else if (lastFailure is RelinquishCancelPostponedTransientException)
										{
											transactionalRequestJob.Message = MrsStrings.JobHasBeenRelinquishedDueToCancelPostponed(((RelinquishCancelPostponedTransientException)lastFailure).RemoveAfter.ToLocalTime());
											this.Report.Append(MrsStrings.ReportRequestCancelPostponed);
										}
										else if (lastFailure is RelinquishJobOfflineTransientException)
										{
											DateTime dateTime = ((RelinquishJobOfflineTransientException)lastFailure).PickupTime;
											this.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, new DateTime?(dateTime));
											transactionalRequestJob.Message = MrsStrings.JobHasBeenRelinquishedDueToTransientErrorDuringOfflineMove(dateTime.ToLocalTime());
											this.Report.Append(MrsStrings.ReportRequestOfflineMovePostponed);
										}
										else
										{
											this.TimeTracker.CurrentState = RequestState.Relinquished;
											this.Report.Append(MrsStrings.ReportRelinquishingJob);
										}
									}
									if (!(lastFailure is JobIsPoisonedPermananentException))
									{
										if (flag2)
										{
											transactionalRequestJob.PoisonCount--;
										}
										else
										{
											transactionalRequestJob.PoisonCount = 0;
										}
									}
									this.TimeTracker.AddDurationToState(this.SuspendTime, RequestState.Idle);
									this.MergeTimestamps(transactionalRequestJob.TimeTracker);
									transactionalRequestJob.TimeTracker = this.TimeTracker;
									this.Report.Append(this.TimeTracker, ReportEntryFlags.TargetThrottleDurations | ReportEntryFlags.SourceThrottleDurations);
									this.UpdateRequestOnSave(transactionalRequestJob, UpdateRequestOnSaveType.InMemoryUpdatesOnly);
									rjProvider.Save(transactionalRequestJob);
									if (!isJobCompleted)
									{
										this.UpdateCachedRequestJob(transactionalRequestJob);
									}
									this.FlushReport(rjProvider.SystemMailbox);
									RequestJobLog.Write(transactionalRequestJob);
									transactionalRequestJob.UpdateAsyncNotification(this.Report);
								}
							}
						});
					}
				}, delegate(Exception failure)
				{
					LocalizedString localizedString = CommonUtils.FullExceptionMessage(failure);
					this.Report.Append(MrsStrings.ReportRequestSaveFailed2(CommonUtils.GetFailureType(failure)), failure, ReportEntryFlags.Cleanup);
					MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_RequestSaveFailed, new object[]
					{
						this.RequestJobIdentity,
						this.GetRequestKeyGuid().ToString(),
						this.RequestJobStoringMDB.ToString(),
						localizedString
					});
				});
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00025EE8 File Offset: 0x000240E8
		private void DisposeReservations()
		{
			if (this.Reservation != null)
			{
				this.Reservation.Dispose();
				this.Reservation = null;
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00025F04 File Offset: 0x00024104
		ISettingsContext ISettingsContextProvider.GetSettingsContext()
		{
			if (this.CachedRequestJob == null)
			{
				return null;
			}
			return ((ISettingsContextProvider)this.CachedRequestJob).GetSettingsContext();
		}

		// Token: 0x04000283 RID: 643
		public static readonly TimeSpan FlushInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000284 RID: 644
		public static readonly TimeSpan RunUnthrottledInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000285 RID: 645
		public static readonly TimeSpan StallReportingDelay = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000286 RID: 646
		public static readonly TimeSpan StallReportingFrequency = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000287 RID: 647
		public readonly BaseJobDiagnosticXml.JobTransferProgress JobTransferProgress;

		// Token: 0x04000288 RID: 648
		protected object syncRoot = new object();

		// Token: 0x04000289 RID: 649
		private ExDateTime lastLazySaveTimestamp;

		// Token: 0x0400028A RID: 650
		private ExDateTime lastRJTouchedTimestamp;

		// Token: 0x0400028B RID: 651
		private ExDateTime nextHealthCheckTimestamp;

		// Token: 0x0400028C RID: 652
		private ExDateTime jobPickupTimestamp;

		// Token: 0x0400028D RID: 653
		private byte[] requestJobMessageId;

		// Token: 0x0400028E RID: 654
		private bool needToRelinquishOnJobCompletion;

		// Token: 0x0400028F RID: 655
		private bool needToRefreshRequest;

		// Token: 0x04000290 RID: 656
		private RequestState? stateBeforeBeingThrottled;

		// Token: 0x04000291 RID: 657
		private ExDateTime throttleStartTimestamp;

		// Token: 0x04000292 RID: 658
		private string lastProxyThrottledReason = "Unknown";

		// Token: 0x04000293 RID: 659
		private ExDateTime lastInProgressRequestJobLogCheck;

		// Token: 0x04000294 RID: 660
		private ResourceKey currentlyThrottledResource;

		// Token: 0x04000295 RID: 661
		protected ResourceKey lastThrottledResource;

		// Token: 0x04000296 RID: 662
		private ExDateTime lastTaskExecutionTime;

		// Token: 0x02000084 RID: 132
		// (Invoke) Token: 0x0600063E RID: 1598
		internal delegate void UpdateRequestDelegate(TransactionalRequestJob requestJob);

		// Token: 0x02000085 RID: 133
		private struct BadItemCounts
		{
			// Token: 0x17000181 RID: 385
			// (get) Token: 0x06000641 RID: 1601 RVA: 0x00025F75 File Offset: 0x00024175
			// (set) Token: 0x06000642 RID: 1602 RVA: 0x00025F7D File Offset: 0x0002417D
			public int BadItemCount { get; set; }

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x06000643 RID: 1603 RVA: 0x00025F86 File Offset: 0x00024186
			// (set) Token: 0x06000644 RID: 1604 RVA: 0x00025F8E File Offset: 0x0002418E
			public int LargeItemCount { get; set; }

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x06000645 RID: 1605 RVA: 0x00025F97 File Offset: 0x00024197
			// (set) Token: 0x06000646 RID: 1606 RVA: 0x00025F9F File Offset: 0x0002419F
			public int MissingItemCount { get; set; }
		}
	}
}

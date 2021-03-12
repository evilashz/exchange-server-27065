using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000094 RID: 148
	internal class OlcMigrationJob : IncrementalMergeJob
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00031E78 File Offset: 0x00030078
		protected bool ReachedThePointOfNoReturn
		{
			get
			{
				return base.TimeTracker.CurrentState == RequestState.Completed || base.TimeTracker.CurrentState == RequestState.CompletedWithWarnings || base.SyncStage >= SyncStage.Cleanup;
			}
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00031EA7 File Offset: 0x000300A7
		protected override bool CanBeCanceledOrSuspended()
		{
			return !this.ReachedThePointOfNoReturn;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00031EB2 File Offset: 0x000300B2
		protected override bool IsInFinalization
		{
			get
			{
				return base.TimeTracker.CurrentState == RequestState.Completed || base.TimeTracker.CurrentState == RequestState.CompletedWithWarnings || base.SyncStage >= SyncStage.FinalIncrementalSync;
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00031EE4 File Offset: 0x000300E4
		public override void Initialize(TransactionalRequestJob syncRequest)
		{
			base.Initialize(syncRequest);
			string orgID = (syncRequest.OrganizationId != null && syncRequest.OrganizationId.OrganizationalUnit != null) ? (syncRequest.OrganizationId.OrganizationalUnit.Name + "\\") : string.Empty;
			base.MailboxMerger = new MailboxMerger(Guid.Empty, syncRequest.TargetExchangeGuid, syncRequest, this, MailboxCopierFlags.Olc, LocalizedString.Empty, MrsStrings.PrimaryMailboxTracingId(orgID, syncRequest.TargetExchangeGuid));
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00031F64 File Offset: 0x00030164
		protected override void StartMerge()
		{
			MrsTracer.Service.Debug("WorkItem: StartMerge", new object[0]);
			ADUser aduser = base.MailboxMerger.DestMailbox.GetADUser();
			if (base.SyncStage >= SyncStage.Cleanup || (PrimaryMailboxSourceType)aduser[ConsumerMailboxSchema.PrimaryMailboxSource] == PrimaryMailboxSourceType.Exo)
			{
				base.Report.Append(MrsStrings.ReportMoveAlreadyFinished2(MrsStrings.ReportPrimaryMservEntryPointsToExo));
				base.ScheduleWorkItem<int>(new Action<int>(this.Cleanup), 0, WorkloadType.Unknown);
				return;
			}
			base.StartMerge();
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00031FE4 File Offset: 0x000301E4
		protected override void AfterTargetConnect()
		{
			ADUser aduser = null;
			string text = base.CachedRequestJob.RemoteHostName;
			if (string.IsNullOrEmpty(text))
			{
				aduser = base.MailboxMerger.DestMailbox.GetADUser();
				IPAddress clusterIp = (IPAddress)aduser[ConsumerMailboxSchema.SatchmoClusterIp];
				text = OlcTopology.Instance.FindServerByClusterIP(clusterIp);
			}
			ulong value;
			int value2;
			if (base.CachedRequestJob.UserPuid != null && base.CachedRequestJob.OlcDGroup != null)
			{
				value = (ulong)base.CachedRequestJob.UserPuid.Value;
				value2 = base.CachedRequestJob.OlcDGroup.Value;
			}
			else
			{
				if (aduser == null)
				{
					aduser = base.MailboxMerger.DestMailbox.GetADUser();
				}
				if (!ConsumerIdentityHelper.TryGetPuidFromGuid(aduser.ExchangeGuid, out value))
				{
					throw new UnexpectedValuePermanentException(aduser.ExchangeGuid.ToString(), "destUser.ExchangeGuid");
				}
				string text2 = (string)aduser[ConsumerMailboxSchema.SatchmoDGroup];
				if (!int.TryParse(text2, out value2))
				{
					throw new UnexpectedValuePermanentException(text2, "MServRecipientSchema.SatchmoDGroup");
				}
			}
			base.MailboxMerger.SourceMailbox.ConfigOlc(new OlcMailboxConfiguration
			{
				Puid = (long)value,
				DGroup = value2,
				RemoteHostName = text
			});
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00032134 File Offset: 0x00030334
		protected override void IncrementalSync(bool firstIncrementalSync)
		{
			base.TestIntegration.Barrier("PostponeSync", new Action(base.RefreshRequestIfNeeded));
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			base.MailboxSnapshotTimestamp = DateTime.UtcNow;
			base.SyncStage = SyncStage.IncrementalSync;
			base.OverallProgress = this.CopyEndPercentage;
			base.TimeTracker.CurrentState = RequestState.IncrementalSync;
			this.IncrementalSync(0);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0003219C File Offset: 0x0003039C
		private void IncrementalSync(int iterationsCount)
		{
			SyncContext syncContext = base.EnumerateAndApplyIncrementalChanges();
			iterationsCount++;
			if (syncContext.CopyMessagesCount.TotalContentCopied > 100 && iterationsCount <= 10)
			{
				MrsTracer.Service.Debug("Too many updates during incremental sync, will repeat the sync.", new object[0]);
				base.ScheduleWorkItem<int>(new Action<int>(this.IncrementalSync), iterationsCount, WorkloadType.Unknown);
				return;
			}
			base.ScheduleWorkItem(new Action(this.FinalSync), WorkloadType.Unknown);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0003220C File Offset: 0x0003040C
		protected override bool ResetAfterFailure(out bool relinquishJobNow)
		{
			if (this.isSourceSetInTransit)
			{
				bool flag;
				base.MailboxMerger.SourceMailbox.SetInTransitStatus(InTransitStatus.NotInTransit, out flag);
			}
			return base.ResetAfterFailure(out relinquishJobNow);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0003223C File Offset: 0x0003043C
		private void FinalSync()
		{
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			base.SyncStage = SyncStage.FinalIncrementalSync;
			base.TimeTracker.CurrentState = RequestState.Finalization;
			base.TimeTracker.SetTimestamp(RequestJobTimestamp.FinalSync, new DateTime?(DateTime.UtcNow));
			base.Report.Append(MrsStrings.ReportFinalSyncStarted);
			base.TestIntegration.Barrier("PostponeFinalSync", new Action(base.RefreshRequestIfNeeded));
			MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_MoveFinalizationStarted, new object[]
			{
				base.RequestJobIdentity,
				base.MailboxMerger.TargetMailboxGuid
			});
			bool flag;
			base.MailboxMerger.SourceMailbox.SetInTransitStatus(InTransitStatus.MoveSource, out flag);
			this.isSourceSetInTransit = true;
			base.EnumerateAndApplyIncrementalChanges();
			base.SaveState(SaveStateFlags.Regular, null);
			base.CheckBadItemCount(true);
			if (!base.SkipContentVerification)
			{
				base.ScheduleContentVerification();
			}
			base.ScheduleWorkItem(new Action(this.SetMailboxSettings), WorkloadType.Unknown);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000323BC File Offset: 0x000305BC
		private void SetMailboxSettings()
		{
			List<ItemPropertiesBase> mailboxSettings = base.MailboxMerger.SourceMailbox.GetMailboxSettings(GetMailboxSettingsFlags.Finalize);
			if (mailboxSettings != null)
			{
				List<BadMessageRec> badItems = new List<BadMessageRec>();
				using (List<ItemPropertiesBase>.Enumerator enumerator = mailboxSettings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ItemPropertiesBase item = enumerator.Current;
						if (!base.MailboxMerger.SyncState.BadItems.ContainsKey(item.GetId()))
						{
							CommonUtils.ProcessKnownExceptions(delegate
							{
								ExecutionContext.Create(new DataContext[]
								{
									new ItemPropertiesDataContext(item)
								}).Execute(delegate
								{
									this.MailboxMerger.DestMailbox.SetMailboxSettings(item);
								});
							}, delegate(Exception failure)
							{
								if (MapiUtils.IsBadItemIndicator(failure))
								{
									badItems.Add(BadMessageRec.MailboxSetting(failure, item));
									return true;
								}
								return false;
							});
						}
					}
				}
				if (badItems != null && badItems.Count > 0)
				{
					base.MailboxMerger.ReportBadItems(badItems);
				}
			}
			base.ScheduleWorkItem(new Action(this.MservSwitchOver), WorkloadType.Unknown);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000324D4 File Offset: 0x000306D4
		private void MservSwitchOver()
		{
			ReportEntry[] entries = null;
			base.TestIntegration.Barrier("BreakpointBeforeUMM", new Action(base.RefreshRequestIfNeeded));
			base.MailboxMerger.MRSJob.CheckBadItemCount(true);
			try
			{
				base.MailboxMerger.DestMailbox.UpdateMovedMailbox(UpdateMovedMailboxOperation.UpdateMailbox, null, null, out entries, Guid.Empty, Guid.Empty, null, ArchiveStatusFlags.None, UpdateMovedMailboxFlags.MakeExoPrimary, null, null);
			}
			finally
			{
				base.AppendReportEntries(entries);
			}
			base.TestIntegration.Barrier("BreakpointAfterUMM", new Action(base.RefreshRequestIfNeeded));
			base.OverallProgress = 99;
			base.SyncStage = SyncStage.Cleanup;
			base.TimeTracker.CurrentState = RequestState.Cleanup;
			base.SaveState(SaveStateFlags.Regular, null);
			base.ScheduleWorkItem<int>(new Action<int>(this.Cleanup), 0, WorkloadType.Unknown);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00032640 File Offset: 0x00030840
		private void Cleanup(int cleanupRetries)
		{
			if (cleanupRetries > base.GetConfig<int>("MaxCleanupRetries"))
			{
				this.ReportCleanupFailure(new TooManyCleanupRetriesPermanentException());
				base.ScheduleWorkItem(new Action(base.CompleteMerge), WorkloadType.Unknown);
				return;
			}
			cleanupRetries++;
			CommonUtils.CatchKnownExceptions(delegate
			{
				base.MailboxMerger.SourceMailbox.DeleteMailbox(1);
				base.ScheduleWorkItem(new Action(base.CompleteMerge), WorkloadType.Unknown);
			}, delegate(Exception failure)
			{
				if (CommonUtils.IsTransientException(failure))
				{
					this.ScheduleWorkItem<int>(new Action<int>(this.Cleanup), cleanupRetries, WorkloadType.Unknown);
					return;
				}
				this.ReportCleanupFailure(failure);
				this.ScheduleWorkItem(new Action(this.CompleteMerge), WorkloadType.Unknown);
			});
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000326C0 File Offset: 0x000308C0
		private void ReportCleanupFailure(Exception failure)
		{
			LocalizedString localizedString = MrsStrings.SourceMailboxCleanupFailed(CommonUtils.FullExceptionMessage(failure));
			base.Report.Append(localizedString, failure, ReportEntryFlags.Cleanup);
			base.Warnings.Add(localizedString);
			FailureLog.Write(base.RequestJobGuid, failure, false, RequestState.Cleanup, SyncStage.CleanupDeleteSourceMailbox, null, null);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0003270A File Offset: 0x0003090A
		[Conditional("Debug")]
		private void ValidatePrimaryMailboxSatchmo()
		{
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0003270C File Offset: 0x0003090C
		[Conditional("Debug")]
		private void ValidatePrimaryMailboxExo()
		{
		}

		// Token: 0x04000300 RID: 768
		private bool isSourceSetInTransit;
	}
}

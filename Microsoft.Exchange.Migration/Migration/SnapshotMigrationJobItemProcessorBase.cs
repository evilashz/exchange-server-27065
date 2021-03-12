using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000FF RID: 255
	internal abstract class SnapshotMigrationJobItemProcessorBase : MigrationJobItemProcessorBase
	{
		// Token: 0x06000D7C RID: 3452 RVA: 0x00037520 File Offset: 0x00035720
		protected SnapshotMigrationJobItemProcessorBase(MigrationJobItem migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000D7D RID: 3453
		protected abstract MigrationState StateToSetWhenStageCompleted { get; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000D7E RID: 3454
		protected abstract MigrationProcessorResult ResultForProcessorActionCompleted { get; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x0003752A File Offset: 0x0003572A
		protected virtual MigrationFlags FlagsToClearOnSteadyState
		{
			get
			{
				return MigrationFlags.None;
			}
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00037530 File Offset: 0x00035730
		protected override MigrationJobItemProcessorResponse ApplyResponse(MigrationJobItemProcessorResponse response)
		{
			MigrationUserStatus status = this.MigrationObject.Status;
			MigrationFlags value = this.MigrationObject.Flags & ~this.FlagsToClearOnSteadyState;
			MigrationUserStatus migrationUserStatus;
			if (response.Result == MigrationProcessorResult.Completed)
			{
				MigrationWorkflowPosition nextPosition = this.MigrationObject.MigrationJob.Workflow.GetNextPosition(this.MigrationObject.WorkflowPosition, this.MigrationObject.SupportedSteps);
				if (nextPosition != null)
				{
					if (this.StateToSetWhenStageCompleted == MigrationState.Stopped)
					{
						migrationUserStatus = MigrationUserStatus.Stopped;
					}
					else
					{
						migrationUserStatus = nextPosition.GetInitialStatus();
					}
					this.MigrationObject.SetStatus(this.DataProvider, migrationUserStatus, this.StateToSetWhenStageCompleted, new MigrationFlags?(value), nextPosition, response.DelayTime, response.MailboxData, response.Settings, response.Snapshot, response.Updated, null);
					response = MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Working, null, null, null, null, null, false, null);
				}
				else
				{
					migrationUserStatus = ((response.Error == null) ? MigrationUserStatus.Completed : MigrationUserStatus.CompletedWithWarnings);
					this.MigrationObject.SetStatus(this.DataProvider, migrationUserStatus, MigrationState.Completed, new MigrationFlags?(MigrationFlags.None), null, response.DelayTime, response.MailboxData, response.Settings, response.Snapshot, response.Updated, response.Error);
				}
			}
			else if (response.Result == MigrationProcessorResult.Suspended)
			{
				migrationUserStatus = MigrationUserStatus.Stopped;
				this.MigrationObject.SetStatus(this.DataProvider, migrationUserStatus, MigrationState.Stopped, new MigrationFlags?(value), null, response.DelayTime, response.MailboxData, response.Settings, response.Snapshot, response.Updated, null);
			}
			else if (response.Result == MigrationProcessorResult.Failed)
			{
				migrationUserStatus = MigrationUserStatus.Failed;
				this.MigrationObject.SetStatus(this.DataProvider, migrationUserStatus, MigrationState.Failed, new MigrationFlags?(value), null, response.DelayTime, response.MailboxData, response.Settings, response.Snapshot, response.Updated, response.Error);
			}
			else if (response.Result == MigrationProcessorResult.Working)
			{
				migrationUserStatus = base.StepHandler.ResolvePresentationStatus(this.MigrationObject.Flags, response.Snapshot);
				this.MigrationObject.SetStatus(this.DataProvider, migrationUserStatus, MigrationState.Active, null, null, response.DelayTime, response.MailboxData, response.Settings, response.Snapshot, response.Updated, null);
			}
			else
			{
				if (response.Result != MigrationProcessorResult.Waiting)
				{
					return base.ApplyResponse(response);
				}
				migrationUserStatus = base.StepHandler.ResolvePresentationStatus(this.MigrationObject.Flags, response.Snapshot);
				this.MigrationObject.SetStatus(this.DataProvider, migrationUserStatus, MigrationState.Waiting, null, null, response.DelayTime, response.MailboxData, response.Settings, response.Snapshot, response.Updated, response.Error);
			}
			response.StatusChange = MigrationCountCache.MigrationStatusChange.CreateStatusChange(status, migrationUserStatus);
			return response;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000377D8 File Offset: 0x000359D8
		protected MigrationJobItemProcessorResponse ResponseFromSnapshot(IStepSnapshot snapshot, bool updated = false)
		{
			if (snapshot == null)
			{
				return MigrationJobItemProcessorResponse.Create(this.ResultForProcessorActionCompleted, null, null, null, null, null, updated, null);
			}
			LocalizedString? errorMessage = snapshot.ErrorMessage;
			if (errorMessage == null || errorMessage.Value.IsEmpty)
			{
				errorMessage = new LocalizedString?(Strings.UnknownMigrationError);
			}
			LocalizedException error = new MigrationPermanentException(errorMessage.Value);
			switch (snapshot.Status)
			{
			case SnapshotStatus.InProgress:
				return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Working, null, null, null, snapshot, null, updated, null);
			case SnapshotStatus.Failed:
				return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Failed, null, error, null, snapshot, null, updated, null);
			case SnapshotStatus.AutoSuspended:
			case SnapshotStatus.Suspended:
				return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Suspended, null, null, null, snapshot, null, updated, null);
			case SnapshotStatus.CompletedWithWarning:
				return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Completed, null, error, null, snapshot, null, updated, null);
			case SnapshotStatus.Finalized:
				return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, snapshot, null, updated, null);
			case SnapshotStatus.Synced:
				return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Waiting, new TimeSpan?(ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationProcessorSyncedJobItemDelay")), null, null, snapshot, null, updated, null);
			}
			throw new NotSupportedException("Status " + snapshot.Status + " not expected...");
		}
	}
}

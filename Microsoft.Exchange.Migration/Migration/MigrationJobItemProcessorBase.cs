using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000FE RID: 254
	internal abstract class MigrationJobItemProcessorBase : MigrationProcessorBase<MigrationJobItem, MigrationJobItemProcessorResponse>
	{
		// Token: 0x06000D70 RID: 3440 RVA: 0x00037210 File Offset: 0x00035410
		protected MigrationJobItemProcessorBase(MigrationJobItem migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
			this.StepHandler = MigrationServiceFactory.Instance.CreateStepHandler(this.MigrationObject.WorkflowPosition, dataProvider, this.MigrationObject.MigrationJob);
			this.SnapshotId = MigrationServiceFactory.Instance.GetStepSnapshotId(this.MigrationObject.WorkflowPosition, this.MigrationObject);
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0003726D File Offset: 0x0003546D
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x00037275 File Offset: 0x00035475
		private protected IStepHandler StepHandler { protected get; private set; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0003727E File Offset: 0x0003547E
		// (set) Token: 0x06000D74 RID: 3444 RVA: 0x00037286 File Offset: 0x00035486
		private protected ISnapshotId SnapshotId { protected get; private set; }

		// Token: 0x06000D75 RID: 3445 RVA: 0x00037290 File Offset: 0x00035490
		protected override MigrationJobItemProcessorResponse HandlePermanentException(LocalizedException ex)
		{
			if (ex is MigrationDataCorruptionException)
			{
				MigrationApplication.NotifyOfCorruptJob(ex, "MigrationJobItemProcessorBase::Process => job-item " + this.MigrationObject);
				MigrationHelper.SendFriendlyWatson(ex, true, this.MigrationObject.ToString());
			}
			else
			{
				MigrationApplication.NotifyOfCriticalError(ex, "MigrationJobItemProcessorBase::Process => job-item " + this.MigrationObject);
			}
			return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Failed, null, ex, null, null, null, false, null);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x000372FC File Offset: 0x000354FC
		protected override MigrationJobItemProcessorResponse HandleTransientException(LocalizedException ex)
		{
			if (MigrationApplication.HasTransientErrorReachedThreshold<MigrationUserStatus>(this.MigrationObject.StatusData))
			{
				MigrationLogger.Log(MigrationEventType.Warning, "Transient error reached threshold for job-item " + this.MigrationObject, new object[0]);
				return this.HandlePermanentException(ex);
			}
			MigrationApplication.NotifyOfTransientException(ex, "MigrationJobItemProcessorBase::Process => job-item " + this.MigrationObject);
			return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Waiting, null, ex, null, null, null, false, null);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0003736C File Offset: 0x0003556C
		protected override MigrationJobItemProcessorResponse PerformPoisonDetection()
		{
			return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Working, null, null, null, null, null, false, null);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0003738E File Offset: 0x0003558E
		protected override void SetContext()
		{
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00037390 File Offset: 0x00035590
		protected override void RestoreContext()
		{
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00037394 File Offset: 0x00035594
		protected override MigrationJobItemProcessorResponse ApplyResponse(MigrationJobItemProcessorResponse response)
		{
			MigrationUserStatus status = this.MigrationObject.Status;
			MigrationUserStatus migrationUserStatus;
			switch (response.Result)
			{
			case MigrationProcessorResult.Working:
				migrationUserStatus = this.StepHandler.ResolvePresentationStatus(this.MigrationObject.Flags, null);
				this.MigrationObject.SetStatus(this.DataProvider, migrationUserStatus, MigrationState.Active, null, null, response.DelayTime, null, null, null, false, null);
				goto IL_135;
			case MigrationProcessorResult.Waiting:
				migrationUserStatus = this.StepHandler.ResolvePresentationStatus(this.MigrationObject.Flags, null);
				this.MigrationObject.SetStatus(this.DataProvider, migrationUserStatus, MigrationState.Waiting, null, null, response.DelayTime, null, null, null, false, response.Error);
				goto IL_135;
			case MigrationProcessorResult.Failed:
				migrationUserStatus = MigrationUserStatus.Failed;
				this.MigrationObject.SetStatus(this.DataProvider, migrationUserStatus, MigrationState.Failed, new MigrationFlags?(MigrationFlags.None), null, null, null, null, null, false, response.Error);
				goto IL_135;
			case MigrationProcessorResult.Deleted:
			case MigrationProcessorResult.Suspended:
				throw new NotSupportedException("Not expected to see " + response.Result + " result for a job-item processor at this level...");
			}
			throw new NotSupportedException("expected " + response.Result + " to be handled at a different level... ");
			IL_135:
			response.StatusChange = MigrationCountCache.MigrationStatusChange.CreateStatusChange(status, migrationUserStatus);
			return response;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x000374E4 File Offset: 0x000356E4
		internal static MigrationJobItemProcessorResponse SetFlags(IMigrationDataProvider dataProvider, MigrationJobItem jobItem, MigrationFlags flags, MigrationProcessorResult result)
		{
			MigrationUserStatus status = jobItem.Status;
			jobItem.SetMigrationFlags(dataProvider, flags);
			return MigrationJobItemProcessorResponse.Create(result, null, null, null, null, null, false, MigrationCountCache.MigrationStatusChange.CreateStatusChange(status, jobItem.Status));
		}
	}
}

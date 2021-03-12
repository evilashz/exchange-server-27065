using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000102 RID: 258
	internal class RemoveMigrationJobItemProcessor : MigrationJobItemProcessorBase
	{
		// Token: 0x06000D96 RID: 3478 RVA: 0x00037BFF File Offset: 0x00035DFF
		public RemoveMigrationJobItemProcessor(MigrationJobItem migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00037C44 File Offset: 0x00035E44
		protected override MigrationJobItemProcessorResponse InternalProcess()
		{
			if (this.MigrationObject.State == MigrationState.Disabled)
			{
				return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, null, null, false, null);
			}
			ISnapshotId snapshotId = base.SnapshotId;
			MigrationStage stage = this.MigrationObject.WorkflowPosition.Stage;
			if (stage <= MigrationStage.Validation)
			{
				if (stage != MigrationStage.Discovery && stage != MigrationStage.Validation)
				{
					goto IL_EE;
				}
			}
			else if (stage != MigrationStage.Injection)
			{
				if (stage != MigrationStage.Processing)
				{
					goto IL_EE;
				}
				if (snapshotId != null)
				{
					base.StepHandler.Delete(snapshotId);
				}
				return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, null, null, false, null);
			}
			if (snapshotId != null)
			{
				CommonUtils.CatchKnownExceptions(delegate
				{
					this.StepHandler.Delete(snapshotId);
				}, delegate(Exception ex)
				{
					MigrationApplication.NotifyOfIgnoredException(ex, "Error clearing unexpected subscription for job-item: " + this.MigrationObject);
				});
			}
			return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, null, null, false, null);
			IL_EE:
			throw new ArgumentException("Don't know how to process stage: " + this.MigrationObject.WorkflowPosition.Stage);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00037D64 File Offset: 0x00035F64
		protected override MigrationJobItemProcessorResponse ApplyResponse(MigrationJobItemProcessorResponse response)
		{
			if (response.Result == MigrationProcessorResult.Completed)
			{
				MigrationUserStatus status = this.MigrationObject.Status;
				this.MigrationObject.Delete(this.DataProvider);
				return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Deleted, null, null, null, null, null, false, MigrationCountCache.MigrationStatusChange.CreateRemoval(status));
			}
			return base.ApplyResponse(response);
		}
	}
}

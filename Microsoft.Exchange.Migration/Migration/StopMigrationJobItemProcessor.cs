using System;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000104 RID: 260
	internal class StopMigrationJobItemProcessor : SnapshotMigrationJobItemProcessorBase
	{
		// Token: 0x06000D9D RID: 3485 RVA: 0x00037F1E File Offset: 0x0003611E
		public StopMigrationJobItemProcessor(MigrationJobItem migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00037F28 File Offset: 0x00036128
		protected override MigrationState StateToSetWhenStageCompleted
		{
			get
			{
				return MigrationState.Stopped;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x00037F2B File Offset: 0x0003612B
		protected override MigrationProcessorResult ResultForProcessorActionCompleted
		{
			get
			{
				return MigrationProcessorResult.Suspended;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00037F2E File Offset: 0x0003612E
		protected override MigrationFlags FlagsToClearOnSteadyState
		{
			get
			{
				return MigrationFlags.Stop;
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00037F34 File Offset: 0x00036134
		protected override MigrationJobItemProcessorResponse InternalProcess()
		{
			IStepSnapshot snapshot = null;
			ISnapshotId snapshotId = base.SnapshotId;
			MigrationStage stage = this.MigrationObject.WorkflowPosition.Stage;
			if (stage <= MigrationStage.Validation)
			{
				if (stage != MigrationStage.Discovery && stage != MigrationStage.Validation)
				{
					goto IL_58;
				}
			}
			else if (stage != MigrationStage.Injection)
			{
				if (stage != MigrationStage.Processing)
				{
					goto IL_58;
				}
				snapshot = base.StepHandler.Stop(snapshotId);
				goto IL_7D;
			}
			if (snapshotId != null)
			{
				snapshot = base.StepHandler.Stop(snapshotId);
				goto IL_7D;
			}
			goto IL_7D;
			IL_58:
			throw new ArgumentException("Don't know how to process stage: " + this.MigrationObject.WorkflowPosition.Stage);
			IL_7D:
			return base.ResponseFromSnapshot(snapshot, false);
		}
	}
}

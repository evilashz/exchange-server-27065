using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000100 RID: 256
	internal class ActiveMigrationJobItemProcessor : SnapshotMigrationJobItemProcessorBase
	{
		// Token: 0x06000D82 RID: 3458 RVA: 0x00037923 File Offset: 0x00035B23
		public ActiveMigrationJobItemProcessor(MigrationJobItem migrationObject, IMigrationDataProvider dataProvider) : base(migrationObject, dataProvider)
		{
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x0003792D File Offset: 0x00035B2D
		protected override MigrationState StateToSetWhenStageCompleted
		{
			get
			{
				return MigrationState.Active;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00037930 File Offset: 0x00035B30
		protected override MigrationProcessorResult ResultForProcessorActionCompleted
		{
			get
			{
				return MigrationProcessorResult.Completed;
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00037934 File Offset: 0x00035B34
		protected override MigrationJobItemProcessorResponse InternalProcess()
		{
			if (this.MigrationObject.MigrationJob.Workflow.ShouldDelay(this.MigrationObject.WorkflowPosition, this.MigrationObject.MigrationJob))
			{
				return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Waiting, new TimeSpan?(ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("SyncMigrationProcessorMinWaitingJobDelay")), null, null, null, null, false, null);
			}
			MigrationStage stage = this.MigrationObject.WorkflowPosition.Stage;
			if (stage <= MigrationStage.Validation)
			{
				if (stage == MigrationStage.Discovery)
				{
					return this.Discovery();
				}
				if (stage == MigrationStage.Validation)
				{
					return this.Validation();
				}
			}
			else
			{
				if (stage == MigrationStage.Injection)
				{
					return this.Injection();
				}
				if (stage == MigrationStage.Processing)
				{
					return this.Processing();
				}
			}
			throw new ArgumentException("Don't know how to process stage: " + this.MigrationObject.WorkflowPosition.Stage);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000379F8 File Offset: 0x00035BF8
		private MigrationJobItemProcessorResponse Discovery()
		{
			MailboxData mailboxDataFromSmtpAddress = this.DataProvider.ADProvider.GetMailboxDataFromSmtpAddress(this.MigrationObject.LocalMailboxIdentifier, false, base.StepHandler.ExpectMailboxData);
			IStepSettings stepSettings = base.StepHandler.Discover(this.MigrationObject, mailboxDataFromSmtpAddress);
			return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, mailboxDataFromSmtpAddress, null, stepSettings, false, null);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00037A58 File Offset: 0x00035C58
		private MigrationJobItemProcessorResponse Validation()
		{
			base.StepHandler.Validate(this.MigrationObject);
			return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, null, null, false, null);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00037A8C File Offset: 0x00035C8C
		private MigrationJobItemProcessorResponse Injection()
		{
			if (!base.StepHandler.CanProcess(this.MigrationObject))
			{
				return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Waiting, null, null, null, null, null, false, null);
			}
			IStepSnapshot stepSnapshot = base.StepHandler.Inject(this.MigrationObject);
			return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Completed, null, null, null, stepSnapshot, null, stepSnapshot != null, null);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00037AF0 File Offset: 0x00035CF0
		private MigrationJobItemProcessorResponse Processing()
		{
			if (!base.StepHandler.CanProcess(this.MigrationObject))
			{
				return MigrationJobItemProcessorResponse.Create(MigrationProcessorResult.Waiting, null, null, null, null, null, false, null);
			}
			bool updated;
			IStepSnapshot snapshot = base.StepHandler.Process(base.SnapshotId, this.MigrationObject, out updated);
			return base.ResponseFromSnapshot(snapshot, updated);
		}
	}
}

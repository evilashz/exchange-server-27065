using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000119 RID: 281
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MigrationJobStoppedProcessor : JobProcessor
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003DB84 File Offset: 0x0003BD84
		internal static MigrationJobStoppedProcessor CreateProcessor(MigrationType type)
		{
			return new MigrationJobStoppedProcessor();
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003DB8B File Offset: 0x0003BD8B
		internal override bool Validate()
		{
			return base.Job.Status == MigrationJobStatus.Stopped;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003DB9C File Offset: 0x0003BD9C
		internal override MigrationJobStatus GetNextStageStatus()
		{
			return MigrationJobStatus.Removing;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003DBA0 File Offset: 0x0003BDA0
		protected override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			if (utcNow < base.Job.StateLastUpdated)
			{
				throw new MigrationItemLastUpdatedInTheFutureTransientException(base.Job.StateLastUpdated.Value.ToLongDateString());
			}
			TimeSpan config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("MigrationJobStoppedThreshold");
			if (utcNow - base.Job.StateLastUpdated > config)
			{
				return LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
			}
			return LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Waiting, config - (utcNow - base.Job.StateLastUpdated));
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003DCDF File Offset: 0x0003BEDF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationJobStoppedProcessor>(this);
		}
	}
}

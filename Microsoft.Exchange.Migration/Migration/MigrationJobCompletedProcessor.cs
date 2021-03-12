using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000113 RID: 275
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MigrationJobCompletedProcessor : JobProcessor
	{
		// Token: 0x06000E6D RID: 3693 RVA: 0x0003C6D0 File Offset: 0x0003A8D0
		internal static MigrationJobCompletedProcessor CreateProcessor(MigrationType type, bool supportsMultiBatchFinalization)
		{
			if (type <= MigrationType.BulkProvisioning)
			{
				if (type == MigrationType.IMAP || type == MigrationType.ExchangeOutlookAnywhere)
				{
					throw new NotSupportedException("IMAP/Exchange not supported in FinalizeReported state");
				}
				if (type == MigrationType.BulkProvisioning)
				{
					throw new NotSupportedException("Bulk Provisioning not supported in FinalizeReported state");
				}
			}
			else if (type == MigrationType.ExchangeRemoteMove || type == MigrationType.ExchangeLocalMove || type == MigrationType.PublicFolder)
			{
				if (supportsMultiBatchFinalization)
				{
					return null;
				}
				return new MigrationJobCompletedProcessor();
			}
			throw new ArgumentException("Invalid MigrationType " + type);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003C739 File Offset: 0x0003A939
		internal override bool Validate()
		{
			return true;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003C73C File Offset: 0x0003A93C
		internal override MigrationJobStatus GetNextStageStatus()
		{
			return MigrationJobStatus.Removing;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0003C740 File Offset: 0x0003A940
		protected override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			return LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0003C75C File Offset: 0x0003A95C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationJobCompletedProcessor>(this);
		}
	}
}

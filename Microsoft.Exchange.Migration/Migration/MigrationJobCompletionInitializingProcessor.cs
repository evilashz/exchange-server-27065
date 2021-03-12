using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000115 RID: 277
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MigrationJobCompletionInitializingProcessor : JobProcessor
	{
		// Token: 0x06000E81 RID: 3713 RVA: 0x0003CB60 File Offset: 0x0003AD60
		internal static MigrationJobCompletionInitializingProcessor CreateProcessor(MigrationType type)
		{
			if (type <= MigrationType.BulkProvisioning)
			{
				if (type == MigrationType.IMAP || type == MigrationType.ExchangeOutlookAnywhere)
				{
					throw new NotSupportedException("Exchange/IMAP not supported in CompletionInitializing state");
				}
				if (type == MigrationType.BulkProvisioning)
				{
					throw new NotSupportedException("Bulk Provisioning not supported in CompletionInitializing state");
				}
			}
			else
			{
				if (type == MigrationType.ExchangeRemoteMove || type == MigrationType.ExchangeLocalMove)
				{
					return new MigrationJobCompletionInitializingProcessor();
				}
				if (type == MigrationType.PublicFolder)
				{
					return new PublicFolderJobCompletionInitializingProcessor();
				}
			}
			throw new ArgumentException("Invalid MigrationType " + type);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003CBCC File Offset: 0x0003ADCC
		internal override bool Validate()
		{
			return base.Job.Status == MigrationJobStatus.CompletionInitializing;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0003CBDC File Offset: 0x0003ADDC
		internal override MigrationJobStatus GetNextStageStatus()
		{
			return MigrationJobStatus.CompletionStarting;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003CBE0 File Offset: 0x0003ADE0
		protected override LegacyMigrationJobProcessorResponse Process(bool scheduleNewWork)
		{
			return LegacyMigrationJobProcessorResponse.Create(MigrationProcessorResult.Completed, null);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0003CBFC File Offset: 0x0003ADFC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationJobCompletionInitializingProcessor>(this);
		}
	}
}

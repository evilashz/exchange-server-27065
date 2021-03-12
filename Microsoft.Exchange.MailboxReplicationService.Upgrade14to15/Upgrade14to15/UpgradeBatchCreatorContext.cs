using System;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000BE RID: 190
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpgradeBatchCreatorContext : AnchorContext
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x0000AF9B File Offset: 0x0000919B
		public UpgradeBatchCreatorContext() : base("UpgradeBatchCreator", OrganizationCapability.TenantUpgrade, new UpgradeBatchCreatorConfig())
		{
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000AFB0 File Offset: 0x000091B0
		public override CacheProcessorBase[] CreateCacheComponents(WaitHandle stopEvent)
		{
			OrgOperationProxy orgOperationProxyInstance = new OrgOperationProxy();
			return new CacheProcessorBase[]
			{
				new CacheScanner(this, stopEvent),
				new UpgradeBatchCreatorScheduler(this, orgOperationProxyInstance, stopEvent)
			};
		}

		// Token: 0x040002C2 RID: 706
		public const string BatchFileDirectoryPath = "BatchFileDirectoryPath";

		// Token: 0x040002C3 RID: 707
		public const string MaxBatchSize = "MaxBatchSize";

		// Token: 0x040002C4 RID: 708
		public const string UpgradeBatchFilenamePrefix = "UpgradeBatchFilenamePrefix";

		// Token: 0x040002C5 RID: 709
		public const string DryRunBatchFilenamePrefix = "DryRunBatchFilenamePrefix";

		// Token: 0x040002C6 RID: 710
		public const string E14CountUpdateIntervalName = "E14CountUpdateInterval";

		// Token: 0x040002C7 RID: 711
		public const string RemoveNonUpgradeMoveRequests = "RemoveNonUpgradeMoveRequests";

		// Token: 0x040002C8 RID: 712
		public const string ConfigOnly = "ConfigOnly";

		// Token: 0x040002C9 RID: 713
		public const string DelayUntilCreateNewBatches = "DelayUntilCreateNewBatches";

		// Token: 0x040002CA RID: 714
		internal const string BatchFileDirectoryName = "UpgradeBatches";

		// Token: 0x040002CB RID: 715
		internal const string UpgradeApplicationName = "UpgradeBatchCreator";
	}
}

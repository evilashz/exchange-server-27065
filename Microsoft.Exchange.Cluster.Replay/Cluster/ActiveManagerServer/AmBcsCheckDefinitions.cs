using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000A0 RID: 160
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AmBcsCheckDefinitions
	{
		// Token: 0x040002C1 RID: 705
		internal const AmBcsChecks DatabaseNeverMountedChecks = AmBcsChecks.IsPassiveCopy;

		// Token: 0x040002C2 RID: 706
		internal const AmBcsChecks Phase1Checks = AmBcsChecks.IsHealthyOrDisconnected | AmBcsChecks.IsCatalogStatusHealthy | AmBcsChecks.CopyQueueLength | AmBcsChecks.ReplayQueueLength | AmBcsChecks.IsPassiveCopy | AmBcsChecks.IsSeedingSource | AmBcsChecks.TotalQueueLengthMaxAllowed | AmBcsChecks.ActivationEnabled | AmBcsChecks.MaxActivesUnderPreferredLimit;

		// Token: 0x040002C3 RID: 707
		internal const AmBcsChecks Phase2Checks = AmBcsChecks.IsHealthyOrDisconnected | AmBcsChecks.CopyQueueLength | AmBcsChecks.ReplayQueueLength | AmBcsChecks.IsCatalogStatusCrawling | AmBcsChecks.IsPassiveCopy | AmBcsChecks.IsSeedingSource | AmBcsChecks.TotalQueueLengthMaxAllowed | AmBcsChecks.ActivationEnabled | AmBcsChecks.MaxActivesUnderPreferredLimit;

		// Token: 0x040002C4 RID: 708
		internal const AmBcsChecks Phase3Checks = AmBcsChecks.IsHealthyOrDisconnected | AmBcsChecks.IsCatalogStatusHealthy | AmBcsChecks.ReplayQueueLength | AmBcsChecks.IsPassiveCopy | AmBcsChecks.IsSeedingSource | AmBcsChecks.TotalQueueLengthMaxAllowed | AmBcsChecks.ActivationEnabled | AmBcsChecks.MaxActivesUnderPreferredLimit;

		// Token: 0x040002C5 RID: 709
		internal const AmBcsChecks Phase4Checks = AmBcsChecks.IsHealthyOrDisconnected | AmBcsChecks.ReplayQueueLength | AmBcsChecks.IsCatalogStatusCrawling | AmBcsChecks.IsPassiveCopy | AmBcsChecks.IsSeedingSource | AmBcsChecks.TotalQueueLengthMaxAllowed | AmBcsChecks.ActivationEnabled | AmBcsChecks.MaxActivesUnderPreferredLimit;

		// Token: 0x040002C6 RID: 710
		internal const AmBcsChecks Phase5Checks = AmBcsChecks.IsHealthyOrDisconnected | AmBcsChecks.ReplayQueueLength | AmBcsChecks.IsPassiveCopy | AmBcsChecks.IsSeedingSource | AmBcsChecks.TotalQueueLengthMaxAllowed | AmBcsChecks.ActivationEnabled | AmBcsChecks.MaxActivesUnderHighestLimit;

		// Token: 0x040002C7 RID: 711
		internal const AmBcsChecks Phase6Checks = AmBcsChecks.IsHealthyOrDisconnected | AmBcsChecks.IsCatalogStatusHealthy | AmBcsChecks.CopyQueueLength | AmBcsChecks.IsPassiveCopy | AmBcsChecks.TotalQueueLengthMaxAllowed | AmBcsChecks.ActivationEnabled | AmBcsChecks.MaxActivesUnderHighestLimit;

		// Token: 0x040002C8 RID: 712
		internal const AmBcsChecks Phase7Checks = AmBcsChecks.IsHealthyOrDisconnected | AmBcsChecks.CopyQueueLength | AmBcsChecks.IsCatalogStatusCrawling | AmBcsChecks.IsPassiveCopy | AmBcsChecks.TotalQueueLengthMaxAllowed | AmBcsChecks.ActivationEnabled | AmBcsChecks.MaxActivesUnderHighestLimit;

		// Token: 0x040002C9 RID: 713
		internal const AmBcsChecks Phase8Checks = AmBcsChecks.IsHealthyOrDisconnected | AmBcsChecks.IsCatalogStatusHealthy | AmBcsChecks.IsPassiveCopy | AmBcsChecks.TotalQueueLengthMaxAllowed | AmBcsChecks.ActivationEnabled | AmBcsChecks.MaxActivesUnderHighestLimit;

		// Token: 0x040002CA RID: 714
		internal const AmBcsChecks Phase9Checks = AmBcsChecks.IsHealthyOrDisconnected | AmBcsChecks.IsCatalogStatusCrawling | AmBcsChecks.IsPassiveCopy | AmBcsChecks.TotalQueueLengthMaxAllowed | AmBcsChecks.ActivationEnabled | AmBcsChecks.MaxActivesUnderHighestLimit;

		// Token: 0x040002CB RID: 715
		internal const AmBcsChecks Phase10Checks = AmBcsChecks.IsHealthyOrDisconnected | AmBcsChecks.IsPassiveCopy | AmBcsChecks.TotalQueueLengthMaxAllowed | AmBcsChecks.MaxActivesUnderHighestLimit;
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x0200009F RID: 159
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AmBcsSkippedCheckDefinitions
	{
		// Token: 0x040002BD RID: 701
		internal static AmBcsChecks[] SkipClientExperienceChecks = new AmBcsChecks[]
		{
			AmBcsChecks.IsCatalogStatusHealthy,
			AmBcsChecks.IsCatalogStatusCrawling,
			AmBcsChecks.ManagedAvailabilityInitiatorBetterThanSource,
			AmBcsChecks.ManagedAvailabilityAllHealthy,
			AmBcsChecks.ManagedAvailabilityUptoNormalHealthy,
			AmBcsChecks.ManagedAvailabilityAllBetterThanSource,
			AmBcsChecks.ManagedAvailabilitySameAsSource
		};

		// Token: 0x040002BE RID: 702
		internal static AmBcsChecks[] SkipHealthChecks = new AmBcsChecks[]
		{
			AmBcsChecks.IsHealthyOrDisconnected
		};

		// Token: 0x040002BF RID: 703
		internal static AmBcsChecks[] SkipLagChecks = new AmBcsChecks[]
		{
			AmBcsChecks.CopyQueueLength,
			AmBcsChecks.ReplayQueueLength,
			AmBcsChecks.IsSeedingSource,
			AmBcsChecks.TotalQueueLengthMaxAllowed
		};

		// Token: 0x040002C0 RID: 704
		internal static AmBcsChecks[] SkipMaximumActiveDatabasesChecks = new AmBcsChecks[]
		{
			AmBcsChecks.MaxActivesUnderPreferredLimit,
			AmBcsChecks.MaxActivesUnderHighestLimit
		};
	}
}

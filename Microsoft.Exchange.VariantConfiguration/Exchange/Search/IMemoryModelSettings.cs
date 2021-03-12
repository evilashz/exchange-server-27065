using System;
using System.CodeDom.Compiler;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000085 RID: 133
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	public interface IMemoryModelSettings : ISettings
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000332 RID: 818
		float MemoryUsageAdjustmentMultiplier { get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000333 RID: 819
		int SandboxMaxPoolSize { get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000334 RID: 820
		int LowAvailableSystemWorkingSetMemoryRatio { get; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000335 RID: 821
		long SearchMemoryModelBaseCost { get; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000336 RID: 822
		long BaselineCostPerActiveItem { get; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000337 RID: 823
		long BaselineCostPerPassiveItem { get; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000338 RID: 824
		long InstantSearchCostPerActiveItem { get; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000339 RID: 825
		long RefinersCostPerActiveItem { get; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600033A RID: 826
		bool DisableGracefulDegradationForInstantSearch { get; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600033B RID: 827
		bool DisableGracefulDegradationForAutoSuspend { get; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600033C RID: 828
		int TimerForGracefulDegradation { get; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600033D RID: 829
		long MemoryMeasureDrift { get; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600033E RID: 830
		long MaxRestoreAmount { get; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600033F RID: 831
		bool ShouldConsiderSearchMemoryUsageBudget { get; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000340 RID: 832
		long SearchMemoryUsageBudget { get; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000341 RID: 833
		long SearchMemoryUsageBudgetFloatingAmount { get; }
	}
}

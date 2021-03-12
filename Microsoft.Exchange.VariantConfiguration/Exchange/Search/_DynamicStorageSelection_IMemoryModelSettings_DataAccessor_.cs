using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000086 RID: 134
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DynamicStorageSelection_IMemoryModelSettings_DataAccessor_ : VariantObjectDataAccessorBase<IMemoryModelSettings, _DynamicStorageSelection_IMemoryModelSettings_Implementation_, _DynamicStorageSelection_IMemoryModelSettings_DataAccessor_>
	{
		// Token: 0x0400026C RID: 620
		internal string _Name_MaterializedValue_;

		// Token: 0x0400026D RID: 621
		internal float _MemoryUsageAdjustmentMultiplier_MaterializedValue_;

		// Token: 0x0400026E RID: 622
		internal ValueProvider<float> _MemoryUsageAdjustmentMultiplier_ValueProvider_;

		// Token: 0x0400026F RID: 623
		internal int _SandboxMaxPoolSize_MaterializedValue_;

		// Token: 0x04000270 RID: 624
		internal ValueProvider<int> _SandboxMaxPoolSize_ValueProvider_;

		// Token: 0x04000271 RID: 625
		internal int _LowAvailableSystemWorkingSetMemoryRatio_MaterializedValue_;

		// Token: 0x04000272 RID: 626
		internal ValueProvider<int> _LowAvailableSystemWorkingSetMemoryRatio_ValueProvider_;

		// Token: 0x04000273 RID: 627
		internal long _SearchMemoryModelBaseCost_MaterializedValue_;

		// Token: 0x04000274 RID: 628
		internal ValueProvider<long> _SearchMemoryModelBaseCost_ValueProvider_;

		// Token: 0x04000275 RID: 629
		internal long _BaselineCostPerActiveItem_MaterializedValue_;

		// Token: 0x04000276 RID: 630
		internal ValueProvider<long> _BaselineCostPerActiveItem_ValueProvider_;

		// Token: 0x04000277 RID: 631
		internal long _BaselineCostPerPassiveItem_MaterializedValue_;

		// Token: 0x04000278 RID: 632
		internal ValueProvider<long> _BaselineCostPerPassiveItem_ValueProvider_;

		// Token: 0x04000279 RID: 633
		internal long _InstantSearchCostPerActiveItem_MaterializedValue_;

		// Token: 0x0400027A RID: 634
		internal ValueProvider<long> _InstantSearchCostPerActiveItem_ValueProvider_;

		// Token: 0x0400027B RID: 635
		internal long _RefinersCostPerActiveItem_MaterializedValue_;

		// Token: 0x0400027C RID: 636
		internal ValueProvider<long> _RefinersCostPerActiveItem_ValueProvider_;

		// Token: 0x0400027D RID: 637
		internal bool _DisableGracefulDegradationForInstantSearch_MaterializedValue_;

		// Token: 0x0400027E RID: 638
		internal ValueProvider<bool> _DisableGracefulDegradationForInstantSearch_ValueProvider_;

		// Token: 0x0400027F RID: 639
		internal bool _DisableGracefulDegradationForAutoSuspend_MaterializedValue_;

		// Token: 0x04000280 RID: 640
		internal ValueProvider<bool> _DisableGracefulDegradationForAutoSuspend_ValueProvider_;

		// Token: 0x04000281 RID: 641
		internal int _TimerForGracefulDegradation_MaterializedValue_;

		// Token: 0x04000282 RID: 642
		internal ValueProvider<int> _TimerForGracefulDegradation_ValueProvider_;

		// Token: 0x04000283 RID: 643
		internal long _MemoryMeasureDrift_MaterializedValue_;

		// Token: 0x04000284 RID: 644
		internal ValueProvider<long> _MemoryMeasureDrift_ValueProvider_;

		// Token: 0x04000285 RID: 645
		internal long _MaxRestoreAmount_MaterializedValue_;

		// Token: 0x04000286 RID: 646
		internal ValueProvider<long> _MaxRestoreAmount_ValueProvider_;

		// Token: 0x04000287 RID: 647
		internal bool _ShouldConsiderSearchMemoryUsageBudget_MaterializedValue_;

		// Token: 0x04000288 RID: 648
		internal ValueProvider<bool> _ShouldConsiderSearchMemoryUsageBudget_ValueProvider_;

		// Token: 0x04000289 RID: 649
		internal long _SearchMemoryUsageBudget_MaterializedValue_;

		// Token: 0x0400028A RID: 650
		internal ValueProvider<long> _SearchMemoryUsageBudget_ValueProvider_;

		// Token: 0x0400028B RID: 651
		internal long _SearchMemoryUsageBudgetFloatingAmount_MaterializedValue_;

		// Token: 0x0400028C RID: 652
		internal ValueProvider<long> _SearchMemoryUsageBudgetFloatingAmount_ValueProvider_;
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000088 RID: 136
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class _DataOnly_IMemoryModelSettings_Implementation_ : IMemoryModelSettings, ISettings, IVariantObjectInstance, IVariantObjectInstanceProvider
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000064DA File Offset: 0x000046DA
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000064DD File Offset: 0x000046DD
		IVariantObjectInstance IVariantObjectInstanceProvider.GetVariantObjectInstance(VariantContextSnapshot context)
		{
			return this;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000064E0 File Offset: 0x000046E0
		public string Name
		{
			get
			{
				return this._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600035B RID: 859 RVA: 0x000064E8 File Offset: 0x000046E8
		public float MemoryUsageAdjustmentMultiplier
		{
			get
			{
				return this._MemoryUsageAdjustmentMultiplier_MaterializedValue_;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000064F0 File Offset: 0x000046F0
		public int SandboxMaxPoolSize
		{
			get
			{
				return this._SandboxMaxPoolSize_MaterializedValue_;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000064F8 File Offset: 0x000046F8
		public int LowAvailableSystemWorkingSetMemoryRatio
		{
			get
			{
				return this._LowAvailableSystemWorkingSetMemoryRatio_MaterializedValue_;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00006500 File Offset: 0x00004700
		public long SearchMemoryModelBaseCost
		{
			get
			{
				return this._SearchMemoryModelBaseCost_MaterializedValue_;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00006508 File Offset: 0x00004708
		public long BaselineCostPerActiveItem
		{
			get
			{
				return this._BaselineCostPerActiveItem_MaterializedValue_;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00006510 File Offset: 0x00004710
		public long BaselineCostPerPassiveItem
		{
			get
			{
				return this._BaselineCostPerPassiveItem_MaterializedValue_;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00006518 File Offset: 0x00004718
		public long InstantSearchCostPerActiveItem
		{
			get
			{
				return this._InstantSearchCostPerActiveItem_MaterializedValue_;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00006520 File Offset: 0x00004720
		public long RefinersCostPerActiveItem
		{
			get
			{
				return this._RefinersCostPerActiveItem_MaterializedValue_;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00006528 File Offset: 0x00004728
		public bool DisableGracefulDegradationForInstantSearch
		{
			get
			{
				return this._DisableGracefulDegradationForInstantSearch_MaterializedValue_;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00006530 File Offset: 0x00004730
		public bool DisableGracefulDegradationForAutoSuspend
		{
			get
			{
				return this._DisableGracefulDegradationForAutoSuspend_MaterializedValue_;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00006538 File Offset: 0x00004738
		public int TimerForGracefulDegradation
		{
			get
			{
				return this._TimerForGracefulDegradation_MaterializedValue_;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00006540 File Offset: 0x00004740
		public long MemoryMeasureDrift
		{
			get
			{
				return this._MemoryMeasureDrift_MaterializedValue_;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00006548 File Offset: 0x00004748
		public long MaxRestoreAmount
		{
			get
			{
				return this._MaxRestoreAmount_MaterializedValue_;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00006550 File Offset: 0x00004750
		public bool ShouldConsiderSearchMemoryUsageBudget
		{
			get
			{
				return this._ShouldConsiderSearchMemoryUsageBudget_MaterializedValue_;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00006558 File Offset: 0x00004758
		public long SearchMemoryUsageBudget
		{
			get
			{
				return this._SearchMemoryUsageBudget_MaterializedValue_;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00006560 File Offset: 0x00004760
		public long SearchMemoryUsageBudgetFloatingAmount
		{
			get
			{
				return this._SearchMemoryUsageBudgetFloatingAmount_MaterializedValue_;
			}
		}

		// Token: 0x0400028F RID: 655
		internal string _Name_MaterializedValue_;

		// Token: 0x04000290 RID: 656
		internal float _MemoryUsageAdjustmentMultiplier_MaterializedValue_;

		// Token: 0x04000291 RID: 657
		internal int _SandboxMaxPoolSize_MaterializedValue_;

		// Token: 0x04000292 RID: 658
		internal int _LowAvailableSystemWorkingSetMemoryRatio_MaterializedValue_;

		// Token: 0x04000293 RID: 659
		internal long _SearchMemoryModelBaseCost_MaterializedValue_;

		// Token: 0x04000294 RID: 660
		internal long _BaselineCostPerActiveItem_MaterializedValue_;

		// Token: 0x04000295 RID: 661
		internal long _BaselineCostPerPassiveItem_MaterializedValue_;

		// Token: 0x04000296 RID: 662
		internal long _InstantSearchCostPerActiveItem_MaterializedValue_;

		// Token: 0x04000297 RID: 663
		internal long _RefinersCostPerActiveItem_MaterializedValue_;

		// Token: 0x04000298 RID: 664
		internal bool _DisableGracefulDegradationForInstantSearch_MaterializedValue_;

		// Token: 0x04000299 RID: 665
		internal bool _DisableGracefulDegradationForAutoSuspend_MaterializedValue_;

		// Token: 0x0400029A RID: 666
		internal int _TimerForGracefulDegradation_MaterializedValue_;

		// Token: 0x0400029B RID: 667
		internal long _MemoryMeasureDrift_MaterializedValue_;

		// Token: 0x0400029C RID: 668
		internal long _MaxRestoreAmount_MaterializedValue_;

		// Token: 0x0400029D RID: 669
		internal bool _ShouldConsiderSearchMemoryUsageBudget_MaterializedValue_;

		// Token: 0x0400029E RID: 670
		internal long _SearchMemoryUsageBudget_MaterializedValue_;

		// Token: 0x0400029F RID: 671
		internal long _SearchMemoryUsageBudgetFloatingAmount_MaterializedValue_;
	}
}

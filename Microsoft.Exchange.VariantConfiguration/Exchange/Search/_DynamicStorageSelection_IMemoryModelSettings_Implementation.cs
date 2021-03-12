using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Search.Platform.Parallax.Core.Model;

namespace Microsoft.Exchange.Search
{
	// Token: 0x02000087 RID: 135
	[EditorBrowsable(EditorBrowsableState.Never)]
	[GeneratedCode("microsoft.search.platform.parallax.tools.codegenerator.exe", "1.0.0.0")]
	internal sealed class _DynamicStorageSelection_IMemoryModelSettings_Implementation_ : IMemoryModelSettings, ISettings, IDataAccessorBackedObject<_DynamicStorageSelection_IMemoryModelSettings_DataAccessor_>, IVariantObjectInstance
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00006195 File Offset: 0x00004395
		VariantContextSnapshot IVariantObjectInstance.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000619D File Offset: 0x0000439D
		_DynamicStorageSelection_IMemoryModelSettings_DataAccessor_ IDataAccessorBackedObject<_DynamicStorageSelection_IMemoryModelSettings_DataAccessor_>.DataAccessor
		{
			get
			{
				return this.dataAccessor;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000061A5 File Offset: 0x000043A5
		void IDataAccessorBackedObject<_DynamicStorageSelection_IMemoryModelSettings_DataAccessor_>.Initialize(_DynamicStorageSelection_IMemoryModelSettings_DataAccessor_ dataAccessor, VariantContextSnapshot context)
		{
			this.dataAccessor = dataAccessor;
			this.context = context;
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000061B5 File Offset: 0x000043B5
		public string Name
		{
			get
			{
				return this.dataAccessor._Name_MaterializedValue_;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000347 RID: 839 RVA: 0x000061C2 File Offset: 0x000043C2
		public float MemoryUsageAdjustmentMultiplier
		{
			get
			{
				if (this.dataAccessor._MemoryUsageAdjustmentMultiplier_ValueProvider_ != null)
				{
					return this.dataAccessor._MemoryUsageAdjustmentMultiplier_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MemoryUsageAdjustmentMultiplier_MaterializedValue_;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000061F3 File Offset: 0x000043F3
		public int SandboxMaxPoolSize
		{
			get
			{
				if (this.dataAccessor._SandboxMaxPoolSize_ValueProvider_ != null)
				{
					return this.dataAccessor._SandboxMaxPoolSize_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._SandboxMaxPoolSize_MaterializedValue_;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00006224 File Offset: 0x00004424
		public int LowAvailableSystemWorkingSetMemoryRatio
		{
			get
			{
				if (this.dataAccessor._LowAvailableSystemWorkingSetMemoryRatio_ValueProvider_ != null)
				{
					return this.dataAccessor._LowAvailableSystemWorkingSetMemoryRatio_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._LowAvailableSystemWorkingSetMemoryRatio_MaterializedValue_;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00006255 File Offset: 0x00004455
		public long SearchMemoryModelBaseCost
		{
			get
			{
				if (this.dataAccessor._SearchMemoryModelBaseCost_ValueProvider_ != null)
				{
					return this.dataAccessor._SearchMemoryModelBaseCost_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._SearchMemoryModelBaseCost_MaterializedValue_;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00006286 File Offset: 0x00004486
		public long BaselineCostPerActiveItem
		{
			get
			{
				if (this.dataAccessor._BaselineCostPerActiveItem_ValueProvider_ != null)
				{
					return this.dataAccessor._BaselineCostPerActiveItem_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._BaselineCostPerActiveItem_MaterializedValue_;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000062B7 File Offset: 0x000044B7
		public long BaselineCostPerPassiveItem
		{
			get
			{
				if (this.dataAccessor._BaselineCostPerPassiveItem_ValueProvider_ != null)
				{
					return this.dataAccessor._BaselineCostPerPassiveItem_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._BaselineCostPerPassiveItem_MaterializedValue_;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000062E8 File Offset: 0x000044E8
		public long InstantSearchCostPerActiveItem
		{
			get
			{
				if (this.dataAccessor._InstantSearchCostPerActiveItem_ValueProvider_ != null)
				{
					return this.dataAccessor._InstantSearchCostPerActiveItem_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._InstantSearchCostPerActiveItem_MaterializedValue_;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00006319 File Offset: 0x00004519
		public long RefinersCostPerActiveItem
		{
			get
			{
				if (this.dataAccessor._RefinersCostPerActiveItem_ValueProvider_ != null)
				{
					return this.dataAccessor._RefinersCostPerActiveItem_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._RefinersCostPerActiveItem_MaterializedValue_;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000634A File Offset: 0x0000454A
		public bool DisableGracefulDegradationForInstantSearch
		{
			get
			{
				if (this.dataAccessor._DisableGracefulDegradationForInstantSearch_ValueProvider_ != null)
				{
					return this.dataAccessor._DisableGracefulDegradationForInstantSearch_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DisableGracefulDegradationForInstantSearch_MaterializedValue_;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000637B File Offset: 0x0000457B
		public bool DisableGracefulDegradationForAutoSuspend
		{
			get
			{
				if (this.dataAccessor._DisableGracefulDegradationForAutoSuspend_ValueProvider_ != null)
				{
					return this.dataAccessor._DisableGracefulDegradationForAutoSuspend_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._DisableGracefulDegradationForAutoSuspend_MaterializedValue_;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000351 RID: 849 RVA: 0x000063AC File Offset: 0x000045AC
		public int TimerForGracefulDegradation
		{
			get
			{
				if (this.dataAccessor._TimerForGracefulDegradation_ValueProvider_ != null)
				{
					return this.dataAccessor._TimerForGracefulDegradation_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._TimerForGracefulDegradation_MaterializedValue_;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000352 RID: 850 RVA: 0x000063DD File Offset: 0x000045DD
		public long MemoryMeasureDrift
		{
			get
			{
				if (this.dataAccessor._MemoryMeasureDrift_ValueProvider_ != null)
				{
					return this.dataAccessor._MemoryMeasureDrift_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MemoryMeasureDrift_MaterializedValue_;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000640E File Offset: 0x0000460E
		public long MaxRestoreAmount
		{
			get
			{
				if (this.dataAccessor._MaxRestoreAmount_ValueProvider_ != null)
				{
					return this.dataAccessor._MaxRestoreAmount_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._MaxRestoreAmount_MaterializedValue_;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000643F File Offset: 0x0000463F
		public bool ShouldConsiderSearchMemoryUsageBudget
		{
			get
			{
				if (this.dataAccessor._ShouldConsiderSearchMemoryUsageBudget_ValueProvider_ != null)
				{
					return this.dataAccessor._ShouldConsiderSearchMemoryUsageBudget_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._ShouldConsiderSearchMemoryUsageBudget_MaterializedValue_;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00006470 File Offset: 0x00004670
		public long SearchMemoryUsageBudget
		{
			get
			{
				if (this.dataAccessor._SearchMemoryUsageBudget_ValueProvider_ != null)
				{
					return this.dataAccessor._SearchMemoryUsageBudget_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._SearchMemoryUsageBudget_MaterializedValue_;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000064A1 File Offset: 0x000046A1
		public long SearchMemoryUsageBudgetFloatingAmount
		{
			get
			{
				if (this.dataAccessor._SearchMemoryUsageBudgetFloatingAmount_ValueProvider_ != null)
				{
					return this.dataAccessor._SearchMemoryUsageBudgetFloatingAmount_ValueProvider_.GetValue(this.context);
				}
				return this.dataAccessor._SearchMemoryUsageBudgetFloatingAmount_MaterializedValue_;
			}
		}

		// Token: 0x0400028D RID: 653
		private _DynamicStorageSelection_IMemoryModelSettings_DataAccessor_ dataAccessor;

		// Token: 0x0400028E RID: 654
		private VariantContextSnapshot context;
	}
}

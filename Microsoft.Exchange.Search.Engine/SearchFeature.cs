using System;
using System.Collections.Generic;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x0200000C RID: 12
	internal class SearchFeature
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00003087 File Offset: 0x00001287
		internal SearchFeature(SearchFeature.SearchFeatureType searchFeatureType, ISearchServiceConfig config, MdbInfo mdbInfo)
		{
			this.searchFeatureType = searchFeatureType;
			this.config = config;
			this.mdbInfo = mdbInfo;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000030A4 File Offset: 0x000012A4
		public static IReadOnlyList<SearchFeature.SearchFeatureType> SearchFeatureTypeList
		{
			get
			{
				return SearchFeature.searchFeatureTypeList;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000030AC File Offset: 0x000012AC
		internal bool IsFlipAllowed()
		{
			switch (this.searchFeatureType)
			{
			case SearchFeature.SearchFeatureType.InstantSearch:
				return !this.config.DisableGracefulDegradationForInstantSearch && this.mdbInfo.MountedOnLocalServer;
			case SearchFeature.SearchFeatureType.PassiveCatalog:
				return !this.config.DisableGracefulDegradationForAutoSuspend && (!this.mdbInfo.MountedOnLocalServer && !this.mdbInfo.IsSuspended) && this.mdbInfo.NumberOfCopies >= 3;
			default:
				throw new InvalidOperationException("searchFeatureType");
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003134 File Offset: 0x00001334
		internal long GetPotentialMemoryChange()
		{
			switch (this.searchFeatureType)
			{
			case SearchFeature.SearchFeatureType.InstantSearch:
				return (long)((float)(this.config.InstantSearchCostPerActiveItem * this.mdbInfo.NumberOfItems * (this.mdbInfo.IsInstantSearchEnabled ? -1L : 1L)) * SearchMemoryModel.MemoryUsageAdjustmentMultiplier);
			case SearchFeature.SearchFeatureType.PassiveCatalog:
				return (long)((float)(this.config.BaselineCostPerPassiveItem * this.mdbInfo.NumberOfItems * (this.mdbInfo.IsCatalogSuspended ? 1L : -1L)) * SearchMemoryModel.MemoryUsageAdjustmentMultiplier);
			default:
				throw new InvalidOperationException("searchFeatureType");
			}
		}

		// Token: 0x04000026 RID: 38
		private static readonly IReadOnlyList<SearchFeature.SearchFeatureType> searchFeatureTypeList = (IReadOnlyList<SearchFeature.SearchFeatureType>)Enum.GetValues(typeof(SearchFeature.SearchFeatureType));

		// Token: 0x04000027 RID: 39
		private SearchFeature.SearchFeatureType searchFeatureType;

		// Token: 0x04000028 RID: 40
		private ISearchServiceConfig config;

		// Token: 0x04000029 RID: 41
		private MdbInfo mdbInfo;

		// Token: 0x0200000D RID: 13
		internal enum SearchFeatureType
		{
			// Token: 0x0400002B RID: 43
			InstantSearch,
			// Token: 0x0400002C RID: 44
			PassiveCatalog
		}
	}
}

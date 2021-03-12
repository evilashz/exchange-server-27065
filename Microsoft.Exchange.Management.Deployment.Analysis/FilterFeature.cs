using System;

namespace Microsoft.Exchange.Management.Deployment.Analysis
{
	// Token: 0x0200001D RID: 29
	public sealed class FilterFeature : Feature
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00004856 File Offset: 0x00002A56
		public FilterFeature(Func<FeatureSet, Result, bool> filterFunc)
		{
			this.filterFunc = filterFunc;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004865 File Offset: 0x00002A65
		public Func<FeatureSet, Result, bool> FilterFunc
		{
			get
			{
				return this.filterFunc;
			}
		}

		// Token: 0x04000052 RID: 82
		private readonly Func<FeatureSet, Result, bool> filterFunc;
	}
}

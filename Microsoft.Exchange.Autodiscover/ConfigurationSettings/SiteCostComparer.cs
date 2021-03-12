using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x02000042 RID: 66
	internal class SiteCostComparer<T> : IComparer<T> where T : Service
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x000089E4 File Offset: 0x00006BE4
		public SiteCostComparer(ServiceTopology serviceTopology, Site source)
		{
			this.serviceTopology = serviceTopology;
			this.mbxServerSite = source;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000089FC File Offset: 0x00006BFC
		public int Compare(T x, T y)
		{
			if (this.mbxServerSite == null)
			{
				return 0;
			}
			int maxValue;
			if (!this.serviceTopology.TryGetConnectionCost(this.mbxServerSite, x.Site, out maxValue, "f:\\15.00.1497\\sources\\dev\\autodisc\\src\\ConfigurationSettings\\SiteCostComparer.cs", "Compare", 68))
			{
				maxValue = int.MaxValue;
			}
			int maxValue2;
			if (!this.serviceTopology.TryGetConnectionCost(this.mbxServerSite, y.Site, out maxValue2, "f:\\15.00.1497\\sources\\dev\\autodisc\\src\\ConfigurationSettings\\SiteCostComparer.cs", "Compare", 75))
			{
				maxValue2 = int.MaxValue;
			}
			if (maxValue < maxValue2)
			{
				return -1;
			}
			if (maxValue > maxValue2)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x040001A9 RID: 425
		private ServiceTopology serviceTopology;

		// Token: 0x040001AA RID: 426
		private Site mbxServerSite;
	}
}

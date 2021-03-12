using System;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x0200000E RID: 14
	internal class ForestRegionTuple
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000029E0 File Offset: 0x00000BE0
		public ForestRegionTuple(string forestFqdn)
		{
			this.ForestFqdn = forestFqdn;
			this.Region = ForestRegionTuple.GetRegionFromForestFqdn(forestFqdn);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000029FB File Offset: 0x00000BFB
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002A03 File Offset: 0x00000C03
		public string ForestFqdn { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002A0C File Offset: 0x00000C0C
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002A14 File Offset: 0x00000C14
		public string Region { get; private set; }

		// Token: 0x06000043 RID: 67 RVA: 0x00002A1D File Offset: 0x00000C1D
		internal static string GetRegionFromForestFqdn(string forestFqdn)
		{
			if (forestFqdn.Equals("prod.exchangelabs.com", StringComparison.OrdinalIgnoreCase))
			{
				return "nam";
			}
			return forestFqdn.Substring(0, 3).ToLower();
		}
	}
}

using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000AC RID: 172
	public class WstDataProviderSettings
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001305D File Offset: 0x0001125D
		public static WstDataProviderSettings Default
		{
			get
			{
				return WstDataProviderSettings.defaultInstance;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00013064 File Offset: 0x00011264
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0001306C File Offset: 0x0001126C
		public bool IgnoreCorruptQueryResults { get; set; }

		// Token: 0x04000380 RID: 896
		private static WstDataProviderSettings defaultInstance = new WstDataProviderSettings();
	}
}

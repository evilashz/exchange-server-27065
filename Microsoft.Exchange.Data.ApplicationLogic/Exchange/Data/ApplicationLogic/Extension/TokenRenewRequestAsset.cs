using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x0200012D RID: 301
	internal sealed class TokenRenewRequestAsset
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x000328FC File Offset: 0x00030AFC
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00032904 File Offset: 0x00030B04
		public string MarketplaceContentMarket { get; set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0003290D File Offset: 0x00030B0D
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x00032915 File Offset: 0x00030B15
		public string ExtensionID { get; set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0003291E File Offset: 0x00030B1E
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x00032926 File Offset: 0x00030B26
		public string MarketplaceAssetID { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0003292F File Offset: 0x00030B2F
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x00032937 File Offset: 0x00030B37
		public Version Version { get; set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00032940 File Offset: 0x00030B40
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x00032948 File Offset: 0x00030B48
		public ExtensionInstallScope Scope { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x00032951 File Offset: 0x00030B51
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x00032959 File Offset: 0x00030B59
		public string Etoken { get; set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00032962 File Offset: 0x00030B62
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x0003296A File Offset: 0x00030B6A
		public bool IsResponseFound { get; set; }
	}
}

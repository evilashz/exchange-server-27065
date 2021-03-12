using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000126 RID: 294
	internal interface IDownloadAppRequestAsset
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C0C RID: 3084
		// (set) Token: 0x06000C0D RID: 3085
		string MarketplaceAssetID { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C0E RID: 3086
		// (set) Token: 0x06000C0F RID: 3087
		string MarketplaceContentMarket { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C10 RID: 3088
		// (set) Token: 0x06000C11 RID: 3089
		DisableReasonType DisableReason { get; set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C12 RID: 3090
		// (set) Token: 0x06000C13 RID: 3091
		bool Enabled { get; set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C14 RID: 3092
		// (set) Token: 0x06000C15 RID: 3093
		ExtensionInstallScope Scope { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C16 RID: 3094
		// (set) Token: 0x06000C17 RID: 3095
		string Etoken { get; set; }
	}
}

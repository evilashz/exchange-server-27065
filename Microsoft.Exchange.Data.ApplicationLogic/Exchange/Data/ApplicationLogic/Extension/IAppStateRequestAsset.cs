using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000125 RID: 293
	internal interface IAppStateRequestAsset
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C08 RID: 3080
		// (set) Token: 0x06000C09 RID: 3081
		string MarketplaceAssetID { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000C0A RID: 3082
		// (set) Token: 0x06000C0B RID: 3083
		string MarketplaceContentMarket { get; set; }
	}
}

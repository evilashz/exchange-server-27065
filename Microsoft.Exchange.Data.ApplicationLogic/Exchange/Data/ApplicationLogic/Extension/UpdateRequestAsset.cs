using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x0200012F RID: 303
	internal sealed class UpdateRequestAsset : IAppStateRequestAsset, IDownloadAppRequestAsset
	{
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00032994 File Offset: 0x00030B94
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x0003299C File Offset: 0x00030B9C
		public string MarketplaceContentMarket { get; set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x000329A5 File Offset: 0x00030BA5
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x000329AD File Offset: 0x00030BAD
		public string ExtensionID { get; set; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x000329B6 File Offset: 0x00030BB6
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x000329BE File Offset: 0x00030BBE
		public string MarketplaceAssetID { get; set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x000329C7 File Offset: 0x00030BC7
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x000329CF File Offset: 0x00030BCF
		public Version Version { get; set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x000329D8 File Offset: 0x00030BD8
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x000329E0 File Offset: 0x00030BE0
		public RequestedCapabilities RequestedCapabilities { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x000329E9 File Offset: 0x00030BE9
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x000329F1 File Offset: 0x00030BF1
		public DisableReasonType DisableReason { get; set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x000329FA File Offset: 0x00030BFA
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x00032A02 File Offset: 0x00030C02
		public bool Enabled { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00032A0B File Offset: 0x00030C0B
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x00032A13 File Offset: 0x00030C13
		public ExtensionInstallScope Scope { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00032A1C File Offset: 0x00030C1C
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00032A24 File Offset: 0x00030C24
		public OmexConstants.AppState State { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00032A2D File Offset: 0x00030C2D
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00032A35 File Offset: 0x00030C35
		public string Etoken { get; set; }
	}
}

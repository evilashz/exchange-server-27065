using System;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x0200001E RID: 30
	internal static class LiveAssetKeys
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00007C5A File Offset: 0x00005E5A
		public static string GetAssetKeyString(LiveAssetKey assetKey)
		{
			return LiveAssetKeys.liveAssetKeyMap[(int)assetKey];
		}

		// Token: 0x0400025F RID: 607
		private static readonly string[] liveAssetKeyMap = new string[]
		{
			"Live.Shared.MarketInfo.Header.HideTabs",
			"Live.Shared.MarketInfo.Header.Tabs",
			"Live.Shared.GlobalSettings.Items.Cobrand.Jewel.Header",
			"Live.Shared.GlobalSettings.Header.Cobrand.OpenLinksInNewWindow"
		};
	}
}

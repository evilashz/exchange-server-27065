using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000109 RID: 265
	public class KilledExtensionEntry
	{
		// Token: 0x06000B42 RID: 2882 RVA: 0x0002D750 File Offset: 0x0002B950
		public KilledExtensionEntry(string extensionId, string assetId)
		{
			if (string.IsNullOrWhiteSpace(extensionId))
			{
				throw new ArgumentException("The extension id is missing.");
			}
			if (string.IsNullOrWhiteSpace(assetId))
			{
				throw new ArgumentException("The asset id is missing.");
			}
			this.ExtensionId = ExtensionDataHelper.FormatExtensionId(extensionId);
			this.AssetId = assetId;
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0002D79C File Offset: 0x0002B99C
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0002D7A4 File Offset: 0x0002B9A4
		public string ExtensionId { get; private set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0002D7AD File Offset: 0x0002B9AD
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x0002D7B5 File Offset: 0x0002B9B5
		public string AssetId { get; private set; }
	}
}

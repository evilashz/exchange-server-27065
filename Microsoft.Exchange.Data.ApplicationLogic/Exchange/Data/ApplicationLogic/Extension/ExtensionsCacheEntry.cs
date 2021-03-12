using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x020000FC RID: 252
	internal sealed class ExtensionsCacheEntry
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00029ED0 File Offset: 0x000280D0
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00029ED8 File Offset: 0x000280D8
		public string ExtensionID { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x00029EE1 File Offset: 0x000280E1
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x00029EE9 File Offset: 0x000280E9
		public string MarketplaceAssetID { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00029EF2 File Offset: 0x000280F2
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x00029EFA File Offset: 0x000280FA
		public Version Version { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00029F03 File Offset: 0x00028103
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x00029F0B File Offset: 0x0002810B
		public RequestedCapabilities? RequestedCapabilities { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x00029F14 File Offset: 0x00028114
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x00029F1C File Offset: 0x0002811C
		public OmexConstants.AppState State { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00029F25 File Offset: 0x00028125
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x00029F2D File Offset: 0x0002812D
		public byte[] Manifest { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00029F36 File Offset: 0x00028136
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x00029F3E File Offset: 0x0002813E
		public DateTime LastUpdateCheckTime { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00029F47 File Offset: 0x00028147
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x00029F4F File Offset: 0x0002814F
		public int Size { get; set; }

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00029F58 File Offset: 0x00028158
		public ExtensionsCacheEntry(string marketplaceAssetID, string extensionID, Version version, RequestedCapabilities? requestedCapabilities, OmexConstants.AppState state, byte[] manifest)
		{
			if (string.IsNullOrEmpty(marketplaceAssetID))
			{
				throw new ArgumentNullException("marketPlaceAssetID");
			}
			if (string.IsNullOrEmpty(extensionID))
			{
				throw new ArgumentNullException("extensionID");
			}
			this.MarketplaceAssetID = marketplaceAssetID;
			this.ExtensionID = extensionID;
			this.Version = version;
			this.RequestedCapabilities = requestedCapabilities;
			this.State = state;
			this.Manifest = manifest;
			this.SetLastUpdateCheckTime();
			this.SetSize();
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00029FCC File Offset: 0x000281CC
		private void SetSize()
		{
			int num = (this.Manifest != null) ? this.Manifest.Length : 0;
			int num2 = (this.ExtensionID != null) ? (this.ExtensionID.Length * 2) : 0;
			int num3 = 16;
			int num4 = (this.MarketplaceAssetID != null) ? (this.MarketplaceAssetID.Length * 2) : 0;
			this.Size = 8 + num + 4 + 4 + 4 + num3 + num2 + num4;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002A037 File Offset: 0x00028237
		public void SetLastUpdateCheckTime()
		{
			this.LastUpdateCheckTime = DateTime.UtcNow;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002A044 File Offset: 0x00028244
		public static byte[] ConvertManifestStringToBytes(string manifestString)
		{
			if (string.IsNullOrEmpty(manifestString))
			{
				throw new ArgumentNullException("manifestString");
			}
			return Encoding.UTF8.GetBytes(manifestString);
		}

		// Token: 0x04000540 RID: 1344
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;
	}
}

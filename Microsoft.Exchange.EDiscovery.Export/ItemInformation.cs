using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000014 RID: 20
	internal class ItemInformation
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000031BB File Offset: 0x000013BB
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000031C3 File Offset: 0x000013C3
		public ItemId Id { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000031CC File Offset: 0x000013CC
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000031D4 File Offset: 0x000013D4
		public byte[] Data { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000031DD File Offset: 0x000013DD
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000031E5 File Offset: 0x000013E5
		public ExportException Error { get; set; }
	}
}

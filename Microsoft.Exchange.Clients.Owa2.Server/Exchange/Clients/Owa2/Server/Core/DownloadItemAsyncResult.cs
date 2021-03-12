using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000045 RID: 69
	public class DownloadItemAsyncResult
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00007AD2 File Offset: 0x00005CD2
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00007ADA File Offset: 0x00005CDA
		public AttachmentResultCode ResultCode { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00007AE3 File Offset: 0x00005CE3
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00007AEB File Offset: 0x00005CEB
		public FileAttachmentDataProviderItem Item { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00007AF4 File Offset: 0x00005CF4
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00007AFC File Offset: 0x00005CFC
		public byte[] Bytes { get; set; }
	}
}

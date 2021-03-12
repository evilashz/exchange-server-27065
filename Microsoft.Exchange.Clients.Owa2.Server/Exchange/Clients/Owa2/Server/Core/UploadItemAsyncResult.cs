using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200004D RID: 77
	public class UploadItemAsyncResult
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00008836 File Offset: 0x00006A36
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000883E File Offset: 0x00006A3E
		public AttachmentResultCode ResultCode { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00008847 File Offset: 0x00006A47
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000884F File Offset: 0x00006A4F
		public FileAttachmentDataProviderItem Item { get; set; }
	}
}

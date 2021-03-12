using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001A6 RID: 422
	internal struct SyncEmailContext
	{
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00025474 File Offset: 0x00023674
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x0002547C File Offset: 0x0002367C
		public bool? IsRead { get; set; }

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00025485 File Offset: 0x00023685
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x0002548D File Offset: 0x0002368D
		public bool? IsDraft { get; set; }

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00025496 File Offset: 0x00023696
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x0002549E File Offset: 0x0002369E
		public SyncMessageResponseType? ResponseType { get; set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x000254A7 File Offset: 0x000236A7
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x000254AF File Offset: 0x000236AF
		public string SyncMessageId { get; set; }
	}
}

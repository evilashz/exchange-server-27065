using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002ED RID: 749
	internal class PagePatchReply : IPagePatchReply
	{
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x00089A4F File Offset: 0x00087C4F
		// (set) Token: 0x06001E18 RID: 7704 RVA: 0x00089A57 File Offset: 0x00087C57
		public int PageNumber { get; set; }

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x00089A60 File Offset: 0x00087C60
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x00089A68 File Offset: 0x00087C68
		public byte[] Token { get; set; }

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x00089A71 File Offset: 0x00087C71
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x00089A79 File Offset: 0x00087C79
		public byte[] Data { get; set; }
	}
}

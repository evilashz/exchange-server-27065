using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000034 RID: 52
	internal class AttachmentPolicyInfo
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00004D2D File Offset: 0x00002F2D
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00004D35 File Offset: 0x00002F35
		public AttachmentPolicyLevel Level { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004D3E File Offset: 0x00002F3E
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00004D46 File Offset: 0x00002F46
		public bool IsViewableInBrowser { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004D4F File Offset: 0x00002F4F
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00004D57 File Offset: 0x00002F57
		public bool ForceBrowserViewingFirst { get; set; }
	}
}

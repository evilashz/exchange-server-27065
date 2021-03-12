using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000F RID: 15
	internal struct EasSyncResult
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006AE7 File Offset: 0x00004CE7
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00006AEF File Offset: 0x00004CEF
		public List<MessageRec> MessageRecs { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006AF8 File Offset: 0x00004CF8
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00006B00 File Offset: 0x00004D00
		public string SyncKeyRequested { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006B09 File Offset: 0x00004D09
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00006B11 File Offset: 0x00004D11
		public string NewSyncKey { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006B1A File Offset: 0x00004D1A
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00006B22 File Offset: 0x00004D22
		public bool HasMoreAvailable { get; set; }
	}
}

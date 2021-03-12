using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000E RID: 14
	internal struct EasSyncOptions
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006AB4 File Offset: 0x00004CB4
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00006ABC File Offset: 0x00004CBC
		public string SyncKey { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006AC5 File Offset: 0x00004CC5
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00006ACD File Offset: 0x00004CCD
		public bool RecentOnly { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006AD6 File Offset: 0x00004CD6
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00006ADE File Offset: 0x00004CDE
		public int MaxNumberOfMessage { get; set; }
	}
}

using System;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch
{
	// Token: 0x02000005 RID: 5
	public class MailboxConnectivity : HealthHandlerResult
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002716 File Offset: 0x00000916
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000271E File Offset: 0x0000091E
		public string TenantAcceptedDomain { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002727 File Offset: 0x00000927
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000272F File Offset: 0x0000092F
		public Guid ExchangeUserId { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002738 File Offset: 0x00000938
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002740 File Offset: 0x00000940
		public bool Success { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002749 File Offset: 0x00000949
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002751 File Offset: 0x00000951
		public string Exception { get; set; }
	}
}

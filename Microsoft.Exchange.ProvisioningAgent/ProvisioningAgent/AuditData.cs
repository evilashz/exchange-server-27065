using System;
using Microsoft.Exchange.Data.Storage.Auditing;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200000D RID: 13
	internal class AuditData
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000047E7 File Offset: 0x000029E7
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000047EF File Offset: 0x000029EF
		public IAuditLogRecord AuditRecord { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000047F8 File Offset: 0x000029F8
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00004800 File Offset: 0x00002A00
		public IAuditLog AuditLogger { get; set; }
	}
}

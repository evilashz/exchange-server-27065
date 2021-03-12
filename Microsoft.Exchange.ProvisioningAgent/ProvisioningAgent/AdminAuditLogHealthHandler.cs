using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000003 RID: 3
	internal class AdminAuditLogHealthHandler : ExchangeDiagnosableWrapper<AdminAuditLogHealth>
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000239C File Offset: 0x0000059C
		private AdminAuditLogHealthHandler()
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023B0 File Offset: 0x000005B0
		public static AdminAuditLogHealthHandler GetInstance()
		{
			return AdminAuditLogHealthHandler.instance.Value;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000023BC File Offset: 0x000005BC
		internal AdminAuditLogHealth Health
		{
			get
			{
				return this.health.Value;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023C9 File Offset: 0x000005C9
		internal override AdminAuditLogHealth GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments)
		{
			return this.Health;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000023D1 File Offset: 0x000005D1
		protected override string ComponentName
		{
			get
			{
				return "AdminAuditLogHealth";
			}
		}

		// Token: 0x04000012 RID: 18
		private static readonly Lazy<AdminAuditLogHealthHandler> instance = new Lazy<AdminAuditLogHealthHandler>(() => new AdminAuditLogHealthHandler(), true);

		// Token: 0x04000013 RID: 19
		private readonly Lazy<AdminAuditLogHealth> health = new Lazy<AdminAuditLogHealth>(true);
	}
}

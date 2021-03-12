using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200019E RID: 414
	[Serializable]
	public class AdminAuditLogIdParameter : ADIdParameter
	{
		// Token: 0x06000EEC RID: 3820 RVA: 0x0002B69A File Offset: 0x0002989A
		public AdminAuditLogIdParameter()
		{
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0002B6A2 File Offset: 0x000298A2
		public AdminAuditLogIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002B6AB File Offset: 0x000298AB
		public AdminAuditLogIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0002B6B4 File Offset: 0x000298B4
		public AdminAuditLogIdParameter(AdminAuditLogConfig adminAuditLogConfig) : base(adminAuditLogConfig.Id)
		{
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0002B6C2 File Offset: 0x000298C2
		public AdminAuditLogIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0002B6CB File Offset: 0x000298CB
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0002B6CE File Offset: 0x000298CE
		public static AdminAuditLogIdParameter Parse(string identity)
		{
			return new AdminAuditLogIdParameter(identity);
		}

		// Token: 0x0400031F RID: 799
		internal const string FixedValue = "Admin Audit Log Settings";
	}
}

using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000052 RID: 82
	[Cmdlet("Get", "AdminAuditLogConfig")]
	public sealed class GetAdminAuditLogConfig : GetMultitenancySingletonSystemConfigurationObjectTask<AdminAuditLogConfig>
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000090E8 File Offset: 0x000072E8
		protected override ObjectId RootId
		{
			get
			{
				ADObjectId orgContainerId = base.CurrentOrgContainerId;
				if (base.SharedConfiguration != null)
				{
					orgContainerId = base.SharedConfiguration.SharedConfigurationCU.Id;
				}
				return AdminAuditLogConfig.GetWellKnownParentLocation(orgContainerId);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000911B File Offset: 0x0000731B
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Static;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000912D File Offset: 0x0000732D
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00009135 File Offset: 0x00007335
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }
	}
}

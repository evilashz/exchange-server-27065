using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000054 RID: 84
	[Cmdlet("Remove", "AdminAuditLogConfig", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveAdminAuditLogConfig : RemoveSystemConfigurationObjectTask<AdminAuditLogIdParameter, AdminAuditLogConfig>
	{
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00009204 File Offset: 0x00007404
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveAdminAuditLogConfig(base.CurrentOrgContainerId.ToString());
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00009216 File Offset: 0x00007416
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00009219 File Offset: 0x00007419
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000923E File Offset: 0x0000743E
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true, Position = 0)]
		public override AdminAuditLogIdParameter Identity
		{
			get
			{
				return ((AdminAuditLogIdParameter)base.Fields["Identity"]) ?? AdminAuditLogIdParameter.Parse("Admin Audit Log Settings");
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}
	}
}

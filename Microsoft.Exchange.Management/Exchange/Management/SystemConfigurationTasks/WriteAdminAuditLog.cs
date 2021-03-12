using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000058 RID: 88
	[Cmdlet("Write", "AdminAuditLog", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class WriteAdminAuditLog : GetMultitenancySingletonSystemConfigurationObjectTask<AdminAuditLogConfig>
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00009F0B File Offset: 0x0000810B
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00009F22 File Offset: 0x00008122
		[Parameter(Mandatory = true)]
		public string Comment
		{
			get
			{
				return (string)base.Fields["Comment"];
			}
			set
			{
				base.Fields["Comment"] = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00009F35 File Offset: 0x00008135
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddAdminAuditLog(base.CurrentOrgContainerId.ToString());
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00009F47 File Offset: 0x00008147
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009F4A File Offset: 0x0000814A
		protected override ObjectId RootId
		{
			get
			{
				return AdminAuditLogConfig.GetWellKnownParentLocation(base.CurrentOrgContainerId);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009F58 File Offset: 0x00008158
		protected override void InternalProvisioningValidation()
		{
			ProvisioningValidationError[] array = ProvisioningLayer.Validate(this, null);
			if (array != null && array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					ProvisioningValidationException exception = new ProvisioningValidationException(array[i].Description, array[i].AgentName, array[i].Exception);
					this.WriteError(exception, (ErrorCategory)array[i].ErrorCategory, null, array.Length - 1 == i);
				}
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009FBF File Offset: 0x000081BF
		protected override void InternalProcessRecord()
		{
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009FC1 File Offset: 0x000081C1
		protected override IConfigDataProvider CreateSession()
		{
			return null;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009FC4 File Offset: 0x000081C4
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
		}
	}
}

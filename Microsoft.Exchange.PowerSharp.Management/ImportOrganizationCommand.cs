using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Deployment.XforestTenantMigration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BB4 RID: 2996
	public class ImportOrganizationCommand : SyntheticCommandWithPipelineInput<ExchangeConfigurationUnit, ExchangeConfigurationUnit>
	{
		// Token: 0x06009109 RID: 37129 RVA: 0x000D3FD0 File Offset: 0x000D21D0
		private ImportOrganizationCommand() : base("Import-Organization")
		{
		}

		// Token: 0x0600910A RID: 37130 RVA: 0x000D3FDD File Offset: 0x000D21DD
		public ImportOrganizationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600910B RID: 37131 RVA: 0x000D3FEC File Offset: 0x000D21EC
		public virtual ImportOrganizationCommand SetParameters(ImportOrganizationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BB5 RID: 2997
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700655A RID: 25946
			// (set) Token: 0x0600910C RID: 37132 RVA: 0x000D3FF6 File Offset: 0x000D21F6
			public virtual OrganizationData Data
			{
				set
				{
					base.PowerSharpParameters["Data"] = value;
				}
			}

			// Token: 0x1700655B RID: 25947
			// (set) Token: 0x0600910D RID: 37133 RVA: 0x000D4009 File Offset: 0x000D2209
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700655C RID: 25948
			// (set) Token: 0x0600910E RID: 37134 RVA: 0x000D401C File Offset: 0x000D221C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700655D RID: 25949
			// (set) Token: 0x0600910F RID: 37135 RVA: 0x000D4034 File Offset: 0x000D2234
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700655E RID: 25950
			// (set) Token: 0x06009110 RID: 37136 RVA: 0x000D404C File Offset: 0x000D224C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700655F RID: 25951
			// (set) Token: 0x06009111 RID: 37137 RVA: 0x000D4064 File Offset: 0x000D2264
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}

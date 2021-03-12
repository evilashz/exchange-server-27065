using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000358 RID: 856
	public class RemoveManagementRoleCommand : SyntheticCommandWithPipelineInput<ExchangeRole, ExchangeRole>
	{
		// Token: 0x060036F8 RID: 14072 RVA: 0x0005F2F2 File Offset: 0x0005D4F2
		private RemoveManagementRoleCommand() : base("Remove-ManagementRole")
		{
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x0005F2FF File Offset: 0x0005D4FF
		public RemoveManagementRoleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x0005F30E File Offset: 0x0005D50E
		public virtual RemoveManagementRoleCommand SetParameters(RemoveManagementRoleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x0005F318 File Offset: 0x0005D518
		public virtual RemoveManagementRoleCommand SetParameters(RemoveManagementRoleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000359 RID: 857
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001C01 RID: 7169
			// (set) Token: 0x060036FC RID: 14076 RVA: 0x0005F322 File Offset: 0x0005D522
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C02 RID: 7170
			// (set) Token: 0x060036FD RID: 14077 RVA: 0x0005F33A File Offset: 0x0005D53A
			public virtual SwitchParameter Recurse
			{
				set
				{
					base.PowerSharpParameters["Recurse"] = value;
				}
			}

			// Token: 0x17001C03 RID: 7171
			// (set) Token: 0x060036FE RID: 14078 RVA: 0x0005F352 File Offset: 0x0005D552
			public virtual SwitchParameter UnScopedTopLevel
			{
				set
				{
					base.PowerSharpParameters["UnScopedTopLevel"] = value;
				}
			}

			// Token: 0x17001C04 RID: 7172
			// (set) Token: 0x060036FF RID: 14079 RVA: 0x0005F36A File Offset: 0x0005D56A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C05 RID: 7173
			// (set) Token: 0x06003700 RID: 14080 RVA: 0x0005F37D File Offset: 0x0005D57D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C06 RID: 7174
			// (set) Token: 0x06003701 RID: 14081 RVA: 0x0005F395 File Offset: 0x0005D595
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C07 RID: 7175
			// (set) Token: 0x06003702 RID: 14082 RVA: 0x0005F3AD File Offset: 0x0005D5AD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C08 RID: 7176
			// (set) Token: 0x06003703 RID: 14083 RVA: 0x0005F3C5 File Offset: 0x0005D5C5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C09 RID: 7177
			// (set) Token: 0x06003704 RID: 14084 RVA: 0x0005F3DD File Offset: 0x0005D5DD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001C0A RID: 7178
			// (set) Token: 0x06003705 RID: 14085 RVA: 0x0005F3F5 File Offset: 0x0005D5F5
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200035A RID: 858
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001C0B RID: 7179
			// (set) Token: 0x06003707 RID: 14087 RVA: 0x0005F415 File Offset: 0x0005D615
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x17001C0C RID: 7180
			// (set) Token: 0x06003708 RID: 14088 RVA: 0x0005F433 File Offset: 0x0005D633
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C0D RID: 7181
			// (set) Token: 0x06003709 RID: 14089 RVA: 0x0005F44B File Offset: 0x0005D64B
			public virtual SwitchParameter Recurse
			{
				set
				{
					base.PowerSharpParameters["Recurse"] = value;
				}
			}

			// Token: 0x17001C0E RID: 7182
			// (set) Token: 0x0600370A RID: 14090 RVA: 0x0005F463 File Offset: 0x0005D663
			public virtual SwitchParameter UnScopedTopLevel
			{
				set
				{
					base.PowerSharpParameters["UnScopedTopLevel"] = value;
				}
			}

			// Token: 0x17001C0F RID: 7183
			// (set) Token: 0x0600370B RID: 14091 RVA: 0x0005F47B File Offset: 0x0005D67B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C10 RID: 7184
			// (set) Token: 0x0600370C RID: 14092 RVA: 0x0005F48E File Offset: 0x0005D68E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C11 RID: 7185
			// (set) Token: 0x0600370D RID: 14093 RVA: 0x0005F4A6 File Offset: 0x0005D6A6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C12 RID: 7186
			// (set) Token: 0x0600370E RID: 14094 RVA: 0x0005F4BE File Offset: 0x0005D6BE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C13 RID: 7187
			// (set) Token: 0x0600370F RID: 14095 RVA: 0x0005F4D6 File Offset: 0x0005D6D6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C14 RID: 7188
			// (set) Token: 0x06003710 RID: 14096 RVA: 0x0005F4EE File Offset: 0x0005D6EE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001C15 RID: 7189
			// (set) Token: 0x06003711 RID: 14097 RVA: 0x0005F506 File Offset: 0x0005D706
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}

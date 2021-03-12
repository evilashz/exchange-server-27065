using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000354 RID: 852
	public class NewManagementRoleCommand : SyntheticCommandWithPipelineInput<ExchangeRole, ExchangeRole>
	{
		// Token: 0x060036D0 RID: 14032 RVA: 0x0005EFB5 File Offset: 0x0005D1B5
		private NewManagementRoleCommand() : base("New-ManagementRole")
		{
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x0005EFC2 File Offset: 0x0005D1C2
		public NewManagementRoleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x0005EFD1 File Offset: 0x0005D1D1
		public virtual NewManagementRoleCommand SetParameters(NewManagementRoleCommand.NewDerivedRoleParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x0005EFDB File Offset: 0x0005D1DB
		public virtual NewManagementRoleCommand SetParameters(NewManagementRoleCommand.UnScopedTopLevelRoleParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x0005EFE5 File Offset: 0x0005D1E5
		public virtual NewManagementRoleCommand SetParameters(NewManagementRoleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000355 RID: 853
		public class NewDerivedRoleParameterSetParameters : ParametersBase
		{
			// Token: 0x17001BE1 RID: 7137
			// (set) Token: 0x060036D5 RID: 14037 RVA: 0x0005EFEF File Offset: 0x0005D1EF
			public virtual string Parent
			{
				set
				{
					base.PowerSharpParameters["Parent"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x17001BE2 RID: 7138
			// (set) Token: 0x060036D6 RID: 14038 RVA: 0x0005F00D File Offset: 0x0005D20D
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17001BE3 RID: 7139
			// (set) Token: 0x060036D7 RID: 14039 RVA: 0x0005F020 File Offset: 0x0005D220
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001BE4 RID: 7140
			// (set) Token: 0x060036D8 RID: 14040 RVA: 0x0005F038 File Offset: 0x0005D238
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001BE5 RID: 7141
			// (set) Token: 0x060036D9 RID: 14041 RVA: 0x0005F056 File Offset: 0x0005D256
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001BE6 RID: 7142
			// (set) Token: 0x060036DA RID: 14042 RVA: 0x0005F069 File Offset: 0x0005D269
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001BE7 RID: 7143
			// (set) Token: 0x060036DB RID: 14043 RVA: 0x0005F07C File Offset: 0x0005D27C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001BE8 RID: 7144
			// (set) Token: 0x060036DC RID: 14044 RVA: 0x0005F094 File Offset: 0x0005D294
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001BE9 RID: 7145
			// (set) Token: 0x060036DD RID: 14045 RVA: 0x0005F0AC File Offset: 0x0005D2AC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001BEA RID: 7146
			// (set) Token: 0x060036DE RID: 14046 RVA: 0x0005F0C4 File Offset: 0x0005D2C4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001BEB RID: 7147
			// (set) Token: 0x060036DF RID: 14047 RVA: 0x0005F0DC File Offset: 0x0005D2DC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000356 RID: 854
		public class UnScopedTopLevelRoleParameterSetParameters : ParametersBase
		{
			// Token: 0x17001BEC RID: 7148
			// (set) Token: 0x060036E1 RID: 14049 RVA: 0x0005F0FC File Offset: 0x0005D2FC
			public virtual SwitchParameter UnScopedTopLevel
			{
				set
				{
					base.PowerSharpParameters["UnScopedTopLevel"] = value;
				}
			}

			// Token: 0x17001BED RID: 7149
			// (set) Token: 0x060036E2 RID: 14050 RVA: 0x0005F114 File Offset: 0x0005D314
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17001BEE RID: 7150
			// (set) Token: 0x060036E3 RID: 14051 RVA: 0x0005F127 File Offset: 0x0005D327
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001BEF RID: 7151
			// (set) Token: 0x060036E4 RID: 14052 RVA: 0x0005F13F File Offset: 0x0005D33F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001BF0 RID: 7152
			// (set) Token: 0x060036E5 RID: 14053 RVA: 0x0005F15D File Offset: 0x0005D35D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001BF1 RID: 7153
			// (set) Token: 0x060036E6 RID: 14054 RVA: 0x0005F170 File Offset: 0x0005D370
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001BF2 RID: 7154
			// (set) Token: 0x060036E7 RID: 14055 RVA: 0x0005F183 File Offset: 0x0005D383
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001BF3 RID: 7155
			// (set) Token: 0x060036E8 RID: 14056 RVA: 0x0005F19B File Offset: 0x0005D39B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001BF4 RID: 7156
			// (set) Token: 0x060036E9 RID: 14057 RVA: 0x0005F1B3 File Offset: 0x0005D3B3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001BF5 RID: 7157
			// (set) Token: 0x060036EA RID: 14058 RVA: 0x0005F1CB File Offset: 0x0005D3CB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001BF6 RID: 7158
			// (set) Token: 0x060036EB RID: 14059 RVA: 0x0005F1E3 File Offset: 0x0005D3E3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000357 RID: 855
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001BF7 RID: 7159
			// (set) Token: 0x060036ED RID: 14061 RVA: 0x0005F203 File Offset: 0x0005D403
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17001BF8 RID: 7160
			// (set) Token: 0x060036EE RID: 14062 RVA: 0x0005F216 File Offset: 0x0005D416
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001BF9 RID: 7161
			// (set) Token: 0x060036EF RID: 14063 RVA: 0x0005F22E File Offset: 0x0005D42E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001BFA RID: 7162
			// (set) Token: 0x060036F0 RID: 14064 RVA: 0x0005F24C File Offset: 0x0005D44C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001BFB RID: 7163
			// (set) Token: 0x060036F1 RID: 14065 RVA: 0x0005F25F File Offset: 0x0005D45F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001BFC RID: 7164
			// (set) Token: 0x060036F2 RID: 14066 RVA: 0x0005F272 File Offset: 0x0005D472
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001BFD RID: 7165
			// (set) Token: 0x060036F3 RID: 14067 RVA: 0x0005F28A File Offset: 0x0005D48A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001BFE RID: 7166
			// (set) Token: 0x060036F4 RID: 14068 RVA: 0x0005F2A2 File Offset: 0x0005D4A2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001BFF RID: 7167
			// (set) Token: 0x060036F5 RID: 14069 RVA: 0x0005F2BA File Offset: 0x0005D4BA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C00 RID: 7168
			// (set) Token: 0x060036F6 RID: 14070 RVA: 0x0005F2D2 File Offset: 0x0005D4D2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}

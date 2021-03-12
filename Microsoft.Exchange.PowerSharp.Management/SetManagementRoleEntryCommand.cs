using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000364 RID: 868
	public class SetManagementRoleEntryCommand : SyntheticCommandWithPipelineInputNoOutput<RoleEntryIdParameter>
	{
		// Token: 0x0600375F RID: 14175 RVA: 0x0005FB42 File Offset: 0x0005DD42
		private SetManagementRoleEntryCommand() : base("Set-ManagementRoleEntry")
		{
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x0005FB4F File Offset: 0x0005DD4F
		public SetManagementRoleEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x0005FB5E File Offset: 0x0005DD5E
		public virtual SetManagementRoleEntryCommand SetParameters(SetManagementRoleEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x0005FB68 File Offset: 0x0005DD68
		public virtual SetManagementRoleEntryCommand SetParameters(SetManagementRoleEntryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000365 RID: 869
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001C50 RID: 7248
			// (set) Token: 0x06003763 RID: 14179 RVA: 0x0005FB72 File Offset: 0x0005DD72
			public virtual SwitchParameter UnScopedTopLevel
			{
				set
				{
					base.PowerSharpParameters["UnScopedTopLevel"] = value;
				}
			}

			// Token: 0x17001C51 RID: 7249
			// (set) Token: 0x06003764 RID: 14180 RVA: 0x0005FB8A File Offset: 0x0005DD8A
			public virtual string Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x17001C52 RID: 7250
			// (set) Token: 0x06003765 RID: 14181 RVA: 0x0005FB9D File Offset: 0x0005DD9D
			public virtual SwitchParameter AddParameter
			{
				set
				{
					base.PowerSharpParameters["AddParameter"] = value;
				}
			}

			// Token: 0x17001C53 RID: 7251
			// (set) Token: 0x06003766 RID: 14182 RVA: 0x0005FBB5 File Offset: 0x0005DDB5
			public virtual SwitchParameter RemoveParameter
			{
				set
				{
					base.PowerSharpParameters["RemoveParameter"] = value;
				}
			}

			// Token: 0x17001C54 RID: 7252
			// (set) Token: 0x06003767 RID: 14183 RVA: 0x0005FBCD File Offset: 0x0005DDCD
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C55 RID: 7253
			// (set) Token: 0x06003768 RID: 14184 RVA: 0x0005FBE5 File Offset: 0x0005DDE5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C56 RID: 7254
			// (set) Token: 0x06003769 RID: 14185 RVA: 0x0005FBF8 File Offset: 0x0005DDF8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C57 RID: 7255
			// (set) Token: 0x0600376A RID: 14186 RVA: 0x0005FC10 File Offset: 0x0005DE10
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C58 RID: 7256
			// (set) Token: 0x0600376B RID: 14187 RVA: 0x0005FC28 File Offset: 0x0005DE28
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C59 RID: 7257
			// (set) Token: 0x0600376C RID: 14188 RVA: 0x0005FC40 File Offset: 0x0005DE40
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C5A RID: 7258
			// (set) Token: 0x0600376D RID: 14189 RVA: 0x0005FC58 File Offset: 0x0005DE58
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000366 RID: 870
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001C5B RID: 7259
			// (set) Token: 0x0600376F RID: 14191 RVA: 0x0005FC78 File Offset: 0x0005DE78
			public virtual SwitchParameter SkipScriptExistenceCheck
			{
				set
				{
					base.PowerSharpParameters["SkipScriptExistenceCheck"] = value;
				}
			}

			// Token: 0x17001C5C RID: 7260
			// (set) Token: 0x06003770 RID: 14192 RVA: 0x0005FC90 File Offset: 0x0005DE90
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleEntryIdParameter(value) : null);
				}
			}

			// Token: 0x17001C5D RID: 7261
			// (set) Token: 0x06003771 RID: 14193 RVA: 0x0005FCAE File Offset: 0x0005DEAE
			public virtual SwitchParameter UnScopedTopLevel
			{
				set
				{
					base.PowerSharpParameters["UnScopedTopLevel"] = value;
				}
			}

			// Token: 0x17001C5E RID: 7262
			// (set) Token: 0x06003772 RID: 14194 RVA: 0x0005FCC6 File Offset: 0x0005DEC6
			public virtual string Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x17001C5F RID: 7263
			// (set) Token: 0x06003773 RID: 14195 RVA: 0x0005FCD9 File Offset: 0x0005DED9
			public virtual SwitchParameter AddParameter
			{
				set
				{
					base.PowerSharpParameters["AddParameter"] = value;
				}
			}

			// Token: 0x17001C60 RID: 7264
			// (set) Token: 0x06003774 RID: 14196 RVA: 0x0005FCF1 File Offset: 0x0005DEF1
			public virtual SwitchParameter RemoveParameter
			{
				set
				{
					base.PowerSharpParameters["RemoveParameter"] = value;
				}
			}

			// Token: 0x17001C61 RID: 7265
			// (set) Token: 0x06003775 RID: 14197 RVA: 0x0005FD09 File Offset: 0x0005DF09
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C62 RID: 7266
			// (set) Token: 0x06003776 RID: 14198 RVA: 0x0005FD21 File Offset: 0x0005DF21
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C63 RID: 7267
			// (set) Token: 0x06003777 RID: 14199 RVA: 0x0005FD34 File Offset: 0x0005DF34
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C64 RID: 7268
			// (set) Token: 0x06003778 RID: 14200 RVA: 0x0005FD4C File Offset: 0x0005DF4C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C65 RID: 7269
			// (set) Token: 0x06003779 RID: 14201 RVA: 0x0005FD64 File Offset: 0x0005DF64
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C66 RID: 7270
			// (set) Token: 0x0600377A RID: 14202 RVA: 0x0005FD7C File Offset: 0x0005DF7C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C67 RID: 7271
			// (set) Token: 0x0600377B RID: 14203 RVA: 0x0005FD94 File Offset: 0x0005DF94
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

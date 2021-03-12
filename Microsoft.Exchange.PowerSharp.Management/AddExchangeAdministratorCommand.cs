using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D1F RID: 3359
	public class AddExchangeAdministratorCommand : SyntheticCommandWithPipelineInputNoOutput<DelegateRoleType>
	{
		// Token: 0x0600B231 RID: 45617 RVA: 0x00100F29 File Offset: 0x000FF129
		private AddExchangeAdministratorCommand() : base("Add-ExchangeAdministrator")
		{
		}

		// Token: 0x0600B232 RID: 45618 RVA: 0x00100F36 File Offset: 0x000FF136
		public AddExchangeAdministratorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B233 RID: 45619 RVA: 0x00100F45 File Offset: 0x000FF145
		public virtual AddExchangeAdministratorCommand SetParameters(AddExchangeAdministratorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B234 RID: 45620 RVA: 0x00100F4F File Offset: 0x000FF14F
		public virtual AddExchangeAdministratorCommand SetParameters(AddExchangeAdministratorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D20 RID: 3360
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170083AC RID: 33708
			// (set) Token: 0x0600B235 RID: 45621 RVA: 0x00100F59 File Offset: 0x000FF159
			public virtual DelegateRoleType Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x170083AD RID: 33709
			// (set) Token: 0x0600B236 RID: 45622 RVA: 0x00100F71 File Offset: 0x000FF171
			public virtual string Scope
			{
				set
				{
					base.PowerSharpParameters["Scope"] = value;
				}
			}

			// Token: 0x170083AE RID: 33710
			// (set) Token: 0x0600B237 RID: 45623 RVA: 0x00100F84 File Offset: 0x000FF184
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170083AF RID: 33711
			// (set) Token: 0x0600B238 RID: 45624 RVA: 0x00100F97 File Offset: 0x000FF197
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170083B0 RID: 33712
			// (set) Token: 0x0600B239 RID: 45625 RVA: 0x00100FAF File Offset: 0x000FF1AF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170083B1 RID: 33713
			// (set) Token: 0x0600B23A RID: 45626 RVA: 0x00100FC7 File Offset: 0x000FF1C7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170083B2 RID: 33714
			// (set) Token: 0x0600B23B RID: 45627 RVA: 0x00100FDF File Offset: 0x000FF1DF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170083B3 RID: 33715
			// (set) Token: 0x0600B23C RID: 45628 RVA: 0x00100FF7 File Offset: 0x000FF1F7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D21 RID: 3361
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170083B4 RID: 33716
			// (set) Token: 0x0600B23E RID: 45630 RVA: 0x00101017 File Offset: 0x000FF217
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x170083B5 RID: 33717
			// (set) Token: 0x0600B23F RID: 45631 RVA: 0x00101035 File Offset: 0x000FF235
			public virtual DelegateRoleType Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = value;
				}
			}

			// Token: 0x170083B6 RID: 33718
			// (set) Token: 0x0600B240 RID: 45632 RVA: 0x0010104D File Offset: 0x000FF24D
			public virtual string Scope
			{
				set
				{
					base.PowerSharpParameters["Scope"] = value;
				}
			}

			// Token: 0x170083B7 RID: 33719
			// (set) Token: 0x0600B241 RID: 45633 RVA: 0x00101060 File Offset: 0x000FF260
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170083B8 RID: 33720
			// (set) Token: 0x0600B242 RID: 45634 RVA: 0x00101073 File Offset: 0x000FF273
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170083B9 RID: 33721
			// (set) Token: 0x0600B243 RID: 45635 RVA: 0x0010108B File Offset: 0x000FF28B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170083BA RID: 33722
			// (set) Token: 0x0600B244 RID: 45636 RVA: 0x001010A3 File Offset: 0x000FF2A3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170083BB RID: 33723
			// (set) Token: 0x0600B245 RID: 45637 RVA: 0x001010BB File Offset: 0x000FF2BB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170083BC RID: 33724
			// (set) Token: 0x0600B246 RID: 45638 RVA: 0x001010D3 File Offset: 0x000FF2D3
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

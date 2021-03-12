using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000524 RID: 1316
	public class GetClientAccessRuleCommand : SyntheticCommandWithPipelineInput<ADClientAccessRule, ADClientAccessRule>
	{
		// Token: 0x060046BD RID: 18109 RVA: 0x00073481 File Offset: 0x00071681
		private GetClientAccessRuleCommand() : base("Get-ClientAccessRule")
		{
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x0007348E File Offset: 0x0007168E
		public GetClientAccessRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x0007349D File Offset: 0x0007169D
		public virtual GetClientAccessRuleCommand SetParameters(GetClientAccessRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x000734A7 File Offset: 0x000716A7
		public virtual GetClientAccessRuleCommand SetParameters(GetClientAccessRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000525 RID: 1317
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700282E RID: 10286
			// (set) Token: 0x060046C1 RID: 18113 RVA: 0x000734B1 File Offset: 0x000716B1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700282F RID: 10287
			// (set) Token: 0x060046C2 RID: 18114 RVA: 0x000734CF File Offset: 0x000716CF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002830 RID: 10288
			// (set) Token: 0x060046C3 RID: 18115 RVA: 0x000734E2 File Offset: 0x000716E2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002831 RID: 10289
			// (set) Token: 0x060046C4 RID: 18116 RVA: 0x000734FA File Offset: 0x000716FA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002832 RID: 10290
			// (set) Token: 0x060046C5 RID: 18117 RVA: 0x00073512 File Offset: 0x00071712
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002833 RID: 10291
			// (set) Token: 0x060046C6 RID: 18118 RVA: 0x0007352A File Offset: 0x0007172A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002834 RID: 10292
			// (set) Token: 0x060046C7 RID: 18119 RVA: 0x00073542 File Offset: 0x00071742
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000526 RID: 1318
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002835 RID: 10293
			// (set) Token: 0x060046C9 RID: 18121 RVA: 0x00073562 File Offset: 0x00071762
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ClientAccessRuleIdParameter(value) : null);
				}
			}

			// Token: 0x17002836 RID: 10294
			// (set) Token: 0x060046CA RID: 18122 RVA: 0x00073580 File Offset: 0x00071780
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002837 RID: 10295
			// (set) Token: 0x060046CB RID: 18123 RVA: 0x0007359E File Offset: 0x0007179E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002838 RID: 10296
			// (set) Token: 0x060046CC RID: 18124 RVA: 0x000735B1 File Offset: 0x000717B1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002839 RID: 10297
			// (set) Token: 0x060046CD RID: 18125 RVA: 0x000735C9 File Offset: 0x000717C9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700283A RID: 10298
			// (set) Token: 0x060046CE RID: 18126 RVA: 0x000735E1 File Offset: 0x000717E1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700283B RID: 10299
			// (set) Token: 0x060046CF RID: 18127 RVA: 0x000735F9 File Offset: 0x000717F9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700283C RID: 10300
			// (set) Token: 0x060046D0 RID: 18128 RVA: 0x00073611 File Offset: 0x00071811
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

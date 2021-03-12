using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200082C RID: 2092
	public class EnableOutlookProtectionRuleCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x0600687A RID: 26746 RVA: 0x0009F10E File Offset: 0x0009D30E
		private EnableOutlookProtectionRuleCommand() : base("Enable-OutlookProtectionRule")
		{
		}

		// Token: 0x0600687B RID: 26747 RVA: 0x0009F11B File Offset: 0x0009D31B
		public EnableOutlookProtectionRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600687C RID: 26748 RVA: 0x0009F12A File Offset: 0x0009D32A
		public virtual EnableOutlookProtectionRuleCommand SetParameters(EnableOutlookProtectionRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600687D RID: 26749 RVA: 0x0009F134 File Offset: 0x0009D334
		public virtual EnableOutlookProtectionRuleCommand SetParameters(EnableOutlookProtectionRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200082D RID: 2093
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170043DB RID: 17371
			// (set) Token: 0x0600687E RID: 26750 RVA: 0x0009F13E File Offset: 0x0009D33E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170043DC RID: 17372
			// (set) Token: 0x0600687F RID: 26751 RVA: 0x0009F151 File Offset: 0x0009D351
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170043DD RID: 17373
			// (set) Token: 0x06006880 RID: 26752 RVA: 0x0009F169 File Offset: 0x0009D369
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170043DE RID: 17374
			// (set) Token: 0x06006881 RID: 26753 RVA: 0x0009F181 File Offset: 0x0009D381
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170043DF RID: 17375
			// (set) Token: 0x06006882 RID: 26754 RVA: 0x0009F199 File Offset: 0x0009D399
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170043E0 RID: 17376
			// (set) Token: 0x06006883 RID: 26755 RVA: 0x0009F1B1 File Offset: 0x0009D3B1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200082E RID: 2094
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170043E1 RID: 17377
			// (set) Token: 0x06006885 RID: 26757 RVA: 0x0009F1D1 File Offset: 0x0009D3D1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x170043E2 RID: 17378
			// (set) Token: 0x06006886 RID: 26758 RVA: 0x0009F1EF File Offset: 0x0009D3EF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170043E3 RID: 17379
			// (set) Token: 0x06006887 RID: 26759 RVA: 0x0009F202 File Offset: 0x0009D402
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170043E4 RID: 17380
			// (set) Token: 0x06006888 RID: 26760 RVA: 0x0009F21A File Offset: 0x0009D41A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170043E5 RID: 17381
			// (set) Token: 0x06006889 RID: 26761 RVA: 0x0009F232 File Offset: 0x0009D432
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170043E6 RID: 17382
			// (set) Token: 0x0600688A RID: 26762 RVA: 0x0009F24A File Offset: 0x0009D44A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170043E7 RID: 17383
			// (set) Token: 0x0600688B RID: 26763 RVA: 0x0009F262 File Offset: 0x0009D462
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

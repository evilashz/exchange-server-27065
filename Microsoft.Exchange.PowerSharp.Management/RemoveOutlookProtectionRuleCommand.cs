using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000834 RID: 2100
	public class RemoveOutlookProtectionRuleCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x060068B4 RID: 26804 RVA: 0x0009F5A8 File Offset: 0x0009D7A8
		private RemoveOutlookProtectionRuleCommand() : base("Remove-OutlookProtectionRule")
		{
		}

		// Token: 0x060068B5 RID: 26805 RVA: 0x0009F5B5 File Offset: 0x0009D7B5
		public RemoveOutlookProtectionRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060068B6 RID: 26806 RVA: 0x0009F5C4 File Offset: 0x0009D7C4
		public virtual RemoveOutlookProtectionRuleCommand SetParameters(RemoveOutlookProtectionRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x0009F5CE File Offset: 0x0009D7CE
		public virtual RemoveOutlookProtectionRuleCommand SetParameters(RemoveOutlookProtectionRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000835 RID: 2101
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004405 RID: 17413
			// (set) Token: 0x060068B8 RID: 26808 RVA: 0x0009F5D8 File Offset: 0x0009D7D8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004406 RID: 17414
			// (set) Token: 0x060068B9 RID: 26809 RVA: 0x0009F5EB File Offset: 0x0009D7EB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004407 RID: 17415
			// (set) Token: 0x060068BA RID: 26810 RVA: 0x0009F603 File Offset: 0x0009D803
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004408 RID: 17416
			// (set) Token: 0x060068BB RID: 26811 RVA: 0x0009F61B File Offset: 0x0009D81B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004409 RID: 17417
			// (set) Token: 0x060068BC RID: 26812 RVA: 0x0009F633 File Offset: 0x0009D833
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700440A RID: 17418
			// (set) Token: 0x060068BD RID: 26813 RVA: 0x0009F64B File Offset: 0x0009D84B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700440B RID: 17419
			// (set) Token: 0x060068BE RID: 26814 RVA: 0x0009F663 File Offset: 0x0009D863
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000836 RID: 2102
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700440C RID: 17420
			// (set) Token: 0x060068C0 RID: 26816 RVA: 0x0009F683 File Offset: 0x0009D883
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x1700440D RID: 17421
			// (set) Token: 0x060068C1 RID: 26817 RVA: 0x0009F6A1 File Offset: 0x0009D8A1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700440E RID: 17422
			// (set) Token: 0x060068C2 RID: 26818 RVA: 0x0009F6B4 File Offset: 0x0009D8B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700440F RID: 17423
			// (set) Token: 0x060068C3 RID: 26819 RVA: 0x0009F6CC File Offset: 0x0009D8CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004410 RID: 17424
			// (set) Token: 0x060068C4 RID: 26820 RVA: 0x0009F6E4 File Offset: 0x0009D8E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004411 RID: 17425
			// (set) Token: 0x060068C5 RID: 26821 RVA: 0x0009F6FC File Offset: 0x0009D8FC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004412 RID: 17426
			// (set) Token: 0x060068C6 RID: 26822 RVA: 0x0009F714 File Offset: 0x0009D914
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004413 RID: 17427
			// (set) Token: 0x060068C7 RID: 26823 RVA: 0x0009F72C File Offset: 0x0009D92C
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

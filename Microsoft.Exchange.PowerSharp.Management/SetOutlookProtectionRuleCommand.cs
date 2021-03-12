using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.OutlookProtectionRules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000837 RID: 2103
	public class SetOutlookProtectionRuleCommand : SyntheticCommandWithPipelineInputNoOutput<OutlookProtectionRulePresentationObject>
	{
		// Token: 0x060068C9 RID: 26825 RVA: 0x0009F74C File Offset: 0x0009D94C
		private SetOutlookProtectionRuleCommand() : base("Set-OutlookProtectionRule")
		{
		}

		// Token: 0x060068CA RID: 26826 RVA: 0x0009F759 File Offset: 0x0009D959
		public SetOutlookProtectionRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060068CB RID: 26827 RVA: 0x0009F768 File Offset: 0x0009D968
		public virtual SetOutlookProtectionRuleCommand SetParameters(SetOutlookProtectionRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060068CC RID: 26828 RVA: 0x0009F772 File Offset: 0x0009D972
		public virtual SetOutlookProtectionRuleCommand SetParameters(SetOutlookProtectionRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000838 RID: 2104
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004414 RID: 17428
			// (set) Token: 0x060068CD RID: 26829 RVA: 0x0009F77C File Offset: 0x0009D97C
			public virtual string ApplyRightsProtectionTemplate
			{
				set
				{
					base.PowerSharpParameters["ApplyRightsProtectionTemplate"] = ((value != null) ? new RmsTemplateIdParameter(value) : null);
				}
			}

			// Token: 0x17004415 RID: 17429
			// (set) Token: 0x060068CE RID: 26830 RVA: 0x0009F79A File Offset: 0x0009D99A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004416 RID: 17430
			// (set) Token: 0x060068CF RID: 26831 RVA: 0x0009F7B2 File Offset: 0x0009D9B2
			public virtual string FromDepartment
			{
				set
				{
					base.PowerSharpParameters["FromDepartment"] = value;
				}
			}

			// Token: 0x17004417 RID: 17431
			// (set) Token: 0x060068D0 RID: 26832 RVA: 0x0009F7C5 File Offset: 0x0009D9C5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004418 RID: 17432
			// (set) Token: 0x060068D1 RID: 26833 RVA: 0x0009F7D8 File Offset: 0x0009D9D8
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17004419 RID: 17433
			// (set) Token: 0x060068D2 RID: 26834 RVA: 0x0009F7F0 File Offset: 0x0009D9F0
			public virtual MultiValuedProperty<RecipientIdParameter> SentTo
			{
				set
				{
					base.PowerSharpParameters["SentTo"] = value;
				}
			}

			// Token: 0x1700441A RID: 17434
			// (set) Token: 0x060068D3 RID: 26835 RVA: 0x0009F803 File Offset: 0x0009DA03
			public virtual Microsoft.Exchange.Management.OutlookProtectionRules.ToUserScope SentToScope
			{
				set
				{
					base.PowerSharpParameters["SentToScope"] = value;
				}
			}

			// Token: 0x1700441B RID: 17435
			// (set) Token: 0x060068D4 RID: 26836 RVA: 0x0009F81B File Offset: 0x0009DA1B
			public virtual bool UserCanOverride
			{
				set
				{
					base.PowerSharpParameters["UserCanOverride"] = value;
				}
			}

			// Token: 0x1700441C RID: 17436
			// (set) Token: 0x060068D5 RID: 26837 RVA: 0x0009F833 File Offset: 0x0009DA33
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700441D RID: 17437
			// (set) Token: 0x060068D6 RID: 26838 RVA: 0x0009F846 File Offset: 0x0009DA46
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700441E RID: 17438
			// (set) Token: 0x060068D7 RID: 26839 RVA: 0x0009F85E File Offset: 0x0009DA5E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700441F RID: 17439
			// (set) Token: 0x060068D8 RID: 26840 RVA: 0x0009F876 File Offset: 0x0009DA76
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004420 RID: 17440
			// (set) Token: 0x060068D9 RID: 26841 RVA: 0x0009F88E File Offset: 0x0009DA8E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004421 RID: 17441
			// (set) Token: 0x060068DA RID: 26842 RVA: 0x0009F8A6 File Offset: 0x0009DAA6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000839 RID: 2105
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004422 RID: 17442
			// (set) Token: 0x060068DC RID: 26844 RVA: 0x0009F8C6 File Offset: 0x0009DAC6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x17004423 RID: 17443
			// (set) Token: 0x060068DD RID: 26845 RVA: 0x0009F8E4 File Offset: 0x0009DAE4
			public virtual string ApplyRightsProtectionTemplate
			{
				set
				{
					base.PowerSharpParameters["ApplyRightsProtectionTemplate"] = ((value != null) ? new RmsTemplateIdParameter(value) : null);
				}
			}

			// Token: 0x17004424 RID: 17444
			// (set) Token: 0x060068DE RID: 26846 RVA: 0x0009F902 File Offset: 0x0009DB02
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004425 RID: 17445
			// (set) Token: 0x060068DF RID: 26847 RVA: 0x0009F91A File Offset: 0x0009DB1A
			public virtual string FromDepartment
			{
				set
				{
					base.PowerSharpParameters["FromDepartment"] = value;
				}
			}

			// Token: 0x17004426 RID: 17446
			// (set) Token: 0x060068E0 RID: 26848 RVA: 0x0009F92D File Offset: 0x0009DB2D
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004427 RID: 17447
			// (set) Token: 0x060068E1 RID: 26849 RVA: 0x0009F940 File Offset: 0x0009DB40
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17004428 RID: 17448
			// (set) Token: 0x060068E2 RID: 26850 RVA: 0x0009F958 File Offset: 0x0009DB58
			public virtual MultiValuedProperty<RecipientIdParameter> SentTo
			{
				set
				{
					base.PowerSharpParameters["SentTo"] = value;
				}
			}

			// Token: 0x17004429 RID: 17449
			// (set) Token: 0x060068E3 RID: 26851 RVA: 0x0009F96B File Offset: 0x0009DB6B
			public virtual Microsoft.Exchange.Management.OutlookProtectionRules.ToUserScope SentToScope
			{
				set
				{
					base.PowerSharpParameters["SentToScope"] = value;
				}
			}

			// Token: 0x1700442A RID: 17450
			// (set) Token: 0x060068E4 RID: 26852 RVA: 0x0009F983 File Offset: 0x0009DB83
			public virtual bool UserCanOverride
			{
				set
				{
					base.PowerSharpParameters["UserCanOverride"] = value;
				}
			}

			// Token: 0x1700442B RID: 17451
			// (set) Token: 0x060068E5 RID: 26853 RVA: 0x0009F99B File Offset: 0x0009DB9B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700442C RID: 17452
			// (set) Token: 0x060068E6 RID: 26854 RVA: 0x0009F9AE File Offset: 0x0009DBAE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700442D RID: 17453
			// (set) Token: 0x060068E7 RID: 26855 RVA: 0x0009F9C6 File Offset: 0x0009DBC6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700442E RID: 17454
			// (set) Token: 0x060068E8 RID: 26856 RVA: 0x0009F9DE File Offset: 0x0009DBDE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700442F RID: 17455
			// (set) Token: 0x060068E9 RID: 26857 RVA: 0x0009F9F6 File Offset: 0x0009DBF6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004430 RID: 17456
			// (set) Token: 0x060068EA RID: 26858 RVA: 0x0009FA0E File Offset: 0x0009DC0E
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

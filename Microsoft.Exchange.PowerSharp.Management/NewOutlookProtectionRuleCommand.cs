using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.OutlookProtectionRules;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000832 RID: 2098
	public class NewOutlookProtectionRuleCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x060068A0 RID: 26784 RVA: 0x0009F402 File Offset: 0x0009D602
		private NewOutlookProtectionRuleCommand() : base("New-OutlookProtectionRule")
		{
		}

		// Token: 0x060068A1 RID: 26785 RVA: 0x0009F40F File Offset: 0x0009D60F
		public NewOutlookProtectionRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060068A2 RID: 26786 RVA: 0x0009F41E File Offset: 0x0009D61E
		public virtual NewOutlookProtectionRuleCommand SetParameters(NewOutlookProtectionRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000833 RID: 2099
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170043F5 RID: 17397
			// (set) Token: 0x060068A3 RID: 26787 RVA: 0x0009F428 File Offset: 0x0009D628
			public virtual string ApplyRightsProtectionTemplate
			{
				set
				{
					base.PowerSharpParameters["ApplyRightsProtectionTemplate"] = ((value != null) ? new RmsTemplateIdParameter(value) : null);
				}
			}

			// Token: 0x170043F6 RID: 17398
			// (set) Token: 0x060068A4 RID: 26788 RVA: 0x0009F446 File Offset: 0x0009D646
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170043F7 RID: 17399
			// (set) Token: 0x060068A5 RID: 26789 RVA: 0x0009F45E File Offset: 0x0009D65E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170043F8 RID: 17400
			// (set) Token: 0x060068A6 RID: 26790 RVA: 0x0009F476 File Offset: 0x0009D676
			public virtual string FromDepartment
			{
				set
				{
					base.PowerSharpParameters["FromDepartment"] = value;
				}
			}

			// Token: 0x170043F9 RID: 17401
			// (set) Token: 0x060068A7 RID: 26791 RVA: 0x0009F489 File Offset: 0x0009D689
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x170043FA RID: 17402
			// (set) Token: 0x060068A8 RID: 26792 RVA: 0x0009F4A1 File Offset: 0x0009D6A1
			public virtual RecipientIdParameter SentTo
			{
				set
				{
					base.PowerSharpParameters["SentTo"] = value;
				}
			}

			// Token: 0x170043FB RID: 17403
			// (set) Token: 0x060068A9 RID: 26793 RVA: 0x0009F4B4 File Offset: 0x0009D6B4
			public virtual ToUserScope SentToScope
			{
				set
				{
					base.PowerSharpParameters["SentToScope"] = value;
				}
			}

			// Token: 0x170043FC RID: 17404
			// (set) Token: 0x060068AA RID: 26794 RVA: 0x0009F4CC File Offset: 0x0009D6CC
			public virtual bool UserCanOverride
			{
				set
				{
					base.PowerSharpParameters["UserCanOverride"] = value;
				}
			}

			// Token: 0x170043FD RID: 17405
			// (set) Token: 0x060068AB RID: 26795 RVA: 0x0009F4E4 File Offset: 0x0009D6E4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170043FE RID: 17406
			// (set) Token: 0x060068AC RID: 26796 RVA: 0x0009F502 File Offset: 0x0009D702
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170043FF RID: 17407
			// (set) Token: 0x060068AD RID: 26797 RVA: 0x0009F515 File Offset: 0x0009D715
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004400 RID: 17408
			// (set) Token: 0x060068AE RID: 26798 RVA: 0x0009F528 File Offset: 0x0009D728
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004401 RID: 17409
			// (set) Token: 0x060068AF RID: 26799 RVA: 0x0009F540 File Offset: 0x0009D740
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004402 RID: 17410
			// (set) Token: 0x060068B0 RID: 26800 RVA: 0x0009F558 File Offset: 0x0009D758
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004403 RID: 17411
			// (set) Token: 0x060068B1 RID: 26801 RVA: 0x0009F570 File Offset: 0x0009D770
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004404 RID: 17412
			// (set) Token: 0x060068B2 RID: 26802 RVA: 0x0009F588 File Offset: 0x0009D788
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

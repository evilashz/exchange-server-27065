using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000829 RID: 2089
	public class DisableOutlookProtectionRuleCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x06006865 RID: 26725 RVA: 0x0009EF6A File Offset: 0x0009D16A
		private DisableOutlookProtectionRuleCommand() : base("Disable-OutlookProtectionRule")
		{
		}

		// Token: 0x06006866 RID: 26726 RVA: 0x0009EF77 File Offset: 0x0009D177
		public DisableOutlookProtectionRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006867 RID: 26727 RVA: 0x0009EF86 File Offset: 0x0009D186
		public virtual DisableOutlookProtectionRuleCommand SetParameters(DisableOutlookProtectionRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006868 RID: 26728 RVA: 0x0009EF90 File Offset: 0x0009D190
		public virtual DisableOutlookProtectionRuleCommand SetParameters(DisableOutlookProtectionRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200082A RID: 2090
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170043CC RID: 17356
			// (set) Token: 0x06006869 RID: 26729 RVA: 0x0009EF9A File Offset: 0x0009D19A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170043CD RID: 17357
			// (set) Token: 0x0600686A RID: 26730 RVA: 0x0009EFAD File Offset: 0x0009D1AD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170043CE RID: 17358
			// (set) Token: 0x0600686B RID: 26731 RVA: 0x0009EFC5 File Offset: 0x0009D1C5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170043CF RID: 17359
			// (set) Token: 0x0600686C RID: 26732 RVA: 0x0009EFDD File Offset: 0x0009D1DD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170043D0 RID: 17360
			// (set) Token: 0x0600686D RID: 26733 RVA: 0x0009EFF5 File Offset: 0x0009D1F5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170043D1 RID: 17361
			// (set) Token: 0x0600686E RID: 26734 RVA: 0x0009F00D File Offset: 0x0009D20D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170043D2 RID: 17362
			// (set) Token: 0x0600686F RID: 26735 RVA: 0x0009F025 File Offset: 0x0009D225
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200082B RID: 2091
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170043D3 RID: 17363
			// (set) Token: 0x06006871 RID: 26737 RVA: 0x0009F045 File Offset: 0x0009D245
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x170043D4 RID: 17364
			// (set) Token: 0x06006872 RID: 26738 RVA: 0x0009F063 File Offset: 0x0009D263
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170043D5 RID: 17365
			// (set) Token: 0x06006873 RID: 26739 RVA: 0x0009F076 File Offset: 0x0009D276
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170043D6 RID: 17366
			// (set) Token: 0x06006874 RID: 26740 RVA: 0x0009F08E File Offset: 0x0009D28E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170043D7 RID: 17367
			// (set) Token: 0x06006875 RID: 26741 RVA: 0x0009F0A6 File Offset: 0x0009D2A6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170043D8 RID: 17368
			// (set) Token: 0x06006876 RID: 26742 RVA: 0x0009F0BE File Offset: 0x0009D2BE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170043D9 RID: 17369
			// (set) Token: 0x06006877 RID: 26743 RVA: 0x0009F0D6 File Offset: 0x0009D2D6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170043DA RID: 17370
			// (set) Token: 0x06006878 RID: 26744 RVA: 0x0009F0EE File Offset: 0x0009D2EE
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

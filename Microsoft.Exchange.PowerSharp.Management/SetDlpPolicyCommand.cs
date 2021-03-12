using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005D2 RID: 1490
	public class SetDlpPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<ADComplianceProgram>
	{
		// Token: 0x06004D32 RID: 19762 RVA: 0x0007B625 File Offset: 0x00079825
		private SetDlpPolicyCommand() : base("Set-DlpPolicy")
		{
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0007B632 File Offset: 0x00079832
		public SetDlpPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x0007B641 File Offset: 0x00079841
		public virtual SetDlpPolicyCommand SetParameters(SetDlpPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004D35 RID: 19765 RVA: 0x0007B64B File Offset: 0x0007984B
		public virtual SetDlpPolicyCommand SetParameters(SetDlpPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005D3 RID: 1491
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002D47 RID: 11591
			// (set) Token: 0x06004D36 RID: 19766 RVA: 0x0007B655 File Offset: 0x00079855
			public virtual RuleState State
			{
				set
				{
					base.PowerSharpParameters["State"] = value;
				}
			}

			// Token: 0x17002D48 RID: 11592
			// (set) Token: 0x06004D37 RID: 19767 RVA: 0x0007B66D File Offset: 0x0007986D
			public virtual RuleMode Mode
			{
				set
				{
					base.PowerSharpParameters["Mode"] = value;
				}
			}

			// Token: 0x17002D49 RID: 11593
			// (set) Token: 0x06004D38 RID: 19768 RVA: 0x0007B685 File Offset: 0x00079885
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17002D4A RID: 11594
			// (set) Token: 0x06004D39 RID: 19769 RVA: 0x0007B698 File Offset: 0x00079898
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D4B RID: 11595
			// (set) Token: 0x06004D3A RID: 19770 RVA: 0x0007B6AB File Offset: 0x000798AB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002D4C RID: 11596
			// (set) Token: 0x06004D3B RID: 19771 RVA: 0x0007B6BE File Offset: 0x000798BE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D4D RID: 11597
			// (set) Token: 0x06004D3C RID: 19772 RVA: 0x0007B6D6 File Offset: 0x000798D6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D4E RID: 11598
			// (set) Token: 0x06004D3D RID: 19773 RVA: 0x0007B6EE File Offset: 0x000798EE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D4F RID: 11599
			// (set) Token: 0x06004D3E RID: 19774 RVA: 0x0007B706 File Offset: 0x00079906
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D50 RID: 11600
			// (set) Token: 0x06004D3F RID: 19775 RVA: 0x0007B71E File Offset: 0x0007991E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020005D4 RID: 1492
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002D51 RID: 11601
			// (set) Token: 0x06004D41 RID: 19777 RVA: 0x0007B73E File Offset: 0x0007993E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DlpPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17002D52 RID: 11602
			// (set) Token: 0x06004D42 RID: 19778 RVA: 0x0007B75C File Offset: 0x0007995C
			public virtual RuleState State
			{
				set
				{
					base.PowerSharpParameters["State"] = value;
				}
			}

			// Token: 0x17002D53 RID: 11603
			// (set) Token: 0x06004D43 RID: 19779 RVA: 0x0007B774 File Offset: 0x00079974
			public virtual RuleMode Mode
			{
				set
				{
					base.PowerSharpParameters["Mode"] = value;
				}
			}

			// Token: 0x17002D54 RID: 11604
			// (set) Token: 0x06004D44 RID: 19780 RVA: 0x0007B78C File Offset: 0x0007998C
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17002D55 RID: 11605
			// (set) Token: 0x06004D45 RID: 19781 RVA: 0x0007B79F File Offset: 0x0007999F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D56 RID: 11606
			// (set) Token: 0x06004D46 RID: 19782 RVA: 0x0007B7B2 File Offset: 0x000799B2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002D57 RID: 11607
			// (set) Token: 0x06004D47 RID: 19783 RVA: 0x0007B7C5 File Offset: 0x000799C5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D58 RID: 11608
			// (set) Token: 0x06004D48 RID: 19784 RVA: 0x0007B7DD File Offset: 0x000799DD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D59 RID: 11609
			// (set) Token: 0x06004D49 RID: 19785 RVA: 0x0007B7F5 File Offset: 0x000799F5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D5A RID: 11610
			// (set) Token: 0x06004D4A RID: 19786 RVA: 0x0007B80D File Offset: 0x00079A0D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D5B RID: 11611
			// (set) Token: 0x06004D4B RID: 19787 RVA: 0x0007B825 File Offset: 0x00079A25
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

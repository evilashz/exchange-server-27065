using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005D8 RID: 1496
	public class RemoveDlpPolicyTemplateCommand : SyntheticCommandWithPipelineInput<ADComplianceProgram, ADComplianceProgram>
	{
		// Token: 0x06004D62 RID: 19810 RVA: 0x0007B9E9 File Offset: 0x00079BE9
		private RemoveDlpPolicyTemplateCommand() : base("Remove-DlpPolicyTemplate")
		{
		}

		// Token: 0x06004D63 RID: 19811 RVA: 0x0007B9F6 File Offset: 0x00079BF6
		public RemoveDlpPolicyTemplateCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x0007BA05 File Offset: 0x00079C05
		public virtual RemoveDlpPolicyTemplateCommand SetParameters(RemoveDlpPolicyTemplateCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x0007BA0F File Offset: 0x00079C0F
		public virtual RemoveDlpPolicyTemplateCommand SetParameters(RemoveDlpPolicyTemplateCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005D9 RID: 1497
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002D6B RID: 11627
			// (set) Token: 0x06004D66 RID: 19814 RVA: 0x0007BA19 File Offset: 0x00079C19
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D6C RID: 11628
			// (set) Token: 0x06004D67 RID: 19815 RVA: 0x0007BA2C File Offset: 0x00079C2C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D6D RID: 11629
			// (set) Token: 0x06004D68 RID: 19816 RVA: 0x0007BA44 File Offset: 0x00079C44
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D6E RID: 11630
			// (set) Token: 0x06004D69 RID: 19817 RVA: 0x0007BA5C File Offset: 0x00079C5C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D6F RID: 11631
			// (set) Token: 0x06004D6A RID: 19818 RVA: 0x0007BA74 File Offset: 0x00079C74
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D70 RID: 11632
			// (set) Token: 0x06004D6B RID: 19819 RVA: 0x0007BA8C File Offset: 0x00079C8C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002D71 RID: 11633
			// (set) Token: 0x06004D6C RID: 19820 RVA: 0x0007BAA4 File Offset: 0x00079CA4
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005DA RID: 1498
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002D72 RID: 11634
			// (set) Token: 0x06004D6E RID: 19822 RVA: 0x0007BAC4 File Offset: 0x00079CC4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DlpPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17002D73 RID: 11635
			// (set) Token: 0x06004D6F RID: 19823 RVA: 0x0007BAE2 File Offset: 0x00079CE2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D74 RID: 11636
			// (set) Token: 0x06004D70 RID: 19824 RVA: 0x0007BAF5 File Offset: 0x00079CF5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D75 RID: 11637
			// (set) Token: 0x06004D71 RID: 19825 RVA: 0x0007BB0D File Offset: 0x00079D0D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D76 RID: 11638
			// (set) Token: 0x06004D72 RID: 19826 RVA: 0x0007BB25 File Offset: 0x00079D25
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D77 RID: 11639
			// (set) Token: 0x06004D73 RID: 19827 RVA: 0x0007BB3D File Offset: 0x00079D3D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D78 RID: 11640
			// (set) Token: 0x06004D74 RID: 19828 RVA: 0x0007BB55 File Offset: 0x00079D55
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002D79 RID: 11641
			// (set) Token: 0x06004D75 RID: 19829 RVA: 0x0007BB6D File Offset: 0x00079D6D
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

using System;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Office.ComplianceJob.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000414 RID: 1044
	public class RemoveComplianceSearchCommand : SyntheticCommandWithPipelineInput<ComplianceSearch, ComplianceSearch>
	{
		// Token: 0x06003D82 RID: 15746 RVA: 0x000679EE File Offset: 0x00065BEE
		private RemoveComplianceSearchCommand() : base("Remove-ComplianceSearch")
		{
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x000679FB File Offset: 0x00065BFB
		public RemoveComplianceSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x00067A0A File Offset: 0x00065C0A
		public virtual RemoveComplianceSearchCommand SetParameters(RemoveComplianceSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x00067A14 File Offset: 0x00065C14
		public virtual RemoveComplianceSearchCommand SetParameters(RemoveComplianceSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000415 RID: 1045
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002113 RID: 8467
			// (set) Token: 0x06003D86 RID: 15750 RVA: 0x00067A1E File Offset: 0x00065C1E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002114 RID: 8468
			// (set) Token: 0x06003D87 RID: 15751 RVA: 0x00067A31 File Offset: 0x00065C31
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002115 RID: 8469
			// (set) Token: 0x06003D88 RID: 15752 RVA: 0x00067A49 File Offset: 0x00065C49
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002116 RID: 8470
			// (set) Token: 0x06003D89 RID: 15753 RVA: 0x00067A61 File Offset: 0x00065C61
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002117 RID: 8471
			// (set) Token: 0x06003D8A RID: 15754 RVA: 0x00067A79 File Offset: 0x00065C79
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002118 RID: 8472
			// (set) Token: 0x06003D8B RID: 15755 RVA: 0x00067A91 File Offset: 0x00065C91
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002119 RID: 8473
			// (set) Token: 0x06003D8C RID: 15756 RVA: 0x00067AA9 File Offset: 0x00065CA9
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000416 RID: 1046
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700211A RID: 8474
			// (set) Token: 0x06003D8E RID: 15758 RVA: 0x00067AC9 File Offset: 0x00065CC9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ComplianceJobIdParameter(value) : null);
				}
			}

			// Token: 0x1700211B RID: 8475
			// (set) Token: 0x06003D8F RID: 15759 RVA: 0x00067AE7 File Offset: 0x00065CE7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700211C RID: 8476
			// (set) Token: 0x06003D90 RID: 15760 RVA: 0x00067AFA File Offset: 0x00065CFA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700211D RID: 8477
			// (set) Token: 0x06003D91 RID: 15761 RVA: 0x00067B12 File Offset: 0x00065D12
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700211E RID: 8478
			// (set) Token: 0x06003D92 RID: 15762 RVA: 0x00067B2A File Offset: 0x00065D2A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700211F RID: 8479
			// (set) Token: 0x06003D93 RID: 15763 RVA: 0x00067B42 File Offset: 0x00065D42
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002120 RID: 8480
			// (set) Token: 0x06003D94 RID: 15764 RVA: 0x00067B5A File Offset: 0x00065D5A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002121 RID: 8481
			// (set) Token: 0x06003D95 RID: 15765 RVA: 0x00067B72 File Offset: 0x00065D72
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

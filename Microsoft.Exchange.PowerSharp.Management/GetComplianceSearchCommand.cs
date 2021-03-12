using System;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Office.ComplianceJob.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000417 RID: 1047
	public class GetComplianceSearchCommand : SyntheticCommandWithPipelineInput<ComplianceSearch, ComplianceSearch>
	{
		// Token: 0x06003D97 RID: 15767 RVA: 0x00067B92 File Offset: 0x00065D92
		private GetComplianceSearchCommand() : base("Get-ComplianceSearch")
		{
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x00067B9F File Offset: 0x00065D9F
		public GetComplianceSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x00067BAE File Offset: 0x00065DAE
		public virtual GetComplianceSearchCommand SetParameters(GetComplianceSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x00067BB8 File Offset: 0x00065DB8
		public virtual GetComplianceSearchCommand SetParameters(GetComplianceSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000418 RID: 1048
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002122 RID: 8482
			// (set) Token: 0x06003D9B RID: 15771 RVA: 0x00067BC2 File Offset: 0x00065DC2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002123 RID: 8483
			// (set) Token: 0x06003D9C RID: 15772 RVA: 0x00067BE0 File Offset: 0x00065DE0
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002124 RID: 8484
			// (set) Token: 0x06003D9D RID: 15773 RVA: 0x00067BF8 File Offset: 0x00065DF8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002125 RID: 8485
			// (set) Token: 0x06003D9E RID: 15774 RVA: 0x00067C0B File Offset: 0x00065E0B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002126 RID: 8486
			// (set) Token: 0x06003D9F RID: 15775 RVA: 0x00067C23 File Offset: 0x00065E23
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002127 RID: 8487
			// (set) Token: 0x06003DA0 RID: 15776 RVA: 0x00067C3B File Offset: 0x00065E3B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002128 RID: 8488
			// (set) Token: 0x06003DA1 RID: 15777 RVA: 0x00067C53 File Offset: 0x00065E53
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000419 RID: 1049
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002129 RID: 8489
			// (set) Token: 0x06003DA3 RID: 15779 RVA: 0x00067C73 File Offset: 0x00065E73
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ComplianceJobIdParameter(value) : null);
				}
			}

			// Token: 0x1700212A RID: 8490
			// (set) Token: 0x06003DA4 RID: 15780 RVA: 0x00067C91 File Offset: 0x00065E91
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700212B RID: 8491
			// (set) Token: 0x06003DA5 RID: 15781 RVA: 0x00067CAF File Offset: 0x00065EAF
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700212C RID: 8492
			// (set) Token: 0x06003DA6 RID: 15782 RVA: 0x00067CC7 File Offset: 0x00065EC7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700212D RID: 8493
			// (set) Token: 0x06003DA7 RID: 15783 RVA: 0x00067CDA File Offset: 0x00065EDA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700212E RID: 8494
			// (set) Token: 0x06003DA8 RID: 15784 RVA: 0x00067CF2 File Offset: 0x00065EF2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700212F RID: 8495
			// (set) Token: 0x06003DA9 RID: 15785 RVA: 0x00067D0A File Offset: 0x00065F0A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002130 RID: 8496
			// (set) Token: 0x06003DAA RID: 15786 RVA: 0x00067D22 File Offset: 0x00065F22
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}

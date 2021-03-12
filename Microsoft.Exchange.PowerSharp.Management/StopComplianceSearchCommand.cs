using System;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Office.ComplianceJob.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000420 RID: 1056
	public class StopComplianceSearchCommand : SyntheticCommandWithPipelineInput<ComplianceSearch, ComplianceSearch>
	{
		// Token: 0x06003E00 RID: 15872 RVA: 0x000683F8 File Offset: 0x000665F8
		private StopComplianceSearchCommand() : base("Stop-ComplianceSearch")
		{
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x00068405 File Offset: 0x00066605
		public StopComplianceSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x00068414 File Offset: 0x00066614
		public virtual StopComplianceSearchCommand SetParameters(StopComplianceSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x0006841E File Offset: 0x0006661E
		public virtual StopComplianceSearchCommand SetParameters(StopComplianceSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000421 RID: 1057
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002179 RID: 8569
			// (set) Token: 0x06003E04 RID: 15876 RVA: 0x00068428 File Offset: 0x00066628
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700217A RID: 8570
			// (set) Token: 0x06003E05 RID: 15877 RVA: 0x0006843B File Offset: 0x0006663B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700217B RID: 8571
			// (set) Token: 0x06003E06 RID: 15878 RVA: 0x00068453 File Offset: 0x00066653
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700217C RID: 8572
			// (set) Token: 0x06003E07 RID: 15879 RVA: 0x0006846B File Offset: 0x0006666B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700217D RID: 8573
			// (set) Token: 0x06003E08 RID: 15880 RVA: 0x00068483 File Offset: 0x00066683
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700217E RID: 8574
			// (set) Token: 0x06003E09 RID: 15881 RVA: 0x0006849B File Offset: 0x0006669B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700217F RID: 8575
			// (set) Token: 0x06003E0A RID: 15882 RVA: 0x000684B3 File Offset: 0x000666B3
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000422 RID: 1058
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002180 RID: 8576
			// (set) Token: 0x06003E0C RID: 15884 RVA: 0x000684D3 File Offset: 0x000666D3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ComplianceJobIdParameter(value) : null);
				}
			}

			// Token: 0x17002181 RID: 8577
			// (set) Token: 0x06003E0D RID: 15885 RVA: 0x000684F1 File Offset: 0x000666F1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002182 RID: 8578
			// (set) Token: 0x06003E0E RID: 15886 RVA: 0x00068504 File Offset: 0x00066704
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002183 RID: 8579
			// (set) Token: 0x06003E0F RID: 15887 RVA: 0x0006851C File Offset: 0x0006671C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002184 RID: 8580
			// (set) Token: 0x06003E10 RID: 15888 RVA: 0x00068534 File Offset: 0x00066734
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002185 RID: 8581
			// (set) Token: 0x06003E11 RID: 15889 RVA: 0x0006854C File Offset: 0x0006674C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002186 RID: 8582
			// (set) Token: 0x06003E12 RID: 15890 RVA: 0x00068564 File Offset: 0x00066764
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002187 RID: 8583
			// (set) Token: 0x06003E13 RID: 15891 RVA: 0x0006857C File Offset: 0x0006677C
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

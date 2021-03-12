using System;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Office.ComplianceJob.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200041D RID: 1053
	public class StartComplianceSearchCommand : SyntheticCommandWithPipelineInput<ComplianceSearch, ComplianceSearch>
	{
		// Token: 0x06003DE7 RID: 15847 RVA: 0x000681F4 File Offset: 0x000663F4
		private StartComplianceSearchCommand() : base("Start-ComplianceSearch")
		{
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x00068201 File Offset: 0x00066401
		public StartComplianceSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x00068210 File Offset: 0x00066410
		public virtual StartComplianceSearchCommand SetParameters(StartComplianceSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x0006821A File Offset: 0x0006641A
		public virtual StartComplianceSearchCommand SetParameters(StartComplianceSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200041E RID: 1054
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002166 RID: 8550
			// (set) Token: 0x06003DEB RID: 15851 RVA: 0x00068224 File Offset: 0x00066424
			public virtual ComplianceSearch.ComplianceSearchType Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17002167 RID: 8551
			// (set) Token: 0x06003DEC RID: 15852 RVA: 0x0006823C File Offset: 0x0006643C
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002168 RID: 8552
			// (set) Token: 0x06003DED RID: 15853 RVA: 0x00068254 File Offset: 0x00066454
			public virtual SwitchParameter Resume
			{
				set
				{
					base.PowerSharpParameters["Resume"] = value;
				}
			}

			// Token: 0x17002169 RID: 8553
			// (set) Token: 0x06003DEE RID: 15854 RVA: 0x0006826C File Offset: 0x0006646C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700216A RID: 8554
			// (set) Token: 0x06003DEF RID: 15855 RVA: 0x0006827F File Offset: 0x0006647F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700216B RID: 8555
			// (set) Token: 0x06003DF0 RID: 15856 RVA: 0x00068297 File Offset: 0x00066497
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700216C RID: 8556
			// (set) Token: 0x06003DF1 RID: 15857 RVA: 0x000682AF File Offset: 0x000664AF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700216D RID: 8557
			// (set) Token: 0x06003DF2 RID: 15858 RVA: 0x000682C7 File Offset: 0x000664C7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700216E RID: 8558
			// (set) Token: 0x06003DF3 RID: 15859 RVA: 0x000682DF File Offset: 0x000664DF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200041F RID: 1055
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700216F RID: 8559
			// (set) Token: 0x06003DF5 RID: 15861 RVA: 0x000682FF File Offset: 0x000664FF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ComplianceJobIdParameter(value) : null);
				}
			}

			// Token: 0x17002170 RID: 8560
			// (set) Token: 0x06003DF6 RID: 15862 RVA: 0x0006831D File Offset: 0x0006651D
			public virtual ComplianceSearch.ComplianceSearchType Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17002171 RID: 8561
			// (set) Token: 0x06003DF7 RID: 15863 RVA: 0x00068335 File Offset: 0x00066535
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002172 RID: 8562
			// (set) Token: 0x06003DF8 RID: 15864 RVA: 0x0006834D File Offset: 0x0006654D
			public virtual SwitchParameter Resume
			{
				set
				{
					base.PowerSharpParameters["Resume"] = value;
				}
			}

			// Token: 0x17002173 RID: 8563
			// (set) Token: 0x06003DF9 RID: 15865 RVA: 0x00068365 File Offset: 0x00066565
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002174 RID: 8564
			// (set) Token: 0x06003DFA RID: 15866 RVA: 0x00068378 File Offset: 0x00066578
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002175 RID: 8565
			// (set) Token: 0x06003DFB RID: 15867 RVA: 0x00068390 File Offset: 0x00066590
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002176 RID: 8566
			// (set) Token: 0x06003DFC RID: 15868 RVA: 0x000683A8 File Offset: 0x000665A8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002177 RID: 8567
			// (set) Token: 0x06003DFD RID: 15869 RVA: 0x000683C0 File Offset: 0x000665C0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002178 RID: 8568
			// (set) Token: 0x06003DFE RID: 15870 RVA: 0x000683D8 File Offset: 0x000665D8
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

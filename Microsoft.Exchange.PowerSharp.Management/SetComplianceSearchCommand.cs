using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Data;
using Microsoft.Office.ComplianceJob.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200041A RID: 1050
	public class SetComplianceSearchCommand : SyntheticCommandWithPipelineInputNoOutput<ComplianceSearch>
	{
		// Token: 0x06003DAC RID: 15788 RVA: 0x00067D42 File Offset: 0x00065F42
		private SetComplianceSearchCommand() : base("Set-ComplianceSearch")
		{
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x00067D4F File Offset: 0x00065F4F
		public SetComplianceSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x00067D5E File Offset: 0x00065F5E
		public virtual SetComplianceSearchCommand SetParameters(SetComplianceSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x00067D68 File Offset: 0x00065F68
		public virtual SetComplianceSearchCommand SetParameters(SetComplianceSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200041B RID: 1051
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002131 RID: 8497
			// (set) Token: 0x06003DB0 RID: 15792 RVA: 0x00067D72 File Offset: 0x00065F72
			public virtual string KeywordQuery
			{
				set
				{
					base.PowerSharpParameters["KeywordQuery"] = value;
				}
			}

			// Token: 0x17002132 RID: 8498
			// (set) Token: 0x06003DB1 RID: 15793 RVA: 0x00067D85 File Offset: 0x00065F85
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17002133 RID: 8499
			// (set) Token: 0x06003DB2 RID: 15794 RVA: 0x00067D9D File Offset: 0x00065F9D
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17002134 RID: 8500
			// (set) Token: 0x06003DB3 RID: 15795 RVA: 0x00067DB5 File Offset: 0x00065FB5
			public virtual CultureInfo Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x17002135 RID: 8501
			// (set) Token: 0x06003DB4 RID: 15796 RVA: 0x00067DC8 File Offset: 0x00065FC8
			public virtual bool IncludeUnindexedItems
			{
				set
				{
					base.PowerSharpParameters["IncludeUnindexedItems"] = value;
				}
			}

			// Token: 0x17002136 RID: 8502
			// (set) Token: 0x06003DB5 RID: 15797 RVA: 0x00067DE0 File Offset: 0x00065FE0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002137 RID: 8503
			// (set) Token: 0x06003DB6 RID: 15798 RVA: 0x00067DF3 File Offset: 0x00065FF3
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17002138 RID: 8504
			// (set) Token: 0x06003DB7 RID: 15799 RVA: 0x00067E06 File Offset: 0x00066006
			public virtual string ExchangeBinding
			{
				set
				{
					base.PowerSharpParameters["ExchangeBinding"] = value;
				}
			}

			// Token: 0x17002139 RID: 8505
			// (set) Token: 0x06003DB8 RID: 15800 RVA: 0x00067E19 File Offset: 0x00066019
			public virtual string PublicFolderBinding
			{
				set
				{
					base.PowerSharpParameters["PublicFolderBinding"] = value;
				}
			}

			// Token: 0x1700213A RID: 8506
			// (set) Token: 0x06003DB9 RID: 15801 RVA: 0x00067E2C File Offset: 0x0006602C
			public virtual string SharePointBinding
			{
				set
				{
					base.PowerSharpParameters["SharePointBinding"] = value;
				}
			}

			// Token: 0x1700213B RID: 8507
			// (set) Token: 0x06003DBA RID: 15802 RVA: 0x00067E3F File Offset: 0x0006603F
			public virtual string AddExchangeBinding
			{
				set
				{
					base.PowerSharpParameters["AddExchangeBinding"] = value;
				}
			}

			// Token: 0x1700213C RID: 8508
			// (set) Token: 0x06003DBB RID: 15803 RVA: 0x00067E52 File Offset: 0x00066052
			public virtual string AddPublicFolderBinding
			{
				set
				{
					base.PowerSharpParameters["AddPublicFolderBinding"] = value;
				}
			}

			// Token: 0x1700213D RID: 8509
			// (set) Token: 0x06003DBC RID: 15804 RVA: 0x00067E65 File Offset: 0x00066065
			public virtual string AddSharePointBinding
			{
				set
				{
					base.PowerSharpParameters["AddSharePointBinding"] = value;
				}
			}

			// Token: 0x1700213E RID: 8510
			// (set) Token: 0x06003DBD RID: 15805 RVA: 0x00067E78 File Offset: 0x00066078
			public virtual string RemoveExchangeBinding
			{
				set
				{
					base.PowerSharpParameters["RemoveExchangeBinding"] = value;
				}
			}

			// Token: 0x1700213F RID: 8511
			// (set) Token: 0x06003DBE RID: 15806 RVA: 0x00067E8B File Offset: 0x0006608B
			public virtual string RemovePublicFolderBinding
			{
				set
				{
					base.PowerSharpParameters["RemovePublicFolderBinding"] = value;
				}
			}

			// Token: 0x17002140 RID: 8512
			// (set) Token: 0x06003DBF RID: 15807 RVA: 0x00067E9E File Offset: 0x0006609E
			public virtual string RemoveSharePointBinding
			{
				set
				{
					base.PowerSharpParameters["RemoveSharePointBinding"] = value;
				}
			}

			// Token: 0x17002141 RID: 8513
			// (set) Token: 0x06003DC0 RID: 15808 RVA: 0x00067EB1 File Offset: 0x000660B1
			public virtual bool AllPublicFolderBindings
			{
				set
				{
					base.PowerSharpParameters["AllPublicFolderBindings"] = value;
				}
			}

			// Token: 0x17002142 RID: 8514
			// (set) Token: 0x06003DC1 RID: 15809 RVA: 0x00067EC9 File Offset: 0x000660C9
			public virtual bool AllExchangeBindings
			{
				set
				{
					base.PowerSharpParameters["AllExchangeBindings"] = value;
				}
			}

			// Token: 0x17002143 RID: 8515
			// (set) Token: 0x06003DC2 RID: 15810 RVA: 0x00067EE1 File Offset: 0x000660E1
			public virtual bool AllSharePointBindings
			{
				set
				{
					base.PowerSharpParameters["AllSharePointBindings"] = value;
				}
			}

			// Token: 0x17002144 RID: 8516
			// (set) Token: 0x06003DC3 RID: 15811 RVA: 0x00067EF9 File Offset: 0x000660F9
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002145 RID: 8517
			// (set) Token: 0x06003DC4 RID: 15812 RVA: 0x00067F11 File Offset: 0x00066111
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002146 RID: 8518
			// (set) Token: 0x06003DC5 RID: 15813 RVA: 0x00067F24 File Offset: 0x00066124
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002147 RID: 8519
			// (set) Token: 0x06003DC6 RID: 15814 RVA: 0x00067F3C File Offset: 0x0006613C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002148 RID: 8520
			// (set) Token: 0x06003DC7 RID: 15815 RVA: 0x00067F54 File Offset: 0x00066154
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002149 RID: 8521
			// (set) Token: 0x06003DC8 RID: 15816 RVA: 0x00067F6C File Offset: 0x0006616C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700214A RID: 8522
			// (set) Token: 0x06003DC9 RID: 15817 RVA: 0x00067F84 File Offset: 0x00066184
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200041C RID: 1052
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700214B RID: 8523
			// (set) Token: 0x06003DCB RID: 15819 RVA: 0x00067FA4 File Offset: 0x000661A4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ComplianceJobIdParameter(value) : null);
				}
			}

			// Token: 0x1700214C RID: 8524
			// (set) Token: 0x06003DCC RID: 15820 RVA: 0x00067FC2 File Offset: 0x000661C2
			public virtual string KeywordQuery
			{
				set
				{
					base.PowerSharpParameters["KeywordQuery"] = value;
				}
			}

			// Token: 0x1700214D RID: 8525
			// (set) Token: 0x06003DCD RID: 15821 RVA: 0x00067FD5 File Offset: 0x000661D5
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x1700214E RID: 8526
			// (set) Token: 0x06003DCE RID: 15822 RVA: 0x00067FED File Offset: 0x000661ED
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x1700214F RID: 8527
			// (set) Token: 0x06003DCF RID: 15823 RVA: 0x00068005 File Offset: 0x00066205
			public virtual CultureInfo Language
			{
				set
				{
					base.PowerSharpParameters["Language"] = value;
				}
			}

			// Token: 0x17002150 RID: 8528
			// (set) Token: 0x06003DD0 RID: 15824 RVA: 0x00068018 File Offset: 0x00066218
			public virtual bool IncludeUnindexedItems
			{
				set
				{
					base.PowerSharpParameters["IncludeUnindexedItems"] = value;
				}
			}

			// Token: 0x17002151 RID: 8529
			// (set) Token: 0x06003DD1 RID: 15825 RVA: 0x00068030 File Offset: 0x00066230
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002152 RID: 8530
			// (set) Token: 0x06003DD2 RID: 15826 RVA: 0x00068043 File Offset: 0x00066243
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17002153 RID: 8531
			// (set) Token: 0x06003DD3 RID: 15827 RVA: 0x00068056 File Offset: 0x00066256
			public virtual string ExchangeBinding
			{
				set
				{
					base.PowerSharpParameters["ExchangeBinding"] = value;
				}
			}

			// Token: 0x17002154 RID: 8532
			// (set) Token: 0x06003DD4 RID: 15828 RVA: 0x00068069 File Offset: 0x00066269
			public virtual string PublicFolderBinding
			{
				set
				{
					base.PowerSharpParameters["PublicFolderBinding"] = value;
				}
			}

			// Token: 0x17002155 RID: 8533
			// (set) Token: 0x06003DD5 RID: 15829 RVA: 0x0006807C File Offset: 0x0006627C
			public virtual string SharePointBinding
			{
				set
				{
					base.PowerSharpParameters["SharePointBinding"] = value;
				}
			}

			// Token: 0x17002156 RID: 8534
			// (set) Token: 0x06003DD6 RID: 15830 RVA: 0x0006808F File Offset: 0x0006628F
			public virtual string AddExchangeBinding
			{
				set
				{
					base.PowerSharpParameters["AddExchangeBinding"] = value;
				}
			}

			// Token: 0x17002157 RID: 8535
			// (set) Token: 0x06003DD7 RID: 15831 RVA: 0x000680A2 File Offset: 0x000662A2
			public virtual string AddPublicFolderBinding
			{
				set
				{
					base.PowerSharpParameters["AddPublicFolderBinding"] = value;
				}
			}

			// Token: 0x17002158 RID: 8536
			// (set) Token: 0x06003DD8 RID: 15832 RVA: 0x000680B5 File Offset: 0x000662B5
			public virtual string AddSharePointBinding
			{
				set
				{
					base.PowerSharpParameters["AddSharePointBinding"] = value;
				}
			}

			// Token: 0x17002159 RID: 8537
			// (set) Token: 0x06003DD9 RID: 15833 RVA: 0x000680C8 File Offset: 0x000662C8
			public virtual string RemoveExchangeBinding
			{
				set
				{
					base.PowerSharpParameters["RemoveExchangeBinding"] = value;
				}
			}

			// Token: 0x1700215A RID: 8538
			// (set) Token: 0x06003DDA RID: 15834 RVA: 0x000680DB File Offset: 0x000662DB
			public virtual string RemovePublicFolderBinding
			{
				set
				{
					base.PowerSharpParameters["RemovePublicFolderBinding"] = value;
				}
			}

			// Token: 0x1700215B RID: 8539
			// (set) Token: 0x06003DDB RID: 15835 RVA: 0x000680EE File Offset: 0x000662EE
			public virtual string RemoveSharePointBinding
			{
				set
				{
					base.PowerSharpParameters["RemoveSharePointBinding"] = value;
				}
			}

			// Token: 0x1700215C RID: 8540
			// (set) Token: 0x06003DDC RID: 15836 RVA: 0x00068101 File Offset: 0x00066301
			public virtual bool AllPublicFolderBindings
			{
				set
				{
					base.PowerSharpParameters["AllPublicFolderBindings"] = value;
				}
			}

			// Token: 0x1700215D RID: 8541
			// (set) Token: 0x06003DDD RID: 15837 RVA: 0x00068119 File Offset: 0x00066319
			public virtual bool AllExchangeBindings
			{
				set
				{
					base.PowerSharpParameters["AllExchangeBindings"] = value;
				}
			}

			// Token: 0x1700215E RID: 8542
			// (set) Token: 0x06003DDE RID: 15838 RVA: 0x00068131 File Offset: 0x00066331
			public virtual bool AllSharePointBindings
			{
				set
				{
					base.PowerSharpParameters["AllSharePointBindings"] = value;
				}
			}

			// Token: 0x1700215F RID: 8543
			// (set) Token: 0x06003DDF RID: 15839 RVA: 0x00068149 File Offset: 0x00066349
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002160 RID: 8544
			// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x00068161 File Offset: 0x00066361
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002161 RID: 8545
			// (set) Token: 0x06003DE1 RID: 15841 RVA: 0x00068174 File Offset: 0x00066374
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002162 RID: 8546
			// (set) Token: 0x06003DE2 RID: 15842 RVA: 0x0006818C File Offset: 0x0006638C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002163 RID: 8547
			// (set) Token: 0x06003DE3 RID: 15843 RVA: 0x000681A4 File Offset: 0x000663A4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002164 RID: 8548
			// (set) Token: 0x06003DE4 RID: 15844 RVA: 0x000681BC File Offset: 0x000663BC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002165 RID: 8549
			// (set) Token: 0x06003DE5 RID: 15845 RVA: 0x000681D4 File Offset: 0x000663D4
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

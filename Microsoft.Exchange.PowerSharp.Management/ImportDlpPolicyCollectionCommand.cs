using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005DB RID: 1499
	public class ImportDlpPolicyCollectionCommand : SyntheticCommandWithPipelineInput<ADComplianceProgram, ADComplianceProgram>
	{
		// Token: 0x06004D77 RID: 19831 RVA: 0x0007BB8D File Offset: 0x00079D8D
		private ImportDlpPolicyCollectionCommand() : base("Import-DlpPolicyCollection")
		{
		}

		// Token: 0x06004D78 RID: 19832 RVA: 0x0007BB9A File Offset: 0x00079D9A
		public ImportDlpPolicyCollectionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004D79 RID: 19833 RVA: 0x0007BBA9 File Offset: 0x00079DA9
		public virtual ImportDlpPolicyCollectionCommand SetParameters(ImportDlpPolicyCollectionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x0007BBB3 File Offset: 0x00079DB3
		public virtual ImportDlpPolicyCollectionCommand SetParameters(ImportDlpPolicyCollectionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005DC RID: 1500
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002D7A RID: 11642
			// (set) Token: 0x06004D7B RID: 19835 RVA: 0x0007BBBD File Offset: 0x00079DBD
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x17002D7B RID: 11643
			// (set) Token: 0x06004D7C RID: 19836 RVA: 0x0007BBD5 File Offset: 0x00079DD5
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002D7C RID: 11644
			// (set) Token: 0x06004D7D RID: 19837 RVA: 0x0007BBED File Offset: 0x00079DED
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002D7D RID: 11645
			// (set) Token: 0x06004D7E RID: 19838 RVA: 0x0007BC0B File Offset: 0x00079E0B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D7E RID: 11646
			// (set) Token: 0x06004D7F RID: 19839 RVA: 0x0007BC1E File Offset: 0x00079E1E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D7F RID: 11647
			// (set) Token: 0x06004D80 RID: 19840 RVA: 0x0007BC36 File Offset: 0x00079E36
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D80 RID: 11648
			// (set) Token: 0x06004D81 RID: 19841 RVA: 0x0007BC4E File Offset: 0x00079E4E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D81 RID: 11649
			// (set) Token: 0x06004D82 RID: 19842 RVA: 0x0007BC66 File Offset: 0x00079E66
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D82 RID: 11650
			// (set) Token: 0x06004D83 RID: 19843 RVA: 0x0007BC7E File Offset: 0x00079E7E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020005DD RID: 1501
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002D83 RID: 11651
			// (set) Token: 0x06004D85 RID: 19845 RVA: 0x0007BC9E File Offset: 0x00079E9E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DlpPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17002D84 RID: 11652
			// (set) Token: 0x06004D86 RID: 19846 RVA: 0x0007BCBC File Offset: 0x00079EBC
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x17002D85 RID: 11653
			// (set) Token: 0x06004D87 RID: 19847 RVA: 0x0007BCD4 File Offset: 0x00079ED4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002D86 RID: 11654
			// (set) Token: 0x06004D88 RID: 19848 RVA: 0x0007BCEC File Offset: 0x00079EEC
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002D87 RID: 11655
			// (set) Token: 0x06004D89 RID: 19849 RVA: 0x0007BD0A File Offset: 0x00079F0A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D88 RID: 11656
			// (set) Token: 0x06004D8A RID: 19850 RVA: 0x0007BD1D File Offset: 0x00079F1D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D89 RID: 11657
			// (set) Token: 0x06004D8B RID: 19851 RVA: 0x0007BD35 File Offset: 0x00079F35
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D8A RID: 11658
			// (set) Token: 0x06004D8C RID: 19852 RVA: 0x0007BD4D File Offset: 0x00079F4D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D8B RID: 11659
			// (set) Token: 0x06004D8D RID: 19853 RVA: 0x0007BD65 File Offset: 0x00079F65
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D8C RID: 11660
			// (set) Token: 0x06004D8E RID: 19854 RVA: 0x0007BD7D File Offset: 0x00079F7D
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

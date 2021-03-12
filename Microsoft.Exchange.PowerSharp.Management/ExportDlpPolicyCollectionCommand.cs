using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005DE RID: 1502
	public class ExportDlpPolicyCollectionCommand : SyntheticCommandWithPipelineInput<ADComplianceProgram, ADComplianceProgram>
	{
		// Token: 0x06004D90 RID: 19856 RVA: 0x0007BD9D File Offset: 0x00079F9D
		private ExportDlpPolicyCollectionCommand() : base("Export-DlpPolicyCollection")
		{
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x0007BDAA File Offset: 0x00079FAA
		public ExportDlpPolicyCollectionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x0007BDB9 File Offset: 0x00079FB9
		public virtual ExportDlpPolicyCollectionCommand SetParameters(ExportDlpPolicyCollectionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x0007BDC3 File Offset: 0x00079FC3
		public virtual ExportDlpPolicyCollectionCommand SetParameters(ExportDlpPolicyCollectionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005DF RID: 1503
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002D8D RID: 11661
			// (set) Token: 0x06004D94 RID: 19860 RVA: 0x0007BDCD File Offset: 0x00079FCD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002D8E RID: 11662
			// (set) Token: 0x06004D95 RID: 19861 RVA: 0x0007BDEB File Offset: 0x00079FEB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D8F RID: 11663
			// (set) Token: 0x06004D96 RID: 19862 RVA: 0x0007BDFE File Offset: 0x00079FFE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D90 RID: 11664
			// (set) Token: 0x06004D97 RID: 19863 RVA: 0x0007BE16 File Offset: 0x0007A016
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D91 RID: 11665
			// (set) Token: 0x06004D98 RID: 19864 RVA: 0x0007BE2E File Offset: 0x0007A02E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D92 RID: 11666
			// (set) Token: 0x06004D99 RID: 19865 RVA: 0x0007BE46 File Offset: 0x0007A046
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D93 RID: 11667
			// (set) Token: 0x06004D9A RID: 19866 RVA: 0x0007BE5E File Offset: 0x0007A05E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020005E0 RID: 1504
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002D94 RID: 11668
			// (set) Token: 0x06004D9C RID: 19868 RVA: 0x0007BE7E File Offset: 0x0007A07E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DlpPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17002D95 RID: 11669
			// (set) Token: 0x06004D9D RID: 19869 RVA: 0x0007BE9C File Offset: 0x0007A09C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002D96 RID: 11670
			// (set) Token: 0x06004D9E RID: 19870 RVA: 0x0007BEBA File Offset: 0x0007A0BA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D97 RID: 11671
			// (set) Token: 0x06004D9F RID: 19871 RVA: 0x0007BECD File Offset: 0x0007A0CD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D98 RID: 11672
			// (set) Token: 0x06004DA0 RID: 19872 RVA: 0x0007BEE5 File Offset: 0x0007A0E5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D99 RID: 11673
			// (set) Token: 0x06004DA1 RID: 19873 RVA: 0x0007BEFD File Offset: 0x0007A0FD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D9A RID: 11674
			// (set) Token: 0x06004DA2 RID: 19874 RVA: 0x0007BF15 File Offset: 0x0007A115
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D9B RID: 11675
			// (set) Token: 0x06004DA3 RID: 19875 RVA: 0x0007BF2D File Offset: 0x0007A12D
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

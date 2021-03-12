using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ClassificationDefinitions;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000504 RID: 1284
	public class GetClassificationRuleCollectionCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x060045D2 RID: 17874 RVA: 0x00072249 File Offset: 0x00070449
		private GetClassificationRuleCollectionCommand() : base("Get-ClassificationRuleCollection")
		{
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x00072256 File Offset: 0x00070456
		public GetClassificationRuleCollectionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x00072265 File Offset: 0x00070465
		public virtual GetClassificationRuleCollectionCommand SetParameters(GetClassificationRuleCollectionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060045D5 RID: 17877 RVA: 0x0007226F File Offset: 0x0007046F
		public virtual GetClassificationRuleCollectionCommand SetParameters(GetClassificationRuleCollectionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000505 RID: 1285
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002783 RID: 10115
			// (set) Token: 0x060045D6 RID: 17878 RVA: 0x00072279 File Offset: 0x00070479
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002784 RID: 10116
			// (set) Token: 0x060045D7 RID: 17879 RVA: 0x00072297 File Offset: 0x00070497
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002785 RID: 10117
			// (set) Token: 0x060045D8 RID: 17880 RVA: 0x000722AA File Offset: 0x000704AA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002786 RID: 10118
			// (set) Token: 0x060045D9 RID: 17881 RVA: 0x000722C2 File Offset: 0x000704C2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002787 RID: 10119
			// (set) Token: 0x060045DA RID: 17882 RVA: 0x000722DA File Offset: 0x000704DA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002788 RID: 10120
			// (set) Token: 0x060045DB RID: 17883 RVA: 0x000722F2 File Offset: 0x000704F2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000506 RID: 1286
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002789 RID: 10121
			// (set) Token: 0x060045DD RID: 17885 RVA: 0x00072312 File Offset: 0x00070512
			public virtual ClassificationRuleCollectionIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700278A RID: 10122
			// (set) Token: 0x060045DE RID: 17886 RVA: 0x00072325 File Offset: 0x00070525
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700278B RID: 10123
			// (set) Token: 0x060045DF RID: 17887 RVA: 0x00072343 File Offset: 0x00070543
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700278C RID: 10124
			// (set) Token: 0x060045E0 RID: 17888 RVA: 0x00072356 File Offset: 0x00070556
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700278D RID: 10125
			// (set) Token: 0x060045E1 RID: 17889 RVA: 0x0007236E File Offset: 0x0007056E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700278E RID: 10126
			// (set) Token: 0x060045E2 RID: 17890 RVA: 0x00072386 File Offset: 0x00070586
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700278F RID: 10127
			// (set) Token: 0x060045E3 RID: 17891 RVA: 0x0007239E File Offset: 0x0007059E
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

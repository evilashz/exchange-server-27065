using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ClassificationDefinitions;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000507 RID: 1287
	public class GetDataClassificationCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x060045E5 RID: 17893 RVA: 0x000723BE File Offset: 0x000705BE
		private GetDataClassificationCommand() : base("Get-DataClassification")
		{
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x000723CB File Offset: 0x000705CB
		public GetDataClassificationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x000723DA File Offset: 0x000705DA
		public virtual GetDataClassificationCommand SetParameters(GetDataClassificationCommand.RuleCollectionIdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x000723E4 File Offset: 0x000705E4
		public virtual GetDataClassificationCommand SetParameters(GetDataClassificationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x000723EE File Offset: 0x000705EE
		public virtual GetDataClassificationCommand SetParameters(GetDataClassificationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000508 RID: 1288
		public class RuleCollectionIdentityParameters : ParametersBase
		{
			// Token: 0x17002790 RID: 10128
			// (set) Token: 0x060045EA RID: 17898 RVA: 0x000723F8 File Offset: 0x000705F8
			public virtual ClassificationRuleCollectionIdParameter ClassificationRuleCollectionIdentity
			{
				set
				{
					base.PowerSharpParameters["ClassificationRuleCollectionIdentity"] = value;
				}
			}

			// Token: 0x17002791 RID: 10129
			// (set) Token: 0x060045EB RID: 17899 RVA: 0x0007240B File Offset: 0x0007060B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002792 RID: 10130
			// (set) Token: 0x060045EC RID: 17900 RVA: 0x00072429 File Offset: 0x00070629
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002793 RID: 10131
			// (set) Token: 0x060045ED RID: 17901 RVA: 0x0007243C File Offset: 0x0007063C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002794 RID: 10132
			// (set) Token: 0x060045EE RID: 17902 RVA: 0x00072454 File Offset: 0x00070654
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002795 RID: 10133
			// (set) Token: 0x060045EF RID: 17903 RVA: 0x0007246C File Offset: 0x0007066C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002796 RID: 10134
			// (set) Token: 0x060045F0 RID: 17904 RVA: 0x00072484 File Offset: 0x00070684
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000509 RID: 1289
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002797 RID: 10135
			// (set) Token: 0x060045F2 RID: 17906 RVA: 0x000724A4 File Offset: 0x000706A4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002798 RID: 10136
			// (set) Token: 0x060045F3 RID: 17907 RVA: 0x000724C2 File Offset: 0x000706C2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002799 RID: 10137
			// (set) Token: 0x060045F4 RID: 17908 RVA: 0x000724D5 File Offset: 0x000706D5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700279A RID: 10138
			// (set) Token: 0x060045F5 RID: 17909 RVA: 0x000724ED File Offset: 0x000706ED
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700279B RID: 10139
			// (set) Token: 0x060045F6 RID: 17910 RVA: 0x00072505 File Offset: 0x00070705
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700279C RID: 10140
			// (set) Token: 0x060045F7 RID: 17911 RVA: 0x0007251D File Offset: 0x0007071D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200050A RID: 1290
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700279D RID: 10141
			// (set) Token: 0x060045F9 RID: 17913 RVA: 0x0007253D File Offset: 0x0007073D
			public virtual DataClassificationIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700279E RID: 10142
			// (set) Token: 0x060045FA RID: 17914 RVA: 0x00072550 File Offset: 0x00070750
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700279F RID: 10143
			// (set) Token: 0x060045FB RID: 17915 RVA: 0x0007256E File Offset: 0x0007076E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170027A0 RID: 10144
			// (set) Token: 0x060045FC RID: 17916 RVA: 0x00072581 File Offset: 0x00070781
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170027A1 RID: 10145
			// (set) Token: 0x060045FD RID: 17917 RVA: 0x00072599 File Offset: 0x00070799
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170027A2 RID: 10146
			// (set) Token: 0x060045FE RID: 17918 RVA: 0x000725B1 File Offset: 0x000707B1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170027A3 RID: 10147
			// (set) Token: 0x060045FF RID: 17919 RVA: 0x000725C9 File Offset: 0x000707C9
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

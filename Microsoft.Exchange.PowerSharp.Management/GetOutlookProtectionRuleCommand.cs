using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200082F RID: 2095
	public class GetOutlookProtectionRuleCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x0600688D RID: 26765 RVA: 0x0009F282 File Offset: 0x0009D482
		private GetOutlookProtectionRuleCommand() : base("Get-OutlookProtectionRule")
		{
		}

		// Token: 0x0600688E RID: 26766 RVA: 0x0009F28F File Offset: 0x0009D48F
		public GetOutlookProtectionRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600688F RID: 26767 RVA: 0x0009F29E File Offset: 0x0009D49E
		public virtual GetOutlookProtectionRuleCommand SetParameters(GetOutlookProtectionRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006890 RID: 26768 RVA: 0x0009F2A8 File Offset: 0x0009D4A8
		public virtual GetOutlookProtectionRuleCommand SetParameters(GetOutlookProtectionRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000830 RID: 2096
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170043E8 RID: 17384
			// (set) Token: 0x06006891 RID: 26769 RVA: 0x0009F2B2 File Offset: 0x0009D4B2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170043E9 RID: 17385
			// (set) Token: 0x06006892 RID: 26770 RVA: 0x0009F2D0 File Offset: 0x0009D4D0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170043EA RID: 17386
			// (set) Token: 0x06006893 RID: 26771 RVA: 0x0009F2E3 File Offset: 0x0009D4E3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170043EB RID: 17387
			// (set) Token: 0x06006894 RID: 26772 RVA: 0x0009F2FB File Offset: 0x0009D4FB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170043EC RID: 17388
			// (set) Token: 0x06006895 RID: 26773 RVA: 0x0009F313 File Offset: 0x0009D513
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170043ED RID: 17389
			// (set) Token: 0x06006896 RID: 26774 RVA: 0x0009F32B File Offset: 0x0009D52B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000831 RID: 2097
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170043EE RID: 17390
			// (set) Token: 0x06006898 RID: 26776 RVA: 0x0009F34B File Offset: 0x0009D54B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x170043EF RID: 17391
			// (set) Token: 0x06006899 RID: 26777 RVA: 0x0009F369 File Offset: 0x0009D569
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170043F0 RID: 17392
			// (set) Token: 0x0600689A RID: 26778 RVA: 0x0009F387 File Offset: 0x0009D587
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170043F1 RID: 17393
			// (set) Token: 0x0600689B RID: 26779 RVA: 0x0009F39A File Offset: 0x0009D59A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170043F2 RID: 17394
			// (set) Token: 0x0600689C RID: 26780 RVA: 0x0009F3B2 File Offset: 0x0009D5B2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170043F3 RID: 17395
			// (set) Token: 0x0600689D RID: 26781 RVA: 0x0009F3CA File Offset: 0x0009D5CA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170043F4 RID: 17396
			// (set) Token: 0x0600689E RID: 26782 RVA: 0x0009F3E2 File Offset: 0x0009D5E2
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

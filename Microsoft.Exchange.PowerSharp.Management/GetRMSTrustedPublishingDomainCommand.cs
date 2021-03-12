using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003E5 RID: 997
	public class GetRMSTrustedPublishingDomainCommand : SyntheticCommandWithPipelineInput<RMSTrustedPublishingDomain, RMSTrustedPublishingDomain>
	{
		// Token: 0x06003B73 RID: 15219 RVA: 0x00064EED File Offset: 0x000630ED
		private GetRMSTrustedPublishingDomainCommand() : base("Get-RMSTrustedPublishingDomain")
		{
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x00064EFA File Offset: 0x000630FA
		public GetRMSTrustedPublishingDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x00064F09 File Offset: 0x00063109
		public virtual GetRMSTrustedPublishingDomainCommand SetParameters(GetRMSTrustedPublishingDomainCommand.OrganizationSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x00064F13 File Offset: 0x00063113
		public virtual GetRMSTrustedPublishingDomainCommand SetParameters(GetRMSTrustedPublishingDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003E6 RID: 998
		public class OrganizationSetParameters : ParametersBase
		{
			// Token: 0x17001F62 RID: 8034
			// (set) Token: 0x06003B77 RID: 15223 RVA: 0x00064F1D File Offset: 0x0006311D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F63 RID: 8035
			// (set) Token: 0x06003B78 RID: 15224 RVA: 0x00064F3B File Offset: 0x0006313B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RmsTrustedPublishingDomainIdParameter(value) : null);
				}
			}

			// Token: 0x17001F64 RID: 8036
			// (set) Token: 0x06003B79 RID: 15225 RVA: 0x00064F59 File Offset: 0x00063159
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x17001F65 RID: 8037
			// (set) Token: 0x06003B7A RID: 15226 RVA: 0x00064F71 File Offset: 0x00063171
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001F66 RID: 8038
			// (set) Token: 0x06003B7B RID: 15227 RVA: 0x00064F84 File Offset: 0x00063184
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F67 RID: 8039
			// (set) Token: 0x06003B7C RID: 15228 RVA: 0x00064F9C File Offset: 0x0006319C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F68 RID: 8040
			// (set) Token: 0x06003B7D RID: 15229 RVA: 0x00064FB4 File Offset: 0x000631B4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F69 RID: 8041
			// (set) Token: 0x06003B7E RID: 15230 RVA: 0x00064FCC File Offset: 0x000631CC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020003E7 RID: 999
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001F6A RID: 8042
			// (set) Token: 0x06003B80 RID: 15232 RVA: 0x00064FEC File Offset: 0x000631EC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RmsTrustedPublishingDomainIdParameter(value) : null);
				}
			}

			// Token: 0x17001F6B RID: 8043
			// (set) Token: 0x06003B81 RID: 15233 RVA: 0x0006500A File Offset: 0x0006320A
			public virtual SwitchParameter Default
			{
				set
				{
					base.PowerSharpParameters["Default"] = value;
				}
			}

			// Token: 0x17001F6C RID: 8044
			// (set) Token: 0x06003B82 RID: 15234 RVA: 0x00065022 File Offset: 0x00063222
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001F6D RID: 8045
			// (set) Token: 0x06003B83 RID: 15235 RVA: 0x00065035 File Offset: 0x00063235
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F6E RID: 8046
			// (set) Token: 0x06003B84 RID: 15236 RVA: 0x0006504D File Offset: 0x0006324D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F6F RID: 8047
			// (set) Token: 0x06003B85 RID: 15237 RVA: 0x00065065 File Offset: 0x00063265
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F70 RID: 8048
			// (set) Token: 0x06003B86 RID: 15238 RVA: 0x0006507D File Offset: 0x0006327D
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

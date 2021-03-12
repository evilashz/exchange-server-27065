using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200060C RID: 1548
	public class GetExchangeAssistanceConfigCommand : SyntheticCommandWithPipelineInput<ExchangeAssistance, ExchangeAssistance>
	{
		// Token: 0x06004F81 RID: 20353 RVA: 0x0007E5BD File Offset: 0x0007C7BD
		private GetExchangeAssistanceConfigCommand() : base("Get-ExchangeAssistanceConfig")
		{
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x0007E5CA File Offset: 0x0007C7CA
		public GetExchangeAssistanceConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x0007E5D9 File Offset: 0x0007C7D9
		public virtual GetExchangeAssistanceConfigCommand SetParameters(GetExchangeAssistanceConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x0007E5E3 File Offset: 0x0007C7E3
		public virtual GetExchangeAssistanceConfigCommand SetParameters(GetExchangeAssistanceConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200060D RID: 1549
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002F22 RID: 12066
			// (set) Token: 0x06004F85 RID: 20357 RVA: 0x0007E5ED File Offset: 0x0007C7ED
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002F23 RID: 12067
			// (set) Token: 0x06004F86 RID: 20358 RVA: 0x0007E60B File Offset: 0x0007C80B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F24 RID: 12068
			// (set) Token: 0x06004F87 RID: 20359 RVA: 0x0007E61E File Offset: 0x0007C81E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F25 RID: 12069
			// (set) Token: 0x06004F88 RID: 20360 RVA: 0x0007E636 File Offset: 0x0007C836
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F26 RID: 12070
			// (set) Token: 0x06004F89 RID: 20361 RVA: 0x0007E64E File Offset: 0x0007C84E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F27 RID: 12071
			// (set) Token: 0x06004F8A RID: 20362 RVA: 0x0007E666 File Offset: 0x0007C866
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200060E RID: 1550
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002F28 RID: 12072
			// (set) Token: 0x06004F8C RID: 20364 RVA: 0x0007E686 File Offset: 0x0007C886
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F29 RID: 12073
			// (set) Token: 0x06004F8D RID: 20365 RVA: 0x0007E699 File Offset: 0x0007C899
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F2A RID: 12074
			// (set) Token: 0x06004F8E RID: 20366 RVA: 0x0007E6B1 File Offset: 0x0007C8B1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F2B RID: 12075
			// (set) Token: 0x06004F8F RID: 20367 RVA: 0x0007E6C9 File Offset: 0x0007C8C9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F2C RID: 12076
			// (set) Token: 0x06004F90 RID: 20368 RVA: 0x0007E6E1 File Offset: 0x0007C8E1
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

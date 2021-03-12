using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003DF RID: 991
	public class GetIRMConfigurationCommand : SyntheticCommandWithPipelineInput<IRMConfiguration, IRMConfiguration>
	{
		// Token: 0x06003B49 RID: 15177 RVA: 0x00064B8D File Offset: 0x00062D8D
		private GetIRMConfigurationCommand() : base("Get-IRMConfiguration")
		{
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x00064B9A File Offset: 0x00062D9A
		public GetIRMConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x00064BA9 File Offset: 0x00062DA9
		public virtual GetIRMConfigurationCommand SetParameters(GetIRMConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x00064BB3 File Offset: 0x00062DB3
		public virtual GetIRMConfigurationCommand SetParameters(GetIRMConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003E0 RID: 992
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001F44 RID: 8004
			// (set) Token: 0x06003B4D RID: 15181 RVA: 0x00064BBD File Offset: 0x00062DBD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F45 RID: 8005
			// (set) Token: 0x06003B4E RID: 15182 RVA: 0x00064BDB File Offset: 0x00062DDB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001F46 RID: 8006
			// (set) Token: 0x06003B4F RID: 15183 RVA: 0x00064BEE File Offset: 0x00062DEE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F47 RID: 8007
			// (set) Token: 0x06003B50 RID: 15184 RVA: 0x00064C06 File Offset: 0x00062E06
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F48 RID: 8008
			// (set) Token: 0x06003B51 RID: 15185 RVA: 0x00064C1E File Offset: 0x00062E1E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F49 RID: 8009
			// (set) Token: 0x06003B52 RID: 15186 RVA: 0x00064C36 File Offset: 0x00062E36
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020003E1 RID: 993
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001F4A RID: 8010
			// (set) Token: 0x06003B54 RID: 15188 RVA: 0x00064C56 File Offset: 0x00062E56
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001F4B RID: 8011
			// (set) Token: 0x06003B55 RID: 15189 RVA: 0x00064C69 File Offset: 0x00062E69
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F4C RID: 8012
			// (set) Token: 0x06003B56 RID: 15190 RVA: 0x00064C81 File Offset: 0x00062E81
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F4D RID: 8013
			// (set) Token: 0x06003B57 RID: 15191 RVA: 0x00064C99 File Offset: 0x00062E99
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F4E RID: 8014
			// (set) Token: 0x06003B58 RID: 15192 RVA: 0x00064CB1 File Offset: 0x00062EB1
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

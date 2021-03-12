using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.E4E;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200010A RID: 266
	public class GetOMEConfigurationCommand : SyntheticCommandWithPipelineInput<EncryptionConfiguration, EncryptionConfiguration>
	{
		// Token: 0x06001F02 RID: 7938 RVA: 0x0003FF38 File Offset: 0x0003E138
		private GetOMEConfigurationCommand() : base("Get-OMEConfiguration")
		{
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0003FF45 File Offset: 0x0003E145
		public GetOMEConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0003FF54 File Offset: 0x0003E154
		public virtual GetOMEConfigurationCommand SetParameters(GetOMEConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0003FF5E File Offset: 0x0003E15E
		public virtual GetOMEConfigurationCommand SetParameters(GetOMEConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200010B RID: 267
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170008A7 RID: 2215
			// (set) Token: 0x06001F06 RID: 7942 RVA: 0x0003FF68 File Offset: 0x0003E168
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170008A8 RID: 2216
			// (set) Token: 0x06001F07 RID: 7943 RVA: 0x0003FF86 File Offset: 0x0003E186
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170008A9 RID: 2217
			// (set) Token: 0x06001F08 RID: 7944 RVA: 0x0003FF99 File Offset: 0x0003E199
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170008AA RID: 2218
			// (set) Token: 0x06001F09 RID: 7945 RVA: 0x0003FFB1 File Offset: 0x0003E1B1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170008AB RID: 2219
			// (set) Token: 0x06001F0A RID: 7946 RVA: 0x0003FFC9 File Offset: 0x0003E1C9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170008AC RID: 2220
			// (set) Token: 0x06001F0B RID: 7947 RVA: 0x0003FFE1 File Offset: 0x0003E1E1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200010C RID: 268
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170008AD RID: 2221
			// (set) Token: 0x06001F0D RID: 7949 RVA: 0x00040001 File Offset: 0x0003E201
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OMEConfigurationIdParameter(value) : null);
				}
			}

			// Token: 0x170008AE RID: 2222
			// (set) Token: 0x06001F0E RID: 7950 RVA: 0x0004001F File Offset: 0x0003E21F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170008AF RID: 2223
			// (set) Token: 0x06001F0F RID: 7951 RVA: 0x0004003D File Offset: 0x0003E23D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170008B0 RID: 2224
			// (set) Token: 0x06001F10 RID: 7952 RVA: 0x00040050 File Offset: 0x0003E250
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170008B1 RID: 2225
			// (set) Token: 0x06001F11 RID: 7953 RVA: 0x00040068 File Offset: 0x0003E268
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170008B2 RID: 2226
			// (set) Token: 0x06001F12 RID: 7954 RVA: 0x00040080 File Offset: 0x0003E280
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170008B3 RID: 2227
			// (set) Token: 0x06001F13 RID: 7955 RVA: 0x00040098 File Offset: 0x0003E298
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

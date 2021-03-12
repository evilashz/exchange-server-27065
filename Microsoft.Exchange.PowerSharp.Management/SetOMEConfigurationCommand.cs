using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.E4E;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200010D RID: 269
	public class SetOMEConfigurationCommand : SyntheticCommandWithPipelineInputNoOutput<EncryptionConfiguration>
	{
		// Token: 0x06001F15 RID: 7957 RVA: 0x000400B8 File Offset: 0x0003E2B8
		private SetOMEConfigurationCommand() : base("Set-OMEConfiguration")
		{
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x000400C5 File Offset: 0x0003E2C5
		public SetOMEConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x000400D4 File Offset: 0x0003E2D4
		public virtual SetOMEConfigurationCommand SetParameters(SetOMEConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000400DE File Offset: 0x0003E2DE
		public virtual SetOMEConfigurationCommand SetParameters(SetOMEConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200010E RID: 270
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170008B4 RID: 2228
			// (set) Token: 0x06001F19 RID: 7961 RVA: 0x000400E8 File Offset: 0x0003E2E8
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170008B5 RID: 2229
			// (set) Token: 0x06001F1A RID: 7962 RVA: 0x00040106 File Offset: 0x0003E306
			public virtual byte Image
			{
				set
				{
					base.PowerSharpParameters["Image"] = value;
				}
			}

			// Token: 0x170008B6 RID: 2230
			// (set) Token: 0x06001F1B RID: 7963 RVA: 0x0004011E File Offset: 0x0003E31E
			public virtual string EmailText
			{
				set
				{
					base.PowerSharpParameters["EmailText"] = value;
				}
			}

			// Token: 0x170008B7 RID: 2231
			// (set) Token: 0x06001F1C RID: 7964 RVA: 0x00040131 File Offset: 0x0003E331
			public virtual string PortalText
			{
				set
				{
					base.PowerSharpParameters["PortalText"] = value;
				}
			}

			// Token: 0x170008B8 RID: 2232
			// (set) Token: 0x06001F1D RID: 7965 RVA: 0x00040144 File Offset: 0x0003E344
			public virtual string DisclaimerText
			{
				set
				{
					base.PowerSharpParameters["DisclaimerText"] = value;
				}
			}

			// Token: 0x170008B9 RID: 2233
			// (set) Token: 0x06001F1E RID: 7966 RVA: 0x00040157 File Offset: 0x0003E357
			public virtual bool OTPEnabled
			{
				set
				{
					base.PowerSharpParameters["OTPEnabled"] = value;
				}
			}

			// Token: 0x170008BA RID: 2234
			// (set) Token: 0x06001F1F RID: 7967 RVA: 0x0004016F File Offset: 0x0003E36F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170008BB RID: 2235
			// (set) Token: 0x06001F20 RID: 7968 RVA: 0x00040182 File Offset: 0x0003E382
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170008BC RID: 2236
			// (set) Token: 0x06001F21 RID: 7969 RVA: 0x0004019A File Offset: 0x0003E39A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170008BD RID: 2237
			// (set) Token: 0x06001F22 RID: 7970 RVA: 0x000401B2 File Offset: 0x0003E3B2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170008BE RID: 2238
			// (set) Token: 0x06001F23 RID: 7971 RVA: 0x000401CA File Offset: 0x0003E3CA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200010F RID: 271
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170008BF RID: 2239
			// (set) Token: 0x06001F25 RID: 7973 RVA: 0x000401EA File Offset: 0x0003E3EA
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OMEConfigurationIdParameter(value) : null);
				}
			}

			// Token: 0x170008C0 RID: 2240
			// (set) Token: 0x06001F26 RID: 7974 RVA: 0x00040208 File Offset: 0x0003E408
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170008C1 RID: 2241
			// (set) Token: 0x06001F27 RID: 7975 RVA: 0x00040226 File Offset: 0x0003E426
			public virtual byte Image
			{
				set
				{
					base.PowerSharpParameters["Image"] = value;
				}
			}

			// Token: 0x170008C2 RID: 2242
			// (set) Token: 0x06001F28 RID: 7976 RVA: 0x0004023E File Offset: 0x0003E43E
			public virtual string EmailText
			{
				set
				{
					base.PowerSharpParameters["EmailText"] = value;
				}
			}

			// Token: 0x170008C3 RID: 2243
			// (set) Token: 0x06001F29 RID: 7977 RVA: 0x00040251 File Offset: 0x0003E451
			public virtual string PortalText
			{
				set
				{
					base.PowerSharpParameters["PortalText"] = value;
				}
			}

			// Token: 0x170008C4 RID: 2244
			// (set) Token: 0x06001F2A RID: 7978 RVA: 0x00040264 File Offset: 0x0003E464
			public virtual string DisclaimerText
			{
				set
				{
					base.PowerSharpParameters["DisclaimerText"] = value;
				}
			}

			// Token: 0x170008C5 RID: 2245
			// (set) Token: 0x06001F2B RID: 7979 RVA: 0x00040277 File Offset: 0x0003E477
			public virtual bool OTPEnabled
			{
				set
				{
					base.PowerSharpParameters["OTPEnabled"] = value;
				}
			}

			// Token: 0x170008C6 RID: 2246
			// (set) Token: 0x06001F2C RID: 7980 RVA: 0x0004028F File Offset: 0x0003E48F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170008C7 RID: 2247
			// (set) Token: 0x06001F2D RID: 7981 RVA: 0x000402A2 File Offset: 0x0003E4A2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170008C8 RID: 2248
			// (set) Token: 0x06001F2E RID: 7982 RVA: 0x000402BA File Offset: 0x0003E4BA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170008C9 RID: 2249
			// (set) Token: 0x06001F2F RID: 7983 RVA: 0x000402D2 File Offset: 0x0003E4D2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170008CA RID: 2250
			// (set) Token: 0x06001F30 RID: 7984 RVA: 0x000402EA File Offset: 0x0003E4EA
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

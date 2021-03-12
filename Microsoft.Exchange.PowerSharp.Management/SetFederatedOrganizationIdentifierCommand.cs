using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200069D RID: 1693
	public class SetFederatedOrganizationIdentifierCommand : SyntheticCommandWithPipelineInputNoOutput<FederatedOrganizationId>
	{
		// Token: 0x06005994 RID: 22932 RVA: 0x0008BFC6 File Offset: 0x0008A1C6
		private SetFederatedOrganizationIdentifierCommand() : base("Set-FederatedOrganizationIdentifier")
		{
		}

		// Token: 0x06005995 RID: 22933 RVA: 0x0008BFD3 File Offset: 0x0008A1D3
		public SetFederatedOrganizationIdentifierCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005996 RID: 22934 RVA: 0x0008BFE2 File Offset: 0x0008A1E2
		public virtual SetFederatedOrganizationIdentifierCommand SetParameters(SetFederatedOrganizationIdentifierCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005997 RID: 22935 RVA: 0x0008BFEC File Offset: 0x0008A1EC
		public virtual SetFederatedOrganizationIdentifierCommand SetParameters(SetFederatedOrganizationIdentifierCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200069E RID: 1694
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003813 RID: 14355
			// (set) Token: 0x06005998 RID: 22936 RVA: 0x0008BFF6 File Offset: 0x0008A1F6
			public virtual FederationTrustIdParameter DelegationFederationTrust
			{
				set
				{
					base.PowerSharpParameters["DelegationFederationTrust"] = value;
				}
			}

			// Token: 0x17003814 RID: 14356
			// (set) Token: 0x06005999 RID: 22937 RVA: 0x0008C009 File Offset: 0x0008A209
			public virtual SmtpDomain AccountNamespace
			{
				set
				{
					base.PowerSharpParameters["AccountNamespace"] = value;
				}
			}

			// Token: 0x17003815 RID: 14357
			// (set) Token: 0x0600599A RID: 22938 RVA: 0x0008C01C File Offset: 0x0008A21C
			public virtual SmtpDomain DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x17003816 RID: 14358
			// (set) Token: 0x0600599B RID: 22939 RVA: 0x0008C02F File Offset: 0x0008A22F
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003817 RID: 14359
			// (set) Token: 0x0600599C RID: 22940 RVA: 0x0008C047 File Offset: 0x0008A247
			public virtual SmtpAddress OrganizationContact
			{
				set
				{
					base.PowerSharpParameters["OrganizationContact"] = value;
				}
			}

			// Token: 0x17003818 RID: 14360
			// (set) Token: 0x0600599D RID: 22941 RVA: 0x0008C05F File Offset: 0x0008A25F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003819 RID: 14361
			// (set) Token: 0x0600599E RID: 22942 RVA: 0x0008C072 File Offset: 0x0008A272
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700381A RID: 14362
			// (set) Token: 0x0600599F RID: 22943 RVA: 0x0008C08A File Offset: 0x0008A28A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700381B RID: 14363
			// (set) Token: 0x060059A0 RID: 22944 RVA: 0x0008C0A2 File Offset: 0x0008A2A2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700381C RID: 14364
			// (set) Token: 0x060059A1 RID: 22945 RVA: 0x0008C0BA File Offset: 0x0008A2BA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700381D RID: 14365
			// (set) Token: 0x060059A2 RID: 22946 RVA: 0x0008C0D2 File Offset: 0x0008A2D2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200069F RID: 1695
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700381E RID: 14366
			// (set) Token: 0x060059A4 RID: 22948 RVA: 0x0008C0F2 File Offset: 0x0008A2F2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700381F RID: 14367
			// (set) Token: 0x060059A5 RID: 22949 RVA: 0x0008C110 File Offset: 0x0008A310
			public virtual FederationTrustIdParameter DelegationFederationTrust
			{
				set
				{
					base.PowerSharpParameters["DelegationFederationTrust"] = value;
				}
			}

			// Token: 0x17003820 RID: 14368
			// (set) Token: 0x060059A6 RID: 22950 RVA: 0x0008C123 File Offset: 0x0008A323
			public virtual SmtpDomain AccountNamespace
			{
				set
				{
					base.PowerSharpParameters["AccountNamespace"] = value;
				}
			}

			// Token: 0x17003821 RID: 14369
			// (set) Token: 0x060059A7 RID: 22951 RVA: 0x0008C136 File Offset: 0x0008A336
			public virtual SmtpDomain DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x17003822 RID: 14370
			// (set) Token: 0x060059A8 RID: 22952 RVA: 0x0008C149 File Offset: 0x0008A349
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003823 RID: 14371
			// (set) Token: 0x060059A9 RID: 22953 RVA: 0x0008C161 File Offset: 0x0008A361
			public virtual SmtpAddress OrganizationContact
			{
				set
				{
					base.PowerSharpParameters["OrganizationContact"] = value;
				}
			}

			// Token: 0x17003824 RID: 14372
			// (set) Token: 0x060059AA RID: 22954 RVA: 0x0008C179 File Offset: 0x0008A379
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003825 RID: 14373
			// (set) Token: 0x060059AB RID: 22955 RVA: 0x0008C18C File Offset: 0x0008A38C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003826 RID: 14374
			// (set) Token: 0x060059AC RID: 22956 RVA: 0x0008C1A4 File Offset: 0x0008A3A4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003827 RID: 14375
			// (set) Token: 0x060059AD RID: 22957 RVA: 0x0008C1BC File Offset: 0x0008A3BC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003828 RID: 14376
			// (set) Token: 0x060059AE RID: 22958 RVA: 0x0008C1D4 File Offset: 0x0008A3D4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003829 RID: 14377
			// (set) Token: 0x060059AF RID: 22959 RVA: 0x0008C1EC File Offset: 0x0008A3EC
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

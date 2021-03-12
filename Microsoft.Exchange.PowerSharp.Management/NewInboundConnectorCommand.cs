using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008A3 RID: 2211
	public class NewInboundConnectorCommand : SyntheticCommandWithPipelineInput<TenantInboundConnector, TenantInboundConnector>
	{
		// Token: 0x06006D91 RID: 28049 RVA: 0x000A5B75 File Offset: 0x000A3D75
		private NewInboundConnectorCommand() : base("New-InboundConnector")
		{
		}

		// Token: 0x06006D92 RID: 28050 RVA: 0x000A5B82 File Offset: 0x000A3D82
		public NewInboundConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006D93 RID: 28051 RVA: 0x000A5B91 File Offset: 0x000A3D91
		public virtual NewInboundConnectorCommand SetParameters(NewInboundConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008A4 RID: 2212
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004804 RID: 18436
			// (set) Token: 0x06006D94 RID: 28052 RVA: 0x000A5B9B File Offset: 0x000A3D9B
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004805 RID: 18437
			// (set) Token: 0x06006D95 RID: 28053 RVA: 0x000A5BB3 File Offset: 0x000A3DB3
			public virtual TenantConnectorType ConnectorType
			{
				set
				{
					base.PowerSharpParameters["ConnectorType"] = value;
				}
			}

			// Token: 0x17004806 RID: 18438
			// (set) Token: 0x06006D96 RID: 28054 RVA: 0x000A5BCB File Offset: 0x000A3DCB
			public virtual TenantConnectorSource ConnectorSource
			{
				set
				{
					base.PowerSharpParameters["ConnectorSource"] = value;
				}
			}

			// Token: 0x17004807 RID: 18439
			// (set) Token: 0x06006D97 RID: 28055 RVA: 0x000A5BE3 File Offset: 0x000A3DE3
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004808 RID: 18440
			// (set) Token: 0x06006D98 RID: 28056 RVA: 0x000A5BF6 File Offset: 0x000A3DF6
			public virtual MultiValuedProperty<IPRange> SenderIPAddresses
			{
				set
				{
					base.PowerSharpParameters["SenderIPAddresses"] = value;
				}
			}

			// Token: 0x17004809 RID: 18441
			// (set) Token: 0x06006D99 RID: 28057 RVA: 0x000A5C09 File Offset: 0x000A3E09
			public virtual MultiValuedProperty<AddressSpace> SenderDomains
			{
				set
				{
					base.PowerSharpParameters["SenderDomains"] = value;
				}
			}

			// Token: 0x1700480A RID: 18442
			// (set) Token: 0x06006D9A RID: 28058 RVA: 0x000A5C1C File Offset: 0x000A3E1C
			public virtual bool RequireTls
			{
				set
				{
					base.PowerSharpParameters["RequireTls"] = value;
				}
			}

			// Token: 0x1700480B RID: 18443
			// (set) Token: 0x06006D9B RID: 28059 RVA: 0x000A5C34 File Offset: 0x000A3E34
			public virtual bool RestrictDomainsToCertificate
			{
				set
				{
					base.PowerSharpParameters["RestrictDomainsToCertificate"] = value;
				}
			}

			// Token: 0x1700480C RID: 18444
			// (set) Token: 0x06006D9C RID: 28060 RVA: 0x000A5C4C File Offset: 0x000A3E4C
			public virtual bool RestrictDomainsToIPAddresses
			{
				set
				{
					base.PowerSharpParameters["RestrictDomainsToIPAddresses"] = value;
				}
			}

			// Token: 0x1700480D RID: 18445
			// (set) Token: 0x06006D9D RID: 28061 RVA: 0x000A5C64 File Offset: 0x000A3E64
			public virtual bool CloudServicesMailEnabled
			{
				set
				{
					base.PowerSharpParameters["CloudServicesMailEnabled"] = value;
				}
			}

			// Token: 0x1700480E RID: 18446
			// (set) Token: 0x06006D9E RID: 28062 RVA: 0x000A5C7C File Offset: 0x000A3E7C
			public virtual TlsCertificate TlsSenderCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsSenderCertificateName"] = value;
				}
			}

			// Token: 0x1700480F RID: 18447
			// (set) Token: 0x06006D9F RID: 28063 RVA: 0x000A5C8F File Offset: 0x000A3E8F
			public virtual MultiValuedProperty<AcceptedDomainIdParameter> AssociatedAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["AssociatedAcceptedDomains"] = value;
				}
			}

			// Token: 0x17004810 RID: 18448
			// (set) Token: 0x06006DA0 RID: 28064 RVA: 0x000A5CA2 File Offset: 0x000A3EA2
			public virtual bool BypassValidation
			{
				set
				{
					base.PowerSharpParameters["BypassValidation"] = value;
				}
			}

			// Token: 0x17004811 RID: 18449
			// (set) Token: 0x06006DA1 RID: 28065 RVA: 0x000A5CBA File Offset: 0x000A3EBA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004812 RID: 18450
			// (set) Token: 0x06006DA2 RID: 28066 RVA: 0x000A5CD8 File Offset: 0x000A3ED8
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004813 RID: 18451
			// (set) Token: 0x06006DA3 RID: 28067 RVA: 0x000A5CEB File Offset: 0x000A3EEB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004814 RID: 18452
			// (set) Token: 0x06006DA4 RID: 28068 RVA: 0x000A5CFE File Offset: 0x000A3EFE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004815 RID: 18453
			// (set) Token: 0x06006DA5 RID: 28069 RVA: 0x000A5D16 File Offset: 0x000A3F16
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004816 RID: 18454
			// (set) Token: 0x06006DA6 RID: 28070 RVA: 0x000A5D2E File Offset: 0x000A3F2E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004817 RID: 18455
			// (set) Token: 0x06006DA7 RID: 28071 RVA: 0x000A5D46 File Offset: 0x000A3F46
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004818 RID: 18456
			// (set) Token: 0x06006DA8 RID: 28072 RVA: 0x000A5D5E File Offset: 0x000A3F5E
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

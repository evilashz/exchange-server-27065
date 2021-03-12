using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000591 RID: 1425
	public class SetHybridConfigurationCommand : SyntheticCommandWithPipelineInputNoOutput<HybridConfiguration>
	{
		// Token: 0x06004A99 RID: 19097 RVA: 0x000781C2 File Offset: 0x000763C2
		private SetHybridConfigurationCommand() : base("Set-HybridConfiguration")
		{
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x000781CF File Offset: 0x000763CF
		public SetHybridConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x000781DE File Offset: 0x000763DE
		public virtual SetHybridConfigurationCommand SetParameters(SetHybridConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000592 RID: 1426
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002B30 RID: 11056
			// (set) Token: 0x06004A9C RID: 19100 RVA: 0x000781E8 File Offset: 0x000763E8
			public virtual MultiValuedProperty<ServerIdParameter> ClientAccessServers
			{
				set
				{
					base.PowerSharpParameters["ClientAccessServers"] = value;
				}
			}

			// Token: 0x17002B31 RID: 11057
			// (set) Token: 0x06004A9D RID: 19101 RVA: 0x000781FB File Offset: 0x000763FB
			public virtual MultiValuedProperty<ServerIdParameter> ReceivingTransportServers
			{
				set
				{
					base.PowerSharpParameters["ReceivingTransportServers"] = value;
				}
			}

			// Token: 0x17002B32 RID: 11058
			// (set) Token: 0x06004A9E RID: 19102 RVA: 0x0007820E File Offset: 0x0007640E
			public virtual MultiValuedProperty<ServerIdParameter> SendingTransportServers
			{
				set
				{
					base.PowerSharpParameters["SendingTransportServers"] = value;
				}
			}

			// Token: 0x17002B33 RID: 11059
			// (set) Token: 0x06004A9F RID: 19103 RVA: 0x00078221 File Offset: 0x00076421
			public virtual MultiValuedProperty<ServerIdParameter> EdgeTransportServers
			{
				set
				{
					base.PowerSharpParameters["EdgeTransportServers"] = value;
				}
			}

			// Token: 0x17002B34 RID: 11060
			// (set) Token: 0x06004AA0 RID: 19104 RVA: 0x00078234 File Offset: 0x00076434
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x17002B35 RID: 11061
			// (set) Token: 0x06004AA1 RID: 19105 RVA: 0x00078247 File Offset: 0x00076447
			public virtual SmtpDomain OnPremisesSmartHost
			{
				set
				{
					base.PowerSharpParameters["OnPremisesSmartHost"] = value;
				}
			}

			// Token: 0x17002B36 RID: 11062
			// (set) Token: 0x06004AA2 RID: 19106 RVA: 0x0007825A File Offset: 0x0007645A
			public virtual MultiValuedProperty<AutoDiscoverSmtpDomain> Domains
			{
				set
				{
					base.PowerSharpParameters["Domains"] = value;
				}
			}

			// Token: 0x17002B37 RID: 11063
			// (set) Token: 0x06004AA3 RID: 19107 RVA: 0x0007826D File Offset: 0x0007646D
			public virtual MultiValuedProperty<HybridFeature> Features
			{
				set
				{
					base.PowerSharpParameters["Features"] = value;
				}
			}

			// Token: 0x17002B38 RID: 11064
			// (set) Token: 0x06004AA4 RID: 19108 RVA: 0x00078280 File Offset: 0x00076480
			public virtual MultiValuedProperty<IPRange> ExternalIPAddresses
			{
				set
				{
					base.PowerSharpParameters["ExternalIPAddresses"] = value;
				}
			}

			// Token: 0x17002B39 RID: 11065
			// (set) Token: 0x06004AA5 RID: 19109 RVA: 0x00078293 File Offset: 0x00076493
			public virtual int ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x17002B3A RID: 11066
			// (set) Token: 0x06004AA6 RID: 19110 RVA: 0x000782AB File Offset: 0x000764AB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B3B RID: 11067
			// (set) Token: 0x06004AA7 RID: 19111 RVA: 0x000782BE File Offset: 0x000764BE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002B3C RID: 11068
			// (set) Token: 0x06004AA8 RID: 19112 RVA: 0x000782D1 File Offset: 0x000764D1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B3D RID: 11069
			// (set) Token: 0x06004AA9 RID: 19113 RVA: 0x000782E9 File Offset: 0x000764E9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B3E RID: 11070
			// (set) Token: 0x06004AAA RID: 19114 RVA: 0x00078301 File Offset: 0x00076501
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B3F RID: 11071
			// (set) Token: 0x06004AAB RID: 19115 RVA: 0x00078319 File Offset: 0x00076519
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002B40 RID: 11072
			// (set) Token: 0x06004AAC RID: 19116 RVA: 0x00078331 File Offset: 0x00076531
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

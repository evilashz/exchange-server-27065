using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200058D RID: 1421
	public class NewHybridConfigurationCommand : SyntheticCommandWithPipelineInput<HybridConfiguration, HybridConfiguration>
	{
		// Token: 0x06004A7A RID: 19066 RVA: 0x00077F75 File Offset: 0x00076175
		private NewHybridConfigurationCommand() : base("New-HybridConfiguration")
		{
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x00077F82 File Offset: 0x00076182
		public NewHybridConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x00077F91 File Offset: 0x00076191
		public virtual NewHybridConfigurationCommand SetParameters(NewHybridConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200058E RID: 1422
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002B19 RID: 11033
			// (set) Token: 0x06004A7D RID: 19069 RVA: 0x00077F9B File Offset: 0x0007619B
			public virtual MultiValuedProperty<ServerIdParameter> ClientAccessServers
			{
				set
				{
					base.PowerSharpParameters["ClientAccessServers"] = value;
				}
			}

			// Token: 0x17002B1A RID: 11034
			// (set) Token: 0x06004A7E RID: 19070 RVA: 0x00077FAE File Offset: 0x000761AE
			public virtual MultiValuedProperty<ServerIdParameter> ReceivingTransportServers
			{
				set
				{
					base.PowerSharpParameters["ReceivingTransportServers"] = value;
				}
			}

			// Token: 0x17002B1B RID: 11035
			// (set) Token: 0x06004A7F RID: 19071 RVA: 0x00077FC1 File Offset: 0x000761C1
			public virtual MultiValuedProperty<ServerIdParameter> SendingTransportServers
			{
				set
				{
					base.PowerSharpParameters["SendingTransportServers"] = value;
				}
			}

			// Token: 0x17002B1C RID: 11036
			// (set) Token: 0x06004A80 RID: 19072 RVA: 0x00077FD4 File Offset: 0x000761D4
			public virtual MultiValuedProperty<ServerIdParameter> EdgeTransportServers
			{
				set
				{
					base.PowerSharpParameters["EdgeTransportServers"] = value;
				}
			}

			// Token: 0x17002B1D RID: 11037
			// (set) Token: 0x06004A81 RID: 19073 RVA: 0x00077FE7 File Offset: 0x000761E7
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x17002B1E RID: 11038
			// (set) Token: 0x06004A82 RID: 19074 RVA: 0x00077FFA File Offset: 0x000761FA
			public virtual SmtpDomain OnPremisesSmartHost
			{
				set
				{
					base.PowerSharpParameters["OnPremisesSmartHost"] = value;
				}
			}

			// Token: 0x17002B1F RID: 11039
			// (set) Token: 0x06004A83 RID: 19075 RVA: 0x0007800D File Offset: 0x0007620D
			public virtual MultiValuedProperty<AutoDiscoverSmtpDomain> Domains
			{
				set
				{
					base.PowerSharpParameters["Domains"] = value;
				}
			}

			// Token: 0x17002B20 RID: 11040
			// (set) Token: 0x06004A84 RID: 19076 RVA: 0x00078020 File Offset: 0x00076220
			public virtual MultiValuedProperty<HybridFeature> Features
			{
				set
				{
					base.PowerSharpParameters["Features"] = value;
				}
			}

			// Token: 0x17002B21 RID: 11041
			// (set) Token: 0x06004A85 RID: 19077 RVA: 0x00078033 File Offset: 0x00076233
			public virtual MultiValuedProperty<IPRange> ExternalIPAddresses
			{
				set
				{
					base.PowerSharpParameters["ExternalIPAddresses"] = value;
				}
			}

			// Token: 0x17002B22 RID: 11042
			// (set) Token: 0x06004A86 RID: 19078 RVA: 0x00078046 File Offset: 0x00076246
			public virtual int ServiceInstance
			{
				set
				{
					base.PowerSharpParameters["ServiceInstance"] = value;
				}
			}

			// Token: 0x17002B23 RID: 11043
			// (set) Token: 0x06004A87 RID: 19079 RVA: 0x0007805E File Offset: 0x0007625E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B24 RID: 11044
			// (set) Token: 0x06004A88 RID: 19080 RVA: 0x00078071 File Offset: 0x00076271
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B25 RID: 11045
			// (set) Token: 0x06004A89 RID: 19081 RVA: 0x00078089 File Offset: 0x00076289
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B26 RID: 11046
			// (set) Token: 0x06004A8A RID: 19082 RVA: 0x000780A1 File Offset: 0x000762A1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B27 RID: 11047
			// (set) Token: 0x06004A8B RID: 19083 RVA: 0x000780B9 File Offset: 0x000762B9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002B28 RID: 11048
			// (set) Token: 0x06004A8C RID: 19084 RVA: 0x000780D1 File Offset: 0x000762D1
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

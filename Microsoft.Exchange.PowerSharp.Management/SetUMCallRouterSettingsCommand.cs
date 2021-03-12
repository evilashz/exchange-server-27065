using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B86 RID: 2950
	public class SetUMCallRouterSettingsCommand : SyntheticCommandWithPipelineInputNoOutput<SIPFEServerConfiguration>
	{
		// Token: 0x06008E82 RID: 36482 RVA: 0x000D0A76 File Offset: 0x000CEC76
		private SetUMCallRouterSettingsCommand() : base("Set-UMCallRouterSettings")
		{
		}

		// Token: 0x06008E83 RID: 36483 RVA: 0x000D0A83 File Offset: 0x000CEC83
		public SetUMCallRouterSettingsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008E84 RID: 36484 RVA: 0x000D0A92 File Offset: 0x000CEC92
		public virtual SetUMCallRouterSettingsCommand SetParameters(SetUMCallRouterSettingsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B87 RID: 2951
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700632F RID: 25391
			// (set) Token: 0x06008E85 RID: 36485 RVA: 0x000D0A9C File Offset: 0x000CEC9C
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17006330 RID: 25392
			// (set) Token: 0x06008E86 RID: 36486 RVA: 0x000D0AAF File Offset: 0x000CECAF
			public virtual MultiValuedProperty<UMDialPlanIdParameter> DialPlans
			{
				set
				{
					base.PowerSharpParameters["DialPlans"] = value;
				}
			}

			// Token: 0x17006331 RID: 25393
			// (set) Token: 0x06008E87 RID: 36487 RVA: 0x000D0AC2 File Offset: 0x000CECC2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006332 RID: 25394
			// (set) Token: 0x06008E88 RID: 36488 RVA: 0x000D0AD5 File Offset: 0x000CECD5
			public virtual int? MaxCallsAllowed
			{
				set
				{
					base.PowerSharpParameters["MaxCallsAllowed"] = value;
				}
			}

			// Token: 0x17006333 RID: 25395
			// (set) Token: 0x06008E89 RID: 36489 RVA: 0x000D0AED File Offset: 0x000CECED
			public virtual int SipTcpListeningPort
			{
				set
				{
					base.PowerSharpParameters["SipTcpListeningPort"] = value;
				}
			}

			// Token: 0x17006334 RID: 25396
			// (set) Token: 0x06008E8A RID: 36490 RVA: 0x000D0B05 File Offset: 0x000CED05
			public virtual int SipTlsListeningPort
			{
				set
				{
					base.PowerSharpParameters["SipTlsListeningPort"] = value;
				}
			}

			// Token: 0x17006335 RID: 25397
			// (set) Token: 0x06008E8B RID: 36491 RVA: 0x000D0B1D File Offset: 0x000CED1D
			public virtual UMSmartHost ExternalHostFqdn
			{
				set
				{
					base.PowerSharpParameters["ExternalHostFqdn"] = value;
				}
			}

			// Token: 0x17006336 RID: 25398
			// (set) Token: 0x06008E8C RID: 36492 RVA: 0x000D0B30 File Offset: 0x000CED30
			public virtual UMSmartHost ExternalServiceFqdn
			{
				set
				{
					base.PowerSharpParameters["ExternalServiceFqdn"] = value;
				}
			}

			// Token: 0x17006337 RID: 25399
			// (set) Token: 0x06008E8D RID: 36493 RVA: 0x000D0B43 File Offset: 0x000CED43
			public virtual string UMPodRedirectTemplate
			{
				set
				{
					base.PowerSharpParameters["UMPodRedirectTemplate"] = value;
				}
			}

			// Token: 0x17006338 RID: 25400
			// (set) Token: 0x06008E8E RID: 36494 RVA: 0x000D0B56 File Offset: 0x000CED56
			public virtual string UMForwardingAddressTemplate
			{
				set
				{
					base.PowerSharpParameters["UMForwardingAddressTemplate"] = value;
				}
			}

			// Token: 0x17006339 RID: 25401
			// (set) Token: 0x06008E8F RID: 36495 RVA: 0x000D0B69 File Offset: 0x000CED69
			public virtual UMStartupMode UMStartupMode
			{
				set
				{
					base.PowerSharpParameters["UMStartupMode"] = value;
				}
			}

			// Token: 0x1700633A RID: 25402
			// (set) Token: 0x06008E90 RID: 36496 RVA: 0x000D0B81 File Offset: 0x000CED81
			public virtual bool IPAddressFamilyConfigurable
			{
				set
				{
					base.PowerSharpParameters["IPAddressFamilyConfigurable"] = value;
				}
			}

			// Token: 0x1700633B RID: 25403
			// (set) Token: 0x06008E91 RID: 36497 RVA: 0x000D0B99 File Offset: 0x000CED99
			public virtual IPAddressFamily IPAddressFamily
			{
				set
				{
					base.PowerSharpParameters["IPAddressFamily"] = value;
				}
			}

			// Token: 0x1700633C RID: 25404
			// (set) Token: 0x06008E92 RID: 36498 RVA: 0x000D0BB1 File Offset: 0x000CEDB1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700633D RID: 25405
			// (set) Token: 0x06008E93 RID: 36499 RVA: 0x000D0BC9 File Offset: 0x000CEDC9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700633E RID: 25406
			// (set) Token: 0x06008E94 RID: 36500 RVA: 0x000D0BE1 File Offset: 0x000CEDE1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700633F RID: 25407
			// (set) Token: 0x06008E95 RID: 36501 RVA: 0x000D0BF9 File Offset: 0x000CEDF9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006340 RID: 25408
			// (set) Token: 0x06008E96 RID: 36502 RVA: 0x000D0C11 File Offset: 0x000CEE11
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

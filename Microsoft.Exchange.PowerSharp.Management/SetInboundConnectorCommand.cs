using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008D2 RID: 2258
	public class SetInboundConnectorCommand : SyntheticCommandWithPipelineInputNoOutput<TenantInboundConnector>
	{
		// Token: 0x060070D4 RID: 28884 RVA: 0x000AA1A1 File Offset: 0x000A83A1
		private SetInboundConnectorCommand() : base("Set-InboundConnector")
		{
		}

		// Token: 0x060070D5 RID: 28885 RVA: 0x000AA1AE File Offset: 0x000A83AE
		public SetInboundConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060070D6 RID: 28886 RVA: 0x000AA1BD File Offset: 0x000A83BD
		public virtual SetInboundConnectorCommand SetParameters(SetInboundConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060070D7 RID: 28887 RVA: 0x000AA1C7 File Offset: 0x000A83C7
		public virtual SetInboundConnectorCommand SetParameters(SetInboundConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008D3 RID: 2259
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004AE9 RID: 19177
			// (set) Token: 0x060070D8 RID: 28888 RVA: 0x000AA1D1 File Offset: 0x000A83D1
			public virtual bool BypassValidation
			{
				set
				{
					base.PowerSharpParameters["BypassValidation"] = value;
				}
			}

			// Token: 0x17004AEA RID: 19178
			// (set) Token: 0x060070D9 RID: 28889 RVA: 0x000AA1E9 File Offset: 0x000A83E9
			public virtual MultiValuedProperty<AcceptedDomainIdParameter> AssociatedAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["AssociatedAcceptedDomains"] = value;
				}
			}

			// Token: 0x17004AEB RID: 19179
			// (set) Token: 0x060070DA RID: 28890 RVA: 0x000AA1FC File Offset: 0x000A83FC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004AEC RID: 19180
			// (set) Token: 0x060070DB RID: 28891 RVA: 0x000AA20F File Offset: 0x000A840F
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004AED RID: 19181
			// (set) Token: 0x060070DC RID: 28892 RVA: 0x000AA227 File Offset: 0x000A8427
			public virtual TenantConnectorType ConnectorType
			{
				set
				{
					base.PowerSharpParameters["ConnectorType"] = value;
				}
			}

			// Token: 0x17004AEE RID: 19182
			// (set) Token: 0x060070DD RID: 28893 RVA: 0x000AA23F File Offset: 0x000A843F
			public virtual TenantConnectorSource ConnectorSource
			{
				set
				{
					base.PowerSharpParameters["ConnectorSource"] = value;
				}
			}

			// Token: 0x17004AEF RID: 19183
			// (set) Token: 0x060070DE RID: 28894 RVA: 0x000AA257 File Offset: 0x000A8457
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004AF0 RID: 19184
			// (set) Token: 0x060070DF RID: 28895 RVA: 0x000AA26A File Offset: 0x000A846A
			public virtual MultiValuedProperty<IPRange> SenderIPAddresses
			{
				set
				{
					base.PowerSharpParameters["SenderIPAddresses"] = value;
				}
			}

			// Token: 0x17004AF1 RID: 19185
			// (set) Token: 0x060070E0 RID: 28896 RVA: 0x000AA27D File Offset: 0x000A847D
			public virtual MultiValuedProperty<AddressSpace> SenderDomains
			{
				set
				{
					base.PowerSharpParameters["SenderDomains"] = value;
				}
			}

			// Token: 0x17004AF2 RID: 19186
			// (set) Token: 0x060070E1 RID: 28897 RVA: 0x000AA290 File Offset: 0x000A8490
			public virtual bool RequireTls
			{
				set
				{
					base.PowerSharpParameters["RequireTls"] = value;
				}
			}

			// Token: 0x17004AF3 RID: 19187
			// (set) Token: 0x060070E2 RID: 28898 RVA: 0x000AA2A8 File Offset: 0x000A84A8
			public virtual bool RestrictDomainsToIPAddresses
			{
				set
				{
					base.PowerSharpParameters["RestrictDomainsToIPAddresses"] = value;
				}
			}

			// Token: 0x17004AF4 RID: 19188
			// (set) Token: 0x060070E3 RID: 28899 RVA: 0x000AA2C0 File Offset: 0x000A84C0
			public virtual bool RestrictDomainsToCertificate
			{
				set
				{
					base.PowerSharpParameters["RestrictDomainsToCertificate"] = value;
				}
			}

			// Token: 0x17004AF5 RID: 19189
			// (set) Token: 0x060070E4 RID: 28900 RVA: 0x000AA2D8 File Offset: 0x000A84D8
			public virtual bool CloudServicesMailEnabled
			{
				set
				{
					base.PowerSharpParameters["CloudServicesMailEnabled"] = value;
				}
			}

			// Token: 0x17004AF6 RID: 19190
			// (set) Token: 0x060070E5 RID: 28901 RVA: 0x000AA2F0 File Offset: 0x000A84F0
			public virtual TlsCertificate TlsSenderCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsSenderCertificateName"] = value;
				}
			}

			// Token: 0x17004AF7 RID: 19191
			// (set) Token: 0x060070E6 RID: 28902 RVA: 0x000AA303 File Offset: 0x000A8503
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004AF8 RID: 19192
			// (set) Token: 0x060070E7 RID: 28903 RVA: 0x000AA316 File Offset: 0x000A8516
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004AF9 RID: 19193
			// (set) Token: 0x060070E8 RID: 28904 RVA: 0x000AA32E File Offset: 0x000A852E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004AFA RID: 19194
			// (set) Token: 0x060070E9 RID: 28905 RVA: 0x000AA346 File Offset: 0x000A8546
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004AFB RID: 19195
			// (set) Token: 0x060070EA RID: 28906 RVA: 0x000AA35E File Offset: 0x000A855E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004AFC RID: 19196
			// (set) Token: 0x060070EB RID: 28907 RVA: 0x000AA376 File Offset: 0x000A8576
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008D4 RID: 2260
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004AFD RID: 19197
			// (set) Token: 0x060070ED RID: 28909 RVA: 0x000AA396 File Offset: 0x000A8596
			public virtual InboundConnectorIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004AFE RID: 19198
			// (set) Token: 0x060070EE RID: 28910 RVA: 0x000AA3A9 File Offset: 0x000A85A9
			public virtual bool BypassValidation
			{
				set
				{
					base.PowerSharpParameters["BypassValidation"] = value;
				}
			}

			// Token: 0x17004AFF RID: 19199
			// (set) Token: 0x060070EF RID: 28911 RVA: 0x000AA3C1 File Offset: 0x000A85C1
			public virtual MultiValuedProperty<AcceptedDomainIdParameter> AssociatedAcceptedDomains
			{
				set
				{
					base.PowerSharpParameters["AssociatedAcceptedDomains"] = value;
				}
			}

			// Token: 0x17004B00 RID: 19200
			// (set) Token: 0x060070F0 RID: 28912 RVA: 0x000AA3D4 File Offset: 0x000A85D4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004B01 RID: 19201
			// (set) Token: 0x060070F1 RID: 28913 RVA: 0x000AA3E7 File Offset: 0x000A85E7
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004B02 RID: 19202
			// (set) Token: 0x060070F2 RID: 28914 RVA: 0x000AA3FF File Offset: 0x000A85FF
			public virtual TenantConnectorType ConnectorType
			{
				set
				{
					base.PowerSharpParameters["ConnectorType"] = value;
				}
			}

			// Token: 0x17004B03 RID: 19203
			// (set) Token: 0x060070F3 RID: 28915 RVA: 0x000AA417 File Offset: 0x000A8617
			public virtual TenantConnectorSource ConnectorSource
			{
				set
				{
					base.PowerSharpParameters["ConnectorSource"] = value;
				}
			}

			// Token: 0x17004B04 RID: 19204
			// (set) Token: 0x060070F4 RID: 28916 RVA: 0x000AA42F File Offset: 0x000A862F
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004B05 RID: 19205
			// (set) Token: 0x060070F5 RID: 28917 RVA: 0x000AA442 File Offset: 0x000A8642
			public virtual MultiValuedProperty<IPRange> SenderIPAddresses
			{
				set
				{
					base.PowerSharpParameters["SenderIPAddresses"] = value;
				}
			}

			// Token: 0x17004B06 RID: 19206
			// (set) Token: 0x060070F6 RID: 28918 RVA: 0x000AA455 File Offset: 0x000A8655
			public virtual MultiValuedProperty<AddressSpace> SenderDomains
			{
				set
				{
					base.PowerSharpParameters["SenderDomains"] = value;
				}
			}

			// Token: 0x17004B07 RID: 19207
			// (set) Token: 0x060070F7 RID: 28919 RVA: 0x000AA468 File Offset: 0x000A8668
			public virtual bool RequireTls
			{
				set
				{
					base.PowerSharpParameters["RequireTls"] = value;
				}
			}

			// Token: 0x17004B08 RID: 19208
			// (set) Token: 0x060070F8 RID: 28920 RVA: 0x000AA480 File Offset: 0x000A8680
			public virtual bool RestrictDomainsToIPAddresses
			{
				set
				{
					base.PowerSharpParameters["RestrictDomainsToIPAddresses"] = value;
				}
			}

			// Token: 0x17004B09 RID: 19209
			// (set) Token: 0x060070F9 RID: 28921 RVA: 0x000AA498 File Offset: 0x000A8698
			public virtual bool RestrictDomainsToCertificate
			{
				set
				{
					base.PowerSharpParameters["RestrictDomainsToCertificate"] = value;
				}
			}

			// Token: 0x17004B0A RID: 19210
			// (set) Token: 0x060070FA RID: 28922 RVA: 0x000AA4B0 File Offset: 0x000A86B0
			public virtual bool CloudServicesMailEnabled
			{
				set
				{
					base.PowerSharpParameters["CloudServicesMailEnabled"] = value;
				}
			}

			// Token: 0x17004B0B RID: 19211
			// (set) Token: 0x060070FB RID: 28923 RVA: 0x000AA4C8 File Offset: 0x000A86C8
			public virtual TlsCertificate TlsSenderCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsSenderCertificateName"] = value;
				}
			}

			// Token: 0x17004B0C RID: 19212
			// (set) Token: 0x060070FC RID: 28924 RVA: 0x000AA4DB File Offset: 0x000A86DB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004B0D RID: 19213
			// (set) Token: 0x060070FD RID: 28925 RVA: 0x000AA4EE File Offset: 0x000A86EE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004B0E RID: 19214
			// (set) Token: 0x060070FE RID: 28926 RVA: 0x000AA506 File Offset: 0x000A8706
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004B0F RID: 19215
			// (set) Token: 0x060070FF RID: 28927 RVA: 0x000AA51E File Offset: 0x000A871E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004B10 RID: 19216
			// (set) Token: 0x06007100 RID: 28928 RVA: 0x000AA536 File Offset: 0x000A8736
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004B11 RID: 19217
			// (set) Token: 0x06007101 RID: 28929 RVA: 0x000AA54E File Offset: 0x000A874E
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

using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008DE RID: 2270
	public class SetSendConnectorCommand : SyntheticCommandWithPipelineInputNoOutput<SmtpSendConnectorConfig>
	{
		// Token: 0x060071DC RID: 29148 RVA: 0x000AB7C4 File Offset: 0x000A99C4
		private SetSendConnectorCommand() : base("Set-SendConnector")
		{
		}

		// Token: 0x060071DD RID: 29149 RVA: 0x000AB7D1 File Offset: 0x000A99D1
		public SetSendConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060071DE RID: 29150 RVA: 0x000AB7E0 File Offset: 0x000A99E0
		public virtual SetSendConnectorCommand SetParameters(SetSendConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060071DF RID: 29151 RVA: 0x000AB7EA File Offset: 0x000A99EA
		public virtual SetSendConnectorCommand SetParameters(SetSendConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008DF RID: 2271
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004BD9 RID: 19417
			// (set) Token: 0x060071E0 RID: 29152 RVA: 0x000AB7F4 File Offset: 0x000A99F4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004BDA RID: 19418
			// (set) Token: 0x060071E1 RID: 29153 RVA: 0x000AB80C File Offset: 0x000A9A0C
			public virtual MultiValuedProperty<ServerIdParameter> SourceTransportServers
			{
				set
				{
					base.PowerSharpParameters["SourceTransportServers"] = value;
				}
			}

			// Token: 0x17004BDB RID: 19419
			// (set) Token: 0x060071E2 RID: 29154 RVA: 0x000AB81F File Offset: 0x000A9A1F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004BDC RID: 19420
			// (set) Token: 0x060071E3 RID: 29155 RVA: 0x000AB832 File Offset: 0x000A9A32
			public virtual bool DNSRoutingEnabled
			{
				set
				{
					base.PowerSharpParameters["DNSRoutingEnabled"] = value;
				}
			}

			// Token: 0x17004BDD RID: 19421
			// (set) Token: 0x060071E4 RID: 29156 RVA: 0x000AB84A File Offset: 0x000A9A4A
			public virtual SmtpDomainWithSubdomains TlsDomain
			{
				set
				{
					base.PowerSharpParameters["TlsDomain"] = value;
				}
			}

			// Token: 0x17004BDE RID: 19422
			// (set) Token: 0x060071E5 RID: 29157 RVA: 0x000AB85D File Offset: 0x000A9A5D
			public virtual TlsAuthLevel? TlsAuthLevel
			{
				set
				{
					base.PowerSharpParameters["TlsAuthLevel"] = value;
				}
			}

			// Token: 0x17004BDF RID: 19423
			// (set) Token: 0x060071E6 RID: 29158 RVA: 0x000AB875 File Offset: 0x000A9A75
			public virtual ErrorPolicies ErrorPolicies
			{
				set
				{
					base.PowerSharpParameters["ErrorPolicies"] = value;
				}
			}

			// Token: 0x17004BE0 RID: 19424
			// (set) Token: 0x060071E7 RID: 29159 RVA: 0x000AB88D File Offset: 0x000A9A8D
			public virtual MultiValuedProperty<SmartHost> SmartHosts
			{
				set
				{
					base.PowerSharpParameters["SmartHosts"] = value;
				}
			}

			// Token: 0x17004BE1 RID: 19425
			// (set) Token: 0x060071E8 RID: 29160 RVA: 0x000AB8A0 File Offset: 0x000A9AA0
			public virtual int Port
			{
				set
				{
					base.PowerSharpParameters["Port"] = value;
				}
			}

			// Token: 0x17004BE2 RID: 19426
			// (set) Token: 0x060071E9 RID: 29161 RVA: 0x000AB8B8 File Offset: 0x000A9AB8
			public virtual EnhancedTimeSpan ConnectionInactivityTimeOut
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeOut"] = value;
				}
			}

			// Token: 0x17004BE3 RID: 19427
			// (set) Token: 0x060071EA RID: 29162 RVA: 0x000AB8D0 File Offset: 0x000A9AD0
			public virtual bool ForceHELO
			{
				set
				{
					base.PowerSharpParameters["ForceHELO"] = value;
				}
			}

			// Token: 0x17004BE4 RID: 19428
			// (set) Token: 0x060071EB RID: 29163 RVA: 0x000AB8E8 File Offset: 0x000A9AE8
			public virtual bool FrontendProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["FrontendProxyEnabled"] = value;
				}
			}

			// Token: 0x17004BE5 RID: 19429
			// (set) Token: 0x060071EC RID: 29164 RVA: 0x000AB900 File Offset: 0x000A9B00
			public virtual bool IgnoreSTARTTLS
			{
				set
				{
					base.PowerSharpParameters["IgnoreSTARTTLS"] = value;
				}
			}

			// Token: 0x17004BE6 RID: 19430
			// (set) Token: 0x060071ED RID: 29165 RVA: 0x000AB918 File Offset: 0x000A9B18
			public virtual bool CloudServicesMailEnabled
			{
				set
				{
					base.PowerSharpParameters["CloudServicesMailEnabled"] = value;
				}
			}

			// Token: 0x17004BE7 RID: 19431
			// (set) Token: 0x060071EE RID: 29166 RVA: 0x000AB930 File Offset: 0x000A9B30
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x17004BE8 RID: 19432
			// (set) Token: 0x060071EF RID: 29167 RVA: 0x000AB943 File Offset: 0x000A9B43
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x17004BE9 RID: 19433
			// (set) Token: 0x060071F0 RID: 29168 RVA: 0x000AB956 File Offset: 0x000A9B56
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x17004BEA RID: 19434
			// (set) Token: 0x060071F1 RID: 29169 RVA: 0x000AB96E File Offset: 0x000A9B6E
			public virtual bool RequireOorg
			{
				set
				{
					base.PowerSharpParameters["RequireOorg"] = value;
				}
			}

			// Token: 0x17004BEB RID: 19435
			// (set) Token: 0x060071F2 RID: 29170 RVA: 0x000AB986 File Offset: 0x000A9B86
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004BEC RID: 19436
			// (set) Token: 0x060071F3 RID: 29171 RVA: 0x000AB99E File Offset: 0x000A9B9E
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x17004BED RID: 19437
			// (set) Token: 0x060071F4 RID: 29172 RVA: 0x000AB9B6 File Offset: 0x000A9BB6
			public virtual SmtpSendConnectorConfig.AuthMechanisms SmartHostAuthMechanism
			{
				set
				{
					base.PowerSharpParameters["SmartHostAuthMechanism"] = value;
				}
			}

			// Token: 0x17004BEE RID: 19438
			// (set) Token: 0x060071F5 RID: 29173 RVA: 0x000AB9CE File Offset: 0x000A9BCE
			public virtual PSCredential AuthenticationCredential
			{
				set
				{
					base.PowerSharpParameters["AuthenticationCredential"] = value;
				}
			}

			// Token: 0x17004BEF RID: 19439
			// (set) Token: 0x060071F6 RID: 29174 RVA: 0x000AB9E1 File Offset: 0x000A9BE1
			public virtual bool UseExternalDNSServersEnabled
			{
				set
				{
					base.PowerSharpParameters["UseExternalDNSServersEnabled"] = value;
				}
			}

			// Token: 0x17004BF0 RID: 19440
			// (set) Token: 0x060071F7 RID: 29175 RVA: 0x000AB9F9 File Offset: 0x000A9BF9
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x17004BF1 RID: 19441
			// (set) Token: 0x060071F8 RID: 29176 RVA: 0x000ABA11 File Offset: 0x000A9C11
			public virtual IPAddress SourceIPAddress
			{
				set
				{
					base.PowerSharpParameters["SourceIPAddress"] = value;
				}
			}

			// Token: 0x17004BF2 RID: 19442
			// (set) Token: 0x060071F9 RID: 29177 RVA: 0x000ABA24 File Offset: 0x000A9C24
			public virtual int SmtpMaxMessagesPerConnection
			{
				set
				{
					base.PowerSharpParameters["SmtpMaxMessagesPerConnection"] = value;
				}
			}

			// Token: 0x17004BF3 RID: 19443
			// (set) Token: 0x060071FA RID: 29178 RVA: 0x000ABA3C File Offset: 0x000A9C3C
			public virtual MultiValuedProperty<AddressSpace> AddressSpaces
			{
				set
				{
					base.PowerSharpParameters["AddressSpaces"] = value;
				}
			}

			// Token: 0x17004BF4 RID: 19444
			// (set) Token: 0x060071FB RID: 29179 RVA: 0x000ABA4F File Offset: 0x000A9C4F
			public virtual bool IsScopedConnector
			{
				set
				{
					base.PowerSharpParameters["IsScopedConnector"] = value;
				}
			}

			// Token: 0x17004BF5 RID: 19445
			// (set) Token: 0x060071FC RID: 29180 RVA: 0x000ABA67 File Offset: 0x000A9C67
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004BF6 RID: 19446
			// (set) Token: 0x060071FD RID: 29181 RVA: 0x000ABA7A File Offset: 0x000A9C7A
			public virtual Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17004BF7 RID: 19447
			// (set) Token: 0x060071FE RID: 29182 RVA: 0x000ABA92 File Offset: 0x000A9C92
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004BF8 RID: 19448
			// (set) Token: 0x060071FF RID: 29183 RVA: 0x000ABAA5 File Offset: 0x000A9CA5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004BF9 RID: 19449
			// (set) Token: 0x06007200 RID: 29184 RVA: 0x000ABABD File Offset: 0x000A9CBD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004BFA RID: 19450
			// (set) Token: 0x06007201 RID: 29185 RVA: 0x000ABAD5 File Offset: 0x000A9CD5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004BFB RID: 19451
			// (set) Token: 0x06007202 RID: 29186 RVA: 0x000ABAED File Offset: 0x000A9CED
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004BFC RID: 19452
			// (set) Token: 0x06007203 RID: 29187 RVA: 0x000ABB05 File Offset: 0x000A9D05
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008E0 RID: 2272
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004BFD RID: 19453
			// (set) Token: 0x06007205 RID: 29189 RVA: 0x000ABB25 File Offset: 0x000A9D25
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SendConnectorIdParameter(value) : null);
				}
			}

			// Token: 0x17004BFE RID: 19454
			// (set) Token: 0x06007206 RID: 29190 RVA: 0x000ABB43 File Offset: 0x000A9D43
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004BFF RID: 19455
			// (set) Token: 0x06007207 RID: 29191 RVA: 0x000ABB5B File Offset: 0x000A9D5B
			public virtual MultiValuedProperty<ServerIdParameter> SourceTransportServers
			{
				set
				{
					base.PowerSharpParameters["SourceTransportServers"] = value;
				}
			}

			// Token: 0x17004C00 RID: 19456
			// (set) Token: 0x06007208 RID: 29192 RVA: 0x000ABB6E File Offset: 0x000A9D6E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004C01 RID: 19457
			// (set) Token: 0x06007209 RID: 29193 RVA: 0x000ABB81 File Offset: 0x000A9D81
			public virtual bool DNSRoutingEnabled
			{
				set
				{
					base.PowerSharpParameters["DNSRoutingEnabled"] = value;
				}
			}

			// Token: 0x17004C02 RID: 19458
			// (set) Token: 0x0600720A RID: 29194 RVA: 0x000ABB99 File Offset: 0x000A9D99
			public virtual SmtpDomainWithSubdomains TlsDomain
			{
				set
				{
					base.PowerSharpParameters["TlsDomain"] = value;
				}
			}

			// Token: 0x17004C03 RID: 19459
			// (set) Token: 0x0600720B RID: 29195 RVA: 0x000ABBAC File Offset: 0x000A9DAC
			public virtual TlsAuthLevel? TlsAuthLevel
			{
				set
				{
					base.PowerSharpParameters["TlsAuthLevel"] = value;
				}
			}

			// Token: 0x17004C04 RID: 19460
			// (set) Token: 0x0600720C RID: 29196 RVA: 0x000ABBC4 File Offset: 0x000A9DC4
			public virtual ErrorPolicies ErrorPolicies
			{
				set
				{
					base.PowerSharpParameters["ErrorPolicies"] = value;
				}
			}

			// Token: 0x17004C05 RID: 19461
			// (set) Token: 0x0600720D RID: 29197 RVA: 0x000ABBDC File Offset: 0x000A9DDC
			public virtual MultiValuedProperty<SmartHost> SmartHosts
			{
				set
				{
					base.PowerSharpParameters["SmartHosts"] = value;
				}
			}

			// Token: 0x17004C06 RID: 19462
			// (set) Token: 0x0600720E RID: 29198 RVA: 0x000ABBEF File Offset: 0x000A9DEF
			public virtual int Port
			{
				set
				{
					base.PowerSharpParameters["Port"] = value;
				}
			}

			// Token: 0x17004C07 RID: 19463
			// (set) Token: 0x0600720F RID: 29199 RVA: 0x000ABC07 File Offset: 0x000A9E07
			public virtual EnhancedTimeSpan ConnectionInactivityTimeOut
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeOut"] = value;
				}
			}

			// Token: 0x17004C08 RID: 19464
			// (set) Token: 0x06007210 RID: 29200 RVA: 0x000ABC1F File Offset: 0x000A9E1F
			public virtual bool ForceHELO
			{
				set
				{
					base.PowerSharpParameters["ForceHELO"] = value;
				}
			}

			// Token: 0x17004C09 RID: 19465
			// (set) Token: 0x06007211 RID: 29201 RVA: 0x000ABC37 File Offset: 0x000A9E37
			public virtual bool FrontendProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["FrontendProxyEnabled"] = value;
				}
			}

			// Token: 0x17004C0A RID: 19466
			// (set) Token: 0x06007212 RID: 29202 RVA: 0x000ABC4F File Offset: 0x000A9E4F
			public virtual bool IgnoreSTARTTLS
			{
				set
				{
					base.PowerSharpParameters["IgnoreSTARTTLS"] = value;
				}
			}

			// Token: 0x17004C0B RID: 19467
			// (set) Token: 0x06007213 RID: 29203 RVA: 0x000ABC67 File Offset: 0x000A9E67
			public virtual bool CloudServicesMailEnabled
			{
				set
				{
					base.PowerSharpParameters["CloudServicesMailEnabled"] = value;
				}
			}

			// Token: 0x17004C0C RID: 19468
			// (set) Token: 0x06007214 RID: 29204 RVA: 0x000ABC7F File Offset: 0x000A9E7F
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x17004C0D RID: 19469
			// (set) Token: 0x06007215 RID: 29205 RVA: 0x000ABC92 File Offset: 0x000A9E92
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x17004C0E RID: 19470
			// (set) Token: 0x06007216 RID: 29206 RVA: 0x000ABCA5 File Offset: 0x000A9EA5
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x17004C0F RID: 19471
			// (set) Token: 0x06007217 RID: 29207 RVA: 0x000ABCBD File Offset: 0x000A9EBD
			public virtual bool RequireOorg
			{
				set
				{
					base.PowerSharpParameters["RequireOorg"] = value;
				}
			}

			// Token: 0x17004C10 RID: 19472
			// (set) Token: 0x06007218 RID: 29208 RVA: 0x000ABCD5 File Offset: 0x000A9ED5
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004C11 RID: 19473
			// (set) Token: 0x06007219 RID: 29209 RVA: 0x000ABCED File Offset: 0x000A9EED
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x17004C12 RID: 19474
			// (set) Token: 0x0600721A RID: 29210 RVA: 0x000ABD05 File Offset: 0x000A9F05
			public virtual SmtpSendConnectorConfig.AuthMechanisms SmartHostAuthMechanism
			{
				set
				{
					base.PowerSharpParameters["SmartHostAuthMechanism"] = value;
				}
			}

			// Token: 0x17004C13 RID: 19475
			// (set) Token: 0x0600721B RID: 29211 RVA: 0x000ABD1D File Offset: 0x000A9F1D
			public virtual PSCredential AuthenticationCredential
			{
				set
				{
					base.PowerSharpParameters["AuthenticationCredential"] = value;
				}
			}

			// Token: 0x17004C14 RID: 19476
			// (set) Token: 0x0600721C RID: 29212 RVA: 0x000ABD30 File Offset: 0x000A9F30
			public virtual bool UseExternalDNSServersEnabled
			{
				set
				{
					base.PowerSharpParameters["UseExternalDNSServersEnabled"] = value;
				}
			}

			// Token: 0x17004C15 RID: 19477
			// (set) Token: 0x0600721D RID: 29213 RVA: 0x000ABD48 File Offset: 0x000A9F48
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x17004C16 RID: 19478
			// (set) Token: 0x0600721E RID: 29214 RVA: 0x000ABD60 File Offset: 0x000A9F60
			public virtual IPAddress SourceIPAddress
			{
				set
				{
					base.PowerSharpParameters["SourceIPAddress"] = value;
				}
			}

			// Token: 0x17004C17 RID: 19479
			// (set) Token: 0x0600721F RID: 29215 RVA: 0x000ABD73 File Offset: 0x000A9F73
			public virtual int SmtpMaxMessagesPerConnection
			{
				set
				{
					base.PowerSharpParameters["SmtpMaxMessagesPerConnection"] = value;
				}
			}

			// Token: 0x17004C18 RID: 19480
			// (set) Token: 0x06007220 RID: 29216 RVA: 0x000ABD8B File Offset: 0x000A9F8B
			public virtual MultiValuedProperty<AddressSpace> AddressSpaces
			{
				set
				{
					base.PowerSharpParameters["AddressSpaces"] = value;
				}
			}

			// Token: 0x17004C19 RID: 19481
			// (set) Token: 0x06007221 RID: 29217 RVA: 0x000ABD9E File Offset: 0x000A9F9E
			public virtual bool IsScopedConnector
			{
				set
				{
					base.PowerSharpParameters["IsScopedConnector"] = value;
				}
			}

			// Token: 0x17004C1A RID: 19482
			// (set) Token: 0x06007222 RID: 29218 RVA: 0x000ABDB6 File Offset: 0x000A9FB6
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004C1B RID: 19483
			// (set) Token: 0x06007223 RID: 29219 RVA: 0x000ABDC9 File Offset: 0x000A9FC9
			public virtual Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17004C1C RID: 19484
			// (set) Token: 0x06007224 RID: 29220 RVA: 0x000ABDE1 File Offset: 0x000A9FE1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004C1D RID: 19485
			// (set) Token: 0x06007225 RID: 29221 RVA: 0x000ABDF4 File Offset: 0x000A9FF4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004C1E RID: 19486
			// (set) Token: 0x06007226 RID: 29222 RVA: 0x000ABE0C File Offset: 0x000AA00C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004C1F RID: 19487
			// (set) Token: 0x06007227 RID: 29223 RVA: 0x000ABE24 File Offset: 0x000AA024
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004C20 RID: 19488
			// (set) Token: 0x06007228 RID: 29224 RVA: 0x000ABE3C File Offset: 0x000AA03C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004C21 RID: 19489
			// (set) Token: 0x06007229 RID: 29225 RVA: 0x000ABE54 File Offset: 0x000AA054
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

using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008B1 RID: 2225
	public class NewSendConnectorCommand : SyntheticCommandWithPipelineInput<SmtpSendConnectorConfig, SmtpSendConnectorConfig>
	{
		// Token: 0x06006F7C RID: 28540 RVA: 0x000A85F1 File Offset: 0x000A67F1
		private NewSendConnectorCommand() : base("New-SendConnector")
		{
		}

		// Token: 0x06006F7D RID: 28541 RVA: 0x000A85FE File Offset: 0x000A67FE
		public NewSendConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006F7E RID: 28542 RVA: 0x000A860D File Offset: 0x000A680D
		public virtual NewSendConnectorCommand SetParameters(NewSendConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006F7F RID: 28543 RVA: 0x000A8617 File Offset: 0x000A6817
		public virtual NewSendConnectorCommand SetParameters(NewSendConnectorCommand.AddressSpacesParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008B2 RID: 2226
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170049D3 RID: 18899
			// (set) Token: 0x06006F80 RID: 28544 RVA: 0x000A8621 File Offset: 0x000A6821
			public virtual NewSendConnector.UsageType Usage
			{
				set
				{
					base.PowerSharpParameters["Usage"] = value;
				}
			}

			// Token: 0x170049D4 RID: 18900
			// (set) Token: 0x06006F81 RID: 28545 RVA: 0x000A8639 File Offset: 0x000A6839
			public virtual SwitchParameter Internet
			{
				set
				{
					base.PowerSharpParameters["Internet"] = value;
				}
			}

			// Token: 0x170049D5 RID: 18901
			// (set) Token: 0x06006F82 RID: 28546 RVA: 0x000A8651 File Offset: 0x000A6851
			public virtual SwitchParameter Internal
			{
				set
				{
					base.PowerSharpParameters["Internal"] = value;
				}
			}

			// Token: 0x170049D6 RID: 18902
			// (set) Token: 0x06006F83 RID: 28547 RVA: 0x000A8669 File Offset: 0x000A6869
			public virtual SwitchParameter Partner
			{
				set
				{
					base.PowerSharpParameters["Partner"] = value;
				}
			}

			// Token: 0x170049D7 RID: 18903
			// (set) Token: 0x06006F84 RID: 28548 RVA: 0x000A8681 File Offset: 0x000A6881
			public virtual SwitchParameter Custom
			{
				set
				{
					base.PowerSharpParameters["Custom"] = value;
				}
			}

			// Token: 0x170049D8 RID: 18904
			// (set) Token: 0x06006F85 RID: 28549 RVA: 0x000A8699 File Offset: 0x000A6899
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170049D9 RID: 18905
			// (set) Token: 0x06006F86 RID: 28550 RVA: 0x000A86B1 File Offset: 0x000A68B1
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x170049DA RID: 18906
			// (set) Token: 0x06006F87 RID: 28551 RVA: 0x000A86C9 File Offset: 0x000A68C9
			public virtual bool DNSRoutingEnabled
			{
				set
				{
					base.PowerSharpParameters["DNSRoutingEnabled"] = value;
				}
			}

			// Token: 0x170049DB RID: 18907
			// (set) Token: 0x06006F88 RID: 28552 RVA: 0x000A86E1 File Offset: 0x000A68E1
			public virtual MultiValuedProperty<SmartHost> SmartHosts
			{
				set
				{
					base.PowerSharpParameters["SmartHosts"] = value;
				}
			}

			// Token: 0x170049DC RID: 18908
			// (set) Token: 0x06006F89 RID: 28553 RVA: 0x000A86F4 File Offset: 0x000A68F4
			public virtual int Port
			{
				set
				{
					base.PowerSharpParameters["Port"] = value;
				}
			}

			// Token: 0x170049DD RID: 18909
			// (set) Token: 0x06006F8A RID: 28554 RVA: 0x000A870C File Offset: 0x000A690C
			public virtual EnhancedTimeSpan ConnectionInactivityTimeOut
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeOut"] = value;
				}
			}

			// Token: 0x170049DE RID: 18910
			// (set) Token: 0x06006F8B RID: 28555 RVA: 0x000A8724 File Offset: 0x000A6924
			public virtual Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x170049DF RID: 18911
			// (set) Token: 0x06006F8C RID: 28556 RVA: 0x000A873C File Offset: 0x000A693C
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x170049E0 RID: 18912
			// (set) Token: 0x06006F8D RID: 28557 RVA: 0x000A874F File Offset: 0x000A694F
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x170049E1 RID: 18913
			// (set) Token: 0x06006F8E RID: 28558 RVA: 0x000A8762 File Offset: 0x000A6962
			public virtual bool ForceHELO
			{
				set
				{
					base.PowerSharpParameters["ForceHELO"] = value;
				}
			}

			// Token: 0x170049E2 RID: 18914
			// (set) Token: 0x06006F8F RID: 28559 RVA: 0x000A877A File Offset: 0x000A697A
			public virtual bool FrontendProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["FrontendProxyEnabled"] = value;
				}
			}

			// Token: 0x170049E3 RID: 18915
			// (set) Token: 0x06006F90 RID: 28560 RVA: 0x000A8792 File Offset: 0x000A6992
			public virtual bool IgnoreSTARTTLS
			{
				set
				{
					base.PowerSharpParameters["IgnoreSTARTTLS"] = value;
				}
			}

			// Token: 0x170049E4 RID: 18916
			// (set) Token: 0x06006F91 RID: 28561 RVA: 0x000A87AA File Offset: 0x000A69AA
			public virtual bool CloudServicesMailEnabled
			{
				set
				{
					base.PowerSharpParameters["CloudServicesMailEnabled"] = value;
				}
			}

			// Token: 0x170049E5 RID: 18917
			// (set) Token: 0x06006F92 RID: 28562 RVA: 0x000A87C2 File Offset: 0x000A69C2
			public virtual bool RequireOorg
			{
				set
				{
					base.PowerSharpParameters["RequireOorg"] = value;
				}
			}

			// Token: 0x170049E6 RID: 18918
			// (set) Token: 0x06006F93 RID: 28563 RVA: 0x000A87DA File Offset: 0x000A69DA
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x170049E7 RID: 18919
			// (set) Token: 0x06006F94 RID: 28564 RVA: 0x000A87F2 File Offset: 0x000A69F2
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170049E8 RID: 18920
			// (set) Token: 0x06006F95 RID: 28565 RVA: 0x000A880A File Offset: 0x000A6A0A
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x170049E9 RID: 18921
			// (set) Token: 0x06006F96 RID: 28566 RVA: 0x000A8822 File Offset: 0x000A6A22
			public virtual SmtpSendConnectorConfig.AuthMechanisms SmartHostAuthMechanism
			{
				set
				{
					base.PowerSharpParameters["SmartHostAuthMechanism"] = value;
				}
			}

			// Token: 0x170049EA RID: 18922
			// (set) Token: 0x06006F97 RID: 28567 RVA: 0x000A883A File Offset: 0x000A6A3A
			public virtual bool UseExternalDNSServersEnabled
			{
				set
				{
					base.PowerSharpParameters["UseExternalDNSServersEnabled"] = value;
				}
			}

			// Token: 0x170049EB RID: 18923
			// (set) Token: 0x06006F98 RID: 28568 RVA: 0x000A8852 File Offset: 0x000A6A52
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x170049EC RID: 18924
			// (set) Token: 0x06006F99 RID: 28569 RVA: 0x000A8865 File Offset: 0x000A6A65
			public virtual IPAddress SourceIPAddress
			{
				set
				{
					base.PowerSharpParameters["SourceIPAddress"] = value;
				}
			}

			// Token: 0x170049ED RID: 18925
			// (set) Token: 0x06006F9A RID: 28570 RVA: 0x000A8878 File Offset: 0x000A6A78
			public virtual int SmtpMaxMessagesPerConnection
			{
				set
				{
					base.PowerSharpParameters["SmtpMaxMessagesPerConnection"] = value;
				}
			}

			// Token: 0x170049EE RID: 18926
			// (set) Token: 0x06006F9B RID: 28571 RVA: 0x000A8890 File Offset: 0x000A6A90
			public virtual PSCredential AuthenticationCredential
			{
				set
				{
					base.PowerSharpParameters["AuthenticationCredential"] = value;
				}
			}

			// Token: 0x170049EF RID: 18927
			// (set) Token: 0x06006F9C RID: 28572 RVA: 0x000A88A3 File Offset: 0x000A6AA3
			public virtual MultiValuedProperty<ServerIdParameter> SourceTransportServers
			{
				set
				{
					base.PowerSharpParameters["SourceTransportServers"] = value;
				}
			}

			// Token: 0x170049F0 RID: 18928
			// (set) Token: 0x06006F9D RID: 28573 RVA: 0x000A88B6 File Offset: 0x000A6AB6
			public virtual SmtpDomainWithSubdomains TlsDomain
			{
				set
				{
					base.PowerSharpParameters["TlsDomain"] = value;
				}
			}

			// Token: 0x170049F1 RID: 18929
			// (set) Token: 0x06006F9E RID: 28574 RVA: 0x000A88C9 File Offset: 0x000A6AC9
			public virtual TlsAuthLevel? TlsAuthLevel
			{
				set
				{
					base.PowerSharpParameters["TlsAuthLevel"] = value;
				}
			}

			// Token: 0x170049F2 RID: 18930
			// (set) Token: 0x06006F9F RID: 28575 RVA: 0x000A88E1 File Offset: 0x000A6AE1
			public virtual ErrorPolicies ErrorPolicies
			{
				set
				{
					base.PowerSharpParameters["ErrorPolicies"] = value;
				}
			}

			// Token: 0x170049F3 RID: 18931
			// (set) Token: 0x06006FA0 RID: 28576 RVA: 0x000A88F9 File Offset: 0x000A6AF9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170049F4 RID: 18932
			// (set) Token: 0x06006FA1 RID: 28577 RVA: 0x000A890C File Offset: 0x000A6B0C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170049F5 RID: 18933
			// (set) Token: 0x06006FA2 RID: 28578 RVA: 0x000A891F File Offset: 0x000A6B1F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170049F6 RID: 18934
			// (set) Token: 0x06006FA3 RID: 28579 RVA: 0x000A8937 File Offset: 0x000A6B37
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170049F7 RID: 18935
			// (set) Token: 0x06006FA4 RID: 28580 RVA: 0x000A894F File Offset: 0x000A6B4F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170049F8 RID: 18936
			// (set) Token: 0x06006FA5 RID: 28581 RVA: 0x000A8967 File Offset: 0x000A6B67
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170049F9 RID: 18937
			// (set) Token: 0x06006FA6 RID: 28582 RVA: 0x000A897F File Offset: 0x000A6B7F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008B3 RID: 2227
		public class AddressSpacesParameters : ParametersBase
		{
			// Token: 0x170049FA RID: 18938
			// (set) Token: 0x06006FA8 RID: 28584 RVA: 0x000A899F File Offset: 0x000A6B9F
			public virtual MultiValuedProperty<AddressSpace> AddressSpaces
			{
				set
				{
					base.PowerSharpParameters["AddressSpaces"] = value;
				}
			}

			// Token: 0x170049FB RID: 18939
			// (set) Token: 0x06006FA9 RID: 28585 RVA: 0x000A89B2 File Offset: 0x000A6BB2
			public virtual bool IsScopedConnector
			{
				set
				{
					base.PowerSharpParameters["IsScopedConnector"] = value;
				}
			}

			// Token: 0x170049FC RID: 18940
			// (set) Token: 0x06006FAA RID: 28586 RVA: 0x000A89CA File Offset: 0x000A6BCA
			public virtual NewSendConnector.UsageType Usage
			{
				set
				{
					base.PowerSharpParameters["Usage"] = value;
				}
			}

			// Token: 0x170049FD RID: 18941
			// (set) Token: 0x06006FAB RID: 28587 RVA: 0x000A89E2 File Offset: 0x000A6BE2
			public virtual SwitchParameter Internet
			{
				set
				{
					base.PowerSharpParameters["Internet"] = value;
				}
			}

			// Token: 0x170049FE RID: 18942
			// (set) Token: 0x06006FAC RID: 28588 RVA: 0x000A89FA File Offset: 0x000A6BFA
			public virtual SwitchParameter Internal
			{
				set
				{
					base.PowerSharpParameters["Internal"] = value;
				}
			}

			// Token: 0x170049FF RID: 18943
			// (set) Token: 0x06006FAD RID: 28589 RVA: 0x000A8A12 File Offset: 0x000A6C12
			public virtual SwitchParameter Partner
			{
				set
				{
					base.PowerSharpParameters["Partner"] = value;
				}
			}

			// Token: 0x17004A00 RID: 18944
			// (set) Token: 0x06006FAE RID: 28590 RVA: 0x000A8A2A File Offset: 0x000A6C2A
			public virtual SwitchParameter Custom
			{
				set
				{
					base.PowerSharpParameters["Custom"] = value;
				}
			}

			// Token: 0x17004A01 RID: 18945
			// (set) Token: 0x06006FAF RID: 28591 RVA: 0x000A8A42 File Offset: 0x000A6C42
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17004A02 RID: 18946
			// (set) Token: 0x06006FB0 RID: 28592 RVA: 0x000A8A5A File Offset: 0x000A6C5A
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x17004A03 RID: 18947
			// (set) Token: 0x06006FB1 RID: 28593 RVA: 0x000A8A72 File Offset: 0x000A6C72
			public virtual bool DNSRoutingEnabled
			{
				set
				{
					base.PowerSharpParameters["DNSRoutingEnabled"] = value;
				}
			}

			// Token: 0x17004A04 RID: 18948
			// (set) Token: 0x06006FB2 RID: 28594 RVA: 0x000A8A8A File Offset: 0x000A6C8A
			public virtual MultiValuedProperty<SmartHost> SmartHosts
			{
				set
				{
					base.PowerSharpParameters["SmartHosts"] = value;
				}
			}

			// Token: 0x17004A05 RID: 18949
			// (set) Token: 0x06006FB3 RID: 28595 RVA: 0x000A8A9D File Offset: 0x000A6C9D
			public virtual int Port
			{
				set
				{
					base.PowerSharpParameters["Port"] = value;
				}
			}

			// Token: 0x17004A06 RID: 18950
			// (set) Token: 0x06006FB4 RID: 28596 RVA: 0x000A8AB5 File Offset: 0x000A6CB5
			public virtual EnhancedTimeSpan ConnectionInactivityTimeOut
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeOut"] = value;
				}
			}

			// Token: 0x17004A07 RID: 18951
			// (set) Token: 0x06006FB5 RID: 28597 RVA: 0x000A8ACD File Offset: 0x000A6CCD
			public virtual Unlimited<ByteQuantifiedSize> MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17004A08 RID: 18952
			// (set) Token: 0x06006FB6 RID: 28598 RVA: 0x000A8AE5 File Offset: 0x000A6CE5
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x17004A09 RID: 18953
			// (set) Token: 0x06006FB7 RID: 28599 RVA: 0x000A8AF8 File Offset: 0x000A6CF8
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x17004A0A RID: 18954
			// (set) Token: 0x06006FB8 RID: 28600 RVA: 0x000A8B0B File Offset: 0x000A6D0B
			public virtual bool ForceHELO
			{
				set
				{
					base.PowerSharpParameters["ForceHELO"] = value;
				}
			}

			// Token: 0x17004A0B RID: 18955
			// (set) Token: 0x06006FB9 RID: 28601 RVA: 0x000A8B23 File Offset: 0x000A6D23
			public virtual bool FrontendProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["FrontendProxyEnabled"] = value;
				}
			}

			// Token: 0x17004A0C RID: 18956
			// (set) Token: 0x06006FBA RID: 28602 RVA: 0x000A8B3B File Offset: 0x000A6D3B
			public virtual bool IgnoreSTARTTLS
			{
				set
				{
					base.PowerSharpParameters["IgnoreSTARTTLS"] = value;
				}
			}

			// Token: 0x17004A0D RID: 18957
			// (set) Token: 0x06006FBB RID: 28603 RVA: 0x000A8B53 File Offset: 0x000A6D53
			public virtual bool CloudServicesMailEnabled
			{
				set
				{
					base.PowerSharpParameters["CloudServicesMailEnabled"] = value;
				}
			}

			// Token: 0x17004A0E RID: 18958
			// (set) Token: 0x06006FBC RID: 28604 RVA: 0x000A8B6B File Offset: 0x000A6D6B
			public virtual bool RequireOorg
			{
				set
				{
					base.PowerSharpParameters["RequireOorg"] = value;
				}
			}

			// Token: 0x17004A0F RID: 18959
			// (set) Token: 0x06006FBD RID: 28605 RVA: 0x000A8B83 File Offset: 0x000A6D83
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x17004A10 RID: 18960
			// (set) Token: 0x06006FBE RID: 28606 RVA: 0x000A8B9B File Offset: 0x000A6D9B
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004A11 RID: 18961
			// (set) Token: 0x06006FBF RID: 28607 RVA: 0x000A8BB3 File Offset: 0x000A6DB3
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x17004A12 RID: 18962
			// (set) Token: 0x06006FC0 RID: 28608 RVA: 0x000A8BCB File Offset: 0x000A6DCB
			public virtual SmtpSendConnectorConfig.AuthMechanisms SmartHostAuthMechanism
			{
				set
				{
					base.PowerSharpParameters["SmartHostAuthMechanism"] = value;
				}
			}

			// Token: 0x17004A13 RID: 18963
			// (set) Token: 0x06006FC1 RID: 28609 RVA: 0x000A8BE3 File Offset: 0x000A6DE3
			public virtual bool UseExternalDNSServersEnabled
			{
				set
				{
					base.PowerSharpParameters["UseExternalDNSServersEnabled"] = value;
				}
			}

			// Token: 0x17004A14 RID: 18964
			// (set) Token: 0x06006FC2 RID: 28610 RVA: 0x000A8BFB File Offset: 0x000A6DFB
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004A15 RID: 18965
			// (set) Token: 0x06006FC3 RID: 28611 RVA: 0x000A8C0E File Offset: 0x000A6E0E
			public virtual IPAddress SourceIPAddress
			{
				set
				{
					base.PowerSharpParameters["SourceIPAddress"] = value;
				}
			}

			// Token: 0x17004A16 RID: 18966
			// (set) Token: 0x06006FC4 RID: 28612 RVA: 0x000A8C21 File Offset: 0x000A6E21
			public virtual int SmtpMaxMessagesPerConnection
			{
				set
				{
					base.PowerSharpParameters["SmtpMaxMessagesPerConnection"] = value;
				}
			}

			// Token: 0x17004A17 RID: 18967
			// (set) Token: 0x06006FC5 RID: 28613 RVA: 0x000A8C39 File Offset: 0x000A6E39
			public virtual PSCredential AuthenticationCredential
			{
				set
				{
					base.PowerSharpParameters["AuthenticationCredential"] = value;
				}
			}

			// Token: 0x17004A18 RID: 18968
			// (set) Token: 0x06006FC6 RID: 28614 RVA: 0x000A8C4C File Offset: 0x000A6E4C
			public virtual MultiValuedProperty<ServerIdParameter> SourceTransportServers
			{
				set
				{
					base.PowerSharpParameters["SourceTransportServers"] = value;
				}
			}

			// Token: 0x17004A19 RID: 18969
			// (set) Token: 0x06006FC7 RID: 28615 RVA: 0x000A8C5F File Offset: 0x000A6E5F
			public virtual SmtpDomainWithSubdomains TlsDomain
			{
				set
				{
					base.PowerSharpParameters["TlsDomain"] = value;
				}
			}

			// Token: 0x17004A1A RID: 18970
			// (set) Token: 0x06006FC8 RID: 28616 RVA: 0x000A8C72 File Offset: 0x000A6E72
			public virtual TlsAuthLevel? TlsAuthLevel
			{
				set
				{
					base.PowerSharpParameters["TlsAuthLevel"] = value;
				}
			}

			// Token: 0x17004A1B RID: 18971
			// (set) Token: 0x06006FC9 RID: 28617 RVA: 0x000A8C8A File Offset: 0x000A6E8A
			public virtual ErrorPolicies ErrorPolicies
			{
				set
				{
					base.PowerSharpParameters["ErrorPolicies"] = value;
				}
			}

			// Token: 0x17004A1C RID: 18972
			// (set) Token: 0x06006FCA RID: 28618 RVA: 0x000A8CA2 File Offset: 0x000A6EA2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004A1D RID: 18973
			// (set) Token: 0x06006FCB RID: 28619 RVA: 0x000A8CB5 File Offset: 0x000A6EB5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A1E RID: 18974
			// (set) Token: 0x06006FCC RID: 28620 RVA: 0x000A8CC8 File Offset: 0x000A6EC8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A1F RID: 18975
			// (set) Token: 0x06006FCD RID: 28621 RVA: 0x000A8CE0 File Offset: 0x000A6EE0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A20 RID: 18976
			// (set) Token: 0x06006FCE RID: 28622 RVA: 0x000A8CF8 File Offset: 0x000A6EF8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A21 RID: 18977
			// (set) Token: 0x06006FCF RID: 28623 RVA: 0x000A8D10 File Offset: 0x000A6F10
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A22 RID: 18978
			// (set) Token: 0x06006FD0 RID: 28624 RVA: 0x000A8D28 File Offset: 0x000A6F28
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

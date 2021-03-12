using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x0200001E RID: 30
	internal static class Schema
	{
		// Token: 0x0200001F RID: 31
		public static class Query
		{
			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005AE5 File Offset: 0x00003CE5
			public static string QueryAll
			{
				get
				{
					return "(objectClass=*)";
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005AEC File Offset: 0x00003CEC
			public static string QueryRecipientsContainer
			{
				get
				{
					return "(CN=Recipients)";
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005AF3 File Offset: 0x00003CF3
			public static string QueryAllEnterpriseRecipients
			{
				get
				{
					if (Schema.Query.queryAllEnterpriseRecipients == null)
					{
						Schema.Query.queryAllEnterpriseRecipients = Schema.Query.BuildRecipientQuery(Schema.Query.RecipientQueryScenario.EnterpriseDirSyncQuery);
					}
					return Schema.Query.queryAllEnterpriseRecipients;
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005B0C File Offset: 0x00003D0C
			public static string QueryAllSmtpRecipients
			{
				get
				{
					return Schema.Query.BuildRecipientQuery(Schema.Query.RecipientQueryScenario.EnterpriseTestEdgeSyncQuery);
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005B14 File Offset: 0x00003D14
			public static string QueryAllHostedSmtpRecipients
			{
				get
				{
					if (Schema.Query.queryAllHostedSmtpRecipients == null)
					{
						Schema.Query.queryAllHostedSmtpRecipients = Schema.Query.BuildRecipientQuery(Schema.Query.RecipientQueryScenario.DataCenterDirSyncQuery);
					}
					return Schema.Query.queryAllHostedSmtpRecipients;
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060000FA RID: 250 RVA: 0x00005B2D File Offset: 0x00003D2D
			public static string QueryBridgeheads
			{
				get
				{
					return "(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=32))";
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x060000FB RID: 251 RVA: 0x00005B34 File Offset: 0x00003D34
			public static string QueryEdges
			{
				get
				{
					return "(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=64))";
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x060000FC RID: 252 RVA: 0x00005B3B File Offset: 0x00003D3B
			public static string QuerySendConnectors
			{
				get
				{
					return "(objectClass=mailGateway)";
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060000FD RID: 253 RVA: 0x00005B42 File Offset: 0x00003D42
			public static string QueryPartnerDomains
			{
				get
				{
					return "(objectClass=msExchDomainContentConfig)";
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060000FE RID: 254 RVA: 0x00005B49 File Offset: 0x00003D49
			public static string QueryTransportSettings
			{
				get
				{
					return "(objectClass=msExchTransportSettings)";
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x060000FF RID: 255 RVA: 0x00005B50 File Offset: 0x00003D50
			public static string QueryExchangeServerRecipients
			{
				get
				{
					return "(objectClass=msExchExchangeServerRecipient)";
				}
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000100 RID: 256 RVA: 0x00005B57 File Offset: 0x00003D57
			public static string QueryMessageClassifications
			{
				get
				{
					return "(objectClass=msExchMessageClassification)";
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000101 RID: 257 RVA: 0x00005B5E File Offset: 0x00003D5E
			public static string QueryAcceptedDomains
			{
				get
				{
					return "(objectClass=msExchAcceptedDomain)";
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000102 RID: 258 RVA: 0x00005B65 File Offset: 0x00003D65
			public static string QueryHostedAcceptedDomains
			{
				get
				{
					return "(&(objectClass=msExchAcceptedDomain)(msExchCU=*)(msExchOURoot=*))";
				}
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000103 RID: 259 RVA: 0x00005B6C File Offset: 0x00003D6C
			public static string QueryPerimeterSettings
			{
				get
				{
					return "(&(objectClass=msExchTenantPerimeterSettings)(msExchCU=*)(msExchOURoot=*))";
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000104 RID: 260 RVA: 0x00005B73 File Offset: 0x00003D73
			public static string QueryUsgMailboxAndOrganization
			{
				get
				{
					return "(|(&(objectClass=group)(groupType:1.2.840.113556.1.4.803:=2147483656)(msExchCU=*)(msExchOURoot=*))(&(objectClass=user)(msExchCU=*)(msExchOURoot=*)(msExchWindowsLiveID=*))(&(objectClass=organizationalUnit)(msExchCU=*)(msExchOURoot=*)(msExchProvisioningFlags=*)))";
				}
			}

			// Token: 0x06000105 RID: 261 RVA: 0x00005B7C File Offset: 0x00003D7C
			public static string BuildHostedRecipientAddressQuery(string attributeName, List<string> addressList)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("(&");
				stringBuilder.Append("(msExchCU=*)(msExchOURoot=*)");
				stringBuilder.Append("(|");
				StringBuilder stringBuilder2 = new StringBuilder();
				string value;
				if (string.Equals(attributeName, "msExchUMAddresses", StringComparison.OrdinalIgnoreCase))
				{
					value = "=meum:";
				}
				else
				{
					value = "=smtp:";
				}
				foreach (string originalValue in addressList)
				{
					stringBuilder2.Append("(");
					stringBuilder2.Append(attributeName);
					stringBuilder2.Append(value);
					ADValueConvertor.EscapeAndAppendString(originalValue, stringBuilder2);
					stringBuilder2.Append(")");
				}
				stringBuilder.Append(stringBuilder2.ToString());
				stringBuilder.Append(")");
				stringBuilder.Append("(|");
				foreach (string value2 in Schema.Query.supportedHostedRecipientClasses)
				{
					stringBuilder.Append("(objectClass=");
					stringBuilder.Append(value2);
					stringBuilder.Append(")");
				}
				stringBuilder.Append("))");
				return stringBuilder.ToString();
			}

			// Token: 0x06000106 RID: 262 RVA: 0x00005CB8 File Offset: 0x00003EB8
			private static string BuildRecipientQuery(Schema.Query.RecipientQueryScenario recipientQueryScenario)
			{
				string[] array = null;
				StringBuilder stringBuilder = new StringBuilder(300);
				stringBuilder.Append("(&");
				switch (recipientQueryScenario)
				{
				case Schema.Query.RecipientQueryScenario.DataCenterDirSyncQuery:
					stringBuilder.Append("(msExchCU=*)(msExchOURoot=*)");
					array = Schema.Query.supportedHostedRecipientClasses;
					break;
				case Schema.Query.RecipientQueryScenario.EnterpriseDirSyncQuery:
					array = Schema.Query.supportedEnterpriseRecipientClasses;
					break;
				case Schema.Query.RecipientQueryScenario.EnterpriseTestEdgeSyncQuery:
					stringBuilder.Append("(proxyAddresses=smtp*)");
					array = Schema.Query.supportedEnterpriseRecipientClasses;
					break;
				}
				stringBuilder.Append("(|");
				foreach (string value in array)
				{
					stringBuilder.Append("(objectClass=");
					stringBuilder.Append(value);
					stringBuilder.Append(")");
				}
				stringBuilder.Append(")");
				stringBuilder.Append("(!(objectclass=computer))");
				stringBuilder.Append(")");
				return stringBuilder.ToString();
			}

			// Token: 0x04000069 RID: 105
			public const string QueryAllProxyAddresses = "(proxyAddresses=*)";

			// Token: 0x0400006A RID: 106
			private const string QueryAllSmtpProxyAddresses = "(proxyAddresses=smtp*)";

			// Token: 0x0400006B RID: 107
			private const string HostedObjects = "(msExchCU=*)(msExchOURoot=*)";

			// Token: 0x0400006C RID: 108
			private static readonly string[] supportedEnterpriseRecipientClasses = new string[]
			{
				"msExchDynamicDistributionList",
				"publicFolder",
				"contact",
				"user",
				"group"
			};

			// Token: 0x0400006D RID: 109
			private static readonly string[] supportedHostedRecipientClasses = new string[]
			{
				"msExchDynamicDistributionList",
				"user",
				"group"
			};

			// Token: 0x0400006E RID: 110
			private static string queryAllEnterpriseRecipients;

			// Token: 0x0400006F RID: 111
			private static string queryAllHostedSmtpRecipients;

			// Token: 0x02000020 RID: 32
			internal enum RecipientQueryScenario
			{
				// Token: 0x04000071 RID: 113
				DataCenterDirSyncQuery,
				// Token: 0x04000072 RID: 114
				EnterpriseDirSyncQuery,
				// Token: 0x04000073 RID: 115
				EnterpriseTestEdgeSyncQuery
			}
		}

		// Token: 0x02000021 RID: 33
		public static class General
		{
			// Token: 0x04000074 RID: 116
			public const string ConfigContext = "configurationNamingContext";

			// Token: 0x04000075 RID: 117
			public const string DefaultContext = "defaultNamingContext";

			// Token: 0x04000076 RID: 118
			public const string NamingContexts = "namingContexts";

			// Token: 0x04000077 RID: 119
			public const string ServerName = "serverName";

			// Token: 0x04000078 RID: 120
			public const string HighestUSN = "highestCommittedUSN";

			// Token: 0x04000079 RID: 121
			public const string NtSecurityDescriptor = "nTSecurityDescriptor";

			// Token: 0x0400007A RID: 122
			public const string EdgeRecipientsPath = "CN=Recipients,OU=MSExchangeGateway";

			// Token: 0x0400007B RID: 123
			public const string MsExchVersion = "msExchVersion";

			// Token: 0x0400007C RID: 124
			public const string MinVersion = "msExchMinAdminVersion";

			// Token: 0x0400007D RID: 125
			public const string VersionNumber = "versionNumber";

			// Token: 0x0400007E RID: 126
			public const string LegacyExchangeDN = "legacyExchangeDN";

			// Token: 0x0400007F RID: 127
			public const string ObjectClass = "objectClass";

			// Token: 0x04000080 RID: 128
			public const string EdgeSyncSourceGuid = "msExchEdgeSyncSourceGuid";

			// Token: 0x04000081 RID: 129
			public const string EdgeSyncCookies = "msExchEdgeSyncCookies";

			// Token: 0x04000082 RID: 130
			public const string SystemFlags = "systemFlags";

			// Token: 0x04000083 RID: 131
			public const string ObjectGUID = "objectGUID";

			// Token: 0x04000084 RID: 132
			public const string WhenCreated = "whenCreated";

			// Token: 0x04000085 RID: 133
			public const string InstanceType = "instanceType";

			// Token: 0x04000086 RID: 134
			public const string ParentGUID = "parentGUID";

			// Token: 0x04000087 RID: 135
			public const string Name = "name";

			// Token: 0x04000088 RID: 136
			public const string SyncErrors = "msExchEdgeSyncCookies";

			// Token: 0x04000089 RID: 137
			public const string ConfigUnitDN = "msExchCU";

			// Token: 0x0400008A RID: 138
			public const string ExchOURoot = "msExchOURoot";

			// Token: 0x0400008B RID: 139
			public const string OtherWellKnownObjects = "otherWellKnownObjects";
		}

		// Token: 0x02000022 RID: 34
		public static class Server
		{
			// Token: 0x0400008C RID: 140
			public const string ClassName = "msExchExchangeServer";

			// Token: 0x0400008D RID: 141
			public const string EdgeSyncLease = "msExchEdgeSyncLease";

			// Token: 0x0400008E RID: 142
			public const string EdgeSyncStatus = "msExchEdgeSyncStatus";

			// Token: 0x0400008F RID: 143
			public const string NetworkAddress = "networkAddress";

			// Token: 0x04000090 RID: 144
			public const string ServerRoles = "msExchCurrentServerRoles";

			// Token: 0x04000091 RID: 145
			public const string ServerSite = "msExchServerSite";

			// Token: 0x04000092 RID: 146
			public const string TransportServerFlags = "msExchTransportFlags";

			// Token: 0x04000093 RID: 147
			public static readonly string[] FilterAttributes = new string[]
			{
				"msExchServerSite"
			};

			// Token: 0x04000094 RID: 148
			public static readonly string[] PayloadAttributes = new string[]
			{
				"msExchVersion",
				"msExchMinAdminVersion",
				"versionNumber",
				"legacyExchangeDN",
				"versionNumber",
				"networkAddress",
				"msExchCurrentServerRoles",
				"msExchEdgeSyncCredential",
				"msExchServerInternalTLSCert",
				"serialNumber",
				"type",
				"msExchProductID"
			};
		}

		// Token: 0x02000023 RID: 35
		public static class DomainConfig
		{
			// Token: 0x04000095 RID: 149
			public static readonly string[] PayloadAttributes = new string[]
			{
				"msExchVersion",
				"msExchMinAdminVersion",
				"domainName",
				"msExchDomainContentConfigFlags",
				"msExchPermittedAuthN",
				"msExchMLSDomainGatewaySMTPAddress",
				"msExchReceiveHashedPassword",
				"msExchReceiveUserName",
				"msExchSendEncryptedPassword",
				"msExchSendUserName",
				"msExchTlsAlternateSubject",
				"msExchNonMIMECharacterSet"
			};
		}

		// Token: 0x02000024 RID: 36
		public static class TransportConfig
		{
			// Token: 0x04000096 RID: 150
			public static readonly string[] PayloadAttributes = new string[]
			{
				"msExchInternalSMTPServers",
				"msExchTLSReceiveDomainSecureList",
				"msExchTLSSendDomainSecureList",
				"msExchTransportSettingsFlags",
				"msExchTransportShadowHeartbeatTimeoutInterval",
				"msExchTransportShadowHeartbeatRetryCount",
				"msExchTransportShadowMessageAutoDiscardInterval"
			};
		}

		// Token: 0x02000025 RID: 37
		public static class ExchangeRecipient
		{
			// Token: 0x04000097 RID: 151
			public static readonly string[] PayloadAttributes = new string[]
			{
				"msExchVersion",
				"proxyAddresses",
				"displayName"
			};
		}

		// Token: 0x02000026 RID: 38
		public static class AcceptedDomain
		{
			// Token: 0x04000098 RID: 152
			public const string ClassName = "msExchAcceptedDomain";

			// Token: 0x04000099 RID: 153
			public const string DomainName = "msExchAcceptedDomainName";

			// Token: 0x0400009A RID: 154
			public const string DomainFlags = "msExchAcceptedDomainFlags";

			// Token: 0x0400009B RID: 155
			public const string MailFlowPartner = "msExchTransportResellerSettingsLink";

			// Token: 0x0400009C RID: 156
			public const string PerimeterDuplicateDetectedFlags = "msExchTransportInboundSettings";

			// Token: 0x0400009D RID: 157
			public static readonly string[] PayloadAttributes = new string[]
			{
				"msExchVersion",
				"msExchAcceptedDomainName",
				"msExchAcceptedDomainFlags",
				"msExchEncryptedTLSP12"
			};
		}

		// Token: 0x02000027 RID: 39
		public static class SendConnector
		{
			// Token: 0x0400009E RID: 158
			public const string SourceBridgehead = "msExchSourceBridgeheadServersDN";

			// Token: 0x0400009F RID: 159
			public const string SendFlags = "msExchSmtpSendFlags";

			// Token: 0x040000A0 RID: 160
			public static readonly string[] PayloadAttributes = new string[]
			{
				"msExchVersion",
				"msExchMinAdminVersion",
				"routingList",
				"msExchSmtpSmartHost",
				"msExchSmtpSendPort",
				"msExchSmtpSendConnectionTimeout",
				"delivContLength",
				"msExchSmtpSendProtocolLoggingLevel",
				"msExchSmtpSendFlags",
				"msExchSmtpSendBindingIPAddress",
				"deliveryMechanism",
				"msExchSmtpOutboundSecurityFlag",
				"msExchSMTPSendConnectorFQDN",
				"msExchSmtpSendEnabled",
				"msExchSMTPSendExternallySecuredAs",
				"msExchSmtpOutboundSecurityUserName",
				"msExchSmtpOutboundSecurityPassword",
				"msExchSmtpSendTlsDomain",
				"msExchSmtpSendNdrLevel",
				"msExchSmtpMaxMessagesPerConnection",
				"msExchSmtpTLSCertificate"
			};

			// Token: 0x040000A1 RID: 161
			internal static readonly ServerVersion ServerVersion14_1_144 = new ServerVersion(14, 1, 144, 0);

			// Token: 0x040000A2 RID: 162
			internal static readonly ServerVersion ServerVersion15_0_620 = new ServerVersion(15, 0, 620, 0);

			// Token: 0x040000A3 RID: 163
			internal static readonly string[] NewAttributesInServerVersion14_1_144 = new string[]
			{
				"msExchSmtpSendTlsDomain",
				"msExchSmtpSendNdrLevel"
			};

			// Token: 0x040000A4 RID: 164
			internal static readonly string[] NewAttributesInServerVersion15_0_620 = new string[]
			{
				"msExchSmtpTLSCertificate"
			};
		}

		// Token: 0x02000028 RID: 40
		public static class MessageClassification
		{
			// Token: 0x040000A5 RID: 165
			public static readonly string[] PayloadAttributes = new string[]
			{
				"msExchMessageClassificationBanner",
				"msExchMessageClassificationConfidentialityAction",
				"msExchMessageClassificationDisplayPrecedence",
				"msExchMessageClassificationFlags",
				"msExchMessageClassificationIntegrityAction",
				"msExchMessageClassificationID",
				"msExchMessageClassificationLocale",
				"msExchMessageClassificationURL",
				"msExchMessageClassificationVersion"
			};
		}

		// Token: 0x02000029 RID: 41
		public static class PerimeterSettings
		{
			// Token: 0x040000A6 RID: 166
			public const string ClassName = "msExchTenantPerimeterSettings";

			// Token: 0x040000A7 RID: 167
			public const string EhfCompanyId = "msExchTenantPerimeterSettingsOrgID";

			// Token: 0x040000A8 RID: 168
			public const string Flags = "msExchTenantPerimeterSettingsFlags";

			// Token: 0x040000A9 RID: 169
			public const string GatewayIPAddresses = "msExchTenantPerimeterSettingsGatewayIPAddresses";

			// Token: 0x040000AA RID: 170
			public const string InternalServerIPAddresses = "msExchTenantPerimeterSettingsInternalServerIPAddresses";

			// Token: 0x040000AB RID: 171
			public const string ForceDomainSync = "msExchTransportInboundSettings";

			// Token: 0x040000AC RID: 172
			public const string TargetServerAdmins = "msExchTargetServerAdmins";

			// Token: 0x040000AD RID: 173
			public const string TargetServerViewOnlyAdmins = "msExchTargetServerViewOnlyAdmins";

			// Token: 0x040000AE RID: 174
			public const string TargetServerPartnerAdmins = "msExchTargetServerPartnerAdmins";

			// Token: 0x040000AF RID: 175
			public const string TargetServerPartnerViewOnlyAdmins = "msExchTargetServerPartnerViewOnlyAdmins";
		}

		// Token: 0x0200002A RID: 42
		public static class Recipient
		{
			// Token: 0x040000B0 RID: 176
			public const string ProxyAddresses = "proxyAddresses";

			// Token: 0x040000B1 RID: 177
			public const string SignupAddresses = "msExchSignupAddresses";

			// Token: 0x040000B2 RID: 178
			public const string ExternalSyncState = "msExchExternalSyncState";

			// Token: 0x040000B3 RID: 179
			public const string TransportSettings = "msExchTransportRecipientSettingsFlags";

			// Token: 0x040000B4 RID: 180
			public const string TargetAddress = "targetAddress";

			// Token: 0x040000B5 RID: 181
			public const string UMProxyAddresses = "msExchUMAddresses";

			// Token: 0x040000B6 RID: 182
			public const string WindowsLiveId = "msExchWindowsLiveID";

			// Token: 0x040000B7 RID: 183
			public const string ClassName = "user";

			// Token: 0x040000B8 RID: 184
			public const string ArchiveGuid = "msExchArchiveGUID";

			// Token: 0x040000B9 RID: 185
			public const string ArchiveAddress = "ArchiveAddress";

			// Token: 0x040000BA RID: 186
			public const string CapabilityIdentifiers = "msExchCapabilityIdentifiers";

			// Token: 0x040000BB RID: 187
			public const string PartnerGroupID = "msExchPartnerGroupID";

			// Token: 0x040000BC RID: 188
			public const string ExternalDirectoryObjectId = "msExchExternalDirectoryObjectId";

			// Token: 0x040000BD RID: 189
			public const string RecipientTypeDetailsValue = "msExchRecipientTypeDetails";

			// Token: 0x040000BE RID: 190
			public const string TenantCU = "msExchCU";

			// Token: 0x040000BF RID: 191
			public const string MailNickname = "mailNickname";

			// Token: 0x040000C0 RID: 192
			public const string ServerLegacyDN = "msExchHomeServerName";
		}

		// Token: 0x0200002B RID: 43
		public static class UniversalSecurityGroup
		{
			// Token: 0x040000C1 RID: 193
			public const string ClassName = "group";

			// Token: 0x040000C2 RID: 194
			public const string Member = "member";
		}

		// Token: 0x0200002C RID: 44
		public static class MailFlowPartner
		{
			// Token: 0x040000C3 RID: 195
			public const string PartnerInboundGatewayId = "msExchTransportResellerSettingsInboundGatewayID";

			// Token: 0x040000C4 RID: 196
			public const string PartnerOutboundGatewayId = "msExchTransportResellerSettingsOutboundGatewayID";
		}

		// Token: 0x0200002D RID: 45
		public static class Organization
		{
			// Token: 0x040000C5 RID: 197
			public const string ClassName = "organizationalUnit";

			// Token: 0x040000C6 RID: 198
			public const string ProvisioningFlags = "msExchProvisioningFlags";
		}
	}
}

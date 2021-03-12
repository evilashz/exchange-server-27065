using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008DB RID: 2267
	public class SetReceiveConnectorCommand : SyntheticCommandWithPipelineInputNoOutput<ReceiveConnector>
	{
		// Token: 0x06007163 RID: 29027 RVA: 0x000AAD2F File Offset: 0x000A8F2F
		private SetReceiveConnectorCommand() : base("Set-ReceiveConnector")
		{
		}

		// Token: 0x06007164 RID: 29028 RVA: 0x000AAD3C File Offset: 0x000A8F3C
		public SetReceiveConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007165 RID: 29029 RVA: 0x000AAD4B File Offset: 0x000A8F4B
		public virtual SetReceiveConnectorCommand SetParameters(SetReceiveConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007166 RID: 29030 RVA: 0x000AAD55 File Offset: 0x000A8F55
		public virtual SetReceiveConnectorCommand SetParameters(SetReceiveConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008DC RID: 2268
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004B66 RID: 19302
			// (set) Token: 0x06007167 RID: 29031 RVA: 0x000AAD5F File Offset: 0x000A8F5F
			public virtual AcceptedDomainIdParameter DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x17004B67 RID: 19303
			// (set) Token: 0x06007168 RID: 29032 RVA: 0x000AAD72 File Offset: 0x000A8F72
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004B68 RID: 19304
			// (set) Token: 0x06007169 RID: 29033 RVA: 0x000AAD85 File Offset: 0x000A8F85
			public virtual AuthMechanisms AuthMechanism
			{
				set
				{
					base.PowerSharpParameters["AuthMechanism"] = value;
				}
			}

			// Token: 0x17004B69 RID: 19305
			// (set) Token: 0x0600716A RID: 29034 RVA: 0x000AAD9D File Offset: 0x000A8F9D
			public virtual string Banner
			{
				set
				{
					base.PowerSharpParameters["Banner"] = value;
				}
			}

			// Token: 0x17004B6A RID: 19306
			// (set) Token: 0x0600716B RID: 29035 RVA: 0x000AADB0 File Offset: 0x000A8FB0
			public virtual bool BinaryMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["BinaryMimeEnabled"] = value;
				}
			}

			// Token: 0x17004B6B RID: 19307
			// (set) Token: 0x0600716C RID: 29036 RVA: 0x000AADC8 File Offset: 0x000A8FC8
			public virtual MultiValuedProperty<IPBinding> Bindings
			{
				set
				{
					base.PowerSharpParameters["Bindings"] = value;
				}
			}

			// Token: 0x17004B6C RID: 19308
			// (set) Token: 0x0600716D RID: 29037 RVA: 0x000AADDB File Offset: 0x000A8FDB
			public virtual bool ChunkingEnabled
			{
				set
				{
					base.PowerSharpParameters["ChunkingEnabled"] = value;
				}
			}

			// Token: 0x17004B6D RID: 19309
			// (set) Token: 0x0600716E RID: 29038 RVA: 0x000AADF3 File Offset: 0x000A8FF3
			public virtual bool DeliveryStatusNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryStatusNotificationEnabled"] = value;
				}
			}

			// Token: 0x17004B6E RID: 19310
			// (set) Token: 0x0600716F RID: 29039 RVA: 0x000AAE0B File Offset: 0x000A900B
			public virtual bool EightBitMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["EightBitMimeEnabled"] = value;
				}
			}

			// Token: 0x17004B6F RID: 19311
			// (set) Token: 0x06007170 RID: 29040 RVA: 0x000AAE23 File Offset: 0x000A9023
			public virtual bool SmtpUtf8Enabled
			{
				set
				{
					base.PowerSharpParameters["SmtpUtf8Enabled"] = value;
				}
			}

			// Token: 0x17004B70 RID: 19312
			// (set) Token: 0x06007171 RID: 29041 RVA: 0x000AAE3B File Offset: 0x000A903B
			public virtual bool BareLinefeedRejectionEnabled
			{
				set
				{
					base.PowerSharpParameters["BareLinefeedRejectionEnabled"] = value;
				}
			}

			// Token: 0x17004B71 RID: 19313
			// (set) Token: 0x06007172 RID: 29042 RVA: 0x000AAE53 File Offset: 0x000A9053
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x17004B72 RID: 19314
			// (set) Token: 0x06007173 RID: 29043 RVA: 0x000AAE6B File Offset: 0x000A906B
			public virtual bool EnhancedStatusCodesEnabled
			{
				set
				{
					base.PowerSharpParameters["EnhancedStatusCodesEnabled"] = value;
				}
			}

			// Token: 0x17004B73 RID: 19315
			// (set) Token: 0x06007174 RID: 29044 RVA: 0x000AAE83 File Offset: 0x000A9083
			public virtual bool LongAddressesEnabled
			{
				set
				{
					base.PowerSharpParameters["LongAddressesEnabled"] = value;
				}
			}

			// Token: 0x17004B74 RID: 19316
			// (set) Token: 0x06007175 RID: 29045 RVA: 0x000AAE9B File Offset: 0x000A909B
			public virtual bool OrarEnabled
			{
				set
				{
					base.PowerSharpParameters["OrarEnabled"] = value;
				}
			}

			// Token: 0x17004B75 RID: 19317
			// (set) Token: 0x06007176 RID: 29046 RVA: 0x000AAEB3 File Offset: 0x000A90B3
			public virtual bool SuppressXAnonymousTls
			{
				set
				{
					base.PowerSharpParameters["SuppressXAnonymousTls"] = value;
				}
			}

			// Token: 0x17004B76 RID: 19318
			// (set) Token: 0x06007177 RID: 29047 RVA: 0x000AAECB File Offset: 0x000A90CB
			public virtual bool ProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["ProxyEnabled"] = value;
				}
			}

			// Token: 0x17004B77 RID: 19319
			// (set) Token: 0x06007178 RID: 29048 RVA: 0x000AAEE3 File Offset: 0x000A90E3
			public virtual bool AdvertiseClientSettings
			{
				set
				{
					base.PowerSharpParameters["AdvertiseClientSettings"] = value;
				}
			}

			// Token: 0x17004B78 RID: 19320
			// (set) Token: 0x06007179 RID: 29049 RVA: 0x000AAEFB File Offset: 0x000A90FB
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x17004B79 RID: 19321
			// (set) Token: 0x0600717A RID: 29050 RVA: 0x000AAF0E File Offset: 0x000A910E
			public virtual Fqdn ServiceDiscoveryFqdn
			{
				set
				{
					base.PowerSharpParameters["ServiceDiscoveryFqdn"] = value;
				}
			}

			// Token: 0x17004B7A RID: 19322
			// (set) Token: 0x0600717B RID: 29051 RVA: 0x000AAF21 File Offset: 0x000A9121
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x17004B7B RID: 19323
			// (set) Token: 0x0600717C RID: 29052 RVA: 0x000AAF34 File Offset: 0x000A9134
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004B7C RID: 19324
			// (set) Token: 0x0600717D RID: 29053 RVA: 0x000AAF47 File Offset: 0x000A9147
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004B7D RID: 19325
			// (set) Token: 0x0600717E RID: 29054 RVA: 0x000AAF5F File Offset: 0x000A915F
			public virtual EnhancedTimeSpan ConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionTimeout"] = value;
				}
			}

			// Token: 0x17004B7E RID: 19326
			// (set) Token: 0x0600717F RID: 29055 RVA: 0x000AAF77 File Offset: 0x000A9177
			public virtual EnhancedTimeSpan ConnectionInactivityTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeout"] = value;
				}
			}

			// Token: 0x17004B7F RID: 19327
			// (set) Token: 0x06007180 RID: 29056 RVA: 0x000AAF8F File Offset: 0x000A918F
			public virtual Unlimited<int> MessageRateLimit
			{
				set
				{
					base.PowerSharpParameters["MessageRateLimit"] = value;
				}
			}

			// Token: 0x17004B80 RID: 19328
			// (set) Token: 0x06007181 RID: 29057 RVA: 0x000AAFA7 File Offset: 0x000A91A7
			public virtual MessageRateSourceFlags MessageRateSource
			{
				set
				{
					base.PowerSharpParameters["MessageRateSource"] = value;
				}
			}

			// Token: 0x17004B81 RID: 19329
			// (set) Token: 0x06007182 RID: 29058 RVA: 0x000AAFBF File Offset: 0x000A91BF
			public virtual Unlimited<int> MaxInboundConnection
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnection"] = value;
				}
			}

			// Token: 0x17004B82 RID: 19330
			// (set) Token: 0x06007183 RID: 29059 RVA: 0x000AAFD7 File Offset: 0x000A91D7
			public virtual Unlimited<int> MaxInboundConnectionPerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPerSource"] = value;
				}
			}

			// Token: 0x17004B83 RID: 19331
			// (set) Token: 0x06007184 RID: 29060 RVA: 0x000AAFEF File Offset: 0x000A91EF
			public virtual int MaxInboundConnectionPercentagePerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPercentagePerSource"] = value;
				}
			}

			// Token: 0x17004B84 RID: 19332
			// (set) Token: 0x06007185 RID: 29061 RVA: 0x000AB007 File Offset: 0x000A9207
			public virtual ByteQuantifiedSize MaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["MaxHeaderSize"] = value;
				}
			}

			// Token: 0x17004B85 RID: 19333
			// (set) Token: 0x06007186 RID: 29062 RVA: 0x000AB01F File Offset: 0x000A921F
			public virtual int MaxHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxHopCount"] = value;
				}
			}

			// Token: 0x17004B86 RID: 19334
			// (set) Token: 0x06007187 RID: 29063 RVA: 0x000AB037 File Offset: 0x000A9237
			public virtual int MaxLocalHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxLocalHopCount"] = value;
				}
			}

			// Token: 0x17004B87 RID: 19335
			// (set) Token: 0x06007188 RID: 29064 RVA: 0x000AB04F File Offset: 0x000A924F
			public virtual int MaxLogonFailures
			{
				set
				{
					base.PowerSharpParameters["MaxLogonFailures"] = value;
				}
			}

			// Token: 0x17004B88 RID: 19336
			// (set) Token: 0x06007189 RID: 29065 RVA: 0x000AB067 File Offset: 0x000A9267
			public virtual ByteQuantifiedSize MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17004B89 RID: 19337
			// (set) Token: 0x0600718A RID: 29066 RVA: 0x000AB07F File Offset: 0x000A927F
			public virtual Unlimited<int> MaxProtocolErrors
			{
				set
				{
					base.PowerSharpParameters["MaxProtocolErrors"] = value;
				}
			}

			// Token: 0x17004B8A RID: 19338
			// (set) Token: 0x0600718B RID: 29067 RVA: 0x000AB097 File Offset: 0x000A9297
			public virtual int MaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x17004B8B RID: 19339
			// (set) Token: 0x0600718C RID: 29068 RVA: 0x000AB0AF File Offset: 0x000A92AF
			public virtual PermissionGroups PermissionGroups
			{
				set
				{
					base.PowerSharpParameters["PermissionGroups"] = value;
				}
			}

			// Token: 0x17004B8C RID: 19340
			// (set) Token: 0x0600718D RID: 29069 RVA: 0x000AB0C7 File Offset: 0x000A92C7
			public virtual bool PipeliningEnabled
			{
				set
				{
					base.PowerSharpParameters["PipeliningEnabled"] = value;
				}
			}

			// Token: 0x17004B8D RID: 19341
			// (set) Token: 0x0600718E RID: 29070 RVA: 0x000AB0DF File Offset: 0x000A92DF
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x17004B8E RID: 19342
			// (set) Token: 0x0600718F RID: 29071 RVA: 0x000AB0F7 File Offset: 0x000A92F7
			public virtual MultiValuedProperty<IPRange> RemoteIPRanges
			{
				set
				{
					base.PowerSharpParameters["RemoteIPRanges"] = value;
				}
			}

			// Token: 0x17004B8F RID: 19343
			// (set) Token: 0x06007190 RID: 29072 RVA: 0x000AB10A File Offset: 0x000A930A
			public virtual bool RequireEHLODomain
			{
				set
				{
					base.PowerSharpParameters["RequireEHLODomain"] = value;
				}
			}

			// Token: 0x17004B90 RID: 19344
			// (set) Token: 0x06007191 RID: 29073 RVA: 0x000AB122 File Offset: 0x000A9322
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x17004B91 RID: 19345
			// (set) Token: 0x06007192 RID: 29074 RVA: 0x000AB13A File Offset: 0x000A933A
			public virtual bool EnableAuthGSSAPI
			{
				set
				{
					base.PowerSharpParameters["EnableAuthGSSAPI"] = value;
				}
			}

			// Token: 0x17004B92 RID: 19346
			// (set) Token: 0x06007193 RID: 29075 RVA: 0x000AB152 File Offset: 0x000A9352
			public virtual ExtendedProtectionPolicySetting ExtendedProtectionPolicy
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionPolicy"] = value;
				}
			}

			// Token: 0x17004B93 RID: 19347
			// (set) Token: 0x06007194 RID: 29076 RVA: 0x000AB16A File Offset: 0x000A936A
			public virtual bool LiveCredentialEnabled
			{
				set
				{
					base.PowerSharpParameters["LiveCredentialEnabled"] = value;
				}
			}

			// Token: 0x17004B94 RID: 19348
			// (set) Token: 0x06007195 RID: 29077 RVA: 0x000AB182 File Offset: 0x000A9382
			public virtual MultiValuedProperty<SmtpReceiveDomainCapabilities> TlsDomainCapabilities
			{
				set
				{
					base.PowerSharpParameters["TlsDomainCapabilities"] = value;
				}
			}

			// Token: 0x17004B95 RID: 19349
			// (set) Token: 0x06007196 RID: 29078 RVA: 0x000AB195 File Offset: 0x000A9395
			public virtual ServerRole TransportRole
			{
				set
				{
					base.PowerSharpParameters["TransportRole"] = value;
				}
			}

			// Token: 0x17004B96 RID: 19350
			// (set) Token: 0x06007197 RID: 29079 RVA: 0x000AB1AD File Offset: 0x000A93AD
			public virtual SizeMode SizeEnabled
			{
				set
				{
					base.PowerSharpParameters["SizeEnabled"] = value;
				}
			}

			// Token: 0x17004B97 RID: 19351
			// (set) Token: 0x06007198 RID: 29080 RVA: 0x000AB1C5 File Offset: 0x000A93C5
			public virtual EnhancedTimeSpan TarpitInterval
			{
				set
				{
					base.PowerSharpParameters["TarpitInterval"] = value;
				}
			}

			// Token: 0x17004B98 RID: 19352
			// (set) Token: 0x06007199 RID: 29081 RVA: 0x000AB1DD File Offset: 0x000A93DD
			public virtual EnhancedTimeSpan MaxAcknowledgementDelay
			{
				set
				{
					base.PowerSharpParameters["MaxAcknowledgementDelay"] = value;
				}
			}

			// Token: 0x17004B99 RID: 19353
			// (set) Token: 0x0600719A RID: 29082 RVA: 0x000AB1F5 File Offset: 0x000A93F5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004B9A RID: 19354
			// (set) Token: 0x0600719B RID: 29083 RVA: 0x000AB208 File Offset: 0x000A9408
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004B9B RID: 19355
			// (set) Token: 0x0600719C RID: 29084 RVA: 0x000AB220 File Offset: 0x000A9420
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004B9C RID: 19356
			// (set) Token: 0x0600719D RID: 29085 RVA: 0x000AB238 File Offset: 0x000A9438
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004B9D RID: 19357
			// (set) Token: 0x0600719E RID: 29086 RVA: 0x000AB250 File Offset: 0x000A9450
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004B9E RID: 19358
			// (set) Token: 0x0600719F RID: 29087 RVA: 0x000AB268 File Offset: 0x000A9468
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008DD RID: 2269
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004B9F RID: 19359
			// (set) Token: 0x060071A1 RID: 29089 RVA: 0x000AB288 File Offset: 0x000A9488
			public virtual ReceiveConnectorIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004BA0 RID: 19360
			// (set) Token: 0x060071A2 RID: 29090 RVA: 0x000AB29B File Offset: 0x000A949B
			public virtual AcceptedDomainIdParameter DefaultDomain
			{
				set
				{
					base.PowerSharpParameters["DefaultDomain"] = value;
				}
			}

			// Token: 0x17004BA1 RID: 19361
			// (set) Token: 0x060071A3 RID: 29091 RVA: 0x000AB2AE File Offset: 0x000A94AE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004BA2 RID: 19362
			// (set) Token: 0x060071A4 RID: 29092 RVA: 0x000AB2C1 File Offset: 0x000A94C1
			public virtual AuthMechanisms AuthMechanism
			{
				set
				{
					base.PowerSharpParameters["AuthMechanism"] = value;
				}
			}

			// Token: 0x17004BA3 RID: 19363
			// (set) Token: 0x060071A5 RID: 29093 RVA: 0x000AB2D9 File Offset: 0x000A94D9
			public virtual string Banner
			{
				set
				{
					base.PowerSharpParameters["Banner"] = value;
				}
			}

			// Token: 0x17004BA4 RID: 19364
			// (set) Token: 0x060071A6 RID: 29094 RVA: 0x000AB2EC File Offset: 0x000A94EC
			public virtual bool BinaryMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["BinaryMimeEnabled"] = value;
				}
			}

			// Token: 0x17004BA5 RID: 19365
			// (set) Token: 0x060071A7 RID: 29095 RVA: 0x000AB304 File Offset: 0x000A9504
			public virtual MultiValuedProperty<IPBinding> Bindings
			{
				set
				{
					base.PowerSharpParameters["Bindings"] = value;
				}
			}

			// Token: 0x17004BA6 RID: 19366
			// (set) Token: 0x060071A8 RID: 29096 RVA: 0x000AB317 File Offset: 0x000A9517
			public virtual bool ChunkingEnabled
			{
				set
				{
					base.PowerSharpParameters["ChunkingEnabled"] = value;
				}
			}

			// Token: 0x17004BA7 RID: 19367
			// (set) Token: 0x060071A9 RID: 29097 RVA: 0x000AB32F File Offset: 0x000A952F
			public virtual bool DeliveryStatusNotificationEnabled
			{
				set
				{
					base.PowerSharpParameters["DeliveryStatusNotificationEnabled"] = value;
				}
			}

			// Token: 0x17004BA8 RID: 19368
			// (set) Token: 0x060071AA RID: 29098 RVA: 0x000AB347 File Offset: 0x000A9547
			public virtual bool EightBitMimeEnabled
			{
				set
				{
					base.PowerSharpParameters["EightBitMimeEnabled"] = value;
				}
			}

			// Token: 0x17004BA9 RID: 19369
			// (set) Token: 0x060071AB RID: 29099 RVA: 0x000AB35F File Offset: 0x000A955F
			public virtual bool SmtpUtf8Enabled
			{
				set
				{
					base.PowerSharpParameters["SmtpUtf8Enabled"] = value;
				}
			}

			// Token: 0x17004BAA RID: 19370
			// (set) Token: 0x060071AC RID: 29100 RVA: 0x000AB377 File Offset: 0x000A9577
			public virtual bool BareLinefeedRejectionEnabled
			{
				set
				{
					base.PowerSharpParameters["BareLinefeedRejectionEnabled"] = value;
				}
			}

			// Token: 0x17004BAB RID: 19371
			// (set) Token: 0x060071AD RID: 29101 RVA: 0x000AB38F File Offset: 0x000A958F
			public virtual bool DomainSecureEnabled
			{
				set
				{
					base.PowerSharpParameters["DomainSecureEnabled"] = value;
				}
			}

			// Token: 0x17004BAC RID: 19372
			// (set) Token: 0x060071AE RID: 29102 RVA: 0x000AB3A7 File Offset: 0x000A95A7
			public virtual bool EnhancedStatusCodesEnabled
			{
				set
				{
					base.PowerSharpParameters["EnhancedStatusCodesEnabled"] = value;
				}
			}

			// Token: 0x17004BAD RID: 19373
			// (set) Token: 0x060071AF RID: 29103 RVA: 0x000AB3BF File Offset: 0x000A95BF
			public virtual bool LongAddressesEnabled
			{
				set
				{
					base.PowerSharpParameters["LongAddressesEnabled"] = value;
				}
			}

			// Token: 0x17004BAE RID: 19374
			// (set) Token: 0x060071B0 RID: 29104 RVA: 0x000AB3D7 File Offset: 0x000A95D7
			public virtual bool OrarEnabled
			{
				set
				{
					base.PowerSharpParameters["OrarEnabled"] = value;
				}
			}

			// Token: 0x17004BAF RID: 19375
			// (set) Token: 0x060071B1 RID: 29105 RVA: 0x000AB3EF File Offset: 0x000A95EF
			public virtual bool SuppressXAnonymousTls
			{
				set
				{
					base.PowerSharpParameters["SuppressXAnonymousTls"] = value;
				}
			}

			// Token: 0x17004BB0 RID: 19376
			// (set) Token: 0x060071B2 RID: 29106 RVA: 0x000AB407 File Offset: 0x000A9607
			public virtual bool ProxyEnabled
			{
				set
				{
					base.PowerSharpParameters["ProxyEnabled"] = value;
				}
			}

			// Token: 0x17004BB1 RID: 19377
			// (set) Token: 0x060071B3 RID: 29107 RVA: 0x000AB41F File Offset: 0x000A961F
			public virtual bool AdvertiseClientSettings
			{
				set
				{
					base.PowerSharpParameters["AdvertiseClientSettings"] = value;
				}
			}

			// Token: 0x17004BB2 RID: 19378
			// (set) Token: 0x060071B4 RID: 29108 RVA: 0x000AB437 File Offset: 0x000A9637
			public virtual Fqdn Fqdn
			{
				set
				{
					base.PowerSharpParameters["Fqdn"] = value;
				}
			}

			// Token: 0x17004BB3 RID: 19379
			// (set) Token: 0x060071B5 RID: 29109 RVA: 0x000AB44A File Offset: 0x000A964A
			public virtual Fqdn ServiceDiscoveryFqdn
			{
				set
				{
					base.PowerSharpParameters["ServiceDiscoveryFqdn"] = value;
				}
			}

			// Token: 0x17004BB4 RID: 19380
			// (set) Token: 0x060071B6 RID: 29110 RVA: 0x000AB45D File Offset: 0x000A965D
			public virtual SmtpX509Identifier TlsCertificateName
			{
				set
				{
					base.PowerSharpParameters["TlsCertificateName"] = value;
				}
			}

			// Token: 0x17004BB5 RID: 19381
			// (set) Token: 0x060071B7 RID: 29111 RVA: 0x000AB470 File Offset: 0x000A9670
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17004BB6 RID: 19382
			// (set) Token: 0x060071B8 RID: 29112 RVA: 0x000AB483 File Offset: 0x000A9683
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17004BB7 RID: 19383
			// (set) Token: 0x060071B9 RID: 29113 RVA: 0x000AB49B File Offset: 0x000A969B
			public virtual EnhancedTimeSpan ConnectionTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionTimeout"] = value;
				}
			}

			// Token: 0x17004BB8 RID: 19384
			// (set) Token: 0x060071BA RID: 29114 RVA: 0x000AB4B3 File Offset: 0x000A96B3
			public virtual EnhancedTimeSpan ConnectionInactivityTimeout
			{
				set
				{
					base.PowerSharpParameters["ConnectionInactivityTimeout"] = value;
				}
			}

			// Token: 0x17004BB9 RID: 19385
			// (set) Token: 0x060071BB RID: 29115 RVA: 0x000AB4CB File Offset: 0x000A96CB
			public virtual Unlimited<int> MessageRateLimit
			{
				set
				{
					base.PowerSharpParameters["MessageRateLimit"] = value;
				}
			}

			// Token: 0x17004BBA RID: 19386
			// (set) Token: 0x060071BC RID: 29116 RVA: 0x000AB4E3 File Offset: 0x000A96E3
			public virtual MessageRateSourceFlags MessageRateSource
			{
				set
				{
					base.PowerSharpParameters["MessageRateSource"] = value;
				}
			}

			// Token: 0x17004BBB RID: 19387
			// (set) Token: 0x060071BD RID: 29117 RVA: 0x000AB4FB File Offset: 0x000A96FB
			public virtual Unlimited<int> MaxInboundConnection
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnection"] = value;
				}
			}

			// Token: 0x17004BBC RID: 19388
			// (set) Token: 0x060071BE RID: 29118 RVA: 0x000AB513 File Offset: 0x000A9713
			public virtual Unlimited<int> MaxInboundConnectionPerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPerSource"] = value;
				}
			}

			// Token: 0x17004BBD RID: 19389
			// (set) Token: 0x060071BF RID: 29119 RVA: 0x000AB52B File Offset: 0x000A972B
			public virtual int MaxInboundConnectionPercentagePerSource
			{
				set
				{
					base.PowerSharpParameters["MaxInboundConnectionPercentagePerSource"] = value;
				}
			}

			// Token: 0x17004BBE RID: 19390
			// (set) Token: 0x060071C0 RID: 29120 RVA: 0x000AB543 File Offset: 0x000A9743
			public virtual ByteQuantifiedSize MaxHeaderSize
			{
				set
				{
					base.PowerSharpParameters["MaxHeaderSize"] = value;
				}
			}

			// Token: 0x17004BBF RID: 19391
			// (set) Token: 0x060071C1 RID: 29121 RVA: 0x000AB55B File Offset: 0x000A975B
			public virtual int MaxHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxHopCount"] = value;
				}
			}

			// Token: 0x17004BC0 RID: 19392
			// (set) Token: 0x060071C2 RID: 29122 RVA: 0x000AB573 File Offset: 0x000A9773
			public virtual int MaxLocalHopCount
			{
				set
				{
					base.PowerSharpParameters["MaxLocalHopCount"] = value;
				}
			}

			// Token: 0x17004BC1 RID: 19393
			// (set) Token: 0x060071C3 RID: 29123 RVA: 0x000AB58B File Offset: 0x000A978B
			public virtual int MaxLogonFailures
			{
				set
				{
					base.PowerSharpParameters["MaxLogonFailures"] = value;
				}
			}

			// Token: 0x17004BC2 RID: 19394
			// (set) Token: 0x060071C4 RID: 29124 RVA: 0x000AB5A3 File Offset: 0x000A97A3
			public virtual ByteQuantifiedSize MaxMessageSize
			{
				set
				{
					base.PowerSharpParameters["MaxMessageSize"] = value;
				}
			}

			// Token: 0x17004BC3 RID: 19395
			// (set) Token: 0x060071C5 RID: 29125 RVA: 0x000AB5BB File Offset: 0x000A97BB
			public virtual Unlimited<int> MaxProtocolErrors
			{
				set
				{
					base.PowerSharpParameters["MaxProtocolErrors"] = value;
				}
			}

			// Token: 0x17004BC4 RID: 19396
			// (set) Token: 0x060071C6 RID: 29126 RVA: 0x000AB5D3 File Offset: 0x000A97D3
			public virtual int MaxRecipientsPerMessage
			{
				set
				{
					base.PowerSharpParameters["MaxRecipientsPerMessage"] = value;
				}
			}

			// Token: 0x17004BC5 RID: 19397
			// (set) Token: 0x060071C7 RID: 29127 RVA: 0x000AB5EB File Offset: 0x000A97EB
			public virtual PermissionGroups PermissionGroups
			{
				set
				{
					base.PowerSharpParameters["PermissionGroups"] = value;
				}
			}

			// Token: 0x17004BC6 RID: 19398
			// (set) Token: 0x060071C8 RID: 29128 RVA: 0x000AB603 File Offset: 0x000A9803
			public virtual bool PipeliningEnabled
			{
				set
				{
					base.PowerSharpParameters["PipeliningEnabled"] = value;
				}
			}

			// Token: 0x17004BC7 RID: 19399
			// (set) Token: 0x060071C9 RID: 29129 RVA: 0x000AB61B File Offset: 0x000A981B
			public virtual ProtocolLoggingLevel ProtocolLoggingLevel
			{
				set
				{
					base.PowerSharpParameters["ProtocolLoggingLevel"] = value;
				}
			}

			// Token: 0x17004BC8 RID: 19400
			// (set) Token: 0x060071CA RID: 29130 RVA: 0x000AB633 File Offset: 0x000A9833
			public virtual MultiValuedProperty<IPRange> RemoteIPRanges
			{
				set
				{
					base.PowerSharpParameters["RemoteIPRanges"] = value;
				}
			}

			// Token: 0x17004BC9 RID: 19401
			// (set) Token: 0x060071CB RID: 29131 RVA: 0x000AB646 File Offset: 0x000A9846
			public virtual bool RequireEHLODomain
			{
				set
				{
					base.PowerSharpParameters["RequireEHLODomain"] = value;
				}
			}

			// Token: 0x17004BCA RID: 19402
			// (set) Token: 0x060071CC RID: 29132 RVA: 0x000AB65E File Offset: 0x000A985E
			public virtual bool RequireTLS
			{
				set
				{
					base.PowerSharpParameters["RequireTLS"] = value;
				}
			}

			// Token: 0x17004BCB RID: 19403
			// (set) Token: 0x060071CD RID: 29133 RVA: 0x000AB676 File Offset: 0x000A9876
			public virtual bool EnableAuthGSSAPI
			{
				set
				{
					base.PowerSharpParameters["EnableAuthGSSAPI"] = value;
				}
			}

			// Token: 0x17004BCC RID: 19404
			// (set) Token: 0x060071CE RID: 29134 RVA: 0x000AB68E File Offset: 0x000A988E
			public virtual ExtendedProtectionPolicySetting ExtendedProtectionPolicy
			{
				set
				{
					base.PowerSharpParameters["ExtendedProtectionPolicy"] = value;
				}
			}

			// Token: 0x17004BCD RID: 19405
			// (set) Token: 0x060071CF RID: 29135 RVA: 0x000AB6A6 File Offset: 0x000A98A6
			public virtual bool LiveCredentialEnabled
			{
				set
				{
					base.PowerSharpParameters["LiveCredentialEnabled"] = value;
				}
			}

			// Token: 0x17004BCE RID: 19406
			// (set) Token: 0x060071D0 RID: 29136 RVA: 0x000AB6BE File Offset: 0x000A98BE
			public virtual MultiValuedProperty<SmtpReceiveDomainCapabilities> TlsDomainCapabilities
			{
				set
				{
					base.PowerSharpParameters["TlsDomainCapabilities"] = value;
				}
			}

			// Token: 0x17004BCF RID: 19407
			// (set) Token: 0x060071D1 RID: 29137 RVA: 0x000AB6D1 File Offset: 0x000A98D1
			public virtual ServerRole TransportRole
			{
				set
				{
					base.PowerSharpParameters["TransportRole"] = value;
				}
			}

			// Token: 0x17004BD0 RID: 19408
			// (set) Token: 0x060071D2 RID: 29138 RVA: 0x000AB6E9 File Offset: 0x000A98E9
			public virtual SizeMode SizeEnabled
			{
				set
				{
					base.PowerSharpParameters["SizeEnabled"] = value;
				}
			}

			// Token: 0x17004BD1 RID: 19409
			// (set) Token: 0x060071D3 RID: 29139 RVA: 0x000AB701 File Offset: 0x000A9901
			public virtual EnhancedTimeSpan TarpitInterval
			{
				set
				{
					base.PowerSharpParameters["TarpitInterval"] = value;
				}
			}

			// Token: 0x17004BD2 RID: 19410
			// (set) Token: 0x060071D4 RID: 29140 RVA: 0x000AB719 File Offset: 0x000A9919
			public virtual EnhancedTimeSpan MaxAcknowledgementDelay
			{
				set
				{
					base.PowerSharpParameters["MaxAcknowledgementDelay"] = value;
				}
			}

			// Token: 0x17004BD3 RID: 19411
			// (set) Token: 0x060071D5 RID: 29141 RVA: 0x000AB731 File Offset: 0x000A9931
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004BD4 RID: 19412
			// (set) Token: 0x060071D6 RID: 29142 RVA: 0x000AB744 File Offset: 0x000A9944
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004BD5 RID: 19413
			// (set) Token: 0x060071D7 RID: 29143 RVA: 0x000AB75C File Offset: 0x000A995C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004BD6 RID: 19414
			// (set) Token: 0x060071D8 RID: 29144 RVA: 0x000AB774 File Offset: 0x000A9974
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004BD7 RID: 19415
			// (set) Token: 0x060071D9 RID: 29145 RVA: 0x000AB78C File Offset: 0x000A998C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004BD8 RID: 19416
			// (set) Token: 0x060071DA RID: 29146 RVA: 0x000AB7A4 File Offset: 0x000A99A4
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

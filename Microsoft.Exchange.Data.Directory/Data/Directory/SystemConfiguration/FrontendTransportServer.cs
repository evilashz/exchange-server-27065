using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000480 RID: 1152
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class FrontendTransportServer : ADLegacyVersionableObject
	{
		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x000CFD8B File Offset: 0x000CDF8B
		// (set) Token: 0x0600339C RID: 13212 RVA: 0x000CFD9D File Offset: 0x000CDF9D
		public ServerVersion AdminDisplayVersion
		{
			get
			{
				return (ServerVersion)this[FrontendTransportServerSchema.AdminDisplayVersion];
			}
			internal set
			{
				this[FrontendTransportServerSchema.AdminDisplayVersion] = value;
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x000CFDAB File Offset: 0x000CDFAB
		// (set) Token: 0x0600339E RID: 13214 RVA: 0x000CFDBD File Offset: 0x000CDFBD
		public ServerEditionType Edition
		{
			get
			{
				return (ServerEditionType)this[FrontendTransportServerSchema.Edition];
			}
			internal set
			{
				this[FrontendTransportServerSchema.Edition] = value;
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x000CFDD0 File Offset: 0x000CDFD0
		// (set) Token: 0x060033A0 RID: 13216 RVA: 0x000CFDE2 File Offset: 0x000CDFE2
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[FrontendTransportServerSchema.ExchangeLegacyDN];
			}
			internal set
			{
				this[FrontendTransportServerSchema.ExchangeLegacyDN] = value;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x060033A1 RID: 13217 RVA: 0x000CFDF0 File Offset: 0x000CDFF0
		// (set) Token: 0x060033A2 RID: 13218 RVA: 0x000CFE02 File Offset: 0x000CE002
		public bool IsFrontendTransportServer
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.IsFrontendTransportServer];
			}
			internal set
			{
				this[FrontendTransportServerSchema.IsFrontendTransportServer] = value;
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x060033A3 RID: 13219 RVA: 0x000CFE15 File Offset: 0x000CE015
		// (set) Token: 0x060033A4 RID: 13220 RVA: 0x000CFE27 File Offset: 0x000CE027
		public bool IsProvisionedServer
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.IsProvisionedServer];
			}
			internal set
			{
				this[FrontendTransportServerSchema.IsProvisionedServer] = value;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x060033A5 RID: 13221 RVA: 0x000CFE3A File Offset: 0x000CE03A
		// (set) Token: 0x060033A6 RID: 13222 RVA: 0x000CFE4C File Offset: 0x000CE04C
		public NetworkAddressCollection NetworkAddress
		{
			get
			{
				return (NetworkAddressCollection)this[FrontendTransportServerSchema.NetworkAddress];
			}
			internal set
			{
				this[FrontendTransportServerSchema.NetworkAddress] = value;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x000CFE5A File Offset: 0x000CE05A
		// (set) Token: 0x060033A8 RID: 13224 RVA: 0x000CFE6C File Offset: 0x000CE06C
		public int VersionNumber
		{
			get
			{
				return (int)this[FrontendTransportServerSchema.VersionNumber];
			}
			internal set
			{
				this[FrontendTransportServerSchema.VersionNumber] = value;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x000CFE7F File Offset: 0x000CE07F
		// (set) Token: 0x060033AA RID: 13226 RVA: 0x000CFE91 File Offset: 0x000CE091
		public EnhancedTimeSpan AgentLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.AgentLogMaxAge];
			}
			set
			{
				this[FrontendTransportServerSchema.AgentLogMaxAge] = value;
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x060033AB RID: 13227 RVA: 0x000CFEA4 File Offset: 0x000CE0A4
		// (set) Token: 0x060033AC RID: 13228 RVA: 0x000CFEB6 File Offset: 0x000CE0B6
		public Unlimited<ByteQuantifiedSize> AgentLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.AgentLogMaxDirectorySize];
			}
			set
			{
				this[FrontendTransportServerSchema.AgentLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x000CFEC9 File Offset: 0x000CE0C9
		// (set) Token: 0x060033AE RID: 13230 RVA: 0x000CFEDB File Offset: 0x000CE0DB
		public Unlimited<ByteQuantifiedSize> AgentLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.AgentLogMaxFileSize];
			}
			set
			{
				this[FrontendTransportServerSchema.AgentLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x060033AF RID: 13231 RVA: 0x000CFEEE File Offset: 0x000CE0EE
		// (set) Token: 0x060033B0 RID: 13232 RVA: 0x000CFF00 File Offset: 0x000CE100
		public LocalLongFullPath AgentLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.AgentLogPath];
			}
			set
			{
				this[FrontendTransportServerSchema.AgentLogPath] = value;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x060033B1 RID: 13233 RVA: 0x000CFF0E File Offset: 0x000CE10E
		// (set) Token: 0x060033B2 RID: 13234 RVA: 0x000CFF20 File Offset: 0x000CE120
		public bool AgentLogEnabled
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.AgentLogEnabled];
			}
			set
			{
				this[FrontendTransportServerSchema.AgentLogEnabled] = value;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x000CFF33 File Offset: 0x000CE133
		// (set) Token: 0x060033B4 RID: 13236 RVA: 0x000CFF45 File Offset: 0x000CE145
		public EnhancedTimeSpan DnsLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.DnsLogMaxAge];
			}
			set
			{
				this[FrontendTransportServerSchema.DnsLogMaxAge] = value;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x060033B5 RID: 13237 RVA: 0x000CFF58 File Offset: 0x000CE158
		// (set) Token: 0x060033B6 RID: 13238 RVA: 0x000CFF6A File Offset: 0x000CE16A
		public Unlimited<ByteQuantifiedSize> DnsLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.DnsLogMaxDirectorySize];
			}
			set
			{
				this[FrontendTransportServerSchema.DnsLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x060033B7 RID: 13239 RVA: 0x000CFF7D File Offset: 0x000CE17D
		// (set) Token: 0x060033B8 RID: 13240 RVA: 0x000CFF8F File Offset: 0x000CE18F
		public Unlimited<ByteQuantifiedSize> DnsLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.DnsLogMaxFileSize];
			}
			set
			{
				this[FrontendTransportServerSchema.DnsLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x000CFFA2 File Offset: 0x000CE1A2
		// (set) Token: 0x060033BA RID: 13242 RVA: 0x000CFFB4 File Offset: 0x000CE1B4
		public LocalLongFullPath DnsLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.DnsLogPath];
			}
			set
			{
				this[FrontendTransportServerSchema.DnsLogPath] = value;
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x060033BB RID: 13243 RVA: 0x000CFFC2 File Offset: 0x000CE1C2
		// (set) Token: 0x060033BC RID: 13244 RVA: 0x000CFFD4 File Offset: 0x000CE1D4
		public bool DnsLogEnabled
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.DnsLogEnabled];
			}
			set
			{
				this[FrontendTransportServerSchema.DnsLogEnabled] = value;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x000CFFE7 File Offset: 0x000CE1E7
		// (set) Token: 0x060033BE RID: 13246 RVA: 0x000CFFF9 File Offset: 0x000CE1F9
		public EnhancedTimeSpan ResourceLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.ResourceLogMaxAge];
			}
			set
			{
				this[FrontendTransportServerSchema.ResourceLogMaxAge] = value;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x000D000C File Offset: 0x000CE20C
		// (set) Token: 0x060033C0 RID: 13248 RVA: 0x000D001E File Offset: 0x000CE21E
		public Unlimited<ByteQuantifiedSize> ResourceLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.ResourceLogMaxDirectorySize];
			}
			set
			{
				this[FrontendTransportServerSchema.ResourceLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x060033C1 RID: 13249 RVA: 0x000D0031 File Offset: 0x000CE231
		// (set) Token: 0x060033C2 RID: 13250 RVA: 0x000D0043 File Offset: 0x000CE243
		public Unlimited<ByteQuantifiedSize> ResourceLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.ResourceLogMaxFileSize];
			}
			set
			{
				this[FrontendTransportServerSchema.ResourceLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x000D0056 File Offset: 0x000CE256
		// (set) Token: 0x060033C4 RID: 13252 RVA: 0x000D0068 File Offset: 0x000CE268
		public LocalLongFullPath ResourceLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.ResourceLogPath];
			}
			set
			{
				this[FrontendTransportServerSchema.ResourceLogPath] = value;
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x000D0076 File Offset: 0x000CE276
		// (set) Token: 0x060033C6 RID: 13254 RVA: 0x000D0088 File Offset: 0x000CE288
		public bool ResourceLogEnabled
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.ResourceLogEnabled];
			}
			set
			{
				this[FrontendTransportServerSchema.ResourceLogEnabled] = value;
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x000D009B File Offset: 0x000CE29B
		// (set) Token: 0x060033C8 RID: 13256 RVA: 0x000D00AD File Offset: 0x000CE2AD
		[Parameter(Mandatory = false)]
		public bool AntispamAgentsEnabled
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.AntispamAgentsEnabled];
			}
			set
			{
				this[FrontendTransportServerSchema.AntispamAgentsEnabled] = value;
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x060033C9 RID: 13257 RVA: 0x000D00C0 File Offset: 0x000CE2C0
		// (set) Token: 0x060033CA RID: 13258 RVA: 0x000D00D2 File Offset: 0x000CE2D2
		public EnhancedTimeSpan AttributionLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.AttributionLogMaxAge];
			}
			set
			{
				this[FrontendTransportServerSchema.AttributionLogMaxAge] = value;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x060033CB RID: 13259 RVA: 0x000D00E5 File Offset: 0x000CE2E5
		// (set) Token: 0x060033CC RID: 13260 RVA: 0x000D00F7 File Offset: 0x000CE2F7
		public Unlimited<ByteQuantifiedSize> AttributionLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.AttributionLogMaxDirectorySize];
			}
			set
			{
				this[FrontendTransportServerSchema.AttributionLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x000D010A File Offset: 0x000CE30A
		// (set) Token: 0x060033CE RID: 13262 RVA: 0x000D011C File Offset: 0x000CE31C
		public Unlimited<ByteQuantifiedSize> AttributionLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.AttributionLogMaxFileSize];
			}
			set
			{
				this[FrontendTransportServerSchema.AttributionLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x060033CF RID: 13263 RVA: 0x000D012F File Offset: 0x000CE32F
		// (set) Token: 0x060033D0 RID: 13264 RVA: 0x000D0141 File Offset: 0x000CE341
		public LocalLongFullPath AttributionLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.AttributionLogPath];
			}
			set
			{
				this[FrontendTransportServerSchema.AttributionLogPath] = value;
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x060033D1 RID: 13265 RVA: 0x000D014F File Offset: 0x000CE34F
		// (set) Token: 0x060033D2 RID: 13266 RVA: 0x000D0161 File Offset: 0x000CE361
		public bool AttributionLogEnabled
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.AttributionLogEnabled];
			}
			set
			{
				this[FrontendTransportServerSchema.AttributionLogEnabled] = value;
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x060033D3 RID: 13267 RVA: 0x000D0174 File Offset: 0x000CE374
		// (set) Token: 0x060033D4 RID: 13268 RVA: 0x000D0186 File Offset: 0x000CE386
		[Parameter(Mandatory = false)]
		public bool ConnectivityLogEnabled
		{
			get
			{
				return (bool)this[FrontendTransportServerSchema.ConnectivityLogEnabled];
			}
			set
			{
				this[FrontendTransportServerSchema.ConnectivityLogEnabled] = value;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x000D0199 File Offset: 0x000CE399
		// (set) Token: 0x060033D6 RID: 13270 RVA: 0x000D01AB File Offset: 0x000CE3AB
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConnectivityLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.ConnectivityLogMaxAge];
			}
			set
			{
				this[FrontendTransportServerSchema.ConnectivityLogMaxAge] = value;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x060033D7 RID: 13271 RVA: 0x000D01BE File Offset: 0x000CE3BE
		// (set) Token: 0x060033D8 RID: 13272 RVA: 0x000D01D0 File Offset: 0x000CE3D0
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ConnectivityLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.ConnectivityLogMaxDirectorySize];
			}
			set
			{
				this[FrontendTransportServerSchema.ConnectivityLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x060033D9 RID: 13273 RVA: 0x000D01E3 File Offset: 0x000CE3E3
		// (set) Token: 0x060033DA RID: 13274 RVA: 0x000D01F5 File Offset: 0x000CE3F5
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ConnectivityLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.ConnectivityLogMaxFileSize];
			}
			set
			{
				this[FrontendTransportServerSchema.ConnectivityLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x000D0208 File Offset: 0x000CE408
		// (set) Token: 0x060033DC RID: 13276 RVA: 0x000D021A File Offset: 0x000CE41A
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ConnectivityLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.ConnectivityLogPath];
			}
			set
			{
				this[FrontendTransportServerSchema.ConnectivityLogPath] = value;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x060033DD RID: 13277 RVA: 0x000D0228 File Offset: 0x000CE428
		// (set) Token: 0x060033DE RID: 13278 RVA: 0x000D023A File Offset: 0x000CE43A
		public int MaxReceiveTlsRatePerMinute
		{
			get
			{
				return (int)this[FrontendTransportServerSchema.MaxReceiveTlsRatePerMinute];
			}
			set
			{
				this[FrontendTransportServerSchema.MaxReceiveTlsRatePerMinute] = value;
			}
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000D024D File Offset: 0x000CE44D
		// (set) Token: 0x060033E0 RID: 13280 RVA: 0x000D025F File Offset: 0x000CE45F
		public ServerRole CurrentServerRole
		{
			get
			{
				return (ServerRole)this[FrontendTransportServerSchema.CurrentServerRole];
			}
			internal set
			{
				this[FrontendTransportServerSchema.CurrentServerRole] = value;
			}
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000D0272 File Offset: 0x000CE472
		// (set) Token: 0x060033E2 RID: 13282 RVA: 0x000D0287 File Offset: 0x000CE487
		[Parameter(Mandatory = false)]
		public bool ExternalDNSAdapterEnabled
		{
			get
			{
				return !(bool)this[FrontendTransportServerSchema.ExternalDNSAdapterDisabled];
			}
			set
			{
				this[FrontendTransportServerSchema.ExternalDNSAdapterDisabled] = !value;
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000D029D File Offset: 0x000CE49D
		// (set) Token: 0x060033E4 RID: 13284 RVA: 0x000D02AF File Offset: 0x000CE4AF
		[Parameter(Mandatory = false)]
		public Guid ExternalDNSAdapterGuid
		{
			get
			{
				return (Guid)this[FrontendTransportServerSchema.ExternalDNSAdapterGuid];
			}
			set
			{
				this[FrontendTransportServerSchema.ExternalDNSAdapterGuid] = value;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x000D02C2 File Offset: 0x000CE4C2
		// (set) Token: 0x060033E6 RID: 13286 RVA: 0x000D02D4 File Offset: 0x000CE4D4
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPAddress> ExternalDNSServers
		{
			get
			{
				return (MultiValuedProperty<IPAddress>)this[FrontendTransportServerSchema.ExternalDNSServers];
			}
			set
			{
				this[FrontendTransportServerSchema.ExternalDNSServers] = value;
			}
		}

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x000D02E2 File Offset: 0x000CE4E2
		// (set) Token: 0x060033E8 RID: 13288 RVA: 0x000D02F4 File Offset: 0x000CE4F4
		[Parameter(Mandatory = false)]
		public IPAddress ExternalIPAddress
		{
			get
			{
				return (IPAddress)this[FrontendTransportServerSchema.ExternalIPAddress];
			}
			set
			{
				this[FrontendTransportServerSchema.ExternalIPAddress] = value;
			}
		}

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x060033E9 RID: 13289 RVA: 0x000D0302 File Offset: 0x000CE502
		// (set) Token: 0x060033EA RID: 13290 RVA: 0x000D0314 File Offset: 0x000CE514
		[Parameter(Mandatory = false)]
		public ProtocolOption ExternalDNSProtocolOption
		{
			get
			{
				return (ProtocolOption)this[FrontendTransportServerSchema.ExternalDNSProtocolOption];
			}
			set
			{
				this[FrontendTransportServerSchema.ExternalDNSProtocolOption] = value;
			}
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x060033EB RID: 13291 RVA: 0x000D0327 File Offset: 0x000CE527
		// (set) Token: 0x060033EC RID: 13292 RVA: 0x000D033C File Offset: 0x000CE53C
		[Parameter(Mandatory = false)]
		public bool InternalDNSAdapterEnabled
		{
			get
			{
				return !(bool)this[FrontendTransportServerSchema.InternalDNSAdapterDisabled];
			}
			set
			{
				this[FrontendTransportServerSchema.InternalDNSAdapterDisabled] = !value;
			}
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x000D0352 File Offset: 0x000CE552
		// (set) Token: 0x060033EE RID: 13294 RVA: 0x000D0364 File Offset: 0x000CE564
		[Parameter(Mandatory = false)]
		public Guid InternalDNSAdapterGuid
		{
			get
			{
				return (Guid)this[FrontendTransportServerSchema.InternalDNSAdapterGuid];
			}
			set
			{
				this[FrontendTransportServerSchema.InternalDNSAdapterGuid] = value;
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x000D0377 File Offset: 0x000CE577
		// (set) Token: 0x060033F0 RID: 13296 RVA: 0x000D0389 File Offset: 0x000CE589
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPAddress> InternalDNSServers
		{
			get
			{
				return (MultiValuedProperty<IPAddress>)this[FrontendTransportServerSchema.InternalDNSServers];
			}
			set
			{
				this[FrontendTransportServerSchema.InternalDNSServers] = value;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x000D0397 File Offset: 0x000CE597
		// (set) Token: 0x060033F2 RID: 13298 RVA: 0x000D03A9 File Offset: 0x000CE5A9
		[Parameter(Mandatory = false)]
		public ProtocolOption InternalDNSProtocolOption
		{
			get
			{
				return (ProtocolOption)this[FrontendTransportServerSchema.InternalDNSProtocolOption];
			}
			set
			{
				this[FrontendTransportServerSchema.InternalDNSProtocolOption] = value;
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x060033F3 RID: 13299 RVA: 0x000D03BC File Offset: 0x000CE5BC
		// (set) Token: 0x060033F4 RID: 13300 RVA: 0x000D03CE File Offset: 0x000CE5CE
		[Parameter(Mandatory = false)]
		public ProtocolLoggingLevel IntraOrgConnectorProtocolLoggingLevel
		{
			get
			{
				return (ProtocolLoggingLevel)this[FrontendTransportServerSchema.IntraOrgConnectorProtocolLoggingLevel];
			}
			set
			{
				this[FrontendTransportServerSchema.IntraOrgConnectorProtocolLoggingLevel] = value;
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x000D03E1 File Offset: 0x000CE5E1
		// (set) Token: 0x060033F6 RID: 13302 RVA: 0x000D03F3 File Offset: 0x000CE5F3
		public int IntraOrgConnectorSmtpMaxMessagesPerConnection
		{
			get
			{
				return (int)this[FrontendTransportServerSchema.IntraOrgConnectorSmtpMaxMessagesPerConnection];
			}
			internal set
			{
				this[FrontendTransportServerSchema.IntraOrgConnectorSmtpMaxMessagesPerConnection] = value;
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x060033F7 RID: 13303 RVA: 0x000D0406 File Offset: 0x000CE606
		// (set) Token: 0x060033F8 RID: 13304 RVA: 0x000D0418 File Offset: 0x000CE618
		[Parameter(Mandatory = false)]
		public int MaxConnectionRatePerMinute
		{
			get
			{
				return (int)this[FrontendTransportServerSchema.MaxConnectionRatePerMinute];
			}
			set
			{
				this[FrontendTransportServerSchema.MaxConnectionRatePerMinute] = value;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x060033F9 RID: 13305 RVA: 0x000D042B File Offset: 0x000CE62B
		// (set) Token: 0x060033FA RID: 13306 RVA: 0x000D043D File Offset: 0x000CE63D
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxOutboundConnections
		{
			get
			{
				return (Unlimited<int>)this[FrontendTransportServerSchema.MaxOutboundConnections];
			}
			set
			{
				this[FrontendTransportServerSchema.MaxOutboundConnections] = value;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x060033FB RID: 13307 RVA: 0x000D0450 File Offset: 0x000CE650
		// (set) Token: 0x060033FC RID: 13308 RVA: 0x000D0462 File Offset: 0x000CE662
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxPerDomainOutboundConnections
		{
			get
			{
				return (Unlimited<int>)this[FrontendTransportServerSchema.MaxPerDomainOutboundConnections];
			}
			set
			{
				this[FrontendTransportServerSchema.MaxPerDomainOutboundConnections] = value;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x060033FD RID: 13309 RVA: 0x000D0475 File Offset: 0x000CE675
		// (set) Token: 0x060033FE RID: 13310 RVA: 0x000D0487 File Offset: 0x000CE687
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ReceiveProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.ReceiveProtocolLogMaxAge];
			}
			set
			{
				this[FrontendTransportServerSchema.ReceiveProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x060033FF RID: 13311 RVA: 0x000D049A File Offset: 0x000CE69A
		// (set) Token: 0x06003400 RID: 13312 RVA: 0x000D04AC File Offset: 0x000CE6AC
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.ReceiveProtocolLogMaxDirectorySize];
			}
			set
			{
				this[FrontendTransportServerSchema.ReceiveProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x000D04BF File Offset: 0x000CE6BF
		// (set) Token: 0x06003402 RID: 13314 RVA: 0x000D04D1 File Offset: 0x000CE6D1
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ReceiveProtocolLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.ReceiveProtocolLogMaxFileSize];
			}
			set
			{
				this[FrontendTransportServerSchema.ReceiveProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x000D04E4 File Offset: 0x000CE6E4
		// (set) Token: 0x06003404 RID: 13316 RVA: 0x000D04F6 File Offset: 0x000CE6F6
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ReceiveProtocolLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.ReceiveProtocolLogPath];
			}
			set
			{
				this[FrontendTransportServerSchema.ReceiveProtocolLogPath] = value;
			}
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000D0504 File Offset: 0x000CE704
		// (set) Token: 0x06003406 RID: 13318 RVA: 0x000D0516 File Offset: 0x000CE716
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan SendProtocolLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.SendProtocolLogMaxAge];
			}
			set
			{
				this[FrontendTransportServerSchema.SendProtocolLogMaxAge] = value;
			}
		}

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06003407 RID: 13319 RVA: 0x000D0529 File Offset: 0x000CE729
		// (set) Token: 0x06003408 RID: 13320 RVA: 0x000D053B File Offset: 0x000CE73B
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> SendProtocolLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.SendProtocolLogMaxDirectorySize];
			}
			set
			{
				this[FrontendTransportServerSchema.SendProtocolLogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06003409 RID: 13321 RVA: 0x000D054E File Offset: 0x000CE74E
		// (set) Token: 0x0600340A RID: 13322 RVA: 0x000D0560 File Offset: 0x000CE760
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> SendProtocolLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[FrontendTransportServerSchema.SendProtocolLogMaxFileSize];
			}
			set
			{
				this[FrontendTransportServerSchema.SendProtocolLogMaxFileSize] = value;
			}
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x0600340B RID: 13323 RVA: 0x000D0573 File Offset: 0x000CE773
		// (set) Token: 0x0600340C RID: 13324 RVA: 0x000D0585 File Offset: 0x000CE785
		[Parameter(Mandatory = false)]
		public LocalLongFullPath SendProtocolLogPath
		{
			get
			{
				return (LocalLongFullPath)this[FrontendTransportServerSchema.SendProtocolLogPath];
			}
			set
			{
				this[FrontendTransportServerSchema.SendProtocolLogPath] = value;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x000D0593 File Offset: 0x000CE793
		// (set) Token: 0x0600340E RID: 13326 RVA: 0x000D05A5 File Offset: 0x000CE7A5
		[Parameter(Mandatory = false)]
		public int TransientFailureRetryCount
		{
			get
			{
				return (int)this[FrontendTransportServerSchema.TransientFailureRetryCount];
			}
			set
			{
				this[FrontendTransportServerSchema.TransientFailureRetryCount] = value;
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x000D05B8 File Offset: 0x000CE7B8
		// (set) Token: 0x06003410 RID: 13328 RVA: 0x000D05CA File Offset: 0x000CE7CA
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransientFailureRetryInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[FrontendTransportServerSchema.TransientFailureRetryInterval];
			}
			set
			{
				this[FrontendTransportServerSchema.TransientFailureRetryInterval] = value;
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x000D05DD File Offset: 0x000CE7DD
		internal override ADObjectSchema Schema
		{
			get
			{
				if (this.schema == null)
				{
					this.schema = ObjectSchema.GetInstance<FrontendTransportServerADSchema>();
				}
				return this.schema;
			}
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000D05F8 File Offset: 0x000CE7F8
		internal override string MostDerivedObjectClass
		{
			get
			{
				return FrontendTransportServer.mostDerivedClass;
			}
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x000D05FF File Offset: 0x000CE7FF
		internal Container GetParentContainer()
		{
			return base.Session.Read<Container>(base.Id.Parent);
		}

		// Token: 0x040023C3 RID: 9155
		internal const string FrontendTransportServerADObjectName = "Frontend";

		// Token: 0x040023C4 RID: 9156
		private static string mostDerivedClass = "msExchExchangeTransportServer";

		// Token: 0x040023C5 RID: 9157
		[NonSerialized]
		private ADObjectSchema schema;
	}
}

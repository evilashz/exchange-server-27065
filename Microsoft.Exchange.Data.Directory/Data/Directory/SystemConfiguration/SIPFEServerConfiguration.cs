using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005A3 RID: 1443
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public sealed class SIPFEServerConfiguration : ADConfigurationObject
	{
		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x060042D5 RID: 17109 RVA: 0x000FBEF2 File Offset: 0x000FA0F2
		public new string Name
		{
			get
			{
				return "UMCallRouterSettings";
			}
		}

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x060042D6 RID: 17110 RVA: 0x000FBEF9 File Offset: 0x000FA0F9
		internal override ADObjectSchema Schema
		{
			get
			{
				return SIPFEServerConfiguration.schema;
			}
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x060042D7 RID: 17111 RVA: 0x000FBF00 File Offset: 0x000FA100
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchProtocolCfgSIPFEServer";
			}
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x060042D8 RID: 17112 RVA: 0x000FBF07 File Offset: 0x000FA107
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x060042D9 RID: 17113 RVA: 0x000FBF0E File Offset: 0x000FA10E
		internal override ADObjectId ParentPath
		{
			get
			{
				return SIPFEServerConfiguration.parentPath;
			}
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x060042DA RID: 17114 RVA: 0x000FBF15 File Offset: 0x000FA115
		// (set) Token: 0x060042DB RID: 17115 RVA: 0x000FBF27 File Offset: 0x000FA127
		[Parameter(Mandatory = false)]
		public int? MaxCallsAllowed
		{
			get
			{
				return (int?)this[SIPFEServerConfigurationSchema.MaxCallsAllowed];
			}
			set
			{
				this[SIPFEServerConfigurationSchema.MaxCallsAllowed] = value;
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x060042DC RID: 17116 RVA: 0x000FBF3A File Offset: 0x000FA13A
		// (set) Token: 0x060042DD RID: 17117 RVA: 0x000FBF4C File Offset: 0x000FA14C
		[Parameter(Mandatory = false)]
		public int SipTcpListeningPort
		{
			get
			{
				return (int)this[SIPFEServerConfigurationSchema.SipTcpListeningPort];
			}
			set
			{
				this[SIPFEServerConfigurationSchema.SipTcpListeningPort] = value;
			}
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x000FBF5F File Offset: 0x000FA15F
		// (set) Token: 0x060042DF RID: 17119 RVA: 0x000FBF71 File Offset: 0x000FA171
		[Parameter(Mandatory = false)]
		public int SipTlsListeningPort
		{
			get
			{
				return (int)this[SIPFEServerConfigurationSchema.SipTlsListeningPort];
			}
			set
			{
				this[SIPFEServerConfigurationSchema.SipTlsListeningPort] = value;
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x060042E0 RID: 17120 RVA: 0x000FBF84 File Offset: 0x000FA184
		// (set) Token: 0x060042E1 RID: 17121 RVA: 0x000FBF96 File Offset: 0x000FA196
		[Parameter(Mandatory = false)]
		public UMSmartHost ExternalHostFqdn
		{
			get
			{
				return (UMSmartHost)this[SIPFEServerConfigurationSchema.ExternalHostFqdn];
			}
			set
			{
				this[SIPFEServerConfigurationSchema.ExternalHostFqdn] = value;
			}
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x060042E2 RID: 17122 RVA: 0x000FBFA4 File Offset: 0x000FA1A4
		// (set) Token: 0x060042E3 RID: 17123 RVA: 0x000FBFB6 File Offset: 0x000FA1B6
		[Parameter(Mandatory = false)]
		public UMSmartHost ExternalServiceFqdn
		{
			get
			{
				return (UMSmartHost)this[SIPFEServerConfigurationSchema.ExternalServiceFqdn];
			}
			set
			{
				this[SIPFEServerConfigurationSchema.ExternalServiceFqdn] = value;
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x060042E4 RID: 17124 RVA: 0x000FBFC4 File Offset: 0x000FA1C4
		// (set) Token: 0x060042E5 RID: 17125 RVA: 0x000FBFD6 File Offset: 0x000FA1D6
		[Parameter(Mandatory = false)]
		public string UMPodRedirectTemplate
		{
			get
			{
				return (string)this[SIPFEServerConfigurationSchema.UMPodRedirectTemplate];
			}
			set
			{
				this[SIPFEServerConfigurationSchema.UMPodRedirectTemplate] = value;
			}
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x000FBFE4 File Offset: 0x000FA1E4
		// (set) Token: 0x060042E7 RID: 17127 RVA: 0x000FBFF6 File Offset: 0x000FA1F6
		[Parameter(Mandatory = false)]
		public string UMForwardingAddressTemplate
		{
			get
			{
				return (string)this[SIPFEServerConfigurationSchema.UMForwardingAddressTemplate];
			}
			set
			{
				this[SIPFEServerConfigurationSchema.UMForwardingAddressTemplate] = value;
			}
		}

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x060042E8 RID: 17128 RVA: 0x000FC004 File Offset: 0x000FA204
		// (set) Token: 0x060042E9 RID: 17129 RVA: 0x000FC016 File Offset: 0x000FA216
		[Parameter(Mandatory = false)]
		public UMStartupMode UMStartupMode
		{
			get
			{
				return (UMStartupMode)this[SIPFEServerConfigurationSchema.UMStartupMode];
			}
			set
			{
				this[SIPFEServerConfigurationSchema.UMStartupMode] = value;
			}
		}

		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x060042EA RID: 17130 RVA: 0x000FC029 File Offset: 0x000FA229
		// (set) Token: 0x060042EB RID: 17131 RVA: 0x000FC03B File Offset: 0x000FA23B
		public MultiValuedProperty<ADObjectId> DialPlans
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[SIPFEServerConfigurationSchema.DialPlans];
			}
			set
			{
				this[SIPFEServerConfigurationSchema.DialPlans] = value;
			}
		}

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x060042EC RID: 17132 RVA: 0x000FC049 File Offset: 0x000FA249
		// (set) Token: 0x060042ED RID: 17133 RVA: 0x000FC05B File Offset: 0x000FA25B
		public string UMCertificateThumbprint
		{
			get
			{
				return (string)this[SIPFEServerConfigurationSchema.UMCertificateThumbprint];
			}
			internal set
			{
				this[SIPFEServerConfigurationSchema.UMCertificateThumbprint] = value;
			}
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x060042EE RID: 17134 RVA: 0x000FC069 File Offset: 0x000FA269
		// (set) Token: 0x060042EF RID: 17135 RVA: 0x000FC07B File Offset: 0x000FA27B
		public NetworkAddressCollection NetworkAddress
		{
			get
			{
				return (NetworkAddressCollection)this[SIPFEServerConfigurationSchema.NetworkAddress];
			}
			internal set
			{
				this[SIPFEServerConfigurationSchema.NetworkAddress] = value;
			}
		}

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x060042F0 RID: 17136 RVA: 0x000FC089 File Offset: 0x000FA289
		// (set) Token: 0x060042F1 RID: 17137 RVA: 0x000FC09B File Offset: 0x000FA29B
		public int VersionNumber
		{
			get
			{
				return (int)this[SIPFEServerConfigurationSchema.VersionNumber];
			}
			internal set
			{
				this[SIPFEServerConfigurationSchema.VersionNumber] = value;
			}
		}

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x060042F2 RID: 17138 RVA: 0x000FC0AE File Offset: 0x000FA2AE
		// (set) Token: 0x060042F3 RID: 17139 RVA: 0x000FC0C0 File Offset: 0x000FA2C0
		public ServerRole CurrentServerRole
		{
			get
			{
				return (ServerRole)this[SIPFEServerConfigurationSchema.CurrentServerRole];
			}
			internal set
			{
				this[SIPFEServerConfigurationSchema.CurrentServerRole] = value;
			}
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x060042F4 RID: 17140 RVA: 0x000FC0D3 File Offset: 0x000FA2D3
		// (set) Token: 0x060042F5 RID: 17141 RVA: 0x000FC0E5 File Offset: 0x000FA2E5
		[Parameter(Mandatory = false)]
		public bool IPAddressFamilyConfigurable
		{
			get
			{
				return (bool)this[SIPFEServerConfigurationSchema.IPAddressFamilyConfigurable];
			}
			set
			{
				this[SIPFEServerConfigurationSchema.IPAddressFamilyConfigurable] = value;
			}
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x060042F6 RID: 17142 RVA: 0x000FC0F8 File Offset: 0x000FA2F8
		// (set) Token: 0x060042F7 RID: 17143 RVA: 0x000FC10A File Offset: 0x000FA30A
		[Parameter(Mandatory = false)]
		public IPAddressFamily IPAddressFamily
		{
			get
			{
				return (IPAddressFamily)this[SIPFEServerConfigurationSchema.IPAddressFamily];
			}
			set
			{
				this[SIPFEServerConfigurationSchema.IPAddressFamily] = value;
			}
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x000FC11D File Offset: 0x000FA31D
		internal static ObjectId GetRootId(Server server)
		{
			return server.Id.GetChildId("Protocols").GetChildId("SIP");
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x000FC13C File Offset: 0x000FA33C
		internal static SIPFEServerConfiguration Find()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 563, "Find", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\SIPFEServerConfiguration.cs");
			return SIPFEServerConfiguration.Find(LocalServer.GetServer(), tenantOrTopologyConfigurationSession);
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x000FC174 File Offset: 0x000FA374
		internal static SIPFEServerConfiguration Find(Server server, IConfigurationSession adSession)
		{
			if (server == null || adSession == null)
			{
				return null;
			}
			ObjectId rootId = SIPFEServerConfiguration.GetRootId(server);
			if (rootId == null)
			{
				return null;
			}
			SIPFEServerConfiguration[] array = adSession.Find<SIPFEServerConfiguration>(rootId as ADObjectId, QueryScope.OneLevel, null, null, 2);
			if (array == null || array.Length <= 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x04002D7C RID: 11644
		private const string MostDerivedClass = "msExchProtocolCfgSIPFEServer";

		// Token: 0x04002D7D RID: 11645
		private const string Protocols = "Protocols";

		// Token: 0x04002D7E RID: 11646
		public const string ProtocolName = "SIP";

		// Token: 0x04002D7F RID: 11647
		private static readonly SIPFEServerConfigurationSchema schema = ObjectSchema.GetInstance<SIPFEServerConfigurationSchema>();

		// Token: 0x04002D80 RID: 11648
		private static readonly ADObjectId parentPath = new ADObjectId("CN=SIP");
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003B7 RID: 951
	[Serializable]
	public class ClientAccessArray : ADConfigurationObject
	{
		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x000B4153 File Offset: 0x000B2353
		public bool IsPriorTo15ExchangeObjectVersion
		{
			get
			{
				return base.ExchangeVersion.IsOlderThan(ClientAccessArray.MinimumSupportedExchangeObjectVersion);
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06002B65 RID: 11109 RVA: 0x000B4165 File Offset: 0x000B2365
		internal static ExchangeObjectVersion MinimumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000B416C File Offset: 0x000B236C
		internal static QueryFilter PriorTo15ExchangeObjectVersionFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.LessThan, ADObjectSchema.ExchangeVersion, ClientAccessArray.MinimumSupportedExchangeObjectVersion);
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x000B417E File Offset: 0x000B237E
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012.NextMajorVersion;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x000B418C File Offset: 0x000B238C
		internal override QueryFilter VersioningFilter
		{
			get
			{
				ExchangeObjectVersion exchange = ExchangeObjectVersion.Exchange2007;
				ExchangeObjectVersion nextMajorVersion = this.MaximumSupportedExchangeObjectVersion.NextMajorVersion;
				return new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.ExchangeVersion, exchange),
					new ComparisonFilter(ComparisonOperator.LessThan, ADObjectSchema.ExchangeVersion, nextMajorVersion)
				});
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000B41D6 File Offset: 0x000B23D6
		internal override ADObjectSchema Schema
		{
			get
			{
				return ClientAccessArray.schema;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000B41DD File Offset: 0x000B23DD
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchClientAccessArray";
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06002B6B RID: 11115 RVA: 0x000B41E4 File Offset: 0x000B23E4
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return QueryFilter.OrTogether(new QueryFilter[]
				{
					base.ImplicitFilter,
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "msExchClientAccessArrayLegacy")
				});
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000B421A File Offset: 0x000B241A
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this.propertyBag[ClientAccessArraySchema.ExchangeLegacyDN];
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x000B4231 File Offset: 0x000B2431
		public string Fqdn
		{
			get
			{
				return (string)this.propertyBag[ClientAccessArraySchema.Fqdn];
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x000B4248 File Offset: 0x000B2448
		// (set) Token: 0x06002B6F RID: 11119 RVA: 0x000B425F File Offset: 0x000B245F
		public string ArrayDefinition
		{
			get
			{
				return (string)this.propertyBag[ClientAccessArraySchema.ArrayDefinition];
			}
			set
			{
				this.propertyBag[ClientAccessArraySchema.ArrayDefinition] = value;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x000B4272 File Offset: 0x000B2472
		// (set) Token: 0x06002B71 RID: 11121 RVA: 0x000B4289 File Offset: 0x000B2489
		public ADObjectId Site
		{
			get
			{
				return (ADObjectId)this.propertyBag[ClientAccessArraySchema.Site];
			}
			set
			{
				this.propertyBag[ClientAccessArraySchema.Site] = value;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x000B429C File Offset: 0x000B249C
		public string SiteName
		{
			get
			{
				if (this.Site != null)
				{
					return this.Site.Name;
				}
				return null;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06002B73 RID: 11123 RVA: 0x000B42B3 File Offset: 0x000B24B3
		public MultiValuedProperty<ADObjectId> Servers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ClientAccessArraySchema.Servers];
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x000B42C5 File Offset: 0x000B24C5
		// (set) Token: 0x06002B75 RID: 11125 RVA: 0x000B42E1 File Offset: 0x000B24E1
		public int ServerCount
		{
			get
			{
				return (int)(this[ClientAccessArraySchema.ServerCount] ?? 0);
			}
			set
			{
				this[ClientAccessArraySchema.ServerCount] = value;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x000B42F4 File Offset: 0x000B24F4
		// (set) Token: 0x06002B77 RID: 11127 RVA: 0x000B42FC File Offset: 0x000B24FC
		public ADObjectId[] Members { get; private set; }

		// Token: 0x06002B78 RID: 11128 RVA: 0x000B4308 File Offset: 0x000B2508
		internal static void FqdnSetter(object value, IPropertyBag propertyBag)
		{
			NetworkAddressCollection networkAddressCollection = ((NetworkAddressCollection)propertyBag[ServerSchema.NetworkAddress]) ?? new NetworkAddressCollection();
			NetworkAddress networkAddress = networkAddressCollection[NetworkProtocol.TcpIP];
			if (networkAddress != null)
			{
				networkAddressCollection.Remove(networkAddress);
			}
			networkAddressCollection.Add(NetworkProtocol.TcpIP.GetNetworkAddress((string)value));
			propertyBag[ServerSchema.NetworkAddress] = networkAddressCollection;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000B436E File Offset: 0x000B256E
		internal static ADObjectId GetParentContainer(ITopologyConfigurationSession adSession)
		{
			return adSession.GetAdministrativeGroupId().GetChildId("Arrays");
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000B43C8 File Offset: 0x000B25C8
		internal static IEnumerable<KeyValuePair<Server, ExchangeRpcClientAccess>> GetMembers(IEnumerable<Server> cachedServers, IEnumerable<ExchangeRpcClientAccess> cachedRpcClientAccess, ADObjectId siteId)
		{
			if (cachedServers == null)
			{
				throw new ArgumentNullException("cachedServers");
			}
			if (cachedRpcClientAccess == null)
			{
				throw new ArgumentNullException("cachedRpcClientAccess");
			}
			return from pair in ExchangeRpcClientAccess.GetServersWithRpcClientAccessEnabled(from server in cachedServers
			where siteId != null && siteId.Equals(server.ServerSite)
			select server, cachedRpcClientAccess)
			where !pair.Key.IsE15OrLater && (pair.Value.Responsibility & RpcClientAccessResponsibility.Mailboxes) == RpcClientAccessResponsibility.Mailboxes
			select pair;
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000B4438 File Offset: 0x000B2638
		internal static object IsClientAccessArrayGetter(IPropertyBag propertyBag)
		{
			foreach (string a in ((MultiValuedProperty<string>)propertyBag[ADObjectSchema.ObjectClass]))
			{
				if (a == "msExchClientAccessArray" || a == "msExchClientAccessArrayLegacy")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000B44CA File Offset: 0x000B26CA
		internal void CompleteAllCalculatedProperties(IEnumerable<Server> cachedServers, IEnumerable<ExchangeRpcClientAccess> cachedRpcClientAccess)
		{
			this.Members = (from serverToRca in ClientAccessArray.GetMembers(cachedServers, cachedRpcClientAccess, this.Site)
			select serverToRca.Key.Id).ToArray<ADObjectId>();
		}

		// Token: 0x04001A0D RID: 6669
		private const string MostDerivedClassInternal = "msExchClientAccessArray";

		// Token: 0x04001A0E RID: 6670
		private const string MostDerivedClassLegacyInternal = "msExchClientAccessArrayLegacy";

		// Token: 0x04001A0F RID: 6671
		private static readonly ClientAccessArraySchema schema = ObjectSchema.GetInstance<ClientAccessArraySchema>();
	}
}

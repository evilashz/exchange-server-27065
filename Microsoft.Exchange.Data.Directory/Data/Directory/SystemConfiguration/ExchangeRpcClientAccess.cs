using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000424 RID: 1060
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class ExchangeRpcClientAccess : ADConfigurationObject
	{
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x000C0BDB File Offset: 0x000BEDDB
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExchangeRpcClientAccess.schema;
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000C0BE2 File Offset: 0x000BEDE2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchProtocolCfgExchangeRpcService";
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x000C0BE9 File Offset: 0x000BEDE9
		public ADObjectId Server
		{
			get
			{
				return (ADObjectId)this[ExchangeRpcClientAccessSchema.Server];
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x000C0BFB File Offset: 0x000BEDFB
		// (set) Token: 0x06002FB1 RID: 12209 RVA: 0x000C0C0D File Offset: 0x000BEE0D
		[Parameter(Mandatory = false)]
		public int MaximumConnections
		{
			get
			{
				return (int)this[ExchangeRpcClientAccessSchema.MaximumConnections];
			}
			set
			{
				this[ExchangeRpcClientAccessSchema.MaximumConnections] = value;
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x000C0C20 File Offset: 0x000BEE20
		// (set) Token: 0x06002FB3 RID: 12211 RVA: 0x000C0C32 File Offset: 0x000BEE32
		[Parameter(Mandatory = false)]
		public bool EncryptionRequired
		{
			get
			{
				return (bool)this[ExchangeRpcClientAccessSchema.IsEncryptionRequired];
			}
			set
			{
				this[ExchangeRpcClientAccessSchema.IsEncryptionRequired] = value;
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x000C0C45 File Offset: 0x000BEE45
		// (set) Token: 0x06002FB5 RID: 12213 RVA: 0x000C0C57 File Offset: 0x000BEE57
		[Parameter(Mandatory = false)]
		public string BlockedClientVersions
		{
			get
			{
				return (string)this[ExchangeRpcClientAccessSchema.BlockedClientVersions];
			}
			set
			{
				this[ExchangeRpcClientAccessSchema.BlockedClientVersions] = value;
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x000C0C65 File Offset: 0x000BEE65
		// (set) Token: 0x06002FB7 RID: 12215 RVA: 0x000C0C6D File Offset: 0x000BEE6D
		public RpcClientAccessResponsibility Responsibility { get; private set; }

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000C0C76 File Offset: 0x000BEE76
		public override string ToString()
		{
			if (this.Server == null)
			{
				return base.ToString();
			}
			return this.Server.ToString();
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000C0C92 File Offset: 0x000BEE92
		internal static bool CanCreateUnder(Server server)
		{
			return server.IsClientAccessServer || server.IsMailboxServer;
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000C0CA4 File Offset: 0x000BEEA4
		internal static LegacyDN CreateSelfRedirectLegacyDN(LegacyDN legacyDN, Guid mailboxGuid)
		{
			string rdnPrefix;
			string str;
			LegacyDN parentLegacyDN = legacyDN.GetParentLegacyDN(out rdnPrefix, out str);
			return parentLegacyDN.GetChildLegacyDN("cn", ExchangeRpcClientAccess.selfRedirectLegacyDNSectionPrefix + mailboxGuid.ToString()).GetChildLegacyDN(rdnPrefix, ExchangeRpcClientAccess.selfRedirectLegacyDNServerPrefix + str);
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000C0CF0 File Offset: 0x000BEEF0
		internal static string CreatePersonalizedServer(Guid mailboxGuid, string domain)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}@{1}", new object[]
			{
				mailboxGuid.ToString("D"),
				domain
			}).ToLowerInvariant();
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000C0D2C File Offset: 0x000BEF2C
		internal static LegacyDN CreatePersonalizedServerRedirectLegacyDN(LegacyDN legacyDN, Guid mailboxGuid, string domain)
		{
			LegacyDN parentLegacyDN = legacyDN.GetParentLegacyDN();
			return parentLegacyDN.GetChildLegacyDN("cn", ExchangeRpcClientAccess.CreatePersonalizedServer(mailboxGuid, domain));
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000C0D54 File Offset: 0x000BEF54
		internal static string FixFakeRedirectLegacyDNIfNeeded(string legacyDN)
		{
			LegacyDN legacyDN2;
			if (!LegacyDN.TryParse(legacyDN, out legacyDN2))
			{
				return legacyDN;
			}
			return ExchangeRpcClientAccess.FixFakeRedirectLegacyDNIfNeeded(legacyDN2).ToString();
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000C0D78 File Offset: 0x000BEF78
		internal static LegacyDN FixFakeRedirectLegacyDNIfNeeded(LegacyDN legacyDN)
		{
			try
			{
				string rdnPrefix;
				string text;
				LegacyDN parentLegacyDN = legacyDN.GetParentLegacyDN(out rdnPrefix, out text);
				string text2;
				string text3;
				LegacyDN parentLegacyDN2 = parentLegacyDN.GetParentLegacyDN(out text2, out text3);
				if (text3 != null && text3.StartsWith(ExchangeRpcClientAccess.selfRedirectLegacyDNSectionPrefix, StringComparison.OrdinalIgnoreCase) && text != null && text.StartsWith(ExchangeRpcClientAccess.selfRedirectLegacyDNServerPrefix, StringComparison.OrdinalIgnoreCase))
				{
					return parentLegacyDN2.GetChildLegacyDN(rdnPrefix, text.Substring(ExchangeRpcClientAccess.selfRedirectLegacyDNServerPrefix.Length));
				}
			}
			catch (FormatException)
			{
			}
			return legacyDN;
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000C0DF8 File Offset: 0x000BEFF8
		internal static ADObjectId FromServerId(ADObjectId serverId)
		{
			return serverId.GetChildId("Protocols").GetChildId("RpcClientAccess");
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000C0E10 File Offset: 0x000BF010
		internal static ExchangeRpcClientAccess[] GetAll(ITopologyConfigurationSession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			IEnumerable<ExchangeRpcClientAccess> source = session.FindPaged<ExchangeRpcClientAccess>(null, QueryScope.SubTree, null, null, 0);
			return source.ToArray<ExchangeRpcClientAccess>();
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000C0E48 File Offset: 0x000BF048
		internal static IEnumerable<Server> GetAllPossibleServers(ITopologyConfigurationSession session, ADObjectId siteId)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			return from server in session.FindPaged<Server>(null, QueryScope.SubTree, QueryFilter.AndTogether(new QueryFilter[]
			{
				new BitMaskOrFilter(ServerSchema.CurrentServerRole, 6UL),
				(siteId != null) ? new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, siteId) : null
			}), null, ADGenericPagedReader<Microsoft.Exchange.Data.Directory.SystemConfiguration.Server>.DefaultPageSize)
			where server.IsE14OrLater
			select server;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000C110C File Offset: 0x000BF30C
		internal static IEnumerable<KeyValuePair<Server, ExchangeRpcClientAccess>> GetServersWithRpcClientAccessEnabled(IEnumerable<Server> cachedServers, IEnumerable<ExchangeRpcClientAccess> cachedRpcClientAccess)
		{
			if (cachedServers == null)
			{
				throw new ArgumentNullException("cachedServers");
			}
			if (cachedRpcClientAccess == null)
			{
				throw new ArgumentNullException("cachedRpcClientAccess");
			}
			Dictionary<string, ExchangeRpcClientAccess> serverIdToRca = cachedRpcClientAccess.ToDictionary((ExchangeRpcClientAccess rca) => rca.Server.DistinguishedName);
			foreach (Server server in cachedServers)
			{
				ExchangeRpcClientAccess rpcClientAccess;
				if (serverIdToRca.TryGetValue(server.Id.DistinguishedName, out rpcClientAccess))
				{
					rpcClientAccess.CompleteAllCalculatedProperties(server);
					yield return new KeyValuePair<Server, ExchangeRpcClientAccess>(server, rpcClientAccess);
				}
			}
			yield break;
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000C1130 File Offset: 0x000BF330
		internal static object ServerGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			if (adobjectId == null)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Server", string.Empty), ADObjectSchema.Id, adobjectId));
			}
			object result;
			try
			{
				result = adobjectId.DescendantDN(8);
			}
			catch (InvalidOperationException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Server", ex.Message), ADObjectSchema.Id, adobjectId), ex);
			}
			return result;
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000C11B0 File Offset: 0x000BF3B0
		internal void CompleteAllCalculatedProperties(Server cachedServer)
		{
			this.Responsibility = this.GetResponsibility(cachedServer);
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000C11C0 File Offset: 0x000BF3C0
		internal RpcClientAccessResponsibility GetResponsibility(Server cachedServer)
		{
			if (cachedServer == null)
			{
				throw new ArgumentNullException("cachedServer");
			}
			if (!cachedServer.Id.Equals(this.Server))
			{
				throw new ArgumentException("Must be the server that this protocol node corresponds to", "server");
			}
			return (cachedServer.IsClientAccessServer ? RpcClientAccessResponsibility.Mailboxes : RpcClientAccessResponsibility.None) | (cachedServer.IsMailboxServer ? RpcClientAccessResponsibility.PublicFolders : RpcClientAccessResponsibility.None);
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06002FC6 RID: 12230 RVA: 0x000C1217 File Offset: 0x000BF417
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x04002035 RID: 8245
		public const string CommonName = "RpcClientAccess";

		// Token: 0x04002036 RID: 8246
		private const string MostDerivedClassInternal = "msExchProtocolCfgExchangeRpcService";

		// Token: 0x04002037 RID: 8247
		private static readonly ExchangeRpcClientAccessSchema schema = ObjectSchema.GetInstance<ExchangeRpcClientAccessSchema>();

		// Token: 0x04002038 RID: 8248
		private static readonly string selfRedirectLegacyDNSectionPrefix = "Instance-";

		// Token: 0x04002039 RID: 8249
		private static readonly string selfRedirectLegacyDNServerPrefix = "X";
	}
}

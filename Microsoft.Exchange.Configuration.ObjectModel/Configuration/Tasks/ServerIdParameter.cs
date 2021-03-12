using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000F5 RID: 245
	[Serializable]
	public class ServerIdParameter : ADIdParameter
	{
		// Token: 0x060008C7 RID: 2247 RVA: 0x0001ECFF File Offset: 0x0001CEFF
		public ServerIdParameter(Fqdn fqdn) : this(fqdn.ToString())
		{
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001ED0D File Offset: 0x0001CF0D
		public ServerIdParameter(MailboxServer server) : base(server.Id)
		{
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001ED1B File Offset: 0x0001CF1B
		public ServerIdParameter(ExchangeServer exServer) : base(exServer.Id)
		{
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001ED29 File Offset: 0x0001CF29
		public ServerIdParameter(ClientAccessServer caServer) : base(caServer.Id)
		{
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001ED37 File Offset: 0x0001CF37
		public ServerIdParameter(UMServer umServer) : base(umServer.Id)
		{
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001ED45 File Offset: 0x0001CF45
		public ServerIdParameter(TransportServer trServer) : base(trServer.Id)
		{
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001ED53 File Offset: 0x0001CF53
		public ServerIdParameter(FrontendTransportServerPresentationObject frontendTransportServer) : base(frontendTransportServer.Id.Parent.Parent.Name)
		{
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001ED70 File Offset: 0x0001CF70
		public ServerIdParameter(MailboxTransportServerPresentationObject mailboxTransportServer) : base(mailboxTransportServer.Id.Parent.Parent.Name)
		{
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001ED8D File Offset: 0x0001CF8D
		public ServerIdParameter(MalwareFilteringServer mfServer) : base(mfServer.Id)
		{
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001ED9B File Offset: 0x0001CF9B
		public ServerIdParameter(ExchangeRpcClientAccess rpcClientAccess) : base(rpcClientAccess.Server)
		{
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001EDA9 File Offset: 0x0001CFA9
		public ServerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001EDB2 File Offset: 0x0001CFB2
		public ServerIdParameter() : this(ServerIdParameter.LocalServerFQDN)
		{
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001EDBF File Offset: 0x0001CFBF
		public ServerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001EDC8 File Offset: 0x0001CFC8
		protected ServerIdParameter(string identity) : base(identity)
		{
			if (base.InternalADObjectId != null)
			{
				return;
			}
			LegacyDN legacyDN;
			if (!ADObjectNameHelper.ReservedADNameStringRegex.IsMatch(identity) && !ServerIdParameter.IsValidName(identity) && !ServerIdParameter.IsValidFqdn(identity) && !LegacyDN.TryParse(identity, out legacyDN))
			{
				throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "identity");
			}
			this.fqdn = identity;
			if (identity.EndsWith(".", StringComparison.Ordinal))
			{
				this.fqdn = identity.Substring(0, identity.Length - 1);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0001EE4C File Offset: 0x0001D04C
		internal string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0001EE54 File Offset: 0x0001D054
		private static string LocalServerFQDN
		{
			get
			{
				return NativeHelpers.GetLocalComputerFqdn(true);
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001EE5C File Offset: 0x0001D05C
		public static ServerIdParameter Parse(string identity)
		{
			return new ServerIdParameter(identity);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001EE64 File Offset: 0x0001D064
		internal static void ClearServerRoleCache()
		{
			lock (ServerIdParameter.serverRoleCache)
			{
				ServerIdParameter.serverRoleCache.Clear();
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001EEA8 File Offset: 0x0001D0A8
		internal static bool HasRole(ADObjectId identity, ServerRole role, IConfigDataProvider session)
		{
			ServerIdParameter serverIdParameter = new ServerIdParameter(identity.DescendantDN(8));
			ServerInfo[] serverInfo = serverIdParameter.GetServerInfo(session);
			return serverInfo != null && serverInfo.Length == 1 && (serverInfo[0].Role & role) != ServerRole.None;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001EEE8 File Offset: 0x0001D0E8
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(Server) && typeof(T) != typeof(MiniServer) && typeof(T) != typeof(MiniClientAccessServerOrArray))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			if (!wrapper.HasElements() && this.Fqdn != null)
			{
				QueryFilter filter = new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.Fqdn, this.Fqdn),
					new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ExchangeLegacyDN, this.Fqdn)
				});
				wrapper = EnumerableWrapper<T>.GetWrapper(base.PerformPrimarySearch<T>(filter, rootId, session, true, optionalData));
			}
			if (typeof(T) == typeof(Server))
			{
				List<T> list = new List<T>();
				foreach (T t in wrapper)
				{
					list.Add(t);
					string key = ((ADObjectId)t.Identity).ToDNString().ToLowerInvariant();
					Server server = (Server)((object)t);
					ServerInfo serverInfo = new ServerInfo();
					serverInfo.Identity = server.Id;
					serverInfo.Role = server.CurrentServerRole;
					lock (ServerIdParameter.serverRoleCache)
					{
						ServerIdParameter.serverRoleCache[key] = serverInfo;
						ServerIdParameter.serverRoleCache[server.Name.ToLowerInvariant()] = serverInfo;
						ServerIdParameter.serverRoleCache[server.Fqdn.ToLowerInvariant()] = serverInfo;
					}
				}
				return list;
			}
			return wrapper;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001F100 File Offset: 0x0001D300
		internal ADObjectId[] GetMatchingIdentities(IConfigDataProvider session)
		{
			ServerInfo[] serverInfo = this.GetServerInfo(session);
			ADObjectId[] array = new ADObjectId[serverInfo.Length];
			for (int i = 0; i < serverInfo.Length; i++)
			{
				array[i] = serverInfo[i].Identity;
			}
			return array;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001F138 File Offset: 0x0001D338
		private static bool IsValidName(string nameString)
		{
			return Regex.IsMatch(nameString, "^[^`~!@#&\\^\\(\\)\\+\\[\\]\\{\\}\\<\\>\\?=,:|./\\\\; ]+$", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001F14C File Offset: 0x0001D34C
		private static bool IsValidFqdn(string fqdnString)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(fqdnString))
			{
				if (fqdnString.EndsWith("."))
				{
					fqdnString = fqdnString.Substring(0, fqdnString.Length - 1);
				}
				string[] array = fqdnString.Split(new char[]
				{
					'.'
				});
				result = (array.Length > 1);
				foreach (string nameString in array)
				{
					if (!ServerIdParameter.IsValidName(nameString))
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001F1C8 File Offset: 0x0001D3C8
		private ServerInfo[] GetServerInfo(IConfigDataProvider session)
		{
			string text;
			if (base.InternalADObjectId != null && !string.IsNullOrEmpty(base.InternalADObjectId.DistinguishedName))
			{
				text = base.InternalADObjectId.ToDNString();
			}
			else
			{
				text = base.RawIdentity;
			}
			text = text.ToLowerInvariant();
			lock (ServerIdParameter.serverRoleCache)
			{
				if (ServerIdParameter.serverRoleCache.ContainsKey(text))
				{
					return new ServerInfo[]
					{
						ServerIdParameter.serverRoleCache[text]
					};
				}
			}
			IEnumerable<Server> objects = base.GetObjects<Server>(null, session);
			List<ServerInfo> list = new List<ServerInfo>();
			foreach (Server server in objects)
			{
				text = ((ADObjectId)server.Identity).ToDNString().ToLowerInvariant();
				lock (ServerIdParameter.serverRoleCache)
				{
					list.Add(ServerIdParameter.serverRoleCache[text]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0400025A RID: 602
		private const int ServerDepth = 8;

		// Token: 0x0400025B RID: 603
		private static Dictionary<string, ServerInfo> serverRoleCache = new Dictionary<string, ServerInfo>();

		// Token: 0x0400025C RID: 604
		private string fqdn;
	}
}

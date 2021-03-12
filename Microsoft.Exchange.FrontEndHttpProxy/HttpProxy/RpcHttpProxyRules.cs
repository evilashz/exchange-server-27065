using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Configuration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000DA RID: 218
	internal class RpcHttpProxyRules
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x0002E64D File Offset: 0x0002C84D
		internal RpcHttpProxyRules() : this(null)
		{
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0002E656 File Offset: 0x0002C856
		internal RpcHttpProxyRules(IDirectory rule)
		{
			this.directory = (rule ?? new Directory());
			this.RefreshServerList(null);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0002E678 File Offset: 0x0002C878
		internal static RpcHttpProxyRules DefaultRpcHttpProxyRules
		{
			get
			{
				if (RpcHttpProxyRules.defaultHttpProxyRule == null)
				{
					lock (RpcHttpProxyRules.lockObject)
					{
						if (RpcHttpProxyRules.defaultHttpProxyRule == null)
						{
							RpcHttpProxyRules.defaultHttpProxyRule = new RpcHttpProxyRules();
						}
					}
				}
				return RpcHttpProxyRules.defaultHttpProxyRule;
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0002E6D0 File Offset: 0x0002C8D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Dictionary<string, ProxyDestination> dictionary = this.proxyDestinations;
			foreach (KeyValuePair<string, ProxyDestination> keyValuePair in dictionary)
			{
				stringBuilder.AppendFormat("{0} : {1}\n", keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0002E744 File Offset: 0x0002C944
		internal static void ApplyManualOverrides(Dictionary<string, ProxyDestination> proxyDestinations, string manualOverrides)
		{
			Regex regex = new Regex("^\\+(.+)=(.+):(\\d+)");
			Regex regex2 = new Regex("^\\-(.+)");
			string[] array = manualOverrides.Split(new char[]
			{
				';'
			});
			foreach (string input in array)
			{
				Match match = regex.Match(input);
				if (match.Success)
				{
					string value = match.Groups[1].Value;
					string value2 = match.Groups[2].Value;
					int port;
					if (int.TryParse(match.Groups[3].Value, out port))
					{
						proxyDestinations.Add(value, RpcHttpProxyRules.CreateFixedDestination(Server.E15MinVersion, value2, port));
					}
				}
				Match match2 = regex2.Match(input);
				if (match2.Success)
				{
					string value3 = match2.Groups[1].Value;
					proxyDestinations.Remove(value3);
				}
			}
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0002E83C File Offset: 0x0002CA3C
		internal bool TryGetProxyDestination(string rpcServerFqdn, out ProxyDestination destination)
		{
			destination = null;
			Dictionary<string, ProxyDestination> dictionary = this.proxyDestinations;
			if (dictionary != null)
			{
				dictionary.TryGetValue(rpcServerFqdn, out destination);
			}
			return destination != null;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0002E867 File Offset: 0x0002CA67
		private static void AddTwoMapsOfDestinations(Dictionary<string, ProxyDestination> dict, Server server, ProxyDestination destination)
		{
			dict[server.Fqdn] = destination;
			dict[server.Name] = destination;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0002E883 File Offset: 0x0002CA83
		private static ProxyDestination CreateFixedDestination(int version, string serverFqdn, int port)
		{
			return new ProxyDestination(version, port, serverFqdn);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0002E98C File Offset: 0x0002CB8C
		private void RefreshServerList(object stateInfo)
		{
			ADSite[] adsites = this.directory.GetADSites();
			ClientAccessArray[] clientAccessArrays = this.directory.GetClientAccessArrays();
			Server[] servers = this.directory.GetServers();
			if (adsites != null && servers != null)
			{
				Dictionary<string, ProxyDestination> dictionary = new Dictionary<string, ProxyDestination>(StringComparer.OrdinalIgnoreCase);
				foreach (Server server5 in from s in servers
				where s.IsE15OrLater && s.IsMailboxServer
				select s)
				{
					RpcHttpProxyRules.AddTwoMapsOfDestinations(dictionary, server5, RpcHttpProxyRules.CreateFixedDestination(Server.E15MinVersion, server5.Fqdn, 444));
				}
				ADSite[] array = adsites;
				for (int i = 0; i < array.Length; i++)
				{
					ADSite site = array[i];
					IEnumerable<Server> source = from s in servers
					where s.ServerSite != null && s.ServerSite.Name == site.Name
					select s;
					IEnumerable<Server> enumerable = from s in source
					where s.IsE14OrLater && !s.IsE15OrLater && s.IsClientAccessServer
					select s;
					IEnumerable<Server> source2 = from s in enumerable
					where !(bool)s[ActiveDirectoryServerSchema.IsOutOfService]
					select s;
					ProxyDestination proxyDestination = null;
					if (source2.Count<Server>() > 0)
					{
						proxyDestination = new ProxyDestination(Server.E14MinVersion, 443, (from server in enumerable
						select server.Fqdn).OrderBy((string str) => str, StringComparer.OrdinalIgnoreCase).ToArray<string>(), (from server in source2
						select server.Fqdn).OrderBy((string str) => str, StringComparer.OrdinalIgnoreCase).ToArray<string>());
					}
					foreach (Server server2 in enumerable)
					{
						RpcHttpProxyRules.AddTwoMapsOfDestinations(dictionary, server2, RpcHttpProxyRules.CreateFixedDestination(Server.E14MinVersion, server2.Fqdn, 443));
					}
					if (proxyDestination != null)
					{
						foreach (Server server3 in from s in source
						where s.IsE14OrLater && !s.IsE15OrLater && !s.IsClientAccessServer && s.IsMailboxServer
						select s)
						{
							RpcHttpProxyRules.AddTwoMapsOfDestinations(dictionary, server3, proxyDestination);
						}
						if (clientAccessArrays != null && clientAccessArrays.Count<ClientAccessArray>() > 0)
						{
							foreach (ClientAccessArray clientAccessArray in from arr in clientAccessArrays
							where arr.SiteName == site.Name
							select arr)
							{
								dictionary[clientAccessArray.Fqdn] = proxyDestination;
							}
						}
					}
					IEnumerable<Server> source3 = from s in source
					where !s.IsE14OrLater && s.IsExchange2007OrLater && s.IsClientAccessServer
					select s;
					ProxyDestination proxyDestination2 = null;
					if (source3.Count<Server>() > 0)
					{
						string[] array2 = (from server in source3
						select server.Fqdn).OrderBy((string str) => str, StringComparer.OrdinalIgnoreCase).ToArray<string>();
						proxyDestination2 = new ProxyDestination(Server.E2007MinVersion, 443, array2, array2);
					}
					else if (proxyDestination != null)
					{
						proxyDestination2 = proxyDestination;
					}
					if (proxyDestination2 != null)
					{
						foreach (Server server4 in from s in source
						where s.IsExchange2007OrLater && !s.IsE14OrLater && s.IsMailboxServer
						select s)
						{
							RpcHttpProxyRules.AddTwoMapsOfDestinations(dictionary, server4, proxyDestination2);
						}
					}
				}
				string text = WebConfigurationManager.AppSettings["OverrideProxyingRules"];
				if (!string.IsNullOrEmpty(text))
				{
					RpcHttpProxyRules.ApplyManualOverrides(dictionary, text);
				}
				this.proxyDestinations = dictionary;
			}
			if (this.refreshTimer != null)
			{
				this.refreshTimer.Change((int)RpcHttpProxyRules.TopologyRefreshInterval.TotalMilliseconds, -1);
				return;
			}
			this.refreshTimer = new Timer(new TimerCallback(this.RefreshServerList), null, (int)RpcHttpProxyRules.TopologyRefreshInterval.TotalMilliseconds, -1);
		}

		// Token: 0x040004CC RID: 1228
		private const int BrickBackEndPort = 444;

		// Token: 0x040004CD RID: 1229
		private const int OriginalRpcVDirPort = 443;

		// Token: 0x040004CE RID: 1230
		private const string AppSettingsOverrideProxyingRules = "OverrideProxyingRules";

		// Token: 0x040004CF RID: 1231
		private static readonly TimeSpan TopologyRefreshInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x040004D0 RID: 1232
		private static RpcHttpProxyRules defaultHttpProxyRule = null;

		// Token: 0x040004D1 RID: 1233
		private static object lockObject = new object();

		// Token: 0x040004D2 RID: 1234
		private IDirectory directory;

		// Token: 0x040004D3 RID: 1235
		private Dictionary<string, ProxyDestination> proxyDestinations;

		// Token: 0x040004D4 RID: 1236
		private Timer refreshTimer;
	}
}

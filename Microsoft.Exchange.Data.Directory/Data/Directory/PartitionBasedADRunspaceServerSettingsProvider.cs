using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000070 RID: 112
	[Serializable]
	internal class PartitionBasedADRunspaceServerSettingsProvider
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x0001CC10 File Offset: 0x0001AE10
		private PartitionBasedADRunspaceServerSettingsProvider(string partitionFqdn, Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState> inSiteGCs, Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState> outOfSiteGCs, Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState> inForestGCs, ADServerInfo configDC, List<string> allInSiteGCs, List<string> allOutSiteGCs, List<string> allInForestGCs)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			ArgumentValidator.ThrowIfNull("inSiteGCs", inSiteGCs);
			ArgumentValidator.ThrowIfNull("outOfSiteGCs", outOfSiteGCs);
			ArgumentValidator.ThrowIfNull("inForestGCs", inForestGCs);
			ArgumentValidator.ThrowIfNull("configDC", configDC);
			ArgumentValidator.ThrowIfNull("allInSiteGCs", allInSiteGCs);
			ArgumentValidator.ThrowIfNull("allOutSiteGCs", allOutSiteGCs);
			ArgumentValidator.ThrowIfNull("allInForestGCs", allInForestGCs);
			this.partitionFqdn = partitionFqdn;
			this.inSiteGCs = inSiteGCs;
			this.outOfSiteGCs = outOfSiteGCs;
			this.inForestGCs = inForestGCs;
			this.configDC = configDC;
			this.allInSiteGCs = allInSiteGCs;
			this.allOutOfSiteGCs = allOutSiteGCs;
			this.allInForestGCs = allInForestGCs;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001CCC8 File Offset: 0x0001AEC8
		internal PartitionBasedADRunspaceServerSettingsProvider(string partitionFqdn)
		{
			this.partitionFqdn = partitionFqdn;
			this.inSiteGCs = new Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState>(StringComparer.OrdinalIgnoreCase);
			this.outOfSiteGCs = new Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState>(StringComparer.OrdinalIgnoreCase);
			this.inForestGCs = new Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState>(StringComparer.OrdinalIgnoreCase);
			this.configDC = null;
			this.allInSiteGCs = new List<string>();
			this.allOutOfSiteGCs = new List<string>();
			this.allInForestGCs = new List<string>();
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0001CD45 File Offset: 0x0001AF45
		public ADServerInfo ConfigDC
		{
			get
			{
				return this.configDC;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0001CD4D File Offset: 0x0001AF4D
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0001CD55 File Offset: 0x0001AF55
		public int TopologyVersion { get; internal set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001CD5E File Offset: 0x0001AF5E
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0001CD66 File Offset: 0x0001AF66
		public int LastTopoRecheck { get; internal set; }

		// Token: 0x0600051E RID: 1310 RVA: 0x0001CD6F File Offset: 0x0001AF6F
		public void ReportServerDown(string serverFqdn, ADServerRole role)
		{
			if (role == ADServerRole.GlobalCatalog)
			{
				this.ReportGcDown(serverFqdn);
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001CD7C File Offset: 0x0001AF7C
		internal static bool TryCreateNew(string partitionFqdn, TopologyProvider topologyProvider, PartitionBasedADRunspaceServerSettingsProvider previousInstance, out PartitionBasedADRunspaceServerSettingsProvider newInstance)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			ArgumentValidator.ThrowIfNull("topologyProvider", topologyProvider);
			newInstance = null;
			Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState> dictionary = new Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState>(StringComparer.OrdinalIgnoreCase);
			Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState> dictionary2 = new Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState>(StringComparer.OrdinalIgnoreCase);
			Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState> dictionary3 = new Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState>(StringComparer.OrdinalIgnoreCase);
			HashSet<string> hashSet;
			if (previousInstance != null)
			{
				hashSet = new HashSet<string>(previousInstance.GetAllGCs(), StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			}
			if (previousInstance != null && ExTraceGlobals.ServerSettingsProviderTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				string arg = string.Join(",", previousInstance.inSiteGCs.Keys);
				ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string, int>((long)partitionFqdn.GetHashCode(), "Previous in site servers {0} (from instance {1})", arg, previousInstance.GetHashCode());
				string arg2 = string.Join(",", previousInstance.outOfSiteGCs.Keys);
				ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string, int>((long)partitionFqdn.GetHashCode(), "Previous out of site servers {0} (from instance {1})", arg2, previousInstance.GetHashCode());
			}
			List<string> currentlyUsedServers = (previousInstance != null) ? previousInstance.GetAllGCs().ToList<string>() : new List<string>();
			IList<ADServerInfo> serversForRole = topologyProvider.GetServersForRole(partitionFqdn, currentlyUsedServers, ADServerRole.GlobalCatalog, 25, false);
			ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<int, string>((long)partitionFqdn.GetHashCode(), "GetServersForRole returned a list of length: {0} for forest ={1}", serversForRole.Count, partitionFqdn);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			for (int i = 0; i < serversForRole.Count; i++)
			{
				ADRunspaceServerSettingsProvider.ADServerInfoState adserverInfoState = new ADRunspaceServerSettingsProvider.ADServerInfoState(serversForRole[i])
				{
					IsDown = false
				};
				bool flag = adserverInfoState.IsInLocalSite && previousInstance != null && !previousInstance.inSiteGCs.ContainsKey(adserverInfoState.ServerInfo.Fqdn);
				bool flag2 = !adserverInfoState.IsInLocalSite && previousInstance != null && !previousInstance.outOfSiteGCs.ContainsKey(adserverInfoState.ServerInfo.Fqdn);
				bool flag3 = flag || flag2;
				if (!hashSet.Contains(adserverInfoState.ServerInfo.Fqdn))
				{
					if (adserverInfoState.IsInLocalSite)
					{
						list.Add(adserverInfoState.ServerInfo.Fqdn);
					}
					else
					{
						list2.Add(adserverInfoState.ServerInfo.Fqdn);
					}
				}
				ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug((long)partitionFqdn.GetHashCode(), "Forest = {0}. {1} server: {2}, {3} site.", new object[]
				{
					partitionFqdn,
					flag3 ? "Adding a new" : "Keeping",
					adserverInfoState.ServerInfo.Fqdn,
					adserverInfoState.IsInLocalSite ? "in the local" : "out of"
				});
				if (adserverInfoState.IsInLocalSite)
				{
					dictionary[adserverInfoState.ServerInfo.Fqdn] = adserverInfoState;
				}
				else
				{
					dictionary2[adserverInfoState.ServerInfo.Fqdn] = adserverInfoState;
				}
			}
			list.Sort();
			list2.Sort();
			if (previousInstance != null)
			{
				list.InsertRange(0, previousInstance.allInSiteGCs);
				list2.InsertRange(0, previousInstance.allOutOfSiteGCs);
			}
			List<string> list4 = new List<string>(1);
			if (previousInstance != null && previousInstance.ConfigDC != null)
			{
				list4.Add(previousInstance.ConfigDC.Fqdn);
			}
			serversForRole = topologyProvider.GetServersForRole(partitionFqdn, list4, ADServerRole.ConfigurationDomainController, 1, false);
			ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<int, string>((long)partitionFqdn.GetHashCode(), "GetServersForRole returned a list of length: {0} for forest={1}", serversForRole.Count, partitionFqdn);
			ADServerInfo adserverInfo = null;
			if (serversForRole.Count > 0)
			{
				adserverInfo = serversForRole[0];
				ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string, string, string>((long)partitionFqdn.GetHashCode(), "Forest = {0}. {1} Config DC {2}", partitionFqdn, (previousInstance != null && previousInstance.configDC != null && string.Equals(serversForRole[0].Fqdn, previousInstance.configDC.Fqdn, StringComparison.OrdinalIgnoreCase)) ? "New" : "Same", adserverInfo.Fqdn);
			}
			else if (previousInstance != null && previousInstance.configDC != null)
			{
				adserverInfo = previousInstance.ConfigDC;
			}
			if (previousInstance != null && ExTraceGlobals.ServerSettingsProviderTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				string arg3 = string.Join(",", previousInstance.inForestGCs.Keys);
				ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string, int>((long)partitionFqdn.GetHashCode(), "Previous in forest servers {0} (from instance {1})", arg3, previousInstance.GetHashCode());
			}
			serversForRole = topologyProvider.GetServersForRole(partitionFqdn, currentlyUsedServers, ADServerRole.GlobalCatalog, 25, true);
			ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<int, string, string>((long)partitionFqdn.GetHashCode(), "GetServersForRole returned a list of length: {0} for forest = {1}. The list is: {2}.", serversForRole.Count, partitionFqdn, string.Join<ADServerInfo>(",", serversForRole));
			for (int j = 0; j < serversForRole.Count; j++)
			{
				ADRunspaceServerSettingsProvider.ADServerInfoState adserverInfoState2 = new ADRunspaceServerSettingsProvider.ADServerInfoState(serversForRole[j], true);
				list3.Add(adserverInfoState2.ServerInfo.Fqdn);
				if (adserverInfoState2.ServerInfo.IsServerSuitable)
				{
					adserverInfoState2.IsDown = false;
					dictionary3[adserverInfoState2.ServerInfo.Fqdn] = adserverInfoState2;
				}
				else
				{
					adserverInfoState2.IsDown = true;
				}
			}
			if (adserverInfo != null && (dictionary.Count > 0 || dictionary2.Count > 0))
			{
				newInstance = new PartitionBasedADRunspaceServerSettingsProvider(partitionFqdn, dictionary, dictionary2, dictionary3, adserverInfo, list, list2, list3);
				return true;
			}
			ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug((long)partitionFqdn.GetHashCode(), "{0} unable to create a new PartitionBasedADRunspaceServerSettingsProvider. CDC {1} InSiteGCs {2} OutOfSiteGCs {3}", new object[]
			{
				partitionFqdn,
				(adserverInfo != null) ? adserverInfo.FqdnPlusPort : "<NULL>",
				dictionary.Count,
				dictionary2.Count
			});
			return false;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001D2A2 File Offset: 0x0001B4A2
		internal ADServerInfo GetGcFromToken(string token, out bool isInLocalSite)
		{
			return this.GetGcFromToken(token, out isInLocalSite, false);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001D2B0 File Offset: 0x0001B4B0
		internal ADServerInfo GetGcFromToken(string token, out bool isInLocalSite, bool forestWideAffinityRequested = false)
		{
			isInLocalSite = false;
			if (forestWideAffinityRequested)
			{
				if (this.inForestGCs != null)
				{
					ADServerInfo serverFromToken = this.GetServerFromToken(token, this.allInForestGCs, this.inForestGCs, PartitionBasedADRunspaceServerSettingsProvider.hashBaseForest);
					isInLocalSite = false;
					if (serverFromToken != null)
					{
						return serverFromToken;
					}
					ExTraceGlobals.ServerSettingsProviderTracer.TraceWarning<string>((long)this.GetHashCode(), "{0} - There is no Global Catalog available in the forest.", this.partitionFqdn);
				}
			}
			else
			{
				isInLocalSite = true;
				if (this.inSiteGCs != null && this.allInSiteGCs != null)
				{
					ADServerInfo serverFromToken = this.GetServerFromToken(token, this.allInSiteGCs, this.inSiteGCs, PartitionBasedADRunspaceServerSettingsProvider.hashBase);
					if (serverFromToken != null)
					{
						return serverFromToken;
					}
					ExTraceGlobals.ServerSettingsProviderTracer.TraceWarning<string, string>((long)this.GetHashCode(), "{0} - There is no Global Catalog available in the local site for token {1}", this.partitionFqdn, token ?? "<null>");
				}
				isInLocalSite = false;
				if (this.outOfSiteGCs != null && this.allOutOfSiteGCs != null)
				{
					ADServerInfo serverFromToken = this.GetServerFromToken(token, this.allOutOfSiteGCs, this.outOfSiteGCs, PartitionBasedADRunspaceServerSettingsProvider.hashBase);
					if (serverFromToken != null)
					{
						return serverFromToken;
					}
					ExTraceGlobals.ServerSettingsProviderTracer.TraceWarning<string, string>((long)this.GetHashCode(), "{0} - There is no Global Catalog available in remote site for token {1}", this.partitionFqdn, token ?? "<null>");
				}
			}
			return null;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001D3C4 File Offset: 0x0001B5C4
		internal ADServerInfo[] GetGcFromToken(string token, int serverCountRequested, out bool isInLocalSite, bool forestWideAffinityRequested = false)
		{
			isInLocalSite = false;
			ADServerInfo[] array = null;
			if (forestWideAffinityRequested)
			{
				if (this.inForestGCs != null)
				{
					array = this.GetServerFromToken(token, this.allInForestGCs, serverCountRequested, this.inForestGCs, PartitionBasedADRunspaceServerSettingsProvider.hashBaseForest);
					if (array.Length == 0)
					{
						ExTraceGlobals.ServerSettingsProviderTracer.TraceWarning<string, string>((long)this.GetHashCode(), "{0} There is no Global Catalog available in the forest for token {1}", this.partitionFqdn, token ?? "<null>");
					}
				}
			}
			else
			{
				isInLocalSite = true;
				if (this.inSiteGCs != null && this.allInSiteGCs != null)
				{
					array = this.GetServerFromToken(token, this.allInSiteGCs, serverCountRequested, this.inSiteGCs, PartitionBasedADRunspaceServerSettingsProvider.hashBase);
					if (array.Length == 0)
					{
						ExTraceGlobals.ServerSettingsProviderTracer.TraceWarning<string, string>((long)this.GetHashCode(), "{0} There is no Global Catalog available in the local site for token {1}", this.partitionFqdn, token ?? "<null>");
						isInLocalSite = false;
						if (this.outOfSiteGCs != null && this.allOutOfSiteGCs != null)
						{
							array = this.GetServerFromToken(token, this.allOutOfSiteGCs, 1, this.outOfSiteGCs, PartitionBasedADRunspaceServerSettingsProvider.hashBase);
						}
					}
				}
			}
			if (array.Length != 0)
			{
				return array;
			}
			return null;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
		internal bool IsServerKnown(Fqdn serverFqdn)
		{
			return (this.configDC != null && this.configDC.Fqdn.Equals(serverFqdn.ToString(), StringComparison.OrdinalIgnoreCase)) || this.inForestGCs.ContainsKey(serverFqdn.ToString()) || this.inSiteGCs.ContainsKey(serverFqdn.ToString()) || this.outOfSiteGCs.ContainsKey(serverFqdn.ToString());
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001D529 File Offset: 0x0001B729
		private IEnumerable<string> GetAllGCs()
		{
			return this.allInSiteGCs.Concat(this.allOutOfSiteGCs);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001D53C File Offset: 0x0001B73C
		private ADServerInfo GetServerFromToken(string token, List<string> serverList, Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState> serverDict, int[] hashArray)
		{
			ADServerInfo[] serverFromToken = this.GetServerFromToken(token, serverList, 1, serverDict, hashArray);
			if (serverFromToken.Length > 0)
			{
				return serverFromToken[0];
			}
			return null;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001D564 File Offset: 0x0001B764
		private ADServerInfo[] GetServerFromToken(string token, List<string> serverList, int serverCountRequested, Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState> serverDict, int[] hashArray)
		{
			List<ADServerInfo> list = new List<ADServerInfo>(serverCountRequested);
			if (serverList.Count <= 0)
			{
				return list.ToArray();
			}
			bool[] array = new bool[serverList.Count];
			int num = 0;
			int num2;
			if (!string.IsNullOrEmpty(token))
			{
				num2 = Math.Abs(token.GetHashCode());
			}
			else
			{
				num2 = this.random.Next(serverList.Count);
			}
			while (list.Count < serverCountRequested && num < hashArray.Length)
			{
				int num3 = num2 % hashArray[num];
				if (num3 < serverList.Count)
				{
					if (!serverDict.ContainsKey(serverList[num3]) || serverDict[serverList[num3]].IsDown)
					{
						ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "{0} Skipping Global Catalog {1} for token {2}", this.partitionFqdn, serverList[num3], token ?? "<null>");
					}
					else if (!array[num3])
					{
						ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "{0} Selecting Global Catalog {1} for token {2}", this.partitionFqdn, serverList[num3], token ?? "<null>");
						list.Add(serverDict[serverList[num3]].ServerInfo);
						array[num3] = true;
					}
				}
				num++;
			}
			int num4 = array.Length - 1;
			while (num4 >= 0 && list.Count < serverCountRequested)
			{
				if (!array[num4] && serverDict.ContainsKey(serverList[num4]) && !serverDict[serverList[num4]].IsDown)
				{
					list.Add(serverDict[serverList[num4]].ServerInfo);
					array[num4] = true;
				}
				num4--;
			}
			return list.ToArray();
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001D70C File Offset: 0x0001B90C
		private void ReportGcDown(string serverFqdn)
		{
			if (this.inSiteGCs.ContainsKey(serverFqdn))
			{
				ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Marking Global Catalog {1} as down", this.partitionFqdn, serverFqdn);
				this.inSiteGCs[serverFqdn].IsDown = true;
				return;
			}
			if (this.outOfSiteGCs.ContainsKey(serverFqdn))
			{
				ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Marking Global Catalog {1} as down", this.partitionFqdn, serverFqdn);
				this.outOfSiteGCs[serverFqdn].IsDown = true;
				return;
			}
			if (this.inForestGCs.ContainsKey(serverFqdn))
			{
				ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - Marking Global Catalog {1} as down", this.partitionFqdn, serverFqdn);
				this.inForestGCs[serverFqdn].IsDown = true;
			}
		}

		// Token: 0x04000228 RID: 552
		private const int MaxServersCount = 25;

		// Token: 0x04000229 RID: 553
		private static readonly int[] hashBase = new int[]
		{
			23,
			17,
			13,
			11,
			7,
			6,
			5,
			3,
			2,
			1
		};

		// Token: 0x0400022A RID: 554
		private static readonly int[] hashBaseForest = new int[]
		{
			97,
			89,
			83,
			79,
			73,
			71,
			67,
			61,
			59,
			53,
			47,
			43,
			41,
			37,
			31,
			29,
			23,
			17,
			13,
			11,
			7,
			6,
			5,
			3,
			2,
			1
		};

		// Token: 0x0400022B RID: 555
		private Random random = new Random();

		// Token: 0x0400022C RID: 556
		private readonly string partitionFqdn;

		// Token: 0x0400022D RID: 557
		private readonly Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState> inSiteGCs;

		// Token: 0x0400022E RID: 558
		private readonly Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState> outOfSiteGCs;

		// Token: 0x0400022F RID: 559
		private readonly Dictionary<string, ADRunspaceServerSettingsProvider.ADServerInfoState> inForestGCs;

		// Token: 0x04000230 RID: 560
		private readonly ADServerInfo configDC;

		// Token: 0x04000231 RID: 561
		private List<string> allInSiteGCs;

		// Token: 0x04000232 RID: 562
		private List<string> allOutOfSiteGCs;

		// Token: 0x04000233 RID: 563
		private List<string> allInForestGCs;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200065B RID: 1627
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ServersCache
	{
		// Token: 0x06004C22 RID: 19490 RVA: 0x001192D8 File Offset: 0x001174D8
		private static ADPagedReader<MiniServer> ReadLocalSiteMailboxServers()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.NonCacheSessionFactory.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 102, "ReadLocalSiteMailboxServers", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\ServersCache.cs");
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, ServersCache.LocalSiteId);
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				queryFilter,
				ServersCache.MailboxServerRoleFilter,
				ServersCache.ServerOnlineFilter
			});
			SortBy sortBy = new SortBy(ServerSchema.VersionNumber, SortOrder.Ascending);
			return topologyConfigurationSession.FindPaged<MiniServer>(null, QueryScope.SubTree, filter, sortBy, 0, null);
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x00119356 File Offset: 0x00117556
		private static bool IsLocalSite(ADObjectId adSiteId)
		{
			return adSiteId != null && adSiteId.Equals(ServersCache.LocalSiteId);
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x00119370 File Offset: 0x00117570
		private static MiniServer GetOneMailboxServerForASite(ADObjectId adSiteId, int versionNumber, bool needsExactVersionMatch)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.NonCacheSessionFactory.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 145, "GetOneMailboxServerForASite", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\ServersCache.cs");
			QueryFilter queryFilter;
			if (needsExactVersionMatch)
			{
				queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.VersionNumber, versionNumber);
			}
			else
			{
				queryFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.VersionNumber, versionNumber);
			}
			QueryFilter filter;
			if (adSiteId != null)
			{
				QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, adSiteId);
				filter = new AndFilter(new QueryFilter[]
				{
					queryFilter2,
					ServersCache.MailboxServerRoleFilter,
					ServersCache.ServerOnlineFilter,
					queryFilter
				});
			}
			else
			{
				filter = new AndFilter(new QueryFilter[]
				{
					ServersCache.MailboxServerRoleFilter,
					ServersCache.ServerOnlineFilter,
					queryFilter
				});
			}
			MiniServer[] array = topologyConfigurationSession.Find<MiniServer>(null, QueryScope.SubTree, filter, null, 1, null);
			if (array != null && array.Length > 0)
			{
				return array[0];
			}
			throw new ServerHasNotBeenFoundException(versionNumber, string.Empty, needsExactVersionMatch, (adSiteId != null) ? adSiteId.Name : string.Empty);
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x0011949C File Offset: 0x0011769C
		private static void UpdateServerInfoInList(List<ServersCache.ServerInfo> list, ServersCache.ServerInfo serverInfo)
		{
			if (list != null || list.Count > 0)
			{
				int num = list.FindIndex((ServersCache.ServerInfo x) => string.Compare(x.MiniServer.Fqdn, serverInfo.MiniServer.Fqdn, true) == 0);
				if (num != -1)
				{
					list.RemoveAt(num);
				}
				list.Insert(0, serverInfo);
			}
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x001194F4 File Offset: 0x001176F4
		private static void UpdateMiniServerIntoCache(MiniServer miniServer)
		{
			ServersCache.ServerInfo serverInfo = null;
			if (miniServer != null)
			{
				try
				{
					ServersCache.CacheLockForServersDictionary.EnterWriteLock();
					serverInfo = new ServersCache.ServerInfo(DateTime.UtcNow, miniServer);
					ServersCache.ServersDictionary[miniServer.Fqdn] = serverInfo;
				}
				finally
				{
					ServersCache.CacheLockForServersDictionary.ExitWriteLock();
				}
				ADObjectId serverSite = miniServer.ServerSite;
				if (!ServersCache.IsLocalSite(serverSite))
				{
					try
					{
						ServersCache.CacheLockForSiteToServersDictionary.EnterWriteLock();
						if (ServersCache.SiteToServersDictionary.ContainsKey(serverSite))
						{
							ServersCache.UpdateServerInfoInList(ServersCache.SiteToServersDictionary[serverSite], serverInfo);
						}
						else
						{
							List<ServersCache.ServerInfo> list = new List<ServersCache.ServerInfo>();
							list.Add(serverInfo);
							ServersCache.SiteToServersDictionary[serverSite] = list;
						}
					}
					finally
					{
						ServersCache.CacheLockForSiteToServersDictionary.ExitWriteLock();
					}
				}
			}
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x001195BC File Offset: 0x001177BC
		private static bool TryCalculateStartAndEndIndex(List<ServersCache.ServerInfo> list, int versionNumber, bool needsExactVersionMatch, out int startIndex, out int endIndex)
		{
			startIndex = -1;
			endIndex = -1;
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].MiniServer.VersionNumber == versionNumber && needsExactVersionMatch)
				{
					if (endIndex != -1 && list[i].MiniServer.VersionNumber > list[endIndex].MiniServer.VersionNumber)
					{
						break;
					}
					if (startIndex == -1)
					{
						startIndex = i;
						endIndex = i;
					}
					else
					{
						endIndex = i;
					}
				}
				else if (list[i].MiniServer.VersionNumber >= versionNumber && !needsExactVersionMatch && startIndex == -1)
				{
					startIndex = i;
					break;
				}
			}
			if (startIndex == -1)
			{
				return false;
			}
			if (!needsExactVersionMatch)
			{
				endIndex = list.Count - 1;
			}
			return true;
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x00119680 File Offset: 0x00117880
		private static int GenerateIndexOfServer(int startIndex, int endIndex, string identifier = null)
		{
			if (string.IsNullOrWhiteSpace(identifier))
			{
				Random random = new Random();
				return random.Next(startIndex, endIndex + 1);
			}
			int num = Math.Abs(identifier.GetHashCode());
			int num2 = num % (endIndex - startIndex + 1);
			return num2 + startIndex;
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x001196C0 File Offset: 0x001178C0
		private static MiniServer FindMiniServerInListWithoutAffinity(List<ServersCache.ServerInfo> list, int versionNumber, bool needsExactVersionMatch)
		{
			MiniServer result = null;
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					TimeSpan t = DateTime.UtcNow - list[i].LastRefreshTime;
					if (needsExactVersionMatch)
					{
						if (list[i].MiniServer.VersionNumber == versionNumber && t <= ServersCache.RefreshInterval)
						{
							result = list[i].MiniServer;
						}
					}
					else if (list[i].MiniServer.VersionNumber >= versionNumber && t <= ServersCache.RefreshInterval)
					{
						result = list[i].MiniServer;
					}
				}
			}
			return result;
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x00119764 File Offset: 0x00117964
		private static MiniServer FindAndReturnMiniServerFromCacheForASite(ADObjectId siteId, int versionNumber, string identifier, bool needsExactVersionMatch)
		{
			int startIndex = -1;
			int endIndex = -1;
			MiniServer miniServer = null;
			if (ServersCache.SiteToServersDictionary.ContainsKey(siteId))
			{
				try
				{
					ServersCache.CacheLockForSiteToServersDictionary.EnterReadLock();
					List<ServersCache.ServerInfo> list = ServersCache.SiteToServersDictionary[siteId];
					if (ServersCache.IsLocalSite(siteId))
					{
						if (DateTime.UtcNow - ServersCache.LastRefreshTimeForLocalSiteCache > ServersCache.RefreshInterval)
						{
							miniServer = null;
						}
						else
						{
							bool flag = ServersCache.TryCalculateStartAndEndIndex(list, versionNumber, needsExactVersionMatch, out startIndex, out endIndex);
							if (flag)
							{
								int index = ServersCache.GenerateIndexOfServer(startIndex, endIndex, identifier);
								miniServer = list[index].MiniServer;
							}
						}
					}
					else
					{
						miniServer = ServersCache.FindMiniServerInListWithoutAffinity(list, versionNumber, needsExactVersionMatch);
					}
					if (miniServer == null)
					{
						ServersCache.Tracer.TraceError<int>(0L, "ServersCache: No server with the version number {0} in the cache.", versionNumber);
					}
					return miniServer;
				}
				finally
				{
					ServersCache.CacheLockForSiteToServersDictionary.ExitReadLock();
				}
			}
			return null;
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x00119834 File Offset: 0x00117A34
		private static MiniServer GetDeterministicBackEndServerForASite(int versionNumber, string identifier, bool needsExactVersionMatch = false, ADObjectId adSiteId = null)
		{
			ADObjectId adobjectId = (adSiteId != null) ? adSiteId : ServersCache.LocalSiteId;
			bool flag = ServersCache.IsLocalSite(adobjectId);
			MiniServer miniServer = ServersCache.FindAndReturnMiniServerFromCacheForASite(adobjectId, versionNumber, identifier, needsExactVersionMatch);
			if (miniServer != null)
			{
				return miniServer;
			}
			if (flag)
			{
				if (ServersCache.SiteToServersDictionary.ContainsKey(adobjectId) && DateTime.UtcNow - ServersCache.LastRefreshTimeForLocalSiteCache <= ServersCache.RefreshInterval)
				{
					throw new ServerHasNotBeenFoundException(versionNumber, identifier, needsExactVersionMatch, adobjectId.Name);
				}
				lock (ServersCache.LockForLocalSiteDiscovery)
				{
					if (ServersCache.SiteToServersDictionary.ContainsKey(adobjectId) && DateTime.UtcNow - ServersCache.LastRefreshTimeForLocalSiteCache <= ServersCache.RefreshInterval)
					{
						MiniServer miniServer2 = ServersCache.FindAndReturnMiniServerFromCacheForASite(adobjectId, versionNumber, identifier, needsExactVersionMatch);
						if (miniServer2 == null)
						{
							ServersCache.Tracer.TraceError(0L, "ServersCache: No server with the version number {0}, identifier '{1}', needsExactVersionMatch {2} and siteId.Name {3} in the cache for local site.", new object[]
							{
								versionNumber,
								identifier,
								needsExactVersionMatch,
								adobjectId.Name
							});
						}
						return miniServer2;
					}
					ADPagedReader<MiniServer> adpagedReader = ServersCache.ReadLocalSiteMailboxServers();
					List<ServersCache.ServerInfo> list = null;
					if (adpagedReader == null)
					{
						throw new ServerHasNotBeenFoundException(versionNumber, identifier, needsExactVersionMatch, adobjectId.Name);
					}
					list = new List<ServersCache.ServerInfo>();
					foreach (MiniServer miniServer3 in adpagedReader)
					{
						ServersCache.ServerInfo item = new ServersCache.ServerInfo(DateTime.UtcNow, miniServer3);
						list.Add(item);
					}
					try
					{
						ServersCache.CacheLockForServersDictionary.EnterWriteLock();
						foreach (ServersCache.ServerInfo serverInfo in list)
						{
							ServersCache.ServersDictionary[serverInfo.MiniServer.Fqdn] = serverInfo;
						}
					}
					finally
					{
						ServersCache.CacheLockForServersDictionary.ExitWriteLock();
					}
					if (list.Count == 0)
					{
						throw new ServerHasNotBeenFoundException(versionNumber, identifier, needsExactVersionMatch, adobjectId.Name);
					}
					ServersCache.LastRefreshTimeForLocalSiteCache = DateTime.UtcNow;
					try
					{
						ServersCache.CacheLockForSiteToServersDictionary.EnterWriteLock();
						ServersCache.SiteToServersDictionary[adobjectId] = list;
					}
					finally
					{
						ServersCache.CacheLockForSiteToServersDictionary.ExitWriteLock();
					}
					MiniServer miniServer4 = ServersCache.FindAndReturnMiniServerFromCacheForASite(adobjectId, versionNumber, identifier, needsExactVersionMatch);
					if (miniServer4 == null)
					{
						throw new ServerHasNotBeenFoundException(versionNumber, identifier, needsExactVersionMatch, adobjectId.Name);
					}
					return miniServer4;
				}
			}
			MiniServer oneMailboxServerForASite = ServersCache.GetOneMailboxServerForASite(adobjectId, versionNumber, needsExactVersionMatch);
			ServersCache.UpdateMiniServerIntoCache(oneMailboxServerForASite);
			return oneMailboxServerForASite;
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x00119B04 File Offset: 0x00117D04
		private static MiniServer MakeADQueryToGetServer(string serverFQDN)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.NonCacheSessionFactory.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 622, "MakeADQueryToGetServer", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\ServersCache.cs");
			MiniServer miniServer = topologyConfigurationSession.FindMiniServerByFqdn(serverFQDN);
			ServersCache.UpdateMiniServerIntoCache(miniServer);
			if (miniServer == null)
			{
				throw new LocalServerNotFoundException(serverFQDN);
			}
			return miniServer;
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x00119B4C File Offset: 0x00117D4C
		internal static MiniServer GetDeterministicBackEndServerFromLocalSite(int versionNumber, string identifier, bool needsExactVersionMatch = false)
		{
			return ServersCache.GetDeterministicBackEndServerForASite(versionNumber, identifier, needsExactVersionMatch, null);
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x00119B64 File Offset: 0x00117D64
		internal static MiniServer GetAnyBackEndServerFromLocalSite(int versionNumber, bool needsExactVersionMatch = false)
		{
			return ServersCache.GetDeterministicBackEndServerForASite(versionNumber, null, needsExactVersionMatch, null);
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x00119B7C File Offset: 0x00117D7C
		internal static MiniServer GetAnyBackEndServerFromASite(ADObjectId adSiteId, int versionNumber, bool needsExactVersionMatch = false)
		{
			return ServersCache.GetDeterministicBackEndServerForASite(versionNumber, null, needsExactVersionMatch, adSiteId);
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x00119B94 File Offset: 0x00117D94
		internal static MiniServer GetServerByFQDN(string serverFQDN, out bool isFromCache)
		{
			if (string.IsNullOrWhiteSpace(serverFQDN))
			{
				throw new ArgumentNullException("serverName should not be empty or null.");
			}
			isFromCache = true;
			try
			{
				ServersCache.CacheLockForServersDictionary.EnterReadLock();
				if (ServersCache.ServersDictionary.ContainsKey(serverFQDN) && DateTime.UtcNow - ServersCache.ServersDictionary[serverFQDN].LastRefreshTime <= ServersCache.RefreshInterval)
				{
					return ServersCache.ServersDictionary[serverFQDN].MiniServer;
				}
			}
			finally
			{
				ServersCache.CacheLockForServersDictionary.ExitReadLock();
			}
			MiniServer result = ServersCache.MakeADQueryToGetServer(serverFQDN);
			isFromCache = false;
			return result;
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x00119C34 File Offset: 0x00117E34
		internal static MiniServer GetDeterministicBackEndServerFromSameSite(string sourceServerFQDN, int versionNumber, string identifier, bool needsExactVersionMatch = false)
		{
			if (string.IsNullOrWhiteSpace(sourceServerFQDN))
			{
				throw new ArgumentNullException("sourceServerName should not be null");
			}
			bool flag = true;
			MiniServer serverByFQDN = ServersCache.GetServerByFQDN(sourceServerFQDN, out flag);
			if (!flag)
			{
				ServersCache.UpdateMiniServerIntoCache(serverByFQDN);
			}
			ADObjectId serverSite = serverByFQDN.ServerSite;
			return ServersCache.GetDeterministicBackEndServerForASite(versionNumber, identifier, needsExactVersionMatch, serverSite);
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x00119C7C File Offset: 0x00117E7C
		internal static MiniServer GetAnyBackEndServerWithExactVersion(int versionNumber)
		{
			return ServersCache.GetAnyBackEndServer(versionNumber, true);
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x00119C94 File Offset: 0x00117E94
		internal static MiniServer GetAnyBackEndServerWithMinVersion(int miniversionNumber)
		{
			return ServersCache.GetAnyBackEndServer(miniversionNumber, false);
		}

		// Token: 0x06004C34 RID: 19508 RVA: 0x00119CAC File Offset: 0x00117EAC
		private static MiniServer GetAnyBackEndServer(int versionNumber, bool needsExactVersionMatch)
		{
			MiniServer miniServer = null;
			try
			{
				miniServer = ServersCache.GetDeterministicBackEndServerForASite(versionNumber, null, needsExactVersionMatch, null);
			}
			catch (ServerHasNotBeenFoundException)
			{
			}
			if (miniServer != null)
			{
				return miniServer;
			}
			try
			{
				ServersCache.CacheLockForServersDictionary.EnterReadLock();
				miniServer = ServersCache.FindMiniServerInListWithoutAffinity(ServersCache.ServersDictionary.Values.ToList<ServersCache.ServerInfo>(), versionNumber, needsExactVersionMatch);
			}
			finally
			{
				ServersCache.CacheLockForServersDictionary.ExitReadLock();
			}
			if (miniServer != null)
			{
				return miniServer;
			}
			MiniServer oneMailboxServerForASite = ServersCache.GetOneMailboxServerForASite(null, versionNumber, needsExactVersionMatch);
			ServersCache.UpdateMiniServerIntoCache(oneMailboxServerForASite);
			return oneMailboxServerForASite;
		}

		// Token: 0x04003439 RID: 13369
		private static readonly Trace Tracer = ExTraceGlobals.SystemConfigurationCacheTracer;

		// Token: 0x0400343A RID: 13370
		private static readonly TimeSpan RefreshInterval = TimeSpan.FromHours(1.0);

		// Token: 0x0400343B RID: 13371
		private static DateTime LastRefreshTimeForLocalSiteCache = DateTime.MinValue;

		// Token: 0x0400343C RID: 13372
		private static Dictionary<ADObjectId, List<ServersCache.ServerInfo>> SiteToServersDictionary = new Dictionary<ADObjectId, List<ServersCache.ServerInfo>>();

		// Token: 0x0400343D RID: 13373
		private static Dictionary<string, ServersCache.ServerInfo> ServersDictionary = new Dictionary<string, ServersCache.ServerInfo>();

		// Token: 0x0400343E RID: 13374
		private static ReaderWriterLockSlim CacheLockForServersDictionary = new ReaderWriterLockSlim();

		// Token: 0x0400343F RID: 13375
		private static ReaderWriterLockSlim CacheLockForSiteToServersDictionary = new ReaderWriterLockSlim();

		// Token: 0x04003440 RID: 13376
		private static readonly object LockForLocalSiteDiscovery = new object();

		// Token: 0x04003441 RID: 13377
		private static QueryFilter MailboxServerRoleFilter = new BitMaskAndFilter(ServerSchema.CurrentServerRole, 2UL);

		// Token: 0x04003442 RID: 13378
		private static AndFilter ServerOnlineFilter = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.Equal, ActiveDirectoryServerSchema.AreServerStatesOnline, true),
			new ComparisonFilter(ComparisonOperator.Equal, ActiveDirectoryServerSchema.IsOutOfService, false)
		});

		// Token: 0x04003443 RID: 13379
		private static ADObjectId LocalSiteId = LocalSiteCache.LocalSite.Id;

		// Token: 0x0200065C RID: 1628
		internal class ServerInfo
		{
			// Token: 0x17001917 RID: 6423
			// (get) Token: 0x06004C36 RID: 19510 RVA: 0x00119DF0 File Offset: 0x00117FF0
			// (set) Token: 0x06004C37 RID: 19511 RVA: 0x00119DF8 File Offset: 0x00117FF8
			public DateTime LastRefreshTime { get; private set; }

			// Token: 0x17001918 RID: 6424
			// (get) Token: 0x06004C38 RID: 19512 RVA: 0x00119E01 File Offset: 0x00118001
			// (set) Token: 0x06004C39 RID: 19513 RVA: 0x00119E09 File Offset: 0x00118009
			public MiniServer MiniServer { get; private set; }

			// Token: 0x06004C3A RID: 19514 RVA: 0x00119E12 File Offset: 0x00118012
			public ServerInfo(DateTime lastRefreshTime, MiniServer miniServer)
			{
				this.LastRefreshTime = lastRefreshTime;
				this.MiniServer = miniServer;
			}
		}
	}
}

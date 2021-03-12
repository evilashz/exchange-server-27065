using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices
{
	// Token: 0x02000438 RID: 1080
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class MiniServerLookupCache : IFindMiniServer
	{
		// Token: 0x0600303D RID: 12349 RVA: 0x000C5E0F File Offset: 0x000C400F
		public MiniServerLookupCache(IADToplogyConfigurationSession adSession) : this(adSession, MiniServerLookupCache.TimeToLive, MiniServerLookupCache.TimeToNegativeLive, MiniServerLookupCache.CacheLockTimeout, MiniServerLookupCache.AdOperationTimeout, MiniServerLookupCache.MaximumTimeToLive)
		{
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000C5E31 File Offset: 0x000C4031
		public MiniServerLookupCache(IADToplogyConfigurationSession adSession, TimeSpan timeToLive, TimeSpan timeToNegativeLive) : this(adSession, timeToLive, timeToNegativeLive, MiniServerLookupCache.CacheLockTimeout, MiniServerLookupCache.AdOperationTimeout, MiniServerLookupCache.MaximumTimeToLive)
		{
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000C5E4B File Offset: 0x000C404B
		public MiniServerLookupCache(IADToplogyConfigurationSession adSession, TimeSpan timeToLive, TimeSpan timeToNegativeLive, TimeSpan cacheLockTimeout, TimeSpan adOperationTimeout) : this(adSession, timeToLive, timeToNegativeLive, cacheLockTimeout, adOperationTimeout, MiniServerLookupCache.MaximumTimeToLive)
		{
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000C5E60 File Offset: 0x000C4060
		public MiniServerLookupCache(IADToplogyConfigurationSession adSession, TimeSpan timeToLive, TimeSpan timeToNegativeLive, TimeSpan cacheLockTimeout, TimeSpan adOperationTimeout, TimeSpan maximumCacheTimeout)
		{
			this.AdSession = adSession;
			this.m_timeToLive = timeToLive;
			this.m_timeToNegativeLive = timeToNegativeLive;
			this.m_cacheLockTimeout = cacheLockTimeout;
			this.m_adOperationTimeout = adOperationTimeout;
			this.m_maximumTimeToLive = maximumCacheTimeout;
		}

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x000C5EBC File Offset: 0x000C40BC
		public static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ActiveManagerClientTracer;
			}
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x000C5EC3 File Offset: 0x000C40C3
		// (set) Token: 0x06003043 RID: 12355 RVA: 0x000C5ECB File Offset: 0x000C40CB
		public IADToplogyConfigurationSession AdSession { get; set; }

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x000C5ED4 File Offset: 0x000C40D4
		// (set) Token: 0x06003045 RID: 12357 RVA: 0x000C5EDC File Offset: 0x000C40DC
		public bool MinimizeObjects { get; set; }

		// Token: 0x06003046 RID: 12358 RVA: 0x000C5EE8 File Offset: 0x000C40E8
		public void Clear()
		{
			bool flag = false;
			try
			{
				flag = this.m_rwLock.TryEnterWriteLock(this.m_cacheLockTimeout);
				if (flag)
				{
					this.m_cache.Clear();
				}
				else
				{
					MiniServerLookupCache.Tracer.TraceError((long)this.GetHashCode(), "MiniServerLookupCache.Clear cound not clear cache due to lock timeout");
				}
			}
			finally
			{
				if (flag)
				{
					this.m_rwLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000C5F50 File Offset: 0x000C4150
		public IADServer FindMiniServerByFqdn(string serverFqdn)
		{
			string nodeNameFromFqdn = MachineName.GetNodeNameFromFqdn(serverFqdn);
			return this.FindMiniServerByShortName(nodeNameFromFqdn);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000C5F6C File Offset: 0x000C416C
		public IADServer FindMiniServerByShortName(string shortName)
		{
			Exception ex;
			return this.FindMiniServerByShortNameEx(shortName, out ex);
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000C6048 File Offset: 0x000C4248
		public IADServer FindMiniServerByShortNameEx(string shortName, out Exception ex)
		{
			Exception tempEx = null;
			IADServer result = this.LookupOrFindMiniServer(shortName, delegate
			{
				IADServer result = null;
				tempEx = ADUtils.RunADOperation(delegate()
				{
					result = this.AdSession.FindMiniServerByName(shortName);
				}, 2);
				if (tempEx != null)
				{
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ErrorActiveManagerClientADError, tempEx.Message, new object[0]);
					MiniServerLookupCache.Tracer.TraceDebug<Exception>((long)this.GetHashCode(), "MiniServerLookupCache.FindMiniServerByFqdn got an exception: {0}", tempEx);
				}
				return result;
			});
			ex = tempEx;
			return result;
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000C6128 File Offset: 0x000C4328
		public IADServer ReadMiniServerByObjectId(ADObjectId serverId)
		{
			string name = serverId.Name;
			return this.LookupOrFindMiniServer(name, delegate
			{
				IADServer result = null;
				Exception ex = ADUtils.RunADOperation(delegate()
				{
					result = this.AdSession.ReadMiniServer(serverId);
				}, 2);
				if (ex != null)
				{
					MiniServerLookupCache.Tracer.TraceDebug<Exception>((long)this.GetHashCode(), "MiniServerLookupCache.ReadMiniServerByObjectId got an exception: {0}", ex);
				}
				return result;
			});
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000C6168 File Offset: 0x000C4368
		internal static ActiveManagerClientPerfmonInstance GetPerfCounters()
		{
			ActiveManagerClientPerfmonInstance instance;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				string instanceName = string.Format("{0} - {1}", currentProcess.ProcessName, currentProcess.Id);
				instance = ActiveManagerClientPerfmon.GetInstance(instanceName);
			}
			return instance;
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000C61BC File Offset: 0x000C43BC
		private static bool ShouldExpireCacheEntry(MiniServerCacheEntry entry)
		{
			return DateTime.UtcNow.CompareTo(entry.TimeToExpire) > 0;
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000C61E4 File Offset: 0x000C43E4
		private static bool MaximumTimeToLiveExpired(MiniServerCacheEntry entry)
		{
			return DateTime.UtcNow.CompareTo(entry.MaximumTimeToExpire) > 0;
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000C6234 File Offset: 0x000C4434
		private IADServer LookupOrFindMiniServer(string serverShortName, FindMiniServerCacheFailure serverLookup)
		{
			ExTraceGlobals.ActiveManagerClientTracer.TraceFunction<string>(0L, "LookupOrFindMiniServer({0})", serverShortName);
			MiniServerCacheEntry miniServerCacheEntry = null;
			bool flag = false;
			bool flag2 = false;
			try
			{
				flag = this.m_rwLock.TryEnterReadLock(MiniServerLookupCache.CacheLockTimeout);
				if (flag)
				{
					bool flag3 = this.m_cache.TryGetValue(serverShortName, out miniServerCacheEntry);
					bool flag4 = false;
					if (flag3)
					{
						flag4 = MiniServerLookupCache.ShouldExpireCacheEntry(miniServerCacheEntry);
						ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<string, MiniServerCacheEntry, bool>((long)this.GetHashCode(), "LookupOrFindMiniServer( {0} ) was found in the cache: {1}, and shouldExpireEntry={2}", serverShortName, miniServerCacheEntry, flag4);
					}
					if (!flag3 || flag4)
					{
						flag2 = true;
						MiniServerLookupCache.m_perfCounters.GetServerForDatabaseClientServerInformationCacheMisses.Increment();
					}
					else
					{
						MiniServerLookupCache.m_perfCounters.GetServerForDatabaseClientServerInformationCacheHits.Increment();
					}
				}
				else
				{
					MiniServerLookupCache.Tracer.TraceError((long)this.GetHashCode(), "Timeout waiting for the read lock in MiniServerLookupCache.LookupOrFindAdObject()");
					flag2 = true;
				}
			}
			finally
			{
				if (flag)
				{
					this.m_rwLock.ExitReadLock();
				}
			}
			if (flag2)
			{
				bool flag5 = false;
				IADServer updatedServer = null;
				try
				{
					InvokeWithTimeout.Invoke(delegate()
					{
						updatedServer = serverLookup();
					}, this.m_adOperationTimeout);
				}
				catch (TimeoutException ex)
				{
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ErrorActiveManagerClientADTimeout, serverShortName, new object[]
					{
						serverShortName,
						this.m_adOperationTimeout
					});
					MiniServerLookupCache.Tracer.TraceError<string>((long)this.GetHashCode(), "Timeout on ad query: {0}", ex.Message);
					flag5 = true;
				}
				if (updatedServer != null && this.MinimizeObjects)
				{
					updatedServer.Minimize();
				}
				miniServerCacheEntry = new MiniServerCacheEntry(updatedServer, this.m_timeToLive, this.m_timeToNegativeLive, this.m_maximumTimeToLive);
				bool flag6 = false;
				try
				{
					flag6 = this.m_rwLock.TryEnterWriteLock(MiniServerLookupCache.CacheLockTimeout);
					if (flag6)
					{
						if (updatedServer == null && flag5)
						{
							MiniServerCacheEntry miniServerCacheEntry2 = null;
							bool flag7 = this.m_cache.TryGetValue(serverShortName, out miniServerCacheEntry2);
							if (flag7 && !MiniServerLookupCache.MaximumTimeToLiveExpired(miniServerCacheEntry2))
							{
								miniServerCacheEntry = miniServerCacheEntry2;
								ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<IADServer, MiniServerCacheEntry>((long)this.GetHashCode(), "New ad object was not found, but found possibly stale result '{0}' in the cache as {1}.", miniServerCacheEntry.MiniServerData, miniServerCacheEntry);
							}
							else
							{
								flag5 = false;
							}
						}
						if (!flag5)
						{
							this.m_cache[serverShortName] = miniServerCacheEntry;
							ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<IADServer, MiniServerCacheEntry>((long)this.GetHashCode(), "Stored server '{0}' in the cache as {1}.", miniServerCacheEntry.MiniServerData, miniServerCacheEntry);
						}
					}
					else
					{
						MiniServerLookupCache.Tracer.TraceError((long)this.GetHashCode(), "Timeout waiting for write lock in MiniServerLookupCache.LookupOrFindAdObject()");
					}
				}
				finally
				{
					if (flag6)
					{
						this.m_rwLock.ExitWriteLock();
					}
				}
			}
			MiniServerLookupCache.m_perfCounters.GetServerForDatabaseClientServerInformationCacheEntries.RawValue = (long)this.m_cache.Count;
			return miniServerCacheEntry.MiniServerData;
		}

		// Token: 0x04001A3E RID: 6718
		private readonly TimeSpan m_cacheLockTimeout;

		// Token: 0x04001A3F RID: 6719
		private readonly TimeSpan m_adOperationTimeout;

		// Token: 0x04001A40 RID: 6720
		internal static readonly TimeSpan CacheLockTimeout = TimeSpan.FromSeconds(60.0);

		// Token: 0x04001A41 RID: 6721
		internal static readonly TimeSpan AdOperationTimeout = TimeSpan.FromSeconds(60.0);

		// Token: 0x04001A42 RID: 6722
		internal static readonly TimeSpan TimeToLive = new TimeSpan(0, 20, 0);

		// Token: 0x04001A43 RID: 6723
		internal static readonly TimeSpan TimeToNegativeLive = new TimeSpan(0, 1, 0);

		// Token: 0x04001A44 RID: 6724
		internal static readonly TimeSpan MaximumTimeToLive = new TimeSpan(0, 30, 0);

		// Token: 0x04001A45 RID: 6725
		private readonly TimeSpan m_maximumTimeToLive;

		// Token: 0x04001A46 RID: 6726
		private readonly TimeSpan m_timeToLive;

		// Token: 0x04001A47 RID: 6727
		private readonly TimeSpan m_timeToNegativeLive;

		// Token: 0x04001A48 RID: 6728
		private static readonly ActiveManagerClientPerfmonInstance m_perfCounters = MiniServerLookupCache.GetPerfCounters();

		// Token: 0x04001A49 RID: 6729
		private Dictionary<string, MiniServerCacheEntry> m_cache = new Dictionary<string, MiniServerCacheEntry>(8, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001A4A RID: 6730
		[NonSerialized]
		private ReaderWriterLockSlim m_rwLock = new ReaderWriterLockSlim();
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage.ActiveManager.Performance;
using Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.Threading;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x020002FE RID: 766
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ActiveManager : IDisposeTrackable, IDisposable
	{
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x0008B36F File Offset: 0x0008956F
		public static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ActiveManagerClientTracer;
			}
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0008B378 File Offset: 0x00089578
		public static ActiveManager GetNoncachingActiveManagerInstance()
		{
			if (ActiveManager.s_uncachedActiveManager == null)
			{
				lock (ActiveManager.s_singletonLock)
				{
					if (ActiveManager.s_uncachedActiveManager == null)
					{
						ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1023, "GetNoncachingActiveManagerInstance", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ActiveManager\\ActiveManager.cs");
						IADToplogyConfigurationSession adSession = ADSessionFactory.CreateWrapper(topologyConfigurationSession);
						ActiveManager.s_uncachedActiveManager = new ActiveManager(false, new SimpleAdObjectLookup<IADDatabaseAvailabilityGroup>(adSession), new SimpleAdObjectLookup<IADServer>(adSession), new SimpleMiniServerLookup(adSession), new SimpleAdObjectLookup<IADClientAccessArray>(adSession), new SimpleMiniClientAccessServerOrArrayLookup(topologyConfigurationSession), new SimpleAdObjectLookup<IADDatabase>(adSession), topologyConfigurationSession, false);
						ActiveManager.s_uncachedActiveManager.SuppressDisposeTracker();
					}
				}
			}
			return ActiveManager.s_uncachedActiveManager;
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0008B42C File Offset: 0x0008962C
		public static ActiveManager CreateRemoteActiveManager(string domainControllerName, NetworkCredential networkCredential, bool createCachingActiveManager)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainControllerName, true, ConsistencyMode.FullyConsistent, networkCredential, ADSessionSettings.FromRootOrgScopeSet(), 1071, "CreateRemoteActiveManager", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ActiveManager\\ActiveManager.cs");
			IADToplogyConfigurationSession adSession = ADSessionFactory.CreateWrapper(topologyConfigurationSession);
			IFindAdObject<IADDatabaseAvailabilityGroup> dagLookup;
			IFindAdObject<IADServer> serverLookup;
			IFindMiniServer miniServerLookup;
			IFindAdObject<IADClientAccessArray> casLookup;
			IFindMiniClientAccessServerOrArray miniCasArrayLookup;
			IFindAdObject<IADDatabase> databaseLookup;
			if (createCachingActiveManager)
			{
				TimeSpan timeSpan = new TimeSpan(0, 0, 75);
				dagLookup = new AdObjectLookupCache<IADDatabaseAvailabilityGroup>(adSession, timeSpan, timeSpan, ActiveManager.s_cacheLockTimeout, AdObjectLookupCache<IADDatabaseAvailabilityGroup>.AdOperationTimeout);
				serverLookup = new AdObjectLookupCache<IADServer>(adSession, AdObjectLookupCache<IADServer>.TimeToLive, AdObjectLookupCache<IADServer>.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, AdObjectLookupCache<IADServer>.AdOperationTimeout);
				miniServerLookup = new MiniServerLookupCache(adSession, MiniServerLookupCache.TimeToLive, MiniServerLookupCache.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, MiniServerLookupCache.AdOperationTimeout);
				casLookup = new AdObjectLookupCache<IADClientAccessArray>(adSession, AdObjectLookupCache<IADClientAccessArray>.TimeToLive, AdObjectLookupCache<IADClientAccessArray>.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, AdObjectLookupCache<IADClientAccessArray>.AdOperationTimeout);
				miniCasArrayLookup = new MiniClientAccessServerOrArrayLookupCache(topologyConfigurationSession, MiniClientAccessServerOrArrayLookupCache.TimeToLive, MiniClientAccessServerOrArrayLookupCache.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, MiniClientAccessServerOrArrayLookupCache.AdOperationTimeout);
				databaseLookup = new AdObjectLookupCache<IADDatabase>(adSession, AdObjectLookupCache<IADDatabase>.TimeToLive, AdObjectLookupCache<IADDatabase>.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, AdObjectLookupCache<IADDatabase>.AdOperationTimeout);
			}
			else
			{
				dagLookup = new SimpleAdObjectLookup<IADDatabaseAvailabilityGroup>(adSession);
				serverLookup = new SimpleAdObjectLookup<IADServer>(adSession);
				miniServerLookup = new SimpleMiniServerLookup(adSession);
				casLookup = new SimpleAdObjectLookup<IADClientAccessArray>(adSession);
				miniCasArrayLookup = new SimpleMiniClientAccessServerOrArrayLookup(topologyConfigurationSession);
				databaseLookup = new SimpleAdObjectLookup<IADDatabase>(adSession);
			}
			ActiveManager activeManager = new ActiveManager(createCachingActiveManager, dagLookup, serverLookup, miniServerLookup, casLookup, miniCasArrayLookup, databaseLookup, topologyConfigurationSession, false);
			activeManager.m_networkCredential = networkCredential;
			ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<string>(0L, "Created an ActiveManager suitable for cross-forest queries, dcName={0}.", domainControllerName);
			return activeManager;
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0008B57C File Offset: 0x0008977C
		public static ActiveManager CreateCustomActiveManager(bool cacheRpcRequests, IFindAdObject<IADDatabaseAvailabilityGroup> dagLookup, IFindAdObject<IADServer> serverLookup, IFindMiniServer miniServerLookup, IFindAdObject<IADClientAccessArray> casLookup, IFindMiniClientAccessServerOrArray miniCasArrayLookup, IFindAdObject<IADDatabase> databaseLookup, ITopologyConfigurationSession adSession, bool isService)
		{
			if (adSession == null)
			{
				adSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1200, "CreateCustomActiveManager", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ActiveManager\\ActiveManager.cs");
			}
			IADToplogyConfigurationSession adSession2 = ADSessionFactory.CreateWrapper(adSession);
			dagLookup = (dagLookup ?? new SimpleAdObjectLookup<IADDatabaseAvailabilityGroup>(adSession2));
			serverLookup = (serverLookup ?? new SimpleAdObjectLookup<IADServer>(adSession2));
			miniServerLookup = (miniServerLookup ?? new SimpleMiniServerLookup(adSession2));
			casLookup = (casLookup ?? new SimpleAdObjectLookup<IADClientAccessArray>(adSession2));
			miniCasArrayLookup = (miniCasArrayLookup ?? new SimpleMiniClientAccessServerOrArrayLookup(adSession));
			databaseLookup = (databaseLookup ?? new SimpleAdObjectLookup<IADDatabase>(adSession2));
			return new ActiveManager(cacheRpcRequests, dagLookup, serverLookup, miniServerLookup, casLookup, miniCasArrayLookup, databaseLookup, adSession, isService);
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0008B61D File Offset: 0x0008981D
		public static ActiveManager GetActiveManagerInstance()
		{
			return ActiveManager.GetNoncachingActiveManagerInstance();
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x0008B624 File Offset: 0x00089824
		public static ActiveManager GetCachingActiveManagerInstance()
		{
			if (ActiveManager.s_cachedActiveManager == null)
			{
				lock (ActiveManager.s_singletonLock)
				{
					if (ActiveManager.s_cachedActiveManager == null)
					{
						ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1240, "GetCachingActiveManagerInstance", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ActiveManager\\ActiveManager.cs");
						IADToplogyConfigurationSession adSession = ADSessionFactory.CreateWrapper(topologyConfigurationSession);
						TimeSpan timeSpan = new TimeSpan(0, 0, 45);
						ActiveManager.s_cachedActiveManager = new ActiveManager(true, new AdObjectLookupCache<IADDatabaseAvailabilityGroup>(adSession, timeSpan, timeSpan, ActiveManager.s_cacheLockTimeout, AdObjectLookupCache<IADDatabaseAvailabilityGroup>.AdOperationTimeout), new AdObjectLookupCache<IADServer>(adSession, AdObjectLookupCache<IADServer>.TimeToLive, AdObjectLookupCache<IADServer>.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, AdObjectLookupCache<IADServer>.AdOperationTimeout), new MiniServerLookupCache(adSession, MiniServerLookupCache.TimeToLive, MiniServerLookupCache.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, MiniServerLookupCache.AdOperationTimeout), new AdObjectLookupCache<IADClientAccessArray>(adSession, AdObjectLookupCache<IADClientAccessArray>.TimeToLive, AdObjectLookupCache<IADClientAccessArray>.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, AdObjectLookupCache<IADClientAccessArray>.AdOperationTimeout), new MiniClientAccessServerOrArrayLookupCache(topologyConfigurationSession, MiniClientAccessServerOrArrayLookupCache.TimeToLive, MiniClientAccessServerOrArrayLookupCache.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, MiniClientAccessServerOrArrayLookupCache.AdOperationTimeout), new AdObjectLookupCache<IADDatabase>(adSession, AdObjectLookupCache<IADDatabase>.TimeToLive, AdObjectLookupCache<IADDatabase>.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, AdObjectLookupCache<IADDatabase>.AdOperationTimeout), topologyConfigurationSession, false);
						ActiveManager.s_cachedActiveManager.SuppressDisposeTracker();
					}
				}
			}
			return ActiveManager.s_cachedActiveManager;
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0008B758 File Offset: 0x00089958
		public static ActiveManagerOperationResult TryGetCachedServerForDatabaseBasic(Guid mdbGuid, out DatabaseLocationInfo databaseLocationInfo)
		{
			return ActiveManager.TryGetCachedServerForDatabaseBasic(mdbGuid, GetServerForDatabaseFlags.IgnoreAdSiteBoundary | GetServerForDatabaseFlags.BasicQuery, out databaseLocationInfo);
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0008B764 File Offset: 0x00089964
		public static ActiveManagerOperationResult TryGetCachedServerForDatabaseBasic(Guid mdbGuid, GetServerForDatabaseFlags gsfdFlags, out DatabaseLocationInfo databaseLocationInfo)
		{
			Exception ex = null;
			databaseLocationInfo = null;
			ActiveManager cachingActiveManagerInstance = ActiveManager.GetCachingActiveManagerInstance();
			try
			{
				databaseLocationInfo = cachingActiveManagerInstance.GetServerForDatabase(mdbGuid, gsfdFlags, NullPerformanceDataLogger.Instance);
				return new ActiveManagerOperationResult(true, ex);
			}
			catch (DatabaseNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (ObjectNotFoundException ex3)
			{
				ex = ex3;
			}
			catch (StoragePermanentException ex4)
			{
				ex = ex4;
			}
			catch (StorageTransientException ex5)
			{
				ex = ex5;
			}
			catch (ServerForDatabaseNotFoundException ex6)
			{
				ex = ex6;
			}
			return new ActiveManagerOperationResult(false, ex);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0008B800 File Offset: 0x00089A00
		private static IADDatabase GetDatabaseByGuid(IFindAdObject<IADDatabase> databaseLookup, Guid databaseId)
		{
			return ActiveManager.GetDatabaseByGuidEx(databaseLookup, databaseId, AdObjectLookupFlags.None, NullPerformanceDataLogger.Instance);
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x0008B810 File Offset: 0x00089A10
		private static IADDatabase GetDatabaseByGuidEx(IFindAdObject<IADDatabase> databaseLookup, Guid databaseId, AdObjectLookupFlags flags, IPerformanceDataLogger perfLogger)
		{
			IADDatabase iaddatabase = databaseLookup.FindAdObjectByGuidEx(databaseId, flags, perfLogger);
			if (iaddatabase == null)
			{
				throw new DatabaseNotFoundException(databaseId.ToString());
			}
			return iaddatabase;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0008B83E File Offset: 0x00089A3E
		private IADDatabase GetDatabaseByGuidEx(Guid databaseId, AdObjectLookupFlags flags, IPerformanceDataLogger perfLogger)
		{
			return ActiveManager.GetDatabaseByGuidEx(this.m_databaseLookup, databaseId, flags, perfLogger);
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x0008B850 File Offset: 0x00089A50
		private void EnableMinimization()
		{
			if (ADObjectWrapperBase.IsMinimizeEnabled())
			{
				AdObjectLookupCache<IADDatabase> adObjectLookupCache = this.m_databaseLookup as AdObjectLookupCache<IADDatabase>;
				if (adObjectLookupCache != null)
				{
					adObjectLookupCache.MinimizeObjects = true;
				}
				MiniServerLookupCache miniServerLookupCache = this.m_miniServerLookup as MiniServerLookupCache;
				if (miniServerLookupCache != null)
				{
					miniServerLookupCache.MinimizeObjects = true;
				}
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x0008B890 File Offset: 0x00089A90
		private ActiveManager(bool enableRpcCaching, IFindAdObject<IADDatabaseAvailabilityGroup> dagLookup, IFindAdObject<IADServer> serverLookup, IFindMiniServer miniServerLookup, IFindAdObject<IADClientAccessArray> casLookup, IFindMiniClientAccessServerOrArray miniCasArrayLookup, IFindAdObject<IADDatabase> databaseLookup, ITopologyConfigurationSession adSession, bool isService)
		{
			ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<bool>((long)this.GetHashCode(), "Active Manager is instantiated (cacheEnabled={0})", enableRpcCaching);
			if (adSession == null)
			{
				throw new ArgumentNullException("adSession");
			}
			IADToplogyConfigurationSession adSession2 = ADSessionFactory.CreateWrapper(adSession);
			this.m_isCacheEnabled = enableRpcCaching;
			this.m_adSession = adSession;
			this.m_disposeTracker = this.GetDisposeTracker();
			if (this.m_isCacheEnabled)
			{
				this.m_dagLookup = (dagLookup ?? new AdObjectLookupCache<IADDatabaseAvailabilityGroup>(adSession2, AdObjectLookupCache<IADDatabaseAvailabilityGroup>.TimeToLive, AdObjectLookupCache<IADDatabaseAvailabilityGroup>.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, AdObjectLookupCache<IADDatabaseAvailabilityGroup>.AdOperationTimeout));
				this.m_casLookup = (casLookup ?? new AdObjectLookupCache<IADClientAccessArray>(adSession2, AdObjectLookupCache<IADClientAccessArray>.TimeToLive, AdObjectLookupCache<IADClientAccessArray>.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, AdObjectLookupCache<IADClientAccessArray>.AdOperationTimeout));
				this.m_miniServerLookup = (miniServerLookup ?? new MiniServerLookupCache(adSession2, MiniServerLookupCache.TimeToLive, MiniServerLookupCache.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, MiniServerLookupCache.AdOperationTimeout));
				this.m_miniCasArrayLookup = (miniCasArrayLookup ?? new MiniClientAccessServerOrArrayLookupCache(this.m_adSession, MiniClientAccessServerOrArrayLookupCache.TimeToLive, MiniClientAccessServerOrArrayLookupCache.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, MiniClientAccessServerOrArrayLookupCache.AdOperationTimeout));
				this.m_databaseLookup = (databaseLookup ?? new AdObjectLookupCache<IADDatabase>(adSession2, AdObjectLookupCache<IADDatabase>.TimeToLive, AdObjectLookupCache<IADDatabase>.TimeToNegativeLive, ActiveManager.s_cacheLockTimeout, AdObjectLookupCache<IADDatabase>.AdOperationTimeout));
				if (!isService)
				{
					this.EnableMinimization();
				}
			}
			else
			{
				this.m_dagLookup = (dagLookup ?? new SimpleAdObjectLookup<IADDatabaseAvailabilityGroup>(adSession2));
				this.m_casLookup = (casLookup ?? new SimpleAdObjectLookup<IADClientAccessArray>(adSession2));
				this.m_miniServerLookup = (miniServerLookup ?? new SimpleMiniServerLookup(adSession2));
				this.m_miniCasArrayLookup = (miniCasArrayLookup ?? new SimpleMiniClientAccessServerOrArrayLookup(this.m_adSession));
				this.m_databaseLookup = (databaseLookup ?? new SimpleAdObjectLookup<IADDatabase>(adSession2));
			}
			this.CacheUpdateInterval = new TimeSpan(0, 0, 60);
			this.m_isRunningInService = isService;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0008BA83 File Offset: 0x00089C83
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x0008BA94 File Offset: 0x00089C94
		private void Dispose(bool calledFromDispose)
		{
			if (!this.m_disposed)
			{
				if (calledFromDispose)
				{
					this.m_stopCacheUpdate = true;
					if (this.m_dbCacheUpdateTimer != null)
					{
						this.m_dbCacheUpdateTimer.Dispose(true);
						this.m_dbCacheUpdateTimer = null;
					}
					using (this.m_threadExitEvent)
					{
					}
					this.m_threadExitEvent = null;
					if (ActiveManager.s_wcfClientProxyPool != null)
					{
						ActiveManager.s_wcfClientProxyPool.Clear();
					}
					if (this.m_disposeTracker != null)
					{
						this.m_disposeTracker.Dispose();
					}
					this.m_disposeTracker = null;
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0008BB2C File Offset: 0x00089D2C
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ActiveManager>(this);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0008BB34 File Offset: 0x00089D34
		public void SuppressDisposeTracker()
		{
			if (this.m_disposeTracker != null)
			{
				this.m_disposeTracker.Suppress();
			}
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0008BB49 File Offset: 0x00089D49
		protected void DisposeCheck()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0008BB64 File Offset: 0x00089D64
		public DatabaseLocationInfo GetServerForDatabase(Guid databaseId)
		{
			return this.GetServerForDatabase(databaseId, GetServerForDatabaseFlags.None, NullPerformanceDataLogger.Instance);
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0008BB74 File Offset: 0x00089D74
		public DatabaseLocationInfo GetServerForDatabase(Guid databaseId, bool ignoreAdSiteBoundary)
		{
			GetServerForDatabaseFlags getServerForDatabaseFlags = GetServerForDatabaseFlags.None;
			if (ignoreAdSiteBoundary)
			{
				getServerForDatabaseFlags |= GetServerForDatabaseFlags.IgnoreAdSiteBoundary;
			}
			return this.GetServerForDatabase(databaseId, getServerForDatabaseFlags, NullPerformanceDataLogger.Instance);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0008BB97 File Offset: 0x00089D97
		public DatabaseLocationInfo GetServerForDatabase(Guid databaseId, GetServerForDatabaseFlags gsfdFlags)
		{
			return this.GetServerForDatabase(databaseId, gsfdFlags, NullPerformanceDataLogger.Instance);
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x0008BBA8 File Offset: 0x00089DA8
		public DatabaseLocationInfo GetServerForDatabase(Guid databaseId, GetServerForDatabaseFlags gsfdFlags, IPerformanceDataLogger perfLogger)
		{
			ExTraceGlobals.ActiveManagerClientTracer.TraceFunction<Guid, int>((long)this.GetHashCode(), "Entering GetServerForDatabase(Guid={0}, Flags={1})", databaseId, (int)gsfdFlags);
			this.DisposeCheck();
			bool flag = (gsfdFlags & GetServerForDatabaseFlags.BasicQuery) != GetServerForDatabaseFlags.None;
			bool flag2 = !flag;
			IADDatabase database;
			DatabaseLocationInfo databaseLocationInfo;
			using (new StopwatchPerformanceTracker("GetServerForDatabaseGetServerNameForDatabase", perfLogger))
			{
				databaseLocationInfo = this.GetServerNameForDatabase(databaseId, gsfdFlags, perfLogger, out database);
			}
			if (databaseLocationInfo == null || string.IsNullOrEmpty(databaseLocationInfo.ServerFqdn))
			{
				ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Performing a BasicQuery for Database {0} failed! Falling back to a full query, which performs AD operations", databaseId);
				flag2 = true;
			}
			if (flag2)
			{
				using (new StopwatchPerformanceTracker("GetServerForDatabaseGetServerInformationForDatabase", perfLogger))
				{
					databaseLocationInfo = this.GetServerInformationForDatabase(databaseId, database, databaseLocationInfo, gsfdFlags, perfLogger);
				}
			}
			return databaseLocationInfo;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x0008BC80 File Offset: 0x00089E80
		private DatabaseLocationInfo GetServerNameForDatabase(Guid databaseId, GetServerForDatabaseFlags gsfdFlags, IPerformanceDataLogger perfLogger, out IADDatabase database)
		{
			ExTraceGlobals.ActiveManagerClientTracer.TraceFunction<Guid, GetServerForDatabaseFlags>((long)this.GetHashCode(), "Entering GetServerNameForDatabase. databaseId = {0}, gsfdFlags = {1}", databaseId, gsfdFlags);
			this.DisposeCheck();
			database = null;
			DatabaseLocationInfo databaseLocationInfo;
			if (!this.GetDbLocationInfoByRegistry(databaseId, out databaseLocationInfo))
			{
				bool throwOnErrors = (gsfdFlags & GetServerForDatabaseFlags.ThrowServerForDatabaseNotFoundException) != GetServerForDatabaseFlags.None;
				bool flag = (gsfdFlags & GetServerForDatabaseFlags.ReadThrough) == GetServerForDatabaseFlags.ReadThrough;
				if (flag)
				{
					this.m_perfCounters.GetServerForDatabaseClientCallsWithReadThrough.Increment();
				}
				if (this.m_isCacheEnabled && !flag)
				{
					databaseLocationInfo = this.m_dbCache.Find(databaseId);
					if (databaseLocationInfo == null)
					{
						ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Database {0} not in the cache.", databaseId);
						this.m_perfCounters.GetServerForDatabaseClientCacheMisses.Increment();
					}
					else
					{
						this.m_perfCounters.GetServerForDatabaseClientCacheHits.Increment();
					}
				}
				this.m_perfCounters.GetServerForDatabaseClientCalls.Increment();
				this.m_perfCounters.GetServerForDatabaseClientCallsPerSec.Increment();
				lock (this.m_uniqueDatabasesSeen)
				{
					this.m_uniqueDatabasesSeen.Add(databaseId);
					try
					{
						this.m_perfCounters.GetServerForDatabaseClientUniqueDatabases.RawValue = (long)this.m_uniqueDatabasesSeen.Count;
					}
					catch (InvalidOperationException)
					{
						ExTraceGlobals.ActiveManagerClientTracer.TraceError((long)this.GetHashCode(), "Perf counters are broken. Please use lodctr to add them back");
					}
				}
				if (databaseLocationInfo == null)
				{
					using (new StopwatchPerformanceTracker("GetServerNameForDatabaseGetDatabaseByGuidEx", perfLogger))
					{
						database = this.GetDatabaseByGuidEx(databaseId, flag ? AdObjectLookupFlags.ReadThrough : AdObjectLookupFlags.None, perfLogger);
					}
					using (new StopwatchPerformanceTracker("GetServerNameForDatabaseLookupDatabaseAndPossiblyPopulateCache", perfLogger))
					{
						databaseLocationInfo = this.LookupDatabaseAndPossiblyPopulateCache(database, throwOnErrors);
					}
				}
				ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, DatabaseLocationInfo>((long)this.GetHashCode(), "Database Location Info ({0}) = {1}", databaseId, databaseLocationInfo);
			}
			lock (this.m_uniqueServersSeen)
			{
				if (databaseLocationInfo != null && databaseLocationInfo.ServerFqdn != null)
				{
					this.m_uniqueServersSeen.Add(databaseLocationInfo.ServerFqdn);
				}
				try
				{
					this.m_perfCounters.GetServerForDatabaseClientUniqueServers.RawValue = (long)this.m_uniqueServersSeen.Count;
				}
				catch (InvalidOperationException)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceError((long)this.GetHashCode(), "Perf counters are broken. Please use lodctr to add them back");
				}
			}
			ExTraceGlobals.ActiveManagerClientTracer.TraceFunction<Guid>((long)this.GetHashCode(), "Exiting GetServerNameForDatabase. databaseId = {0}", databaseId);
			return databaseLocationInfo;
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x0008BF08 File Offset: 0x0008A108
		private DatabaseLocationInfo GetServerInformationForDatabase(Guid databaseId, IADDatabase database, DatabaseLocationInfo dbLocationInfo, GetServerForDatabaseFlags gsfdFlags, IPerformanceDataLogger perfLogger)
		{
			if (database != null && databaseId != database.Guid)
			{
				throw new ArgumentException("When passing in database, its GUID must match databaseId.", "database");
			}
			ExTraceGlobals.ActiveManagerClientTracer.TraceFunction<Guid, DatabaseLocationInfo, int>((long)this.GetHashCode(), "Entering GetServerInformationForDatabase(Guid={0}, minimalLocationInfo={1}, Flags={2})", databaseId, dbLocationInfo, (int)gsfdFlags);
			this.DisposeCheck();
			bool flag = (gsfdFlags & GetServerForDatabaseFlags.IgnoreAdSiteBoundary) != GetServerForDatabaseFlags.None;
			bool flag2 = (gsfdFlags & GetServerForDatabaseFlags.ReadThrough) != GetServerForDatabaseFlags.None;
			if (flag2 || dbLocationInfo.ServerLegacyDN == null)
			{
				if (database == null)
				{
					AdObjectLookupFlags flags = flag2 ? AdObjectLookupFlags.ReadThrough : AdObjectLookupFlags.None;
					using (new StopwatchPerformanceTracker("GetServerInformationForDatabaseGetDatabaseByGuidEx", perfLogger))
					{
						database = this.GetDatabaseByGuidEx(databaseId, flags, perfLogger);
					}
				}
				ActiveManagerImplementation.GetServerInformationForDatabaseInternal(database, dbLocationInfo, this.m_miniServerLookup);
			}
			if (dbLocationInfo != null)
			{
				DatabaseLocationInfoResult databaseLocationInfoResult = dbLocationInfo.RequestResult;
				bool flag3 = false;
				if (flag && databaseLocationInfoResult == DatabaseLocationInfoResult.SiteViolation)
				{
					databaseLocationInfoResult = DatabaseLocationInfoResult.Success;
					flag3 = true;
				}
				if (flag && databaseLocationInfoResult == DatabaseLocationInfoResult.InTransitCrossSite)
				{
					databaseLocationInfoResult = DatabaseLocationInfoResult.InTransitSameSite;
					flag3 = true;
				}
				ExTraceGlobals.FaultInjectionTracer.TraceTest<DatabaseLocationInfoResult>(3831901501U, ref databaseLocationInfoResult);
				ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(2221288765U, ref flag3);
				if (flag3)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<DatabaseLocationInfoResult, DatabaseLocationInfoResult>((long)this.GetHashCode(), "GetServerForDatabase(): At the caller's request, changing the location info's result from {0} to {1}.", dbLocationInfo.RequestResult, databaseLocationInfoResult);
					dbLocationInfo = DatabaseLocationInfo.CloneDatabaseLocationInfo(dbLocationInfo, databaseLocationInfoResult);
				}
			}
			if (ExTraceGlobals.ActiveManagerClientTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, DatabaseLocationInfo>((long)this.GetHashCode(), "Database Location Info ({0}) = {1}", databaseId, dbLocationInfo);
			}
			ExTraceGlobals.ActiveManagerClientTracer.TraceFunction<Guid>((long)this.GetHashCode(), "Exiting GetServerInformationForDatabase(Guid={0})", databaseId);
			return dbLocationInfo;
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x0008C078 File Offset: 0x0008A278
		private DatabaseLocationInfo LookupDatabaseAndPossiblyPopulateCache(IADDatabase database, bool throwOnErrors)
		{
			DatabaseLocationInfo databaseLocationInfo = null;
			if (this.m_isCacheEnabled)
			{
				bool flag = false;
				try
				{
					flag = this.m_dbCache.CheckAndSetRPCLock(database.Guid);
					if (flag)
					{
						databaseLocationInfo = ActiveManagerImplementation.GetServerNameForDatabaseInternal(database, this.m_networkCredential, this.m_dagLookup, this.m_miniServerLookup, this.m_perfCounters, throwOnErrors, this.m_isRunningInService);
					}
				}
				finally
				{
					if (flag)
					{
						this.m_dbCache.ReleaseRPCLock(database.Guid);
					}
				}
				if (databaseLocationInfo == null)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceError<ADObjectId>((long)this.GetHashCode(), "DatabaseLocationInfo for Database {0} is null because some other thread is already trying to do RPC.", database.Id);
					if (throwOnErrors)
					{
						throw new ServerForDatabaseNotFoundException(database.Name, database.Guid.ToString(), new RPCOperationAbortedBecauseOfAnotherRPCThreadException());
					}
					databaseLocationInfo = new DatabaseLocationInfo(null, null, null, null, null, null, string.Empty, false, false, Guid.Empty, DateTime.MinValue, null, null, null, MailboxRelease.None, DatabaseLocationInfoResult.Unknown, false);
				}
			}
			else
			{
				databaseLocationInfo = ActiveManagerImplementation.GetServerNameForDatabaseInternal(database, this.m_networkCredential, this.m_dagLookup, this.m_miniServerLookup, this.m_perfCounters, throwOnErrors, this.m_isRunningInService);
			}
			if (databaseLocationInfo != null)
			{
				if (this.m_isCacheEnabled)
				{
					this.m_dbCache.Add(database.Guid, databaseLocationInfo);
					if (!this.m_stopCacheUpdate)
					{
						this.StartOrRefreshPeriodicCacheUpdate();
					}
				}
			}
			else
			{
				ExTraceGlobals.ActiveManagerClientTracer.TraceError<ADObjectId>((long)this.GetHashCode(), "DatabaseLocationInfo for Database {0} is null.", database.Id);
			}
			try
			{
				this.m_perfCounters.GetServerForDatabaseClientLocationCacheEntries.RawValue = (long)this.m_dbCache.Count;
			}
			catch (InvalidOperationException)
			{
				ExTraceGlobals.ActiveManagerClientTracer.TraceError((long)this.GetHashCode(), "Perf counters are broken. Please use lodctr to add them back");
			}
			return databaseLocationInfo;
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x0008C220 File Offset: 0x0008A420
		internal void ClearCache()
		{
			lock (this.locker)
			{
				this.m_dbCache.ForceExpire();
			}
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0008C268 File Offset: 0x0008A468
		public void StartOrRefreshPeriodicCacheUpdate()
		{
			ExTraceGlobals.ActiveManagerClientTracer.TraceDebug((long)this.GetHashCode(), "Setting the flag that starting the cache thread is acceptable.");
			this.DisposeCheck();
			if (!this.m_isCacheEnabled)
			{
				ExTraceGlobals.ActiveManagerClientTracer.TraceDebug((long)this.GetHashCode(), "StartOrRefreshPeriodicCacheUpdate() called on a non-caching AM. Ignoring.");
				return;
			}
			lock (this.locker)
			{
				this.m_stopCacheUpdate = false;
				if (this.m_dbCacheUpdateTimer == null)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug((long)this.GetHashCode(), "Starting the AM cache update thread");
					this.m_dbCacheUpdateTimer = new GuardedTimer(new TimerCallback(this.DatabaseCacheUpdateCallback), null, this.CacheUpdateInterval);
				}
				else
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "StartOrRefreshPeriodicCacheUpdate(): Triggering the cache update thread with a timeout of {0}.", this.CacheUpdateInterval);
					this.m_dbCacheUpdateTimer.Change(new TimeSpan(0L), this.CacheUpdateInterval);
				}
			}
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x0008C358 File Offset: 0x0008A558
		public void SetCacheUpdateInterval(TimeSpan newInterval)
		{
			this.CacheUpdateInterval = newInterval;
			this.StartOrRefreshPeriodicCacheUpdate();
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x0008C368 File Offset: 0x0008A568
		private void DatabaseCacheUpdateCallback(object unusedState)
		{
			this.m_dbChaceUpdateStartTime = DateTime.UtcNow;
			Thread.CurrentPrincipal = null;
			ExTraceGlobals.ActiveManagerClientTracer.TraceDebug((long)this.GetHashCode(), "Entering AM cache update callback");
			Guid[] array = this.m_dbCache.CopyDatabaseGuids();
			Dictionary<Guid, DatabaseLocationInfo> dictionary = new Dictionary<Guid, DatabaseLocationInfo>(array.Length);
			Dictionary<Guid, int> dictionary2 = new Dictionary<Guid, int>(array.Length);
			foreach (Guid guid in array)
			{
				if (this.m_stopCacheUpdate)
				{
					break;
				}
				DatabaseLocationInfo databaseLocationInfo = null;
				int value = 0;
				try
				{
					IADDatabase databaseByGuidEx = this.GetDatabaseByGuidEx(guid, AdObjectLookupFlags.None, NullPerformanceDataLogger.Instance);
					databaseLocationInfo = ActiveManagerImplementation.GetServerNameForDatabaseInternal(databaseByGuidEx, this.m_networkCredential, this.m_dagLookup, this.m_miniServerLookup, this.m_perfCounters, false, this.m_isRunningInService);
					if (ExTraceGlobals.ActiveManagerClientTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, DatabaseLocationInfo>((long)this.GetHashCode(), "(Cache update thread) Database Location Info ({0}) = {1}", guid, databaseLocationInfo);
					}
					value = 2;
				}
				catch (DataValidationException arg)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, DataValidationException>((long)this.GetHashCode(), "Some of the Database {0} properties are invalid (exception: {1})", guid, arg);
				}
				catch (ServerForDatabaseNotFoundException arg2)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, ServerForDatabaseNotFoundException>((long)this.GetHashCode(), "Server for database {0} not found (exception: {1})", guid, arg2);
				}
				catch (ObjectNotFoundException arg3)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, ObjectNotFoundException>((long)this.GetHashCode(), "Database {0} does not exist anymore (exception: {1})", guid, arg3);
				}
				catch (StorageTransientException arg4)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, StorageTransientException>((long)this.GetHashCode(), "GetSFD({0}) generated exception {1}", guid, arg4);
				}
				catch (StoragePermanentException arg5)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, StoragePermanentException>((long)this.GetHashCode(), "GetSFD({0}) generated exception {1}", guid, arg5);
				}
				catch (DataSourceOperationException arg6)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, DataSourceOperationException>((long)this.GetHashCode(), "GetSFD({0}) generated exception {1}", guid, arg6);
				}
				catch (DataSourceTransientException arg7)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, DataSourceTransientException>((long)this.GetHashCode(), "GetSFD({0}) generated exception {1}", guid, arg7);
				}
				catch (AmDatabaseMasterIsInvalid arg8)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, AmDatabaseMasterIsInvalid>((long)this.GetHashCode(), "GetSFD({0}) generated exception {1}", guid, arg8);
				}
				catch (AmDatabaseADException arg9)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, AmDatabaseADException>((long)this.GetHashCode(), "GetSFD({0}) generated exception {1}", guid, arg9);
				}
				dictionary[guid] = databaseLocationInfo;
				dictionary2[guid] = value;
			}
			if (!this.m_stopCacheUpdate)
			{
				this.m_dbCache.Update(dictionary, this.m_cacheExpiryThreshold, dictionary2);
			}
			TimeSpan arg10 = DateTime.UtcNow - this.m_dbChaceUpdateStartTime;
			try
			{
				this.m_perfCounters.CacheUpdateTimeInSec.RawValue = (long)arg10.TotalSeconds;
			}
			catch (InvalidOperationException)
			{
				ExTraceGlobals.ActiveManagerClientTracer.TraceError((long)this.GetHashCode(), "Perf counters are broken. Please use lodctr to add them back");
			}
			ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "Leaving AM cache update callback. Time in callback {0}.", arg10);
			if ((long)arg10.TotalMilliseconds > 120000L)
			{
				ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<TimeSpan, int>((long)this.GetHashCode(), "AM cache update callback took longer than expected. Time in callback {0}. Maximum time is {1} msec. Cache will be cleared as mitigation.", arg10, 120000);
				this.m_dbCache.Clear();
			}
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0008C6AC File Offset: 0x0008A8AC
		private bool GetDbLocationInfoByRegistry(Guid databaseId, out DatabaseLocationInfo databaseLocationInfo)
		{
			databaseLocationInfo = null;
			if (this.m_key == null)
			{
				this.m_key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveManager\\TestOverride", RegistryKeyPermissionCheck.ReadSubTree);
				if (this.m_key == null)
				{
					ExTraceGlobals.ActiveManagerClientTracer.TraceDebug((long)this.GetHashCode(), "GetServerForDatabase failed opening the registry override key!");
					return false;
				}
				ExTraceGlobals.ActiveManagerClientTracer.TraceDebug((long)this.GetHashCode(), "Found registry override entry");
			}
			object value = this.m_key.GetValue(databaseId.ToString());
			if (value == null)
			{
				return false;
			}
			string text = value as string;
			if (text == null)
			{
				throw new ArgumentException("Registry override for active server should be a string-typed value", "regValue");
			}
			string[] array = text.Split(new char[]
			{
				'|'
			});
			int num = array.Length;
			if (num == 3)
			{
				databaseLocationInfo = new DatabaseLocationInfo(array[0], array[1], ActiveManagerUtil.GetServerSiteFromServer(LocalServer.GetServer()), new ServerVersion(int.Parse(array[2], CultureInfo.InvariantCulture)), false);
				return true;
			}
			if (num != 9)
			{
				throw new ArgumentException("Registry override for active server should be in a \"<fqdn>|<legacyDN>|<version>|<lastMountedServerFqdn>|<lastMountedServerLegacyDN>|<databaseLegacyDN>|<mountedTime>|<serverVersion>|<isHA>\" format", "databaseId");
			}
			databaseLocationInfo = new DatabaseLocationInfo(array[0], array[1], array[2], array[3], array[4], array[1], string.Empty, false, false, string.IsNullOrEmpty(array[5]) ? Guid.Empty : new Guid(array[5]), DateTime.Parse(array[6]), null, ActiveManagerUtil.GetServerSiteFromServer(LocalServer.GetServer()), new ServerVersion(int.Parse(array[7], CultureInfo.InvariantCulture)), MailboxRelease.None, DatabaseLocationInfoResult.Success, bool.Parse(array[8]));
			return true;
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0008C820 File Offset: 0x0008AA20
		public void CalculatePreferredHomeServer(Guid databaseGuid, out LegacyDN preferredRpcClientAccessServerLegacyDN, out ADObjectId preferredSiteId)
		{
			using (ActiveManagerPerformanceData.CalculatePreferredHomeServerDataProvider.StartRequestTimer())
			{
				ExTraceGlobals.ActiveManagerClientTracer.TraceFunction<Guid>((long)this.GetHashCode(), "Entering CalculatePreferredHomeServer. databaseGuid = {0}.", databaseGuid);
				this.DisposeCheck();
				this.m_perfCounters.CalculatePreferredHomeServerCalls.Increment();
				this.m_perfCounters.CalculatePreferredHomeServerCallsPerSec.Increment();
				lock (this.m_uniqueDatabasesSeen)
				{
					this.m_uniqueDatabasesSeen.Add(databaseGuid);
					try
					{
						this.m_perfCounters.GetServerForDatabaseClientUniqueDatabases.RawValue = (long)this.m_uniqueDatabasesSeen.Count;
					}
					catch (InvalidOperationException)
					{
						ExTraceGlobals.ActiveManagerClientTracer.TraceError((long)this.GetHashCode(), "Perf counters are broken. Please use lodctr to add them back");
					}
				}
				IADDatabase databaseByGuidEx = this.GetDatabaseByGuidEx(databaseGuid, AdObjectLookupFlags.None, NullPerformanceDataLogger.Instance);
				ActiveManagerImplementation.CalculatePreferredHomeServerInternal(this, databaseByGuidEx, this.m_adSession, this.m_dagLookup, this.m_casLookup, this.m_miniCasArrayLookup, out preferredRpcClientAccessServerLegacyDN, out preferredSiteId);
				ExTraceGlobals.ActiveManagerClientTracer.TraceDebug<Guid, LegacyDN, ADObjectId>((long)this.GetHashCode(), "CalculatePreferredHomeServer. databaseGuid = {0}, preferredRpcClientAccessServerLegacyDN = {1}, preferredSite = {2}", databaseGuid, preferredRpcClientAccessServerLegacyDN, preferredSiteId);
				ExTraceGlobals.ActiveManagerClientTracer.TraceFunction<Guid>((long)this.GetHashCode(), "Exiting CalculatePreferredHomeServer. databaseGuid = {0}", databaseGuid);
			}
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x0008C994 File Offset: 0x0008AB94
		internal static ActiveManagerClientPerfmonInstance GetPerfCounters()
		{
			Process currentProcess = Process.GetCurrentProcess();
			string instanceName = string.Format("{0} - {1}", currentProcess.ProcessName, currentProcess.Id);
			return ActiveManagerClientPerfmon.GetInstance(instanceName);
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x0008C9C9 File Offset: 0x0008ABC9
		// (set) Token: 0x060022BF RID: 8895 RVA: 0x0008C9D1 File Offset: 0x0008ABD1
		internal TimeSpan CacheUpdateInterval { get; private set; }

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x0008C9DA File Offset: 0x0008ABDA
		// (set) Token: 0x060022C1 RID: 8897 RVA: 0x0008C9E2 File Offset: 0x0008ABE2
		internal int CacheExpiryThreshold
		{
			get
			{
				return this.m_cacheExpiryThreshold;
			}
			set
			{
				this.m_cacheExpiryThreshold = value;
			}
		}

		// Token: 0x04001421 RID: 5153
		private const string RegistryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveManager\\TestOverride";

		// Token: 0x04001422 RID: 5154
		private const int CacheNegativeExpiryThreshold = 2;

		// Token: 0x04001423 RID: 5155
		private static readonly TimeSpan s_cacheLockTimeout = TimeSpan.FromSeconds(15.0);

		// Token: 0x04001424 RID: 5156
		private static readonly object s_singletonLock = new object();

		// Token: 0x04001425 RID: 5157
		private readonly ActiveManagerClientPerfmonInstance m_perfCounters = ActiveManager.GetPerfCounters();

		// Token: 0x04001426 RID: 5158
		private readonly bool m_isRunningInService;

		// Token: 0x04001427 RID: 5159
		private static ActiveManager s_uncachedActiveManager;

		// Token: 0x04001428 RID: 5160
		private static ActiveManager s_cachedActiveManager;

		// Token: 0x04001429 RID: 5161
		internal static WcfProxyObjectsPool s_wcfClientProxyPool = new WcfProxyObjectsPool();

		// Token: 0x0400142A RID: 5162
		public static readonly ADPropertyDefinition[] PropertiesNeededForDatabase = new ADPropertyDefinition[]
		{
			ADObjectSchema.ObjectClass,
			DatabaseSchema.MasterServerOrAvailabilityGroup,
			DatabaseSchema.Server,
			DatabaseSchema.ExchangeLegacyDN,
			DatabaseSchema.RpcClientAccessServerExchangeLegacyDN,
			DatabaseSchema.MailboxPublicFolderDatabase,
			DatabaseSchema.IsExchange2009OrLater,
			DatabaseSchema.Recovery
		};

		// Token: 0x0400142B RID: 5163
		private bool m_disposed;

		// Token: 0x0400142C RID: 5164
		private DisposeTracker m_disposeTracker;

		// Token: 0x0400142D RID: 5165
		private bool m_stopCacheUpdate;

		// Token: 0x0400142E RID: 5166
		private bool m_isCacheEnabled;

		// Token: 0x0400142F RID: 5167
		private int m_cacheExpiryThreshold = 4;

		// Token: 0x04001430 RID: 5168
		private DateTime m_dbChaceUpdateStartTime = DateTime.MinValue;

		// Token: 0x04001431 RID: 5169
		private DatabaseLocationCache m_dbCache = new DatabaseLocationCache();

		// Token: 0x04001432 RID: 5170
		private HashSet<Guid> m_uniqueDatabasesSeen = new HashSet<Guid>();

		// Token: 0x04001433 RID: 5171
		private HashSet<string> m_uniqueServersSeen = new HashSet<string>();

		// Token: 0x04001434 RID: 5172
		private IFindMiniServer m_miniServerLookup;

		// Token: 0x04001435 RID: 5173
		private IFindAdObject<IADDatabaseAvailabilityGroup> m_dagLookup;

		// Token: 0x04001436 RID: 5174
		private IFindAdObject<IADClientAccessArray> m_casLookup;

		// Token: 0x04001437 RID: 5175
		private IFindMiniClientAccessServerOrArray m_miniCasArrayLookup;

		// Token: 0x04001438 RID: 5176
		private IFindAdObject<IADDatabase> m_databaseLookup;

		// Token: 0x04001439 RID: 5177
		private AutoResetEvent m_threadExitEvent;

		// Token: 0x0400143A RID: 5178
		private GuardedTimer m_dbCacheUpdateTimer;

		// Token: 0x0400143B RID: 5179
		private RegistryKey m_key;

		// Token: 0x0400143C RID: 5180
		private object locker = new object();

		// Token: 0x0400143D RID: 5181
		private NetworkCredential m_networkCredential;

		// Token: 0x0400143E RID: 5182
		private ITopologyConfigurationSession m_adSession;
	}
}

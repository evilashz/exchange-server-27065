using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D61 RID: 3425
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ServiceCache
	{
		// Token: 0x0600765E RID: 30302 RVA: 0x0020A7CC File Offset: 0x002089CC
		private ServiceCache()
		{
			this.cachePresentEvent = new ManualResetEvent(false);
			this.semaphore = new Semaphore(1, 1);
			this.stopTimerEvent = new AutoResetEvent(false);
			this.notificationHandler = new ADNotificationHandler();
			this.isFirstLoad = true;
			this.dropCacheOnInactivity = true;
		}

		// Token: 0x17001FBD RID: 8125
		// (get) Token: 0x0600765F RID: 30303 RVA: 0x0020A81D File Offset: 0x00208A1D
		// (set) Token: 0x06007660 RID: 30304 RVA: 0x0020A82E File Offset: 0x00208A2E
		internal static ExchangeTopologyScope Scope
		{
			get
			{
				return ServiceCache.Instance.notificationHandler.Scope;
			}
			set
			{
				ServiceCache.Instance.notificationHandler.Scope = value;
			}
		}

		// Token: 0x06007661 RID: 30305 RVA: 0x0020A840 File Offset: 0x00208A40
		internal static ServiceTopology GetCurrentLegacyServiceTopology()
		{
			return ServiceCache.GetCurrentLegacyServiceTopology(ServiceDiscovery.ExchangeTopologyBridge.GetServiceTopologyDefaultTimeout);
		}

		// Token: 0x06007662 RID: 30306 RVA: 0x0020A854 File Offset: 0x00208A54
		internal static ServiceTopology GetCurrentLegacyServiceTopology(TimeSpan getServiceTopologyTimeout)
		{
			ServiceTopology currentServiceTopology = ServiceCache.GetCurrentServiceTopology(getServiceTopologyTimeout);
			ServiceTopology serviceTopology = ServiceCache.Instance.legacyServiceTopologyInstance;
			if (serviceTopology == null)
			{
				serviceTopology = currentServiceTopology.ToLegacyServiceTopology("f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceCache.cs", "GetCurrentLegacyServiceTopology", 144);
				ServiceCache.Instance.legacyServiceTopologyInstance = serviceTopology;
			}
			return serviceTopology;
		}

		// Token: 0x06007663 RID: 30307 RVA: 0x0020A898 File Offset: 0x00208A98
		internal static ServiceTopology GetCurrentServiceTopology()
		{
			return ServiceCache.GetCurrentServiceTopology(ServiceDiscovery.ExchangeTopologyBridge.GetServiceTopologyDefaultTimeout);
		}

		// Token: 0x06007664 RID: 30308 RVA: 0x0020A8AC File Offset: 0x00208AAC
		internal static ServiceTopology GetCurrentServiceTopology(TimeSpan getServiceTopologyTimeout)
		{
			ServiceCache instance = ServiceCache.Instance;
			instance.DropCacheIfNeeded();
			instance.TriggerCacheRefreshIfNeeded();
			ServiceTopology serviceTopology = instance.serviceTopologyInstance;
			if (serviceTopology == null)
			{
				if (!instance.cachePresentEvent.WaitOne(getServiceTopologyTimeout, false))
				{
					throw new ReadTopologyTimeoutException(ServerStrings.ExReadTopologyTimeout);
				}
				serviceTopology = instance.serviceTopologyInstance;
			}
			serviceTopology.IncrementRequestCount("f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceCache.cs", "GetCurrentServiceTopology", 188);
			return serviceTopology;
		}

		// Token: 0x06007665 RID: 30309 RVA: 0x0020A913 File Offset: 0x00208B13
		internal static void TriggerCacheRefreshDueToNotification()
		{
			ServiceCache.Instance.TriggerCacheRefresh(ServiceCache.CachePopulateReason.TopologyChangeNotification, null);
		}

		// Token: 0x06007666 RID: 30310 RVA: 0x0020A921 File Offset: 0x00208B21
		internal static void TriggerCacheRefreshDueToCacheMiss(ServiceTopology requestingTopology)
		{
			Util.ThrowOnNullArgument(requestingTopology, "requestingTopology");
			ServiceCache.Instance.TriggerCacheRefresh(ServiceCache.CachePopulateReason.CacheMissDetected, requestingTopology);
		}

		// Token: 0x06007667 RID: 30311 RVA: 0x0020A93C File Offset: 0x00208B3C
		internal static void Purge()
		{
			ServiceCache instance = ServiceCache.Instance;
			instance.semaphore.WaitOne();
			try
			{
				instance.DropCache();
				instance.isFirstLoad = true;
				instance.notificationHandler.UnRegisterExchangeTopologyNotification();
				instance.dropCacheOnInactivity = false;
				if (!(ServiceDiscovery.ExchangeTopologyBridge is ExchangeTopologyBridge))
				{
					instance.notificationHandler.RegisterExchangeTopologyNotificationIfNeeded();
				}
			}
			finally
			{
				instance.semaphore.Release();
			}
		}

		// Token: 0x06007668 RID: 30312 RVA: 0x0020A9B0 File Offset: 0x00208BB0
		private void TriggerCacheRefresh(ServiceCache.CachePopulateReason reason, ServiceTopology requestingTopology)
		{
			if (requestingTopology != null)
			{
				ServiceTopology serviceTopology = this.serviceTopologyInstance;
				if (requestingTopology != serviceTopology)
				{
					return;
				}
				if (serviceTopology != null && serviceTopology.CreationTime + ServiceDiscovery.ExchangeTopologyBridge.MinExpirationTimeForCacheDueToCacheMiss > ExDateTime.UtcNow)
				{
					return;
				}
			}
			bool flag = false;
			bool flag2 = this.semaphore.WaitOne(0, false);
			try
			{
				if (flag2 && (requestingTopology == null || requestingTopology == this.serviceTopologyInstance))
				{
					this.StopCacheRefreshTimer();
					this.DropCacheIfNeeded();
					using (ActivityContext.SuppressThreadScope())
					{
						flag = ThreadPool.QueueUserWorkItem(new WaitCallback(this.PopulateServiceCache), reason);
					}
				}
			}
			finally
			{
				if (flag2 && !flag)
				{
					this.semaphore.Release();
				}
			}
		}

		// Token: 0x06007669 RID: 30313 RVA: 0x0020AA78 File Offset: 0x00208C78
		private void TriggerCacheRefreshIfNeeded()
		{
			if (this.serviceTopologyInstance == null)
			{
				this.TriggerCacheRefresh(ServiceCache.CachePopulateReason.CacheNotPresent, null);
			}
		}

		// Token: 0x0600766A RID: 30314 RVA: 0x0020AA98 File Offset: 0x00208C98
		private void DropCacheIfNeeded()
		{
			ServiceTopology serviceTopology = this.serviceTopologyInstance;
			ServiceTopology serviceTopology2 = this.legacyServiceTopologyInstance;
			ExDateTime utcNow = ExDateTime.UtcNow;
			ExDateTime exDateTime = ExDateTime.MaxValue;
			if (serviceTopology != null)
			{
				exDateTime = serviceTopology.CreationTime;
			}
			if (serviceTopology2 != null && serviceTopology2.CreationTime < exDateTime)
			{
				exDateTime = serviceTopology2.CreationTime;
			}
			if (exDateTime != ExDateTime.MaxValue && utcNow - exDateTime > ServiceDiscovery.ExchangeTopologyBridge.CacheExpirationTimeout)
			{
				this.DropCache();
			}
		}

		// Token: 0x0600766B RID: 30315 RVA: 0x0020AB0C File Offset: 0x00208D0C
		private void DropCache()
		{
			this.serviceTopologyInstance = null;
			this.legacyServiceTopologyInstance = null;
			this.cachePresentEvent.Reset();
			ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug(0L, "ServiceCache::DropCache. Cached service topology instance was dropped");
		}

		// Token: 0x0600766C RID: 30316 RVA: 0x0020AB39 File Offset: 0x00208D39
		private void StartCacheRefreshTimer()
		{
			this.cacheTimerRegisteredWaitHandle = ThreadPool.RegisterWaitForSingleObject(this.stopTimerEvent, new WaitOrTimerCallback(this.TimerCallback), null, ServiceDiscovery.ExchangeTopologyBridge.CacheTimerRefreshTimeout, true);
		}

		// Token: 0x0600766D RID: 30317 RVA: 0x0020AB64 File Offset: 0x00208D64
		private void StopCacheRefreshTimer()
		{
			if (this.cacheTimerRegisteredWaitHandle != null)
			{
				this.cacheTimerRegisteredWaitHandle.Unregister(null);
			}
		}

		// Token: 0x0600766E RID: 30318 RVA: 0x0020AB7C File Offset: 0x00208D7C
		private void PopulateServiceCache(object obj)
		{
			ServiceCache.CachePopulateReason cachePopulateReason = (ServiceCache.CachePopulateReason)obj;
			Exception ex = null;
			try
			{
				try
				{
					DateTime dateTime = (cachePopulateReason == ServiceCache.CachePopulateReason.CacheMissDetected || this.serviceTopologyInstance == null) ? DateTime.MinValue : this.serviceTopologyInstance.DiscoveryStarted;
					bool forceRefresh = false;
					if (this.dropCacheOnInactivity && dateTime != DateTime.MinValue && this.serviceTopologyInstance.TopologyRequestCount <= 1)
					{
						this.DropCache();
						return;
					}
					if (cachePopulateReason == ServiceCache.CachePopulateReason.CacheMissDetected || (cachePopulateReason == ServiceCache.CachePopulateReason.CacheNotPresent && this.isFirstLoad))
					{
						forceRefresh = true;
					}
					ExchangeTopology exchangeTopology = ServiceDiscovery.ExchangeTopologyBridge.ReadExchangeTopology(dateTime, ExchangeTopologyScope.Complete, forceRefresh);
					if (exchangeTopology != null)
					{
						this.serviceTopologyInstance = new ServiceTopology(exchangeTopology, "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ServiceDiscovery\\ServiceCache.cs", "PopulateServiceCache", 426);
					}
					else if (this.serviceTopologyInstance != null)
					{
						this.serviceTopologyInstance.CreationTime = ExDateTime.Now;
					}
					this.isFirstLoad = false;
					if (this.serviceTopologyInstance != null)
					{
						this.cachePresentEvent.Set();
					}
					else
					{
						ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug(0L, "ServiceCache::ServiceDiscovery.ExchangeTopologyBridge.ReadExchangeTopology returned null. This is unexpected");
					}
				}
				catch (ServiceDiscoveryPermanentException ex2)
				{
					ex = ex2;
				}
				catch (ServiceDiscoveryTransientException ex3)
				{
					ex = ex3;
				}
				this.StartCacheRefreshTimer();
			}
			finally
			{
				this.semaphore.Release();
			}
			if (ex == null)
			{
				ExTraceGlobals.ServiceDiscoveryTracer.TraceDebug<ServiceCache.CachePopulateReason>(0L, "ServiceCache::PopulateServiceCache. Successfully populated ServiceTopology. Reason to populate cache = {0}", cachePopulateReason);
				ProcessInfoEventLogger.Log(StorageEventLogConstants.Tuple_PopulatedServiceTopology, null, new object[]
				{
					cachePopulateReason
				});
				return;
			}
			string text = ex.ToString().TruncateToUseInEventLog();
			ExTraceGlobals.ServiceDiscoveryTracer.TraceError<ServiceCache.CachePopulateReason, string>(0L, "ServiceCache::PopulateServiceCache. Failed to populate a ServiceTopology. Reason to populate cache = {0}. Error = {1}.", cachePopulateReason, text);
			ProcessInfoEventLogger.Log(StorageEventLogConstants.Tuple_ErrorPopulatingServiceTopology, null, new object[]
			{
				cachePopulateReason,
				text
			});
		}

		// Token: 0x0600766F RID: 30319 RVA: 0x0020AD34 File Offset: 0x00208F34
		private void TimerCallback(object state, bool timedOut)
		{
			if (timedOut)
			{
				this.TriggerCacheRefresh(ServiceCache.CachePopulateReason.RefreshTimeout, null);
			}
		}

		// Token: 0x04005215 RID: 21013
		private static readonly ServiceCache Instance = new ServiceCache();

		// Token: 0x04005216 RID: 21014
		private readonly ManualResetEvent cachePresentEvent;

		// Token: 0x04005217 RID: 21015
		private readonly Semaphore semaphore;

		// Token: 0x04005218 RID: 21016
		private readonly AutoResetEvent stopTimerEvent;

		// Token: 0x04005219 RID: 21017
		private readonly ADNotificationHandler notificationHandler;

		// Token: 0x0400521A RID: 21018
		private RegisteredWaitHandle cacheTimerRegisteredWaitHandle;

		// Token: 0x0400521B RID: 21019
		private ServiceTopology serviceTopologyInstance;

		// Token: 0x0400521C RID: 21020
		private ServiceTopology legacyServiceTopologyInstance;

		// Token: 0x0400521D RID: 21021
		private bool isFirstLoad;

		// Token: 0x0400521E RID: 21022
		private bool dropCacheOnInactivity;

		// Token: 0x02000D62 RID: 3426
		internal enum CachePopulateReason
		{
			// Token: 0x04005220 RID: 21024
			CacheNotPresent,
			// Token: 0x04005221 RID: 21025
			RefreshTimeout,
			// Token: 0x04005222 RID: 21026
			TopologyChangeNotification,
			// Token: 0x04005223 RID: 21027
			CacheMissDetected
		}
	}
}

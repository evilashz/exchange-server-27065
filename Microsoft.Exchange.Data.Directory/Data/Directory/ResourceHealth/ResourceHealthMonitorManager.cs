using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x020009FF RID: 2559
	internal class ResourceHealthMonitorManager : IDisposable
	{
		// Token: 0x06007674 RID: 30324 RVA: 0x00185F4E File Offset: 0x0018414E
		static ResourceHealthMonitorManager()
		{
			ResourceHealthMonitorManager.Active = false;
		}

		// Token: 0x06007675 RID: 30325 RVA: 0x00185F8C File Offset: 0x0018418C
		private static void CheckRegistryActive()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ResourceHealth", false))
				{
					if (registryKey == null)
					{
						ResourceHealthMonitorManager.Active = false;
					}
					else
					{
						object value = registryKey.GetValue(ResourceHealthMonitorManager.resourceHealthComponent.ToString());
						if (value == null || !(value is int))
						{
							ResourceHealthMonitorManager.Active = false;
						}
						else
						{
							ResourceHealthMonitorManager.Active = ((int)value != 0);
						}
					}
				}
			}
			catch (SecurityException)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<ResourceHealthComponent>(0L, "[ResourceHealthMonitorManager::CheckRegistryActive] Security exception encountered while retrieving {0} registry value.", ResourceHealthMonitorManager.resourceHealthComponent);
			}
			catch (UnauthorizedAccessException)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceError<ResourceHealthComponent>(0L, "[ResourceHealthMonitorManagaer::CheckRegistryActive] Security exception encountered while retrieving {0} registry value", ResourceHealthMonitorManager.resourceHealthComponent);
			}
		}

		// Token: 0x06007676 RID: 30326 RVA: 0x00186058 File Offset: 0x00184258
		public static void Initialize(ResourceHealthComponent resourceHealthComponent)
		{
			if (resourceHealthComponent == ResourceHealthComponent.None)
			{
				throw new ArgumentException("You should not use ResourceHealthComponent.None when initializing the ResourceHealthMonitorManager.");
			}
			if (!ResourceHealthMonitorManager.Initialized)
			{
				ResourceHealthMonitorManager.resourceHealthComponent = resourceHealthComponent;
				ResourceHealthMonitorManager.CheckRegistryActive();
				return;
			}
			if (resourceHealthComponent != ResourceHealthMonitorManager.resourceHealthComponent)
			{
				throw new InvalidOperationException("ResourceHealthMonitorManager was already initialized with budget type: " + resourceHealthComponent);
			}
		}

		// Token: 0x17002A68 RID: 10856
		// (get) Token: 0x06007677 RID: 30327 RVA: 0x001860A4 File Offset: 0x001842A4
		public static bool Initialized
		{
			get
			{
				return ResourceHealthMonitorManager.resourceHealthComponent != ResourceHealthComponent.None;
			}
		}

		// Token: 0x17002A69 RID: 10857
		// (get) Token: 0x06007678 RID: 30328 RVA: 0x001860B1 File Offset: 0x001842B1
		public static ResourceHealthMonitorManager Singleton
		{
			get
			{
				return ResourceHealthMonitorManager.singleton;
			}
		}

		// Token: 0x17002A6A RID: 10858
		// (get) Token: 0x06007679 RID: 30329 RVA: 0x001860B8 File Offset: 0x001842B8
		internal List<ResourceKey> ResourceKeys
		{
			get
			{
				return this.monitors.Keys;
			}
		}

		// Token: 0x0600767A RID: 30330 RVA: 0x001860C8 File Offset: 0x001842C8
		private ResourceHealthMonitorManager()
		{
			this.monitors = new ExactTimeoutCache<ResourceKey, CacheableResourceHealthMonitor>(new RemoveItemDelegate<ResourceKey, CacheableResourceHealthMonitor>(this.HandleMonitorRemove), new ShouldRemoveDelegate<ResourceKey, CacheableResourceHealthMonitor>(this.HandleResourceCacheShouldRemove), new UnhandledExceptionDelegate(this.HandleTimeoutCacheWorkerException), 100000, true);
			this.pollingMonitors = new ExactTimeoutCache<ResourceKey, IResourceHealthPoller>(null, new ShouldRemoveDelegate<ResourceKey, IResourceHealthPoller>(this.HandlePollingItemShouldRemove), new UnhandledExceptionDelegate(this.HandleTimeoutCacheWorkerException), 100000, false);
		}

		// Token: 0x0600767B RID: 30331 RVA: 0x00186150 File Offset: 0x00184350
		~ResourceHealthMonitorManager()
		{
			this.Dispose(false);
		}

		// Token: 0x0600767C RID: 30332 RVA: 0x00186180 File Offset: 0x00184380
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600767D RID: 30333 RVA: 0x00186190 File Offset: 0x00184390
		private void Dispose(bool fromDispose)
		{
			if (!this.disposed && fromDispose)
			{
				lock (this.instanceLock)
				{
					this.monitors.Dispose();
					this.monitors = null;
					this.pollingMonitors.Dispose();
					this.pollingMonitors = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x17002A6B RID: 10859
		// (get) Token: 0x0600767E RID: 30334 RVA: 0x00186200 File Offset: 0x00184400
		// (set) Token: 0x0600767F RID: 30335 RVA: 0x00186207 File Offset: 0x00184407
		public static bool Active { get; private set; }

		// Token: 0x06007680 RID: 30336 RVA: 0x0018620F File Offset: 0x0018440F
		internal static void SetActiveForTest(bool active)
		{
			ResourceHealthMonitorManager.Active = active;
		}

		// Token: 0x06007681 RID: 30337 RVA: 0x00186217 File Offset: 0x00184417
		internal static void UpdateActiveFromRegistry()
		{
			if (!ResourceHealthMonitorManager.Initialized)
			{
				throw new InvalidOperationException("ResourceHealthMonitorManager must first be initialized.");
			}
			ResourceHealthMonitorManager.CheckRegistryActive();
		}

		// Token: 0x17002A6C RID: 10860
		// (get) Token: 0x06007682 RID: 30338 RVA: 0x00186230 File Offset: 0x00184430
		// (set) Token: 0x06007683 RID: 30339 RVA: 0x00186237 File Offset: 0x00184437
		internal static TimeSpan MonitorSlidingExpiration { get; set; } = TimeSpan.FromMinutes(5.0);

		// Token: 0x17002A6D RID: 10861
		// (get) Token: 0x06007684 RID: 30340 RVA: 0x0018623F File Offset: 0x0018443F
		// (set) Token: 0x06007685 RID: 30341 RVA: 0x00186246 File Offset: 0x00184446
		internal static TimeSpan DummyMonitorAbsoluteExpiration { get; set; } = TimeSpan.FromMinutes(1.0);

		// Token: 0x06007686 RID: 30342 RVA: 0x00186284 File Offset: 0x00184484
		public IResourceLoadMonitor Get(ResourceKey key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			CacheableResourceHealthMonitor cacheableResourceHealthMonitor = null;
			if (!this.monitors.TryGetValue(key, out cacheableResourceHealthMonitor))
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = true;
				CacheableResourceHealthMonitor cacheableResourceHealthMonitor2 = null;
				try
				{
					if (ResourceHealthMonitorManager.Active && ExTraceGlobals.ResourceHealthManagerTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						StackTrace arg = new StackTrace();
						ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceHealthComponent, ResourceKey, StackTrace>((long)this.GetHashCode(), "[ResourceHealthMonitorManager.Get] Creating new monitor for component '{0}'.  Resource Identity: {1}, Stack Trace: {2}", ResourceHealthMonitorManager.resourceHealthComponent, key, arg);
					}
					cacheableResourceHealthMonitor2 = ((ResourceHealthMonitorManager.Active && key.MetricType != ResourceMetricType.None) ? key.CreateMonitor() : new DummyResourceHealthMonitor(key));
					lock (this.instanceLock)
					{
						if (!this.monitors.TryGetValue(key, out cacheableResourceHealthMonitor))
						{
							flag3 = false;
							flag2 = true;
							if (cacheableResourceHealthMonitor2 is DummyResourceHealthMonitor)
							{
								ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "[ResourceHealthMonitorManager.Get] Dummy monitor created for resource key {0}", key);
								this.monitors.TryAddAbsolute(key, cacheableResourceHealthMonitor2, ResourceHealthMonitorManager.DummyMonitorAbsoluteExpiration);
							}
							else
							{
								this.monitors.TryAddAbsolute(key, cacheableResourceHealthMonitor2, ResourceHealthMonitorManager.MonitorSlidingExpiration);
							}
							cacheableResourceHealthMonitor = cacheableResourceHealthMonitor2;
							flag = true;
						}
					}
				}
				finally
				{
					if (flag3 && cacheableResourceHealthMonitor2 != null)
					{
						cacheableResourceHealthMonitor2.Expire();
						IDisposable disposable = cacheableResourceHealthMonitor2 as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
						cacheableResourceHealthMonitor2 = null;
					}
				}
				if (flag2)
				{
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "[ResourceHealthMonitorManager::Get] Cache miss for key '{0}'", key);
				}
				if (flag)
				{
					IResourceHealthPoller pollHealth = cacheableResourceHealthMonitor as IResourceHealthPoller;
					if (pollHealth != null)
					{
						using (ActivityContext.SuppressThreadScope())
						{
							ThreadPool.QueueUserWorkItem(delegate(object state)
							{
								this.HandlePollingItemShouldRemove(key, pollHealth);
							});
						}
						ExTraceGlobals.ClientThrottlingTracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "[ResourceHealthMonitorManager::Get] Adding monitor as polling monitor: {0}", key);
						this.pollingMonitors.TryAddAbsolute(key, pollHealth, pollHealth.Interval);
					}
				}
			}
			return cacheableResourceHealthMonitor.CreateWrapper();
		}

		// Token: 0x06007687 RID: 30343 RVA: 0x00186528 File Offset: 0x00184728
		public void Remove(ResourceKey resourceKey)
		{
			lock (this.instanceLock)
			{
				this.monitors.Remove(resourceKey);
				this.pollingMonitors.Remove(resourceKey);
			}
		}

		// Token: 0x06007688 RID: 30344 RVA: 0x0018657C File Offset: 0x0018477C
		internal void Clear()
		{
			lock (this.instanceLock)
			{
				this.monitors.Clear();
				this.pollingMonitors.Clear();
			}
		}

		// Token: 0x06007689 RID: 30345 RVA: 0x001865CC File Offset: 0x001847CC
		internal bool IsCached(ResourceKey resourceKey)
		{
			return this.monitors.Contains(resourceKey);
		}

		// Token: 0x0600768A RID: 30346 RVA: 0x001865DC File Offset: 0x001847DC
		private bool HandleResourceCacheShouldRemove(ResourceKey key, CacheableResourceHealthMonitor monitor)
		{
			bool flag = false;
			bool result;
			try
			{
				if (monitor is IResourceHealthPoller)
				{
					if (!Monitor.TryEnter(monitor, 0))
					{
						ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "[ResourceHealthMonitorManager.HandleResourceCacheShouldRemove] Could not grab the monitor - extending life in cache for key: {0}", key);
						return false;
					}
					flag = true;
				}
				bool flag2 = true;
				if (monitor != null)
				{
					flag2 = (DateTime.UtcNow - monitor.LastAccessUtc > ResourceHealthMonitorManager.MonitorSlidingExpiration && monitor.ShouldRemoveResourceFromCache());
				}
				if (flag2)
				{
					this.pollingMonitors.Remove(key);
					monitor.Expire();
				}
				result = flag2;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(monitor);
				}
			}
			return result;
		}

		// Token: 0x0600768B RID: 30347 RVA: 0x0018667C File Offset: 0x0018487C
		private void HandleTimeoutCacheWorkerException(Exception unhandledException)
		{
			ExTraceGlobals.ResourceHealthManagerTracer.TraceError<Exception>((long)this.GetHashCode(), "[ResourceHealthMonitorManager.HandleTimeoutCacheWorkerException] Encountered exception on timeout cache worker thread.  Exception: {0}", unhandledException);
			if (!(unhandledException is ThreadAbortException) && !(unhandledException is AppDomainUnloadedException))
			{
				ExWatson.SendReport(unhandledException);
			}
		}

		// Token: 0x0600768C RID: 30348 RVA: 0x001866AC File Offset: 0x001848AC
		private bool HandlePollingItemShouldRemove(ResourceKey key, IResourceHealthPoller value)
		{
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "[ResourceHealthMonitorManager.HandlePollingItemShouldRemove] Firing poll for monitor {0}", key);
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).WorkloadManagement.GetObject<IResourceSettings>(key.MetricType, new object[0]).Enabled)
			{
				return false;
			}
			CacheableResourceHealthMonitor cacheableResourceHealthMonitor = value as CacheableResourceHealthMonitor;
			if (cacheableResourceHealthMonitor.Expired)
			{
				ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "[ResourceHealthMonitorManager.HandlePollingItemShouldRemove] Monitor has already expired - will not poll for key: {0}", key);
				return false;
			}
			bool flag = false;
			try
			{
				if (!Monitor.TryEnter(value, 0))
				{
					ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceKey>((long)this.GetHashCode(), "[ResourceHealthMonitorManager.HandlePollingItemShouldRemove] Could not obtain lock.  Will not poll for key: {0}", key);
					return false;
				}
				flag = true;
				if (cacheableResourceHealthMonitor.Expired)
				{
					return false;
				}
				if (!value.IsActive)
				{
					return false;
				}
				bool flag2 = false;
				try
				{
					lock (this.instanceLock)
					{
						if (!this.outstandingPollCalls.ContainsKey(key))
						{
							this.outstandingPollCalls[key] = true;
							flag2 = true;
						}
					}
					if (flag2)
					{
						try
						{
							value.Execute();
						}
						catch (DataSourceTransientException ex)
						{
							ExTraceGlobals.ResourceHealthManagerTracer.TraceError<ResourceKey, string>((long)this.GetHashCode(), "[ResourceHealthMonitorManager.HandlePollingItemShouldRemove] Failed to set metrics for monitor {0}. Exception: {1}", key, ex.Message);
							return false;
						}
					}
				}
				finally
				{
					if (flag2)
					{
						lock (this.instanceLock)
						{
							this.outstandingPollCalls.Remove(key);
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(value);
				}
			}
			return false;
		}

		// Token: 0x0600768D RID: 30349 RVA: 0x0018686C File Offset: 0x00184A6C
		private void HandleMonitorRemove(ResourceKey key, CacheableResourceHealthMonitor value, RemoveReason reason)
		{
			ExTraceGlobals.ResourceHealthManagerTracer.TraceDebug<ResourceKey, RemoveReason>((long)this.GetHashCode(), "[ResourceHealthMonitorManager.HandleMonitorRemove] Removing monitor {0} due to reason {1}", key, reason);
			if (!value.Expired)
			{
				value.Expire();
			}
			IDisposable disposable = value as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x04004BD8 RID: 19416
		public const string MSExchangeResourceHealthRegistryPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange ResourceHealth";

		// Token: 0x04004BD9 RID: 19417
		private static ResourceHealthComponent resourceHealthComponent = ResourceHealthComponent.None;

		// Token: 0x04004BDA RID: 19418
		private static ResourceHealthMonitorManager singleton = new ResourceHealthMonitorManager();

		// Token: 0x04004BDB RID: 19419
		private ExactTimeoutCache<ResourceKey, CacheableResourceHealthMonitor> monitors;

		// Token: 0x04004BDC RID: 19420
		private ExactTimeoutCache<ResourceKey, IResourceHealthPoller> pollingMonitors;

		// Token: 0x04004BDD RID: 19421
		private object instanceLock = new object();

		// Token: 0x04004BDE RID: 19422
		private bool disposed;

		// Token: 0x04004BDF RID: 19423
		private Dictionary<ResourceKey, bool> outstandingPollCalls = new Dictionary<ResourceKey, bool>();
	}
}

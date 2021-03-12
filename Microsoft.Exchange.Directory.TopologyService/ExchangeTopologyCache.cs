using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.Configuration;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200000A RID: 10
	internal class ExchangeTopologyCache
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00003778 File Offset: 0x00001978
		static ExchangeTopologyCache()
		{
			int num = 4;
			ExchangeTopologyCache.instances = new ExchangeTopologyCache[num];
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003792 File Offset: 0x00001992
		internal ExchangeTopologyCache(ExchangeTopologyScope scope)
		{
			this.scope = scope;
			this.refreshLock = new object();
			this.stopTimerEvent = new AutoResetEvent(false);
			this.Refresh();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000037BE File Offset: 0x000019BE
		internal ExchangeTopologyCache.SerializedTopology Topology
		{
			get
			{
				return this.topology;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000037C8 File Offset: 0x000019C8
		internal static ExchangeTopologyCache Get(ExchangeTopologyScope scope)
		{
			ExchangeTopologyCache exchangeTopologyCache = ExchangeTopologyCache.instances[(int)scope];
			if (exchangeTopologyCache == null)
			{
				lock (ExchangeTopologyCache.instances)
				{
					exchangeTopologyCache = ExchangeTopologyCache.instances[(int)scope];
					TimeSpan timeSpan;
					TimeSpan timeSpan2;
					if (exchangeTopologyCache == null && ExchangeTopologyCache.GetConfig(scope, out timeSpan, out timeSpan2))
					{
						exchangeTopologyCache = new ExchangeTopologyCache(scope);
						ExchangeTopologyCache.instances[(int)scope] = exchangeTopologyCache;
					}
				}
				if (exchangeTopologyCache == null)
				{
					exchangeTopologyCache = ExchangeTopologyCache.instances[0];
				}
			}
			return exchangeTopologyCache;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003844 File Offset: 0x00001A44
		internal void Refresh()
		{
			DateTime utcNow = DateTime.UtcNow;
			lock (this.refreshLock)
			{
				if (this.topology == null || this.lastRefreshStart < utcNow)
				{
					this.lastRefreshStart = DateTime.UtcNow;
					this.RefreshNoLock();
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000038AC File Offset: 0x00001AAC
		private static bool GetConfig(ExchangeTopologyScope scope, out TimeSpan cacheLifetime, out TimeSpan cacheFrequency)
		{
			cacheLifetime = TimeSpan.Zero;
			cacheFrequency = TimeSpan.Zero;
			char[] separator = new char[]
			{
				','
			};
			string[] array = ConfigurationData.Instance.ExchangeTopologyCacheLifetime.Split(separator);
			string[] array2 = ConfigurationData.Instance.ExchangeTopologyCacheFrequency.Split(separator);
			if (scope >= (ExchangeTopologyScope)array.Length || scope >= (ExchangeTopologyScope)array2.Length)
			{
				return false;
			}
			TimeSpan timeSpan = string.IsNullOrEmpty(array[(int)scope]) ? TimeSpan.Zero : TimeSpan.Parse(array[(int)scope]);
			TimeSpan timeSpan2 = string.IsNullOrEmpty(array2[(int)scope]) ? TimeSpan.Zero : TimeSpan.Parse(array2[(int)scope]);
			if (timeSpan == TimeSpan.Zero || timeSpan2 == TimeSpan.Zero)
			{
				return false;
			}
			cacheLifetime = timeSpan;
			cacheFrequency = timeSpan2;
			return true;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003978 File Offset: 0x00001B78
		private void RefreshNoLock()
		{
			this.StopCacheRefreshTimer();
			TimeSpan t;
			TimeSpan timeout;
			if (!ExchangeTopologyCache.GetConfig(this.scope, out t, out timeout))
			{
				ExchangeTopologyCache.instances[(int)this.scope] = null;
				return;
			}
			try
			{
				ExchangeTopologyDiscovery topologyDiscovery = ExchangeTopologyDiscovery.Create(null, this.scope);
				ExchangeTopologyDiscovery.Simple simple = ExchangeTopologyDiscovery.Simple.CreateFrom(topologyDiscovery);
				if (this.topology == null || !simple.Equals(this.topology.Discovery) || simple.DiscoveryStarted - this.topology.Discovery.DiscoveryStarted > t)
				{
					this.topology = new ExchangeTopologyCache.SerializedTopology(simple);
				}
			}
			catch (Exception arg)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug<Exception>((long)this.GetHashCode(), "Service topology failed. {0}", arg);
			}
			this.StartCacheRefreshTimer(timeout);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003A40 File Offset: 0x00001C40
		private void StartCacheRefreshTimer(TimeSpan timeout)
		{
			this.cacheTimerRegisteredWaitHandle = ThreadPool.RegisterWaitForSingleObject(this.stopTimerEvent, new WaitOrTimerCallback(this.TimerCallback), null, timeout, true);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003A62 File Offset: 0x00001C62
		private void StopCacheRefreshTimer()
		{
			if (this.cacheTimerRegisteredWaitHandle != null)
			{
				this.cacheTimerRegisteredWaitHandle.Unregister(null);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003A79 File Offset: 0x00001C79
		private void TimerCallback(object state, bool timedOut)
		{
			if (timedOut)
			{
				this.Refresh();
			}
		}

		// Token: 0x04000023 RID: 35
		private static readonly ExchangeTopologyCache[] instances;

		// Token: 0x04000024 RID: 36
		private readonly ExchangeTopologyScope scope;

		// Token: 0x04000025 RID: 37
		private readonly AutoResetEvent stopTimerEvent;

		// Token: 0x04000026 RID: 38
		private RegisteredWaitHandle cacheTimerRegisteredWaitHandle;

		// Token: 0x04000027 RID: 39
		private ExchangeTopologyCache.SerializedTopology topology;

		// Token: 0x04000028 RID: 40
		private DateTime lastRefreshStart;

		// Token: 0x04000029 RID: 41
		private object refreshLock;

		// Token: 0x0200000B RID: 11
		internal class SerializedTopology
		{
			// Token: 0x06000060 RID: 96 RVA: 0x00003A84 File Offset: 0x00001C84
			public SerializedTopology(ExchangeTopologyDiscovery.Simple discovery)
			{
				this.Discovery = discovery;
				this.Serialized = discovery.Serialize();
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00003AA0 File Offset: 0x00001CA0
			public byte[][] Get(ExchangeTopologyScope topologyScope)
			{
				return ExchangeTopologyDiscovery.Simple.Rescope(topologyScope, this.Serialized);
			}

			// Token: 0x0400002A RID: 42
			public readonly ExchangeTopologyDiscovery.Simple Discovery;

			// Token: 0x0400002B RID: 43
			public readonly byte[][] Serialized;
		}
	}
}

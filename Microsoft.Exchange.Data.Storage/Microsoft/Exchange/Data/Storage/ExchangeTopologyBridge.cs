using System;
using System.ServiceModel;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D4D RID: 3405
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ExchangeTopologyBridge : IExchangeTopologyBridge
	{
		// Token: 0x17001FA9 RID: 8105
		// (get) Token: 0x06007612 RID: 30226 RVA: 0x00209DE4 File Offset: 0x00207FE4
		public TimeSpan CacheTimerRefreshTimeout
		{
			get
			{
				return ExchangeTopologyBridge.cacheTimerRefreshTimeout;
			}
		}

		// Token: 0x17001FAA RID: 8106
		// (get) Token: 0x06007613 RID: 30227 RVA: 0x00209DEB File Offset: 0x00207FEB
		public TimeSpan CacheExpirationTimeout
		{
			get
			{
				return ExchangeTopologyBridge.cacheExpirationTimeout;
			}
		}

		// Token: 0x17001FAB RID: 8107
		// (get) Token: 0x06007614 RID: 30228 RVA: 0x00209DF2 File Offset: 0x00207FF2
		public TimeSpan GetServiceTopologyDefaultTimeout
		{
			get
			{
				return ExchangeTopologyBridge.getServiceTopologyDefaultTimeout;
			}
		}

		// Token: 0x17001FAC RID: 8108
		// (get) Token: 0x06007615 RID: 30229 RVA: 0x00209DF9 File Offset: 0x00207FF9
		public TimeSpan NotificationDelayTimeout
		{
			get
			{
				return ExchangeTopologyBridge.notificationDelayTimeout;
			}
		}

		// Token: 0x17001FAD RID: 8109
		// (get) Token: 0x06007616 RID: 30230 RVA: 0x00209E00 File Offset: 0x00208000
		public TimeSpan MinExpirationTimeForCacheDueToCacheMiss
		{
			get
			{
				return ExchangeTopologyBridge.minExpirationTimeForCacheDueToCacheMiss;
			}
		}

		// Token: 0x06007617 RID: 30231 RVA: 0x00209E08 File Offset: 0x00208008
		public ExchangeTopology ReadExchangeTopology(DateTime timestamp, ExchangeTopologyScope topologyScope, bool forceRefresh)
		{
			ExchangeTopology result;
			try
			{
				using (TopologyServiceClient topologyServiceClient = TopologyServiceClient.CreateClient("localhost"))
				{
					byte[][] exchangeTopology = topologyServiceClient.GetExchangeTopology(timestamp, topologyScope, forceRefresh);
					if (exchangeTopology == null)
					{
						result = null;
					}
					else
					{
						ExchangeTopologyDiscovery.Simple topology = ExchangeTopologyDiscovery.Simple.Deserialize(exchangeTopology);
						ExchangeTopologyDiscovery topologyDiscovery = ExchangeTopologyDiscovery.Simple.CreateFrom(topology);
						result = ExchangeTopologyDiscovery.Populate(topologyDiscovery);
					}
				}
			}
			catch (DataSourceOperationException innerException)
			{
				ServiceDiscoveryPermanentException ex = new ServiceDiscoveryPermanentException(ServerStrings.ExReadExchangeTopologyFailed, innerException);
				ExTraceGlobals.ServiceDiscoveryTracer.TraceError<ServiceDiscoveryPermanentException>(0L, "ExchangeTopologyBridge::ReadExchangeTopology. Read ExchangeTopology failed. Throwing exception: {0}.", ex);
				throw ex;
			}
			catch (Exception ex2)
			{
				if (!(ex2 is CommunicationException) && !(ex2 is TimeoutException) && !(ex2 is InvalidOperationException))
				{
					throw;
				}
				ServiceDiscoveryTransientException ex3 = new ServiceDiscoveryTransientException(ServerStrings.ExReadExchangeTopologyFailed, ex2);
				ExTraceGlobals.ServiceDiscoveryTracer.TraceError<ServiceDiscoveryTransientException>(0L, "ExchangeTopologyBridge::ReadExchangeTopology. Read ExchangeTopology failed. Throwing exception: {0}.", ex3);
				throw ex3;
			}
			return result;
		}

		// Token: 0x06007618 RID: 30232 RVA: 0x00209F04 File Offset: 0x00208104
		public IRegisteredExchangeTopologyNotification RegisterExchangeTopologyNotification(ADNotificationCallback callback, ExchangeTopologyScope scope)
		{
			return new RegisteredExchangeTopologyNotification(callback, scope);
		}

		// Token: 0x040051D6 RID: 20950
		private static readonly TimeSpan cacheTimerRefreshTimeout = new TimeSpan(0, 15, 0);

		// Token: 0x040051D7 RID: 20951
		private static readonly TimeSpan cacheExpirationTimeout = new TimeSpan(24, 0, 0);

		// Token: 0x040051D8 RID: 20952
		private static readonly TimeSpan getServiceTopologyDefaultTimeout = new TimeSpan(0, 1, 0);

		// Token: 0x040051D9 RID: 20953
		private static readonly TimeSpan notificationDelayTimeout = new TimeSpan(0, 5, 0);

		// Token: 0x040051DA RID: 20954
		private static readonly TimeSpan minExpirationTimeForCacheDueToCacheMiss = new TimeSpan(0, 5, 0);
	}
}

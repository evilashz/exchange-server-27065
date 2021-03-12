using System;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D63 RID: 3427
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ServiceDiscovery
	{
		// Token: 0x17001FBE RID: 8126
		// (get) Token: 0x06007671 RID: 30321 RVA: 0x0020AD4D File Offset: 0x00208F4D
		// (set) Token: 0x06007672 RID: 30322 RVA: 0x0020AD54 File Offset: 0x00208F54
		public static ExchangeTopologyScope ADNotificationScope
		{
			get
			{
				return ServiceCache.Scope;
			}
			set
			{
				ServiceCache.Scope = value;
			}
		}

		// Token: 0x17001FBF RID: 8127
		// (get) Token: 0x06007673 RID: 30323 RVA: 0x0020AD5C File Offset: 0x00208F5C
		internal static IExchangeTopologyBridge ExchangeTopologyBridge
		{
			get
			{
				return ServiceDiscovery.topologyBridge;
			}
		}

		// Token: 0x06007674 RID: 30324 RVA: 0x0020AD63 File Offset: 0x00208F63
		internal static void PurgeServiceCache()
		{
			ServiceCache.Purge();
		}

		// Token: 0x06007675 RID: 30325 RVA: 0x0020AD6A File Offset: 0x00208F6A
		internal static void SetTestExchangeTopologyBridge(IExchangeTopologyBridge testExchangeTopologyBridge)
		{
			ServiceDiscovery.topologyBridge = (testExchangeTopologyBridge ?? new ExchangeTopologyBridge());
			ServiceDiscovery.PurgeServiceCache();
		}

		// Token: 0x04005224 RID: 21028
		private static IExchangeTopologyBridge topologyBridge = new ExchangeTopologyBridge();
	}
}

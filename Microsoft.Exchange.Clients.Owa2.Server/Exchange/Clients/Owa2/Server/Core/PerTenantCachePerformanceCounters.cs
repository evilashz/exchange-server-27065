using System;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Common.Cache;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000AE RID: 174
	internal sealed class PerTenantCachePerformanceCounters : DefaultCachePerformanceCounters
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x00014ECC File Offset: 0x000130CC
		public PerTenantCachePerformanceCounters(string cachePerfCounterInstance)
		{
			if (string.IsNullOrEmpty(cachePerfCounterInstance))
			{
				throw new ArgumentNullException("cachePerfCounterInstance");
			}
			try
			{
				this.perfCounters = ConfigurationCachePerfCounters.GetInstance(cachePerfCounterInstance);
			}
			catch (InvalidOperationException ex)
			{
				OwaDiagnostics.Logger.LogEvent(ClientsEventLogConstants.Tuple_ConfigurationCachePerfCountersLoadFailure, string.Empty, new object[]
				{
					cachePerfCounterInstance,
					ex
				});
				this.perfCounters = null;
			}
			if (this.perfCounters != null)
			{
				base.InitializeCounters(this.perfCounters.Requests, this.perfCounters.HitRatio, this.perfCounters.HitRatio_Base, this.perfCounters.CacheSize);
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00014F7C File Offset: 0x0001317C
		public override void SizeUpdated(long cacheSize)
		{
			base.SizeUpdated(cacheSize);
			if (this.perfCounters != null)
			{
				this.perfCounters.CacheSizeKB.RawValue = cacheSize / 1024L;
			}
		}

		// Token: 0x040003C9 RID: 969
		private const long KB = 1024L;

		// Token: 0x040003CA RID: 970
		private ConfigurationCachePerfCountersInstance perfCounters;
	}
}

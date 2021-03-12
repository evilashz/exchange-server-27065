using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002DE RID: 734
	internal sealed class PerTenantCachePerformanceCounters : DefaultCachePerformanceCounters
	{
		// Token: 0x06002075 RID: 8309 RVA: 0x0007C298 File Offset: 0x0007A498
		public PerTenantCachePerformanceCounters(ProcessTransportRole transportRole, string cachePerfCounterInstance)
		{
			if (cachePerfCounterInstance == null)
			{
				throw new ArgumentNullException("cachePerfCounterInstance");
			}
			try
			{
				ConfigurationCachePerfCounters.SetCategoryName(PerTenantCachePerformanceCounters.perfCounterCategoryMap[transportRole]);
				this.perfCounters = ConfigurationCachePerfCounters.GetInstance(cachePerfCounterInstance);
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.GeneralTracer.TraceError<string, InvalidOperationException>(0L, "Failed to initialize performance counters for component '{0}': {1}", cachePerfCounterInstance, ex);
				ExEventLog exEventLog = new ExEventLog(ExTraceGlobals.GeneralTracer.Category, TransportEventLog.GetEventSource());
				exEventLog.LogEvent(TransportEventLogConstants.Tuple_PerfCountersLoadFailure, null, new object[]
				{
					"Cache",
					cachePerfCounterInstance,
					ex.ToString()
				});
				this.perfCounters = null;
			}
			if (this.perfCounters != null)
			{
				base.InitializeCounters(this.perfCounters.Requests, this.perfCounters.HitRatio, this.perfCounters.HitRatio_Base, this.perfCounters.CacheSize);
			}
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x0007C380 File Offset: 0x0007A580
		public override void SizeUpdated(long cacheSize)
		{
			base.SizeUpdated(cacheSize);
			if (this.perfCounters != null)
			{
				this.perfCounters.CacheSizeMB.RawValue = cacheSize / 1048576L;
			}
		}

		// Token: 0x040010FE RID: 4350
		private const long MB = 1048576L;

		// Token: 0x040010FF RID: 4351
		private const string EventTag = "Cache";

		// Token: 0x04001100 RID: 4352
		private static readonly IDictionary<ProcessTransportRole, string> perfCounterCategoryMap = new Dictionary<ProcessTransportRole, string>
		{
			{
				ProcessTransportRole.Edge,
				"MSExchangeTransport Configuration Cache"
			},
			{
				ProcessTransportRole.Hub,
				"MSExchangeTransport Configuration Cache"
			},
			{
				ProcessTransportRole.FrontEnd,
				"MSExchangeFrontEndTransport Configuration Cache"
			},
			{
				ProcessTransportRole.MailboxDelivery,
				"MSExchange Delivery Configuration Cache"
			},
			{
				ProcessTransportRole.MailboxSubmission,
				"MSExchange Submission Configuration Cache"
			}
		};

		// Token: 0x04001101 RID: 4353
		private ConfigurationCachePerfCountersInstance perfCounters;
	}
}

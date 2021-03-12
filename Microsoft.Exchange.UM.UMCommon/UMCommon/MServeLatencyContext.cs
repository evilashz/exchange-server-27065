using System;
using Microsoft.Exchange.Diagnostics.LatencyDetection;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000C7 RID: 199
	internal class MServeLatencyContext : PerformanceDataProvider
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x00019FAE File Offset: 0x000181AE
		internal MServeLatencyContext() : base("MServe Requests")
		{
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x00019FBB File Offset: 0x000181BB
		internal static MServeLatencyContext Current
		{
			get
			{
				if (MServeLatencyContext.current == null)
				{
					MServeLatencyContext.current = new MServeLatencyContext();
				}
				return MServeLatencyContext.current;
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00019FD4 File Offset: 0x000181D4
		internal static void UpdateContext(uint requestCount, int requestLatency)
		{
			MServeLatencyContext mserveLatencyContext = MServeLatencyContext.Current;
			mserveLatencyContext.RequestCount += requestCount;
			mserveLatencyContext.Latency += TimeSpan.FromMilliseconds((double)requestLatency);
		}

		// Token: 0x040003E5 RID: 997
		[ThreadStatic]
		private static MServeLatencyContext current;
	}
}

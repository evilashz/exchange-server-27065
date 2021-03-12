using System;
using Microsoft.Exchange.Diagnostics.LatencyDetection;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000177 RID: 375
	internal sealed class PerformanceContext : PerformanceDataProvider
	{
		// Token: 0x0600100D RID: 4109 RVA: 0x0004CA3F File Offset: 0x0004AC3F
		internal PerformanceContext() : base("LDAP Requests")
		{
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0004CA4C File Offset: 0x0004AC4C
		internal PerformanceContext(PerformanceContext performanceContext) : this()
		{
			base.RequestCount = performanceContext.RequestCount;
			base.Latency = performanceContext.Latency;
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0004CA6C File Offset: 0x0004AC6C
		public int RequestLatency
		{
			get
			{
				return (int)base.Latency.TotalMilliseconds;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x0004CA88 File Offset: 0x0004AC88
		internal static PerformanceContext Current
		{
			get
			{
				if (PerformanceContext.current == null)
				{
					PerformanceContext.current = new PerformanceContext();
				}
				return PerformanceContext.current;
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0004CAA0 File Offset: 0x0004ACA0
		internal static void UpdateContext(uint requestCount, int requestLatency)
		{
			PerformanceContext performanceContext = PerformanceContext.Current;
			performanceContext.RequestCount += requestCount;
			performanceContext.Latency += TimeSpan.FromMilliseconds((double)requestLatency);
		}

		// Token: 0x04000931 RID: 2353
		[ThreadStatic]
		private static PerformanceContext current;
	}
}

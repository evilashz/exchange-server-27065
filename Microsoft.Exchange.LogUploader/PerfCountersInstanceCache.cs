using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000005 RID: 5
	internal static class PerfCountersInstanceCache
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003118 File Offset: 0x00001318
		static PerfCountersInstanceCache()
		{
			PerfCountersInstanceCache.instances.Add("_Total", PerfCountersInstanceCache.TotalInstance);
			PerfCountersInstanceCache.GetPerfCountersInstance = ((string instanceName) => new LogUploaderDefaultCommonPerfCountersInstance(instanceName, PerfCountersInstanceCache.TotalInstance));
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003188 File Offset: 0x00001388
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000318F File Offset: 0x0000138F
		public static Func<string, ILogUploaderPerformanceCounters> GetPerfCountersInstance { get; set; }

		// Token: 0x06000091 RID: 145 RVA: 0x00003198 File Offset: 0x00001398
		public static ILogUploaderPerformanceCounters GetInstance(string instanceName)
		{
			ILogUploaderPerformanceCounters logUploaderPerformanceCounters;
			if (!PerfCountersInstanceCache.instances.TryGetValue(instanceName, out logUploaderPerformanceCounters))
			{
				lock (PerfCountersInstanceCache.InstancesMutex)
				{
					if (!PerfCountersInstanceCache.instances.TryGetValue(instanceName, out logUploaderPerformanceCounters))
					{
						Tools.DebugAssert(PerfCountersInstanceCache.GetPerfCountersInstance != null, "Performance counters factory method expected.");
						logUploaderPerformanceCounters = PerfCountersInstanceCache.GetPerfCountersInstance(instanceName);
						PerfCountersInstanceCache.instances = new Dictionary<string, ILogUploaderPerformanceCounters>(PerfCountersInstanceCache.instances, StringComparer.OrdinalIgnoreCase)
						{
							{
								instanceName,
								logUploaderPerformanceCounters
							}
						};
					}
				}
			}
			return logUploaderPerformanceCounters;
		}

		// Token: 0x04000032 RID: 50
		private const string TotalInstanceName = "_Total";

		// Token: 0x04000033 RID: 51
		private static readonly object InstancesMutex = new object();

		// Token: 0x04000034 RID: 52
		private static readonly LogUploaderDefaultCommonPerfCountersInstance TotalInstance = new LogUploaderDefaultCommonPerfCountersInstance("_Total", null);

		// Token: 0x04000035 RID: 53
		private static volatile Dictionary<string, ILogUploaderPerformanceCounters> instances = new Dictionary<string, ILogUploaderPerformanceCounters>(StringComparer.OrdinalIgnoreCase);
	}
}

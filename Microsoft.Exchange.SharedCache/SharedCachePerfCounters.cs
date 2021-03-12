using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x02000002 RID: 2
	internal static class SharedCachePerfCounters
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static SharedCachePerfCountersInstance GetInstance(string instanceName)
		{
			return (SharedCachePerfCountersInstance)SharedCachePerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E2 File Offset: 0x000002E2
		public static void CloseInstance(string instanceName)
		{
			SharedCachePerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020EF File Offset: 0x000002EF
		public static bool InstanceExists(string instanceName)
		{
			return SharedCachePerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		public static string[] GetInstanceNames()
		{
			return SharedCachePerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002108 File Offset: 0x00000308
		public static void RemoveInstance(string instanceName)
		{
			SharedCachePerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002115 File Offset: 0x00000315
		public static void ResetInstance(string instanceName)
		{
			SharedCachePerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002122 File Offset: 0x00000322
		public static void RemoveAllInstances()
		{
			SharedCachePerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212E File Offset: 0x0000032E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new SharedCachePerfCountersInstance(instanceName, (SharedCachePerfCountersInstance)totalInstance);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new SharedCachePerfCountersInstance(instanceName);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002144 File Offset: 0x00000344
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SharedCachePerfCounters.counters == null)
			{
				return;
			}
			SharedCachePerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000001 RID: 1
		public const string CategoryName = "MSExchange Shared Cache";

		// Token: 0x04000002 RID: 2
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Shared Cache", new CreateInstanceDelegate(SharedCachePerfCounters.CreateInstance));
	}
}

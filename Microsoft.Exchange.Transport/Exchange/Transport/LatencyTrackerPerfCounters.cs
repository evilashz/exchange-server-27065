using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000569 RID: 1385
	internal static class LatencyTrackerPerfCounters
	{
		// Token: 0x06003F6D RID: 16237 RVA: 0x0011371C File Offset: 0x0011191C
		public static LatencyTrackerPerfCountersInstance GetInstance(string instanceName)
		{
			return (LatencyTrackerPerfCountersInstance)LatencyTrackerPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x0011372E File Offset: 0x0011192E
		public static void CloseInstance(string instanceName)
		{
			LatencyTrackerPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x0011373B File Offset: 0x0011193B
		public static bool InstanceExists(string instanceName)
		{
			return LatencyTrackerPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x00113748 File Offset: 0x00111948
		public static string[] GetInstanceNames()
		{
			return LatencyTrackerPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x00113754 File Offset: 0x00111954
		public static void RemoveInstance(string instanceName)
		{
			LatencyTrackerPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x00113761 File Offset: 0x00111961
		public static void ResetInstance(string instanceName)
		{
			LatencyTrackerPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x0011376E File Offset: 0x0011196E
		public static void RemoveAllInstances()
		{
			LatencyTrackerPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x0011377A File Offset: 0x0011197A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new LatencyTrackerPerfCountersInstance(instanceName, (LatencyTrackerPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x00113788 File Offset: 0x00111988
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new LatencyTrackerPerfCountersInstance(instanceName);
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x00113790 File Offset: 0x00111990
		public static void SetCategoryName(string categoryName)
		{
			if (LatencyTrackerPerfCounters.counters == null)
			{
				LatencyTrackerPerfCounters.CategoryName = categoryName;
				LatencyTrackerPerfCounters.counters = new PerformanceCounterMultipleInstance(LatencyTrackerPerfCounters.CategoryName, new CreateInstanceDelegate(LatencyTrackerPerfCounters.CreateInstance));
			}
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x001137BA File Offset: 0x001119BA
		public static void GetPerfCounterInfo(XElement element)
		{
			if (LatencyTrackerPerfCounters.counters == null)
			{
				return;
			}
			LatencyTrackerPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040023A7 RID: 9127
		public static string CategoryName;

		// Token: 0x040023A8 RID: 9128
		private static PerformanceCounterMultipleInstance counters;
	}
}

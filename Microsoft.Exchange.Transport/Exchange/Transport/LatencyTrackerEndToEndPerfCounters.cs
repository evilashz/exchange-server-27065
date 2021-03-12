using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200056B RID: 1387
	internal static class LatencyTrackerEndToEndPerfCounters
	{
		// Token: 0x06003F7B RID: 16251 RVA: 0x00113D18 File Offset: 0x00111F18
		public static LatencyTrackerEndToEndPerfCountersInstance GetInstance(string instanceName)
		{
			return (LatencyTrackerEndToEndPerfCountersInstance)LatencyTrackerEndToEndPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x00113D2A File Offset: 0x00111F2A
		public static void CloseInstance(string instanceName)
		{
			LatencyTrackerEndToEndPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003F7D RID: 16253 RVA: 0x00113D37 File Offset: 0x00111F37
		public static bool InstanceExists(string instanceName)
		{
			return LatencyTrackerEndToEndPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003F7E RID: 16254 RVA: 0x00113D44 File Offset: 0x00111F44
		public static string[] GetInstanceNames()
		{
			return LatencyTrackerEndToEndPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x00113D50 File Offset: 0x00111F50
		public static void RemoveInstance(string instanceName)
		{
			LatencyTrackerEndToEndPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x00113D5D File Offset: 0x00111F5D
		public static void ResetInstance(string instanceName)
		{
			LatencyTrackerEndToEndPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x00113D6A File Offset: 0x00111F6A
		public static void RemoveAllInstances()
		{
			LatencyTrackerEndToEndPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x00113D76 File Offset: 0x00111F76
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new LatencyTrackerEndToEndPerfCountersInstance(instanceName, (LatencyTrackerEndToEndPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x00113D84 File Offset: 0x00111F84
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new LatencyTrackerEndToEndPerfCountersInstance(instanceName);
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x00113D8C File Offset: 0x00111F8C
		public static void SetCategoryName(string categoryName)
		{
			if (LatencyTrackerEndToEndPerfCounters.counters == null)
			{
				LatencyTrackerEndToEndPerfCounters.CategoryName = categoryName;
				LatencyTrackerEndToEndPerfCounters.counters = new PerformanceCounterMultipleInstance(LatencyTrackerEndToEndPerfCounters.CategoryName, new CreateInstanceDelegate(LatencyTrackerEndToEndPerfCounters.CreateInstance));
			}
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x00113DB6 File Offset: 0x00111FB6
		public static void GetPerfCounterInfo(XElement element)
		{
			if (LatencyTrackerEndToEndPerfCounters.counters == null)
			{
				return;
			}
			LatencyTrackerEndToEndPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040023B3 RID: 9139
		public static string CategoryName;

		// Token: 0x040023B4 RID: 9140
		private static PerformanceCounterMultipleInstance counters;
	}
}

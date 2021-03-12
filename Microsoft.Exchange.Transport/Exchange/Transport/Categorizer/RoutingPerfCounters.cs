using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000557 RID: 1367
	internal static class RoutingPerfCounters
	{
		// Token: 0x06003EED RID: 16109 RVA: 0x0010DCA0 File Offset: 0x0010BEA0
		public static RoutingPerfCountersInstance GetInstance(string instanceName)
		{
			return (RoutingPerfCountersInstance)RoutingPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x0010DCB2 File Offset: 0x0010BEB2
		public static void CloseInstance(string instanceName)
		{
			RoutingPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x0010DCBF File Offset: 0x0010BEBF
		public static bool InstanceExists(string instanceName)
		{
			return RoutingPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x0010DCCC File Offset: 0x0010BECC
		public static string[] GetInstanceNames()
		{
			return RoutingPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x0010DCD8 File Offset: 0x0010BED8
		public static void RemoveInstance(string instanceName)
		{
			RoutingPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x0010DCE5 File Offset: 0x0010BEE5
		public static void ResetInstance(string instanceName)
		{
			RoutingPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x0010DCF2 File Offset: 0x0010BEF2
		public static void RemoveAllInstances()
		{
			RoutingPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x0010DCFE File Offset: 0x0010BEFE
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new RoutingPerfCountersInstance(instanceName, (RoutingPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x0010DD0C File Offset: 0x0010BF0C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new RoutingPerfCountersInstance(instanceName);
		}

		// Token: 0x06003EF6 RID: 16118 RVA: 0x0010DD14 File Offset: 0x0010BF14
		public static void SetCategoryName(string categoryName)
		{
			if (RoutingPerfCounters.counters == null)
			{
				RoutingPerfCounters.CategoryName = categoryName;
				RoutingPerfCounters.counters = new PerformanceCounterMultipleInstance(RoutingPerfCounters.CategoryName, new CreateInstanceDelegate(RoutingPerfCounters.CreateInstance));
			}
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x0010DD3E File Offset: 0x0010BF3E
		public static void GetPerfCounterInfo(XElement element)
		{
			if (RoutingPerfCounters.counters == null)
			{
				return;
			}
			RoutingPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400230C RID: 8972
		public static string CategoryName;

		// Token: 0x0400230D RID: 8973
		private static PerformanceCounterMultipleInstance counters;
	}
}

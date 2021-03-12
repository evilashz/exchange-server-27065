using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric
{
	// Token: 0x0200002A RID: 42
	internal static class TaskDistributionFabricPerfCounters
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public static TaskDistributionFabricPerfCountersInstance GetInstance(string instanceName)
		{
			return (TaskDistributionFabricPerfCountersInstance)TaskDistributionFabricPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005DC2 File Offset: 0x00003FC2
		public static void CloseInstance(string instanceName)
		{
			TaskDistributionFabricPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005DCF File Offset: 0x00003FCF
		public static bool InstanceExists(string instanceName)
		{
			return TaskDistributionFabricPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005DDC File Offset: 0x00003FDC
		public static string[] GetInstanceNames()
		{
			return TaskDistributionFabricPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005DE8 File Offset: 0x00003FE8
		public static void RemoveInstance(string instanceName)
		{
			TaskDistributionFabricPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005DF5 File Offset: 0x00003FF5
		public static void ResetInstance(string instanceName)
		{
			TaskDistributionFabricPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005E02 File Offset: 0x00004002
		public static void RemoveAllInstances()
		{
			TaskDistributionFabricPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005E0E File Offset: 0x0000400E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new TaskDistributionFabricPerfCountersInstance(instanceName, (TaskDistributionFabricPerfCountersInstance)totalInstance);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005E1C File Offset: 0x0000401C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new TaskDistributionFabricPerfCountersInstance(instanceName);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005E24 File Offset: 0x00004024
		public static void GetPerfCounterInfo(XElement element)
		{
			if (TaskDistributionFabricPerfCounters.counters == null)
			{
				return;
			}
			TaskDistributionFabricPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000063 RID: 99
		public const string CategoryName = "MSExchange Task Distribution Fabric";

		// Token: 0x04000064 RID: 100
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Task Distribution Fabric", new CreateInstanceDelegate(TaskDistributionFabricPerfCounters.CreateInstance));
	}
}

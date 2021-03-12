using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001AE RID: 430
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MiddleTierStoragePerformanceCounters
	{
		// Token: 0x060017A9 RID: 6057 RVA: 0x000726AE File Offset: 0x000708AE
		public static MiddleTierStoragePerformanceCountersInstance GetInstance(string instanceName)
		{
			return (MiddleTierStoragePerformanceCountersInstance)MiddleTierStoragePerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x000726C0 File Offset: 0x000708C0
		public static void CloseInstance(string instanceName)
		{
			MiddleTierStoragePerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x000726CD File Offset: 0x000708CD
		public static bool InstanceExists(string instanceName)
		{
			return MiddleTierStoragePerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000726DA File Offset: 0x000708DA
		public static string[] GetInstanceNames()
		{
			return MiddleTierStoragePerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x000726E6 File Offset: 0x000708E6
		public static void RemoveInstance(string instanceName)
		{
			MiddleTierStoragePerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x000726F3 File Offset: 0x000708F3
		public static void ResetInstance(string instanceName)
		{
			MiddleTierStoragePerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x00072700 File Offset: 0x00070900
		public static void RemoveAllInstances()
		{
			MiddleTierStoragePerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0007270C File Offset: 0x0007090C
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MiddleTierStoragePerformanceCountersInstance(instanceName, (MiddleTierStoragePerformanceCountersInstance)totalInstance);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0007271A File Offset: 0x0007091A
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MiddleTierStoragePerformanceCountersInstance(instanceName);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00072722 File Offset: 0x00070922
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MiddleTierStoragePerformanceCounters.counters == null)
			{
				return;
			}
			MiddleTierStoragePerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000C11 RID: 3089
		public const string CategoryName = "MSExchange Middle-Tier Storage";

		// Token: 0x04000C12 RID: 3090
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Middle-Tier Storage", new CreateInstanceDelegate(MiddleTierStoragePerformanceCounters.CreateInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200054C RID: 1356
	internal static class DsnGeneratorPerfCounters
	{
		// Token: 0x06003EAE RID: 16046 RVA: 0x0010C620 File Offset: 0x0010A820
		public static DsnGeneratorPerfCountersInstance GetInstance(string instanceName)
		{
			return (DsnGeneratorPerfCountersInstance)DsnGeneratorPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x0010C632 File Offset: 0x0010A832
		public static void CloseInstance(string instanceName)
		{
			DsnGeneratorPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x0010C63F File Offset: 0x0010A83F
		public static bool InstanceExists(string instanceName)
		{
			return DsnGeneratorPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x0010C64C File Offset: 0x0010A84C
		public static string[] GetInstanceNames()
		{
			return DsnGeneratorPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x0010C658 File Offset: 0x0010A858
		public static void RemoveInstance(string instanceName)
		{
			DsnGeneratorPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x0010C665 File Offset: 0x0010A865
		public static void ResetInstance(string instanceName)
		{
			DsnGeneratorPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x0010C672 File Offset: 0x0010A872
		public static void RemoveAllInstances()
		{
			DsnGeneratorPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x0010C67E File Offset: 0x0010A87E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new DsnGeneratorPerfCountersInstance(instanceName, (DsnGeneratorPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x0010C68C File Offset: 0x0010A88C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new DsnGeneratorPerfCountersInstance(instanceName);
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x0010C694 File Offset: 0x0010A894
		public static void SetCategoryName(string categoryName)
		{
			if (DsnGeneratorPerfCounters.counters == null)
			{
				DsnGeneratorPerfCounters.CategoryName = categoryName;
				DsnGeneratorPerfCounters.counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal(DsnGeneratorPerfCounters.CategoryName, new CreateInstanceDelegate(DsnGeneratorPerfCounters.CreateInstance));
			}
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x0010C6BE File Offset: 0x0010A8BE
		public static DsnGeneratorPerfCountersInstance TotalInstance
		{
			get
			{
				return (DsnGeneratorPerfCountersInstance)DsnGeneratorPerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x0010C6CF File Offset: 0x0010A8CF
		public static void GetPerfCounterInfo(XElement element)
		{
			if (DsnGeneratorPerfCounters.counters == null)
			{
				return;
			}
			DsnGeneratorPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040022D4 RID: 8916
		public static string CategoryName;

		// Token: 0x040022D5 RID: 8917
		private static PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters;
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000A45 RID: 2629
	internal static class GlsProcessPerformanceCounters
	{
		// Token: 0x0600784E RID: 30798 RVA: 0x0018DE68 File Offset: 0x0018C068
		public static GlsProcessPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (GlsProcessPerformanceCountersInstance)GlsProcessPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600784F RID: 30799 RVA: 0x0018DE7A File Offset: 0x0018C07A
		public static void CloseInstance(string instanceName)
		{
			GlsProcessPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06007850 RID: 30800 RVA: 0x0018DE87 File Offset: 0x0018C087
		public static bool InstanceExists(string instanceName)
		{
			return GlsProcessPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06007851 RID: 30801 RVA: 0x0018DE94 File Offset: 0x0018C094
		public static string[] GetInstanceNames()
		{
			return GlsProcessPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x06007852 RID: 30802 RVA: 0x0018DEA0 File Offset: 0x0018C0A0
		public static void RemoveInstance(string instanceName)
		{
			GlsProcessPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06007853 RID: 30803 RVA: 0x0018DEAD File Offset: 0x0018C0AD
		public static void ResetInstance(string instanceName)
		{
			GlsProcessPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06007854 RID: 30804 RVA: 0x0018DEBA File Offset: 0x0018C0BA
		public static void RemoveAllInstances()
		{
			GlsProcessPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06007855 RID: 30805 RVA: 0x0018DEC6 File Offset: 0x0018C0C6
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new GlsProcessPerformanceCountersInstance(instanceName, (GlsProcessPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x06007856 RID: 30806 RVA: 0x0018DED4 File Offset: 0x0018C0D4
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new GlsProcessPerformanceCountersInstance(instanceName);
		}

		// Token: 0x17002ADE RID: 10974
		// (get) Token: 0x06007857 RID: 30807 RVA: 0x0018DEDC File Offset: 0x0018C0DC
		public static GlsProcessPerformanceCountersInstance TotalInstance
		{
			get
			{
				return (GlsProcessPerformanceCountersInstance)GlsProcessPerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06007858 RID: 30808 RVA: 0x0018DEED File Offset: 0x0018C0ED
		public static void GetPerfCounterInfo(XElement element)
		{
			if (GlsProcessPerformanceCounters.counters == null)
			{
				return;
			}
			GlsProcessPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04004F38 RID: 20280
		public const string CategoryName = "MSExchange Global Locator Processes";

		// Token: 0x04004F39 RID: 20281
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Global Locator Processes", new CreateInstanceDelegate(GlsProcessPerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(GlsProcessPerformanceCounters.CreateTotalInstance));
	}
}

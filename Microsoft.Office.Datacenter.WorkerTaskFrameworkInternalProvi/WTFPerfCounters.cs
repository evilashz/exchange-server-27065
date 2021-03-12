using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000039 RID: 57
	internal static class WTFPerfCounters
	{
		// Token: 0x06000384 RID: 900 RVA: 0x0000C1D3 File Offset: 0x0000A3D3
		public static WTFPerfCountersInstance GetInstance(string instanceName)
		{
			return (WTFPerfCountersInstance)WTFPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000C1E5 File Offset: 0x0000A3E5
		public static void CloseInstance(string instanceName)
		{
			WTFPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000C1F2 File Offset: 0x0000A3F2
		public static bool InstanceExists(string instanceName)
		{
			return WTFPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000C1FF File Offset: 0x0000A3FF
		public static string[] GetInstanceNames()
		{
			return WTFPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000C20B File Offset: 0x0000A40B
		public static void RemoveInstance(string instanceName)
		{
			WTFPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000C218 File Offset: 0x0000A418
		public static void ResetInstance(string instanceName)
		{
			WTFPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000C225 File Offset: 0x0000A425
		public static void RemoveAllInstances()
		{
			WTFPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000C231 File Offset: 0x0000A431
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new WTFPerfCountersInstance(instanceName, (WTFPerfCountersInstance)totalInstance);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000C23F File Offset: 0x0000A43F
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new WTFPerfCountersInstance(instanceName);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000C247 File Offset: 0x0000A447
		public static void GetPerfCounterInfo(XElement element)
		{
			if (WTFPerfCounters.counters == null)
			{
				return;
			}
			WTFPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400016D RID: 365
		public const string CategoryName = "MSExchangeWorkerTaskFramework";

		// Token: 0x0400016E RID: 366
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeWorkerTaskFramework", new CreateInstanceDelegate(WTFPerfCounters.CreateInstance));
	}
}

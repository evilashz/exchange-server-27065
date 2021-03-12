using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000038 RID: 56
	internal static class PswshttpRequestPerformanceCounters
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00007EA4 File Offset: 0x000060A4
		public static PswshttpRequestPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (PswshttpRequestPerformanceCountersInstance)PswshttpRequestPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007EB6 File Offset: 0x000060B6
		public static void CloseInstance(string instanceName)
		{
			PswshttpRequestPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007EC3 File Offset: 0x000060C3
		public static bool InstanceExists(string instanceName)
		{
			return PswshttpRequestPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007ED0 File Offset: 0x000060D0
		public static string[] GetInstanceNames()
		{
			return PswshttpRequestPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007EDC File Offset: 0x000060DC
		public static void RemoveInstance(string instanceName)
		{
			PswshttpRequestPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007EE9 File Offset: 0x000060E9
		public static void ResetInstance(string instanceName)
		{
			PswshttpRequestPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007EF6 File Offset: 0x000060F6
		public static void RemoveAllInstances()
		{
			PswshttpRequestPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007F02 File Offset: 0x00006102
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new PswshttpRequestPerformanceCountersInstance(instanceName, (PswshttpRequestPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007F10 File Offset: 0x00006110
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new PswshttpRequestPerformanceCountersInstance(instanceName);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00007F18 File Offset: 0x00006118
		public static PswshttpRequestPerformanceCountersInstance TotalInstance
		{
			get
			{
				return (PswshttpRequestPerformanceCountersInstance)PswshttpRequestPerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007F29 File Offset: 0x00006129
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PswshttpRequestPerformanceCounters.counters == null)
			{
				return;
			}
			PswshttpRequestPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040000DB RID: 219
		public const string CategoryName = "MSExchangePowershellWebServiceHttpRequest";

		// Token: 0x040000DC RID: 220
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangePowershellWebServiceHttpRequest", new CreateInstanceDelegate(PswshttpRequestPerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(PswshttpRequestPerformanceCounters.CreateTotalInstance));
	}
}

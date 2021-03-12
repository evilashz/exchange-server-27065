using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000032 RID: 50
	internal static class SchedulerPerfCounters
	{
		// Token: 0x0600012E RID: 302 RVA: 0x000056F2 File Offset: 0x000038F2
		public static SchedulerPerfCountersInstance GetInstance(string instanceName)
		{
			return (SchedulerPerfCountersInstance)SchedulerPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005704 File Offset: 0x00003904
		public static void CloseInstance(string instanceName)
		{
			SchedulerPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005711 File Offset: 0x00003911
		public static bool InstanceExists(string instanceName)
		{
			return SchedulerPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000571E File Offset: 0x0000391E
		public static string[] GetInstanceNames()
		{
			return SchedulerPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000572A File Offset: 0x0000392A
		public static void RemoveInstance(string instanceName)
		{
			SchedulerPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005737 File Offset: 0x00003937
		public static void ResetInstance(string instanceName)
		{
			SchedulerPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005744 File Offset: 0x00003944
		public static void RemoveAllInstances()
		{
			SchedulerPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005750 File Offset: 0x00003950
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new SchedulerPerfCountersInstance(instanceName, (SchedulerPerfCountersInstance)totalInstance);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000575E File Offset: 0x0000395E
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new SchedulerPerfCountersInstance(instanceName);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005766 File Offset: 0x00003966
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SchedulerPerfCounters.counters == null)
			{
				return;
			}
			SchedulerPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040000AC RID: 172
		public const string CategoryName = "MSExchangeTransport Processing Scheduler";

		// Token: 0x040000AD RID: 173
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeTransport Processing Scheduler", new CreateInstanceDelegate(SchedulerPerfCounters.CreateInstance));
	}
}

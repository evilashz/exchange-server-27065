using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000016 RID: 22
	internal static class EdgeSynchronizerPerfCounters
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00008D64 File Offset: 0x00006F64
		public static EdgeSynchronizerPerfCountersInstance GetInstance(string instanceName)
		{
			return (EdgeSynchronizerPerfCountersInstance)EdgeSynchronizerPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00008D76 File Offset: 0x00006F76
		public static void CloseInstance(string instanceName)
		{
			EdgeSynchronizerPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00008D83 File Offset: 0x00006F83
		public static bool InstanceExists(string instanceName)
		{
			return EdgeSynchronizerPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00008D90 File Offset: 0x00006F90
		public static string[] GetInstanceNames()
		{
			return EdgeSynchronizerPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00008D9C File Offset: 0x00006F9C
		public static void RemoveInstance(string instanceName)
		{
			EdgeSynchronizerPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00008DA9 File Offset: 0x00006FA9
		public static void ResetInstance(string instanceName)
		{
			EdgeSynchronizerPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00008DB6 File Offset: 0x00006FB6
		public static void RemoveAllInstances()
		{
			EdgeSynchronizerPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00008DC2 File Offset: 0x00006FC2
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new EdgeSynchronizerPerfCountersInstance(instanceName, (EdgeSynchronizerPerfCountersInstance)totalInstance);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00008DD0 File Offset: 0x00006FD0
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new EdgeSynchronizerPerfCountersInstance(instanceName);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public static EdgeSynchronizerPerfCountersInstance TotalInstance
		{
			get
			{
				return (EdgeSynchronizerPerfCountersInstance)EdgeSynchronizerPerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00008DE9 File Offset: 0x00006FE9
		public static void GetPerfCounterInfo(XElement element)
		{
			if (EdgeSynchronizerPerfCounters.counters == null)
			{
				return;
			}
			EdgeSynchronizerPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000078 RID: 120
		public const string CategoryName = "MSExchangeEdgeSync Synchronizer";

		// Token: 0x04000079 RID: 121
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeEdgeSync Synchronizer", new CreateInstanceDelegate(EdgeSynchronizerPerfCounters.CreateInstance), new CreateTotalInstanceDelegate(EdgeSynchronizerPerfCounters.CreateTotalInstance));
	}
}

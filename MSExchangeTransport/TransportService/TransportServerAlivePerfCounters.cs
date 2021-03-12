using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.TransportService
{
	// Token: 0x02000006 RID: 6
	internal static class TransportServerAlivePerfCounters
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00004B41 File Offset: 0x00002D41
		public static TransportServerAlivePerfCountersInstance GetInstance(string instanceName)
		{
			return (TransportServerAlivePerfCountersInstance)TransportServerAlivePerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00004B53 File Offset: 0x00002D53
		public static void CloseInstance(string instanceName)
		{
			TransportServerAlivePerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00004B60 File Offset: 0x00002D60
		public static bool InstanceExists(string instanceName)
		{
			return TransportServerAlivePerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00004B6D File Offset: 0x00002D6D
		public static string[] GetInstanceNames()
		{
			return TransportServerAlivePerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00004B79 File Offset: 0x00002D79
		public static void RemoveInstance(string instanceName)
		{
			TransportServerAlivePerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004B86 File Offset: 0x00002D86
		public static void ResetInstance(string instanceName)
		{
			TransportServerAlivePerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004B93 File Offset: 0x00002D93
		public static void RemoveAllInstances()
		{
			TransportServerAlivePerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00004B9F File Offset: 0x00002D9F
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new TransportServerAlivePerfCountersInstance(instanceName, (TransportServerAlivePerfCountersInstance)totalInstance);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004BAD File Offset: 0x00002DAD
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new TransportServerAlivePerfCountersInstance(instanceName);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004BB5 File Offset: 0x00002DB5
		public static void GetPerfCounterInfo(XElement element)
		{
			if (TransportServerAlivePerfCounters.counters == null)
			{
				return;
			}
			TransportServerAlivePerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040002B0 RID: 688
		public const string CategoryName = "MSExchangeTransport ServerAlive";

		// Token: 0x040002B1 RID: 689
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeTransport ServerAlive", new CreateInstanceDelegate(TransportServerAlivePerfCounters.CreateInstance));
	}
}

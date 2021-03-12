using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x02000548 RID: 1352
	internal static class QueuingPerfCounters
	{
		// Token: 0x06003E91 RID: 16017 RVA: 0x0010B170 File Offset: 0x00109370
		public static QueuingPerfCountersInstance GetInstance(string instanceName)
		{
			return (QueuingPerfCountersInstance)QueuingPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x0010B182 File Offset: 0x00109382
		public static void CloseInstance(string instanceName)
		{
			QueuingPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x0010B18F File Offset: 0x0010938F
		public static bool InstanceExists(string instanceName)
		{
			return QueuingPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x0010B19C File Offset: 0x0010939C
		public static string[] GetInstanceNames()
		{
			return QueuingPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x0010B1A8 File Offset: 0x001093A8
		public static void RemoveInstance(string instanceName)
		{
			QueuingPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x0010B1B5 File Offset: 0x001093B5
		public static void ResetInstance(string instanceName)
		{
			QueuingPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x0010B1C2 File Offset: 0x001093C2
		public static void RemoveAllInstances()
		{
			QueuingPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x0010B1CE File Offset: 0x001093CE
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new QueuingPerfCountersInstance(instanceName, (QueuingPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x0010B1DC File Offset: 0x001093DC
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new QueuingPerfCountersInstance(instanceName);
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x0010B1E4 File Offset: 0x001093E4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (QueuingPerfCounters.counters == null)
			{
				return;
			}
			QueuingPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040022A8 RID: 8872
		public const string CategoryName = "MSExchangeTransport Queues";

		// Token: 0x040022A9 RID: 8873
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeTransport Queues", new CreateInstanceDelegate(QueuingPerfCounters.CreateInstance));
	}
}

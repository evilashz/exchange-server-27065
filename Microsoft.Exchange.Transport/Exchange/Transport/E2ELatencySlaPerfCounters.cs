using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000567 RID: 1383
	internal static class E2ELatencySlaPerfCounters
	{
		// Token: 0x06003F5F RID: 16223 RVA: 0x001133FC File Offset: 0x001115FC
		public static E2ELatencySlaPerfCountersInstance GetInstance(string instanceName)
		{
			return (E2ELatencySlaPerfCountersInstance)E2ELatencySlaPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x0011340E File Offset: 0x0011160E
		public static void CloseInstance(string instanceName)
		{
			E2ELatencySlaPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x0011341B File Offset: 0x0011161B
		public static bool InstanceExists(string instanceName)
		{
			return E2ELatencySlaPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x00113428 File Offset: 0x00111628
		public static string[] GetInstanceNames()
		{
			return E2ELatencySlaPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x00113434 File Offset: 0x00111634
		public static void RemoveInstance(string instanceName)
		{
			E2ELatencySlaPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x00113441 File Offset: 0x00111641
		public static void ResetInstance(string instanceName)
		{
			E2ELatencySlaPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x0011344E File Offset: 0x0011164E
		public static void RemoveAllInstances()
		{
			E2ELatencySlaPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x0011345A File Offset: 0x0011165A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new E2ELatencySlaPerfCountersInstance(instanceName, (E2ELatencySlaPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003F67 RID: 16231 RVA: 0x00113468 File Offset: 0x00111668
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new E2ELatencySlaPerfCountersInstance(instanceName);
		}

		// Token: 0x06003F68 RID: 16232 RVA: 0x00113470 File Offset: 0x00111670
		public static void GetPerfCounterInfo(XElement element)
		{
			if (E2ELatencySlaPerfCounters.counters == null)
			{
				return;
			}
			E2ELatencySlaPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040023A3 RID: 9123
		public const string CategoryName = "MSExchangeTransport E2E Latency SLA";

		// Token: 0x040023A4 RID: 9124
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeTransport E2E Latency SLA", new CreateInstanceDelegate(E2ELatencySlaPerfCounters.CreateInstance));
	}
}

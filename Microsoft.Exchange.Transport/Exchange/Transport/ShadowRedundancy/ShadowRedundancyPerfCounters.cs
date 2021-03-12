using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200055F RID: 1375
	internal static class ShadowRedundancyPerfCounters
	{
		// Token: 0x06003F26 RID: 16166 RVA: 0x00111D1C File Offset: 0x0010FF1C
		public static ShadowRedundancyPerfCountersInstance GetInstance(string instanceName)
		{
			return (ShadowRedundancyPerfCountersInstance)ShadowRedundancyPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x00111D2E File Offset: 0x0010FF2E
		public static void CloseInstance(string instanceName)
		{
			ShadowRedundancyPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x00111D3B File Offset: 0x0010FF3B
		public static bool InstanceExists(string instanceName)
		{
			return ShadowRedundancyPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x00111D48 File Offset: 0x0010FF48
		public static string[] GetInstanceNames()
		{
			return ShadowRedundancyPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x00111D54 File Offset: 0x0010FF54
		public static void RemoveInstance(string instanceName)
		{
			ShadowRedundancyPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x00111D61 File Offset: 0x0010FF61
		public static void ResetInstance(string instanceName)
		{
			ShadowRedundancyPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x00111D6E File Offset: 0x0010FF6E
		public static void RemoveAllInstances()
		{
			ShadowRedundancyPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x00111D7A File Offset: 0x0010FF7A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ShadowRedundancyPerfCountersInstance(instanceName, (ShadowRedundancyPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x00111D88 File Offset: 0x0010FF88
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ShadowRedundancyPerfCountersInstance(instanceName);
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x00111D90 File Offset: 0x0010FF90
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ShadowRedundancyPerfCounters.counters == null)
			{
				return;
			}
			ShadowRedundancyPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04002379 RID: 9081
		public const string CategoryName = "MSExchangeTransport Shadow Redundancy";

		// Token: 0x0400237A RID: 9082
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeTransport Shadow Redundancy", new CreateInstanceDelegate(ShadowRedundancyPerfCounters.CreateInstance));
	}
}

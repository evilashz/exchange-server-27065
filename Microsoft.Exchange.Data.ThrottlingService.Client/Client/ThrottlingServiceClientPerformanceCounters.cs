using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ThrottlingService.Client
{
	// Token: 0x0200000C RID: 12
	internal static class ThrottlingServiceClientPerformanceCounters
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000031C4 File Offset: 0x000013C4
		public static ThrottlingServiceClientPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (ThrottlingServiceClientPerformanceCountersInstance)ThrottlingServiceClientPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000031D6 File Offset: 0x000013D6
		public static void CloseInstance(string instanceName)
		{
			ThrottlingServiceClientPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000031E3 File Offset: 0x000013E3
		public static bool InstanceExists(string instanceName)
		{
			return ThrottlingServiceClientPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000031F0 File Offset: 0x000013F0
		public static string[] GetInstanceNames()
		{
			return ThrottlingServiceClientPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000031FC File Offset: 0x000013FC
		public static void RemoveInstance(string instanceName)
		{
			ThrottlingServiceClientPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003209 File Offset: 0x00001409
		public static void ResetInstance(string instanceName)
		{
			ThrottlingServiceClientPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003216 File Offset: 0x00001416
		public static void RemoveAllInstances()
		{
			ThrottlingServiceClientPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003222 File Offset: 0x00001422
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ThrottlingServiceClientPerformanceCountersInstance(instanceName, (ThrottlingServiceClientPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003230 File Offset: 0x00001430
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ThrottlingServiceClientPerformanceCountersInstance(instanceName);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003238 File Offset: 0x00001438
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ThrottlingServiceClientPerformanceCounters.counters == null)
			{
				return;
			}
			ThrottlingServiceClientPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400003F RID: 63
		public const string CategoryName = "MSExchange Throttling Service Client";

		// Token: 0x04000040 RID: 64
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Throttling Service Client", new CreateInstanceDelegate(ThrottlingServiceClientPerformanceCounters.CreateInstance));
	}
}

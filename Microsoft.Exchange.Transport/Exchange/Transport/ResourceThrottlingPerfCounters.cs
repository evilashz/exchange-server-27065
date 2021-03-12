using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000577 RID: 1399
	internal static class ResourceThrottlingPerfCounters
	{
		// Token: 0x06003FD1 RID: 16337 RVA: 0x001160C8 File Offset: 0x001142C8
		public static ResourceThrottlingPerfCountersInstance GetInstance(string instanceName)
		{
			return (ResourceThrottlingPerfCountersInstance)ResourceThrottlingPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x001160DA File Offset: 0x001142DA
		public static void CloseInstance(string instanceName)
		{
			ResourceThrottlingPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x001160E7 File Offset: 0x001142E7
		public static bool InstanceExists(string instanceName)
		{
			return ResourceThrottlingPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x001160F4 File Offset: 0x001142F4
		public static string[] GetInstanceNames()
		{
			return ResourceThrottlingPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x00116100 File Offset: 0x00114300
		public static void RemoveInstance(string instanceName)
		{
			ResourceThrottlingPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x0011610D File Offset: 0x0011430D
		public static void ResetInstance(string instanceName)
		{
			ResourceThrottlingPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x0011611A File Offset: 0x0011431A
		public static void RemoveAllInstances()
		{
			ResourceThrottlingPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x00116126 File Offset: 0x00114326
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ResourceThrottlingPerfCountersInstance(instanceName, (ResourceThrottlingPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x00116134 File Offset: 0x00114334
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ResourceThrottlingPerfCountersInstance(instanceName);
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x0011613C File Offset: 0x0011433C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ResourceThrottlingPerfCounters.counters == null)
			{
				return;
			}
			ResourceThrottlingPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040023E7 RID: 9191
		public const string CategoryName = "MSExchangeTransport ResourceThrottling";

		// Token: 0x040023E8 RID: 9192
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeTransport ResourceThrottling", new CreateInstanceDelegate(ResourceThrottlingPerfCounters.CreateInstance));
	}
}

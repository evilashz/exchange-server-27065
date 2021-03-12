using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000181 RID: 385
	internal static class ServiceProxyPoolCounters
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x0001A3D5 File Offset: 0x000185D5
		public static ServiceProxyPoolCountersInstance GetInstance(string instanceName)
		{
			return (ServiceProxyPoolCountersInstance)ServiceProxyPoolCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001A3E7 File Offset: 0x000185E7
		public static void CloseInstance(string instanceName)
		{
			ServiceProxyPoolCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001A3F4 File Offset: 0x000185F4
		public static bool InstanceExists(string instanceName)
		{
			return ServiceProxyPoolCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001A401 File Offset: 0x00018601
		public static string[] GetInstanceNames()
		{
			return ServiceProxyPoolCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001A40D File Offset: 0x0001860D
		public static void RemoveInstance(string instanceName)
		{
			ServiceProxyPoolCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001A41A File Offset: 0x0001861A
		public static void ResetInstance(string instanceName)
		{
			ServiceProxyPoolCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001A427 File Offset: 0x00018627
		public static void RemoveAllInstances()
		{
			ServiceProxyPoolCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001A433 File Offset: 0x00018633
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ServiceProxyPoolCountersInstance(instanceName, (ServiceProxyPoolCountersInstance)totalInstance);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001A441 File Offset: 0x00018641
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ServiceProxyPoolCountersInstance(instanceName);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001A449 File Offset: 0x00018649
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ServiceProxyPoolCounters.counters == null)
			{
				return;
			}
			ServiceProxyPoolCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040006AE RID: 1710
		public const string CategoryName = "MSExchange ServiceProxyPool";

		// Token: 0x040006AF RID: 1711
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange ServiceProxyPool", new CreateInstanceDelegate(ServiceProxyPoolCounters.CreateInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000022 RID: 34
	internal static class HttpProxyCacheCounters
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000066D8 File Offset: 0x000048D8
		public static HttpProxyCacheCountersInstance GetInstance(string instanceName)
		{
			return (HttpProxyCacheCountersInstance)HttpProxyCacheCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000066EA File Offset: 0x000048EA
		public static void CloseInstance(string instanceName)
		{
			HttpProxyCacheCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000066F7 File Offset: 0x000048F7
		public static bool InstanceExists(string instanceName)
		{
			return HttpProxyCacheCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006704 File Offset: 0x00004904
		public static string[] GetInstanceNames()
		{
			return HttpProxyCacheCounters.counters.GetInstanceNames();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006710 File Offset: 0x00004910
		public static void RemoveInstance(string instanceName)
		{
			HttpProxyCacheCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000671D File Offset: 0x0000491D
		public static void ResetInstance(string instanceName)
		{
			HttpProxyCacheCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000672A File Offset: 0x0000492A
		public static void RemoveAllInstances()
		{
			HttpProxyCacheCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006736 File Offset: 0x00004936
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new HttpProxyCacheCountersInstance(instanceName, (HttpProxyCacheCountersInstance)totalInstance);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006744 File Offset: 0x00004944
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new HttpProxyCacheCountersInstance(instanceName);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000674C File Offset: 0x0000494C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (HttpProxyCacheCounters.counters == null)
			{
				return;
			}
			HttpProxyCacheCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000117 RID: 279
		public const string CategoryName = "MSExchange HttpProxy Cache";

		// Token: 0x04000118 RID: 280
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange HttpProxy Cache", new CreateInstanceDelegate(HttpProxyCacheCounters.CreateInstance));
	}
}

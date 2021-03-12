using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000020 RID: 32
	internal static class HttpProxyCounters
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00005668 File Offset: 0x00003868
		public static HttpProxyCountersInstance GetInstance(string instanceName)
		{
			return (HttpProxyCountersInstance)HttpProxyCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000567A File Offset: 0x0000387A
		public static void CloseInstance(string instanceName)
		{
			HttpProxyCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005687 File Offset: 0x00003887
		public static bool InstanceExists(string instanceName)
		{
			return HttpProxyCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005694 File Offset: 0x00003894
		public static string[] GetInstanceNames()
		{
			return HttpProxyCounters.counters.GetInstanceNames();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000056A0 File Offset: 0x000038A0
		public static void RemoveInstance(string instanceName)
		{
			HttpProxyCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000056AD File Offset: 0x000038AD
		public static void ResetInstance(string instanceName)
		{
			HttpProxyCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000056BA File Offset: 0x000038BA
		public static void RemoveAllInstances()
		{
			HttpProxyCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000056C6 File Offset: 0x000038C6
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new HttpProxyCountersInstance(instanceName, (HttpProxyCountersInstance)totalInstance);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000056D4 File Offset: 0x000038D4
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new HttpProxyCountersInstance(instanceName);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000056DC File Offset: 0x000038DC
		public static void GetPerfCounterInfo(XElement element)
		{
			if (HttpProxyCounters.counters == null)
			{
				return;
			}
			HttpProxyCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040000F5 RID: 245
		public const string CategoryName = "MSExchange HttpProxy";

		// Token: 0x040000F6 RID: 246
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange HttpProxy", new CreateInstanceDelegate(HttpProxyCounters.CreateInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000026 RID: 38
	internal static class HttpProxyPerSiteCounters
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00007628 File Offset: 0x00005828
		public static HttpProxyPerSiteCountersInstance GetInstance(string instanceName)
		{
			return (HttpProxyPerSiteCountersInstance)HttpProxyPerSiteCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000763A File Offset: 0x0000583A
		public static void CloseInstance(string instanceName)
		{
			HttpProxyPerSiteCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007647 File Offset: 0x00005847
		public static bool InstanceExists(string instanceName)
		{
			return HttpProxyPerSiteCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007654 File Offset: 0x00005854
		public static string[] GetInstanceNames()
		{
			return HttpProxyPerSiteCounters.counters.GetInstanceNames();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007660 File Offset: 0x00005860
		public static void RemoveInstance(string instanceName)
		{
			HttpProxyPerSiteCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000766D File Offset: 0x0000586D
		public static void ResetInstance(string instanceName)
		{
			HttpProxyPerSiteCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000767A File Offset: 0x0000587A
		public static void RemoveAllInstances()
		{
			HttpProxyPerSiteCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007686 File Offset: 0x00005886
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new HttpProxyPerSiteCountersInstance(instanceName, (HttpProxyPerSiteCountersInstance)totalInstance);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007694 File Offset: 0x00005894
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new HttpProxyPerSiteCountersInstance(instanceName);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000769C File Offset: 0x0000589C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (HttpProxyPerSiteCounters.counters == null)
			{
				return;
			}
			HttpProxyPerSiteCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400013A RID: 314
		public const string CategoryName = "MSExchange HttpProxy Per Site";

		// Token: 0x0400013B RID: 315
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange HttpProxy Per Site", new CreateInstanceDelegate(HttpProxyPerSiteCounters.CreateInstance));
	}
}

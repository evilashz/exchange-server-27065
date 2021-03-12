using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000024 RID: 36
	internal static class HttpProxyPerArrayCounters
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00007358 File Offset: 0x00005558
		public static HttpProxyPerArrayCountersInstance GetInstance(string instanceName)
		{
			return (HttpProxyPerArrayCountersInstance)HttpProxyPerArrayCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000736A File Offset: 0x0000556A
		public static void CloseInstance(string instanceName)
		{
			HttpProxyPerArrayCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007377 File Offset: 0x00005577
		public static bool InstanceExists(string instanceName)
		{
			return HttpProxyPerArrayCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007384 File Offset: 0x00005584
		public static string[] GetInstanceNames()
		{
			return HttpProxyPerArrayCounters.counters.GetInstanceNames();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00007390 File Offset: 0x00005590
		public static void RemoveInstance(string instanceName)
		{
			HttpProxyPerArrayCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000739D File Offset: 0x0000559D
		public static void ResetInstance(string instanceName)
		{
			HttpProxyPerArrayCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000073AA File Offset: 0x000055AA
		public static void RemoveAllInstances()
		{
			HttpProxyPerArrayCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000073B6 File Offset: 0x000055B6
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new HttpProxyPerArrayCountersInstance(instanceName, (HttpProxyPerArrayCountersInstance)totalInstance);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000073C4 File Offset: 0x000055C4
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new HttpProxyPerArrayCountersInstance(instanceName);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000073CC File Offset: 0x000055CC
		public static void GetPerfCounterInfo(XElement element)
		{
			if (HttpProxyPerArrayCounters.counters == null)
			{
				return;
			}
			HttpProxyPerArrayCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000137 RID: 311
		public const string CategoryName = "MSExchange HttpProxy Per Array";

		// Token: 0x04000138 RID: 312
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange HttpProxy Per Array", new CreateInstanceDelegate(HttpProxyPerArrayCounters.CreateInstance));
	}
}

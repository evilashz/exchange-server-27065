using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020004A5 RID: 1189
	internal static class ConfigurationCachePerfCounters
	{
		// Token: 0x0600288D RID: 10381 RVA: 0x000963D6 File Offset: 0x000945D6
		public static ConfigurationCachePerfCountersInstance GetInstance(string instanceName)
		{
			return (ConfigurationCachePerfCountersInstance)ConfigurationCachePerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000963E8 File Offset: 0x000945E8
		public static void CloseInstance(string instanceName)
		{
			ConfigurationCachePerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000963F5 File Offset: 0x000945F5
		public static bool InstanceExists(string instanceName)
		{
			return ConfigurationCachePerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x00096402 File Offset: 0x00094602
		public static string[] GetInstanceNames()
		{
			return ConfigurationCachePerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x0009640E File Offset: 0x0009460E
		public static void RemoveInstance(string instanceName)
		{
			ConfigurationCachePerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x0009641B File Offset: 0x0009461B
		public static void ResetInstance(string instanceName)
		{
			ConfigurationCachePerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x00096428 File Offset: 0x00094628
		public static void RemoveAllInstances()
		{
			ConfigurationCachePerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x00096434 File Offset: 0x00094634
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ConfigurationCachePerfCountersInstance(instanceName, (ConfigurationCachePerfCountersInstance)totalInstance);
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x00096442 File Offset: 0x00094642
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ConfigurationCachePerfCountersInstance(instanceName);
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x0009644A File Offset: 0x0009464A
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ConfigurationCachePerfCounters.counters == null)
			{
				return;
			}
			ConfigurationCachePerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04001780 RID: 6016
		public const string CategoryName = "MSExchange Owa Configuration Cache";

		// Token: 0x04001781 RID: 6017
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Owa Configuration Cache", new CreateInstanceDelegate(ConfigurationCachePerfCounters.CreateInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x02000565 RID: 1381
	internal static class ConfigurationCachePerfCounters
	{
		// Token: 0x06003F51 RID: 16209 RVA: 0x00112F40 File Offset: 0x00111140
		public static ConfigurationCachePerfCountersInstance GetInstance(string instanceName)
		{
			return (ConfigurationCachePerfCountersInstance)ConfigurationCachePerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x00112F52 File Offset: 0x00111152
		public static void CloseInstance(string instanceName)
		{
			ConfigurationCachePerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x00112F5F File Offset: 0x0011115F
		public static bool InstanceExists(string instanceName)
		{
			return ConfigurationCachePerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x00112F6C File Offset: 0x0011116C
		public static string[] GetInstanceNames()
		{
			return ConfigurationCachePerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x00112F78 File Offset: 0x00111178
		public static void RemoveInstance(string instanceName)
		{
			ConfigurationCachePerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x00112F85 File Offset: 0x00111185
		public static void ResetInstance(string instanceName)
		{
			ConfigurationCachePerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x00112F92 File Offset: 0x00111192
		public static void RemoveAllInstances()
		{
			ConfigurationCachePerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x00112F9E File Offset: 0x0011119E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ConfigurationCachePerfCountersInstance(instanceName, (ConfigurationCachePerfCountersInstance)totalInstance);
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x00112FAC File Offset: 0x001111AC
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ConfigurationCachePerfCountersInstance(instanceName);
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x00112FB4 File Offset: 0x001111B4
		public static void SetCategoryName(string categoryName)
		{
			if (ConfigurationCachePerfCounters.counters == null)
			{
				ConfigurationCachePerfCounters.CategoryName = categoryName;
				ConfigurationCachePerfCounters.counters = new PerformanceCounterMultipleInstance(ConfigurationCachePerfCounters.CategoryName, new CreateInstanceDelegate(ConfigurationCachePerfCounters.CreateInstance));
			}
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x00112FDE File Offset: 0x001111DE
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ConfigurationCachePerfCounters.counters == null)
			{
				return;
			}
			ConfigurationCachePerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400239C RID: 9116
		public static string CategoryName;

		// Token: 0x0400239D RID: 9117
		private static PerformanceCounterMultipleInstance counters;
	}
}

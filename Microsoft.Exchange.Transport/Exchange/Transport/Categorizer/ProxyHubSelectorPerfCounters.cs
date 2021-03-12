using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200055B RID: 1371
	internal static class ProxyHubSelectorPerfCounters
	{
		// Token: 0x06003F09 RID: 16137 RVA: 0x0010E62C File Offset: 0x0010C82C
		public static ProxyHubSelectorPerfCountersInstance GetInstance(string instanceName)
		{
			return (ProxyHubSelectorPerfCountersInstance)ProxyHubSelectorPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x0010E63E File Offset: 0x0010C83E
		public static void CloseInstance(string instanceName)
		{
			ProxyHubSelectorPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x0010E64B File Offset: 0x0010C84B
		public static bool InstanceExists(string instanceName)
		{
			return ProxyHubSelectorPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x0010E658 File Offset: 0x0010C858
		public static string[] GetInstanceNames()
		{
			return ProxyHubSelectorPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x0010E664 File Offset: 0x0010C864
		public static void RemoveInstance(string instanceName)
		{
			ProxyHubSelectorPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x0010E671 File Offset: 0x0010C871
		public static void ResetInstance(string instanceName)
		{
			ProxyHubSelectorPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x0010E67E File Offset: 0x0010C87E
		public static void RemoveAllInstances()
		{
			ProxyHubSelectorPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x0010E68A File Offset: 0x0010C88A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ProxyHubSelectorPerfCountersInstance(instanceName, (ProxyHubSelectorPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x0010E698 File Offset: 0x0010C898
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ProxyHubSelectorPerfCountersInstance(instanceName);
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x0010E6A0 File Offset: 0x0010C8A0
		public static void SetCategoryName(string categoryName)
		{
			if (ProxyHubSelectorPerfCounters.counters == null)
			{
				ProxyHubSelectorPerfCounters.CategoryName = categoryName;
				ProxyHubSelectorPerfCounters.counters = new PerformanceCounterMultipleInstance(ProxyHubSelectorPerfCounters.CategoryName, new CreateInstanceDelegate(ProxyHubSelectorPerfCounters.CreateInstance));
			}
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x0010E6CA File Offset: 0x0010C8CA
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ProxyHubSelectorPerfCounters.counters == null)
			{
				return;
			}
			ProxyHubSelectorPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400231D RID: 8989
		public static string CategoryName;

		// Token: 0x0400231E RID: 8990
		private static PerformanceCounterMultipleInstance counters;
	}
}

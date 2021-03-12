using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000035 RID: 53
	internal static class ForestDiscoveryPerfCounters
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0000E6FA File Offset: 0x0000C8FA
		public static ForestDiscoveryPerfCountersInstance GetInstance(string instanceName)
		{
			return (ForestDiscoveryPerfCountersInstance)ForestDiscoveryPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000E70C File Offset: 0x0000C90C
		public static void CloseInstance(string instanceName)
		{
			ForestDiscoveryPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000E719 File Offset: 0x0000C919
		public static bool InstanceExists(string instanceName)
		{
			return ForestDiscoveryPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000E726 File Offset: 0x0000C926
		public static string[] GetInstanceNames()
		{
			return ForestDiscoveryPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000E732 File Offset: 0x0000C932
		public static void RemoveInstance(string instanceName)
		{
			ForestDiscoveryPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000E73F File Offset: 0x0000C93F
		public static void ResetInstance(string instanceName)
		{
			ForestDiscoveryPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000E74C File Offset: 0x0000C94C
		public static void RemoveAllInstances()
		{
			ForestDiscoveryPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000E758 File Offset: 0x0000C958
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ForestDiscoveryPerfCountersInstance(instanceName, (ForestDiscoveryPerfCountersInstance)totalInstance);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000E766 File Offset: 0x0000C966
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ForestDiscoveryPerfCountersInstance(instanceName);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000E76E File Offset: 0x0000C96E
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ForestDiscoveryPerfCounters.counters == null)
			{
				return;
			}
			ForestDiscoveryPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400016A RID: 362
		public const string CategoryName = "MSExchange ADAccess Forest Discovery";

		// Token: 0x0400016B RID: 363
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange ADAccess Forest Discovery", new CreateInstanceDelegate(ForestDiscoveryPerfCounters.CreateInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x0200000E RID: 14
	internal static class AutodiscoverDatacenterPerformanceCounters
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000038A6 File Offset: 0x00001AA6
		public static AutodiscoverDatacenterPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (AutodiscoverDatacenterPerformanceCountersInstance)AutodiscoverDatacenterPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000038B8 File Offset: 0x00001AB8
		public static void CloseInstance(string instanceName)
		{
			AutodiscoverDatacenterPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000038C5 File Offset: 0x00001AC5
		public static bool InstanceExists(string instanceName)
		{
			return AutodiscoverDatacenterPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000038D2 File Offset: 0x00001AD2
		public static string[] GetInstanceNames()
		{
			return AutodiscoverDatacenterPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000038DE File Offset: 0x00001ADE
		public static void RemoveInstance(string instanceName)
		{
			AutodiscoverDatacenterPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000038EB File Offset: 0x00001AEB
		public static void ResetInstance(string instanceName)
		{
			AutodiscoverDatacenterPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000038F8 File Offset: 0x00001AF8
		public static void RemoveAllInstances()
		{
			AutodiscoverDatacenterPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003904 File Offset: 0x00001B04
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new AutodiscoverDatacenterPerformanceCountersInstance(instanceName, (AutodiscoverDatacenterPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003912 File Offset: 0x00001B12
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new AutodiscoverDatacenterPerformanceCountersInstance(instanceName);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000391A File Offset: 0x00001B1A
		public static AutodiscoverDatacenterPerformanceCountersInstance TotalInstance
		{
			get
			{
				return (AutodiscoverDatacenterPerformanceCountersInstance)AutodiscoverDatacenterPerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000392B File Offset: 0x00001B2B
		public static void GetPerfCounterInfo(XElement element)
		{
			if (AutodiscoverDatacenterPerformanceCounters.counters == null)
			{
				return;
			}
			AutodiscoverDatacenterPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000091 RID: 145
		public const string CategoryName = "MSExchangeAutodiscover:Datacenter";

		// Token: 0x04000092 RID: 146
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeAutodiscover:Datacenter", new CreateInstanceDelegate(AutodiscoverDatacenterPerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(AutodiscoverDatacenterPerformanceCounters.CreateTotalInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.TenantMonitoring
{
	// Token: 0x02000015 RID: 21
	internal static class MSExchangeTenantMonitoring
	{
		// Token: 0x0600006A RID: 106 RVA: 0x0000413A File Offset: 0x0000233A
		public static MSExchangeTenantMonitoringInstance GetInstance(string instanceName)
		{
			return (MSExchangeTenantMonitoringInstance)MSExchangeTenantMonitoring.counters.GetInstance(instanceName);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000414C File Offset: 0x0000234C
		public static void CloseInstance(string instanceName)
		{
			MSExchangeTenantMonitoring.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004159 File Offset: 0x00002359
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeTenantMonitoring.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004166 File Offset: 0x00002366
		public static string[] GetInstanceNames()
		{
			return MSExchangeTenantMonitoring.counters.GetInstanceNames();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004172 File Offset: 0x00002372
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeTenantMonitoring.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000417F File Offset: 0x0000237F
		public static void ResetInstance(string instanceName)
		{
			MSExchangeTenantMonitoring.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000418C File Offset: 0x0000238C
		public static void RemoveAllInstances()
		{
			MSExchangeTenantMonitoring.counters.RemoveAllInstances();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004198 File Offset: 0x00002398
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeTenantMonitoringInstance(instanceName, (MSExchangeTenantMonitoringInstance)totalInstance);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000041A6 File Offset: 0x000023A6
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeTenantMonitoringInstance(instanceName);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000041AE File Offset: 0x000023AE
		public static MSExchangeTenantMonitoringInstance TotalInstance
		{
			get
			{
				return (MSExchangeTenantMonitoringInstance)MSExchangeTenantMonitoring.counters.TotalInstance;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000041BF File Offset: 0x000023BF
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeTenantMonitoring.counters == null)
			{
				return;
			}
			MSExchangeTenantMonitoring.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400005F RID: 95
		public const string CategoryName = "MSExchangeTenantMonitoring";

		// Token: 0x04000060 RID: 96
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeTenantMonitoring", new CreateInstanceDelegate(MSExchangeTenantMonitoring.CreateInstance), new CreateTotalInstanceDelegate(MSExchangeTenantMonitoring.CreateTotalInstance));
	}
}

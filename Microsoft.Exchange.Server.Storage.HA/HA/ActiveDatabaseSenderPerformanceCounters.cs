using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x02000004 RID: 4
	internal static class ActiveDatabaseSenderPerformanceCounters
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002868 File Offset: 0x00000A68
		public static ActiveDatabaseSenderPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (ActiveDatabaseSenderPerformanceCountersInstance)ActiveDatabaseSenderPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000287A File Offset: 0x00000A7A
		public static void CloseInstance(string instanceName)
		{
			ActiveDatabaseSenderPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002887 File Offset: 0x00000A87
		public static bool InstanceExists(string instanceName)
		{
			return ActiveDatabaseSenderPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002894 File Offset: 0x00000A94
		public static string[] GetInstanceNames()
		{
			return ActiveDatabaseSenderPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000028A0 File Offset: 0x00000AA0
		public static void RemoveInstance(string instanceName)
		{
			ActiveDatabaseSenderPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000028AD File Offset: 0x00000AAD
		public static void ResetInstance(string instanceName)
		{
			ActiveDatabaseSenderPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000028BA File Offset: 0x00000ABA
		public static void RemoveAllInstances()
		{
			ActiveDatabaseSenderPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000028C6 File Offset: 0x00000AC6
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ActiveDatabaseSenderPerformanceCountersInstance(instanceName, (ActiveDatabaseSenderPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000028D4 File Offset: 0x00000AD4
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ActiveDatabaseSenderPerformanceCountersInstance(instanceName);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000028DC File Offset: 0x00000ADC
		public static ActiveDatabaseSenderPerformanceCountersInstance TotalInstance
		{
			get
			{
				return (ActiveDatabaseSenderPerformanceCountersInstance)ActiveDatabaseSenderPerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028ED File Offset: 0x00000AED
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ActiveDatabaseSenderPerformanceCounters.counters == null)
			{
				return;
			}
			ActiveDatabaseSenderPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000010 RID: 16
		public const string CategoryName = "MSExchangeIS HA Active Database Sender";

		// Token: 0x04000011 RID: 17
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeIS HA Active Database Sender", new CreateInstanceDelegate(ActiveDatabaseSenderPerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(ActiveDatabaseSenderPerformanceCounters.CreateTotalInstance));
	}
}

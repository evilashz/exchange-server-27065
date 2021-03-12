using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x02000002 RID: 2
	internal static class ActiveDatabasePerformanceCounters
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static ActiveDatabasePerformanceCountersInstance GetInstance(string instanceName)
		{
			return (ActiveDatabasePerformanceCountersInstance)ActiveDatabasePerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E2 File Offset: 0x000002E2
		public static void CloseInstance(string instanceName)
		{
			ActiveDatabasePerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020EF File Offset: 0x000002EF
		public static bool InstanceExists(string instanceName)
		{
			return ActiveDatabasePerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		public static string[] GetInstanceNames()
		{
			return ActiveDatabasePerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002108 File Offset: 0x00000308
		public static void RemoveInstance(string instanceName)
		{
			ActiveDatabasePerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002115 File Offset: 0x00000315
		public static void ResetInstance(string instanceName)
		{
			ActiveDatabasePerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002122 File Offset: 0x00000322
		public static void RemoveAllInstances()
		{
			ActiveDatabasePerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000212E File Offset: 0x0000032E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ActiveDatabasePerformanceCountersInstance(instanceName, (ActiveDatabasePerformanceCountersInstance)totalInstance);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ActiveDatabasePerformanceCountersInstance(instanceName);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002144 File Offset: 0x00000344
		public static ActiveDatabasePerformanceCountersInstance TotalInstance
		{
			get
			{
				return (ActiveDatabasePerformanceCountersInstance)ActiveDatabasePerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002155 File Offset: 0x00000355
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ActiveDatabasePerformanceCounters.counters == null)
			{
				return;
			}
			ActiveDatabasePerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000001 RID: 1
		public const string CategoryName = "MSExchangeIS HA Active Database";

		// Token: 0x04000002 RID: 2
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeIS HA Active Database", new CreateInstanceDelegate(ActiveDatabasePerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(ActiveDatabasePerformanceCounters.CreateTotalInstance));
	}
}

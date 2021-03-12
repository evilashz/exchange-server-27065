using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000005 RID: 5
	internal static class PerformanceCountersPerDatabase
	{
		// Token: 0x06000002 RID: 2 RVA: 0x0000240E File Offset: 0x0000060E
		public static PerformanceCountersPerDatabaseInstance GetInstance(string instanceName)
		{
			return (PerformanceCountersPerDatabaseInstance)PerformanceCountersPerDatabase.counters.GetInstance(instanceName);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002420 File Offset: 0x00000620
		public static void CloseInstance(string instanceName)
		{
			PerformanceCountersPerDatabase.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000242D File Offset: 0x0000062D
		public static bool InstanceExists(string instanceName)
		{
			return PerformanceCountersPerDatabase.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000243A File Offset: 0x0000063A
		public static string[] GetInstanceNames()
		{
			return PerformanceCountersPerDatabase.counters.GetInstanceNames();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002446 File Offset: 0x00000646
		public static void RemoveInstance(string instanceName)
		{
			PerformanceCountersPerDatabase.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002453 File Offset: 0x00000653
		public static void ResetInstance(string instanceName)
		{
			PerformanceCountersPerDatabase.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002460 File Offset: 0x00000660
		public static void RemoveAllInstances()
		{
			PerformanceCountersPerDatabase.counters.RemoveAllInstances();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000246C File Offset: 0x0000066C
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new PerformanceCountersPerDatabaseInstance(instanceName, (PerformanceCountersPerDatabaseInstance)totalInstance);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000247A File Offset: 0x0000067A
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new PerformanceCountersPerDatabaseInstance(instanceName);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002482 File Offset: 0x00000682
		public static PerformanceCountersPerDatabaseInstance TotalInstance
		{
			get
			{
				return (PerformanceCountersPerDatabaseInstance)PerformanceCountersPerDatabase.counters.TotalInstance;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002493 File Offset: 0x00000693
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PerformanceCountersPerDatabase.counters == null)
			{
				return;
			}
			PerformanceCountersPerDatabase.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400005A RID: 90
		public const string CategoryName = "MSExchange Assistants - Per Database";

		// Token: 0x0400005B RID: 91
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Assistants - Per Database", new CreateInstanceDelegate(PerformanceCountersPerDatabase.CreateInstance), new CreateTotalInstanceDelegate(PerformanceCountersPerDatabase.CreateTotalInstance));
	}
}

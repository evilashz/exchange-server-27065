using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200000A RID: 10
	internal static class StorePerDatabasePerformanceCounters
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00005B94 File Offset: 0x00003D94
		public static StorePerDatabasePerformanceCountersInstance GetInstance(string instanceName)
		{
			return (StorePerDatabasePerformanceCountersInstance)StorePerDatabasePerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005BA6 File Offset: 0x00003DA6
		public static void CloseInstance(string instanceName)
		{
			StorePerDatabasePerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005BB3 File Offset: 0x00003DB3
		public static bool InstanceExists(string instanceName)
		{
			return StorePerDatabasePerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005BC0 File Offset: 0x00003DC0
		public static string[] GetInstanceNames()
		{
			return StorePerDatabasePerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005BCC File Offset: 0x00003DCC
		public static void RemoveInstance(string instanceName)
		{
			StorePerDatabasePerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005BD9 File Offset: 0x00003DD9
		public static void ResetInstance(string instanceName)
		{
			StorePerDatabasePerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005BE6 File Offset: 0x00003DE6
		public static void RemoveAllInstances()
		{
			StorePerDatabasePerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005BF2 File Offset: 0x00003DF2
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new StorePerDatabasePerformanceCountersInstance(instanceName, (StorePerDatabasePerformanceCountersInstance)totalInstance);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005C00 File Offset: 0x00003E00
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new StorePerDatabasePerformanceCountersInstance(instanceName);
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00005C08 File Offset: 0x00003E08
		public static StorePerDatabasePerformanceCountersInstance TotalInstance
		{
			get
			{
				return (StorePerDatabasePerformanceCountersInstance)StorePerDatabasePerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005C19 File Offset: 0x00003E19
		public static void GetPerfCounterInfo(XElement element)
		{
			if (StorePerDatabasePerformanceCounters.counters == null)
			{
				return;
			}
			StorePerDatabasePerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040000B3 RID: 179
		public const string CategoryName = "MSExchangeIS Store";

		// Token: 0x040000B4 RID: 180
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeIS Store", new CreateInstanceDelegate(StorePerDatabasePerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(StorePerDatabasePerformanceCounters.CreateTotalInstance));
	}
}

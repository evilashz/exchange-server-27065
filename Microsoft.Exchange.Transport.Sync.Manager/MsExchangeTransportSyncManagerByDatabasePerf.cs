using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000060 RID: 96
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MsExchangeTransportSyncManagerByDatabasePerf
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x0001B894 File Offset: 0x00019A94
		public static MsExchangeTransportSyncManagerByDatabasePerfInstance GetInstance(string instanceName)
		{
			return (MsExchangeTransportSyncManagerByDatabasePerfInstance)MsExchangeTransportSyncManagerByDatabasePerf.counters.GetInstance(instanceName);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001B8A6 File Offset: 0x00019AA6
		public static void CloseInstance(string instanceName)
		{
			MsExchangeTransportSyncManagerByDatabasePerf.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001B8B3 File Offset: 0x00019AB3
		public static bool InstanceExists(string instanceName)
		{
			return MsExchangeTransportSyncManagerByDatabasePerf.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001B8C0 File Offset: 0x00019AC0
		public static string[] GetInstanceNames()
		{
			return MsExchangeTransportSyncManagerByDatabasePerf.counters.GetInstanceNames();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001B8CC File Offset: 0x00019ACC
		public static void RemoveInstance(string instanceName)
		{
			MsExchangeTransportSyncManagerByDatabasePerf.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001B8D9 File Offset: 0x00019AD9
		public static void ResetInstance(string instanceName)
		{
			MsExchangeTransportSyncManagerByDatabasePerf.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001B8E6 File Offset: 0x00019AE6
		public static void RemoveAllInstances()
		{
			MsExchangeTransportSyncManagerByDatabasePerf.counters.RemoveAllInstances();
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001B8F2 File Offset: 0x00019AF2
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MsExchangeTransportSyncManagerByDatabasePerfInstance(instanceName, (MsExchangeTransportSyncManagerByDatabasePerfInstance)totalInstance);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001B900 File Offset: 0x00019B00
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MsExchangeTransportSyncManagerByDatabasePerfInstance(instanceName);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001B908 File Offset: 0x00019B08
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MsExchangeTransportSyncManagerByDatabasePerf.counters == null)
			{
				return;
			}
			MsExchangeTransportSyncManagerByDatabasePerf.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000295 RID: 661
		public const string CategoryName = "MSExchange Transport Sync Manager By Database";

		// Token: 0x04000296 RID: 662
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Transport Sync Manager By Database", new CreateInstanceDelegate(MsExchangeTransportSyncManagerByDatabasePerf.CreateInstance));
	}
}

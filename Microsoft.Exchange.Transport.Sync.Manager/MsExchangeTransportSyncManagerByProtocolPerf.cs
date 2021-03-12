using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200005E RID: 94
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MsExchangeTransportSyncManagerByProtocolPerf
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x0001B0E6 File Offset: 0x000192E6
		public static MsExchangeTransportSyncManagerByProtocolPerfInstance GetInstance(string instanceName)
		{
			return (MsExchangeTransportSyncManagerByProtocolPerfInstance)MsExchangeTransportSyncManagerByProtocolPerf.counters.GetInstance(instanceName);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001B0F8 File Offset: 0x000192F8
		public static void CloseInstance(string instanceName)
		{
			MsExchangeTransportSyncManagerByProtocolPerf.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001B105 File Offset: 0x00019305
		public static bool InstanceExists(string instanceName)
		{
			return MsExchangeTransportSyncManagerByProtocolPerf.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001B112 File Offset: 0x00019312
		public static string[] GetInstanceNames()
		{
			return MsExchangeTransportSyncManagerByProtocolPerf.counters.GetInstanceNames();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001B11E File Offset: 0x0001931E
		public static void RemoveInstance(string instanceName)
		{
			MsExchangeTransportSyncManagerByProtocolPerf.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001B12B File Offset: 0x0001932B
		public static void ResetInstance(string instanceName)
		{
			MsExchangeTransportSyncManagerByProtocolPerf.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001B138 File Offset: 0x00019338
		public static void RemoveAllInstances()
		{
			MsExchangeTransportSyncManagerByProtocolPerf.counters.RemoveAllInstances();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001B144 File Offset: 0x00019344
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MsExchangeTransportSyncManagerByProtocolPerfInstance(instanceName, (MsExchangeTransportSyncManagerByProtocolPerfInstance)totalInstance);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001B152 File Offset: 0x00019352
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MsExchangeTransportSyncManagerByProtocolPerfInstance(instanceName);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001B15A File Offset: 0x0001935A
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MsExchangeTransportSyncManagerByProtocolPerf.counters == null)
			{
				return;
			}
			MsExchangeTransportSyncManagerByProtocolPerf.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000287 RID: 647
		public const string CategoryName = "MSExchange Transport Sync Manager By Protocol";

		// Token: 0x04000288 RID: 648
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Transport Sync Manager By Protocol", new CreateInstanceDelegate(MsExchangeTransportSyncManagerByProtocolPerf.CreateInstance));
	}
}

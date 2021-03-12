using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000062 RID: 98
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MsExchangeTransportSyncManagerBySLAPerf
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		public static MsExchangeTransportSyncManagerBySLAPerfInstance GetInstance(string instanceName)
		{
			return (MsExchangeTransportSyncManagerBySLAPerfInstance)MsExchangeTransportSyncManagerBySLAPerf.counters.GetInstance(instanceName);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001BBC6 File Offset: 0x00019DC6
		public static void CloseInstance(string instanceName)
		{
			MsExchangeTransportSyncManagerBySLAPerf.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001BBD3 File Offset: 0x00019DD3
		public static bool InstanceExists(string instanceName)
		{
			return MsExchangeTransportSyncManagerBySLAPerf.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001BBE0 File Offset: 0x00019DE0
		public static string[] GetInstanceNames()
		{
			return MsExchangeTransportSyncManagerBySLAPerf.counters.GetInstanceNames();
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001BBEC File Offset: 0x00019DEC
		public static void RemoveInstance(string instanceName)
		{
			MsExchangeTransportSyncManagerBySLAPerf.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001BBF9 File Offset: 0x00019DF9
		public static void ResetInstance(string instanceName)
		{
			MsExchangeTransportSyncManagerBySLAPerf.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001BC06 File Offset: 0x00019E06
		public static void RemoveAllInstances()
		{
			MsExchangeTransportSyncManagerBySLAPerf.counters.RemoveAllInstances();
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001BC12 File Offset: 0x00019E12
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MsExchangeTransportSyncManagerBySLAPerfInstance(instanceName, (MsExchangeTransportSyncManagerBySLAPerfInstance)totalInstance);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001BC20 File Offset: 0x00019E20
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MsExchangeTransportSyncManagerBySLAPerfInstance(instanceName);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001BC28 File Offset: 0x00019E28
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MsExchangeTransportSyncManagerBySLAPerf.counters == null)
			{
				return;
			}
			MsExchangeTransportSyncManagerBySLAPerf.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000299 RID: 665
		public const string CategoryName = "MSExchange Transport Sync Manager By SLA";

		// Token: 0x0400029A RID: 666
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Transport Sync Manager By SLA", new CreateInstanceDelegate(MsExchangeTransportSyncManagerBySLAPerf.CreateInstance));
	}
}

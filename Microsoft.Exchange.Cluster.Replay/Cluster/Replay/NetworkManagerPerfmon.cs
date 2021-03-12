using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200038B RID: 907
	internal static class NetworkManagerPerfmon
	{
		// Token: 0x06002457 RID: 9303 RVA: 0x000AA95E File Offset: 0x000A8B5E
		public static NetworkManagerPerfmonInstance GetInstance(string instanceName)
		{
			return (NetworkManagerPerfmonInstance)NetworkManagerPerfmon.counters.GetInstance(instanceName);
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000AA970 File Offset: 0x000A8B70
		public static void CloseInstance(string instanceName)
		{
			NetworkManagerPerfmon.counters.CloseInstance(instanceName);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000AA97D File Offset: 0x000A8B7D
		public static bool InstanceExists(string instanceName)
		{
			return NetworkManagerPerfmon.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000AA98A File Offset: 0x000A8B8A
		public static string[] GetInstanceNames()
		{
			return NetworkManagerPerfmon.counters.GetInstanceNames();
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000AA996 File Offset: 0x000A8B96
		public static void RemoveInstance(string instanceName)
		{
			NetworkManagerPerfmon.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000AA9A3 File Offset: 0x000A8BA3
		public static void ResetInstance(string instanceName)
		{
			NetworkManagerPerfmon.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000AA9B0 File Offset: 0x000A8BB0
		public static void RemoveAllInstances()
		{
			NetworkManagerPerfmon.counters.RemoveAllInstances();
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000AA9BC File Offset: 0x000A8BBC
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new NetworkManagerPerfmonInstance(instanceName, (NetworkManagerPerfmonInstance)totalInstance);
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000AA9CA File Offset: 0x000A8BCA
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new NetworkManagerPerfmonInstance(instanceName);
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x000AA9D2 File Offset: 0x000A8BD2
		public static NetworkManagerPerfmonInstance TotalInstance
		{
			get
			{
				return (NetworkManagerPerfmonInstance)NetworkManagerPerfmon.counters.TotalInstance;
			}
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000AA9E3 File Offset: 0x000A8BE3
		public static void GetPerfCounterInfo(XElement element)
		{
			if (NetworkManagerPerfmon.counters == null)
			{
				return;
			}
			NetworkManagerPerfmon.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04001080 RID: 4224
		public const string CategoryName = "MSExchange Network Manager";

		// Token: 0x04001081 RID: 4225
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Network Manager", new CreateInstanceDelegate(NetworkManagerPerfmon.CreateInstance), new CreateTotalInstanceDelegate(NetworkManagerPerfmon.CreateTotalInstance));
	}
}

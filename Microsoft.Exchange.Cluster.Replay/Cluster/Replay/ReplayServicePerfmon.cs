using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200038E RID: 910
	internal static class ReplayServicePerfmon
	{
		// Token: 0x06002468 RID: 9320 RVA: 0x000AB18E File Offset: 0x000A938E
		public static ReplayServicePerfmonInstance GetInstance(string instanceName)
		{
			return (ReplayServicePerfmonInstance)ReplayServicePerfmon.counters.GetInstance(instanceName);
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000AB1A0 File Offset: 0x000A93A0
		public static void CloseInstance(string instanceName)
		{
			ReplayServicePerfmon.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000AB1AD File Offset: 0x000A93AD
		public static bool InstanceExists(string instanceName)
		{
			return ReplayServicePerfmon.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000AB1BA File Offset: 0x000A93BA
		public static string[] GetInstanceNames()
		{
			return ReplayServicePerfmon.counters.GetInstanceNames();
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000AB1C6 File Offset: 0x000A93C6
		public static void RemoveInstance(string instanceName)
		{
			ReplayServicePerfmon.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000AB1D3 File Offset: 0x000A93D3
		public static void ResetInstance(string instanceName)
		{
			ReplayServicePerfmon.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000AB1E0 File Offset: 0x000A93E0
		public static void RemoveAllInstances()
		{
			ReplayServicePerfmon.counters.RemoveAllInstances();
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000AB1EC File Offset: 0x000A93EC
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ReplayServicePerfmonInstance(instanceName, (ReplayServicePerfmonInstance)totalInstance);
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000AB1FA File Offset: 0x000A93FA
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ReplayServicePerfmonInstance(instanceName);
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x000AB202 File Offset: 0x000A9402
		public static ReplayServicePerfmonInstance TotalInstance
		{
			get
			{
				return (ReplayServicePerfmonInstance)ReplayServicePerfmon.counters.TotalInstance;
			}
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000AB213 File Offset: 0x000A9413
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ReplayServicePerfmon.counters == null)
			{
				return;
			}
			ReplayServicePerfmon.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400109A RID: 4250
		public const string CategoryName = "MSExchange Replication";

		// Token: 0x0400109B RID: 4251
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Replication", new CreateInstanceDelegate(ReplayServicePerfmon.CreateInstance), new CreateTotalInstanceDelegate(ReplayServicePerfmon.CreateTotalInstance));
	}
}

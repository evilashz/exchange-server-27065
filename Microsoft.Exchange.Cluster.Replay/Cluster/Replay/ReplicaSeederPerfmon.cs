using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000390 RID: 912
	internal static class ReplicaSeederPerfmon
	{
		// Token: 0x06002477 RID: 9335 RVA: 0x000AC44C File Offset: 0x000AA64C
		public static ReplicaSeederPerfmonInstance GetInstance(string instanceName)
		{
			return (ReplicaSeederPerfmonInstance)ReplicaSeederPerfmon.counters.GetInstance(instanceName);
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000AC45E File Offset: 0x000AA65E
		public static void CloseInstance(string instanceName)
		{
			ReplicaSeederPerfmon.counters.CloseInstance(instanceName);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000AC46B File Offset: 0x000AA66B
		public static bool InstanceExists(string instanceName)
		{
			return ReplicaSeederPerfmon.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000AC478 File Offset: 0x000AA678
		public static string[] GetInstanceNames()
		{
			return ReplicaSeederPerfmon.counters.GetInstanceNames();
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000AC484 File Offset: 0x000AA684
		public static void RemoveInstance(string instanceName)
		{
			ReplicaSeederPerfmon.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000AC491 File Offset: 0x000AA691
		public static void ResetInstance(string instanceName)
		{
			ReplicaSeederPerfmon.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000AC49E File Offset: 0x000AA69E
		public static void RemoveAllInstances()
		{
			ReplicaSeederPerfmon.counters.RemoveAllInstances();
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000AC4AA File Offset: 0x000AA6AA
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ReplicaSeederPerfmonInstance(instanceName, (ReplicaSeederPerfmonInstance)totalInstance);
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000AC4B8 File Offset: 0x000AA6B8
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ReplicaSeederPerfmonInstance(instanceName);
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000AC4C0 File Offset: 0x000AA6C0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ReplicaSeederPerfmon.counters == null)
			{
				return;
			}
			ReplicaSeederPerfmon.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040010C7 RID: 4295
		public const string CategoryName = "MSExchange Replica Seeder";

		// Token: 0x040010C8 RID: 4296
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Replica Seeder", new CreateInstanceDelegate(ReplicaSeederPerfmon.CreateInstance));
	}
}

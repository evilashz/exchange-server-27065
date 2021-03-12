using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000385 RID: 901
	internal static class ActiveManagerDagNamePerfmon
	{
		// Token: 0x06002446 RID: 9286 RVA: 0x000A9AB0 File Offset: 0x000A7CB0
		public static ActiveManagerDagNamePerfmonInstance GetInstance(string instanceName)
		{
			return (ActiveManagerDagNamePerfmonInstance)ActiveManagerDagNamePerfmon.counters.GetInstance(instanceName);
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000A9AC2 File Offset: 0x000A7CC2
		public static void CloseInstance(string instanceName)
		{
			ActiveManagerDagNamePerfmon.counters.CloseInstance(instanceName);
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000A9ACF File Offset: 0x000A7CCF
		public static bool InstanceExists(string instanceName)
		{
			return ActiveManagerDagNamePerfmon.counters.InstanceExists(instanceName);
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000A9ADC File Offset: 0x000A7CDC
		public static string[] GetInstanceNames()
		{
			return ActiveManagerDagNamePerfmon.counters.GetInstanceNames();
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000A9AE8 File Offset: 0x000A7CE8
		public static void RemoveInstance(string instanceName)
		{
			ActiveManagerDagNamePerfmon.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000A9AF5 File Offset: 0x000A7CF5
		public static void ResetInstance(string instanceName)
		{
			ActiveManagerDagNamePerfmon.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000A9B02 File Offset: 0x000A7D02
		public static void RemoveAllInstances()
		{
			ActiveManagerDagNamePerfmon.counters.RemoveAllInstances();
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000A9B0E File Offset: 0x000A7D0E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ActiveManagerDagNamePerfmonInstance(instanceName, (ActiveManagerDagNamePerfmonInstance)totalInstance);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000A9B1C File Offset: 0x000A7D1C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ActiveManagerDagNamePerfmonInstance(instanceName);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000A9B24 File Offset: 0x000A7D24
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ActiveManagerDagNamePerfmon.counters == null)
			{
				return;
			}
			ActiveManagerDagNamePerfmon.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000F6B RID: 3947
		public const string CategoryName = "MSExchange Active Manager Dag Name Instance";

		// Token: 0x04000F6C RID: 3948
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Active Manager Dag Name Instance", new CreateInstanceDelegate(ActiveManagerDagNamePerfmon.CreateInstance));
	}
}

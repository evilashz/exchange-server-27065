using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000383 RID: 899
	internal static class ActiveManagerPerfmon
	{
		// Token: 0x06002437 RID: 9271 RVA: 0x000A975D File Offset: 0x000A795D
		public static ActiveManagerPerfmonInstance GetInstance(string instanceName)
		{
			return (ActiveManagerPerfmonInstance)ActiveManagerPerfmon.counters.GetInstance(instanceName);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000A976F File Offset: 0x000A796F
		public static void CloseInstance(string instanceName)
		{
			ActiveManagerPerfmon.counters.CloseInstance(instanceName);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000A977C File Offset: 0x000A797C
		public static bool InstanceExists(string instanceName)
		{
			return ActiveManagerPerfmon.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000A9789 File Offset: 0x000A7989
		public static string[] GetInstanceNames()
		{
			return ActiveManagerPerfmon.counters.GetInstanceNames();
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000A9795 File Offset: 0x000A7995
		public static void RemoveInstance(string instanceName)
		{
			ActiveManagerPerfmon.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000A97A2 File Offset: 0x000A79A2
		public static void ResetInstance(string instanceName)
		{
			ActiveManagerPerfmon.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000A97AF File Offset: 0x000A79AF
		public static void RemoveAllInstances()
		{
			ActiveManagerPerfmon.counters.RemoveAllInstances();
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000A97BB File Offset: 0x000A79BB
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ActiveManagerPerfmonInstance(instanceName, (ActiveManagerPerfmonInstance)totalInstance);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000A97C9 File Offset: 0x000A79C9
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ActiveManagerPerfmonInstance(instanceName);
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x000A97D1 File Offset: 0x000A79D1
		public static ActiveManagerPerfmonInstance TotalInstance
		{
			get
			{
				return (ActiveManagerPerfmonInstance)ActiveManagerPerfmon.counters.TotalInstance;
			}
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000A97E2 File Offset: 0x000A79E2
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ActiveManagerPerfmon.counters == null)
			{
				return;
			}
			ActiveManagerPerfmon.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000F67 RID: 3943
		public const string CategoryName = "MSExchange Active Manager";

		// Token: 0x04000F68 RID: 3944
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Active Manager", new CreateInstanceDelegate(ActiveManagerPerfmon.CreateInstance), new CreateTotalInstanceDelegate(ActiveManagerPerfmon.CreateTotalInstance));
	}
}

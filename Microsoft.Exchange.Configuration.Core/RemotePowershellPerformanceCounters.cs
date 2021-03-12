using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000036 RID: 54
	internal static class RemotePowershellPerformanceCounters
	{
		// Token: 0x0600012C RID: 300 RVA: 0x000076AC File Offset: 0x000058AC
		public static RemotePowershellPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (RemotePowershellPerformanceCountersInstance)RemotePowershellPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000076BE File Offset: 0x000058BE
		public static void CloseInstance(string instanceName)
		{
			RemotePowershellPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000076CB File Offset: 0x000058CB
		public static bool InstanceExists(string instanceName)
		{
			return RemotePowershellPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000076D8 File Offset: 0x000058D8
		public static string[] GetInstanceNames()
		{
			return RemotePowershellPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000076E4 File Offset: 0x000058E4
		public static void RemoveInstance(string instanceName)
		{
			RemotePowershellPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000076F1 File Offset: 0x000058F1
		public static void ResetInstance(string instanceName)
		{
			RemotePowershellPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000076FE File Offset: 0x000058FE
		public static void RemoveAllInstances()
		{
			RemotePowershellPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000770A File Offset: 0x0000590A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new RemotePowershellPerformanceCountersInstance(instanceName, (RemotePowershellPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007718 File Offset: 0x00005918
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new RemotePowershellPerformanceCountersInstance(instanceName);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00007720 File Offset: 0x00005920
		public static RemotePowershellPerformanceCountersInstance TotalInstance
		{
			get
			{
				return (RemotePowershellPerformanceCountersInstance)RemotePowershellPerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007731 File Offset: 0x00005931
		public static void GetPerfCounterInfo(XElement element)
		{
			if (RemotePowershellPerformanceCounters.counters == null)
			{
				return;
			}
			RemotePowershellPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040000CB RID: 203
		public const string CategoryName = "MSExchangeRemotePowershell";

		// Token: 0x040000CC RID: 204
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeRemotePowershell", new CreateInstanceDelegate(RemotePowershellPerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(RemotePowershellPerformanceCounters.CreateTotalInstance));
	}
}

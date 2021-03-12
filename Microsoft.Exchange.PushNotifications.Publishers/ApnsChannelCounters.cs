using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000108 RID: 264
	internal static class ApnsChannelCounters
	{
		// Token: 0x0600089C RID: 2204 RVA: 0x00019E11 File Offset: 0x00018011
		public static ApnsChannelCountersInstance GetInstance(string instanceName)
		{
			return (ApnsChannelCountersInstance)ApnsChannelCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00019E23 File Offset: 0x00018023
		public static void CloseInstance(string instanceName)
		{
			ApnsChannelCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00019E30 File Offset: 0x00018030
		public static bool InstanceExists(string instanceName)
		{
			return ApnsChannelCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00019E3D File Offset: 0x0001803D
		public static string[] GetInstanceNames()
		{
			return ApnsChannelCounters.counters.GetInstanceNames();
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00019E49 File Offset: 0x00018049
		public static void RemoveInstance(string instanceName)
		{
			ApnsChannelCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00019E56 File Offset: 0x00018056
		public static void ResetInstance(string instanceName)
		{
			ApnsChannelCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00019E63 File Offset: 0x00018063
		public static void RemoveAllInstances()
		{
			ApnsChannelCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00019E6F File Offset: 0x0001806F
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ApnsChannelCountersInstance(instanceName, (ApnsChannelCountersInstance)totalInstance);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00019E7D File Offset: 0x0001807D
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ApnsChannelCountersInstance(instanceName);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00019E85 File Offset: 0x00018085
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ApnsChannelCounters.counters == null)
			{
				return;
			}
			ApnsChannelCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040004C3 RID: 1219
		public const string CategoryName = "MSExchange Push Notifications Apns Channel";

		// Token: 0x040004C4 RID: 1220
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Push Notifications Apns Channel", new CreateInstanceDelegate(ApnsChannelCounters.CreateInstance));
	}
}

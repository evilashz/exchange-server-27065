using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200010D RID: 269
	internal static class PublisherCounters
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x0001AA56 File Offset: 0x00018C56
		public static PublisherCountersInstance GetInstance(string instanceName)
		{
			return (PublisherCountersInstance)PublisherCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001AA68 File Offset: 0x00018C68
		public static void CloseInstance(string instanceName)
		{
			PublisherCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001AA75 File Offset: 0x00018C75
		public static bool InstanceExists(string instanceName)
		{
			return PublisherCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001AA82 File Offset: 0x00018C82
		public static string[] GetInstanceNames()
		{
			return PublisherCounters.counters.GetInstanceNames();
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001AA8E File Offset: 0x00018C8E
		public static void RemoveInstance(string instanceName)
		{
			PublisherCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001AA9B File Offset: 0x00018C9B
		public static void ResetInstance(string instanceName)
		{
			PublisherCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		public static void RemoveAllInstances()
		{
			PublisherCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001AAB4 File Offset: 0x00018CB4
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new PublisherCountersInstance(instanceName, (PublisherCountersInstance)totalInstance);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001AAC2 File Offset: 0x00018CC2
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new PublisherCountersInstance(instanceName);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001AACA File Offset: 0x00018CCA
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PublisherCounters.counters == null)
			{
				return;
			}
			PublisherCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040004ED RID: 1261
		public const string CategoryName = "MSExchange Push Notifications Publishers";

		// Token: 0x040004EE RID: 1262
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Push Notifications Publishers", new CreateInstanceDelegate(PublisherCounters.CreateInstance));
	}
}

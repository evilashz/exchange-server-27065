using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000007 RID: 7
	internal static class MessageDepotPerfCounters
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000036FD File Offset: 0x000018FD
		public static MessageDepotPerfCountersInstance GetInstance(string instanceName)
		{
			return (MessageDepotPerfCountersInstance)MessageDepotPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000370F File Offset: 0x0000190F
		public static void CloseInstance(string instanceName)
		{
			MessageDepotPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000371C File Offset: 0x0000191C
		public static bool InstanceExists(string instanceName)
		{
			return MessageDepotPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003729 File Offset: 0x00001929
		public static string[] GetInstanceNames()
		{
			return MessageDepotPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003735 File Offset: 0x00001935
		public static void RemoveInstance(string instanceName)
		{
			MessageDepotPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003742 File Offset: 0x00001942
		public static void ResetInstance(string instanceName)
		{
			MessageDepotPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000374F File Offset: 0x0000194F
		public static void RemoveAllInstances()
		{
			MessageDepotPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000375B File Offset: 0x0000195B
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MessageDepotPerfCountersInstance(instanceName, (MessageDepotPerfCountersInstance)totalInstance);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003769 File Offset: 0x00001969
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MessageDepotPerfCountersInstance(instanceName);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003771 File Offset: 0x00001971
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MessageDepotPerfCounters.counters == null)
			{
				return;
			}
			MessageDepotPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400002F RID: 47
		public const string CategoryName = "MSExchangeTransport MessageDepot";

		// Token: 0x04000030 RID: 48
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeTransport MessageDepot", new CreateInstanceDelegate(MessageDepotPerfCounters.CreateInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Performance
{
	// Token: 0x02000039 RID: 57
	internal static class TransportCtsFlowCounters
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000F4AE File Offset: 0x0000D6AE
		public static TransportCtsFlowCountersInstance GetInstance(string instanceName)
		{
			return (TransportCtsFlowCountersInstance)TransportCtsFlowCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		public static void CloseInstance(string instanceName)
		{
			TransportCtsFlowCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000F4CD File Offset: 0x0000D6CD
		public static bool InstanceExists(string instanceName)
		{
			return TransportCtsFlowCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000F4DA File Offset: 0x0000D6DA
		public static string[] GetInstanceNames()
		{
			return TransportCtsFlowCounters.counters.GetInstanceNames();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000F4E6 File Offset: 0x0000D6E6
		public static void RemoveInstance(string instanceName)
		{
			TransportCtsFlowCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000F4F3 File Offset: 0x0000D6F3
		public static void ResetInstance(string instanceName)
		{
			TransportCtsFlowCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000F500 File Offset: 0x0000D700
		public static void RemoveAllInstances()
		{
			TransportCtsFlowCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000F50C File Offset: 0x0000D70C
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new TransportCtsFlowCountersInstance(instanceName, (TransportCtsFlowCountersInstance)totalInstance);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000F51A File Offset: 0x0000D71A
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new TransportCtsFlowCountersInstance(instanceName);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000F522 File Offset: 0x0000D722
		public static void GetPerfCounterInfo(XElement element)
		{
			if (TransportCtsFlowCounters.counters == null)
			{
				return;
			}
			TransportCtsFlowCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000149 RID: 329
		public const string CategoryName = "MSExchangeSearch Transport CTS Flow";

		// Token: 0x0400014A RID: 330
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeSearch Transport CTS Flow", new CreateInstanceDelegate(TransportCtsFlowCounters.CreateInstance));
	}
}

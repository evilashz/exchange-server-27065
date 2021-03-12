using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ExchangeTopology
{
	// Token: 0x02000A40 RID: 2624
	internal static class ExchangeTopologyPerformanceCounters
	{
		// Token: 0x0600783A RID: 30778 RVA: 0x0018D2F7 File Offset: 0x0018B4F7
		public static ExchangeTopologyPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (ExchangeTopologyPerformanceCountersInstance)ExchangeTopologyPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600783B RID: 30779 RVA: 0x0018D309 File Offset: 0x0018B509
		public static void CloseInstance(string instanceName)
		{
			ExchangeTopologyPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600783C RID: 30780 RVA: 0x0018D316 File Offset: 0x0018B516
		public static bool InstanceExists(string instanceName)
		{
			return ExchangeTopologyPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600783D RID: 30781 RVA: 0x0018D323 File Offset: 0x0018B523
		public static string[] GetInstanceNames()
		{
			return ExchangeTopologyPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x0600783E RID: 30782 RVA: 0x0018D32F File Offset: 0x0018B52F
		public static void RemoveInstance(string instanceName)
		{
			ExchangeTopologyPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600783F RID: 30783 RVA: 0x0018D33C File Offset: 0x0018B53C
		public static void ResetInstance(string instanceName)
		{
			ExchangeTopologyPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06007840 RID: 30784 RVA: 0x0018D349 File Offset: 0x0018B549
		public static void RemoveAllInstances()
		{
			ExchangeTopologyPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06007841 RID: 30785 RVA: 0x0018D355 File Offset: 0x0018B555
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ExchangeTopologyPerformanceCountersInstance(instanceName, (ExchangeTopologyPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x06007842 RID: 30786 RVA: 0x0018D363 File Offset: 0x0018B563
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ExchangeTopologyPerformanceCountersInstance(instanceName);
		}

		// Token: 0x06007843 RID: 30787 RVA: 0x0018D36B File Offset: 0x0018B56B
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ExchangeTopologyPerformanceCounters.counters == null)
			{
				return;
			}
			ExchangeTopologyPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04004F07 RID: 20231
		public const string CategoryName = "MSExchange Topology";

		// Token: 0x04004F08 RID: 20232
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Topology", new CreateInstanceDelegate(ExchangeTopologyPerformanceCounters.CreateInstance));
	}
}

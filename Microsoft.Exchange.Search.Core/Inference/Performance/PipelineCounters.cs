using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.Performance
{
	// Token: 0x020000C8 RID: 200
	internal static class PipelineCounters
	{
		// Token: 0x06000617 RID: 1559 RVA: 0x000131B5 File Offset: 0x000113B5
		public static PipelineCountersInstance GetInstance(string instanceName)
		{
			return (PipelineCountersInstance)PipelineCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000131C7 File Offset: 0x000113C7
		public static void CloseInstance(string instanceName)
		{
			PipelineCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000131D4 File Offset: 0x000113D4
		public static bool InstanceExists(string instanceName)
		{
			return PipelineCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000131E1 File Offset: 0x000113E1
		public static string[] GetInstanceNames()
		{
			return PipelineCounters.counters.GetInstanceNames();
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x000131ED File Offset: 0x000113ED
		public static void RemoveInstance(string instanceName)
		{
			PipelineCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000131FA File Offset: 0x000113FA
		public static void ResetInstance(string instanceName)
		{
			PipelineCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00013207 File Offset: 0x00011407
		public static void RemoveAllInstances()
		{
			PipelineCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00013213 File Offset: 0x00011413
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new PipelineCountersInstance(instanceName, (PipelineCountersInstance)totalInstance);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00013221 File Offset: 0x00011421
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new PipelineCountersInstance(instanceName);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00013229 File Offset: 0x00011429
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PipelineCounters.counters == null)
			{
				return;
			}
			PipelineCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040002D0 RID: 720
		public const string CategoryName = "MSExchangeInference Pipeline";

		// Token: 0x040002D1 RID: 721
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeInference Pipeline", new CreateInstanceDelegate(PipelineCounters.CreateInstance));
	}
}

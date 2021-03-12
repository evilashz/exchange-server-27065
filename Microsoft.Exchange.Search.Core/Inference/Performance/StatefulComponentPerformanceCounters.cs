using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.Performance
{
	// Token: 0x020000CD RID: 205
	internal static class StatefulComponentPerformanceCounters
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x00013B0C File Offset: 0x00011D0C
		public static StatefulComponentPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (StatefulComponentPerformanceCountersInstance)StatefulComponentPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00013B1E File Offset: 0x00011D1E
		public static void CloseInstance(string instanceName)
		{
			StatefulComponentPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00013B2B File Offset: 0x00011D2B
		public static bool InstanceExists(string instanceName)
		{
			return StatefulComponentPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00013B38 File Offset: 0x00011D38
		public static string[] GetInstanceNames()
		{
			return StatefulComponentPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00013B44 File Offset: 0x00011D44
		public static void RemoveInstance(string instanceName)
		{
			StatefulComponentPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00013B51 File Offset: 0x00011D51
		public static void ResetInstance(string instanceName)
		{
			StatefulComponentPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00013B5E File Offset: 0x00011D5E
		public static void RemoveAllInstances()
		{
			StatefulComponentPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00013B6A File Offset: 0x00011D6A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new StatefulComponentPerformanceCountersInstance(instanceName, (StatefulComponentPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00013B78 File Offset: 0x00011D78
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new StatefulComponentPerformanceCountersInstance(instanceName);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00013B80 File Offset: 0x00011D80
		public static void GetPerfCounterInfo(XElement element)
		{
			if (StatefulComponentPerformanceCounters.counters == null)
			{
				return;
			}
			StatefulComponentPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000310 RID: 784
		public const string CategoryName = "MSExchangeInference StatefulComponent";

		// Token: 0x04000311 RID: 785
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeInference StatefulComponent", new CreateInstanceDelegate(StatefulComponentPerformanceCounters.CreateInstance));
	}
}

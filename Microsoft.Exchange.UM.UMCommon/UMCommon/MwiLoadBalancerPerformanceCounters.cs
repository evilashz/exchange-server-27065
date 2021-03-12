using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000235 RID: 565
	internal static class MwiLoadBalancerPerformanceCounters
	{
		// Token: 0x060011B7 RID: 4535 RVA: 0x0003C7F0 File Offset: 0x0003A9F0
		public static MwiLoadBalancerPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (MwiLoadBalancerPerformanceCountersInstance)MwiLoadBalancerPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0003C802 File Offset: 0x0003AA02
		public static void CloseInstance(string instanceName)
		{
			MwiLoadBalancerPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0003C80F File Offset: 0x0003AA0F
		public static bool InstanceExists(string instanceName)
		{
			return MwiLoadBalancerPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0003C81C File Offset: 0x0003AA1C
		public static string[] GetInstanceNames()
		{
			return MwiLoadBalancerPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0003C828 File Offset: 0x0003AA28
		public static void RemoveInstance(string instanceName)
		{
			MwiLoadBalancerPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0003C835 File Offset: 0x0003AA35
		public static void ResetInstance(string instanceName)
		{
			MwiLoadBalancerPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0003C842 File Offset: 0x0003AA42
		public static void RemoveAllInstances()
		{
			MwiLoadBalancerPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0003C84E File Offset: 0x0003AA4E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MwiLoadBalancerPerformanceCountersInstance(instanceName, (MwiLoadBalancerPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0003C85C File Offset: 0x0003AA5C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MwiLoadBalancerPerformanceCountersInstance(instanceName);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0003C864 File Offset: 0x0003AA64
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MwiLoadBalancerPerformanceCounters.counters == null)
			{
				return;
			}
			MwiLoadBalancerPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000B71 RID: 2929
		public const string CategoryName = "MSExchangeUMMessageWaitingIndicator";

		// Token: 0x04000B72 RID: 2930
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeUMMessageWaitingIndicator", new CreateInstanceDelegate(MwiLoadBalancerPerformanceCounters.CreateInstance));
	}
}

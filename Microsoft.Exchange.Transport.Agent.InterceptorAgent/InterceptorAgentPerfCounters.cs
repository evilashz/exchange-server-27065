using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000028 RID: 40
	internal static class InterceptorAgentPerfCounters
	{
		// Token: 0x06000164 RID: 356 RVA: 0x000077D3 File Offset: 0x000059D3
		public static InterceptorAgentPerfCountersInstance GetInstance(string instanceName)
		{
			return (InterceptorAgentPerfCountersInstance)InterceptorAgentPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000077E5 File Offset: 0x000059E5
		public static void CloseInstance(string instanceName)
		{
			InterceptorAgentPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000077F2 File Offset: 0x000059F2
		public static bool InstanceExists(string instanceName)
		{
			return InterceptorAgentPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000077FF File Offset: 0x000059FF
		public static string[] GetInstanceNames()
		{
			return InterceptorAgentPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000780B File Offset: 0x00005A0B
		public static void RemoveInstance(string instanceName)
		{
			InterceptorAgentPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007818 File Offset: 0x00005A18
		public static void ResetInstance(string instanceName)
		{
			InterceptorAgentPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007825 File Offset: 0x00005A25
		public static void RemoveAllInstances()
		{
			InterceptorAgentPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007831 File Offset: 0x00005A31
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new InterceptorAgentPerfCountersInstance(instanceName, (InterceptorAgentPerfCountersInstance)totalInstance);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000783F File Offset: 0x00005A3F
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new InterceptorAgentPerfCountersInstance(instanceName);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007847 File Offset: 0x00005A47
		public static void GetPerfCounterInfo(XElement element)
		{
			if (InterceptorAgentPerfCounters.counters == null)
			{
				return;
			}
			InterceptorAgentPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040000CE RID: 206
		public const string CategoryName = "MSExchange Interceptor Agent";

		// Token: 0x040000CF RID: 207
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Interceptor Agent", new CreateInstanceDelegate(InterceptorAgentPerfCounters.CreateInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Mserve.Perf
{
	// Token: 0x0200017C RID: 380
	internal static class MserveWebServiceCounters
	{
		// Token: 0x06000926 RID: 2342 RVA: 0x00019939 File Offset: 0x00017B39
		public static MserveWebServiceCountersInstance GetInstance(string instanceName)
		{
			return (MserveWebServiceCountersInstance)MserveWebServiceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001994B File Offset: 0x00017B4B
		public static void CloseInstance(string instanceName)
		{
			MserveWebServiceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00019958 File Offset: 0x00017B58
		public static bool InstanceExists(string instanceName)
		{
			return MserveWebServiceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00019965 File Offset: 0x00017B65
		public static string[] GetInstanceNames()
		{
			return MserveWebServiceCounters.counters.GetInstanceNames();
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00019971 File Offset: 0x00017B71
		public static void RemoveInstance(string instanceName)
		{
			MserveWebServiceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001997E File Offset: 0x00017B7E
		public static void ResetInstance(string instanceName)
		{
			MserveWebServiceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001998B File Offset: 0x00017B8B
		public static void RemoveAllInstances()
		{
			MserveWebServiceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00019997 File Offset: 0x00017B97
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MserveWebServiceCountersInstance(instanceName, (MserveWebServiceCountersInstance)totalInstance);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000199A5 File Offset: 0x00017BA5
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MserveWebServiceCountersInstance(instanceName);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000199AD File Offset: 0x00017BAD
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MserveWebServiceCounters.counters == null)
			{
				return;
			}
			MserveWebServiceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000649 RID: 1609
		public const string CategoryName = "MSExchange MserveWebService";

		// Token: 0x0400064A RID: 1610
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange MserveWebService", new CreateInstanceDelegate(MserveWebServiceCounters.CreateInstance));
	}
}

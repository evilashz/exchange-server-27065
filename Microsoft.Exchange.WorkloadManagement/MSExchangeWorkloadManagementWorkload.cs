using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000049 RID: 73
	internal static class MSExchangeWorkloadManagementWorkload
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0000D594 File Offset: 0x0000B794
		public static MSExchangeWorkloadManagementWorkloadInstance GetInstance(string instanceName)
		{
			return (MSExchangeWorkloadManagementWorkloadInstance)MSExchangeWorkloadManagementWorkload.counters.GetInstance(instanceName);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
		public static void CloseInstance(string instanceName)
		{
			MSExchangeWorkloadManagementWorkload.counters.CloseInstance(instanceName);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000D5B3 File Offset: 0x0000B7B3
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeWorkloadManagementWorkload.counters.InstanceExists(instanceName);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		public static string[] GetInstanceNames()
		{
			return MSExchangeWorkloadManagementWorkload.counters.GetInstanceNames();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeWorkloadManagementWorkload.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000D5D9 File Offset: 0x0000B7D9
		public static void ResetInstance(string instanceName)
		{
			MSExchangeWorkloadManagementWorkload.counters.ResetInstance(instanceName);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000D5E6 File Offset: 0x0000B7E6
		public static void RemoveAllInstances()
		{
			MSExchangeWorkloadManagementWorkload.counters.RemoveAllInstances();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000D5F2 File Offset: 0x0000B7F2
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeWorkloadManagementWorkloadInstance(instanceName, (MSExchangeWorkloadManagementWorkloadInstance)totalInstance);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000D600 File Offset: 0x0000B800
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeWorkloadManagementWorkloadInstance(instanceName);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000D608 File Offset: 0x0000B808
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeWorkloadManagementWorkload.counters == null)
			{
				return;
			}
			MSExchangeWorkloadManagementWorkload.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400017A RID: 378
		public const string CategoryName = "MSExchange WorkloadManagement Workloads";

		// Token: 0x0400017B RID: 379
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange WorkloadManagement Workloads", new CreateInstanceDelegate(MSExchangeWorkloadManagementWorkload.CreateInstance));
	}
}

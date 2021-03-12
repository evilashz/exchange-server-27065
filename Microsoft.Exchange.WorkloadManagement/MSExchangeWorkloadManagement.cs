using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000043 RID: 67
	internal static class MSExchangeWorkloadManagement
	{
		// Token: 0x060002B0 RID: 688 RVA: 0x0000C6D3 File Offset: 0x0000A8D3
		public static MSExchangeWorkloadManagementInstance GetInstance(string instanceName)
		{
			return (MSExchangeWorkloadManagementInstance)MSExchangeWorkloadManagement.counters.GetInstance(instanceName);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000C6E5 File Offset: 0x0000A8E5
		public static void CloseInstance(string instanceName)
		{
			MSExchangeWorkloadManagement.counters.CloseInstance(instanceName);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000C6F2 File Offset: 0x0000A8F2
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeWorkloadManagement.counters.InstanceExists(instanceName);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000C6FF File Offset: 0x0000A8FF
		public static string[] GetInstanceNames()
		{
			return MSExchangeWorkloadManagement.counters.GetInstanceNames();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000C70B File Offset: 0x0000A90B
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeWorkloadManagement.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000C718 File Offset: 0x0000A918
		public static void ResetInstance(string instanceName)
		{
			MSExchangeWorkloadManagement.counters.ResetInstance(instanceName);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000C725 File Offset: 0x0000A925
		public static void RemoveAllInstances()
		{
			MSExchangeWorkloadManagement.counters.RemoveAllInstances();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000C731 File Offset: 0x0000A931
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeWorkloadManagementInstance(instanceName, (MSExchangeWorkloadManagementInstance)totalInstance);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000C73F File Offset: 0x0000A93F
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeWorkloadManagementInstance(instanceName);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000C747 File Offset: 0x0000A947
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeWorkloadManagement.counters == null)
			{
				return;
			}
			MSExchangeWorkloadManagement.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000162 RID: 354
		public const string CategoryName = "MSExchange WorkloadManagement";

		// Token: 0x04000163 RID: 355
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange WorkloadManagement", new CreateInstanceDelegate(MSExchangeWorkloadManagement.CreateInstance));
	}
}

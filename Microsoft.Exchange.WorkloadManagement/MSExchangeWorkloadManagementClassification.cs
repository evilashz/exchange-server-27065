using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000045 RID: 69
	internal static class MSExchangeWorkloadManagementClassification
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		public static MSExchangeWorkloadManagementClassificationInstance GetInstance(string instanceName)
		{
			return (MSExchangeWorkloadManagementClassificationInstance)MSExchangeWorkloadManagementClassification.counters.GetInstance(instanceName);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000CA0E File Offset: 0x0000AC0E
		public static void CloseInstance(string instanceName)
		{
			MSExchangeWorkloadManagementClassification.counters.CloseInstance(instanceName);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000CA1B File Offset: 0x0000AC1B
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeWorkloadManagementClassification.counters.InstanceExists(instanceName);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000CA28 File Offset: 0x0000AC28
		public static string[] GetInstanceNames()
		{
			return MSExchangeWorkloadManagementClassification.counters.GetInstanceNames();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000CA34 File Offset: 0x0000AC34
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeWorkloadManagementClassification.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000CA41 File Offset: 0x0000AC41
		public static void ResetInstance(string instanceName)
		{
			MSExchangeWorkloadManagementClassification.counters.ResetInstance(instanceName);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000CA4E File Offset: 0x0000AC4E
		public static void RemoveAllInstances()
		{
			MSExchangeWorkloadManagementClassification.counters.RemoveAllInstances();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000CA5A File Offset: 0x0000AC5A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeWorkloadManagementClassificationInstance(instanceName, (MSExchangeWorkloadManagementClassificationInstance)totalInstance);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000CA68 File Offset: 0x0000AC68
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeWorkloadManagementClassificationInstance(instanceName);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000CA70 File Offset: 0x0000AC70
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeWorkloadManagementClassification.counters == null)
			{
				return;
			}
			MSExchangeWorkloadManagementClassification.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000166 RID: 358
		public const string CategoryName = "MSExchange WorkloadManagement Classification";

		// Token: 0x04000167 RID: 359
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange WorkloadManagement Classification", new CreateInstanceDelegate(MSExchangeWorkloadManagementClassification.CreateInstance));
	}
}

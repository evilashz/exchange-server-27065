using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000047 RID: 71
	internal static class MSExchangeUserWorkloadManager
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0000CD74 File Offset: 0x0000AF74
		public static MSExchangeUserWorkloadManagerInstance GetInstance(string instanceName)
		{
			return (MSExchangeUserWorkloadManagerInstance)MSExchangeUserWorkloadManager.counters.GetInstance(instanceName);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000CD86 File Offset: 0x0000AF86
		public static void CloseInstance(string instanceName)
		{
			MSExchangeUserWorkloadManager.counters.CloseInstance(instanceName);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000CD93 File Offset: 0x0000AF93
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeUserWorkloadManager.counters.InstanceExists(instanceName);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000CDA0 File Offset: 0x0000AFA0
		public static string[] GetInstanceNames()
		{
			return MSExchangeUserWorkloadManager.counters.GetInstanceNames();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeUserWorkloadManager.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000CDB9 File Offset: 0x0000AFB9
		public static void ResetInstance(string instanceName)
		{
			MSExchangeUserWorkloadManager.counters.ResetInstance(instanceName);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000CDC6 File Offset: 0x0000AFC6
		public static void RemoveAllInstances()
		{
			MSExchangeUserWorkloadManager.counters.RemoveAllInstances();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000CDD2 File Offset: 0x0000AFD2
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeUserWorkloadManagerInstance(instanceName, (MSExchangeUserWorkloadManagerInstance)totalInstance);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeUserWorkloadManagerInstance(instanceName);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeUserWorkloadManager.counters == null)
			{
				return;
			}
			MSExchangeUserWorkloadManager.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400016B RID: 363
		public const string CategoryName = "MSExchange User WorkloadManager";

		// Token: 0x0400016C RID: 364
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange User WorkloadManager", new CreateInstanceDelegate(MSExchangeUserWorkloadManager.CreateInstance));
	}
}

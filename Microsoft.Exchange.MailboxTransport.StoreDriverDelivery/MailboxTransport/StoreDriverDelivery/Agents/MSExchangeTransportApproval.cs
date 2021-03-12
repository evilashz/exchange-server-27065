using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x0200004F RID: 79
	internal static class MSExchangeTransportApproval
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000E4FD File Offset: 0x0000C6FD
		public static MSExchangeTransportApprovalInstance GetInstance(string instanceName)
		{
			return (MSExchangeTransportApprovalInstance)MSExchangeTransportApproval.counters.GetInstance(instanceName);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000E50F File Offset: 0x0000C70F
		public static void CloseInstance(string instanceName)
		{
			MSExchangeTransportApproval.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000E51C File Offset: 0x0000C71C
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeTransportApproval.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000E529 File Offset: 0x0000C729
		public static string[] GetInstanceNames()
		{
			return MSExchangeTransportApproval.counters.GetInstanceNames();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000E535 File Offset: 0x0000C735
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeTransportApproval.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000E542 File Offset: 0x0000C742
		public static void ResetInstance(string instanceName)
		{
			MSExchangeTransportApproval.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000E54F File Offset: 0x0000C74F
		public static void RemoveAllInstances()
		{
			MSExchangeTransportApproval.counters.RemoveAllInstances();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000E55B File Offset: 0x0000C75B
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeTransportApprovalInstance(instanceName, (MSExchangeTransportApprovalInstance)totalInstance);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000E569 File Offset: 0x0000C769
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeTransportApprovalInstance(instanceName);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000E571 File Offset: 0x0000C771
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeTransportApproval.counters == null)
			{
				return;
			}
			MSExchangeTransportApproval.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040001A1 RID: 417
		public const string CategoryName = "MSExchange Approval Framework";

		// Token: 0x040001A2 RID: 418
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Approval Framework", new CreateInstanceDelegate(MSExchangeTransportApproval.CreateInstance));
	}
}

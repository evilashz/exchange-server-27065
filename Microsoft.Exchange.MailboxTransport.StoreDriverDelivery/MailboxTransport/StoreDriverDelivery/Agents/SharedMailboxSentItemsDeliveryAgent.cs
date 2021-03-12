using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000BB RID: 187
	internal static class SharedMailboxSentItemsDeliveryAgent
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x0001FE5C File Offset: 0x0001E05C
		public static SharedMailboxSentItemsDeliveryAgentInstance GetInstance(string instanceName)
		{
			return (SharedMailboxSentItemsDeliveryAgentInstance)SharedMailboxSentItemsDeliveryAgent.counters.GetInstance(instanceName);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001FE6E File Offset: 0x0001E06E
		public static void CloseInstance(string instanceName)
		{
			SharedMailboxSentItemsDeliveryAgent.counters.CloseInstance(instanceName);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001FE7B File Offset: 0x0001E07B
		public static bool InstanceExists(string instanceName)
		{
			return SharedMailboxSentItemsDeliveryAgent.counters.InstanceExists(instanceName);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001FE88 File Offset: 0x0001E088
		public static string[] GetInstanceNames()
		{
			return SharedMailboxSentItemsDeliveryAgent.counters.GetInstanceNames();
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001FE94 File Offset: 0x0001E094
		public static void RemoveInstance(string instanceName)
		{
			SharedMailboxSentItemsDeliveryAgent.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001FEA1 File Offset: 0x0001E0A1
		public static void ResetInstance(string instanceName)
		{
			SharedMailboxSentItemsDeliveryAgent.counters.ResetInstance(instanceName);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001FEAE File Offset: 0x0001E0AE
		public static void RemoveAllInstances()
		{
			SharedMailboxSentItemsDeliveryAgent.counters.RemoveAllInstances();
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001FEBA File Offset: 0x0001E0BA
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new SharedMailboxSentItemsDeliveryAgentInstance(instanceName, (SharedMailboxSentItemsDeliveryAgentInstance)totalInstance);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001FEC8 File Offset: 0x0001E0C8
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new SharedMailboxSentItemsDeliveryAgentInstance(instanceName);
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001FED0 File Offset: 0x0001E0D0
		public static SharedMailboxSentItemsDeliveryAgentInstance TotalInstance
		{
			get
			{
				return (SharedMailboxSentItemsDeliveryAgentInstance)SharedMailboxSentItemsDeliveryAgent.counters.TotalInstance;
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001FEE1 File Offset: 0x0001E0E1
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SharedMailboxSentItemsDeliveryAgent.counters == null)
			{
				return;
			}
			SharedMailboxSentItemsDeliveryAgent.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000355 RID: 853
		public const string CategoryName = "MSExchange Shared Mailbox Sent Items Agent";

		// Token: 0x04000356 RID: 854
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Shared Mailbox Sent Items Agent", new CreateInstanceDelegate(SharedMailboxSentItemsDeliveryAgent.CreateInstance), new CreateTotalInstanceDelegate(SharedMailboxSentItemsDeliveryAgent.CreateTotalInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200004A RID: 74
	internal static class MSExchangeStoreDriverDeliveryAgent
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0000D8BE File Offset: 0x0000BABE
		public static MSExchangeStoreDriverDeliveryAgentInstance GetInstance(string instanceName)
		{
			return (MSExchangeStoreDriverDeliveryAgentInstance)MSExchangeStoreDriverDeliveryAgent.counters.GetInstance(instanceName);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		public static void CloseInstance(string instanceName)
		{
			MSExchangeStoreDriverDeliveryAgent.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000D8DD File Offset: 0x0000BADD
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeStoreDriverDeliveryAgent.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000D8EA File Offset: 0x0000BAEA
		public static string[] GetInstanceNames()
		{
			return MSExchangeStoreDriverDeliveryAgent.counters.GetInstanceNames();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000D8F6 File Offset: 0x0000BAF6
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeStoreDriverDeliveryAgent.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000D903 File Offset: 0x0000BB03
		public static void ResetInstance(string instanceName)
		{
			MSExchangeStoreDriverDeliveryAgent.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000D910 File Offset: 0x0000BB10
		public static void RemoveAllInstances()
		{
			MSExchangeStoreDriverDeliveryAgent.counters.RemoveAllInstances();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000D91C File Offset: 0x0000BB1C
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeStoreDriverDeliveryAgentInstance(instanceName, (MSExchangeStoreDriverDeliveryAgentInstance)totalInstance);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000D92A File Offset: 0x0000BB2A
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeStoreDriverDeliveryAgentInstance(instanceName);
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000D932 File Offset: 0x0000BB32
		public static MSExchangeStoreDriverDeliveryAgentInstance TotalInstance
		{
			get
			{
				return (MSExchangeStoreDriverDeliveryAgentInstance)MSExchangeStoreDriverDeliveryAgent.counters.TotalInstance;
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000D943 File Offset: 0x0000BB43
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeStoreDriverDeliveryAgent.counters == null)
			{
				return;
			}
			MSExchangeStoreDriverDeliveryAgent.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000179 RID: 377
		public const string CategoryName = "MSExchange Delivery Store Driver Agents";

		// Token: 0x0400017A RID: 378
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Delivery Store Driver Agents", new CreateInstanceDelegate(MSExchangeStoreDriverDeliveryAgent.CreateInstance), new CreateTotalInstanceDelegate(MSExchangeStoreDriverDeliveryAgent.CreateTotalInstance));
	}
}

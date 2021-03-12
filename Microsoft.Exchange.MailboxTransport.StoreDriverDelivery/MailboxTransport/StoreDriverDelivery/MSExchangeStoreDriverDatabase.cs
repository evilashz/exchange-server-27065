using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200004C RID: 76
	internal static class MSExchangeStoreDriverDatabase
	{
		// Token: 0x0600033E RID: 830 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		public static MSExchangeStoreDriverDatabaseInstance GetInstance(string instanceName)
		{
			return (MSExchangeStoreDriverDatabaseInstance)MSExchangeStoreDriverDatabase.counters.GetInstance(instanceName);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000DBC6 File Offset: 0x0000BDC6
		public static void CloseInstance(string instanceName)
		{
			MSExchangeStoreDriverDatabase.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000DBD3 File Offset: 0x0000BDD3
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeStoreDriverDatabase.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		public static string[] GetInstanceNames()
		{
			return MSExchangeStoreDriverDatabase.counters.GetInstanceNames();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000DBEC File Offset: 0x0000BDEC
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeStoreDriverDatabase.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000DBF9 File Offset: 0x0000BDF9
		public static void ResetInstance(string instanceName)
		{
			MSExchangeStoreDriverDatabase.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000DC06 File Offset: 0x0000BE06
		public static void RemoveAllInstances()
		{
			MSExchangeStoreDriverDatabase.counters.RemoveAllInstances();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000DC12 File Offset: 0x0000BE12
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeStoreDriverDatabaseInstance(instanceName, (MSExchangeStoreDriverDatabaseInstance)totalInstance);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000DC20 File Offset: 0x0000BE20
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeStoreDriverDatabaseInstance(instanceName);
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000DC28 File Offset: 0x0000BE28
		public static MSExchangeStoreDriverDatabaseInstance TotalInstance
		{
			get
			{
				return (MSExchangeStoreDriverDatabaseInstance)MSExchangeStoreDriverDatabase.counters.TotalInstance;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000DC39 File Offset: 0x0000BE39
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeStoreDriverDatabase.counters == null)
			{
				return;
			}
			MSExchangeStoreDriverDatabase.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400017C RID: 380
		public const string CategoryName = "MSExchange Delivery Store Driver Database";

		// Token: 0x0400017D RID: 381
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Delivery Store Driver Database", new CreateInstanceDelegate(MSExchangeStoreDriverDatabase.CreateInstance), new CreateTotalInstanceDelegate(MSExchangeStoreDriverDatabase.CreateTotalInstance));
	}
}

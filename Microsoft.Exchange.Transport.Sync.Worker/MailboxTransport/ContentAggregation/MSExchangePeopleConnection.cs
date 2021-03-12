using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200023B RID: 571
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MSExchangePeopleConnection
	{
		// Token: 0x060014D7 RID: 5335 RVA: 0x0004B240 File Offset: 0x00049440
		public static MSExchangePeopleConnectionInstance GetInstance(string instanceName)
		{
			return (MSExchangePeopleConnectionInstance)MSExchangePeopleConnection.counters.GetInstance(instanceName);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0004B252 File Offset: 0x00049452
		public static void CloseInstance(string instanceName)
		{
			MSExchangePeopleConnection.counters.CloseInstance(instanceName);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0004B25F File Offset: 0x0004945F
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangePeopleConnection.counters.InstanceExists(instanceName);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0004B26C File Offset: 0x0004946C
		public static string[] GetInstanceNames()
		{
			return MSExchangePeopleConnection.counters.GetInstanceNames();
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0004B278 File Offset: 0x00049478
		public static void RemoveInstance(string instanceName)
		{
			MSExchangePeopleConnection.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0004B285 File Offset: 0x00049485
		public static void ResetInstance(string instanceName)
		{
			MSExchangePeopleConnection.counters.ResetInstance(instanceName);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0004B292 File Offset: 0x00049492
		public static void RemoveAllInstances()
		{
			MSExchangePeopleConnection.counters.RemoveAllInstances();
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0004B29E File Offset: 0x0004949E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangePeopleConnectionInstance(instanceName, (MSExchangePeopleConnectionInstance)totalInstance);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0004B2AC File Offset: 0x000494AC
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangePeopleConnectionInstance(instanceName);
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0004B2B4 File Offset: 0x000494B4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangePeopleConnection.counters == null)
			{
				return;
			}
			MSExchangePeopleConnection.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000AE1 RID: 2785
		public const string CategoryName = "MSExchange Transport Sync - Contacts";

		// Token: 0x04000AE2 RID: 2786
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Transport Sync - Contacts", new CreateInstanceDelegate(MSExchangePeopleConnection.CreateInstance));
	}
}

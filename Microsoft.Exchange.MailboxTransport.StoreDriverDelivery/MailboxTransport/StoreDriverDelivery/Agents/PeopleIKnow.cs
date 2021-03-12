using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000AD RID: 173
	internal static class PeopleIKnow
	{
		// Token: 0x060005A6 RID: 1446 RVA: 0x0001EF82 File Offset: 0x0001D182
		public static PeopleIKnowInstance GetInstance(string instanceName)
		{
			return (PeopleIKnowInstance)PeopleIKnow.counters.GetInstance(instanceName);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001EF94 File Offset: 0x0001D194
		public static void CloseInstance(string instanceName)
		{
			PeopleIKnow.counters.CloseInstance(instanceName);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001EFA1 File Offset: 0x0001D1A1
		public static bool InstanceExists(string instanceName)
		{
			return PeopleIKnow.counters.InstanceExists(instanceName);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001EFAE File Offset: 0x0001D1AE
		public static string[] GetInstanceNames()
		{
			return PeopleIKnow.counters.GetInstanceNames();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001EFBA File Offset: 0x0001D1BA
		public static void RemoveInstance(string instanceName)
		{
			PeopleIKnow.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001EFC7 File Offset: 0x0001D1C7
		public static void ResetInstance(string instanceName)
		{
			PeopleIKnow.counters.ResetInstance(instanceName);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001EFD4 File Offset: 0x0001D1D4
		public static void RemoveAllInstances()
		{
			PeopleIKnow.counters.RemoveAllInstances();
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001EFE0 File Offset: 0x0001D1E0
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new PeopleIKnowInstance(instanceName, (PeopleIKnowInstance)totalInstance);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001EFEE File Offset: 0x0001D1EE
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new PeopleIKnowInstance(instanceName);
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001EFF6 File Offset: 0x0001D1F6
		public static PeopleIKnowInstance TotalInstance
		{
			get
			{
				return (PeopleIKnowInstance)PeopleIKnow.counters.TotalInstance;
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001F007 File Offset: 0x0001D207
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PeopleIKnow.counters == null)
			{
				return;
			}
			PeopleIKnow.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400033E RID: 830
		public const string CategoryName = "People-I-Know Delivery Agent";

		// Token: 0x0400033F RID: 831
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("People-I-Know Delivery Agent", new CreateInstanceDelegate(PeopleIKnow.CreateInstance), new CreateTotalInstanceDelegate(PeopleIKnow.CreateTotalInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x0200006A RID: 106
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class AdminAuditPerfCounters
	{
		// Token: 0x06000305 RID: 773 RVA: 0x0001162E File Offset: 0x0000F82E
		public static AdminAuditPerfCountersInstance GetInstance(string instanceName)
		{
			return (AdminAuditPerfCountersInstance)AdminAuditPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00011640 File Offset: 0x0000F840
		public static void CloseInstance(string instanceName)
		{
			AdminAuditPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0001164D File Offset: 0x0000F84D
		public static bool InstanceExists(string instanceName)
		{
			return AdminAuditPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001165A File Offset: 0x0000F85A
		public static string[] GetInstanceNames()
		{
			return AdminAuditPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00011666 File Offset: 0x0000F866
		public static void RemoveInstance(string instanceName)
		{
			AdminAuditPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00011673 File Offset: 0x0000F873
		public static void ResetInstance(string instanceName)
		{
			AdminAuditPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00011680 File Offset: 0x0000F880
		public static void RemoveAllInstances()
		{
			AdminAuditPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001168C File Offset: 0x0000F88C
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new AdminAuditPerfCountersInstance(instanceName, (AdminAuditPerfCountersInstance)totalInstance);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001169A File Offset: 0x0000F89A
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new AdminAuditPerfCountersInstance(instanceName);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000116A2 File Offset: 0x0000F8A2
		public static void GetPerfCounterInfo(XElement element)
		{
			if (AdminAuditPerfCounters.counters == null)
			{
				return;
			}
			AdminAuditPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040001BC RID: 444
		public const string CategoryName = "MSExchange Admin Audit Log";

		// Token: 0x040001BD RID: 445
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Admin Audit Log", new CreateInstanceDelegate(AdminAuditPerfCounters.CreateInstance));
	}
}

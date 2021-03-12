using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000303 RID: 771
	internal static class AACounters
	{
		// Token: 0x06001772 RID: 6002 RVA: 0x00063A2D File Offset: 0x00061C2D
		public static AACountersInstance GetInstance(string instanceName)
		{
			return (AACountersInstance)AACounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00063A3F File Offset: 0x00061C3F
		public static void CloseInstance(string instanceName)
		{
			AACounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00063A4C File Offset: 0x00061C4C
		public static bool InstanceExists(string instanceName)
		{
			return AACounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00063A59 File Offset: 0x00061C59
		public static string[] GetInstanceNames()
		{
			return AACounters.counters.GetInstanceNames();
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00063A65 File Offset: 0x00061C65
		public static void RemoveInstance(string instanceName)
		{
			AACounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00063A72 File Offset: 0x00061C72
		public static void ResetInstance(string instanceName)
		{
			AACounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00063A7F File Offset: 0x00061C7F
		public static void RemoveAllInstances()
		{
			AACounters.counters.RemoveAllInstances();
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00063A8B File Offset: 0x00061C8B
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new AACountersInstance(instanceName, (AACountersInstance)totalInstance);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00063A99 File Offset: 0x00061C99
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new AACountersInstance(instanceName);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00063AA1 File Offset: 0x00061CA1
		public static void GetPerfCounterInfo(XElement element)
		{
			if (AACounters.counters == null)
			{
				return;
			}
			AACounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000DB6 RID: 3510
		public const string CategoryName = "MSExchangeUMAutoAttendant";

		// Token: 0x04000DB7 RID: 3511
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeUMAutoAttendant", new CreateInstanceDelegate(AACounters.CreateInstance));
	}
}

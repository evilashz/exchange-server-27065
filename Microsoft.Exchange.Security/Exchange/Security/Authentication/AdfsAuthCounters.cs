using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000005 RID: 5
	internal static class AdfsAuthCounters
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000025D6 File Offset: 0x000007D6
		public static AdfsAuthCountersInstance GetInstance(string instanceName)
		{
			return (AdfsAuthCountersInstance)AdfsAuthCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000025E8 File Offset: 0x000007E8
		public static void CloseInstance(string instanceName)
		{
			AdfsAuthCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000025F5 File Offset: 0x000007F5
		public static bool InstanceExists(string instanceName)
		{
			return AdfsAuthCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002602 File Offset: 0x00000802
		public static string[] GetInstanceNames()
		{
			return AdfsAuthCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000260E File Offset: 0x0000080E
		public static void RemoveInstance(string instanceName)
		{
			AdfsAuthCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000261B File Offset: 0x0000081B
		public static void ResetInstance(string instanceName)
		{
			AdfsAuthCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002628 File Offset: 0x00000828
		public static void RemoveAllInstances()
		{
			AdfsAuthCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002634 File Offset: 0x00000834
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new AdfsAuthCountersInstance(instanceName, (AdfsAuthCountersInstance)totalInstance);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002642 File Offset: 0x00000842
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new AdfsAuthCountersInstance(instanceName);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000264A File Offset: 0x0000084A
		public static void GetPerfCounterInfo(XElement element)
		{
			if (AdfsAuthCounters.counters == null)
			{
				return;
			}
			AdfsAuthCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400008C RID: 140
		public const string CategoryName = "MSExchange AdfsAuth";

		// Token: 0x0400008D RID: 141
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange AdfsAuth", new CreateInstanceDelegate(AdfsAuthCounters.CreateInstance));
	}
}

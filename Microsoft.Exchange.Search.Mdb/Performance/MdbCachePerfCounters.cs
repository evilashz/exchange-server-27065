using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Performance
{
	// Token: 0x0200003D RID: 61
	internal static class MdbCachePerfCounters
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x0000D4DF File Offset: 0x0000B6DF
		public static MdbCachePerfCountersInstance GetInstance(string instanceName)
		{
			return (MdbCachePerfCountersInstance)MdbCachePerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000D4F1 File Offset: 0x0000B6F1
		public static void CloseInstance(string instanceName)
		{
			MdbCachePerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000D4FE File Offset: 0x0000B6FE
		public static bool InstanceExists(string instanceName)
		{
			return MdbCachePerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000D50B File Offset: 0x0000B70B
		public static string[] GetInstanceNames()
		{
			return MdbCachePerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000D517 File Offset: 0x0000B717
		public static void RemoveInstance(string instanceName)
		{
			MdbCachePerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000D524 File Offset: 0x0000B724
		public static void ResetInstance(string instanceName)
		{
			MdbCachePerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000D531 File Offset: 0x0000B731
		public static void RemoveAllInstances()
		{
			MdbCachePerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000D53D File Offset: 0x0000B73D
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MdbCachePerfCountersInstance(instanceName, (MdbCachePerfCountersInstance)totalInstance);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000D54B File Offset: 0x0000B74B
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MdbCachePerfCountersInstance(instanceName);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000D553 File Offset: 0x0000B753
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MdbCachePerfCounters.counters == null)
			{
				return;
			}
			MdbCachePerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400013A RID: 314
		public const string CategoryName = "MSExchangeSearch MailboxSession Cache";

		// Token: 0x0400013B RID: 315
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeSearch MailboxSession Cache", new CreateInstanceDelegate(MdbCachePerfCounters.CreateInstance));
	}
}

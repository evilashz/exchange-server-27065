using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Performance
{
	// Token: 0x0200003F RID: 63
	internal static class MdbPerfCounters
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x0000D9D8 File Offset: 0x0000BBD8
		public static MdbPerfCountersInstance GetInstance(string instanceName)
		{
			return (MdbPerfCountersInstance)MdbPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000D9EA File Offset: 0x0000BBEA
		public static void CloseInstance(string instanceName)
		{
			MdbPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000D9F7 File Offset: 0x0000BBF7
		public static bool InstanceExists(string instanceName)
		{
			return MdbPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000DA04 File Offset: 0x0000BC04
		public static string[] GetInstanceNames()
		{
			return MdbPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000DA10 File Offset: 0x0000BC10
		public static void RemoveInstance(string instanceName)
		{
			MdbPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000DA1D File Offset: 0x0000BC1D
		public static void ResetInstance(string instanceName)
		{
			MdbPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000DA2A File Offset: 0x0000BC2A
		public static void RemoveAllInstances()
		{
			MdbPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000DA36 File Offset: 0x0000BC36
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MdbPerfCountersInstance(instanceName, (MdbPerfCountersInstance)totalInstance);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000DA44 File Offset: 0x0000BC44
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MdbPerfCountersInstance(instanceName);
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		public static MdbPerfCountersInstance TotalInstance
		{
			get
			{
				return (MdbPerfCountersInstance)MdbPerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000DA5D File Offset: 0x0000BC5D
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MdbPerfCounters.counters == null)
			{
				return;
			}
			MdbPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000143 RID: 323
		public const string CategoryName = "MSExchange Search Indexes";

		// Token: 0x04000144 RID: 324
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchange Search Indexes", new CreateInstanceDelegate(MdbPerfCounters.CreateInstance), new CreateTotalInstanceDelegate(MdbPerfCounters.CreateTotalInstance));
	}
}

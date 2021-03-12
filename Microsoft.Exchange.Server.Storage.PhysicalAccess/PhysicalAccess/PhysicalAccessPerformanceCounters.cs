using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000004 RID: 4
	internal static class PhysicalAccessPerformanceCounters
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00009967 File Offset: 0x00007B67
		public static PhysicalAccessPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (PhysicalAccessPerformanceCountersInstance)PhysicalAccessPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00009979 File Offset: 0x00007B79
		public static void CloseInstance(string instanceName)
		{
			PhysicalAccessPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00009986 File Offset: 0x00007B86
		public static bool InstanceExists(string instanceName)
		{
			return PhysicalAccessPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00009993 File Offset: 0x00007B93
		public static string[] GetInstanceNames()
		{
			return PhysicalAccessPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000999F File Offset: 0x00007B9F
		public static void RemoveInstance(string instanceName)
		{
			PhysicalAccessPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000099AC File Offset: 0x00007BAC
		public static void ResetInstance(string instanceName)
		{
			PhysicalAccessPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000099B9 File Offset: 0x00007BB9
		public static void RemoveAllInstances()
		{
			PhysicalAccessPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000099C5 File Offset: 0x00007BC5
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new PhysicalAccessPerformanceCountersInstance(instanceName, (PhysicalAccessPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000099D3 File Offset: 0x00007BD3
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new PhysicalAccessPerformanceCountersInstance(instanceName);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000099DB File Offset: 0x00007BDB
		public static PhysicalAccessPerformanceCountersInstance TotalInstance
		{
			get
			{
				return (PhysicalAccessPerformanceCountersInstance)PhysicalAccessPerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000099EC File Offset: 0x00007BEC
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PhysicalAccessPerformanceCounters.counters == null)
			{
				return;
			}
			PhysicalAccessPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000007 RID: 7
		public const string CategoryName = "MSExchangeIS Physical Access";

		// Token: 0x04000008 RID: 8
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeIS Physical Access", new CreateInstanceDelegate(PhysicalAccessPerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(PhysicalAccessPerformanceCounters.CreateTotalInstance));
	}
}

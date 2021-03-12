using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200003B RID: 59
	internal static class EhfPerfCounters
	{
		// Token: 0x06000285 RID: 645 RVA: 0x00011792 File Offset: 0x0000F992
		public static EhfPerfCountersInstance GetInstance(string instanceName)
		{
			return (EhfPerfCountersInstance)EhfPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000117A4 File Offset: 0x0000F9A4
		public static void CloseInstance(string instanceName)
		{
			EhfPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000117B1 File Offset: 0x0000F9B1
		public static bool InstanceExists(string instanceName)
		{
			return EhfPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000117BE File Offset: 0x0000F9BE
		public static string[] GetInstanceNames()
		{
			return EhfPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000117CA File Offset: 0x0000F9CA
		public static void RemoveInstance(string instanceName)
		{
			EhfPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000117D7 File Offset: 0x0000F9D7
		public static void ResetInstance(string instanceName)
		{
			EhfPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000117E4 File Offset: 0x0000F9E4
		public static void RemoveAllInstances()
		{
			EhfPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000117F0 File Offset: 0x0000F9F0
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new EhfPerfCountersInstance(instanceName, (EhfPerfCountersInstance)totalInstance);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000117FE File Offset: 0x0000F9FE
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new EhfPerfCountersInstance(instanceName);
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00011806 File Offset: 0x0000FA06
		public static EhfPerfCountersInstance TotalInstance
		{
			get
			{
				return (EhfPerfCountersInstance)EhfPerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00011817 File Offset: 0x0000FA17
		public static void GetPerfCounterInfo(XElement element)
		{
			if (EhfPerfCounters.counters == null)
			{
				return;
			}
			EhfPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000119 RID: 281
		public const string CategoryName = "MSExchangeEdgeSync EHF Sync Operations";

		// Token: 0x0400011A RID: 282
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeEdgeSync EHF Sync Operations", new CreateInstanceDelegate(EhfPerfCounters.CreateInstance), new CreateTotalInstanceDelegate(EhfPerfCounters.CreateTotalInstance));
	}
}

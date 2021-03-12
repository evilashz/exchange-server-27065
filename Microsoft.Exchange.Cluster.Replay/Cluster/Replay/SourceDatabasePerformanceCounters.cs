using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000392 RID: 914
	internal static class SourceDatabasePerformanceCounters
	{
		// Token: 0x06002485 RID: 9349 RVA: 0x000AC9EC File Offset: 0x000AABEC
		public static SourceDatabasePerformanceCountersInstance GetInstance(string instanceName)
		{
			return (SourceDatabasePerformanceCountersInstance)SourceDatabasePerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x000AC9FE File Offset: 0x000AABFE
		public static void CloseInstance(string instanceName)
		{
			SourceDatabasePerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000ACA0B File Offset: 0x000AAC0B
		public static bool InstanceExists(string instanceName)
		{
			return SourceDatabasePerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000ACA18 File Offset: 0x000AAC18
		public static string[] GetInstanceNames()
		{
			return SourceDatabasePerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000ACA24 File Offset: 0x000AAC24
		public static void RemoveInstance(string instanceName)
		{
			SourceDatabasePerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000ACA31 File Offset: 0x000AAC31
		public static void ResetInstance(string instanceName)
		{
			SourceDatabasePerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000ACA3E File Offset: 0x000AAC3E
		public static void RemoveAllInstances()
		{
			SourceDatabasePerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000ACA4A File Offset: 0x000AAC4A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new SourceDatabasePerformanceCountersInstance(instanceName, (SourceDatabasePerformanceCountersInstance)totalInstance);
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000ACA58 File Offset: 0x000AAC58
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new SourceDatabasePerformanceCountersInstance(instanceName);
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x000ACA60 File Offset: 0x000AAC60
		public static SourceDatabasePerformanceCountersInstance TotalInstance
		{
			get
			{
				return (SourceDatabasePerformanceCountersInstance)SourceDatabasePerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000ACA71 File Offset: 0x000AAC71
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SourceDatabasePerformanceCounters.counters == null)
			{
				return;
			}
			SourceDatabasePerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040010D2 RID: 4306
		public const string CategoryName = "MSExchangeRepl Source Database";

		// Token: 0x040010D3 RID: 4307
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeRepl Source Database", new CreateInstanceDelegate(SourceDatabasePerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(SourceDatabasePerformanceCounters.CreateTotalInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000008 RID: 8
	internal static class StorePerClientTypePerformanceCounters
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00004F27 File Offset: 0x00003127
		public static StorePerClientTypePerformanceCountersInstance GetInstance(string instanceName)
		{
			return (StorePerClientTypePerformanceCountersInstance)StorePerClientTypePerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004F39 File Offset: 0x00003139
		public static void CloseInstance(string instanceName)
		{
			StorePerClientTypePerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004F46 File Offset: 0x00003146
		public static bool InstanceExists(string instanceName)
		{
			return StorePerClientTypePerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004F53 File Offset: 0x00003153
		public static string[] GetInstanceNames()
		{
			return StorePerClientTypePerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004F5F File Offset: 0x0000315F
		public static void RemoveInstance(string instanceName)
		{
			StorePerClientTypePerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004F6C File Offset: 0x0000316C
		public static void ResetInstance(string instanceName)
		{
			StorePerClientTypePerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004F79 File Offset: 0x00003179
		public static void RemoveAllInstances()
		{
			StorePerClientTypePerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004F85 File Offset: 0x00003185
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new StorePerClientTypePerformanceCountersInstance(instanceName, (StorePerClientTypePerformanceCountersInstance)totalInstance);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004F93 File Offset: 0x00003193
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new StorePerClientTypePerformanceCountersInstance(instanceName);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00004F9B File Offset: 0x0000319B
		public static StorePerClientTypePerformanceCountersInstance TotalInstance
		{
			get
			{
				return (StorePerClientTypePerformanceCountersInstance)StorePerClientTypePerformanceCounters.counters.TotalInstance;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004FAC File Offset: 0x000031AC
		public static void GetPerfCounterInfo(XElement element)
		{
			if (StorePerClientTypePerformanceCounters.counters == null)
			{
				return;
			}
			StorePerClientTypePerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000097 RID: 151
		public const string CategoryName = "MSExchangeIS Client Type";

		// Token: 0x04000098 RID: 152
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeIS Client Type", new CreateInstanceDelegate(StorePerClientTypePerformanceCounters.CreateInstance), new CreateTotalInstanceDelegate(StorePerClientTypePerformanceCounters.CreateTotalInstance));
	}
}

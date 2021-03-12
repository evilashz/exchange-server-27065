using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x02000A38 RID: 2616
	internal static class MSExchangeADAccessCacheCounters
	{
		// Token: 0x0600781D RID: 30749 RVA: 0x0018B9E3 File Offset: 0x00189BE3
		public static MSExchangeADAccessCacheCountersInstance GetInstance(string instanceName)
		{
			return (MSExchangeADAccessCacheCountersInstance)MSExchangeADAccessCacheCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600781E RID: 30750 RVA: 0x0018B9F5 File Offset: 0x00189BF5
		public static void CloseInstance(string instanceName)
		{
			MSExchangeADAccessCacheCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600781F RID: 30751 RVA: 0x0018BA02 File Offset: 0x00189C02
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeADAccessCacheCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06007820 RID: 30752 RVA: 0x0018BA0F File Offset: 0x00189C0F
		public static string[] GetInstanceNames()
		{
			return MSExchangeADAccessCacheCounters.counters.GetInstanceNames();
		}

		// Token: 0x06007821 RID: 30753 RVA: 0x0018BA1B File Offset: 0x00189C1B
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeADAccessCacheCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06007822 RID: 30754 RVA: 0x0018BA28 File Offset: 0x00189C28
		public static void ResetInstance(string instanceName)
		{
			MSExchangeADAccessCacheCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06007823 RID: 30755 RVA: 0x0018BA35 File Offset: 0x00189C35
		public static void RemoveAllInstances()
		{
			MSExchangeADAccessCacheCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06007824 RID: 30756 RVA: 0x0018BA41 File Offset: 0x00189C41
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeADAccessCacheCountersInstance(instanceName, (MSExchangeADAccessCacheCountersInstance)totalInstance);
		}

		// Token: 0x06007825 RID: 30757 RVA: 0x0018BA4F File Offset: 0x00189C4F
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeADAccessCacheCountersInstance(instanceName);
		}

		// Token: 0x06007826 RID: 30758 RVA: 0x0018BA57 File Offset: 0x00189C57
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeADAccessCacheCounters.counters == null)
			{
				return;
			}
			MSExchangeADAccessCacheCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04004CDA RID: 19674
		public const string CategoryName = "MSExchange ADAccess Cache";

		// Token: 0x04004CDB RID: 19675
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange ADAccess Cache", new CreateInstanceDelegate(MSExchangeADAccessCacheCounters.CreateInstance));
	}
}

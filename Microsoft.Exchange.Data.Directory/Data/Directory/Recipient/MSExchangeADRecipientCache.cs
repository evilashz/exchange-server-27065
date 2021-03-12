using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000A3A RID: 2618
	internal static class MSExchangeADRecipientCache
	{
		// Token: 0x0600782B RID: 30763 RVA: 0x0018BD5C File Offset: 0x00189F5C
		public static MSExchangeADRecipientCacheInstance GetInstance(string instanceName)
		{
			return (MSExchangeADRecipientCacheInstance)MSExchangeADRecipientCache.counters.GetInstance(instanceName);
		}

		// Token: 0x0600782C RID: 30764 RVA: 0x0018BD6E File Offset: 0x00189F6E
		public static void CloseInstance(string instanceName)
		{
			MSExchangeADRecipientCache.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600782D RID: 30765 RVA: 0x0018BD7B File Offset: 0x00189F7B
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeADRecipientCache.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600782E RID: 30766 RVA: 0x0018BD88 File Offset: 0x00189F88
		public static string[] GetInstanceNames()
		{
			return MSExchangeADRecipientCache.counters.GetInstanceNames();
		}

		// Token: 0x0600782F RID: 30767 RVA: 0x0018BD94 File Offset: 0x00189F94
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeADRecipientCache.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06007830 RID: 30768 RVA: 0x0018BDA1 File Offset: 0x00189FA1
		public static void ResetInstance(string instanceName)
		{
			MSExchangeADRecipientCache.counters.ResetInstance(instanceName);
		}

		// Token: 0x06007831 RID: 30769 RVA: 0x0018BDAE File Offset: 0x00189FAE
		public static void RemoveAllInstances()
		{
			MSExchangeADRecipientCache.counters.RemoveAllInstances();
		}

		// Token: 0x06007832 RID: 30770 RVA: 0x0018BDBA File Offset: 0x00189FBA
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeADRecipientCacheInstance(instanceName, (MSExchangeADRecipientCacheInstance)totalInstance);
		}

		// Token: 0x06007833 RID: 30771 RVA: 0x0018BDC8 File Offset: 0x00189FC8
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeADRecipientCacheInstance(instanceName);
		}

		// Token: 0x06007834 RID: 30772 RVA: 0x0018BDD0 File Offset: 0x00189FD0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeADRecipientCache.counters == null)
			{
				return;
			}
			MSExchangeADRecipientCache.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04004CDE RID: 19678
		public const string CategoryName = "MSExchange Recipient Cache";

		// Token: 0x04004CDF RID: 19679
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Recipient Cache", new CreateInstanceDelegate(MSExchangeADRecipientCache.CreateInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x02000A4B RID: 2635
	internal static class MSExchangeProvisioningCache
	{
		// Token: 0x06007879 RID: 30841 RVA: 0x0018F010 File Offset: 0x0018D210
		public static MSExchangeProvisioningCacheInstance GetInstance(string instanceName)
		{
			return (MSExchangeProvisioningCacheInstance)MSExchangeProvisioningCache.counters.GetInstance(instanceName);
		}

		// Token: 0x0600787A RID: 30842 RVA: 0x0018F022 File Offset: 0x0018D222
		public static void CloseInstance(string instanceName)
		{
			MSExchangeProvisioningCache.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600787B RID: 30843 RVA: 0x0018F02F File Offset: 0x0018D22F
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeProvisioningCache.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600787C RID: 30844 RVA: 0x0018F03C File Offset: 0x0018D23C
		public static string[] GetInstanceNames()
		{
			return MSExchangeProvisioningCache.counters.GetInstanceNames();
		}

		// Token: 0x0600787D RID: 30845 RVA: 0x0018F048 File Offset: 0x0018D248
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeProvisioningCache.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600787E RID: 30846 RVA: 0x0018F055 File Offset: 0x0018D255
		public static void ResetInstance(string instanceName)
		{
			MSExchangeProvisioningCache.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600787F RID: 30847 RVA: 0x0018F062 File Offset: 0x0018D262
		public static void RemoveAllInstances()
		{
			MSExchangeProvisioningCache.counters.RemoveAllInstances();
		}

		// Token: 0x06007880 RID: 30848 RVA: 0x0018F06E File Offset: 0x0018D26E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeProvisioningCacheInstance(instanceName, (MSExchangeProvisioningCacheInstance)totalInstance);
		}

		// Token: 0x06007881 RID: 30849 RVA: 0x0018F07C File Offset: 0x0018D27C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeProvisioningCacheInstance(instanceName);
		}

		// Token: 0x06007882 RID: 30850 RVA: 0x0018F084 File Offset: 0x0018D284
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeProvisioningCache.counters == null)
			{
				return;
			}
			MSExchangeProvisioningCache.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04004F59 RID: 20313
		public const string CategoryName = "MSExchange Provisioning Cache";

		// Token: 0x04004F5A RID: 20314
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Provisioning Cache", new CreateInstanceDelegate(MSExchangeProvisioningCache.CreateInstance));
	}
}

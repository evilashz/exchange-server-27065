using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A4D RID: 2637
	internal static class MSExchangeThrottling
	{
		// Token: 0x06007887 RID: 30855 RVA: 0x0018F4C0 File Offset: 0x0018D6C0
		public static MSExchangeThrottlingInstance GetInstance(string instanceName)
		{
			return (MSExchangeThrottlingInstance)MSExchangeThrottling.counters.GetInstance(instanceName);
		}

		// Token: 0x06007888 RID: 30856 RVA: 0x0018F4D2 File Offset: 0x0018D6D2
		public static void CloseInstance(string instanceName)
		{
			MSExchangeThrottling.counters.CloseInstance(instanceName);
		}

		// Token: 0x06007889 RID: 30857 RVA: 0x0018F4DF File Offset: 0x0018D6DF
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeThrottling.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600788A RID: 30858 RVA: 0x0018F4EC File Offset: 0x0018D6EC
		public static string[] GetInstanceNames()
		{
			return MSExchangeThrottling.counters.GetInstanceNames();
		}

		// Token: 0x0600788B RID: 30859 RVA: 0x0018F4F8 File Offset: 0x0018D6F8
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeThrottling.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600788C RID: 30860 RVA: 0x0018F505 File Offset: 0x0018D705
		public static void ResetInstance(string instanceName)
		{
			MSExchangeThrottling.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600788D RID: 30861 RVA: 0x0018F512 File Offset: 0x0018D712
		public static void RemoveAllInstances()
		{
			MSExchangeThrottling.counters.RemoveAllInstances();
		}

		// Token: 0x0600788E RID: 30862 RVA: 0x0018F51E File Offset: 0x0018D71E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeThrottlingInstance(instanceName, (MSExchangeThrottlingInstance)totalInstance);
		}

		// Token: 0x0600788F RID: 30863 RVA: 0x0018F52C File Offset: 0x0018D72C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeThrottlingInstance(instanceName);
		}

		// Token: 0x06007890 RID: 30864 RVA: 0x0018F534 File Offset: 0x0018D734
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeThrottling.counters == null)
			{
				return;
			}
			MSExchangeThrottling.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04004F61 RID: 20321
		public const string CategoryName = "MSExchange Throttling";

		// Token: 0x04004F62 RID: 20322
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Throttling", new CreateInstanceDelegate(MSExchangeThrottling.CreateInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A51 RID: 2641
	internal static class MSExchangeUserThrottling
	{
		// Token: 0x060078A3 RID: 30883 RVA: 0x0018FED8 File Offset: 0x0018E0D8
		public static MSExchangeUserThrottlingInstance GetInstance(string instanceName)
		{
			return (MSExchangeUserThrottlingInstance)MSExchangeUserThrottling.counters.GetInstance(instanceName);
		}

		// Token: 0x060078A4 RID: 30884 RVA: 0x0018FEEA File Offset: 0x0018E0EA
		public static void CloseInstance(string instanceName)
		{
			MSExchangeUserThrottling.counters.CloseInstance(instanceName);
		}

		// Token: 0x060078A5 RID: 30885 RVA: 0x0018FEF7 File Offset: 0x0018E0F7
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeUserThrottling.counters.InstanceExists(instanceName);
		}

		// Token: 0x060078A6 RID: 30886 RVA: 0x0018FF04 File Offset: 0x0018E104
		public static string[] GetInstanceNames()
		{
			return MSExchangeUserThrottling.counters.GetInstanceNames();
		}

		// Token: 0x060078A7 RID: 30887 RVA: 0x0018FF10 File Offset: 0x0018E110
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeUserThrottling.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060078A8 RID: 30888 RVA: 0x0018FF1D File Offset: 0x0018E11D
		public static void ResetInstance(string instanceName)
		{
			MSExchangeUserThrottling.counters.ResetInstance(instanceName);
		}

		// Token: 0x060078A9 RID: 30889 RVA: 0x0018FF2A File Offset: 0x0018E12A
		public static void RemoveAllInstances()
		{
			MSExchangeUserThrottling.counters.RemoveAllInstances();
		}

		// Token: 0x060078AA RID: 30890 RVA: 0x0018FF36 File Offset: 0x0018E136
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeUserThrottlingInstance(instanceName, (MSExchangeUserThrottlingInstance)totalInstance);
		}

		// Token: 0x060078AB RID: 30891 RVA: 0x0018FF44 File Offset: 0x0018E144
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeUserThrottlingInstance(instanceName);
		}

		// Token: 0x060078AC RID: 30892 RVA: 0x0018FF4C File Offset: 0x0018E14C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeUserThrottling.counters == null)
			{
				return;
			}
			MSExchangeUserThrottling.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04004F72 RID: 20338
		public const string CategoryName = "MSExchange User Throttling";

		// Token: 0x04004F73 RID: 20339
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange User Throttling", new CreateInstanceDelegate(MSExchangeUserThrottling.CreateInstance));
	}
}

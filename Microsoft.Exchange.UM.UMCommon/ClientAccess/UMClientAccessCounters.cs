using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x02000233 RID: 563
	internal static class UMClientAccessCounters
	{
		// Token: 0x060011A9 RID: 4521 RVA: 0x0003C3D2 File Offset: 0x0003A5D2
		public static UMClientAccessCountersInstance GetInstance(string instanceName)
		{
			return (UMClientAccessCountersInstance)UMClientAccessCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0003C3E4 File Offset: 0x0003A5E4
		public static void CloseInstance(string instanceName)
		{
			UMClientAccessCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0003C3F1 File Offset: 0x0003A5F1
		public static bool InstanceExists(string instanceName)
		{
			return UMClientAccessCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0003C3FE File Offset: 0x0003A5FE
		public static string[] GetInstanceNames()
		{
			return UMClientAccessCounters.counters.GetInstanceNames();
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0003C40A File Offset: 0x0003A60A
		public static void RemoveInstance(string instanceName)
		{
			UMClientAccessCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0003C417 File Offset: 0x0003A617
		public static void ResetInstance(string instanceName)
		{
			UMClientAccessCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0003C424 File Offset: 0x0003A624
		public static void RemoveAllInstances()
		{
			UMClientAccessCounters.counters.RemoveAllInstances();
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0003C430 File Offset: 0x0003A630
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new UMClientAccessCountersInstance(instanceName, (UMClientAccessCountersInstance)totalInstance);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0003C43E File Offset: 0x0003A63E
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new UMClientAccessCountersInstance(instanceName);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0003C446 File Offset: 0x0003A646
		public static void GetPerfCounterInfo(XElement element)
		{
			if (UMClientAccessCounters.counters == null)
			{
				return;
			}
			UMClientAccessCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000B6A RID: 2922
		public const string CategoryName = "MSExchangeUMClientAccess";

		// Token: 0x04000B6B RID: 2923
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeUMClientAccess", new CreateInstanceDelegate(UMClientAccessCounters.CreateInstance));
	}
}

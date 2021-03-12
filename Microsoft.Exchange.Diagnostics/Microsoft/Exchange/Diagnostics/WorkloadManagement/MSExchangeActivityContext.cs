using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x02000206 RID: 518
	internal static class MSExchangeActivityContext
	{
		// Token: 0x06000F30 RID: 3888 RVA: 0x0003DC73 File Offset: 0x0003BE73
		public static MSExchangeActivityContextInstance GetInstance(string instanceName)
		{
			return (MSExchangeActivityContextInstance)MSExchangeActivityContext.counters.GetInstance(instanceName);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0003DC85 File Offset: 0x0003BE85
		public static void CloseInstance(string instanceName)
		{
			MSExchangeActivityContext.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0003DC92 File Offset: 0x0003BE92
		public static bool InstanceExists(string instanceName)
		{
			return MSExchangeActivityContext.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0003DC9F File Offset: 0x0003BE9F
		public static string[] GetInstanceNames()
		{
			return MSExchangeActivityContext.counters.GetInstanceNames();
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003DCAB File Offset: 0x0003BEAB
		public static void RemoveInstance(string instanceName)
		{
			MSExchangeActivityContext.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0003DCB8 File Offset: 0x0003BEB8
		public static void ResetInstance(string instanceName)
		{
			MSExchangeActivityContext.counters.ResetInstance(instanceName);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0003DCC5 File Offset: 0x0003BEC5
		public static void RemoveAllInstances()
		{
			MSExchangeActivityContext.counters.RemoveAllInstances();
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0003DCD1 File Offset: 0x0003BED1
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MSExchangeActivityContextInstance(instanceName, (MSExchangeActivityContextInstance)totalInstance);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0003DCDF File Offset: 0x0003BEDF
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MSExchangeActivityContextInstance(instanceName);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0003DCE7 File Offset: 0x0003BEE7
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeActivityContext.counters == null)
			{
				return;
			}
			MSExchangeActivityContext.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000AC5 RID: 2757
		public const string CategoryName = "MSExchange Activity Context Resources";

		// Token: 0x04000AC6 RID: 2758
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Activity Context Resources", new CreateInstanceDelegate(MSExchangeActivityContext.CreateInstance));
	}
}

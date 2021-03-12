using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x020001A9 RID: 425
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ActiveManagerClientPerfmon
	{
		// Token: 0x0600179A RID: 6042 RVA: 0x00070F70 File Offset: 0x0006F170
		public static ActiveManagerClientPerfmonInstance GetInstance(string instanceName)
		{
			return (ActiveManagerClientPerfmonInstance)ActiveManagerClientPerfmon.counters.GetInstance(instanceName);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00070F82 File Offset: 0x0006F182
		public static void CloseInstance(string instanceName)
		{
			ActiveManagerClientPerfmon.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00070F8F File Offset: 0x0006F18F
		public static bool InstanceExists(string instanceName)
		{
			return ActiveManagerClientPerfmon.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00070F9C File Offset: 0x0006F19C
		public static string[] GetInstanceNames()
		{
			return ActiveManagerClientPerfmon.counters.GetInstanceNames();
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00070FA8 File Offset: 0x0006F1A8
		public static void RemoveInstance(string instanceName)
		{
			ActiveManagerClientPerfmon.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00070FB5 File Offset: 0x0006F1B5
		public static void ResetInstance(string instanceName)
		{
			ActiveManagerClientPerfmon.counters.ResetInstance(instanceName);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00070FC2 File Offset: 0x0006F1C2
		public static void RemoveAllInstances()
		{
			ActiveManagerClientPerfmon.counters.RemoveAllInstances();
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00070FCE File Offset: 0x0006F1CE
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ActiveManagerClientPerfmonInstance(instanceName, (ActiveManagerClientPerfmonInstance)totalInstance);
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00070FDC File Offset: 0x0006F1DC
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ActiveManagerClientPerfmonInstance(instanceName);
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00070FE4 File Offset: 0x0006F1E4
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ActiveManagerClientPerfmon.counters == null)
			{
				return;
			}
			ActiveManagerClientPerfmon.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000B26 RID: 2854
		public const string CategoryName = "MSExchange Active Manager Client";

		// Token: 0x04000B27 RID: 2855
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchange Active Manager Client", new CreateInstanceDelegate(ActiveManagerClientPerfmon.CreateInstance));
	}
}

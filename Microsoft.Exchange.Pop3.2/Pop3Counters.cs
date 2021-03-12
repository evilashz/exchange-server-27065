using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000022 RID: 34
	internal static class Pop3Counters
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00006638 File Offset: 0x00004838
		public static Pop3CountersInstance GetInstance(string instanceName)
		{
			return (Pop3CountersInstance)Pop3Counters.counters.GetInstance(instanceName);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000664A File Offset: 0x0000484A
		public static void CloseInstance(string instanceName)
		{
			Pop3Counters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006657 File Offset: 0x00004857
		public static bool InstanceExists(string instanceName)
		{
			return Pop3Counters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006664 File Offset: 0x00004864
		public static string[] GetInstanceNames()
		{
			return Pop3Counters.counters.GetInstanceNames();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006670 File Offset: 0x00004870
		public static void RemoveInstance(string instanceName)
		{
			Pop3Counters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000667D File Offset: 0x0000487D
		public static void ResetInstance(string instanceName)
		{
			Pop3Counters.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000668A File Offset: 0x0000488A
		public static void RemoveAllInstances()
		{
			Pop3Counters.counters.RemoveAllInstances();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006696 File Offset: 0x00004896
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new Pop3CountersInstance(instanceName, (Pop3CountersInstance)totalInstance);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000066A4 File Offset: 0x000048A4
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new Pop3CountersInstance(instanceName);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000066AC File Offset: 0x000048AC
		public static Pop3CountersInstance TotalInstance
		{
			get
			{
				return (Pop3CountersInstance)Pop3Counters.counters.TotalInstance;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000066BD File Offset: 0x000048BD
		public static void GetPerfCounterInfo(XElement element)
		{
			if (Pop3Counters.counters == null)
			{
				return;
			}
			Pop3Counters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040000AC RID: 172
		public const string CategoryName = "MSExchangePop3";

		// Token: 0x040000AD RID: 173
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangePop3", new CreateInstanceDelegate(Pop3Counters.CreateInstance), new CreateTotalInstanceDelegate(Pop3Counters.CreateTotalInstance));
	}
}

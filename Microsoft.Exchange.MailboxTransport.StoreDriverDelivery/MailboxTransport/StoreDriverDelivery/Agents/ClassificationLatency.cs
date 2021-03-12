using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x0200007F RID: 127
	internal static class ClassificationLatency
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x00017350 File Offset: 0x00015550
		public static ClassificationLatencyInstance GetInstance(string instanceName)
		{
			return (ClassificationLatencyInstance)ClassificationLatency.counters.GetInstance(instanceName);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00017362 File Offset: 0x00015562
		public static void CloseInstance(string instanceName)
		{
			ClassificationLatency.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001736F File Offset: 0x0001556F
		public static bool InstanceExists(string instanceName)
		{
			return ClassificationLatency.counters.InstanceExists(instanceName);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001737C File Offset: 0x0001557C
		public static string[] GetInstanceNames()
		{
			return ClassificationLatency.counters.GetInstanceNames();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00017388 File Offset: 0x00015588
		public static void RemoveInstance(string instanceName)
		{
			ClassificationLatency.counters.RemoveInstance(instanceName);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00017395 File Offset: 0x00015595
		public static void ResetInstance(string instanceName)
		{
			ClassificationLatency.counters.ResetInstance(instanceName);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000173A2 File Offset: 0x000155A2
		public static void RemoveAllInstances()
		{
			ClassificationLatency.counters.RemoveAllInstances();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000173AE File Offset: 0x000155AE
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ClassificationLatencyInstance(instanceName, (ClassificationLatencyInstance)totalInstance);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000173BC File Offset: 0x000155BC
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ClassificationLatencyInstance(instanceName);
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x000173C4 File Offset: 0x000155C4
		public static ClassificationLatencyInstance TotalInstance
		{
			get
			{
				return (ClassificationLatencyInstance)ClassificationLatency.counters.TotalInstance;
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000173D5 File Offset: 0x000155D5
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ClassificationLatency.counters == null)
			{
				return;
			}
			ClassificationLatency.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400028A RID: 650
		public const string CategoryName = "MSExchangeInference Classification Latency";

		// Token: 0x0400028B RID: 651
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeInference Classification Latency", new CreateInstanceDelegate(ClassificationLatency.CreateInstance), new CreateTotalInstanceDelegate(ClassificationLatency.CreateTotalInstance));
	}
}

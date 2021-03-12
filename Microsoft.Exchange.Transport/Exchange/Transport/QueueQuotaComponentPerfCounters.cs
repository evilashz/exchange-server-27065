using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200054A RID: 1354
	internal static class QueueQuotaComponentPerfCounters
	{
		// Token: 0x06003E9F RID: 16031 RVA: 0x0010C208 File Offset: 0x0010A408
		public static QueueQuotaComponentPerfCountersInstance GetInstance(string instanceName)
		{
			return (QueueQuotaComponentPerfCountersInstance)QueueQuotaComponentPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x0010C21A File Offset: 0x0010A41A
		public static void CloseInstance(string instanceName)
		{
			QueueQuotaComponentPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x0010C227 File Offset: 0x0010A427
		public static bool InstanceExists(string instanceName)
		{
			return QueueQuotaComponentPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x0010C234 File Offset: 0x0010A434
		public static string[] GetInstanceNames()
		{
			return QueueQuotaComponentPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x0010C240 File Offset: 0x0010A440
		public static void RemoveInstance(string instanceName)
		{
			QueueQuotaComponentPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x0010C24D File Offset: 0x0010A44D
		public static void ResetInstance(string instanceName)
		{
			QueueQuotaComponentPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x0010C25A File Offset: 0x0010A45A
		public static void RemoveAllInstances()
		{
			QueueQuotaComponentPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x0010C266 File Offset: 0x0010A466
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new QueueQuotaComponentPerfCountersInstance(instanceName, (QueueQuotaComponentPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x0010C274 File Offset: 0x0010A474
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new QueueQuotaComponentPerfCountersInstance(instanceName);
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x0010C27C File Offset: 0x0010A47C
		public static QueueQuotaComponentPerfCountersInstance TotalInstance
		{
			get
			{
				return (QueueQuotaComponentPerfCountersInstance)QueueQuotaComponentPerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06003EA9 RID: 16041 RVA: 0x0010C28D File Offset: 0x0010A48D
		public static void GetPerfCounterInfo(XElement element)
		{
			if (QueueQuotaComponentPerfCounters.counters == null)
			{
				return;
			}
			QueueQuotaComponentPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040022CE RID: 8910
		public const string CategoryName = "MSExchangeTransport Queue Quota Component";

		// Token: 0x040022CF RID: 8911
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeTransport Queue Quota Component", new CreateInstanceDelegate(QueueQuotaComponentPerfCounters.CreateInstance), new CreateTotalInstanceDelegate(QueueQuotaComponentPerfCounters.CreateTotalInstance));
	}
}

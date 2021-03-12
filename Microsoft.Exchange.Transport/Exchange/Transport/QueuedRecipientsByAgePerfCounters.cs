using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200056F RID: 1391
	internal static class QueuedRecipientsByAgePerfCounters
	{
		// Token: 0x06003F98 RID: 16280 RVA: 0x001148D0 File Offset: 0x00112AD0
		public static QueuedRecipientsByAgePerfCountersInstance GetInstance(string instanceName)
		{
			return (QueuedRecipientsByAgePerfCountersInstance)QueuedRecipientsByAgePerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x001148E2 File Offset: 0x00112AE2
		public static void CloseInstance(string instanceName)
		{
			QueuedRecipientsByAgePerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x001148EF File Offset: 0x00112AEF
		public static bool InstanceExists(string instanceName)
		{
			return QueuedRecipientsByAgePerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x001148FC File Offset: 0x00112AFC
		public static string[] GetInstanceNames()
		{
			return QueuedRecipientsByAgePerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x00114908 File Offset: 0x00112B08
		public static void RemoveInstance(string instanceName)
		{
			QueuedRecipientsByAgePerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x00114915 File Offset: 0x00112B15
		public static void ResetInstance(string instanceName)
		{
			QueuedRecipientsByAgePerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x00114922 File Offset: 0x00112B22
		public static void RemoveAllInstances()
		{
			QueuedRecipientsByAgePerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x0011492E File Offset: 0x00112B2E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new QueuedRecipientsByAgePerfCountersInstance(instanceName, (QueuedRecipientsByAgePerfCountersInstance)totalInstance);
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x0011493C File Offset: 0x00112B3C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new QueuedRecipientsByAgePerfCountersInstance(instanceName);
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06003FA1 RID: 16289 RVA: 0x00114944 File Offset: 0x00112B44
		public static QueuedRecipientsByAgePerfCountersInstance TotalInstance
		{
			get
			{
				return (QueuedRecipientsByAgePerfCountersInstance)QueuedRecipientsByAgePerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x00114955 File Offset: 0x00112B55
		public static void GetPerfCounterInfo(XElement element)
		{
			if (QueuedRecipientsByAgePerfCounters.counters == null)
			{
				return;
			}
			QueuedRecipientsByAgePerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040023C9 RID: 9161
		public const string CategoryName = "MSExchangeTransport Queued Recipients By Age";

		// Token: 0x040023CA RID: 9162
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeTransport Queued Recipients By Age", new CreateInstanceDelegate(QueuedRecipientsByAgePerfCounters.CreateInstance), new CreateTotalInstanceDelegate(QueuedRecipientsByAgePerfCounters.CreateTotalInstance));
	}
}

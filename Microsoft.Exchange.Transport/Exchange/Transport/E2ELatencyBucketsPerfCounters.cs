using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200056D RID: 1389
	internal static class E2ELatencyBucketsPerfCounters
	{
		// Token: 0x06003F89 RID: 16265 RVA: 0x00114314 File Offset: 0x00112514
		public static E2ELatencyBucketsPerfCountersInstance GetInstance(string instanceName)
		{
			return (E2ELatencyBucketsPerfCountersInstance)E2ELatencyBucketsPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003F8A RID: 16266 RVA: 0x00114326 File Offset: 0x00112526
		public static void CloseInstance(string instanceName)
		{
			E2ELatencyBucketsPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003F8B RID: 16267 RVA: 0x00114333 File Offset: 0x00112533
		public static bool InstanceExists(string instanceName)
		{
			return E2ELatencyBucketsPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003F8C RID: 16268 RVA: 0x00114340 File Offset: 0x00112540
		public static string[] GetInstanceNames()
		{
			return E2ELatencyBucketsPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003F8D RID: 16269 RVA: 0x0011434C File Offset: 0x0011254C
		public static void RemoveInstance(string instanceName)
		{
			E2ELatencyBucketsPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x00114359 File Offset: 0x00112559
		public static void ResetInstance(string instanceName)
		{
			E2ELatencyBucketsPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x00114366 File Offset: 0x00112566
		public static void RemoveAllInstances()
		{
			E2ELatencyBucketsPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x00114372 File Offset: 0x00112572
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new E2ELatencyBucketsPerfCountersInstance(instanceName, (E2ELatencyBucketsPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003F91 RID: 16273 RVA: 0x00114380 File Offset: 0x00112580
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new E2ELatencyBucketsPerfCountersInstance(instanceName);
		}

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06003F92 RID: 16274 RVA: 0x00114388 File Offset: 0x00112588
		public static E2ELatencyBucketsPerfCountersInstance TotalInstance
		{
			get
			{
				return (E2ELatencyBucketsPerfCountersInstance)E2ELatencyBucketsPerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06003F93 RID: 16275 RVA: 0x00114399 File Offset: 0x00112599
		public static void GetPerfCounterInfo(XElement element)
		{
			if (E2ELatencyBucketsPerfCounters.counters == null)
			{
				return;
			}
			E2ELatencyBucketsPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040023BF RID: 9151
		public const string CategoryName = "MSExchangeTransport E2E Latency Buckets";

		// Token: 0x040023C0 RID: 9152
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeTransport E2E Latency Buckets", new CreateInstanceDelegate(E2ELatencyBucketsPerfCounters.CreateInstance), new CreateTotalInstanceDelegate(E2ELatencyBucketsPerfCounters.CreateTotalInstance));
	}
}

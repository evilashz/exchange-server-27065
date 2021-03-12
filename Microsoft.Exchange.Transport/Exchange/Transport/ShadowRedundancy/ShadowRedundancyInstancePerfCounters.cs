using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000563 RID: 1379
	internal static class ShadowRedundancyInstancePerfCounters
	{
		// Token: 0x06003F42 RID: 16194 RVA: 0x001129D4 File Offset: 0x00110BD4
		public static ShadowRedundancyInstancePerfCountersInstance GetInstance(string instanceName)
		{
			return (ShadowRedundancyInstancePerfCountersInstance)ShadowRedundancyInstancePerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x001129E6 File Offset: 0x00110BE6
		public static void CloseInstance(string instanceName)
		{
			ShadowRedundancyInstancePerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x001129F3 File Offset: 0x00110BF3
		public static bool InstanceExists(string instanceName)
		{
			return ShadowRedundancyInstancePerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x00112A00 File Offset: 0x00110C00
		public static string[] GetInstanceNames()
		{
			return ShadowRedundancyInstancePerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x00112A0C File Offset: 0x00110C0C
		public static void RemoveInstance(string instanceName)
		{
			ShadowRedundancyInstancePerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x00112A19 File Offset: 0x00110C19
		public static void ResetInstance(string instanceName)
		{
			ShadowRedundancyInstancePerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x00112A26 File Offset: 0x00110C26
		public static void RemoveAllInstances()
		{
			ShadowRedundancyInstancePerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x00112A32 File Offset: 0x00110C32
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ShadowRedundancyInstancePerfCountersInstance(instanceName, (ShadowRedundancyInstancePerfCountersInstance)totalInstance);
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x00112A40 File Offset: 0x00110C40
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ShadowRedundancyInstancePerfCountersInstance(instanceName);
		}

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x00112A48 File Offset: 0x00110C48
		public static ShadowRedundancyInstancePerfCountersInstance TotalInstance
		{
			get
			{
				return (ShadowRedundancyInstancePerfCountersInstance)ShadowRedundancyInstancePerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x00112A59 File Offset: 0x00110C59
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ShadowRedundancyInstancePerfCounters.counters == null)
			{
				return;
			}
			ShadowRedundancyInstancePerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04002393 RID: 9107
		public const string CategoryName = "MSExchangeTransport Shadow Redundancy Host Info";

		// Token: 0x04002394 RID: 9108
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeTransport Shadow Redundancy Host Info", new CreateInstanceDelegate(ShadowRedundancyInstancePerfCounters.CreateInstance), new CreateTotalInstanceDelegate(ShadowRedundancyInstancePerfCounters.CreateTotalInstance));
	}
}

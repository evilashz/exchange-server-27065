using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200053A RID: 1338
	internal static class InboundProxyAccountForestPerfCounters
	{
		// Token: 0x06003E2A RID: 15914 RVA: 0x00106DD8 File Offset: 0x00104FD8
		public static InboundProxyAccountForestPerfCountersInstance GetInstance(string instanceName)
		{
			return (InboundProxyAccountForestPerfCountersInstance)InboundProxyAccountForestPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x00106DEA File Offset: 0x00104FEA
		public static void CloseInstance(string instanceName)
		{
			InboundProxyAccountForestPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x00106DF7 File Offset: 0x00104FF7
		public static bool InstanceExists(string instanceName)
		{
			return InboundProxyAccountForestPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x00106E04 File Offset: 0x00105004
		public static string[] GetInstanceNames()
		{
			return InboundProxyAccountForestPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x00106E10 File Offset: 0x00105010
		public static void RemoveInstance(string instanceName)
		{
			InboundProxyAccountForestPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x00106E1D File Offset: 0x0010501D
		public static void ResetInstance(string instanceName)
		{
			InboundProxyAccountForestPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x00106E2A File Offset: 0x0010502A
		public static void RemoveAllInstances()
		{
			InboundProxyAccountForestPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x00106E36 File Offset: 0x00105036
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new InboundProxyAccountForestPerfCountersInstance(instanceName, (InboundProxyAccountForestPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x00106E44 File Offset: 0x00105044
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new InboundProxyAccountForestPerfCountersInstance(instanceName);
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06003E33 RID: 15923 RVA: 0x00106E4C File Offset: 0x0010504C
		public static InboundProxyAccountForestPerfCountersInstance TotalInstance
		{
			get
			{
				return (InboundProxyAccountForestPerfCountersInstance)InboundProxyAccountForestPerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x00106E5D File Offset: 0x0010505D
		public static void GetPerfCounterInfo(XElement element)
		{
			if (InboundProxyAccountForestPerfCounters.counters == null)
			{
				return;
			}
			InboundProxyAccountForestPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x0400224D RID: 8781
		public const string CategoryName = "MSExchangeFrontendTransport InboundProxyEXOAccountForests";

		// Token: 0x0400224E RID: 8782
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeFrontendTransport InboundProxyEXOAccountForests", new CreateInstanceDelegate(InboundProxyAccountForestPerfCounters.CreateInstance), new CreateTotalInstanceDelegate(InboundProxyAccountForestPerfCounters.CreateTotalInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000538 RID: 1336
	internal static class InboundProxyDestinationPerfCounters
	{
		// Token: 0x06003E1B RID: 15899 RVA: 0x001066AD File Offset: 0x001048AD
		public static InboundProxyDestinationPerfCountersInstance GetInstance(string instanceName)
		{
			return (InboundProxyDestinationPerfCountersInstance)InboundProxyDestinationPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x001066BF File Offset: 0x001048BF
		public static void CloseInstance(string instanceName)
		{
			InboundProxyDestinationPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x001066CC File Offset: 0x001048CC
		public static bool InstanceExists(string instanceName)
		{
			return InboundProxyDestinationPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x001066D9 File Offset: 0x001048D9
		public static string[] GetInstanceNames()
		{
			return InboundProxyDestinationPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x001066E5 File Offset: 0x001048E5
		public static void RemoveInstance(string instanceName)
		{
			InboundProxyDestinationPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x001066F2 File Offset: 0x001048F2
		public static void ResetInstance(string instanceName)
		{
			InboundProxyDestinationPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x001066FF File Offset: 0x001048FF
		public static void RemoveAllInstances()
		{
			InboundProxyDestinationPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x0010670B File Offset: 0x0010490B
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new InboundProxyDestinationPerfCountersInstance(instanceName, (InboundProxyDestinationPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x00106719 File Offset: 0x00104919
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new InboundProxyDestinationPerfCountersInstance(instanceName);
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06003E24 RID: 15908 RVA: 0x00106721 File Offset: 0x00104921
		public static InboundProxyDestinationPerfCountersInstance TotalInstance
		{
			get
			{
				return (InboundProxyDestinationPerfCountersInstance)InboundProxyDestinationPerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x00106732 File Offset: 0x00104932
		public static void GetPerfCounterInfo(XElement element)
		{
			if (InboundProxyDestinationPerfCounters.counters == null)
			{
				return;
			}
			InboundProxyDestinationPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04002246 RID: 8774
		public const string CategoryName = "MSExchangeFrontendTransport InboundProxyDestinations";

		// Token: 0x04002247 RID: 8775
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeFrontendTransport InboundProxyDestinations", new CreateInstanceDelegate(InboundProxyDestinationPerfCounters.CreateInstance), new CreateTotalInstanceDelegate(InboundProxyDestinationPerfCounters.CreateTotalInstance));
	}
}

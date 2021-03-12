using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200053C RID: 1340
	internal static class OutboundProxyBySourcePerfCounters
	{
		// Token: 0x06003E39 RID: 15929 RVA: 0x00107504 File Offset: 0x00105704
		public static OutboundProxyBySourcePerfCountersInstance GetInstance(string instanceName)
		{
			return (OutboundProxyBySourcePerfCountersInstance)OutboundProxyBySourcePerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x00107516 File Offset: 0x00105716
		public static void CloseInstance(string instanceName)
		{
			OutboundProxyBySourcePerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x00107523 File Offset: 0x00105723
		public static bool InstanceExists(string instanceName)
		{
			return OutboundProxyBySourcePerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x00107530 File Offset: 0x00105730
		public static string[] GetInstanceNames()
		{
			return OutboundProxyBySourcePerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x0010753C File Offset: 0x0010573C
		public static void RemoveInstance(string instanceName)
		{
			OutboundProxyBySourcePerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x00107549 File Offset: 0x00105749
		public static void ResetInstance(string instanceName)
		{
			OutboundProxyBySourcePerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x00107556 File Offset: 0x00105756
		public static void RemoveAllInstances()
		{
			OutboundProxyBySourcePerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x00107562 File Offset: 0x00105762
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new OutboundProxyBySourcePerfCountersInstance(instanceName, (OutboundProxyBySourcePerfCountersInstance)totalInstance);
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x00107570 File Offset: 0x00105770
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new OutboundProxyBySourcePerfCountersInstance(instanceName);
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06003E42 RID: 15938 RVA: 0x00107578 File Offset: 0x00105778
		public static OutboundProxyBySourcePerfCountersInstance TotalInstance
		{
			get
			{
				return (OutboundProxyBySourcePerfCountersInstance)OutboundProxyBySourcePerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x00107589 File Offset: 0x00105789
		public static void GetPerfCounterInfo(XElement element)
		{
			if (OutboundProxyBySourcePerfCounters.counters == null)
			{
				return;
			}
			OutboundProxyBySourcePerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04002254 RID: 8788
		public const string CategoryName = "MSExchangeFrontendTransport OutboundProxyBySource";

		// Token: 0x04002255 RID: 8789
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeFrontendTransport OutboundProxyBySource", new CreateInstanceDelegate(OutboundProxyBySourcePerfCounters.CreateInstance), new CreateTotalInstanceDelegate(OutboundProxyBySourcePerfCounters.CreateTotalInstance));
	}
}

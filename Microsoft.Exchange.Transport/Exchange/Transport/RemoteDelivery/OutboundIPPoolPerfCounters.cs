using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x0200054F RID: 1359
	internal static class OutboundIPPoolPerfCounters
	{
		// Token: 0x06003EBF RID: 16063 RVA: 0x0010CF2C File Offset: 0x0010B12C
		public static OutboundIPPoolPerfCountersInstance GetInstance(string instanceName)
		{
			return (OutboundIPPoolPerfCountersInstance)OutboundIPPoolPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x0010CF3E File Offset: 0x0010B13E
		public static void CloseInstance(string instanceName)
		{
			OutboundIPPoolPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x0010CF4B File Offset: 0x0010B14B
		public static bool InstanceExists(string instanceName)
		{
			return OutboundIPPoolPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x0010CF58 File Offset: 0x0010B158
		public static string[] GetInstanceNames()
		{
			return OutboundIPPoolPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x0010CF64 File Offset: 0x0010B164
		public static void RemoveInstance(string instanceName)
		{
			OutboundIPPoolPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x0010CF71 File Offset: 0x0010B171
		public static void ResetInstance(string instanceName)
		{
			OutboundIPPoolPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x0010CF7E File Offset: 0x0010B17E
		public static void RemoveAllInstances()
		{
			OutboundIPPoolPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x0010CF8A File Offset: 0x0010B18A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new OutboundIPPoolPerfCountersInstance(instanceName, (OutboundIPPoolPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x0010CF98 File Offset: 0x0010B198
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new OutboundIPPoolPerfCountersInstance(instanceName);
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x0010CFA0 File Offset: 0x0010B1A0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (OutboundIPPoolPerfCounters.counters == null)
			{
				return;
			}
			OutboundIPPoolPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040022EF RID: 8943
		public const string CategoryName = "MSExchangeTransport Outbound IP Pools";

		// Token: 0x040022F0 RID: 8944
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeTransport Outbound IP Pools", new CreateInstanceDelegate(OutboundIPPoolPerfCounters.CreateInstance));
	}
}

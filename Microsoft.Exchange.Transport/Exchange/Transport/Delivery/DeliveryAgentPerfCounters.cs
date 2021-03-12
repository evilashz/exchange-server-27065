using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Delivery
{
	// Token: 0x02000571 RID: 1393
	internal static class DeliveryAgentPerfCounters
	{
		// Token: 0x06003FA7 RID: 16295 RVA: 0x00114CE0 File Offset: 0x00112EE0
		public static DeliveryAgentPerfCountersInstance GetInstance(string instanceName)
		{
			return (DeliveryAgentPerfCountersInstance)DeliveryAgentPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x00114CF2 File Offset: 0x00112EF2
		public static void CloseInstance(string instanceName)
		{
			DeliveryAgentPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x00114CFF File Offset: 0x00112EFF
		public static bool InstanceExists(string instanceName)
		{
			return DeliveryAgentPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x00114D0C File Offset: 0x00112F0C
		public static string[] GetInstanceNames()
		{
			return DeliveryAgentPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x00114D18 File Offset: 0x00112F18
		public static void RemoveInstance(string instanceName)
		{
			DeliveryAgentPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x00114D25 File Offset: 0x00112F25
		public static void ResetInstance(string instanceName)
		{
			DeliveryAgentPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x00114D32 File Offset: 0x00112F32
		public static void RemoveAllInstances()
		{
			DeliveryAgentPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x00114D3E File Offset: 0x00112F3E
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new DeliveryAgentPerfCountersInstance(instanceName, (DeliveryAgentPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x00114D4C File Offset: 0x00112F4C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new DeliveryAgentPerfCountersInstance(instanceName);
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x00114D54 File Offset: 0x00112F54
		public static void GetPerfCounterInfo(XElement element)
		{
			if (DeliveryAgentPerfCounters.counters == null)
			{
				return;
			}
			DeliveryAgentPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040023CF RID: 9167
		public const string CategoryName = "MSExchangeTransport DeliveryAgent";

		// Token: 0x040023D0 RID: 9168
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeTransport DeliveryAgent", new CreateInstanceDelegate(DeliveryAgentPerfCounters.CreateInstance));
	}
}

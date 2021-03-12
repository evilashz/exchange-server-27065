using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Pickup
{
	// Token: 0x02000551 RID: 1361
	internal static class PickupPerfCounters
	{
		// Token: 0x06003ECD RID: 16077 RVA: 0x0010D2FC File Offset: 0x0010B4FC
		public static PickupPerfCountersInstance GetInstance(string instanceName)
		{
			return (PickupPerfCountersInstance)PickupPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x0010D30E File Offset: 0x0010B50E
		public static void CloseInstance(string instanceName)
		{
			PickupPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x0010D31B File Offset: 0x0010B51B
		public static bool InstanceExists(string instanceName)
		{
			return PickupPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x0010D328 File Offset: 0x0010B528
		public static string[] GetInstanceNames()
		{
			return PickupPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x0010D334 File Offset: 0x0010B534
		public static void RemoveInstance(string instanceName)
		{
			PickupPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x0010D341 File Offset: 0x0010B541
		public static void ResetInstance(string instanceName)
		{
			PickupPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x0010D34E File Offset: 0x0010B54E
		public static void RemoveAllInstances()
		{
			PickupPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x0010D35A File Offset: 0x0010B55A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new PickupPerfCountersInstance(instanceName, (PickupPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x0010D368 File Offset: 0x0010B568
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new PickupPerfCountersInstance(instanceName);
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x0010D370 File Offset: 0x0010B570
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PickupPerfCounters.counters == null)
			{
				return;
			}
			PickupPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040022F5 RID: 8949
		public const string CategoryName = "MSExchangeTransport Pickup";

		// Token: 0x040022F6 RID: 8950
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeTransport Pickup", new CreateInstanceDelegate(PickupPerfCounters.CreateInstance));
	}
}

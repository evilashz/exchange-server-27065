using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000559 RID: 1369
	internal static class ResolverPerfCounters
	{
		// Token: 0x06003EFB RID: 16123 RVA: 0x0010E024 File Offset: 0x0010C224
		public static ResolverPerfCountersInstance GetInstance(string instanceName)
		{
			return (ResolverPerfCountersInstance)ResolverPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x0010E036 File Offset: 0x0010C236
		public static void CloseInstance(string instanceName)
		{
			ResolverPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x0010E043 File Offset: 0x0010C243
		public static bool InstanceExists(string instanceName)
		{
			return ResolverPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x0010E050 File Offset: 0x0010C250
		public static string[] GetInstanceNames()
		{
			return ResolverPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x0010E05C File Offset: 0x0010C25C
		public static void RemoveInstance(string instanceName)
		{
			ResolverPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x0010E069 File Offset: 0x0010C269
		public static void ResetInstance(string instanceName)
		{
			ResolverPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x0010E076 File Offset: 0x0010C276
		public static void RemoveAllInstances()
		{
			ResolverPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x0010E082 File Offset: 0x0010C282
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new ResolverPerfCountersInstance(instanceName, (ResolverPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003F03 RID: 16131 RVA: 0x0010E090 File Offset: 0x0010C290
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new ResolverPerfCountersInstance(instanceName);
		}

		// Token: 0x06003F04 RID: 16132 RVA: 0x0010E098 File Offset: 0x0010C298
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ResolverPerfCounters.counters == null)
			{
				return;
			}
			ResolverPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04002311 RID: 8977
		public const string CategoryName = "MSExchangeTransport Resolver";

		// Token: 0x04002312 RID: 8978
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeTransport Resolver", new CreateInstanceDelegate(ResolverPerfCounters.CreateInstance));
	}
}

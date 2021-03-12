using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.IsMemberOfProvider
{
	// Token: 0x02000A47 RID: 2631
	internal static class IsMemberOfResolverPerfCounters
	{
		// Token: 0x0600785D RID: 30813 RVA: 0x0018E7A0 File Offset: 0x0018C9A0
		public static IsMemberOfResolverPerfCountersInstance GetInstance(string instanceName)
		{
			return (IsMemberOfResolverPerfCountersInstance)IsMemberOfResolverPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x0600785E RID: 30814 RVA: 0x0018E7B2 File Offset: 0x0018C9B2
		public static void CloseInstance(string instanceName)
		{
			IsMemberOfResolverPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x0600785F RID: 30815 RVA: 0x0018E7BF File Offset: 0x0018C9BF
		public static bool InstanceExists(string instanceName)
		{
			return IsMemberOfResolverPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06007860 RID: 30816 RVA: 0x0018E7CC File Offset: 0x0018C9CC
		public static string[] GetInstanceNames()
		{
			return IsMemberOfResolverPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06007861 RID: 30817 RVA: 0x0018E7D8 File Offset: 0x0018C9D8
		public static void RemoveInstance(string instanceName)
		{
			IsMemberOfResolverPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06007862 RID: 30818 RVA: 0x0018E7E5 File Offset: 0x0018C9E5
		public static void ResetInstance(string instanceName)
		{
			IsMemberOfResolverPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06007863 RID: 30819 RVA: 0x0018E7F2 File Offset: 0x0018C9F2
		public static void RemoveAllInstances()
		{
			IsMemberOfResolverPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06007864 RID: 30820 RVA: 0x0018E7FE File Offset: 0x0018C9FE
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new IsMemberOfResolverPerfCountersInstance(instanceName, (IsMemberOfResolverPerfCountersInstance)totalInstance);
		}

		// Token: 0x06007865 RID: 30821 RVA: 0x0018E80C File Offset: 0x0018CA0C
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new IsMemberOfResolverPerfCountersInstance(instanceName);
		}

		// Token: 0x06007866 RID: 30822 RVA: 0x0018E814 File Offset: 0x0018CA14
		public static void GetPerfCounterInfo(XElement element)
		{
			if (IsMemberOfResolverPerfCounters.counters == null)
			{
				return;
			}
			IsMemberOfResolverPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04004F4B RID: 20299
		public const string CategoryName = "Expanded Groups Cache";

		// Token: 0x04004F4C RID: 20300
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("Expanded Groups Cache", new CreateInstanceDelegate(IsMemberOfResolverPerfCounters.CreateInstance));
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000573 RID: 1395
	internal static class SmtpConnectionCachePerfCounters
	{
		// Token: 0x06003FB5 RID: 16309 RVA: 0x00115850 File Offset: 0x00113A50
		public static SmtpConnectionCachePerfCountersInstance GetInstance(string instanceName)
		{
			return (SmtpConnectionCachePerfCountersInstance)SmtpConnectionCachePerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x00115862 File Offset: 0x00113A62
		public static void CloseInstance(string instanceName)
		{
			SmtpConnectionCachePerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x0011586F File Offset: 0x00113A6F
		public static bool InstanceExists(string instanceName)
		{
			return SmtpConnectionCachePerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x0011587C File Offset: 0x00113A7C
		public static string[] GetInstanceNames()
		{
			return SmtpConnectionCachePerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x00115888 File Offset: 0x00113A88
		public static void RemoveInstance(string instanceName)
		{
			SmtpConnectionCachePerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x00115895 File Offset: 0x00113A95
		public static void ResetInstance(string instanceName)
		{
			SmtpConnectionCachePerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x001158A2 File Offset: 0x00113AA2
		public static void RemoveAllInstances()
		{
			SmtpConnectionCachePerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x001158AE File Offset: 0x00113AAE
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new SmtpConnectionCachePerfCountersInstance(instanceName, (SmtpConnectionCachePerfCountersInstance)totalInstance);
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x001158BC File Offset: 0x00113ABC
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new SmtpConnectionCachePerfCountersInstance(instanceName);
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x001158C4 File Offset: 0x00113AC4
		public static void SetCategoryName(string categoryName)
		{
			if (SmtpConnectionCachePerfCounters.counters == null)
			{
				SmtpConnectionCachePerfCounters.CategoryName = categoryName;
				SmtpConnectionCachePerfCounters.counters = new PerformanceCounterMultipleInstance(SmtpConnectionCachePerfCounters.CategoryName, new CreateInstanceDelegate(SmtpConnectionCachePerfCounters.CreateInstance));
			}
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x001158EE File Offset: 0x00113AEE
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SmtpConnectionCachePerfCounters.counters == null)
			{
				return;
			}
			SmtpConnectionCachePerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040023DB RID: 9179
		public static string CategoryName;

		// Token: 0x040023DC RID: 9180
		private static PerformanceCounterMultipleInstance counters;
	}
}

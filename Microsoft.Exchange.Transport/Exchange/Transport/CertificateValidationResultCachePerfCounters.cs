using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000575 RID: 1397
	internal static class CertificateValidationResultCachePerfCounters
	{
		// Token: 0x06003FC3 RID: 16323 RVA: 0x00115C8C File Offset: 0x00113E8C
		public static CertificateValidationResultCachePerfCountersInstance GetInstance(string instanceName)
		{
			return (CertificateValidationResultCachePerfCountersInstance)CertificateValidationResultCachePerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x00115C9E File Offset: 0x00113E9E
		public static void CloseInstance(string instanceName)
		{
			CertificateValidationResultCachePerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x00115CAB File Offset: 0x00113EAB
		public static bool InstanceExists(string instanceName)
		{
			return CertificateValidationResultCachePerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x00115CB8 File Offset: 0x00113EB8
		public static string[] GetInstanceNames()
		{
			return CertificateValidationResultCachePerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x00115CC4 File Offset: 0x00113EC4
		public static void RemoveInstance(string instanceName)
		{
			CertificateValidationResultCachePerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x00115CD1 File Offset: 0x00113ED1
		public static void ResetInstance(string instanceName)
		{
			CertificateValidationResultCachePerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x00115CDE File Offset: 0x00113EDE
		public static void RemoveAllInstances()
		{
			CertificateValidationResultCachePerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x00115CEA File Offset: 0x00113EEA
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new CertificateValidationResultCachePerfCountersInstance(instanceName, (CertificateValidationResultCachePerfCountersInstance)totalInstance);
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x00115CF8 File Offset: 0x00113EF8
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new CertificateValidationResultCachePerfCountersInstance(instanceName);
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x00115D00 File Offset: 0x00113F00
		public static void SetCategoryName(string categoryName)
		{
			if (CertificateValidationResultCachePerfCounters.counters == null)
			{
				CertificateValidationResultCachePerfCounters.CategoryName = categoryName;
				CertificateValidationResultCachePerfCounters.counters = new PerformanceCounterMultipleInstance(CertificateValidationResultCachePerfCounters.CategoryName, new CreateInstanceDelegate(CertificateValidationResultCachePerfCounters.CreateInstance));
			}
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x00115D2A File Offset: 0x00113F2A
		public static void GetPerfCounterInfo(XElement element)
		{
			if (CertificateValidationResultCachePerfCounters.counters == null)
			{
				return;
			}
			CertificateValidationResultCachePerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x040023E1 RID: 9185
		public static string CategoryName;

		// Token: 0x040023E2 RID: 9186
		private static PerformanceCounterMultipleInstance counters;
	}
}

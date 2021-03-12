using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200053E RID: 1342
	internal static class SmtpAvailabilityPerfCounters
	{
		// Token: 0x06003E48 RID: 15944 RVA: 0x001078B8 File Offset: 0x00105AB8
		public static SmtpAvailabilityPerfCountersInstance GetInstance(string instanceName)
		{
			return (SmtpAvailabilityPerfCountersInstance)SmtpAvailabilityPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x001078CA File Offset: 0x00105ACA
		public static void CloseInstance(string instanceName)
		{
			SmtpAvailabilityPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x001078D7 File Offset: 0x00105AD7
		public static bool InstanceExists(string instanceName)
		{
			return SmtpAvailabilityPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x001078E4 File Offset: 0x00105AE4
		public static string[] GetInstanceNames()
		{
			return SmtpAvailabilityPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x001078F0 File Offset: 0x00105AF0
		public static void RemoveInstance(string instanceName)
		{
			SmtpAvailabilityPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x001078FD File Offset: 0x00105AFD
		public static void ResetInstance(string instanceName)
		{
			SmtpAvailabilityPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003E4E RID: 15950 RVA: 0x0010790A File Offset: 0x00105B0A
		public static void RemoveAllInstances()
		{
			SmtpAvailabilityPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x00107916 File Offset: 0x00105B16
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new SmtpAvailabilityPerfCountersInstance(instanceName, (SmtpAvailabilityPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x00107924 File Offset: 0x00105B24
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new SmtpAvailabilityPerfCountersInstance(instanceName);
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x0010792C File Offset: 0x00105B2C
		public static void SetCategoryName(string categoryName)
		{
			if (SmtpAvailabilityPerfCounters.counters == null)
			{
				SmtpAvailabilityPerfCounters.CategoryName = categoryName;
				SmtpAvailabilityPerfCounters.counters = new PerformanceCounterMultipleInstance(SmtpAvailabilityPerfCounters.CategoryName, new CreateInstanceDelegate(SmtpAvailabilityPerfCounters.CreateInstance));
			}
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x00107956 File Offset: 0x00105B56
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SmtpAvailabilityPerfCounters.counters == null)
			{
				return;
			}
			SmtpAvailabilityPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04002258 RID: 8792
		public static string CategoryName;

		// Token: 0x04002259 RID: 8793
		private static PerformanceCounterMultipleInstance counters;
	}
}

using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000542 RID: 1346
	internal static class SmtpReceivePerfCounters
	{
		// Token: 0x06003E65 RID: 15973 RVA: 0x00108E70 File Offset: 0x00107070
		public static SmtpReceivePerfCountersInstance GetInstance(string instanceName)
		{
			return (SmtpReceivePerfCountersInstance)SmtpReceivePerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x00108E82 File Offset: 0x00107082
		public static void CloseInstance(string instanceName)
		{
			SmtpReceivePerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003E67 RID: 15975 RVA: 0x00108E8F File Offset: 0x0010708F
		public static bool InstanceExists(string instanceName)
		{
			return SmtpReceivePerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x00108E9C File Offset: 0x0010709C
		public static string[] GetInstanceNames()
		{
			return SmtpReceivePerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x00108EA8 File Offset: 0x001070A8
		public static void RemoveInstance(string instanceName)
		{
			SmtpReceivePerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x00108EB5 File Offset: 0x001070B5
		public static void ResetInstance(string instanceName)
		{
			SmtpReceivePerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x00108EC2 File Offset: 0x001070C2
		public static void RemoveAllInstances()
		{
			SmtpReceivePerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x00108ECE File Offset: 0x001070CE
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new SmtpReceivePerfCountersInstance(instanceName, (SmtpReceivePerfCountersInstance)totalInstance);
		}

		// Token: 0x06003E6D RID: 15981 RVA: 0x00108EDC File Offset: 0x001070DC
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new SmtpReceivePerfCountersInstance(instanceName);
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x00108EE4 File Offset: 0x001070E4
		public static void SetCategoryName(string categoryName)
		{
			if (SmtpReceivePerfCounters.counters == null)
			{
				SmtpReceivePerfCounters.CategoryName = categoryName;
				SmtpReceivePerfCounters.counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal(SmtpReceivePerfCounters.CategoryName, new CreateInstanceDelegate(SmtpReceivePerfCounters.CreateInstance));
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06003E6F RID: 15983 RVA: 0x00108F0E File Offset: 0x0010710E
		public static SmtpReceivePerfCountersInstance TotalInstance
		{
			get
			{
				return (SmtpReceivePerfCountersInstance)SmtpReceivePerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x00108F1F File Offset: 0x0010711F
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SmtpReceivePerfCounters.counters == null)
			{
				return;
			}
			SmtpReceivePerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04002277 RID: 8823
		public static string CategoryName;

		// Token: 0x04002278 RID: 8824
		private static PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters;
	}
}

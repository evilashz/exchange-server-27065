using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000540 RID: 1344
	internal static class SmtpReceiveFrontendPerfCounters
	{
		// Token: 0x06003E56 RID: 15958 RVA: 0x00107F1C File Offset: 0x0010611C
		public static SmtpReceiveFrontendPerfCountersInstance GetInstance(string instanceName)
		{
			return (SmtpReceiveFrontendPerfCountersInstance)SmtpReceiveFrontendPerfCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x00107F2E File Offset: 0x0010612E
		public static void CloseInstance(string instanceName)
		{
			SmtpReceiveFrontendPerfCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x00107F3B File Offset: 0x0010613B
		public static bool InstanceExists(string instanceName)
		{
			return SmtpReceiveFrontendPerfCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x00107F48 File Offset: 0x00106148
		public static string[] GetInstanceNames()
		{
			return SmtpReceiveFrontendPerfCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x00107F54 File Offset: 0x00106154
		public static void RemoveInstance(string instanceName)
		{
			SmtpReceiveFrontendPerfCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x00107F61 File Offset: 0x00106161
		public static void ResetInstance(string instanceName)
		{
			SmtpReceiveFrontendPerfCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003E5C RID: 15964 RVA: 0x00107F6E File Offset: 0x0010616E
		public static void RemoveAllInstances()
		{
			SmtpReceiveFrontendPerfCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x00107F7A File Offset: 0x0010617A
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new SmtpReceiveFrontendPerfCountersInstance(instanceName, (SmtpReceiveFrontendPerfCountersInstance)totalInstance);
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x00107F88 File Offset: 0x00106188
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new SmtpReceiveFrontendPerfCountersInstance(instanceName);
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06003E5F RID: 15967 RVA: 0x00107F90 File Offset: 0x00106190
		public static SmtpReceiveFrontendPerfCountersInstance TotalInstance
		{
			get
			{
				return (SmtpReceiveFrontendPerfCountersInstance)SmtpReceiveFrontendPerfCounters.counters.TotalInstance;
			}
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x00107FA1 File Offset: 0x001061A1
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SmtpReceiveFrontendPerfCounters.counters == null)
			{
				return;
			}
			SmtpReceiveFrontendPerfCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04002265 RID: 8805
		public const string CategoryName = "MSExchangeFrontEndTransport SMTPReceive";

		// Token: 0x04002266 RID: 8806
		private static readonly PerformanceCounterMultipleInstanceWithAutoUpdateTotal counters = new PerformanceCounterMultipleInstanceWithAutoUpdateTotal("MSExchangeFrontEndTransport SMTPReceive", new CreateInstanceDelegate(SmtpReceiveFrontendPerfCounters.CreateInstance), new CreateTotalInstanceDelegate(SmtpReceiveFrontendPerfCounters.CreateTotalInstance));
	}
}

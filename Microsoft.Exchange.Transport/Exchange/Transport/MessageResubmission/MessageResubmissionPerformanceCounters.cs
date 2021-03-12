using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.MessageResubmission
{
	// Token: 0x02000561 RID: 1377
	internal static class MessageResubmissionPerformanceCounters
	{
		// Token: 0x06003F34 RID: 16180 RVA: 0x0011247C File Offset: 0x0011067C
		public static MessageResubmissionPerformanceCountersInstance GetInstance(string instanceName)
		{
			return (MessageResubmissionPerformanceCountersInstance)MessageResubmissionPerformanceCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x0011248E File Offset: 0x0011068E
		public static void CloseInstance(string instanceName)
		{
			MessageResubmissionPerformanceCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x0011249B File Offset: 0x0011069B
		public static bool InstanceExists(string instanceName)
		{
			return MessageResubmissionPerformanceCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x001124A8 File Offset: 0x001106A8
		public static string[] GetInstanceNames()
		{
			return MessageResubmissionPerformanceCounters.counters.GetInstanceNames();
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x001124B4 File Offset: 0x001106B4
		public static void RemoveInstance(string instanceName)
		{
			MessageResubmissionPerformanceCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x001124C1 File Offset: 0x001106C1
		public static void ResetInstance(string instanceName)
		{
			MessageResubmissionPerformanceCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x001124CE File Offset: 0x001106CE
		public static void RemoveAllInstances()
		{
			MessageResubmissionPerformanceCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x001124DA File Offset: 0x001106DA
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new MessageResubmissionPerformanceCountersInstance(instanceName, (MessageResubmissionPerformanceCountersInstance)totalInstance);
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x001124E8 File Offset: 0x001106E8
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new MessageResubmissionPerformanceCountersInstance(instanceName);
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x001124F0 File Offset: 0x001106F0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MessageResubmissionPerformanceCounters.counters == null)
			{
				return;
			}
			MessageResubmissionPerformanceCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04002389 RID: 9097
		public const string CategoryName = "MSExchangeTransport Safety Net";

		// Token: 0x0400238A RID: 9098
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeTransport Safety Net", new CreateInstanceDelegate(MessageResubmissionPerformanceCounters.CreateInstance));
	}
}

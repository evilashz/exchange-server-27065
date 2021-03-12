using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000324 RID: 804
	internal static class TranscriptionCounters
	{
		// Token: 0x06001BC1 RID: 7105 RVA: 0x0006B98D File Offset: 0x00069B8D
		public static TranscriptionCountersInstance GetInstance(string instanceName)
		{
			return (TranscriptionCountersInstance)TranscriptionCounters.counters.GetInstance(instanceName);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0006B99F File Offset: 0x00069B9F
		public static void CloseInstance(string instanceName)
		{
			TranscriptionCounters.counters.CloseInstance(instanceName);
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0006B9AC File Offset: 0x00069BAC
		public static bool InstanceExists(string instanceName)
		{
			return TranscriptionCounters.counters.InstanceExists(instanceName);
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0006B9B9 File Offset: 0x00069BB9
		public static string[] GetInstanceNames()
		{
			return TranscriptionCounters.counters.GetInstanceNames();
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0006B9C5 File Offset: 0x00069BC5
		public static void RemoveInstance(string instanceName)
		{
			TranscriptionCounters.counters.RemoveInstance(instanceName);
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0006B9D2 File Offset: 0x00069BD2
		public static void ResetInstance(string instanceName)
		{
			TranscriptionCounters.counters.ResetInstance(instanceName);
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0006B9DF File Offset: 0x00069BDF
		public static void RemoveAllInstances()
		{
			TranscriptionCounters.counters.RemoveAllInstances();
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0006B9EB File Offset: 0x00069BEB
		private static PerformanceCounterInstance CreateInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			return new TranscriptionCountersInstance(instanceName, (TranscriptionCountersInstance)totalInstance);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0006B9F9 File Offset: 0x00069BF9
		private static PerformanceCounterInstance CreateTotalInstance(string instanceName)
		{
			return new TranscriptionCountersInstance(instanceName);
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x0006BA01 File Offset: 0x00069C01
		public static void GetPerfCounterInfo(XElement element)
		{
			if (TranscriptionCounters.counters == null)
			{
				return;
			}
			TranscriptionCounters.counters.GetPerfCounterDiagnosticsInfo(element);
		}

		// Token: 0x04000E6C RID: 3692
		public const string CategoryName = "MSExchangeUMVoiceMailSpeechRecognition";

		// Token: 0x04000E6D RID: 3693
		private static readonly PerformanceCounterMultipleInstance counters = new PerformanceCounterMultipleInstance("MSExchangeUMVoiceMailSpeechRecognition", new CreateInstanceDelegate(TranscriptionCounters.CreateInstance));
	}
}

using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000307 RID: 775
	internal static class CallAnswerCounters
	{
		// Token: 0x06001784 RID: 6020 RVA: 0x00064F88 File Offset: 0x00063188
		public static void GetPerfCounterInfo(XElement element)
		{
			if (CallAnswerCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in CallAnswerCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04000E02 RID: 3586
		public const string CategoryName = "MSExchangeUMCallAnswer";

		// Token: 0x04000E03 RID: 3587
		public static readonly ExPerformanceCounter CallAnsweringCalls = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Call Answering Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E04 RID: 3588
		public static readonly ExPerformanceCounter CallAnsweringVoiceMessages = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Call Answering Voice Messages", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E05 RID: 3589
		public static readonly ExPerformanceCounter CallAnsweringProtectedVoiceMessages = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Call Answering Protected Voice Messages", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E06 RID: 3590
		public static readonly ExPerformanceCounter CallAnsweringVoiceMessageProtectionFailures = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Call Answering Voice Message Protection Failures", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E07 RID: 3591
		public static readonly ExPerformanceCounter CallAnsweringMissedCalls = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Call Answering Missed Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E08 RID: 3592
		public static readonly ExPerformanceCounter CallAnsweringEscapes = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Call Answering Escapes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E09 RID: 3593
		public static readonly ExPerformanceCounter AverageVoiceMessageSize = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Average Voice Message Size", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E0A RID: 3594
		public static readonly ExPerformanceCounter AverageRecentVoiceMessageSize = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Average Recent Voice Message Size", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E0B RID: 3595
		public static readonly ExPerformanceCounter AverageGreetingSize = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Average Greeting Size", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E0C RID: 3596
		public static readonly ExPerformanceCounter CallsWithoutPersonalGreetings = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Calls Without Personal Greetings", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E0D RID: 3597
		public static readonly ExPerformanceCounter FetchGreetingTimedOut = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Fetch Greeting Timed Out", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E0E RID: 3598
		public static readonly ExPerformanceCounter CallsDisconnectedByCallersDuringUMAudioHourglass = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Calls Disconnected by Callers During UM Audio Hourglass", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E0F RID: 3599
		public static readonly ExPerformanceCounter DivertedExtensionNotProvisioned = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Diverted Extension Not Provisioned", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E10 RID: 3600
		public static readonly ExPerformanceCounter CallsDisconnectedOnIrrecoverableExternalError = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Calls Disconnected by UM on Irrecoverable External Error", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E11 RID: 3601
		public static readonly ExPerformanceCounter CallsMissedBecausePipelineNotHealthy = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Pipeline Not Healthy", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E12 RID: 3602
		public static readonly ExPerformanceCounter CallsForSubscribersHavingOneOrMoreCARConfigured = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Total Calls to Subscribers with One or More Call Answering Rules Configured", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E13 RID: 3603
		public static readonly ExPerformanceCounter CARTimedOutEvaluations = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Total Number of Timed-out Call Answering Rule Evaluations", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E14 RID: 3604
		public static readonly ExPerformanceCounter CAREvaluationAverageTime = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Average Time Taken for Call Answering Rule Evaluations", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E15 RID: 3605
		public static readonly ExPerformanceCounter TotalCARCalls = new ExPerformanceCounter("MSExchangeUMCallAnswer", "Total Number of Call Answering Rules Calls", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000E16 RID: 3606
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			CallAnswerCounters.CallAnsweringCalls,
			CallAnswerCounters.CallAnsweringVoiceMessages,
			CallAnswerCounters.CallAnsweringProtectedVoiceMessages,
			CallAnswerCounters.CallAnsweringVoiceMessageProtectionFailures,
			CallAnswerCounters.CallAnsweringMissedCalls,
			CallAnswerCounters.CallAnsweringEscapes,
			CallAnswerCounters.AverageVoiceMessageSize,
			CallAnswerCounters.AverageRecentVoiceMessageSize,
			CallAnswerCounters.AverageGreetingSize,
			CallAnswerCounters.CallsWithoutPersonalGreetings,
			CallAnswerCounters.FetchGreetingTimedOut,
			CallAnswerCounters.CallsDisconnectedByCallersDuringUMAudioHourglass,
			CallAnswerCounters.DivertedExtensionNotProvisioned,
			CallAnswerCounters.CallsDisconnectedOnIrrecoverableExternalError,
			CallAnswerCounters.CallsMissedBecausePipelineNotHealthy,
			CallAnswerCounters.CallsForSubscribersHavingOneOrMoreCARConfigured,
			CallAnswerCounters.CARTimedOutEvaluations,
			CallAnswerCounters.CAREvaluationAverageTime,
			CallAnswerCounters.TotalCARCalls
		};
	}
}

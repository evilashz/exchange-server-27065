using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.Submission
{
	// Token: 0x0200000E RID: 14
	internal static class MSExchangeSubmission
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00004B78 File Offset: 0x00002D78
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeSubmission.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MSExchangeSubmission.AllCounters)
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

		// Token: 0x04000065 RID: 101
		public const string CategoryName = "MSExchange Submission";

		// Token: 0x04000066 RID: 102
		private static readonly ExPerformanceCounter SuccessfulSubmissionsPerSecond = new ExPerformanceCounter("MSExchange Submission", "Successful Submissions Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000067 RID: 103
		public static readonly ExPerformanceCounter SuccessfulSubmissions = new ExPerformanceCounter("MSExchange Submission", "Successful Submissions", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeSubmission.SuccessfulSubmissionsPerSecond
		});

		// Token: 0x04000068 RID: 104
		public static readonly ExPerformanceCounter SuccessfulPoisonNdrSubmissions = new ExPerformanceCounter("MSExchange Submission", "Successful Poison NDR Submissions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000069 RID: 105
		private static readonly ExPerformanceCounter FailedSubmissionsPerSecond = new ExPerformanceCounter("MSExchange Submission", "Failed Submissions Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400006A RID: 106
		public static readonly ExPerformanceCounter FailedSubmissions = new ExPerformanceCounter("MSExchange Submission", "Failed Submissions", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeSubmission.FailedSubmissionsPerSecond
		});

		// Token: 0x0400006B RID: 107
		private static readonly ExPerformanceCounter NonActionableFailedSubmissionsPerSecond = new ExPerformanceCounter("MSExchange Submission", "Non Actionable Failed Submissions Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400006C RID: 108
		public static readonly ExPerformanceCounter NonActionableFailedSubmissions = new ExPerformanceCounter("MSExchange Submission", "Non Actionable Failed Submissions", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeSubmission.NonActionableFailedSubmissionsPerSecond
		});

		// Token: 0x0400006D RID: 109
		public static readonly ExPerformanceCounter PermanentFailedPoisonNdrSubmissions = new ExPerformanceCounter("MSExchange Submission", "Permanent Failures during Poison NDR Submissions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400006E RID: 110
		public static readonly ExPerformanceCounter PercentFailedSubmissions = new ExPerformanceCounter("MSExchange Submission", "Percent of Permanent Failed Submissions within the last 5 minutes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400006F RID: 111
		private static readonly ExPerformanceCounter TemporarySubmissionFailuresPerSecond = new ExPerformanceCounter("MSExchange Submission", "Temporary Submission Failures/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000070 RID: 112
		public static readonly ExPerformanceCounter TemporarySubmissionFailures = new ExPerformanceCounter("MSExchange Submission", "Temporary Submission Failures", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeSubmission.TemporarySubmissionFailuresPerSecond
		});

		// Token: 0x04000071 RID: 113
		public static readonly ExPerformanceCounter TemporaryPoisonNdrSubmissionFailures = new ExPerformanceCounter("MSExchange Submission", "Temporary Poison NDR Submission Failures", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000072 RID: 114
		public static readonly ExPerformanceCounter PendingSubmissions = new ExPerformanceCounter("MSExchange Submission", "Pending Submissions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000073 RID: 115
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MSExchangeSubmission.SuccessfulSubmissions,
			MSExchangeSubmission.SuccessfulPoisonNdrSubmissions,
			MSExchangeSubmission.FailedSubmissions,
			MSExchangeSubmission.NonActionableFailedSubmissions,
			MSExchangeSubmission.PermanentFailedPoisonNdrSubmissions,
			MSExchangeSubmission.PercentFailedSubmissions,
			MSExchangeSubmission.TemporarySubmissionFailures,
			MSExchangeSubmission.TemporaryPoisonNdrSubmissionFailures,
			MSExchangeSubmission.PendingSubmissions
		};
	}
}

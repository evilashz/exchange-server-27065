using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x0200031B RID: 795
	internal static class MailTipsPerfCounters
	{
		// Token: 0x06001768 RID: 5992 RVA: 0x0006D4B0 File Offset: 0x0006B6B0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MailTipsPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MailTipsPerfCounters.AllCounters)
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

		// Token: 0x04000F28 RID: 3880
		public const string CategoryName = "MSExchange MailTips";

		// Token: 0x04000F29 RID: 3881
		public static readonly ExPerformanceCounter MailTipsConfigurationAverageResponseTime = new ExPerformanceCounter("MSExchange MailTips", "GetMailTipsConfiguration Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F2A RID: 3882
		public static readonly ExPerformanceCounter ServiceConfigurationAverageResponseTime = new ExPerformanceCounter("MSExchange MailTips", "GetServiceConfiguration average response time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F2B RID: 3883
		public static readonly ExPerformanceCounter MailTipsAverageResponseTime = new ExPerformanceCounter("MSExchange MailTips", "GetMailTips Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F2C RID: 3884
		public static readonly ExPerformanceCounter MailTipsInForest99thPercentileResponseTime = new ExPerformanceCounter("MSExchange MailTips", "In-Forest GetMailTips Response Time, 99th Percentile", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F2D RID: 3885
		public static readonly ExPerformanceCounter MailTipsInForest95thPercentileResponseTime = new ExPerformanceCounter("MSExchange MailTips", "In-Forest GetMailTips Response Time, 95th Percentile", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F2E RID: 3886
		public static readonly ExPerformanceCounter MailTipsInForest90thPercentileResponseTime = new ExPerformanceCounter("MSExchange MailTips", "In-Forest GetMailTips Response Time, 90th Percentile", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F2F RID: 3887
		public static readonly ExPerformanceCounter MailTipsInForest80thPercentileResponseTime = new ExPerformanceCounter("MSExchange MailTips", "In-Forest GetMailTips Response Time, 80th Percentile", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F30 RID: 3888
		public static readonly ExPerformanceCounter MailTipsInForest50thPercentileResponseTime = new ExPerformanceCounter("MSExchange MailTips", "In-Forest GetMailTips Response Time, 50th Percentile", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F31 RID: 3889
		public static readonly ExPerformanceCounter MailTipsCrossForest99thPercentileResponseTime = new ExPerformanceCounter("MSExchange MailTips", "Cross-Forest GetMailTips Response Time, 99th Percentile", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F32 RID: 3890
		public static readonly ExPerformanceCounter MailTipsCrossForest95thPercentileResponseTime = new ExPerformanceCounter("MSExchange MailTips", "Cross-Forest GetMailTips Response Time, 95th Percentile", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F33 RID: 3891
		public static readonly ExPerformanceCounter MailTipsCrossForest90thPercentileResponseTime = new ExPerformanceCounter("MSExchange MailTips", "Cross-Forest GetMailTips Response Time, 90th Percentile", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F34 RID: 3892
		public static readonly ExPerformanceCounter MailTipsCrossForest80thPercentileResponseTime = new ExPerformanceCounter("MSExchange MailTips", "Cross-Forest GetMailTips Response Time, 80th Percentile", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F35 RID: 3893
		public static readonly ExPerformanceCounter MailTipsCrossForest50thPercentileResponseTime = new ExPerformanceCounter("MSExchange MailTips", "Cross-Forest GetMailTips Response Time, 50th Percentile", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F36 RID: 3894
		public static readonly ExPerformanceCounter MailTipsAverageRecipientNumber = new ExPerformanceCounter("MSExchange MailTips", "GetMailTips Average Recipient Number", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F37 RID: 3895
		public static readonly ExPerformanceCounter MailTipsAverageActiveDirectoryResponseTime = new ExPerformanceCounter("MSExchange MailTips", "Average Active Directory Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F38 RID: 3896
		public static readonly ExPerformanceCounter MailboxFullAnsweredWithinOneSecond = new ExPerformanceCounter("MSExchange MailTips", "MailboxFull Answered Within One Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F39 RID: 3897
		public static readonly ExPerformanceCounter MailboxFullAnsweredWithinThreeSeconds = new ExPerformanceCounter("MSExchange MailTips", "MailboxFull Answered Within Three Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F3A RID: 3898
		private static readonly ExPerformanceCounter MailboxFullAnsweredWithinThreeSeconds_Base = new ExPerformanceCounter("MSExchange MailTips", "Base Counter for MailboxFull Answered Within Three Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F3B RID: 3899
		public static readonly ExPerformanceCounter MailboxFullAnsweredWithinTenSeconds = new ExPerformanceCounter("MSExchange MailTips", "MailboxFull Answered Within Ten Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F3C RID: 3900
		private static readonly ExPerformanceCounter MailboxFullAnsweredWithinTenSeconds_Base = new ExPerformanceCounter("MSExchange MailTips", "Base Counter for MailboxFull Answered Within Ten Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F3D RID: 3901
		public static readonly ExPerformanceCounter MailboxFullAnsweredWithinOneSecond_Base = new ExPerformanceCounter("MSExchange MailTips", "Base Counter for MailboxFull Answered Within One Second", string.Empty, null, new ExPerformanceCounter[]
		{
			MailTipsPerfCounters.MailboxFullAnsweredWithinThreeSeconds_Base,
			MailTipsPerfCounters.MailboxFullAnsweredWithinTenSeconds_Base
		});

		// Token: 0x04000F3E RID: 3902
		public static readonly ExPerformanceCounter MailboxFullPositiveResponses = new ExPerformanceCounter("MSExchange MailTips", "MailboxFull with Positive Responses", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F3F RID: 3903
		public static readonly ExPerformanceCounter OutOfOfficeAnsweredWithinOneSecond = new ExPerformanceCounter("MSExchange MailTips", "Automatic Replies (1 sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F40 RID: 3904
		public static readonly ExPerformanceCounter OutOfOfficeAnsweredWithinThreeSeconds = new ExPerformanceCounter("MSExchange MailTips", "Automatic Replies (3 sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F41 RID: 3905
		private static readonly ExPerformanceCounter OutOfOfficeAnsweredWithinThreeSeconds_Base = new ExPerformanceCounter("MSExchange MailTips", "Base counter for Automatic Replies (3 sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F42 RID: 3906
		public static readonly ExPerformanceCounter OutOfOfficeAnsweredWithinTenSeconds = new ExPerformanceCounter("MSExchange MailTips", "Automatic Replies (10 sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F43 RID: 3907
		private static readonly ExPerformanceCounter OutOfOfficeAnsweredWithinTenSeconds_Base = new ExPerformanceCounter("MSExchange MailTips", "Base counter for Automatic Replies (10 sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F44 RID: 3908
		public static readonly ExPerformanceCounter OutOfOfficeAnsweredWithinOneSecond_Base = new ExPerformanceCounter("MSExchange MailTips", "Base counter for Automatic Replies (1 sec)", string.Empty, null, new ExPerformanceCounter[]
		{
			MailTipsPerfCounters.OutOfOfficeAnsweredWithinThreeSeconds_Base,
			MailTipsPerfCounters.OutOfOfficeAnsweredWithinTenSeconds_Base
		});

		// Token: 0x04000F45 RID: 3909
		public static readonly ExPerformanceCounter OutOfOfficePositiveResponses = new ExPerformanceCounter("MSExchange MailTips", "Automatic Replies Turned On", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F46 RID: 3910
		public static readonly ExPerformanceCounter MailTipsQueriesAnsweredWithinOneSecond = new ExPerformanceCounter("MSExchange MailTips", "MailTips Queries Answered Within One Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F47 RID: 3911
		public static readonly ExPerformanceCounter MailTipsQueriesAnsweredWithinThreeSeconds = new ExPerformanceCounter("MSExchange MailTips", "MailTips Queries Answered Within Three Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F48 RID: 3912
		private static readonly ExPerformanceCounter MailTipsQueriesAnsweredWithinThreeSeconds_Base = new ExPerformanceCounter("MSExchange MailTips", "Base Counter for MailTips Queries Answered Within Three Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F49 RID: 3913
		public static readonly ExPerformanceCounter MailTipsQueriesAnsweredWithinTenSeconds = new ExPerformanceCounter("MSExchange MailTips", "MailTips Queries Answered Within Ten Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F4A RID: 3914
		private static readonly ExPerformanceCounter MailTipsQueriesAnsweredWithinTenSeconds_Base = new ExPerformanceCounter("MSExchange MailTips", "Base Counter for MailTips Queries Answered Within Ten Seconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F4B RID: 3915
		public static readonly ExPerformanceCounter MailTipsQueriesAnsweredWithinOneSecond_Base = new ExPerformanceCounter("MSExchange MailTips", "Base Counter for MailTips Queries Answered Within One Second", string.Empty, null, new ExPerformanceCounter[]
		{
			MailTipsPerfCounters.MailTipsQueriesAnsweredWithinThreeSeconds_Base,
			MailTipsPerfCounters.MailTipsQueriesAnsweredWithinTenSeconds_Base
		});

		// Token: 0x04000F4C RID: 3916
		public static readonly ExPerformanceCounter IntraSiteMailTipsFailuresPerSecond = new ExPerformanceCounter("MSExchange MailTips", "Intra-Site MailTips Failures (sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F4D RID: 3917
		public static readonly ExPerformanceCounter GroupMetricsRequestCountPerSecond = new ExPerformanceCounter("MSExchange MailTips", "Count of GroupMetrics Queries (sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F4E RID: 3918
		public static readonly ExPerformanceCounter MailTipsCurrentRequests = new ExPerformanceCounter("MSExchange MailTips", "MailTips Current Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F4F RID: 3919
		public static readonly ExPerformanceCounter MailTipsAccumulatedRecipients = new ExPerformanceCounter("MSExchange MailTips", "MailTips Accumulated Recipients", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F50 RID: 3920
		public static readonly ExPerformanceCounter MailTipsAccumulatedMailboxSourcedRecipients = new ExPerformanceCounter("MSExchange MailTips", "MailTips Accumulated Mailbox Sourced Recipients", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F51 RID: 3921
		public static readonly ExPerformanceCounter MailTipsAccumulatedExceptionRecipients = new ExPerformanceCounter("MSExchange MailTips", "MailTips Accumulated Exception Recipients", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F52 RID: 3922
		public static readonly ExPerformanceCounter MailTipsAccumulatedRequests = new ExPerformanceCounter("MSExchange MailTips", "MailTips Accumulated Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F53 RID: 3923
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MailTipsPerfCounters.MailTipsConfigurationAverageResponseTime,
			MailTipsPerfCounters.ServiceConfigurationAverageResponseTime,
			MailTipsPerfCounters.MailTipsAverageResponseTime,
			MailTipsPerfCounters.MailTipsInForest99thPercentileResponseTime,
			MailTipsPerfCounters.MailTipsInForest95thPercentileResponseTime,
			MailTipsPerfCounters.MailTipsInForest90thPercentileResponseTime,
			MailTipsPerfCounters.MailTipsInForest80thPercentileResponseTime,
			MailTipsPerfCounters.MailTipsInForest50thPercentileResponseTime,
			MailTipsPerfCounters.MailTipsCrossForest99thPercentileResponseTime,
			MailTipsPerfCounters.MailTipsCrossForest95thPercentileResponseTime,
			MailTipsPerfCounters.MailTipsCrossForest90thPercentileResponseTime,
			MailTipsPerfCounters.MailTipsCrossForest80thPercentileResponseTime,
			MailTipsPerfCounters.MailTipsCrossForest50thPercentileResponseTime,
			MailTipsPerfCounters.MailTipsAverageRecipientNumber,
			MailTipsPerfCounters.MailTipsAverageActiveDirectoryResponseTime,
			MailTipsPerfCounters.MailboxFullAnsweredWithinOneSecond,
			MailTipsPerfCounters.MailboxFullAnsweredWithinOneSecond_Base,
			MailTipsPerfCounters.MailboxFullAnsweredWithinThreeSeconds,
			MailTipsPerfCounters.MailboxFullAnsweredWithinTenSeconds,
			MailTipsPerfCounters.MailboxFullPositiveResponses,
			MailTipsPerfCounters.OutOfOfficeAnsweredWithinOneSecond,
			MailTipsPerfCounters.OutOfOfficeAnsweredWithinOneSecond_Base,
			MailTipsPerfCounters.OutOfOfficeAnsweredWithinThreeSeconds,
			MailTipsPerfCounters.OutOfOfficeAnsweredWithinTenSeconds,
			MailTipsPerfCounters.OutOfOfficePositiveResponses,
			MailTipsPerfCounters.MailTipsQueriesAnsweredWithinOneSecond,
			MailTipsPerfCounters.MailTipsQueriesAnsweredWithinOneSecond_Base,
			MailTipsPerfCounters.MailTipsQueriesAnsweredWithinThreeSeconds,
			MailTipsPerfCounters.MailTipsQueriesAnsweredWithinTenSeconds,
			MailTipsPerfCounters.IntraSiteMailTipsFailuresPerSecond,
			MailTipsPerfCounters.GroupMetricsRequestCountPerSecond,
			MailTipsPerfCounters.MailTipsCurrentRequests,
			MailTipsPerfCounters.MailTipsAccumulatedRecipients,
			MailTipsPerfCounters.MailTipsAccumulatedMailboxSourcedRecipients,
			MailTipsPerfCounters.MailTipsAccumulatedExceptionRecipients,
			MailTipsPerfCounters.MailTipsAccumulatedRequests
		};
	}
}

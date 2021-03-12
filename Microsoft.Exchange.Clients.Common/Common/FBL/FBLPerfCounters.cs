using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Common.FBL
{
	// Token: 0x02000005 RID: 5
	internal static class FBLPerfCounters
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002B64 File Offset: 0x00000D64
		public static void GetPerfCounterInfo(XElement element)
		{
			if (FBLPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in FBLPerfCounters.AllCounters)
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

		// Token: 0x04000128 RID: 296
		public const string CategoryName = "MSExchange FBL";

		// Token: 0x04000129 RID: 297
		public static readonly ExPerformanceCounter NumberOfFblRequestsReceived = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Requests Received", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400012A RID: 298
		public static readonly ExPerformanceCounter NumberOfFblClassificationRequestsReceived = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Classification Requests Received", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400012B RID: 299
		public static readonly ExPerformanceCounter NumberOfFblSubscriptionRequestsReceived = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Subscription Requests Received", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400012C RID: 300
		public static readonly ExPerformanceCounter NumberOfFblOptInRequestsReceived = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Opt-In Requests Received", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400012D RID: 301
		public static readonly ExPerformanceCounter NumberOfFblOptOutRequestsReceived = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Opt-Out Requests Received", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400012E RID: 302
		public static readonly ExPerformanceCounter NumberOfFblRequestsSuccessfullyProcessed = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Requests Successfully Processed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400012F RID: 303
		public static readonly ExPerformanceCounter NumberOfFblClassificationRequestsSuccessfullyProcessed = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Classification Requests Successfully Processed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000130 RID: 304
		public static readonly ExPerformanceCounter NumberOfFblSubscriptionRequestsSuccessfullyProcessed = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Subscription Requests Successfully Processed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000131 RID: 305
		public static readonly ExPerformanceCounter NumberOfFblOptInRequestsSuccessfullyProcessed = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Opt-In Requests Successfully Processed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000132 RID: 306
		public static readonly ExPerformanceCounter NumberOfFblOptOutRequestsSuccessfullyProcessed = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Opt-Out Requests Successfully Processed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000133 RID: 307
		public static readonly ExPerformanceCounter NumberOfFblRequestsFailed = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Requests Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000134 RID: 308
		public static readonly ExPerformanceCounter NumberOfFblClassificationRequestsFailed = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Classification Requests Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000135 RID: 309
		public static readonly ExPerformanceCounter NumberOfFblSubscriptionRequestsFailed = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Subscription Requests Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000136 RID: 310
		public static readonly ExPerformanceCounter NumberOfFblOptInRequestsFailed = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Opt-In Requests Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000137 RID: 311
		public static readonly ExPerformanceCounter NumberOfFblOptOutRequestsFailed = new ExPerformanceCounter("MSExchange FBL", "Number of FBL Opt-Out Requests Failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000138 RID: 312
		public static readonly ExPerformanceCounter NumberOfSuccessfulMSERVReadRequests = new ExPerformanceCounter("MSExchange FBL", "Number of Successful MSERV Read Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000139 RID: 313
		public static readonly ExPerformanceCounter NumberOfFailedMSERVReadRequests = new ExPerformanceCounter("MSExchange FBL", "Number of Failed MSERV Read Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400013A RID: 314
		public static readonly ExPerformanceCounter NumberOfSuccessfulMSERVWriteRequests = new ExPerformanceCounter("MSExchange FBL", "Number of Successful MSERV Write Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400013B RID: 315
		public static readonly ExPerformanceCounter NumberOfFailedMSERVWriteRequests = new ExPerformanceCounter("MSExchange FBL", "Number of Failed MSERV Write Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400013C RID: 316
		public static readonly ExPerformanceCounter NumberOfXMRMessagesSuccessfullySent = new ExPerformanceCounter("MSExchange FBL", "Number of XMR Messages Successfully Sent", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400013D RID: 317
		public static readonly ExPerformanceCounter NumberOfXMRMessagesFailedToSend = new ExPerformanceCounter("MSExchange FBL", "Number of XMR Messages Failed To Send", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400013E RID: 318
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			FBLPerfCounters.NumberOfFblRequestsReceived,
			FBLPerfCounters.NumberOfFblClassificationRequestsReceived,
			FBLPerfCounters.NumberOfFblSubscriptionRequestsReceived,
			FBLPerfCounters.NumberOfFblOptInRequestsReceived,
			FBLPerfCounters.NumberOfFblOptOutRequestsReceived,
			FBLPerfCounters.NumberOfFblRequestsSuccessfullyProcessed,
			FBLPerfCounters.NumberOfFblClassificationRequestsSuccessfullyProcessed,
			FBLPerfCounters.NumberOfFblSubscriptionRequestsSuccessfullyProcessed,
			FBLPerfCounters.NumberOfFblOptInRequestsSuccessfullyProcessed,
			FBLPerfCounters.NumberOfFblOptOutRequestsSuccessfullyProcessed,
			FBLPerfCounters.NumberOfFblRequestsFailed,
			FBLPerfCounters.NumberOfFblClassificationRequestsFailed,
			FBLPerfCounters.NumberOfFblSubscriptionRequestsFailed,
			FBLPerfCounters.NumberOfFblOptInRequestsFailed,
			FBLPerfCounters.NumberOfFblOptOutRequestsFailed,
			FBLPerfCounters.NumberOfSuccessfulMSERVReadRequests,
			FBLPerfCounters.NumberOfFailedMSERVReadRequests,
			FBLPerfCounters.NumberOfSuccessfulMSERVWriteRequests,
			FBLPerfCounters.NumberOfFailedMSERVWriteRequests,
			FBLPerfCounters.NumberOfXMRMessagesSuccessfullySent,
			FBLPerfCounters.NumberOfXMRMessagesFailedToSend
		};
	}
}

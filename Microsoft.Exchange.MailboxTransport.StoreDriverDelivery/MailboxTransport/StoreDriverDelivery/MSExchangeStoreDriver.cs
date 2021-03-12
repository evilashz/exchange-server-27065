using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200004E RID: 78
	internal static class MSExchangeStoreDriver
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0000DF68 File Offset: 0x0000C168
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MSExchangeStoreDriver.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MSExchangeStoreDriver.AllCounters)
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

		// Token: 0x04000181 RID: 385
		public const string CategoryName = "MSExchange Delivery Store Driver";

		// Token: 0x04000182 RID: 386
		private static readonly ExPerformanceCounter LocalDeliveryCallsPerSecond = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: LocalDeliveryCallsPerSecond", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000183 RID: 387
		public static readonly ExPerformanceCounter LocalDeliveryCalls = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: LocalDeliveryCalls", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeStoreDriver.LocalDeliveryCallsPerSecond
		});

		// Token: 0x04000184 RID: 388
		private static readonly ExPerformanceCounter MessageDeliveryAttemptsPerSecond = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: MessageDeliveryAttemptsPerSecond", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000185 RID: 389
		public static readonly ExPerformanceCounter MessageDeliveryAttempts = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: MessageDeliveryAttempts", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeStoreDriver.MessageDeliveryAttemptsPerSecond
		});

		// Token: 0x04000186 RID: 390
		private static readonly ExPerformanceCounter RecipientsDeliveredPerSecond = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: Recipients Delivered Per Second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000187 RID: 391
		public static readonly ExPerformanceCounter RecipientsDelivered = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: Recipients Delivered", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeStoreDriver.RecipientsDeliveredPerSecond
		});

		// Token: 0x04000188 RID: 392
		public static readonly ExPerformanceCounter RecipientLevelPercentFailedDeliveries = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Percent of Permanent Failed Deliveries within the last 5 minutes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000189 RID: 393
		public static readonly ExPerformanceCounter RecipientLevelPercentTemporaryFailedDeliveries = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Percent of Temporary Failed Deliveries within the last 5 minutes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018A RID: 394
		public static readonly ExPerformanceCounter BytesDelivered = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: Bytes Delivered", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018B RID: 395
		public static readonly ExPerformanceCounter CurrentDeliveryThreads = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: Number of Delivering Threads", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018C RID: 396
		public static readonly ExPerformanceCounter DeliveryRetry = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: Retried Recipients", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018D RID: 397
		public static readonly ExPerformanceCounter DeliveryReroute = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: Rerouted Recipients", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018E RID: 398
		public static readonly ExPerformanceCounter DuplicateDelivery = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: Duplicate Deliveries", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400018F RID: 399
		private static readonly ExPerformanceCounter MailboxRulesActiveDirectoryQueriesPerSecond = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Mailbox Rules: Active Directory queries per second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000190 RID: 400
		public static readonly ExPerformanceCounter MailboxRulesActiveDirectoryQueries = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Mailbox Rules: Active Directory queries", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeStoreDriver.MailboxRulesActiveDirectoryQueriesPerSecond
		});

		// Token: 0x04000191 RID: 401
		private static readonly ExPerformanceCounter MailboxRulesMapiOperationsPerSecond = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Mailbox Rules: MAPI operations per second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000192 RID: 402
		public static readonly ExPerformanceCounter MailboxRulesMapiOperations = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Mailbox Rules: MAPI operations", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeStoreDriver.MailboxRulesMapiOperationsPerSecond
		});

		// Token: 0x04000193 RID: 403
		public static readonly ExPerformanceCounter MailboxRulesMilliseconds = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Mailbox Rules: Average milliseconds spent processing rules.", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000194 RID: 404
		public static readonly ExPerformanceCounter MailboxRulesMilliseconds90thPercentile = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Mailbox Rules: 90th percentile of milliseconds spent processing rules.", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000195 RID: 405
		public static readonly ExPerformanceCounter TotalMeetingMessages = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: TotalMeetingMessages", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000196 RID: 406
		public static readonly ExPerformanceCounter TotalMeetingFailures = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Inbound: TotalMeetingFailures", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000197 RID: 407
		public static readonly ExPerformanceCounter PendingDeliveries = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Pending Deliveries", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000198 RID: 408
		public static readonly ExPerformanceCounter DeliveryAttempts = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Delivery attempts per minute over the last 5 minutes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000199 RID: 409
		public static readonly ExPerformanceCounter DeliveryFailures = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Delivery failures per minute over the last 5 minutes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400019A RID: 410
		private static readonly ExPerformanceCounter SuccessfulDeliveriesPerSecond = new ExPerformanceCounter("MSExchange Delivery Store Driver", "SuccessfulDeliveriesPerSecond", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400019B RID: 411
		public static readonly ExPerformanceCounter SuccessfulDeliveries = new ExPerformanceCounter("MSExchange Delivery Store Driver", "SuccessfulDeliveries", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeStoreDriver.SuccessfulDeliveriesPerSecond
		});

		// Token: 0x0400019C RID: 412
		private static readonly ExPerformanceCounter FailedDeliveriesPerSecond = new ExPerformanceCounter("MSExchange Delivery Store Driver", "FailedDeliveriesPerSecond", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400019D RID: 413
		public static readonly ExPerformanceCounter FailedDeliveries = new ExPerformanceCounter("MSExchange Delivery Store Driver", "FailedDeliveries", string.Empty, null, new ExPerformanceCounter[]
		{
			MSExchangeStoreDriver.FailedDeliveriesPerSecond
		});

		// Token: 0x0400019E RID: 414
		public static readonly ExPerformanceCounter TotalUnjournaledItemsDelivered = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Unjournaling: Total number of unjournaled items delivered", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400019F RID: 415
		public static readonly ExPerformanceCounter DeliveryLatencyPerRecipientMilliseconds = new ExPerformanceCounter("MSExchange Delivery Store Driver", "Average delivery latency per recipient in milliseconds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001A0 RID: 416
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MSExchangeStoreDriver.LocalDeliveryCalls,
			MSExchangeStoreDriver.MessageDeliveryAttempts,
			MSExchangeStoreDriver.RecipientsDelivered,
			MSExchangeStoreDriver.RecipientLevelPercentFailedDeliveries,
			MSExchangeStoreDriver.RecipientLevelPercentTemporaryFailedDeliveries,
			MSExchangeStoreDriver.BytesDelivered,
			MSExchangeStoreDriver.CurrentDeliveryThreads,
			MSExchangeStoreDriver.DeliveryRetry,
			MSExchangeStoreDriver.DeliveryReroute,
			MSExchangeStoreDriver.DuplicateDelivery,
			MSExchangeStoreDriver.MailboxRulesActiveDirectoryQueries,
			MSExchangeStoreDriver.MailboxRulesMapiOperations,
			MSExchangeStoreDriver.MailboxRulesMilliseconds,
			MSExchangeStoreDriver.MailboxRulesMilliseconds90thPercentile,
			MSExchangeStoreDriver.TotalMeetingMessages,
			MSExchangeStoreDriver.TotalMeetingFailures,
			MSExchangeStoreDriver.PendingDeliveries,
			MSExchangeStoreDriver.DeliveryAttempts,
			MSExchangeStoreDriver.DeliveryFailures,
			MSExchangeStoreDriver.SuccessfulDeliveries,
			MSExchangeStoreDriver.FailedDeliveries,
			MSExchangeStoreDriver.TotalUnjournaledItemsDelivered,
			MSExchangeStoreDriver.DeliveryLatencyPerRecipientMilliseconds
		};
	}
}

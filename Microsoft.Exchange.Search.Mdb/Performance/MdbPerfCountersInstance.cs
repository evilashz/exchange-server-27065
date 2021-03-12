using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.Performance
{
	// Token: 0x02000040 RID: 64
	internal sealed class MdbPerfCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000200 RID: 512 RVA: 0x0000DA9C File Offset: 0x0000BC9C
		internal MdbPerfCountersInstance(string instanceName, MdbPerfCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Search Indexes")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Notifications: Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.NumberOfNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Processed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfNotifications, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.NumberOfNotifications);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Notifications: Updates Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.NumberOfUpdateNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Updates Processed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfUpdateNotifications, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.NumberOfUpdateNotifications);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Notifications: Creates Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.NumberOfCreateNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Creates Processed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfCreateNotifications, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.NumberOfCreateNotifications);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Notifications: Deletes Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.NumberOfDeleteNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Deletes Processed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfDeleteNotifications, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.NumberOfDeleteNotifications);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Notifications: Moves Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.NumberOfMoveNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Moves Processed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfMoveNotifications, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.NumberOfMoveNotifications);
				this.MessagesFromTransport = new ExPerformanceCounter(base.CategoryName, "Source Transport", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesFromTransport, new ExPerformanceCounter[0]);
				list.Add(this.MessagesFromTransport);
				this.MessagesFromEventBasedAssistants = new ExPerformanceCounter(base.CategoryName, "Source Event Based Assistants", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesFromEventBasedAssistants, new ExPerformanceCounter[0]);
				list.Add(this.MessagesFromEventBasedAssistants);
				this.MessagesFromTimeBasedAssistants = new ExPerformanceCounter(base.CategoryName, "Source Time Based Assistants", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesFromTimeBasedAssistants, new ExPerformanceCounter[0]);
				list.Add(this.MessagesFromTimeBasedAssistants);
				this.MessagesFromMigration = new ExPerformanceCounter(base.CategoryName, "Source Migration", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MessagesFromMigration, new ExPerformanceCounter[0]);
				list.Add(this.MessagesFromMigration);
				this.NumberOfItemsInNotificationQueue = new ExPerformanceCounter(base.CategoryName, "Notifications: Queue Length", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfItemsInNotificationQueue, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfItemsInNotificationQueue);
				this.NumberOfBackloggedItemsAddedToRetryTable = new ExPerformanceCounter(base.CategoryName, "Notifications: Delayed Items", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfBackloggedItemsAddedToRetryTable, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfBackloggedItemsAddedToRetryTable);
				this.AgeOfLastNotificationProcessed = new ExPerformanceCounter(base.CategoryName, "Notifications: Age of Last Notification Processed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AgeOfLastNotificationProcessed, new ExPerformanceCounter[0]);
				list.Add(this.AgeOfLastNotificationProcessed);
				this.NumberOfNotificationsNotYetProcessed = new ExPerformanceCounter(base.CategoryName, "Notifications: Awaiting Processing", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfNotificationsNotYetProcessed, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfNotificationsNotYetProcessed);
				this.NumberOfDocumentsSentForProcessingNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Items Sent for Processing", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfDocumentsSentForProcessingNotifications, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsSentForProcessingNotifications);
				this.NumberOfDocumentsIndexedNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Items Processed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfDocumentsIndexedNotifications, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsIndexedNotifications);
				this.LastSuccessfulPollTimestamp = new ExPerformanceCounter(base.CategoryName, "Notifications: Last Successful Poll Timestamp", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LastSuccessfulPollTimestamp, new ExPerformanceCounter[0]);
				list.Add(this.LastSuccessfulPollTimestamp);
				this.NotificationsStallTime = new ExPerformanceCounter(base.CategoryName, "Notifications: Stall Time", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NotificationsStallTime, new ExPerformanceCounter[0]);
				list.Add(this.NotificationsStallTime);
				this.MailboxesLeftToCrawl = new ExPerformanceCounter(base.CategoryName, "Crawler: Mailboxes Remaining", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MailboxesLeftToCrawl, new ExPerformanceCounter[0]);
				list.Add(this.MailboxesLeftToCrawl);
				this.MailboxesLeftToRecrawl = new ExPerformanceCounter(base.CategoryName, "Crawler: Mailboxes Remaining to Recrawl", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MailboxesLeftToRecrawl, new ExPerformanceCounter[0]);
				list.Add(this.MailboxesLeftToRecrawl);
				this.NumberOfDocumentsSentForProcessingCrawler = new ExPerformanceCounter(base.CategoryName, "Crawler: Items Sent for Processing", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfDocumentsSentForProcessingCrawler, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsSentForProcessingCrawler);
				this.NumberOfDocumentsIndexedCrawler = new ExPerformanceCounter(base.CategoryName, "Crawler: Items Processed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfDocumentsIndexedCrawler, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsIndexedCrawler);
				this.AverageAttemptedCrawlerRate = new ExPerformanceCounter(base.CategoryName, "Crawler: Average Rate of Attempted Items Submission", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageAttemptedCrawlerRate, new ExPerformanceCounter[0]);
				list.Add(this.AverageAttemptedCrawlerRate);
				this.AverageCrawlerRate = new ExPerformanceCounter(base.CategoryName, "Crawler: Average Rate of Items Submission", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageCrawlerRate, new ExPerformanceCounter[0]);
				list.Add(this.AverageCrawlerRate);
				this.FailedItemsCount = new ExPerformanceCounter(base.CategoryName, "Failed Items", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FailedItemsCount, new ExPerformanceCounter[0]);
				list.Add(this.FailedItemsCount);
				this.RetriableItemsCount = new ExPerformanceCounter(base.CategoryName, "Retry: Retriable Items", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RetriableItemsCount, new ExPerformanceCounter[0]);
				list.Add(this.RetriableItemsCount);
				this.NumberOfDocumentsSentForProcessingRetry = new ExPerformanceCounter(base.CategoryName, "Retry: Items Sent for Processing", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfDocumentsSentForProcessingRetry, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsSentForProcessingRetry);
				this.NumberOfDocumentsIndexedRetry = new ExPerformanceCounter(base.CategoryName, "Retry: Items Processed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfDocumentsIndexedRetry, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsIndexedRetry);
				this.SubmissionDelaysRetry = new ExPerformanceCounter(base.CategoryName, "Retry: Submission Delays", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SubmissionDelaysRetry, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionDelaysRetry);
				this.DelayTimeRetry = new ExPerformanceCounter(base.CategoryName, "Retry: Submission Delay Time", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DelayTimeRetry, new ExPerformanceCounter[0]);
				list.Add(this.DelayTimeRetry);
				this.MailboxesLeftToDelete = new ExPerformanceCounter(base.CategoryName, "Retry: Deleted Mailboxes Remaining", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MailboxesLeftToDelete, new ExPerformanceCounter[0]);
				list.Add(this.MailboxesLeftToDelete);
				this.NumberOfDocumentsSentForDeletion = new ExPerformanceCounter(base.CategoryName, "Retry: Items Sent for Deletion", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfDocumentsSentForDeletion, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsSentForDeletion);
				this.NumberOfDocumentsDeleted = new ExPerformanceCounter(base.CategoryName, "Retry: Items Deleted", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfDocumentsDeleted, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsDeleted);
				this.TotalDocumentsFirstRetryAttempt = new ExPerformanceCounter(base.CategoryName, "Retry: Documents With One Retry Attempt", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalDocumentsFirstRetryAttempt, new ExPerformanceCounter[0]);
				list.Add(this.TotalDocumentsFirstRetryAttempt);
				this.TotalDocumentsMutlipleRetryAttempts = new ExPerformanceCounter(base.CategoryName, "Retry: Documents With Multiple Retries", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalDocumentsMutlipleRetryAttempts, new ExPerformanceCounter[0]);
				list.Add(this.TotalDocumentsMutlipleRetryAttempts);
				this.IndexingStatus = new ExPerformanceCounter(base.CategoryName, "Indexing Status", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.IndexingStatus, new ExPerformanceCounter[0]);
				list.Add(this.IndexingStatus);
				this.FeedingSessions = new ExPerformanceCounter(base.CategoryName, "Feeding Sessions", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FeedingSessions, new ExPerformanceCounter[0]);
				list.Add(this.FeedingSessions);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "Items Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.NumberOfDocumentsProcessed = new ExPerformanceCounter(base.CategoryName, "Items Processed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfDocumentsProcessed, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.NumberOfDocumentsProcessed);
				this.NumberOfItems = new ExPerformanceCounter(base.CategoryName, "Number Of Items", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfItems, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfItems);
				this.InstantSearchEnabled = new ExPerformanceCounter(base.CategoryName, "Instant Search Enabled", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.InstantSearchEnabled, new ExPerformanceCounter[0]);
				list.Add(this.InstantSearchEnabled);
				this.CatalogSuspended = new ExPerformanceCounter(base.CategoryName, "Catalog Suspended", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CatalogSuspended, new ExPerformanceCounter[0]);
				list.Add(this.CatalogSuspended);
				long num = this.NumberOfNotifications.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter7 in list)
					{
						exPerformanceCounter7.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		internal MdbPerfCountersInstance(string instanceName) : base(instanceName, "MSExchange Search Indexes")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Notifications: Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.NumberOfNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Processed", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.NumberOfNotifications);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Notifications: Updates Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.NumberOfUpdateNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Updates Processed", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.NumberOfUpdateNotifications);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Notifications: Creates Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.NumberOfCreateNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Creates Processed", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.NumberOfCreateNotifications);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Notifications: Deletes Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.NumberOfDeleteNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Deletes Processed", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.NumberOfDeleteNotifications);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "Notifications: Moves Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.NumberOfMoveNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Moves Processed", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.NumberOfMoveNotifications);
				this.MessagesFromTransport = new ExPerformanceCounter(base.CategoryName, "Source Transport", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesFromTransport);
				this.MessagesFromEventBasedAssistants = new ExPerformanceCounter(base.CategoryName, "Source Event Based Assistants", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesFromEventBasedAssistants);
				this.MessagesFromTimeBasedAssistants = new ExPerformanceCounter(base.CategoryName, "Source Time Based Assistants", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesFromTimeBasedAssistants);
				this.MessagesFromMigration = new ExPerformanceCounter(base.CategoryName, "Source Migration", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MessagesFromMigration);
				this.NumberOfItemsInNotificationQueue = new ExPerformanceCounter(base.CategoryName, "Notifications: Queue Length", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfItemsInNotificationQueue);
				this.NumberOfBackloggedItemsAddedToRetryTable = new ExPerformanceCounter(base.CategoryName, "Notifications: Delayed Items", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfBackloggedItemsAddedToRetryTable);
				this.AgeOfLastNotificationProcessed = new ExPerformanceCounter(base.CategoryName, "Notifications: Age of Last Notification Processed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AgeOfLastNotificationProcessed);
				this.NumberOfNotificationsNotYetProcessed = new ExPerformanceCounter(base.CategoryName, "Notifications: Awaiting Processing", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfNotificationsNotYetProcessed);
				this.NumberOfDocumentsSentForProcessingNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Items Sent for Processing", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsSentForProcessingNotifications);
				this.NumberOfDocumentsIndexedNotifications = new ExPerformanceCounter(base.CategoryName, "Notifications: Items Processed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsIndexedNotifications);
				this.LastSuccessfulPollTimestamp = new ExPerformanceCounter(base.CategoryName, "Notifications: Last Successful Poll Timestamp", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.LastSuccessfulPollTimestamp);
				this.NotificationsStallTime = new ExPerformanceCounter(base.CategoryName, "Notifications: Stall Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NotificationsStallTime);
				this.MailboxesLeftToCrawl = new ExPerformanceCounter(base.CategoryName, "Crawler: Mailboxes Remaining", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxesLeftToCrawl);
				this.MailboxesLeftToRecrawl = new ExPerformanceCounter(base.CategoryName, "Crawler: Mailboxes Remaining to Recrawl", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxesLeftToRecrawl);
				this.NumberOfDocumentsSentForProcessingCrawler = new ExPerformanceCounter(base.CategoryName, "Crawler: Items Sent for Processing", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsSentForProcessingCrawler);
				this.NumberOfDocumentsIndexedCrawler = new ExPerformanceCounter(base.CategoryName, "Crawler: Items Processed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsIndexedCrawler);
				this.AverageAttemptedCrawlerRate = new ExPerformanceCounter(base.CategoryName, "Crawler: Average Rate of Attempted Items Submission", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAttemptedCrawlerRate);
				this.AverageCrawlerRate = new ExPerformanceCounter(base.CategoryName, "Crawler: Average Rate of Items Submission", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageCrawlerRate);
				this.FailedItemsCount = new ExPerformanceCounter(base.CategoryName, "Failed Items", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedItemsCount);
				this.RetriableItemsCount = new ExPerformanceCounter(base.CategoryName, "Retry: Retriable Items", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RetriableItemsCount);
				this.NumberOfDocumentsSentForProcessingRetry = new ExPerformanceCounter(base.CategoryName, "Retry: Items Sent for Processing", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsSentForProcessingRetry);
				this.NumberOfDocumentsIndexedRetry = new ExPerformanceCounter(base.CategoryName, "Retry: Items Processed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsIndexedRetry);
				this.SubmissionDelaysRetry = new ExPerformanceCounter(base.CategoryName, "Retry: Submission Delays", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SubmissionDelaysRetry);
				this.DelayTimeRetry = new ExPerformanceCounter(base.CategoryName, "Retry: Submission Delay Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.DelayTimeRetry);
				this.MailboxesLeftToDelete = new ExPerformanceCounter(base.CategoryName, "Retry: Deleted Mailboxes Remaining", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxesLeftToDelete);
				this.NumberOfDocumentsSentForDeletion = new ExPerformanceCounter(base.CategoryName, "Retry: Items Sent for Deletion", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsSentForDeletion);
				this.NumberOfDocumentsDeleted = new ExPerformanceCounter(base.CategoryName, "Retry: Items Deleted", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfDocumentsDeleted);
				this.TotalDocumentsFirstRetryAttempt = new ExPerformanceCounter(base.CategoryName, "Retry: Documents With One Retry Attempt", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalDocumentsFirstRetryAttempt);
				this.TotalDocumentsMutlipleRetryAttempts = new ExPerformanceCounter(base.CategoryName, "Retry: Documents With Multiple Retries", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalDocumentsMutlipleRetryAttempts);
				this.IndexingStatus = new ExPerformanceCounter(base.CategoryName, "Indexing Status", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.IndexingStatus);
				this.FeedingSessions = new ExPerformanceCounter(base.CategoryName, "Feeding Sessions", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FeedingSessions);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "Items Processed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.NumberOfDocumentsProcessed = new ExPerformanceCounter(base.CategoryName, "Items Processed", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.NumberOfDocumentsProcessed);
				this.NumberOfItems = new ExPerformanceCounter(base.CategoryName, "Number Of Items", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfItems);
				this.InstantSearchEnabled = new ExPerformanceCounter(base.CategoryName, "Instant Search Enabled", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.InstantSearchEnabled);
				this.CatalogSuspended = new ExPerformanceCounter(base.CategoryName, "Catalog Suspended", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CatalogSuspended);
				long num = this.NumberOfNotifications.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter7 in list)
					{
						exPerformanceCounter7.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000ED54 File Offset: 0x0000CF54
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04000145 RID: 325
		public readonly ExPerformanceCounter NumberOfNotifications;

		// Token: 0x04000146 RID: 326
		public readonly ExPerformanceCounter NumberOfUpdateNotifications;

		// Token: 0x04000147 RID: 327
		public readonly ExPerformanceCounter NumberOfCreateNotifications;

		// Token: 0x04000148 RID: 328
		public readonly ExPerformanceCounter NumberOfDeleteNotifications;

		// Token: 0x04000149 RID: 329
		public readonly ExPerformanceCounter NumberOfMoveNotifications;

		// Token: 0x0400014A RID: 330
		public readonly ExPerformanceCounter MessagesFromTransport;

		// Token: 0x0400014B RID: 331
		public readonly ExPerformanceCounter MessagesFromEventBasedAssistants;

		// Token: 0x0400014C RID: 332
		public readonly ExPerformanceCounter MessagesFromTimeBasedAssistants;

		// Token: 0x0400014D RID: 333
		public readonly ExPerformanceCounter MessagesFromMigration;

		// Token: 0x0400014E RID: 334
		public readonly ExPerformanceCounter NumberOfItemsInNotificationQueue;

		// Token: 0x0400014F RID: 335
		public readonly ExPerformanceCounter NumberOfBackloggedItemsAddedToRetryTable;

		// Token: 0x04000150 RID: 336
		public readonly ExPerformanceCounter AgeOfLastNotificationProcessed;

		// Token: 0x04000151 RID: 337
		public readonly ExPerformanceCounter NumberOfNotificationsNotYetProcessed;

		// Token: 0x04000152 RID: 338
		public readonly ExPerformanceCounter NumberOfDocumentsSentForProcessingNotifications;

		// Token: 0x04000153 RID: 339
		public readonly ExPerformanceCounter NumberOfDocumentsIndexedNotifications;

		// Token: 0x04000154 RID: 340
		public readonly ExPerformanceCounter LastSuccessfulPollTimestamp;

		// Token: 0x04000155 RID: 341
		public readonly ExPerformanceCounter NotificationsStallTime;

		// Token: 0x04000156 RID: 342
		public readonly ExPerformanceCounter MailboxesLeftToCrawl;

		// Token: 0x04000157 RID: 343
		public readonly ExPerformanceCounter MailboxesLeftToRecrawl;

		// Token: 0x04000158 RID: 344
		public readonly ExPerformanceCounter NumberOfDocumentsSentForProcessingCrawler;

		// Token: 0x04000159 RID: 345
		public readonly ExPerformanceCounter NumberOfDocumentsIndexedCrawler;

		// Token: 0x0400015A RID: 346
		public readonly ExPerformanceCounter AverageAttemptedCrawlerRate;

		// Token: 0x0400015B RID: 347
		public readonly ExPerformanceCounter AverageCrawlerRate;

		// Token: 0x0400015C RID: 348
		public readonly ExPerformanceCounter FailedItemsCount;

		// Token: 0x0400015D RID: 349
		public readonly ExPerformanceCounter RetriableItemsCount;

		// Token: 0x0400015E RID: 350
		public readonly ExPerformanceCounter NumberOfDocumentsSentForProcessingRetry;

		// Token: 0x0400015F RID: 351
		public readonly ExPerformanceCounter NumberOfDocumentsIndexedRetry;

		// Token: 0x04000160 RID: 352
		public readonly ExPerformanceCounter SubmissionDelaysRetry;

		// Token: 0x04000161 RID: 353
		public readonly ExPerformanceCounter DelayTimeRetry;

		// Token: 0x04000162 RID: 354
		public readonly ExPerformanceCounter MailboxesLeftToDelete;

		// Token: 0x04000163 RID: 355
		public readonly ExPerformanceCounter NumberOfDocumentsSentForDeletion;

		// Token: 0x04000164 RID: 356
		public readonly ExPerformanceCounter NumberOfDocumentsDeleted;

		// Token: 0x04000165 RID: 357
		public readonly ExPerformanceCounter TotalDocumentsFirstRetryAttempt;

		// Token: 0x04000166 RID: 358
		public readonly ExPerformanceCounter TotalDocumentsMutlipleRetryAttempts;

		// Token: 0x04000167 RID: 359
		public readonly ExPerformanceCounter IndexingStatus;

		// Token: 0x04000168 RID: 360
		public readonly ExPerformanceCounter FeedingSessions;

		// Token: 0x04000169 RID: 361
		public readonly ExPerformanceCounter NumberOfDocumentsProcessed;

		// Token: 0x0400016A RID: 362
		public readonly ExPerformanceCounter NumberOfItems;

		// Token: 0x0400016B RID: 363
		public readonly ExPerformanceCounter InstantSearchEnabled;

		// Token: 0x0400016C RID: 364
		public readonly ExPerformanceCounter CatalogSuspended;
	}
}

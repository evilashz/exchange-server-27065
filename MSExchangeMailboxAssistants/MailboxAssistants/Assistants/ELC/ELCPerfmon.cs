using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x020000B2 RID: 178
	internal static class ELCPerfmon
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x00033F00 File Offset: 0x00032100
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ELCPerfmon.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ELCPerfmon.AllCounters)
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

		// Token: 0x04000544 RID: 1348
		public const string CategoryName = "MSExchange Managed Folder Assistant";

		// Token: 0x04000545 RID: 1349
		public static readonly ExPerformanceCounter TotalItemsMoved = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items Moved", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000546 RID: 1350
		public static readonly ExPerformanceCounter TotalItemsSoftDeleted = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items Deleted but Recoverable", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000547 RID: 1351
		public static readonly ExPerformanceCounter TotalItemsPermanentlyDeleted = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items Permanently Deleted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000548 RID: 1352
		public static readonly ExPerformanceCounter TotalItemsMovedToDiscoveryHolds = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items Moved to Discovery Holds", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000549 RID: 1353
		public static readonly ExPerformanceCounter TotalItemsDeletedDueToEHAExpiryDate = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items deleted due to Eha expiry date", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400054A RID: 1354
		public static readonly ExPerformanceCounter TotalItemsMovedDueToEHAExpiryDate = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items moved due to Eha expiry date", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400054B RID: 1355
		public static readonly ExPerformanceCounter TotalItemsDeletedByEHAHiddenFolderCleanupEnforcer = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items deleted by EHA hidden folder cleanup enforcer", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400054C RID: 1356
		public static readonly ExPerformanceCounter TotalItemsTagged = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items Marked as Past Retention Date", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400054D RID: 1357
		public static readonly ExPerformanceCounter TotalItemsExpired = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items Subject to Retention Policy", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400054E RID: 1358
		public static readonly ExPerformanceCounter TotalItemsAutoCopied = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items Journaled", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400054F RID: 1359
		public static readonly ExPerformanceCounter TotalSkippedDumpsters = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Mailbox Dumpsters Skipped", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000550 RID: 1360
		public static readonly ExPerformanceCounter TotalExpiredDumpsterItems = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Mailbox Dumpsters Expired Items", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000551 RID: 1361
		public static readonly ExPerformanceCounter TotalExpiredSystemDataItems = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "System Data Expired Items", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000552 RID: 1362
		public static readonly ExPerformanceCounter TotalOverQuotaDumpsters = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Total Over Quota Dumpsters", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000553 RID: 1363
		public static readonly ExPerformanceCounter TotalOverQuotaDumpsterItems = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Total Over Quota Dumpster Items", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000554 RID: 1364
		public static readonly ExPerformanceCounter TotalOverQuotaDumpsterItemsDeleted = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Total Over Quota Dumpster Items Deleted", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000555 RID: 1365
		public static readonly ExPerformanceCounter TotalSizeItemsExpired = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Size of Items subject to Retention Policy (In Bytes)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000556 RID: 1366
		public static readonly ExPerformanceCounter TotalSizeItemsSoftDeleted = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Size of Items Deleted but Recoverable (In Bytes)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000557 RID: 1367
		public static readonly ExPerformanceCounter TotalSizeItemsPermanentlyDeleted = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Size of Items Permanently Deleted (In Bytes)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000558 RID: 1368
		public static readonly ExPerformanceCounter TotalSizeItemsMovedToDiscoveryHolds = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Size of Items Moved to Discovery Holds (In Bytes)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000559 RID: 1369
		public static readonly ExPerformanceCounter TotalSizeItemsMoved = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Size of Items Moved due to an Archive policy tag (In Bytes)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400055A RID: 1370
		public static readonly ExPerformanceCounter TotalItemMovedToArchiveForMigration = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items Moved to Archive due to migration", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400055B RID: 1371
		public static readonly ExPerformanceCounter TotalMigratedItemsDeletedDueToItemAgeBased = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Migrated items Hard deleted due to item age based hold", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400055C RID: 1372
		public static readonly ExPerformanceCounter TotalItemsWithPersonalTag = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items stamped with Personal Tag", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400055D RID: 1373
		public static readonly ExPerformanceCounter TotalItemsWithDefaultTag = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items stamped with Default Tag", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400055E RID: 1374
		public static readonly ExPerformanceCounter TotalItemsWithSystemCleanupTag = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items stamped with Cleanup System Tag", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400055F RID: 1375
		public static readonly ExPerformanceCounter TotalItemsExpiredByDefaultExpiryTag = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items expired by a Default Policy Tag", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000560 RID: 1376
		public static readonly ExPerformanceCounter TotalItemsExpiredByPersonalExpiryTag = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Total items expired by a Personal Tag", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000561 RID: 1377
		public static readonly ExPerformanceCounter TotalItemsMovedByDefaultArchiveTag = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Total items moved by a default Archive tag", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000562 RID: 1378
		public static readonly ExPerformanceCounter TotalItemsMovedByPersonalArchiveTag = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Items Moved due to an Archive Tag", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000563 RID: 1379
		public static readonly ExPerformanceCounter TotalMovedDumpsterItems = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Mailbox Dumpsters Moved Items", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000564 RID: 1380
		public static readonly ExPerformanceCounter HealthMonitorAverageDelay = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Health Monitor Average Delay (In Milliseconds)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000565 RID: 1381
		public static readonly ExPerformanceCounter HealthMonitorDelayRate = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Health Monitor Delays/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000566 RID: 1382
		public static readonly ExPerformanceCounter HealthMonitorUnhealthy = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Health Monitor Count of Unhealthy Database Hits", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000567 RID: 1383
		public static readonly ExPerformanceCounter NumberOfMailboxesWhoseProcessingTimeIsAboveFirstLevelThreshold = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Number of Mailboxes Whose Processing Time Is Above First Level Threshold", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000568 RID: 1384
		public static readonly ExPerformanceCounter NumberOfMailboxesWhoseProcessingTimeIsAboveFirstLevelThresholdPerHour = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Number of Mailboxes Whose Processing Time Is Above First Level Threshold/hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000569 RID: 1385
		public static readonly ExPerformanceCounter NumberOfMailboxesWhoseProcessingTimeIsAboveSecondLevelThreshold = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Number of Mailboxes Whose Processing Time Is Above Second Level Threshold", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400056A RID: 1386
		public static readonly ExPerformanceCounter NumberOfMailboxesWhoseProcessingTimeIsAboveSecondLevelThresholdPerHour = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Number of Mailboxes Whose Processing Time Is Above Second Level Threshold/hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400056B RID: 1387
		public static readonly ExPerformanceCounter NumberOfFailures = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Number of Failures", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400056C RID: 1388
		public static readonly ExPerformanceCounter NumberOfFailuresPerHour = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Number of Failures/hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400056D RID: 1389
		public static readonly ExPerformanceCounter NumberOfWarnings = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Number of Warnings", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400056E RID: 1390
		public static readonly ExPerformanceCounter NumberOfWarningsPerHour = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Number of Warnings/hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400056F RID: 1391
		public static readonly ExPerformanceCounter NumberOfMailboxesProcessed = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Number of Mailboxes Processed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000570 RID: 1392
		public static readonly ExPerformanceCounter NumberOfMailboxesProcessedPerHour = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Number of Mailboxes Processed/hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000571 RID: 1393
		public static readonly ExPerformanceCounter NumberOfDiscoveryHoldSearchExceptions = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Number of DiscoveryHoldSearchExceptions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000572 RID: 1394
		public static readonly ExPerformanceCounter NumberOfDiscoveryHoldSearchExceptionsPerHour = new ExPerformanceCounter("MSExchange Managed Folder Assistant", "Number of DiscoveryHoldSearchExceptions/hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000573 RID: 1395
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ELCPerfmon.TotalItemsMoved,
			ELCPerfmon.TotalItemsSoftDeleted,
			ELCPerfmon.TotalItemsPermanentlyDeleted,
			ELCPerfmon.TotalItemsMovedToDiscoveryHolds,
			ELCPerfmon.TotalItemsDeletedDueToEHAExpiryDate,
			ELCPerfmon.TotalItemsMovedDueToEHAExpiryDate,
			ELCPerfmon.TotalItemsDeletedByEHAHiddenFolderCleanupEnforcer,
			ELCPerfmon.TotalItemsTagged,
			ELCPerfmon.TotalItemsExpired,
			ELCPerfmon.TotalItemsAutoCopied,
			ELCPerfmon.TotalSkippedDumpsters,
			ELCPerfmon.TotalExpiredDumpsterItems,
			ELCPerfmon.TotalExpiredSystemDataItems,
			ELCPerfmon.TotalOverQuotaDumpsters,
			ELCPerfmon.TotalOverQuotaDumpsterItems,
			ELCPerfmon.TotalOverQuotaDumpsterItemsDeleted,
			ELCPerfmon.TotalSizeItemsExpired,
			ELCPerfmon.TotalSizeItemsSoftDeleted,
			ELCPerfmon.TotalSizeItemsPermanentlyDeleted,
			ELCPerfmon.TotalSizeItemsMovedToDiscoveryHolds,
			ELCPerfmon.TotalSizeItemsMoved,
			ELCPerfmon.TotalItemMovedToArchiveForMigration,
			ELCPerfmon.TotalMigratedItemsDeletedDueToItemAgeBased,
			ELCPerfmon.TotalItemsWithPersonalTag,
			ELCPerfmon.TotalItemsWithDefaultTag,
			ELCPerfmon.TotalItemsWithSystemCleanupTag,
			ELCPerfmon.TotalItemsExpiredByDefaultExpiryTag,
			ELCPerfmon.TotalItemsExpiredByPersonalExpiryTag,
			ELCPerfmon.TotalItemsMovedByDefaultArchiveTag,
			ELCPerfmon.TotalItemsMovedByPersonalArchiveTag,
			ELCPerfmon.TotalMovedDumpsterItems,
			ELCPerfmon.HealthMonitorAverageDelay,
			ELCPerfmon.HealthMonitorDelayRate,
			ELCPerfmon.HealthMonitorUnhealthy,
			ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveFirstLevelThreshold,
			ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveFirstLevelThresholdPerHour,
			ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveSecondLevelThreshold,
			ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveSecondLevelThresholdPerHour,
			ELCPerfmon.NumberOfFailures,
			ELCPerfmon.NumberOfFailuresPerHour,
			ELCPerfmon.NumberOfWarnings,
			ELCPerfmon.NumberOfWarningsPerHour,
			ELCPerfmon.NumberOfMailboxesProcessed,
			ELCPerfmon.NumberOfMailboxesProcessedPerHour,
			ELCPerfmon.NumberOfDiscoveryHoldSearchExceptions,
			ELCPerfmon.NumberOfDiscoveryHoldSearchExceptionsPerHour
		};
	}
}

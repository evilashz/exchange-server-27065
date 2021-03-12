using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200062A RID: 1578
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDumpsterItemOperations
	{
		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x06004101 RID: 16641
		StoreObjectId RecoverableItemsDeletionsFolderId { get; }

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06004102 RID: 16642
		StoreObjectId RecoverableItemsVersionsFolderId { get; }

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x06004103 RID: 16643
		StoreObjectId RecoverableItemsPurgesFolderId { get; }

		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06004104 RID: 16644
		StoreObjectId RecoverableItemsDiscoveryHoldsFolderId { get; }

		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x06004105 RID: 16645
		StoreObjectId RecoverableItemsRootFolderId { get; }

		// Token: 0x1700134D RID: 4941
		// (get) Token: 0x06004106 RID: 16646
		StoreObjectId CalendarLoggingFolderId { get; }

		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06004107 RID: 16647
		StoreObjectId AuditsFolderId { get; }

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06004108 RID: 16648
		StoreObjectId AdminAuditLogsFolderId { get; }

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06004109 RID: 16649
		COWResults Results { get; }

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x0600410A RID: 16650
		StoreSession StoreSession { get; }

		// Token: 0x0600410B RID: 16651
		bool IsDumpsterFolder(MailboxSession sessionWithBestAccess, StoreObjectId itemId);

		// Token: 0x0600410C RID: 16652
		StoreObjectId CopyItemToDumpster(MailboxSession sessionWithBestAccess, StoreObjectId destinationFolderId, ICoreItem item);

		// Token: 0x0600410D RID: 16653
		void CopyItemsToDumpster(MailboxSession sessionWithBestAccess, StoreObjectId destinationFolderId, StoreObjectId[] itemIds);

		// Token: 0x0600410E RID: 16654
		void CopyItemsToDumpster(MailboxSession sessionWithBestAccess, StoreObjectId destinationFolderId, StoreObjectId[] itemIds, bool forceNonIPM);

		// Token: 0x0600410F RID: 16655
		void MoveItemsToDumpster(MailboxSession sessionWithBestAccess, StoreObjectId destinationFolderId, StoreObjectId[] itemIds);

		// Token: 0x06004110 RID: 16656
		void RollbackItemVersion(MailboxSession sessionWithBestAccess, CoreItem itemUpdated, StoreObjectId itemIdToRollback);

		// Token: 0x06004111 RID: 16657
		bool IsDumpsterOverWarningQuota(COWSettings settings);

		// Token: 0x06004112 RID: 16658
		void DisableCalendarLogging();

		// Token: 0x06004113 RID: 16659
		bool IsDumpsterOverCalendarLoggingQuota(MailboxSession sessionWithBestAccess, COWSettings settings);

		// Token: 0x06004114 RID: 16660
		bool IsAuditFolder(StoreObjectId folderId);
	}
}

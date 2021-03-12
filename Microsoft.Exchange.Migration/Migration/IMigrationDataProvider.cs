using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000BE RID: 190
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationDataProvider : IDisposable
	{
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000A19 RID: 2585
		IMigrationADProvider ADProvider { get; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000A1A RID: 2586
		string TenantName { get; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000A1B RID: 2587
		string MailboxName { get; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000A1C RID: 2588
		Guid MdbGuid { get; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000A1D RID: 2589
		IMigrationStoreObject Folder { get; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000A1E RID: 2590
		ADObjectId OwnerId { get; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000A1F RID: 2591
		OrganizationId OrganizationId { get; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000A20 RID: 2592
		IMigrationRunspaceProxy RunspaceProxy { get; }

		// Token: 0x06000A21 RID: 2593
		IEnumerable<StoreObjectId> FindMessageIds(MigrationEqualityFilter primaryFilter, PropertyDefinition[] filterColumns, SortBy[] additionalSorts, MigrationRowSelector rowSelectorPredicate, int? maxCount);

		// Token: 0x06000A22 RID: 2594
		IEnumerable<StoreObjectId> FindMessageIds(QueryFilter filter, PropertyDefinition[] properties, SortBy[] sortBy, MigrationRowSelector rowSelectorPredicate, int? maxCount);

		// Token: 0x06000A23 RID: 2595
		IMigrationMessageItem FindMessage(StoreObjectId messageId, PropertyDefinition[] properties);

		// Token: 0x06000A24 RID: 2596
		IMigrationStoreObject GetRootFolder(PropertyDefinition[] properties);

		// Token: 0x06000A25 RID: 2597
		int CountMessages(QueryFilter filter, SortBy[] sortBy);

		// Token: 0x06000A26 RID: 2598
		object[] QueryRow(QueryFilter filter, SortBy[] sortBy, PropertyDefinition[] propertyDefinitions);

		// Token: 0x06000A27 RID: 2599
		IEnumerable<object[]> QueryRows(QueryFilter filter, SortBy[] sortBy, PropertyDefinition[] propertyDefinitions, int pageSize);

		// Token: 0x06000A28 RID: 2600
		IMigrationMessageItem CreateMessage();

		// Token: 0x06000A29 RID: 2601
		IMigrationEmailMessageItem CreateEmailMessage();

		// Token: 0x06000A2A RID: 2602
		void RemoveMessage(StoreObjectId messageId);

		// Token: 0x06000A2B RID: 2603
		bool MoveMessageItems(StoreObjectId[] itemsToMove, MigrationFolderName folderName);

		// Token: 0x06000A2C RID: 2604
		IMigrationDataProvider GetProviderForFolder(MigrationFolderName folderName);

		// Token: 0x06000A2D RID: 2605
		Uri GetEcpUrl();

		// Token: 0x06000A2E RID: 2606
		void FlushReport(ReportData reportData);

		// Token: 0x06000A2F RID: 2607
		void LoadReport(ReportData reportData);

		// Token: 0x06000A30 RID: 2608
		void DeleteReport(ReportData reportData);
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000285 RID: 645
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ILogChanges
	{
		// Token: 0x06001AC3 RID: 6851
		bool OnBeforeItemChange(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item);

		// Token: 0x06001AC4 RID: 6852
		bool OnBeforeItemSave(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item);

		// Token: 0x06001AC5 RID: 6853
		void OnAfterItemChange(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, ConflictResolutionResult result);

		// Token: 0x06001AC6 RID: 6854
		void OnAfterItemSave(ItemChangeOperation operation, StoreSession session, StoreId itemId, CoreItem item, ConflictResolutionResult result);

		// Token: 0x06001AC7 RID: 6855
		bool OnBeforeFolderChange(FolderChangeOperation operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds);

		// Token: 0x06001AC8 RID: 6856
		void OnAfterFolderChange(FolderChangeOperation operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, GroupOperationResult result);

		// Token: 0x06001AC9 RID: 6857
		void OnBeforeFolderBind(StoreSession session, StoreObjectId folderId);

		// Token: 0x06001ACA RID: 6858
		void OnAfterFolderBind(StoreSession session, StoreObjectId folderId, CoreFolder folder, bool success);

		// Token: 0x06001ACB RID: 6859
		GroupOperationResult GetCallbackResults();
	}
}

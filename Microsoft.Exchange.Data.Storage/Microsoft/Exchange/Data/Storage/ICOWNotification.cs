using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000616 RID: 1558
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICOWNotification
	{
		// Token: 0x06003FF7 RID: 16375
		bool SkipItemOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, COWTriggerActionState state, StoreSession session, StoreObjectId itemId, CoreItem item, bool onBeforeNotification, bool onDumpster, bool success, CallbackContext callbackContext);

		// Token: 0x06003FF8 RID: 16376
		void ItemOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, COWTriggerActionState state, StoreSession session, StoreObjectId itemId, CoreItem item, CoreFolder folder, bool onBeforeNotification, OperationResult result, CallbackContext callbackContext);

		// Token: 0x06003FF9 RID: 16377
		CowClientOperationSensitivity SkipGroupOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId sourceFolderId, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds, bool onBeforeNotification, bool onDumpster, CallbackContext callbackContext);

		// Token: 0x06003FFA RID: 16378
		void GroupOperation(COWSettings settings, IDumpsterItemOperations dumpster, COWTriggerAction operation, FolderChangeOperationFlags flags, StoreSession sourceSession, StoreSession destinationSession, StoreObjectId destinationFolderId, StoreObjectId[] itemIds, GroupOperationResult result, bool onBeforeNotification, CallbackContext callbackContext);
	}
}

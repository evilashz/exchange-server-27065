using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000348 RID: 840
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IActivitySession
	{
		// Token: 0x0600251D RID: 9501
		void CaptureActivity(ActivityId activityId, StoreObjectId itemId, StoreObjectId previousItemId, IDictionary<string, string> customProperties);

		// Token: 0x0600251E RID: 9502
		void CaptureActivityBeforeItemChange(ItemChangeOperation operation, StoreId objectId, CoreItem item);

		// Token: 0x0600251F RID: 9503
		void CaptureActivityAfterFolderChange(FolderChangeOperation operation, FolderChangeOperationFlags flags, IList<StoreObjectId> itemIdsBeforeChange, IList<StoreObjectId> itemIdsAfterChange, StoreObjectId sourceFolder, StoreObjectId targetFolder);

		// Token: 0x06002520 RID: 9504
		void CaptureMarkAsUnread(ICollection<StoreObjectId> itemIds);

		// Token: 0x06002521 RID: 9505
		void CaptureMarkAsUnread(ICoreItem item);

		// Token: 0x06002522 RID: 9506
		void CaptureMarkAsRead(ICollection<StoreObjectId> itemIds);

		// Token: 0x06002523 RID: 9507
		void CaptureMarkAsRead(ICoreItem item);

		// Token: 0x06002524 RID: 9508
		void CaptureDelivery(StoreObjectId storeObjectId, IDictionary<string, string> deliveryActivityInfo);

		// Token: 0x06002525 RID: 9509
		void CaptureMarkAsClutterOrNotClutter(Dictionary<StoreObjectId, bool> itemClutterActions);

		// Token: 0x06002526 RID: 9510
		void CaptureRemoteSend(StoreObjectId storeObjectId);

		// Token: 0x06002527 RID: 9511
		void CaptureMessageSent(StoreObjectId itemId, string itemSchemaType);

		// Token: 0x06002528 RID: 9512
		void CaptureCalendarEventActivity(ActivityId activityId, StoreObjectId id);

		// Token: 0x06002529 RID: 9513
		void CaptureClutterNotificationSent(StoreObjectId itemId, IDictionary<string, string> messageProperties);

		// Token: 0x0600252A RID: 9514
		void CaptureServerLogonActivity(string result, string exceptionInfo, string userName, string clientIP, string userAgent);
	}
}

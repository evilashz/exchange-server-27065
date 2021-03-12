using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002DE RID: 734
	internal class ExchangeServiceHierarchySubscription : ExchangeServiceSubscription
	{
		// Token: 0x06001460 RID: 5216 RVA: 0x00065964 File Offset: 0x00063B64
		internal ExchangeServiceHierarchySubscription(string subscriptionId) : base(subscriptionId)
		{
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0006596D File Offset: 0x00063B6D
		// (set) Token: 0x06001462 RID: 5218 RVA: 0x00065975 File Offset: 0x00063B75
		internal Guid MailboxGuid { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0006597E File Offset: 0x00063B7E
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x00065986 File Offset: 0x00063B86
		internal Subscription Subscription { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0006598F File Offset: 0x00063B8F
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x00065997 File Offset: 0x00063B97
		internal Action<HierarchyNotification> Callback { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x000659A0 File Offset: 0x00063BA0
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x000659A8 File Offset: 0x00063BA8
		internal QueryResult QueryResult { get; set; }

		// Token: 0x06001469 RID: 5225 RVA: 0x000659B4 File Offset: 0x00063BB4
		internal override void HandleNotification(Notification notification)
		{
			HierarchyNotification hierarchyNotification = null;
			if (notification == null)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceWarning<string>((long)this.GetHashCode(), "ExchangeServiceHierarchySubscription.HandleNotification: Received a null notification for subscriptionId: {0}", base.SubscriptionId);
				return;
			}
			QueryNotification queryNotification = notification as QueryNotification;
			if (queryNotification == null)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceWarning<string>((long)this.GetHashCode(), "ExchangeServiceHierarchySubscription.HandleNotification: Received a notification of an unknown type for subscriptionId: {0}", base.SubscriptionId);
				return;
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ExchangeServiceHierarchySubscription.HandleNotification: Received a {0} notification for subscriptionId: {1}", queryNotification.EventType.ToString(), base.SubscriptionId);
			switch (queryNotification.EventType)
			{
			case QueryNotificationType.RowAdded:
			case QueryNotificationType.RowModified:
				hierarchyNotification = this.ProcessHierarchyNotification(queryNotification, (queryNotification.EventType == QueryNotificationType.RowAdded) ? NotificationTypeType.Create : NotificationTypeType.Update);
				goto IL_10B;
			case QueryNotificationType.RowDeleted:
				hierarchyNotification = new HierarchyNotification
				{
					InstanceKey = queryNotification.Index,
					NotificationType = NotificationTypeType.Delete
				};
				goto IL_10B;
			case QueryNotificationType.Reload:
				hierarchyNotification = new HierarchyNotification
				{
					InstanceKey = queryNotification.Index,
					NotificationType = NotificationTypeType.Reload
				};
				goto IL_10B;
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceHierarchySubscription.HandleNotification: Unknown notification event type");
			IL_10B:
			if (hierarchyNotification != null)
			{
				this.Callback(hierarchyNotification);
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "ExchangeServiceHierarchySubscription.HandleNotification: Returned from callback");
			}
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00065AF1 File Offset: 0x00063CF1
		protected override void InternalDispose(bool isDisposing)
		{
			if (this.Subscription != null)
			{
				this.Subscription.Dispose();
				this.Subscription = null;
			}
			if (this.QueryResult != null)
			{
				this.QueryResult.Dispose();
				this.QueryResult = null;
			}
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00065B27 File Offset: 0x00063D27
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ExchangeServiceHierarchySubscription>(this);
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00065B30 File Offset: 0x00063D30
		private HierarchyNotification ProcessHierarchyNotification(QueryNotification notification, NotificationTypeType notificationType)
		{
			HierarchyNotification hierarchyNotification = new HierarchyNotification();
			hierarchyNotification.InstanceKey = notification.Index;
			VersionedId versionedId = notification.Row[0] as VersionedId;
			StoreObjectId storeObjectId = null;
			if (versionedId != null)
			{
				storeObjectId = versionedId.ObjectId;
			}
			if (storeObjectId == null || notification.Row[2] == null || notification.Row[3] == null || notification.Row[4] == null || notification.Row[6] == null)
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Hierarchy notification has a null folder id or is malformed.");
				return null;
			}
			int num = (int)notification.Row[2];
			int num2 = (int)notification.Row[3];
			if (!(notification.Row[6] is bool) || !(bool)notification.Row[6])
			{
				return null;
			}
			hierarchyNotification.NotificationType = notificationType;
			hierarchyNotification.FolderId = new FolderId(StoreId.StoreIdToEwsId(this.MailboxGuid, storeObjectId), null);
			hierarchyNotification.DisplayName = (string)notification.Row[1];
			hierarchyNotification.ParentFolderId = ((notification.Row[5] != null) ? new FolderId(StoreId.StoreIdToEwsId(this.MailboxGuid, notification.Row[5] as StoreId), null) : null);
			hierarchyNotification.ItemCount = (long)num;
			hierarchyNotification.UnreadCount = (long)num2;
			hierarchyNotification.IsHidden = (notification.Row[7] is bool && (bool)notification.Row[7]);
			if (!(notification.Row[4] is PropertyError))
			{
				hierarchyNotification.FolderClass = (string)notification.Row[4];
				hierarchyNotification.FolderType = ObjectClass.GetObjectType(hierarchyNotification.FolderClass);
			}
			else
			{
				ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Hierarchy notification received PropertyError for Item Class.");
			}
			return hierarchyNotification;
		}
	}
}

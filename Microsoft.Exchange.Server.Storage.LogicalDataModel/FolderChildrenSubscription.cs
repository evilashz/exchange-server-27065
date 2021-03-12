using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200009C RID: 156
	public class FolderChildrenSubscription : NotificationSubscription
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x0004CFF8 File Offset: 0x0004B1F8
		public FolderChildrenSubscription(SubscriptionKind kind, NotificationContext notificationContext, StoreDatabase database, int mailboxNumber, EventType eventTypeMask, NotificationCallback callback, ExchangeId fid) : base(kind, notificationContext, database, mailboxNumber, (int)eventTypeMask, callback)
		{
			this.fid = fid;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0004D011 File Offset: 0x0004B211
		public EventType EventTypeMask
		{
			get
			{
				return (EventType)base.EventTypeValueMask;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0004D019 File Offset: 0x0004B219
		public ExchangeId Fid
		{
			get
			{
				return this.fid;
			}
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0004D024 File Offset: 0x0004B224
		public override bool IsInterested(NotificationEvent nev)
		{
			if ((nev.EventTypeValue & 92012670) != 0)
			{
				ObjectNotificationEvent objectNotificationEvent = nev as ObjectNotificationEvent;
				if (objectNotificationEvent != null)
				{
					if ((objectNotificationEvent.EventType & (EventType.NewMail | EventType.ObjectCreated | EventType.ObjectDeleted | EventType.ObjectModified | EventType.CategRowAdded | EventType.CategRowModified | EventType.CategRowDeleted | EventType.BeginLongOperation | EventType.EndLongOperation | EventType.MessageUnlinked)) != (EventType)0)
					{
						if (objectNotificationEvent.ParentFid == this.fid)
						{
							return true;
						}
					}
					else if ((objectNotificationEvent.EventType & EventType.MessagesLinked) != (EventType)0)
					{
						if (objectNotificationEvent.Fid == this.fid)
						{
							return true;
						}
					}
					else
					{
						if (objectNotificationEvent.ParentFid == this.fid)
						{
							return true;
						}
						ObjectMovedCopiedNotificationEvent objectMovedCopiedNotificationEvent = objectNotificationEvent as ObjectMovedCopiedNotificationEvent;
						if (objectMovedCopiedNotificationEvent != null && objectMovedCopiedNotificationEvent.OldParentFid == this.fid)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0004D0C7 File Offset: 0x0004B2C7
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("FolderChildrenSubscription");
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0004D0D8 File Offset: 0x0004B2D8
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" EventTypeMask:[");
			sb.Append(this.EventTypeMask);
			sb.Append("] Fid:[");
			sb.Append(this.Fid);
			sb.Append("]");
		}

		// Token: 0x0400047C RID: 1148
		private ExchangeId fid;
	}
}

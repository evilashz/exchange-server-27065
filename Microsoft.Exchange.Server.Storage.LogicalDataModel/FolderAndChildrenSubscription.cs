using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200009D RID: 157
	public class FolderAndChildrenSubscription : NotificationSubscription
	{
		// Token: 0x0600091C RID: 2332 RVA: 0x0004D134 File Offset: 0x0004B334
		public FolderAndChildrenSubscription(SubscriptionKind kind, NotificationContext notificationContext, StoreDatabase database, int mailboxNumber, EventType eventTypeMask, NotificationCallback callback, ExchangeId fid) : base(kind, notificationContext, database, mailboxNumber, (int)eventTypeMask, callback)
		{
			this.fid = fid;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0004D14D File Offset: 0x0004B34D
		public EventType EventTypeMask
		{
			get
			{
				return (EventType)base.EventTypeValueMask;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0004D155 File Offset: 0x0004B355
		public ExchangeId Fid
		{
			get
			{
				return this.fid;
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0004D160 File Offset: 0x0004B360
		public override bool IsInterested(NotificationEvent nev)
		{
			if ((nev.EventTypeValue & 24903934) != 0)
			{
				ObjectNotificationEvent objectNotificationEvent = nev as ObjectNotificationEvent;
				if (objectNotificationEvent != null)
				{
					if ((objectNotificationEvent.EventType & (EventType.NewMail | EventType.ObjectCreated | EventType.CategRowAdded | EventType.CategRowModified | EventType.CategRowDeleted | EventType.BeginLongOperation | EventType.EndLongOperation)) != (EventType)0)
					{
						if (objectNotificationEvent.ParentFid == this.fid)
						{
							return true;
						}
					}
					else if ((objectNotificationEvent.EventType & (EventType.ObjectDeleted | EventType.ObjectModified | EventType.MessageUnlinked)) != (EventType)0)
					{
						if ((objectNotificationEvent.IsFolderEvent && objectNotificationEvent.Fid == this.fid) || objectNotificationEvent.ParentFid == this.fid)
						{
							return true;
						}
					}
					else if ((objectNotificationEvent.EventType & (EventType.ObjectMoved | EventType.ObjectCopied)) != (EventType)0)
					{
						if (objectNotificationEvent.ParentFid == this.fid)
						{
							return true;
						}
						ObjectMovedCopiedNotificationEvent objectMovedCopiedNotificationEvent = objectNotificationEvent as ObjectMovedCopiedNotificationEvent;
						if (objectMovedCopiedNotificationEvent != null && ((objectNotificationEvent.IsFolderEvent && objectMovedCopiedNotificationEvent.OldFid == this.fid) || objectMovedCopiedNotificationEvent.OldParentFid == this.fid))
						{
							return true;
						}
					}
					else if (objectNotificationEvent.Fid == this.fid || objectNotificationEvent.ParentFid == this.fid)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0004D275 File Offset: 0x0004B475
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("FolderAndChildrenSubscription");
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0004D284 File Offset: 0x0004B484
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" EventTypeMask:[");
			sb.Append(this.EventTypeMask);
			sb.Append("] Fid:[");
			sb.Append(this.Fid);
			sb.Append("]");
		}

		// Token: 0x0400047D RID: 1149
		private ExchangeId fid;
	}
}

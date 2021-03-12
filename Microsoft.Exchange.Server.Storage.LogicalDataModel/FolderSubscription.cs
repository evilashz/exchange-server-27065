using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200009B RID: 155
	public class FolderSubscription : NotificationSubscription
	{
		// Token: 0x06000910 RID: 2320 RVA: 0x0004CE5E File Offset: 0x0004B05E
		public FolderSubscription(SubscriptionKind kind, NotificationContext notificationContext, StoreDatabase database, int mailboxNumber, EventType eventTypeMask, NotificationCallback callback, ExchangeId fid) : base(kind, notificationContext, database, mailboxNumber, (int)eventTypeMask, callback)
		{
			this.fid = fid;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0004CE77 File Offset: 0x0004B077
		public EventType EventTypeMask
		{
			get
			{
				return (EventType)base.EventTypeValueMask;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0004CE7F File Offset: 0x0004B07F
		public ExchangeId Fid
		{
			get
			{
				return this.fid;
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0004CE88 File Offset: 0x0004B088
		public override bool IsInterested(NotificationEvent nev)
		{
			if ((nev.EventTypeValue & 24903934) != 0)
			{
				ObjectNotificationEvent objectNotificationEvent = nev as ObjectNotificationEvent;
				if (objectNotificationEvent != null)
				{
					if ((objectNotificationEvent.EventType & (EventType.NewMail | EventType.ObjectCreated | EventType.SearchComplete | EventType.CategRowAdded | EventType.CategRowModified | EventType.CategRowDeleted | EventType.BeginLongOperation | EventType.EndLongOperation)) != (EventType)0)
					{
						if (objectNotificationEvent.ParentFid == this.fid)
						{
							return true;
						}
					}
					else if ((objectNotificationEvent.EventType & (EventType.ObjectDeleted | EventType.ObjectModified | EventType.MessageUnlinked)) != (EventType)0)
					{
						if (objectNotificationEvent.IsFolderEvent && objectNotificationEvent.Fid == this.fid)
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

		// Token: 0x06000914 RID: 2324 RVA: 0x0004CF8D File Offset: 0x0004B18D
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("FolderSubscription");
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0004CF9C File Offset: 0x0004B19C
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" EventTypeMask:[");
			sb.Append(this.EventTypeMask);
			sb.Append("] Fid:[");
			sb.Append(this.Fid);
			sb.Append("]");
		}

		// Token: 0x0400047B RID: 1147
		private ExchangeId fid;
	}
}

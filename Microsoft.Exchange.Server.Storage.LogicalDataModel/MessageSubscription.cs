using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200009A RID: 154
	public class MessageSubscription : NotificationSubscription
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x0004CD04 File Offset: 0x0004AF04
		public MessageSubscription(SubscriptionKind kind, NotificationContext notificationContext, StoreDatabase database, int mailboxNumber, EventType eventTypeMask, NotificationCallback callback, ExchangeId fid, ExchangeId mid) : base(kind, notificationContext, database, mailboxNumber, (int)eventTypeMask, callback)
		{
			this.fid = fid;
			this.mid = mid;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0004CD25 File Offset: 0x0004AF25
		public EventType EventTypeMask
		{
			get
			{
				return (EventType)base.EventTypeValueMask;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0004CD2D File Offset: 0x0004AF2D
		public ExchangeId Fid
		{
			get
			{
				return this.fid;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0004CD35 File Offset: 0x0004AF35
		public ExchangeId Mid
		{
			get
			{
				return this.mid;
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0004CD40 File Offset: 0x0004AF40
		public override bool IsInterested(NotificationEvent nev)
		{
			if ((nev.EventTypeValue & 16777336) != 0)
			{
				ObjectNotificationEvent objectNotificationEvent = nev as ObjectNotificationEvent;
				if (objectNotificationEvent != null && objectNotificationEvent.IsMessageEvent)
				{
					if ((objectNotificationEvent.EventType & (EventType.ObjectDeleted | EventType.ObjectModified | EventType.MessageUnlinked)) != (EventType)0)
					{
						if (objectNotificationEvent.Fid == this.fid && objectNotificationEvent.Mid == this.mid)
						{
							return true;
						}
					}
					else
					{
						ObjectMovedCopiedNotificationEvent objectMovedCopiedNotificationEvent = objectNotificationEvent as ObjectMovedCopiedNotificationEvent;
						if (objectMovedCopiedNotificationEvent != null && objectMovedCopiedNotificationEvent.OldFid == this.fid && objectMovedCopiedNotificationEvent.OldMid == this.mid)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0004CDD6 File Offset: 0x0004AFD6
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MessageSubscription");
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0004CDE4 File Offset: 0x0004AFE4
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" EventTypeMask:[");
			sb.Append(this.EventTypeMask);
			sb.Append("] Fid:[");
			sb.Append(this.Fid);
			sb.Append("] Mid:[");
			sb.Append(this.Mid);
			sb.Append("]");
		}

		// Token: 0x04000479 RID: 1145
		private ExchangeId fid;

		// Token: 0x0400047A RID: 1146
		private ExchangeId mid;
	}
}

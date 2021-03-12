using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200009E RID: 158
	public class ConversationMessageSubscription : NotificationSubscription
	{
		// Token: 0x06000922 RID: 2338 RVA: 0x0004D2E0 File Offset: 0x0004B4E0
		public ConversationMessageSubscription(SubscriptionKind kind, NotificationContext notificationContext, StoreDatabase database, int mailboxNumber, EventType eventTypeMask, NotificationCallback callback) : base(kind, notificationContext, database, mailboxNumber, (int)eventTypeMask, callback)
		{
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0004D2F1 File Offset: 0x0004B4F1
		public EventType EventTypeMask
		{
			get
			{
				return (EventType)base.EventTypeValueMask;
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0004D2FC File Offset: 0x0004B4FC
		public override bool IsInterested(NotificationEvent nev)
		{
			bool result = false;
			EventType eventType = EventType.NewMail | EventType.ObjectCreated | EventType.ObjectDeleted | EventType.ObjectModified | EventType.ObjectMoved | EventType.ObjectCopied;
			if ((nev.EventTypeValue & (int)eventType) != 0)
			{
				ObjectNotificationEvent objectNotificationEvent = nev as ObjectNotificationEvent;
				if (objectNotificationEvent != null && objectNotificationEvent.IsMessageEvent && (objectNotificationEvent.EventType & eventType) != (EventType)0 && (objectNotificationEvent.EventFlags & (EventFlags.SearchFolder | EventFlags.Conversation)) == EventFlags.None)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0004D345 File Offset: 0x0004B545
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("ConversationMessageSubscription");
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0004D353 File Offset: 0x0004B553
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" EventTypeMask:[");
			sb.Append(this.EventTypeMask);
			sb.Append("]");
		}
	}
}

using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000099 RID: 153
	public class MailboxFoldersSubscription : NotificationSubscription
	{
		// Token: 0x06000904 RID: 2308 RVA: 0x0004CC7A File Offset: 0x0004AE7A
		public MailboxFoldersSubscription(SubscriptionKind kind, NotificationContext notificationContext, StoreDatabase database, int mailboxNumber, EventType eventTypeMask, NotificationCallback callback) : base(kind, notificationContext, database, mailboxNumber, (int)eventTypeMask, callback)
		{
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0004CC8B File Offset: 0x0004AE8B
		public EventType EventTypeMask
		{
			get
			{
				return (EventType)base.EventTypeValueMask;
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0004CC94 File Offset: 0x0004AE94
		public override bool IsInterested(NotificationEvent nev)
		{
			if ((nev.EventTypeValue & 8126716) != 0)
			{
				ObjectNotificationEvent objectNotificationEvent = nev as ObjectNotificationEvent;
				return objectNotificationEvent != null && objectNotificationEvent.IsFolderEvent;
			}
			return false;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0004CCC3 File Offset: 0x0004AEC3
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MailboxFoldersSubscription");
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0004CCD1 File Offset: 0x0004AED1
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" EventTypeMask:[");
			sb.Append(this.EventTypeMask);
			sb.Append("]");
		}
	}
}

using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000098 RID: 152
	public class MailboxGlobalSubscription : NotificationSubscription
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x0004CC1D File Offset: 0x0004AE1D
		public MailboxGlobalSubscription(SubscriptionKind kind, NotificationContext notificationContext, StoreDatabase database, int mailboxNumber, EventType eventTypeMask, NotificationCallback callback) : base(kind, notificationContext, database, mailboxNumber, (int)eventTypeMask, callback)
		{
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x0004CC2E File Offset: 0x0004AE2E
		public EventType EventTypeMask
		{
			get
			{
				return (EventType)base.EventTypeValueMask;
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0004CC36 File Offset: 0x0004AE36
		public override bool IsInterested(NotificationEvent nev)
		{
			return true;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0004CC39 File Offset: 0x0004AE39
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MailboxGlobalSubscription");
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0004CC47 File Offset: 0x0004AE47
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" EventTypeMask:[");
			sb.Append(this.EventTypeMask);
			sb.Append("]");
		}
	}
}

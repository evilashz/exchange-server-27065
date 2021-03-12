using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200008D RID: 141
	public class MailboxMoveFailedNotificationEvent : MailboxNotificationEvent
	{
		// Token: 0x060008EA RID: 2282 RVA: 0x0004C919 File Offset: 0x0004AB19
		public MailboxMoveFailedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags) : base(database, mailboxNumber, EventType.MailboxMoveFailed, userIdentity, clientType, eventFlags)
		{
			Statistics.NotificationTypes.MailboxMoveFailed.Bump();
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0004C937 File Offset: 0x0004AB37
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MailboxMoveFailedNotificationEvent");
		}
	}
}

using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200008C RID: 140
	public class MailboxMoveSucceededNotificationEvent : MailboxNotificationEvent
	{
		// Token: 0x060008E8 RID: 2280 RVA: 0x0004C8ED File Offset: 0x0004AAED
		public MailboxMoveSucceededNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags) : base(database, mailboxNumber, EventType.MailboxMoveSucceeded, userIdentity, clientType, eventFlags)
		{
			Statistics.NotificationTypes.MailboxMoveSucceeded.Bump();
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0004C90B File Offset: 0x0004AB0B
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MailboxMoveSucceededNotificationEvent");
		}
	}
}

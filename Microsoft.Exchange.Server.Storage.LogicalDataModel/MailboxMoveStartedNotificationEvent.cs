using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200008B RID: 139
	public class MailboxMoveStartedNotificationEvent : MailboxNotificationEvent
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x0004C8C1 File Offset: 0x0004AAC1
		public MailboxMoveStartedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags) : base(database, mailboxNumber, EventType.MailboxMoveStarted, userIdentity, clientType, eventFlags)
		{
			Statistics.NotificationTypes.MailboxMoveStarted.Bump();
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0004C8DF File Offset: 0x0004AADF
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MailboxMoveStartedNotificationEvent");
		}
	}
}

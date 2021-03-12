using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200008A RID: 138
	public class MailboxReconnectedNotificationEvent : MailboxNotificationEvent
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x0004C895 File Offset: 0x0004AA95
		public MailboxReconnectedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags) : base(database, mailboxNumber, EventType.MailboxReconnected, userIdentity, clientType, eventFlags)
		{
			Statistics.NotificationTypes.MailboxReconnected.Bump();
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0004C8B3 File Offset: 0x0004AAB3
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MailboxReconnectedNotificationEvent");
		}
	}
}

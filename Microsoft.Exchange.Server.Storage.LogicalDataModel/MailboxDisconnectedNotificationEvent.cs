using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000089 RID: 137
	public class MailboxDisconnectedNotificationEvent : MailboxNotificationEvent
	{
		// Token: 0x060008E2 RID: 2274 RVA: 0x0004C869 File Offset: 0x0004AA69
		public MailboxDisconnectedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags) : base(database, mailboxNumber, EventType.MailboxDisconnected, userIdentity, clientType, eventFlags)
		{
			Statistics.NotificationTypes.MailboxDisconnected.Bump();
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0004C887 File Offset: 0x0004AA87
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MailboxDisconnectedNotificationEvent");
		}
	}
}

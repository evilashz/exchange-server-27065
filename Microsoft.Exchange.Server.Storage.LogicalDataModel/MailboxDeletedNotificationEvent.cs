using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000088 RID: 136
	public class MailboxDeletedNotificationEvent : MailboxNotificationEvent
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0004C83D File Offset: 0x0004AA3D
		public MailboxDeletedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags) : base(database, mailboxNumber, EventType.MailboxDeleted, userIdentity, clientType, eventFlags)
		{
			Statistics.NotificationTypes.MailboxDeleted.Bump();
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0004C85B File Offset: 0x0004AA5B
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MailboxDeletedNotificationEvent");
		}
	}
}

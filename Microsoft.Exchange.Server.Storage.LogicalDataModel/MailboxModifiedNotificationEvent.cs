using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200008E RID: 142
	public class MailboxModifiedNotificationEvent : MailboxNotificationEvent
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x0004C945 File Offset: 0x0004AB45
		public MailboxModifiedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags) : base(database, mailboxNumber, EventType.MailboxModified, userIdentity, clientType, eventFlags)
		{
			Statistics.NotificationTypes.MailboxModified.Bump();
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0004C963 File Offset: 0x0004AB63
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MailboxModifiedNotificationEvent");
		}
	}
}

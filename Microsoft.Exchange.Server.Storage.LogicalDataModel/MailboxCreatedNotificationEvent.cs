using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000087 RID: 135
	public class MailboxCreatedNotificationEvent : MailboxNotificationEvent
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x0004C811 File Offset: 0x0004AA11
		public MailboxCreatedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags) : base(database, mailboxNumber, EventType.MailboxCreated, userIdentity, clientType, eventFlags)
		{
			Statistics.NotificationTypes.MailboxCreated.Bump();
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0004C82F File Offset: 0x0004AA2F
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MailboxCreatedNotificationEvent");
		}
	}
}

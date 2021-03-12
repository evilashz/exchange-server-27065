using System;
using System.Security.Principal;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000086 RID: 134
	public abstract class MailboxNotificationEvent : LogicalModelNotificationEvent
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x0004C7EC File Offset: 0x0004A9EC
		public MailboxNotificationEvent(StoreDatabase database, int mailboxNumber, EventType eventType, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags) : base(database, mailboxNumber, eventType, userIdentity, clientType, eventFlags, null)
		{
		}
	}
}

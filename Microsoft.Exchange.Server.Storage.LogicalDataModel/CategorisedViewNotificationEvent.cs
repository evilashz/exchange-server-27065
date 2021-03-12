using System;
using System.Security.Principal;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200008F RID: 143
	public abstract class CategorisedViewNotificationEvent : ObjectNotificationEvent
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x0004C974 File Offset: 0x0004AB74
		public CategorisedViewNotificationEvent(StoreDatabase database, int mailboxNumber, EventType eventType, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid) : base(database, mailboxNumber, eventType, userIdentity, clientType, eventFlags, fid, mid, parentFid, null, null, null)
		{
		}
	}
}

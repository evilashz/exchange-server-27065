using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000092 RID: 146
	public class CategorisedRowDeletedNotificationEvent : CategorisedViewNotificationEvent
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x0004CA2C File Offset: 0x0004AC2C
		public CategorisedRowDeletedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid) : base(database, mailboxNumber, EventType.CategRowDeleted, userIdentity, clientType, eventFlags, fid, mid, parentFid)
		{
			Statistics.NotificationTypes.CategorizedRowDeleted.Bump();
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0004CA5B File Offset: 0x0004AC5B
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("CategorisedRowDeletedNotificationEvent");
		}
	}
}

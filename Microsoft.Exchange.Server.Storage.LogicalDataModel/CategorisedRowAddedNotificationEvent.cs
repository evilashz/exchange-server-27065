using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000090 RID: 144
	public class CategorisedRowAddedNotificationEvent : CategorisedViewNotificationEvent
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x0004C9AC File Offset: 0x0004ABAC
		public CategorisedRowAddedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid) : base(database, mailboxNumber, EventType.CategRowAdded, userIdentity, clientType, eventFlags, fid, mid, parentFid)
		{
			Statistics.NotificationTypes.CategorizedRowAdded.Bump();
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0004C9DB File Offset: 0x0004ABDB
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("CategorisedRowAddedNotificationEvent");
		}
	}
}

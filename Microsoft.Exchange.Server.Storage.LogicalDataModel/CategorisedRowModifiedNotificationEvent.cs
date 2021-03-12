using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000091 RID: 145
	public class CategorisedRowModifiedNotificationEvent : CategorisedViewNotificationEvent
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x0004C9EC File Offset: 0x0004ABEC
		public CategorisedRowModifiedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid) : base(database, mailboxNumber, EventType.CategRowModified, userIdentity, clientType, eventFlags, fid, mid, parentFid)
		{
			Statistics.NotificationTypes.CategorizedRowModified.Bump();
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0004CA1B File Offset: 0x0004AC1B
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("CategorisedRowModifiedNotificationEvent");
		}
	}
}

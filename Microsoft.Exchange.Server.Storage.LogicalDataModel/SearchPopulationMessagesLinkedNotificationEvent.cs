using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000095 RID: 149
	public class SearchPopulationMessagesLinkedNotificationEvent : ObjectNotificationEvent
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x0004CB14 File Offset: 0x0004AD14
		public SearchPopulationMessagesLinkedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, ExchangeId fid) : base(database, mailboxNumber, EventType.MessagesLinked, userIdentity, clientType, EventFlags.SearchFolder, fid, ExchangeId.Null, fid, null, null, null)
		{
			Statistics.NotificationTypes.MessagesLinked.Bump();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0004CB5C File Offset: 0x0004AD5C
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("SearchPopulationMessagesLinkedNotificationEvent");
		}
	}
}

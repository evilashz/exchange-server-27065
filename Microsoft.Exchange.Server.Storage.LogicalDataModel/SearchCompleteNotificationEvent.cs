using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000096 RID: 150
	public class SearchCompleteNotificationEvent : ObjectNotificationEvent
	{
		// Token: 0x060008FB RID: 2299 RVA: 0x0004CB6C File Offset: 0x0004AD6C
		public SearchCompleteNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, ExchangeId fid) : base(database, mailboxNumber, EventType.SearchComplete, userIdentity, clientType, EventFlags.SearchFolder, fid, ExchangeId.Null, fid, null, null, null)
		{
			Statistics.NotificationTypes.SearchComplete.Bump();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0004CBB4 File Offset: 0x0004ADB4
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("SearchCompleteNotificationEvent");
		}
	}
}

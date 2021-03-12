using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000094 RID: 148
	public class EndLongOperationNotificationEvent : ObjectNotificationEvent
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x0004CAC0 File Offset: 0x0004ACC0
		public EndLongOperationNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExchangeId fid) : base(database, mailboxNumber, EventType.EndLongOperation, userIdentity, clientType, eventFlags, fid, ExchangeId.Null, fid, null, null, null)
		{
			Statistics.NotificationTypes.EndLongOperation.Bump();
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0004CB05 File Offset: 0x0004AD05
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("EndLongOperationNotificationEvent");
		}
	}
}

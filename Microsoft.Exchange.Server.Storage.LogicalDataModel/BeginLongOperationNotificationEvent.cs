using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000093 RID: 147
	public class BeginLongOperationNotificationEvent : ObjectNotificationEvent
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x0004CA6C File Offset: 0x0004AC6C
		public BeginLongOperationNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExchangeId fid) : base(database, mailboxNumber, EventType.BeginLongOperation, userIdentity, clientType, eventFlags, fid, ExchangeId.Null, fid, null, null, null)
		{
			Statistics.NotificationTypes.BeginLongOperation.Bump();
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0004CAB1 File Offset: 0x0004ACB1
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("BeginLongOperationNotificationEvent");
		}
	}
}

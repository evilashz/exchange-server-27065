using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000097 RID: 151
	public class IcsNotificationEvent : ObjectNotificationEvent
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x0004CBC4 File Offset: 0x0004ADC4
		public IcsNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags) : base(database, mailboxNumber, EventType.StatusObjectModified, userIdentity, clientType, eventFlags, ExchangeId.Null, ExchangeId.Null, ExchangeId.Null, null, null, null)
		{
			Statistics.NotificationTypes.Ics.Bump();
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0004CC0F File Offset: 0x0004AE0F
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("IcsNotificationEvent");
		}
	}
}

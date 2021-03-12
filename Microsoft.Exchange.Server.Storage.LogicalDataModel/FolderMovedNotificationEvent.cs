using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000084 RID: 132
	public class FolderMovedNotificationEvent : ObjectMovedCopiedNotificationEvent
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x0004C71C File Offset: 0x0004A91C
		public FolderMovedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId parentFid, ExchangeId oldFid, ExchangeId oldParentFid, string containerClass) : base(database, mailboxNumber, EventType.ObjectMoved, userIdentity, clientType, eventFlags, extendedEventFlags, fid, ExchangeId.Null, parentFid, null, null, oldFid, ExchangeId.Null, oldParentFid, null, containerClass)
		{
			Statistics.NotificationTypes.FolderMoved.Bump();
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0004C773 File Offset: 0x0004A973
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("FolderMovedNotificationEvent");
		}
	}
}

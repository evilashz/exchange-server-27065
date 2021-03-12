using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000081 RID: 129
	public class FolderCreatedNotificationEvent : ObjectCreatedModifiedNotificationEvent
	{
		// Token: 0x060008CD RID: 2253 RVA: 0x0004C408 File Offset: 0x0004A608
		public FolderCreatedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId parentFid, StorePropTag[] changedPropTags, string containerClass) : base(database, mailboxNumber, EventType.ObjectCreated, userIdentity, clientType, eventFlags, extendedEventFlags, fid, ExchangeId.Null, parentFid, null, null, changedPropTags, containerClass, null)
		{
			Statistics.NotificationTypes.FolderCreated.Bump();
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0004C457 File Offset: 0x0004A657
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("FolderCreatedNotificationEvent");
		}
	}
}

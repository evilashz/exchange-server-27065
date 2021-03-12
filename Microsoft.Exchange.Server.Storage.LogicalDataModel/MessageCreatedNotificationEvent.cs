using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200007A RID: 122
	public class MessageCreatedNotificationEvent : ObjectCreatedModifiedNotificationEvent
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x0004BF84 File Offset: 0x0004A184
		public MessageCreatedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid, int documentId, int? conversationDocumentId, StorePropTag[] changedPropTags, string messageClass, Guid? userIdentityContext) : base(database, mailboxNumber, EventType.ObjectCreated, userIdentity, clientType, eventFlags, extendedEventFlags, fid, mid, parentFid, new int?(documentId), conversationDocumentId, changedPropTags, messageClass, userIdentityContext)
		{
			Statistics.NotificationTypes.MessageCreated.Bump();
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0004BFC0 File Offset: 0x0004A1C0
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MessageCreatedNotificationEvent");
		}
	}
}

using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200007D RID: 125
	public class MessageMovedNotificationEvent : ObjectMovedCopiedNotificationEvent
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x0004C290 File Offset: 0x0004A490
		public MessageMovedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid, int documentId, int? conversationDocumentId, ExchangeId oldFid, ExchangeId oldMid, ExchangeId oldParentFid, int? oldConversationDocumentId, string messageClass) : base(database, mailboxNumber, EventType.ObjectMoved, userIdentity, clientType, eventFlags, extendedEventFlags, fid, mid, parentFid, new int?(documentId), conversationDocumentId, oldFid, oldMid, oldParentFid, oldConversationDocumentId, messageClass)
		{
			Statistics.NotificationTypes.MessageMoved.Bump();
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0004C2D1 File Offset: 0x0004A4D1
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MessageMovedNotificationEvent");
		}
	}
}

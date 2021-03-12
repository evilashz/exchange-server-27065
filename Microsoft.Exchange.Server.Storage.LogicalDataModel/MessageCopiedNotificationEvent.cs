using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200007E RID: 126
	public class MessageCopiedNotificationEvent : ObjectMovedCopiedNotificationEvent
	{
		// Token: 0x060008C4 RID: 2244 RVA: 0x0004C2E0 File Offset: 0x0004A4E0
		public MessageCopiedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid, int documentId, int? conversationDocumentId, ExchangeId oldFid, ExchangeId oldMid, ExchangeId oldParentFid, int? oldConversationDocumentId, string messageClass) : base(database, mailboxNumber, EventType.ObjectCopied, userIdentity, clientType, eventFlags, extendedEventFlags, fid, mid, parentFid, new int?(documentId), conversationDocumentId, oldFid, oldMid, oldParentFid, oldConversationDocumentId, messageClass)
		{
			Statistics.NotificationTypes.MessageCopied.Bump();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0004C321 File Offset: 0x0004A521
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MessageCopiedNotificationEvent");
		}
	}
}

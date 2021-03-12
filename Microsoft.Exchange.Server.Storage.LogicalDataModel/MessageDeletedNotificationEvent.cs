using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200007C RID: 124
	public class MessageDeletedNotificationEvent : ObjectNotificationEvent
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0004C1AD File Offset: 0x0004A3AD
		public byte[] ConversationId
		{
			get
			{
				return this.conversationId;
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0004C1B8 File Offset: 0x0004A3B8
		public MessageDeletedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid, int documentId, int? conversationDocumentId, string messageClass, byte[] conversationId, Guid? userIdentityContext) : base(database, mailboxNumber, EventType.ObjectDeleted, userIdentity, clientType, eventFlags, extendedEventFlags, fid, mid, parentFid, new int?(documentId), conversationDocumentId, messageClass, userIdentityContext)
		{
			Statistics.NotificationTypes.MessageDeleted.Bump();
			this.conversationId = conversationId;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0004C1FC File Offset: 0x0004A3FC
		public override NotificationEvent.RedundancyStatus GetRedundancyStatus(NotificationEvent oldNev)
		{
			ObjectNotificationEvent objectNotificationEvent = oldNev as ObjectNotificationEvent;
			if (objectNotificationEvent != null)
			{
				if (base.IsSameObject(objectNotificationEvent))
				{
					if (objectNotificationEvent.EventType == EventType.ObjectModified)
					{
						return NotificationEvent.RedundancyStatus.DropOldAndStop;
					}
					if (objectNotificationEvent.EventType == EventType.ObjectCreated)
					{
						return NotificationEvent.RedundancyStatus.DropBothAndStop;
					}
					return NotificationEvent.RedundancyStatus.FlagStopSearch;
				}
				else if (objectNotificationEvent.EventType == EventType.ObjectCopied)
				{
					MessageCopiedNotificationEvent messageCopiedNotificationEvent = oldNev as MessageCopiedNotificationEvent;
					if (messageCopiedNotificationEvent != null && base.Mid == messageCopiedNotificationEvent.OldMid && base.Fid == messageCopiedNotificationEvent.OldFid)
					{
						return NotificationEvent.RedundancyStatus.FlagStopSearch;
					}
				}
			}
			return NotificationEvent.RedundancyStatus.Continue;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0004C282 File Offset: 0x0004A482
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MessageDeletedNotificationEvent");
		}

		// Token: 0x04000475 RID: 1141
		private byte[] conversationId;
	}
}

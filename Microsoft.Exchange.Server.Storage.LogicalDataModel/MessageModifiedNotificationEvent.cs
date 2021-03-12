using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200007B RID: 123
	public class MessageModifiedNotificationEvent : ObjectCreatedModifiedNotificationEvent
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x0004BFD0 File Offset: 0x0004A1D0
		public MessageModifiedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid, int documentId, int? conversationDocumentId, int? oldConversationDocumentId, StorePropTag[] changedPropTags, string messageClass, Guid? userIdentityContext) : base(database, mailboxNumber, EventType.ObjectModified, userIdentity, clientType, eventFlags, extendedEventFlags, fid, mid, parentFid, new int?(documentId), conversationDocumentId, changedPropTags, messageClass, userIdentityContext)
		{
			this.oldConversationDocumentId = oldConversationDocumentId;
			Statistics.NotificationTypes.MessageModified.Bump();
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0004C015 File Offset: 0x0004A215
		public int? OldConversationDocumentId
		{
			get
			{
				return this.oldConversationDocumentId;
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0004C020 File Offset: 0x0004A220
		public override NotificationEvent.RedundancyStatus GetRedundancyStatus(NotificationEvent oldNev)
		{
			ObjectNotificationEvent objectNotificationEvent = oldNev as ObjectNotificationEvent;
			if (objectNotificationEvent != null)
			{
				if (base.IsSameObject(objectNotificationEvent))
				{
					if (objectNotificationEvent.EventType == EventType.ObjectModified)
					{
						MessageModifiedNotificationEvent messageModifiedNotificationEvent = oldNev as MessageModifiedNotificationEvent;
						if (ObjectCreatedModifiedNotificationEvent.PropTagArraysEqual(messageModifiedNotificationEvent.ChangedPropTags, base.ChangedPropTags))
						{
							return NotificationEvent.RedundancyStatus.ReplaceOldAndStop;
						}
						return NotificationEvent.RedundancyStatus.MergeReplaceOldAndStop;
					}
					else
					{
						if (objectNotificationEvent.EventType == EventType.ObjectCreated)
						{
							return NotificationEvent.RedundancyStatus.DropNewAndStop;
						}
						return NotificationEvent.RedundancyStatus.FlagStopSearch;
					}
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

		// Token: 0x060008BB RID: 2235 RVA: 0x0004C0CC File Offset: 0x0004A2CC
		public override NotificationEvent MergeWithOldEvent(NotificationEvent oldNev)
		{
			return new MessageModifiedNotificationEvent(base.Database, base.MailboxNumber, base.UserIdentity, base.ClientType, base.EventFlags, (base.ExtendedEventFlags != null) ? base.ExtendedEventFlags.Value : Microsoft.Exchange.Server.Storage.LogicalDataModel.ExtendedEventFlags.None, base.Fid, base.Mid, base.ParentFid, base.DocumentId.Value, base.ConversationDocumentId, this.OldConversationDocumentId, ObjectCreatedModifiedNotificationEvent.MergeChangedPropTagArrays((oldNev as MessageModifiedNotificationEvent).ChangedPropTags, base.ChangedPropTags), base.ObjectClass, base.UserIdentityContext);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0004C16C File Offset: 0x0004A36C
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MessageModifiedNotificationEvent");
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0004C17A File Offset: 0x0004A37A
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" OldConversationDocumentId:[");
			sb.Append(this.oldConversationDocumentId);
			sb.Append("]");
		}

		// Token: 0x04000474 RID: 1140
		private readonly int? oldConversationDocumentId;
	}
}

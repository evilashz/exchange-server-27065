using System;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000075 RID: 117
	public static class NotificationEvents
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x0004AF27 File Offset: 0x00049127
		public static MailboxMoveStartedNotificationEvent CreateMailboxMoveStartedNotificationEvent(Context context, Mailbox mailbox, bool forSource)
		{
			return new MailboxMoveStartedNotificationEvent(mailbox.Database, mailbox.MailboxNumber, null, context.ClientType, forSource ? EventFlags.Source : EventFlags.Destination);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0004AF50 File Offset: 0x00049150
		public static MailboxMoveSucceededNotificationEvent CreateMailboxMoveSucceededNotificationEvent(Context context, Mailbox mailbox, bool forSource)
		{
			return new MailboxMoveSucceededNotificationEvent(mailbox.Database, mailbox.MailboxNumber, null, context.ClientType, forSource ? EventFlags.Source : EventFlags.Destination);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0004AF79 File Offset: 0x00049179
		public static MailboxMoveFailedNotificationEvent CreateMailboxMoveFailedNotificationEvent(Context context, Mailbox mailbox, bool forSource)
		{
			return new MailboxMoveFailedNotificationEvent(mailbox.Database, mailbox.MailboxNumber, null, context.ClientType, forSource ? EventFlags.Source : EventFlags.Destination);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0004AFA2 File Offset: 0x000491A2
		public static MailboxCreatedNotificationEvent CreateMailboxCreatedNotificationEvent(Context context, Mailbox mailbox)
		{
			return new MailboxCreatedNotificationEvent(mailbox.Database, mailbox.MailboxNumber, null, context.ClientType, EventFlags.None);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0004AFBD File Offset: 0x000491BD
		public static MailboxDeletedNotificationEvent CreateMailboxDeletedNotificationEvent(Context context, Mailbox mailbox)
		{
			return new MailboxDeletedNotificationEvent(mailbox.Database, mailbox.MailboxNumber, null, context.ClientType, EventFlags.None);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0004AFD8 File Offset: 0x000491D8
		public static MailboxDisconnectedNotificationEvent CreateMailboxDisconnectedNotificationEvent(Context context, Mailbox mailbox)
		{
			return new MailboxDisconnectedNotificationEvent(mailbox.Database, mailbox.MailboxNumber, null, context.ClientType, EventFlags.None);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0004AFF3 File Offset: 0x000491F3
		public static MailboxReconnectedNotificationEvent CreateMailboxReconnectedNotificationEvent(Context context, Mailbox mailbox)
		{
			return new MailboxReconnectedNotificationEvent(mailbox.Database, mailbox.MailboxNumber, null, context.ClientType, EventFlags.None);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0004B010 File Offset: 0x00049210
		public static MessageCreatedNotificationEvent CreateMessageCreatedEvent(Context context, TopMessage message)
		{
			return new MessageCreatedNotificationEvent(message.Mailbox.Database, message.Mailbox.MailboxNumber, null, context.ClientType, message.GetIsHidden(context) ? EventFlags.Associated : EventFlags.None, NotificationEvents.ComputeMessageExtendedEventFlags(context, message), message.GetFolderId(context), message.GetId(context), message.GetFolderId(context), message.GetDocumentId(context), message.GetConversationDocumentId(context), null, message.GetMessageClass(context), null);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0004B088 File Offset: 0x00049288
		public static MessageCreatedNotificationEvent CreateMessageCreatedEvent(Context context, TopMessage message, Folder searchFolder, Guid? userIdentityContext)
		{
			EventFlags eventFlags = EventFlags.SearchFolder;
			if (message.GetIsHidden(context))
			{
				eventFlags |= EventFlags.Associated;
			}
			return new MessageCreatedNotificationEvent(message.Mailbox.Database, message.Mailbox.MailboxNumber, null, context.ClientType, eventFlags, NotificationEvents.ComputeMessageExtendedEventFlags(context, message, null, searchFolder), message.GetFolderId(context), message.GetId(context), searchFolder.GetId(context), message.GetDocumentId(context), message.GetConversationDocumentId(context), null, message.GetMessageClass(context), userIdentityContext);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0004B100 File Offset: 0x00049300
		public static MessageModifiedNotificationEvent CreateMessageModifiedEvent(Context context, TopMessage message, Guid? userIdentityContext)
		{
			EventFlags eventFlags = message.GetIsHidden(context) ? EventFlags.Associated : EventFlags.None;
			if (0UL != (message.ChangedGroups & 36028797018963968UL))
			{
				eventFlags |= EventFlags.CommonCategorizationPropertyChanged;
			}
			return new MessageModifiedNotificationEvent(message.Mailbox.Database, message.Mailbox.MailboxNumber, null, context.ClientType, eventFlags, NotificationEvents.ComputeMessageExtendedEventFlags(context, message), message.GetFolderId(context), message.GetId(context), message.GetFolderId(context), message.GetDocumentId(context), message.GetConversationDocumentId(context), message.GetOriginalConversationDocumentId(context), null, message.GetMessageClass(context), userIdentityContext);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0004B194 File Offset: 0x00049394
		public static MessageModifiedNotificationEvent CreateMessageModifiedEvent(Context context, TopMessage message, SearchFolder searchFolder, Guid? userIdentityContext)
		{
			EventFlags eventFlags = EventFlags.SearchFolder;
			if (message.GetIsHidden(context))
			{
				eventFlags |= EventFlags.Associated;
			}
			if (0UL != (message.ChangedGroups & 36028797018963968UL))
			{
				eventFlags |= EventFlags.CommonCategorizationPropertyChanged;
			}
			return new MessageModifiedNotificationEvent(message.Mailbox.Database, message.Mailbox.MailboxNumber, null, context.ClientType, eventFlags, NotificationEvents.ComputeMessageExtendedEventFlags(context, message, null, searchFolder), message.GetFolderId(context), message.GetId(context), searchFolder.GetId(context), message.GetDocumentId(context), message.GetConversationDocumentId(context), message.GetOriginalConversationDocumentId(context), null, message.GetMessageClass(context), userIdentityContext);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0004B230 File Offset: 0x00049430
		public static MessageDeletedNotificationEvent CreateMessageDeletedEvent(Context context, TopMessage message)
		{
			return new MessageDeletedNotificationEvent(message.Mailbox.Database, message.Mailbox.MailboxNumber, null, context.ClientType, message.GetIsHidden(context) ? EventFlags.Associated : EventFlags.None, NotificationEvents.ComputeMessageExtendedEventFlags(context, message), message.OriginalFolder.GetId(context), message.OriginalMessageID, message.OriginalFolder.GetId(context), message.GetDocumentId(context), message.GetOriginalConversationDocumentId(context), message.GetMessageClass(context), null, null);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0004B2B0 File Offset: 0x000494B0
		public static MessageDeletedNotificationEvent CreateMessageDeletedEvent(Context context, TopMessage message, Folder searchFolder, Guid? userIdentityContext)
		{
			EventFlags eventFlags = EventFlags.SearchFolder;
			if (message.GetIsHidden(context))
			{
				eventFlags |= EventFlags.Associated;
			}
			return new MessageDeletedNotificationEvent(message.Mailbox.Database, message.Mailbox.MailboxNumber, null, context.ClientType, eventFlags, NotificationEvents.ComputeMessageExtendedEventFlags(context, message, null, searchFolder), message.GetOriginalFolderId(context), message.OriginalMessageID, searchFolder.GetId(context), message.GetDocumentId(context), message.GetOriginalConversationDocumentId(context), message.GetMessageClass(context), null, userIdentityContext);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0004B328 File Offset: 0x00049528
		public static MessageMovedNotificationEvent CreateMessageMovedEvent(Context context, TopMessage message, Folder originalFolder)
		{
			return new MessageMovedNotificationEvent(message.Mailbox.Database, message.Mailbox.MailboxNumber, null, context.ClientType, message.GetIsHidden(context) ? EventFlags.Associated : EventFlags.None, NotificationEvents.ComputeMessageExtendedEventFlags(context, message, originalFolder, null), message.GetFolderId(context), message.GetId(context), message.GetFolderId(context), message.GetDocumentId(context), message.GetConversationDocumentId(context), originalFolder.GetId(context), message.OriginalMessageID, originalFolder.GetId(context), message.GetOriginalConversationDocumentId(context), message.GetMessageClass(context));
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0004B3B4 File Offset: 0x000495B4
		public static MessageCopiedNotificationEvent CreateMessageCopiedEvent(Context context, TopMessage message, Folder originalFolder, ExchangeId originalMessageId)
		{
			return new MessageCopiedNotificationEvent(message.Mailbox.Database, message.Mailbox.MailboxNumber, null, context.ClientType, EventFlags.None, NotificationEvents.ComputeMessageExtendedEventFlags(context, message), message.GetFolderId(context), message.GetId(context), message.GetFolderId(context), message.GetDocumentId(context), message.GetConversationDocumentId(context), originalFolder.GetId(context), originalMessageId, originalFolder.GetId(context), message.GetOriginalConversationDocumentId(context), message.GetMessageClass(context));
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0004B42B File Offset: 0x0004962B
		public static NewMailNotificationEvent CreateNewMailEvent(Context context, TopMessage message)
		{
			return NotificationEvents.CreateNewMailEvent(context, message, message.GetMessageClass(context), MessageFlags.None);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0004B43C File Offset: 0x0004963C
		public static NewMailNotificationEvent CreateNewMailEvent(Context context, TopMessage message, string messageClass, MessageFlags messageFlags)
		{
			return new NewMailNotificationEvent(message.Mailbox.Database, message.Mailbox.MailboxNumber, null, context.ClientType, EventFlags.None, NotificationEvents.ComputeMessageExtendedEventFlags(context, message), message.GetFolderId(context), message.GetId(context), message.GetDocumentId(context), message.GetConversationDocumentId(context), messageClass.ToUpperInvariant(), (int)messageFlags);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0004B498 File Offset: 0x00049698
		public static MailSubmittedNotificationEvent CreateMailSubmittedEvent(Context context, TopMessage message)
		{
			return new MailSubmittedNotificationEvent(message.Mailbox.Database, message.Mailbox.MailboxNumber, null, context.ClientType, EventFlags.None, NotificationEvents.ComputeMessageExtendedEventFlags(context, message), message.GetFolderId(context), message.GetId(context), message.GetDocumentId(context), message.GetConversationDocumentId(context), message.GetMessageClass(context));
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0004B4F4 File Offset: 0x000496F4
		public static MessageCreatedNotificationEvent CreateConversationCreatedEvent(Context context, Folder parentFolder, ConversationItem conversationItem)
		{
			EventFlags eventFlags = EventFlags.Conversation;
			if (parentFolder is SearchFolder)
			{
				eventFlags |= EventFlags.SearchFolder;
			}
			return new MessageCreatedNotificationEvent(conversationItem.Mailbox.Database, conversationItem.Mailbox.MailboxNumber, null, context.ClientType, eventFlags, NotificationEvents.ComputeMessageExtendedEventFlags(context, conversationItem), conversationItem.GetFolderId(context), conversationItem.GetId(context), parentFolder.GetId(context), conversationItem.GetDocumentId(context), null, null, null, null);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0004B574 File Offset: 0x00049774
		public static MessageModifiedNotificationEvent CreateConversationModifiedEvent(Context context, Folder parentFolder, ConversationItem conversationItem)
		{
			EventFlags eventFlags = EventFlags.Conversation;
			if (parentFolder is SearchFolder)
			{
				eventFlags |= EventFlags.SearchFolder;
			}
			return new MessageModifiedNotificationEvent(conversationItem.Mailbox.Database, conversationItem.Mailbox.MailboxNumber, null, context.ClientType, eventFlags, NotificationEvents.ComputeMessageExtendedEventFlags(context, conversationItem), conversationItem.GetFolderId(context), conversationItem.GetId(context), parentFolder.GetId(context), conversationItem.GetDocumentId(context), null, null, null, null, null);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0004B5FC File Offset: 0x000497FC
		public static MessageDeletedNotificationEvent CreateConversationDeletedEvent(Context context, Folder parentFolder, ConversationItem conversationItem)
		{
			EventFlags eventFlags = EventFlags.Conversation;
			if (parentFolder is SearchFolder)
			{
				eventFlags |= EventFlags.SearchFolder;
			}
			return new MessageDeletedNotificationEvent(conversationItem.Mailbox.Database, conversationItem.Mailbox.MailboxNumber, null, context.ClientType, eventFlags, NotificationEvents.ComputeMessageExtendedEventFlags(context, conversationItem), conversationItem.GetFolderId(context), conversationItem.GetId(context), parentFolder.GetId(context), conversationItem.GetDocumentId(context), null, null, (byte[])conversationItem.GetPropertyValue(context, PropTag.Message.ConversationId), null);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0004B68C File Offset: 0x0004988C
		public static FolderCreatedNotificationEvent CreateFolderCreatedEvent(Context context, Folder folder)
		{
			return new FolderCreatedNotificationEvent(folder.Mailbox.Database, folder.Mailbox.MailboxNumber, null, context.ClientType, EventFlags.None, NotificationEvents.ComputeFolderExtendedEventFlags(context, folder, ExtendedEventFlags.None), folder.GetId(context), folder.GetParentFolderId(context), null, folder.GetContainerClass(context));
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0004B6DC File Offset: 0x000498DC
		public static FolderModifiedNotificationEvent CreateFolderModifiedEvent(Context context, Folder folder, EventFlags flags, ExtendedEventFlags additionalExtentedFlags, int totalMessageCount, int unreadMessageCount)
		{
			return new FolderModifiedNotificationEvent(folder.Mailbox.Database, folder.Mailbox.MailboxNumber, null, context.ClientType, flags, NotificationEvents.ComputeFolderExtendedEventFlags(context, folder, additionalExtentedFlags), folder.GetId(context), folder.GetParentFolderId(context), null, folder.GetContainerClass(context), totalMessageCount, unreadMessageCount);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0004B730 File Offset: 0x00049930
		public static FolderDeletedNotificationEvent CreateFolderDeletedEvent(Context context, Folder folder)
		{
			return new FolderDeletedNotificationEvent(folder.Mailbox.Database, folder.Mailbox.MailboxNumber, null, context.ClientType, EventFlags.None, NotificationEvents.ComputeFolderExtendedEventFlags(context, folder, ExtendedEventFlags.None), folder.GetId(context), folder.GetParentFolderId(context), folder.GetContainerClass(context));
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0004B780 File Offset: 0x00049980
		public static FolderMovedNotificationEvent CreateFolderMovedEvent(Context context, Folder folder, ExchangeId originalParentFolderId)
		{
			return new FolderMovedNotificationEvent(folder.Mailbox.Database, folder.Mailbox.MailboxNumber, null, context.ClientType, EventFlags.None, NotificationEvents.ComputeFolderExtendedEventFlags(context, folder, ExtendedEventFlags.None), folder.GetId(context), folder.GetParentFolderId(context), folder.GetId(context), originalParentFolderId, folder.GetContainerClass(context));
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0004B7D8 File Offset: 0x000499D8
		public static FolderCopiedNotificationEvent CreateFolderCopiedEvent(Context context, Folder folder, ExchangeId originalFolderId, ExchangeId originalParentFolderId)
		{
			return new FolderCopiedNotificationEvent(folder.Mailbox.Database, folder.Mailbox.MailboxNumber, null, context.ClientType, EventFlags.None, NotificationEvents.ComputeFolderExtendedEventFlags(context, folder, ExtendedEventFlags.None), folder.GetId(context), folder.GetParentFolderId(context), originalFolderId, originalParentFolderId, folder.GetContainerClass(context));
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0004B828 File Offset: 0x00049A28
		private static ExtendedEventFlags ComputeMessageExtendedEventFlags(Context context, TopMessage message)
		{
			return NotificationEvents.ComputeMessageExtendedEventFlags(context, message, null, null);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0004B834 File Offset: 0x00049A34
		private static ExtendedEventFlags ComputeMessageExtendedEventFlags(Context context, TopMessage message, Folder originalFolder, Folder searchFolder)
		{
			ExtendedEventFlags extendedEventFlags;
			if (searchFolder != null)
			{
				extendedEventFlags = NotificationEvents.ComputeFolderExtendedEventFlags(context, searchFolder, ExtendedEventFlags.None);
			}
			else
			{
				extendedEventFlags = NotificationEvents.ComputeFolderExtendedEventFlags(context, message.ParentFolder, ExtendedEventFlags.None);
				if (originalFolder != null)
				{
					extendedEventFlags |= NotificationEvents.ComputeFolderExtendedEventFlags(context, originalFolder, ExtendedEventFlags.None);
				}
			}
			extendedEventFlags &= (ExtendedEventFlags.NonIPMFolder | ExtendedEventFlags.IPMFolder | ExtendedEventFlags.InternalAccessFolder);
			if ((extendedEventFlags & (ExtendedEventFlags)((ulong)-2147483648)) == (ExtendedEventFlags)((ulong)-2147483648))
			{
				extendedEventFlags &= ~ExtendedEventFlags.NonIPMFolder;
			}
			if (0UL == (message.ChangedGroups & 4611686018427387904UL))
			{
				extendedEventFlags |= ExtendedEventFlags.NoReminderPropertyModified;
			}
			bool flag = message.ParentFolder.IsPartOfContentIndexing(context);
			if (0UL == (message.ChangedGroups & 2305843009213693952UL) || !flag)
			{
				extendedEventFlags |= ExtendedEventFlags.NoCIPropertyModified;
			}
			if (0UL != (message.ChangedGroups & 576460752303423488UL))
			{
				extendedEventFlags |= ExtendedEventFlags.RetentionTagModified;
			}
			if (0UL != (message.ChangedGroups & 288230376151711744UL))
			{
				extendedEventFlags |= ExtendedEventFlags.RetentionPropertiesModified;
			}
			if (0UL == (message.ChangedGroups & 144115188075855872UL))
			{
				extendedEventFlags |= ExtendedEventFlags.AppointmentTimeNotModified;
			}
			if (0UL == (message.ChangedGroups & 72057594037927936UL))
			{
				extendedEventFlags |= ExtendedEventFlags.AppointmentFreeBusyNotModified;
			}
			if (message.NeedsGroupExpansion(context))
			{
				extendedEventFlags |= ExtendedEventFlags.NeedGroupExpansion;
			}
			if (message.IsInferenceProcessingNeeded(context))
			{
				extendedEventFlags |= ExtendedEventFlags.InferenceProcessingNeeded;
			}
			if (0UL != (message.ChangedGroups & 18014398509481984UL))
			{
				extendedEventFlags |= ExtendedEventFlags.ModernRemindersChanged;
			}
			if (!flag)
			{
				extendedEventFlags |= ExtendedEventFlags.FolderIsNotPartOfContentIndexing;
			}
			if (0UL != (message.ChangedGroups & 9007199254740992UL) && message.TimerEventFired)
			{
				extendedEventFlags |= ExtendedEventFlags.TimerEventFired;
			}
			return extendedEventFlags;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0004B9C4 File Offset: 0x00049BC4
		private static ExtendedEventFlags ComputeFolderExtendedEventFlags(Context context, Folder folder, ExtendedEventFlags additionalFlags)
		{
			ExtendedEventFlags extendedEventFlags = additionalFlags | (ExtendedEventFlags)(folder.IsIpmFolder(context) ? ((ulong)int.MinValue) : 1073741824UL);
			if (!folder.IsPartOfContentIndexing(context))
			{
				extendedEventFlags |= ExtendedEventFlags.FolderIsNotPartOfContentIndexing;
			}
			if (folder.IsInternalAccess(context))
			{
				extendedEventFlags |= ExtendedEventFlags.InternalAccessFolder;
			}
			return extendedEventFlags;
		}
	}
}

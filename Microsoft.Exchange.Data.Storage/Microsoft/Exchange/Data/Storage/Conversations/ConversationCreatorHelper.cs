using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008A1 RID: 2209
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ConversationCreatorHelper
	{
		// Token: 0x060052AF RID: 21167 RVA: 0x001597C0 File Offset: 0x001579C0
		public static bool TryCalculateConversationCreatorSidOnDeliveryProcessing(MailboxSession mailboxSession, ICorePropertyBag itemPropertyBag, ConversationIndex.FixupStage fixupStage, ConversationIndex conversationIndex, out byte[] conversationCreatorSid, out bool updateAllConversationMessages)
		{
			conversationCreatorSid = null;
			updateAllConversationMessages = false;
			if (!ConversationCreatorHelper.SupportsConversationCreator(mailboxSession))
			{
				return false;
			}
			byte[] valueOrDefault = itemPropertyBag.GetValueOrDefault<byte[]>(InternalSchema.SenderSID, null);
			return ConversationCreatorHelper.TryCalculateConversationCreatorSid(mailboxSession, itemPropertyBag, fixupStage, conversationIndex, valueOrDefault, out conversationCreatorSid, out updateAllConversationMessages);
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x001597FC File Offset: 0x001579FC
		public static bool TryCalculateConversationCreatorSidOnSaving(MailboxSession mailboxSession, ICorePropertyBag itemPropertyBag, ConversationIndex.FixupStage fixupStage, ConversationIndex conversationIndex, out byte[] conversationCreatorSid)
		{
			conversationCreatorSid = null;
			if (!ConversationCreatorHelper.SupportsConversationCreator(mailboxSession) || itemPropertyBag.GetValueOrDefault<bool>(InternalSchema.DeleteAfterSubmit, false))
			{
				return false;
			}
			conversationCreatorSid = ConversationCreatorHelper.CalculateConversationCreatorSid(mailboxSession, itemPropertyBag, fixupStage, conversationIndex, ValueConvertor.ConvertValueToBinary(mailboxSession.MailboxOwner.Sid, null));
			return conversationCreatorSid != null;
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x0015984C File Offset: 0x00157A4C
		public static bool TryCalculateConversationCreatorSidOnReplying(MailboxSession mailboxSession, ConversationIndex conversationIndex, out byte[] conversationCreatorSid)
		{
			conversationCreatorSid = null;
			if (!ConversationCreatorHelper.SupportsConversationCreator(mailboxSession))
			{
				return false;
			}
			ConversationCreatorHelper.ConversationCreatorDefinitionData definitionData = new ConversationCreatorHelper.ConversationCreatorDefinitionData(mailboxSession, conversationIndex);
			conversationCreatorSid = ConversationCreatorHelper.CalculateConversationCreatorSid(definitionData, ConversationCreatorHelper.MessageDeliveryScenario.DeliveringNonRootMessage, null);
			return conversationCreatorSid != null;
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x00159880 File Offset: 0x00157A80
		public static void FixupConversationMessagesCreatorSid(MailboxSession mailboxSession, ConversationIndex conversationIndex, byte[] conversationCreatorSid)
		{
			ConversationCreatorHelper.ConversationCreatorDefinitionData conversationCreatorDefinitionData = new ConversationCreatorHelper.ConversationCreatorDefinitionData(mailboxSession, conversationIndex);
			foreach (IConversationTreeNode conversationTreeNode in conversationCreatorDefinitionData.Conversation.ConversationTree)
			{
				ConversationTreeNode conversationTreeNode2 = (ConversationTreeNode)conversationTreeNode;
				foreach (StoreObjectId storeId in conversationTreeNode2.ToListStoreObjectId())
				{
					using (Item item = Item.Bind(mailboxSession, storeId, null))
					{
						item.OpenAsReadWrite();
						item.SetOrDeleteProperty(ItemSchema.ConversationCreatorSID, conversationCreatorSid);
						item.Save(SaveMode.ResolveConflicts);
					}
				}
			}
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x00159954 File Offset: 0x00157B54
		private static ConversationCreatorHelper.MessageDeliveryScenario CalculateConversationDeliveryScenario(ConversationCreatorHelper.ConversationCreatorDefinitionData definitionData, ICorePropertyBag itemPropertyBag, ConversationIndex.FixupStage fixupStage)
		{
			if (ConversationIndex.CheckStageValue(fixupStage, ConversationIndex.FixupStage.Error))
			{
				return ConversationCreatorHelper.MessageDeliveryScenario.Unknown;
			}
			if (ConversationIndex.IsFixUpCreatingNewConversation(fixupStage))
			{
				return ConversationCreatorHelper.MessageDeliveryScenario.DeliveringRootMessage;
			}
			if (definitionData.Conversation.RootMessageId == null)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string, string>(0L, "ConversationCreatorHelper::CalculateConversationDeliveryScenario : On some corner cases, the conversation is loaded without nodes and then root node is null. MessageClassConversationID:{0} FixupStage:{1}", definitionData.Conversation.ConversationId.ToString(), fixupStage.ToString());
				return ConversationCreatorHelper.MessageDeliveryScenario.DeliveringRootMessage;
			}
			if (ConversationIndex.IsFixupAddingOutOfOrderMessageToConversation(fixupStage) && ConversationCreatorHelper.IsRootMessage(definitionData.Conversation, itemPropertyBag))
			{
				return ConversationCreatorHelper.MessageDeliveryScenario.DeliveringOutOfOrderRootMessage;
			}
			return ConversationCreatorHelper.MessageDeliveryScenario.Unknown;
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x001599D0 File Offset: 0x00157BD0
		private static bool IsRootMessage(Conversation conversation, ICorePropertyBag messagePropertyBag)
		{
			StoreObjectId rootMessageId = conversation.RootMessageId;
			if (rootMessageId == null)
			{
				return false;
			}
			VersionedId valueOrDefault = messagePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
			return rootMessageId.Equals(valueOrDefault.ObjectId);
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x00159A04 File Offset: 0x00157C04
		private static bool TryCalculateConversationCreatorSid(MailboxSession mailboxSession, ICorePropertyBag itemPropertyBag, ConversationIndex.FixupStage fixupStage, ConversationIndex conversationIndex, byte[] itemOwnerSID, out byte[] conversationCreatorSid, out bool updateAllConversationMessages)
		{
			ConversationCreatorHelper.ConversationCreatorDefinitionData definitionData = new ConversationCreatorHelper.ConversationCreatorDefinitionData(mailboxSession, conversationIndex);
			ConversationCreatorHelper.MessageDeliveryScenario messageDeliveryScenario = ConversationCreatorHelper.CalculateConversationDeliveryScenario(definitionData, itemPropertyBag, fixupStage);
			return ConversationCreatorHelper.TryCalculateConversationCreatorSid(definitionData, messageDeliveryScenario, itemOwnerSID, out conversationCreatorSid, out updateAllConversationMessages);
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x00159A30 File Offset: 0x00157C30
		private static byte[] CalculateConversationCreatorSid(MailboxSession mailboxSession, ICorePropertyBag itemPropertyBag, ConversationIndex.FixupStage fixupStage, ConversationIndex conversationIndex, byte[] itemOwnerSID)
		{
			ConversationCreatorHelper.ConversationCreatorDefinitionData definitionData = new ConversationCreatorHelper.ConversationCreatorDefinitionData(mailboxSession, conversationIndex);
			ConversationCreatorHelper.MessageDeliveryScenario messageDeliveryScenario = ConversationCreatorHelper.CalculateConversationDeliveryScenario(definitionData, itemPropertyBag, fixupStage);
			return ConversationCreatorHelper.CalculateConversationCreatorSid(definitionData, messageDeliveryScenario, itemOwnerSID);
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x00159A58 File Offset: 0x00157C58
		private static bool TryCalculateConversationCreatorSid(ConversationCreatorHelper.ConversationCreatorDefinitionData definitionData, ConversationCreatorHelper.MessageDeliveryScenario messageDeliveryScenario, byte[] itemOwnerSID, out byte[] conversationCreatorSid, out bool updateAllConversationMessages)
		{
			updateAllConversationMessages = false;
			if (messageDeliveryScenario == ConversationCreatorHelper.MessageDeliveryScenario.DeliveringRootMessage || messageDeliveryScenario == ConversationCreatorHelper.MessageDeliveryScenario.Unknown)
			{
				conversationCreatorSid = itemOwnerSID;
				return true;
			}
			if (messageDeliveryScenario == ConversationCreatorHelper.MessageDeliveryScenario.DeliveringOutOfOrderRootMessage)
			{
				conversationCreatorSid = itemOwnerSID;
				updateAllConversationMessages = true;
				return true;
			}
			Conversation conversation = definitionData.Conversation;
			conversationCreatorSid = conversation.ConversationCreatorSID;
			if (conversationCreatorSid == null)
			{
				IConversationTreeNode rootMessageNode = definitionData.Conversation.ConversationTree.RootMessageNode;
				if (rootMessageNode == null)
				{
					return false;
				}
				ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "ConversationCreatorHelper::TryCalculateConversationCreatorSid : Probably the conversation was created before we started tracking the ConversationCreatorSID or the conversation was created from a message sent by X-Prem user. ConversationID:{0}", conversation.ConversationId.ToString());
				conversationCreatorSid = rootMessageNode.GetValueOrDefault<byte[]>(InternalSchema.SenderSID, null);
				updateAllConversationMessages = (conversationCreatorSid != null);
			}
			return conversationCreatorSid != null;
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x00159AE8 File Offset: 0x00157CE8
		private static byte[] CalculateConversationCreatorSid(ConversationCreatorHelper.ConversationCreatorDefinitionData definitionData, ConversationCreatorHelper.MessageDeliveryScenario messageDeliveryScenario, byte[] itemOwnerSID)
		{
			if (messageDeliveryScenario == ConversationCreatorHelper.MessageDeliveryScenario.DeliveringRootMessage || messageDeliveryScenario == ConversationCreatorHelper.MessageDeliveryScenario.Unknown)
			{
				return itemOwnerSID;
			}
			Conversation conversation = definitionData.Conversation;
			return conversation.ConversationCreatorSID;
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x00159B0C File Offset: 0x00157D0C
		private static bool SupportsConversationCreator(MailboxSession session)
		{
			if (session == null)
			{
				return false;
			}
			if (!ConversationCreatorHelper.IsSessionLogonTypeSupported(session.LogonType))
			{
				return false;
			}
			int? num = session.Mailbox.TryGetProperty(MailboxSchema.MailboxTypeDetail) as int?;
			return num != null && StoreSession.IsGroupMailbox(num.Value);
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x00159B60 File Offset: 0x00157D60
		private static bool IsSessionLogonTypeSupported(LogonType logonType)
		{
			switch (logonType)
			{
			case LogonType.Owner:
			case LogonType.Delegated:
			case LogonType.Transport:
				return true;
			}
			return false;
		}

		// Token: 0x020008A2 RID: 2210
		private class ConversationCreatorDefinitionData
		{
			// Token: 0x060052BB RID: 21179 RVA: 0x00159B8A File Offset: 0x00157D8A
			public ConversationCreatorDefinitionData(MailboxSession mailboxSession, ConversationIndex conversationIndex)
			{
				this.mailboxSession = mailboxSession;
				this.conversationIndex = conversationIndex;
			}

			// Token: 0x17001705 RID: 5893
			// (get) Token: 0x060052BC RID: 21180 RVA: 0x00159BA0 File Offset: 0x00157DA0
			public Conversation Conversation
			{
				get
				{
					if (this.conversation == null)
					{
						this.conversation = this.LoadConversationForConversationCreatorSidDefinition();
					}
					return this.conversation;
				}
			}

			// Token: 0x060052BD RID: 21181 RVA: 0x00159BBC File Offset: 0x00157DBC
			private Conversation LoadConversationForConversationCreatorSidDefinition()
			{
				ConversationId conversationId = ConversationId.Create(this.conversationIndex.Guid);
				return Conversation.Load(this.mailboxSession, conversationId, ConversationCreatorHelper.ConversationCreatorDefinitionData.ConversationCreatorRelevantProperties);
			}

			// Token: 0x04002D0E RID: 11534
			private static PropertyDefinition[] ConversationCreatorRelevantProperties = new PropertyDefinition[]
			{
				ItemSchema.Id,
				ItemSchema.ConversationCreatorSID,
				InternalSchema.SenderSID,
				ItemSchema.ReceivedTime
			};

			// Token: 0x04002D0F RID: 11535
			private readonly MailboxSession mailboxSession;

			// Token: 0x04002D10 RID: 11536
			private readonly ConversationIndex conversationIndex;

			// Token: 0x04002D11 RID: 11537
			private Conversation conversation;
		}

		// Token: 0x020008A3 RID: 2211
		private enum MessageDeliveryScenario
		{
			// Token: 0x04002D13 RID: 11539
			Unknown,
			// Token: 0x04002D14 RID: 11540
			DeliveringRootMessage,
			// Token: 0x04002D15 RID: 11541
			DeliveringOutOfOrderRootMessage,
			// Token: 0x04002D16 RID: 11542
			DeliveringNonRootMessage
		}
	}
}

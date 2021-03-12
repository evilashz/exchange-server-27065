using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008A6 RID: 2214
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationCreatorSidCalculator : IConversationCreatorSidCalculator
	{
		// Token: 0x060052C8 RID: 21192 RVA: 0x00159C91 File Offset: 0x00157E91
		public ConversationCreatorSidCalculator(IXSOFactory xsoFactory, IMailboxSession mailboxSession, ICoreConversationFactory<IConversation> conversationFactory)
		{
			this.mailboxSession = mailboxSession;
			this.conversationFactory = conversationFactory;
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x00159CB0 File Offset: 0x00157EB0
		public bool TryCalculateOnDelivery(ICorePropertyBag itemPropertyBag, ConversationIndex.FixupStage stage, ConversationIndex conversationIndex, out byte[] conversationCreatorSid, out bool updateAllConversationMessages)
		{
			ConversationCreatorSidCalculator.MessageType messageType = this.CalculateMessageTypeOnDelivery(conversationIndex, itemPropertyBag, stage);
			updateAllConversationMessages = (messageType == ConversationCreatorSidCalculator.MessageType.OutOfOrderRootMessage);
			byte[] valueOrDefault = itemPropertyBag.GetValueOrDefault<byte[]>(InternalSchema.SenderSID, null);
			return this.TryCalculateConversationCreatorSid(conversationIndex, messageType, valueOrDefault, out conversationCreatorSid);
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x00159CE8 File Offset: 0x00157EE8
		public bool TryCalculateOnSave(ICorePropertyBag itemPropertyBag, ConversationIndex.FixupStage stage, ConversationIndex conversationIndex, CoreItemOperation operation, out byte[] conversationCreatorSid)
		{
			conversationCreatorSid = null;
			if (operation != CoreItemOperation.Save)
			{
				return false;
			}
			byte[] itemOwnerSid = ValueConvertor.ConvertValueToBinary(this.mailboxSession.MailboxOwner.Sid, null);
			ConversationCreatorSidCalculator.MessageType messageType = this.CalculateMessageTypeOnSave(stage);
			return this.TryCalculateConversationCreatorSid(conversationIndex, messageType, itemOwnerSid, out conversationCreatorSid);
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x00159D29 File Offset: 0x00157F29
		public bool TryCalculateOnReply(ConversationIndex conversationIndex, out byte[] conversationCreatorSid)
		{
			conversationCreatorSid = null;
			return false;
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x00159D30 File Offset: 0x00157F30
		public void UpdateConversationMessages(ConversationIndex conversationIndex, byte[] conversationCreatorSid)
		{
			IConversation conversation = this.LoadConversation(conversationIndex);
			foreach (IConversationTreeNode conversationTreeNode in conversation.ConversationTree)
			{
				foreach (StoreObjectId id in conversationTreeNode.ToListStoreObjectId())
				{
					using (IItem item = this.xsoFactory.BindToItem(this.mailboxSession, id, new PropertyDefinition[0]))
					{
						item.OpenAsReadWrite();
						item.SetOrDeleteProperty(ItemSchema.ConversationCreatorSID, conversationCreatorSid);
						item.Save(SaveMode.ResolveConflicts);
					}
				}
			}
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x00159E0C File Offset: 0x0015800C
		private IConversation LoadConversation(ConversationIndex index)
		{
			ConversationId conversationId = ConversationId.Create(index);
			return this.conversationFactory.CreateConversation(conversationId, ConversationCreatorSidCalculator.ConversationCreatorRelevantProperties);
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x00159E34 File Offset: 0x00158034
		private ConversationCreatorSidCalculator.MessageType CalculateMessageTypeOnDelivery(ConversationIndex conversationIndex, ICorePropertyBag itemBag, ConversationIndex.FixupStage fixupStage)
		{
			if (ConversationIndex.CheckStageValue(fixupStage, ConversationIndex.FixupStage.Error))
			{
				return ConversationCreatorSidCalculator.MessageType.Unknown;
			}
			if (ConversationIndex.IsFixUpCreatingNewConversation(fixupStage))
			{
				return ConversationCreatorSidCalculator.MessageType.RootMessage;
			}
			IConversation conversation = this.LoadConversation(conversationIndex);
			if (conversation.RootMessageNode == null)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string, string>(0L, "ConversationCreatorHelper::CalculateConversationDeliveryScenario : On some corner cases, the conversation is loaded without nodes and then root node is null. MessageClassConversationID:{0} FixupStage:{1}", this.LoadConversation(conversationIndex).ConversationId.ToString(), fixupStage.ToString());
				return ConversationCreatorSidCalculator.MessageType.RootMessage;
			}
			if (ConversationIndex.IsFixupAddingOutOfOrderMessageToConversation(fixupStage) && this.IsRootMessage(conversation, itemBag))
			{
				return ConversationCreatorSidCalculator.MessageType.OutOfOrderRootMessage;
			}
			return ConversationCreatorSidCalculator.MessageType.NonRootMessage;
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x00159EAE File Offset: 0x001580AE
		private ConversationCreatorSidCalculator.MessageType CalculateMessageTypeOnSave(ConversationIndex.FixupStage fixupStage)
		{
			if (ConversationIndex.CheckStageValue(fixupStage, ConversationIndex.FixupStage.Error))
			{
				return ConversationCreatorSidCalculator.MessageType.Unknown;
			}
			if (ConversationIndex.IsFixUpCreatingNewConversation(fixupStage))
			{
				return ConversationCreatorSidCalculator.MessageType.RootMessage;
			}
			return ConversationCreatorSidCalculator.MessageType.NonRootMessage;
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x00159ECC File Offset: 0x001580CC
		private bool IsRootMessage(IConversation conversation, ICorePropertyBag message)
		{
			string valueOrDefault = conversation.RootMessageNode.GetValueOrDefault<string>(InternalSchema.InReplyTo, string.Empty);
			if (string.IsNullOrEmpty(valueOrDefault))
			{
				return false;
			}
			string valueOrDefault2 = message.GetValueOrDefault<string>(InternalSchema.InternetMessageId, string.Empty);
			return valueOrDefault.Equals(valueOrDefault2, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x00159F14 File Offset: 0x00158114
		private bool TryCalculateConversationCreatorSid(ConversationIndex conversationIndex, ConversationCreatorSidCalculator.MessageType messageType, byte[] itemOwnerSid, out byte[] conversationCreatorSid)
		{
			switch (messageType)
			{
			case ConversationCreatorSidCalculator.MessageType.RootMessage:
			case ConversationCreatorSidCalculator.MessageType.OutOfOrderRootMessage:
				conversationCreatorSid = itemOwnerSid;
				break;
			case ConversationCreatorSidCalculator.MessageType.NonRootMessage:
				conversationCreatorSid = this.LoadConversation(conversationIndex).ConversationCreatorSID;
				break;
			default:
				conversationCreatorSid = null;
				break;
			}
			return conversationCreatorSid != null;
		}

		// Token: 0x04002D18 RID: 11544
		private static PropertyDefinition[] ConversationCreatorRelevantProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.ConversationCreatorSID,
			InternalSchema.SenderSID,
			ItemSchema.ReceivedTime,
			ItemSchema.InReplyTo
		};

		// Token: 0x04002D19 RID: 11545
		private readonly IMailboxSession mailboxSession;

		// Token: 0x04002D1A RID: 11546
		private readonly ICoreConversationFactory<IConversation> conversationFactory;

		// Token: 0x04002D1B RID: 11547
		private readonly IXSOFactory xsoFactory;

		// Token: 0x020008A7 RID: 2215
		private enum MessageType
		{
			// Token: 0x04002D1D RID: 11549
			Unknown,
			// Token: 0x04002D1E RID: 11550
			RootMessage,
			// Token: 0x04002D1F RID: 11551
			OutOfOrderRootMessage,
			// Token: 0x04002D20 RID: 11552
			NonRootMessage
		}
	}
}

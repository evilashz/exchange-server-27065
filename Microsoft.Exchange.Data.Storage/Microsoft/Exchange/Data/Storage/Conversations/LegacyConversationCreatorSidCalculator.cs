using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008A5 RID: 2213
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LegacyConversationCreatorSidCalculator : IConversationCreatorSidCalculator
	{
		// Token: 0x060052C3 RID: 21187 RVA: 0x00159C2A File Offset: 0x00157E2A
		public LegacyConversationCreatorSidCalculator(IMailboxSession mailboxSession)
		{
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x00159C39 File Offset: 0x00157E39
		public bool TryCalculateOnDelivery(ICorePropertyBag itemPropertyBag, ConversationIndex.FixupStage stage, ConversationIndex conversationIndex, out byte[] conversationCreatorSid, out bool updateAllConversationMessages)
		{
			return ConversationCreatorHelper.TryCalculateConversationCreatorSidOnDeliveryProcessing(this.mailboxSession as MailboxSession, itemPropertyBag, stage, conversationIndex, out conversationCreatorSid, out updateAllConversationMessages);
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x00159C52 File Offset: 0x00157E52
		public bool TryCalculateOnSave(ICorePropertyBag itemPropertyBag, ConversationIndex.FixupStage stage, ConversationIndex conversationIndex, CoreItemOperation operation, out byte[] conversationCreatorSid)
		{
			return ConversationCreatorHelper.TryCalculateConversationCreatorSidOnSaving(this.mailboxSession as MailboxSession, itemPropertyBag, stage, conversationIndex, out conversationCreatorSid);
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x00159C69 File Offset: 0x00157E69
		public bool TryCalculateOnReply(ConversationIndex conversationIndex, out byte[] conversationCreatorSid)
		{
			return ConversationCreatorHelper.TryCalculateConversationCreatorSidOnReplying(this.mailboxSession as MailboxSession, conversationIndex, out conversationCreatorSid);
		}

		// Token: 0x060052C7 RID: 21191 RVA: 0x00159C7D File Offset: 0x00157E7D
		public void UpdateConversationMessages(ConversationIndex conversationIndex, byte[] conversationCreatorSid)
		{
			ConversationCreatorHelper.FixupConversationMessagesCreatorSid(this.mailboxSession as MailboxSession, conversationIndex, conversationCreatorSid);
		}

		// Token: 0x04002D17 RID: 11543
		private readonly IMailboxSession mailboxSession;
	}
}

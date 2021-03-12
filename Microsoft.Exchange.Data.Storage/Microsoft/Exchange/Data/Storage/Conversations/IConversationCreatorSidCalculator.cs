using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008A4 RID: 2212
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationCreatorSidCalculator
	{
		// Token: 0x060052BF RID: 21183
		bool TryCalculateOnDelivery(ICorePropertyBag itemPropertyBag, ConversationIndex.FixupStage stage, ConversationIndex conversationIndex, out byte[] conversationCreatorSid, out bool updateAllConversationMessages);

		// Token: 0x060052C0 RID: 21184
		bool TryCalculateOnSave(ICorePropertyBag itemPropertyBag, ConversationIndex.FixupStage stage, ConversationIndex conversationIndex, CoreItemOperation operation, out byte[] conversationCreatorSid);

		// Token: 0x060052C1 RID: 21185
		bool TryCalculateOnReply(ConversationIndex conversationIndex, out byte[] conversationCreatorSid);

		// Token: 0x060052C2 RID: 21186
		void UpdateConversationMessages(ConversationIndex conversationIndex, byte[] conversationCreatorSid);
	}
}

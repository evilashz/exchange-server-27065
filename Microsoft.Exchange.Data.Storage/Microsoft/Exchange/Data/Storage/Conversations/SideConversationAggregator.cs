using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F6A RID: 3946
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SideConversationAggregator : AudienceBasedConversationAggregator
	{
		// Token: 0x060086F5 RID: 34549 RVA: 0x0025008A File Offset: 0x0024E28A
		public SideConversationAggregator(IMailboxOwner mailboxOwner, bool scenarioSupportsSearchForDuplicatedMessages, IAggregationByItemClassReferencesSubjectProcessor referencesProcessor, IAggregationByParticipantProcessor participantProcessor, IConversationAggregationDiagnosticsFrame diagnosticsFrame) : base(mailboxOwner, scenarioSupportsSearchForDuplicatedMessages, referencesProcessor, participantProcessor, diagnosticsFrame)
		{
		}

		// Token: 0x060086F6 RID: 34550 RVA: 0x0025009C File Offset: 0x0024E29C
		protected override ConversationAggregationResult ConstructResult(ConversationIndex.FixupStage bySubjectResultingStage, ConversationIndex bySubjectResultingIndex, IStorePropertyBag parentItem, bool participantRemoved)
		{
			ConversationAggregationResult conversationAggregationResult = this.ConstructResult(bySubjectResultingStage, bySubjectResultingIndex, parentItem);
			if (participantRemoved)
			{
				conversationAggregationResult.Stage |= ConversationIndex.FixupStage.SC;
				conversationAggregationResult.ConversationIndex = bySubjectResultingIndex.UpdateGuid(Guid.NewGuid());
			}
			return conversationAggregationResult;
		}
	}
}

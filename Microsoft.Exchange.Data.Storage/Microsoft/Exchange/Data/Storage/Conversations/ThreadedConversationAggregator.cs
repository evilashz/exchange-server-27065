using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000885 RID: 2181
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ThreadedConversationAggregator : AudienceBasedConversationAggregator
	{
		// Token: 0x060051F4 RID: 20980 RVA: 0x00157239 File Offset: 0x00155439
		public ThreadedConversationAggregator(IMailboxOwner mailboxOwner, bool scenarioSupportsSearchForDuplicatedMessages, IAggregationByItemClassReferencesSubjectProcessor referencesProcessor, IAggregationByParticipantProcessor participantProcessor, IConversationAggregationDiagnosticsFrame diagnosticsFrame) : base(mailboxOwner, scenarioSupportsSearchForDuplicatedMessages, referencesProcessor, participantProcessor, diagnosticsFrame)
		{
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x00157248 File Offset: 0x00155448
		protected override ConversationAggregationResult ConstructResult(ConversationIndex.FixupStage bySubjectResultingStage, ConversationIndex bySubjectResultingIndex, IStorePropertyBag parentItem, bool participantRemoved)
		{
			ConversationAggregationResult conversationAggregationResult = this.ConstructResult(bySubjectResultingStage, bySubjectResultingIndex, parentItem);
			if (participantRemoved)
			{
				conversationAggregationResult.Stage |= ConversationIndex.FixupStage.TC;
				conversationAggregationResult.ConversationFamilyId = ConversationId.Create(Guid.NewGuid());
			}
			return conversationAggregationResult;
		}
	}
}

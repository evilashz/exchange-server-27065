using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000884 RID: 2180
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AudienceBasedConversationAggregator : ConversationAggregatorBase
	{
		// Token: 0x060051ED RID: 20973 RVA: 0x0015702D File Offset: 0x0015522D
		public AudienceBasedConversationAggregator(IMailboxOwner mailboxOwner, bool scenarioSupportsSearchForDuplicatedMessages, IAggregationByItemClassReferencesSubjectProcessor referencesProcessor, IAggregationByParticipantProcessor participantProcessor, IConversationAggregationDiagnosticsFrame diagnosticsFrame) : base(referencesProcessor)
		{
			this.mailboxOwner = mailboxOwner;
			this.participantProcessor = participantProcessor;
			this.diagnosticsFrame = diagnosticsFrame;
			this.scenarioSupportsSearchForDuplicatedMessages = scenarioSupportsSearchForDuplicatedMessages;
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x00157054 File Offset: 0x00155254
		public static bool IsMessageCreatingNewConversation(IStorePropertyBag parentItemPropertyBag, ConversationIndex.FixupStage stage)
		{
			return parentItemPropertyBag == null || ConversationIndex.IsFixUpCreatingNewConversation(stage);
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x00157070 File Offset: 0x00155270
		public static bool SupportsSideConversation(IStorePropertyBag message)
		{
			if (!message.GetValueOrDefault<bool>(ItemSchema.SupportsSideConversation, false))
			{
				string itemClass = message.TryGetProperty(InternalSchema.ItemClass) as string;
				return ObjectClass.IsCalendarItem(itemClass);
			}
			return true;
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x0015717C File Offset: 0x0015537C
		public override ConversationAggregationResult Aggregate(ICoreItem message)
		{
			return this.diagnosticsFrame.TrackAggregation(base.GetType().Name, delegate(IConversationAggregationLogger logger)
			{
				bool shouldSearchForDuplicatedMessage = this.ShouldSearchForDuplicatedMessage(message.PropertyBag);
				logger.LogMailboxOwnerData(this.mailboxOwner, shouldSearchForDuplicatedMessage);
				logger.LogDeliveredMessageData(message.PropertyBag);
				IStorePropertyBag storePropertyBag;
				ConversationIndex bySubjectResultingIndex;
				ConversationIndex.FixupStage fixupStage;
				this.ReferencesProcessor.Aggregate(message.PropertyBag, shouldSearchForDuplicatedMessage, out storePropertyBag, out bySubjectResultingIndex, out fixupStage);
				logger.LogParentMessageData(storePropertyBag);
				bool participantRemoved = false;
				if (this.participantProcessor.ShouldAggregate(message.PropertyBag, storePropertyBag, fixupStage))
				{
					participantRemoved = this.participantProcessor.Aggregate(logger, message.PropertyBag, storePropertyBag);
				}
				ConversationAggregationResult conversationAggregationResult = this.ConstructResult(fixupStage, bySubjectResultingIndex, storePropertyBag, participantRemoved);
				logger.LogAggregationResultData(conversationAggregationResult);
				return conversationAggregationResult;
			});
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x001571C0 File Offset: 0x001553C0
		protected override ConversationAggregationResult ConstructResult(ConversationIndex.FixupStage bySubjectResultingStage, ConversationIndex bySubjectResultingIndex, IStorePropertyBag parentItem)
		{
			ConversationAggregationResult conversationAggregationResult = base.ConstructResult(bySubjectResultingStage, bySubjectResultingIndex, parentItem);
			if (AudienceBasedConversationAggregator.IsMessageCreatingNewConversation(parentItem, bySubjectResultingStage))
			{
				conversationAggregationResult.SupportsSideConversation = true;
				conversationAggregationResult.ConversationFamilyId = ConversationId.Create(bySubjectResultingIndex);
			}
			else
			{
				conversationAggregationResult.SupportsSideConversation = AudienceBasedConversationAggregator.SupportsSideConversation(parentItem);
				conversationAggregationResult.ConversationFamilyId = parentItem.GetValueOrDefault<ConversationId>(ItemSchema.ConversationFamilyId, null);
			}
			return conversationAggregationResult;
		}

		// Token: 0x060051F2 RID: 20978
		protected abstract ConversationAggregationResult ConstructResult(ConversationIndex.FixupStage bySubjectResultingStage, ConversationIndex bySubjectResultingIndex, IStorePropertyBag parentItem, bool participantRemoved);

		// Token: 0x060051F3 RID: 20979 RVA: 0x00157214 File Offset: 0x00155414
		private bool ShouldSearchForDuplicatedMessage(ICorePropertyBag messageToAggregate)
		{
			return this.scenarioSupportsSearchForDuplicatedMessages && this.mailboxOwner.SearchDuplicatedMessagesEnabled && this.mailboxOwner.SentToMySelf(messageToAggregate);
		}

		// Token: 0x04002C90 RID: 11408
		private readonly bool scenarioSupportsSearchForDuplicatedMessages;

		// Token: 0x04002C91 RID: 11409
		private readonly IConversationAggregationDiagnosticsFrame diagnosticsFrame;

		// Token: 0x04002C92 RID: 11410
		private readonly IAggregationByParticipantProcessor participantProcessor;

		// Token: 0x04002C93 RID: 11411
		private readonly IMailboxOwner mailboxOwner;
	}
}

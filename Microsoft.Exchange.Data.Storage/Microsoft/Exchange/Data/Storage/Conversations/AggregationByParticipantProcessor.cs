using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x0200087C RID: 2172
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregationByParticipantProcessor : IAggregationByParticipantProcessor
	{
		// Token: 0x060051C8 RID: 20936 RVA: 0x00155400 File Offset: 0x00153600
		public AggregationByParticipantProcessor(IReplyAllExtractor replyAllExtractor)
		{
			this.replyAllExtractor = replyAllExtractor;
		}

		// Token: 0x170016D4 RID: 5844
		// (get) Token: 0x060051C9 RID: 20937 RVA: 0x0015540F File Offset: 0x0015360F
		protected IReplyAllExtractor ReplyAllExtractor
		{
			get
			{
				return this.replyAllExtractor;
			}
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x00155417 File Offset: 0x00153617
		public static AggregationByParticipantProcessor CreateInstance(IMailboxSession session, IXSOFactory xsoFactory)
		{
			return new AggregationByParticipantProcessor(new ReplyAllExtractor(session, xsoFactory));
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x00155428 File Offset: 0x00153628
		public bool ShouldAggregate(ICorePropertyBag message, IStorePropertyBag parentMessage, ConversationIndex.FixupStage previousStage)
		{
			if (AudienceBasedConversationAggregator.IsMessageCreatingNewConversation(parentMessage, previousStage))
			{
				return false;
			}
			if (!AudienceBasedConversationAggregator.SupportsSideConversation(parentMessage))
			{
				return false;
			}
			if (!this.IsSupportedItem(message) || !this.IsSupportedItem(parentMessage))
			{
				return false;
			}
			if (previousStage <= ConversationIndex.FixupStage.H13)
			{
				if (previousStage != ConversationIndex.FixupStage.Unknown && previousStage != ConversationIndex.FixupStage.H13)
				{
					return true;
				}
			}
			else if (previousStage != ConversationIndex.FixupStage.Error && previousStage != ConversationIndex.FixupStage.S1 && previousStage != ConversationIndex.FixupStage.S2)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x00155491 File Offset: 0x00153691
		public bool Aggregate(IConversationAggregationLogger logger, ICorePropertyBag message, IStorePropertyBag parentMessage)
		{
			return this.CheckParticipantsGotRemoved(logger, message, parentMessage);
		}

		// Token: 0x060051CD RID: 20941 RVA: 0x0015549C File Offset: 0x0015369C
		protected virtual bool IsSupportedItem(IStorePropertyBag item)
		{
			return ComplexParticipantExtractorBase<IStorePropertyBag>.CanExtractRecipients(item);
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x001554A4 File Offset: 0x001536A4
		protected virtual bool IsSupportedItem(ICorePropertyBag item)
		{
			return ComplexParticipantExtractorBase<ICorePropertyBag>.CanExtractRecipients(item);
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x001554AC File Offset: 0x001536AC
		private bool CheckParticipantsGotRemoved(IConversationAggregationLogger logger, ICorePropertyBag deliveredItemPropertyBag, IStorePropertyBag parentItemPropertyBag)
		{
			bool result;
			AggregationByParticipantProcessor.DisplayNameCheckResult displayNameCheckResult = this.TryCheckParticipantsRemovedUsingDisplayNames(logger, deliveredItemPropertyBag, parentItemPropertyBag, out result);
			if (displayNameCheckResult == AggregationByParticipantProcessor.DisplayNameCheckResult.Success)
			{
				logger.LogSideConversationProcessingData(displayNameCheckResult.ToString(), false);
				return result;
			}
			logger.LogSideConversationProcessingData(displayNameCheckResult.ToString(), true);
			return this.CheckParticipantsRemovedUsingParticipants(logger, deliveredItemPropertyBag, parentItemPropertyBag);
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x001554F8 File Offset: 0x001536F8
		private AggregationByParticipantProcessor.DisplayNameCheckResult TryCheckParticipantsRemovedUsingDisplayNames(IConversationAggregationLogger logger, ICorePropertyBag deliveredItemPropertyBag, IStorePropertyBag parentItemPropertyBag, out bool participantsRemoved)
		{
			HashSet<string> hashSet = null;
			HashSet<string> hashSet2 = null;
			AggregationByParticipantProcessor.DisplayNameCheckResult displayNameCheckResult = AggregationByParticipantProcessor.DisplayNameCheckResult.Success;
			participantsRemoved = false;
			if (this.ReplyAllExtractor.TryRetrieveReplyAllDisplayNames(parentItemPropertyBag, out hashSet2))
			{
				hashSet = this.ReplyAllExtractor.RetrieveReplyAllDisplayNames(deliveredItemPropertyBag);
				if (hashSet.Count < hashSet2.Count)
				{
					participantsRemoved = true;
					displayNameCheckResult = AggregationByParticipantProcessor.DisplayNameCheckResult.Success;
				}
				else
				{
					hashSet2.ExceptWith(hashSet);
					if (hashSet2.Any<string>())
					{
						displayNameCheckResult = AggregationByParticipantProcessor.DisplayNameCheckResult.DisplayNamesNotMatching;
					}
				}
			}
			else
			{
				displayNameCheckResult = AggregationByParticipantProcessor.DisplayNameCheckResult.DisplayNamesTooLong;
			}
			if (displayNameCheckResult == AggregationByParticipantProcessor.DisplayNameCheckResult.Success && participantsRemoved)
			{
				logger.LogSideConversationProcessingData(hashSet2, hashSet);
			}
			return displayNameCheckResult;
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x00155568 File Offset: 0x00153768
		private bool CheckParticipantsRemovedUsingParticipants(IConversationAggregationLogger logger, ICorePropertyBag deliveredItemPropertyBag, IStorePropertyBag parentItemPropertyBag)
		{
			ParticipantSet participantSet = this.ReplyAllExtractor.RetrieveReplyAllParticipants(deliveredItemPropertyBag);
			ParticipantSet participantSet2 = this.ReplyAllExtractor.RetrieveReplyAllParticipants(parentItemPropertyBag);
			bool flag = !participantSet2.IsSubsetOf(participantSet);
			if (flag)
			{
				logger.LogSideConversationProcessingData(participantSet2, participantSet);
			}
			return flag;
		}

		// Token: 0x04002C76 RID: 11382
		private readonly IReplyAllExtractor replyAllExtractor;

		// Token: 0x0200087D RID: 2173
		private enum DisplayNameCheckResult
		{
			// Token: 0x04002C78 RID: 11384
			Success,
			// Token: 0x04002C79 RID: 11385
			DisplayNamesTooLong,
			// Token: 0x04002C7A RID: 11386
			DisplayNamesNotMatching
		}
	}
}

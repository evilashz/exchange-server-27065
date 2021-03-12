using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000883 RID: 2179
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ConversationAggregatorBase : IConversationAggregator
	{
		// Token: 0x060051E9 RID: 20969 RVA: 0x00156FE0 File Offset: 0x001551E0
		public ConversationAggregatorBase(IAggregationByItemClassReferencesSubjectProcessor referencesProcessor)
		{
			this.referencesProcessor = referencesProcessor;
		}

		// Token: 0x170016D6 RID: 5846
		// (get) Token: 0x060051EA RID: 20970 RVA: 0x00156FEF File Offset: 0x001551EF
		protected IAggregationByItemClassReferencesSubjectProcessor ReferencesProcessor
		{
			get
			{
				return this.referencesProcessor;
			}
		}

		// Token: 0x060051EB RID: 20971
		public abstract ConversationAggregationResult Aggregate(ICoreItem message);

		// Token: 0x060051EC RID: 20972 RVA: 0x00156FF8 File Offset: 0x001551F8
		protected virtual ConversationAggregationResult ConstructResult(ConversationIndex.FixupStage bySubjectResultingStage, ConversationIndex bySubjectResultingIndex, IStorePropertyBag parentItem)
		{
			return new ConversationAggregationResult
			{
				Stage = bySubjectResultingStage,
				ConversationIndex = bySubjectResultingIndex,
				SupportsSideConversation = false,
				ConversationFamilyId = ConversationId.Create(bySubjectResultingIndex)
			};
		}

		// Token: 0x04002C8F RID: 11407
		private readonly IAggregationByItemClassReferencesSubjectProcessor referencesProcessor;
	}
}

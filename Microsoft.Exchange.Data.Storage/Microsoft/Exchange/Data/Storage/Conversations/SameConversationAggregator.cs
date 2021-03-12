using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F69 RID: 3945
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SameConversationAggregator : ConversationAggregatorBase
	{
		// Token: 0x060086F3 RID: 34547 RVA: 0x00250051 File Offset: 0x0024E251
		public SameConversationAggregator(IAggregationByItemClassReferencesSubjectProcessor referenceProcessor) : base(referenceProcessor)
		{
		}

		// Token: 0x060086F4 RID: 34548 RVA: 0x0025005C File Offset: 0x0024E25C
		public override ConversationAggregationResult Aggregate(ICoreItem message)
		{
			IStorePropertyBag parentItem;
			ConversationIndex bySubjectResultingIndex;
			ConversationIndex.FixupStage bySubjectResultingStage;
			base.ReferencesProcessor.Aggregate(message.PropertyBag, false, out parentItem, out bySubjectResultingIndex, out bySubjectResultingStage);
			return this.ConstructResult(bySubjectResultingStage, bySubjectResultingIndex, parentItem);
		}
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x0200087B RID: 2171
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAggregationByParticipantProcessor
	{
		// Token: 0x060051C6 RID: 20934
		bool ShouldAggregate(ICorePropertyBag message, IStorePropertyBag parentMessage, ConversationIndex.FixupStage previousStage);

		// Token: 0x060051C7 RID: 20935
		bool Aggregate(IConversationAggregationLogger logger, ICorePropertyBag message, IStorePropertyBag parentMessage);
	}
}

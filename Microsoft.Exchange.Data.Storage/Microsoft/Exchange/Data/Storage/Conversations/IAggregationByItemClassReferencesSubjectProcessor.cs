using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x0200087E RID: 2174
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAggregationByItemClassReferencesSubjectProcessor
	{
		// Token: 0x060051D2 RID: 20946
		void Aggregate(ICorePropertyBag item, bool shouldSearchForDuplicatedMessage, out IStorePropertyBag parentItem, out ConversationIndex newIndex, out ConversationIndex.FixupStage stage);
	}
}

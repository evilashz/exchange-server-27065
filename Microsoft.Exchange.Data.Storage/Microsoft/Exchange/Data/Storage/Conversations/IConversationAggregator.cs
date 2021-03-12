using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000882 RID: 2178
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationAggregator
	{
		// Token: 0x060051E8 RID: 20968
		ConversationAggregationResult Aggregate(ICoreItem message);
	}
}

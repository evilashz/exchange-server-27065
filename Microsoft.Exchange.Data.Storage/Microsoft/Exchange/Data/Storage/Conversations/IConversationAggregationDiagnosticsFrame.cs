using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000888 RID: 2184
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationAggregationDiagnosticsFrame
	{
		// Token: 0x06005201 RID: 20993
		ConversationAggregationResult TrackAggregation(string operationName, AggregationDelegate aggregation);
	}
}

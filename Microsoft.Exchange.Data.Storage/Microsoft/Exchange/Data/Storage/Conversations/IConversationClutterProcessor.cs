using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008F2 RID: 2290
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationClutterProcessor
	{
		// Token: 0x060055DE RID: 21982
		int Process(bool markAsClutter, IConversationTree conversationTree, List<GroupOperationResult> results);
	}
}

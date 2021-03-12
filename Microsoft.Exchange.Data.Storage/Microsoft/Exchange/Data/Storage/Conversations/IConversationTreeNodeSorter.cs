using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008BC RID: 2236
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationTreeNodeSorter
	{
		// Token: 0x06005316 RID: 21270
		List<IConversationTreeNode> Sort(IConversationTreeNode rootNode, ConversationTreeSortOrder sortOrder);
	}
}

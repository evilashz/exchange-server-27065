using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200002C RID: 44
	internal interface IConversationTree : IEnumerable<IConversationTreeNode>, IEnumerable
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000B3 RID: 179
		ConversationTreeSortOrder SortOrder { get; }
	}
}

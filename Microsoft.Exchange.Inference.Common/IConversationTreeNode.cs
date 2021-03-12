using System;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200002D RID: 45
	internal interface IConversationTreeNode
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B4 RID: 180
		IConversationTreeNode ParentNode { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B5 RID: 181
		IDocument ConversationMessage { get; }
	}
}

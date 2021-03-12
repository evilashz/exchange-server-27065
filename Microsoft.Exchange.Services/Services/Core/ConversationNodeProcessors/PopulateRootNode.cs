using System;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.ConversationNodeProcessors
{
	// Token: 0x020003B4 RID: 948
	internal class PopulateRootNode : IConversationNodeProcessor
	{
		// Token: 0x06001AA4 RID: 6820 RVA: 0x00098603 File Offset: 0x00096803
		public void ProcessNode(IConversationTreeNode node, ConversationNode serviceNode)
		{
			serviceNode.IsRootNode = true;
		}
	}
}

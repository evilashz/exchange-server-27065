using System;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.ConversationNodeProcessors
{
	// Token: 0x020003AE RID: 942
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IConversationNodeProcessor
	{
		// Token: 0x06001A95 RID: 6805
		void ProcessNode(IConversationTreeNode node, ConversationNode serviceNode);
	}
}

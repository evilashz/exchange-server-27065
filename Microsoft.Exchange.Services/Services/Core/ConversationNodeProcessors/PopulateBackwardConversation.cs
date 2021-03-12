using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.ConversationNodeProcessors
{
	// Token: 0x020003AF RID: 943
	internal class PopulateBackwardConversation : IConversationNodeProcessor
	{
		// Token: 0x06001A96 RID: 6806 RVA: 0x0009830A File Offset: 0x0009650A
		public PopulateBackwardConversation(IStorePropertyBag backwardLink, IModernConversationNodeFactory conversationNodeFactory)
		{
			this.backwardLink = backwardLink;
			this.conversationNodeFactory = conversationNodeFactory;
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00098320 File Offset: 0x00096520
		public void ProcessNode(IConversationTreeNode node, ConversationNode serviceNode)
		{
			if (this.backwardLink == null)
			{
				return;
			}
			IRelatedItemInfo itemInfo;
			if (this.conversationNodeFactory.TryLoadRelatedItemInfo(this.backwardLink, out itemInfo))
			{
				serviceNode.BackwardMessage = BreadcrumbAdapterType.FromRelatedItemInfo(itemInfo);
			}
		}

		// Token: 0x04001182 RID: 4482
		private readonly IStorePropertyBag backwardLink;

		// Token: 0x04001183 RID: 4483
		private readonly IModernConversationNodeFactory conversationNodeFactory;
	}
}

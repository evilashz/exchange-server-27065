using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.ConversationNodeProcessors
{
	// Token: 0x020003B0 RID: 944
	internal class PopulateForwardConversations : IConversationNodeProcessor
	{
		// Token: 0x06001A98 RID: 6808 RVA: 0x00098357 File Offset: 0x00096557
		public PopulateForwardConversations(Dictionary<StoreObjectId, List<IStorePropertyBag>> forwardLinks, IModernConversationNodeFactory conversationNodeFactory)
		{
			this.forwardLinks = forwardLinks;
			this.conversationNodeFactory = conversationNodeFactory;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x00098370 File Offset: 0x00096570
		public void ProcessNode(IConversationTreeNode node, ConversationNode serviceNode)
		{
			if (this.forwardLinks == null)
			{
				return;
			}
			List<IStorePropertyBag> list;
			if (this.forwardLinks.TryGetValue(node.MainStoreObjectId, out list))
			{
				List<BreadcrumbAdapterType> list2 = new List<BreadcrumbAdapterType>();
				foreach (IStorePropertyBag storePropertyBag in list)
				{
					IRelatedItemInfo itemInfo;
					if (this.conversationNodeFactory.TryLoadRelatedItemInfo(storePropertyBag, out itemInfo))
					{
						list2.Add(BreadcrumbAdapterType.FromRelatedItemInfo(itemInfo));
					}
				}
				serviceNode.ForwardMessages = list2.ToArray();
			}
		}

		// Token: 0x04001184 RID: 4484
		private readonly Dictionary<StoreObjectId, List<IStorePropertyBag>> forwardLinks;

		// Token: 0x04001185 RID: 4485
		private readonly IModernConversationNodeFactory conversationNodeFactory;
	}
}

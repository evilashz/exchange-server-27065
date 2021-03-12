using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.Conversations;

namespace Microsoft.Exchange.Services.Core.Conversations.LoadingListBuilders
{
	// Token: 0x020003A5 RID: 933
	internal class ThreadedConversationNodeLoadingListBuilder : ConversationNodeLoadingListBuilderBase
	{
		// Token: 0x06001A3A RID: 6714 RVA: 0x0009699B File Offset: 0x00094B9B
		public ThreadedConversationNodeLoadingListBuilder(IEnumerable<IConversationTreeNode> allNodes, ConversationRequestArguments requestArguments) : base(allNodes)
		{
			this.requestArguments = requestArguments;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000969AC File Offset: 0x00094BAC
		protected override void MarkDependentNodesToBeLoaded()
		{
			List<IConversationTreeNode> list = base.LoadingList.NotToBeLoaded.ToList<IConversationTreeNode>();
			foreach (IConversationTreeNode treeNode in list)
			{
				base.LoadingList.MarkToBeLoaded(treeNode);
			}
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00096A10 File Offset: 0x00094C10
		protected override void MarkNodesToBeIgnored()
		{
			IConversationTreeNode[] array = base.LoadingList.ToBeLoaded.ToArray<IConversationTreeNode>();
			foreach (IConversationTreeNode conversationTreeNode in array)
			{
				if (base.ShouldIgnore(conversationTreeNode, this.requestArguments.ReturnSubmittedItems))
				{
					base.LoadingList.MarkToBeIgnored(conversationTreeNode);
				}
			}
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x00096A62 File Offset: 0x00094C62
		protected override void MarkNodesForProcessorsToBeLoaded()
		{
		}

		// Token: 0x04001160 RID: 4448
		private readonly ConversationRequestArguments requestArguments;
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Conversations;

namespace Microsoft.Exchange.Services.Core.Conversations.LoadingListBuilders
{
	// Token: 0x020003A2 RID: 930
	internal abstract class ConversationNodeLoadingListBuilderBase
	{
		// Token: 0x06001A26 RID: 6694 RVA: 0x0009667A File Offset: 0x0009487A
		protected ConversationNodeLoadingListBuilderBase(IEnumerable<IConversationTreeNode> allNodes)
		{
			this.LoadingList = new ConversationNodeLoadingList(allNodes);
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0009668E File Offset: 0x0009488E
		public ConversationNodeLoadingList Build()
		{
			this.MarkDependentNodesToBeLoaded();
			this.MarkNodesToBeIgnored();
			this.MarkNodesForProcessorsToBeLoaded();
			return this.LoadingList;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x000966A8 File Offset: 0x000948A8
		// (set) Token: 0x06001A29 RID: 6697 RVA: 0x000966B0 File Offset: 0x000948B0
		private protected ConversationNodeLoadingList LoadingList { protected get; private set; }

		// Token: 0x06001A2A RID: 6698
		protected abstract void MarkDependentNodesToBeLoaded();

		// Token: 0x06001A2B RID: 6699
		protected abstract void MarkNodesToBeIgnored();

		// Token: 0x06001A2C RID: 6700
		protected abstract void MarkNodesForProcessorsToBeLoaded();

		// Token: 0x06001A2D RID: 6701 RVA: 0x000966B9 File Offset: 0x000948B9
		protected bool ShouldIgnore(IConversationTreeNode node, bool returnSubmittedItems)
		{
			return !node.HasData || (!returnSubmittedItems && node.HasBeenSubmitted);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.Conversations;

namespace Microsoft.Exchange.Services.Core.Conversations.LoadingListBuilders
{
	// Token: 0x020003A3 RID: 931
	internal class ConversationNodeDiagnosticsLoadingListBuilder : ConversationNodeLoadingListBuilderBase
	{
		// Token: 0x06001A2E RID: 6702 RVA: 0x000966D0 File Offset: 0x000948D0
		public ConversationNodeDiagnosticsLoadingListBuilder(IEnumerable<IConversationTreeNode> allNodes) : base(allNodes)
		{
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000966DC File Offset: 0x000948DC
		protected override void MarkDependentNodesToBeLoaded()
		{
			List<IConversationTreeNode> list = base.LoadingList.NotToBeLoaded.ToList<IConversationTreeNode>();
			foreach (IConversationTreeNode treeNode in list)
			{
				base.LoadingList.MarkToBeLoaded(treeNode);
			}
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00096740 File Offset: 0x00094940
		protected override void MarkNodesToBeIgnored()
		{
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00096742 File Offset: 0x00094942
		protected override void MarkNodesForProcessorsToBeLoaded()
		{
		}
	}
}

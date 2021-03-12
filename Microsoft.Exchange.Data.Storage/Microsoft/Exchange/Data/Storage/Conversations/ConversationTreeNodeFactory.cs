using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008C7 RID: 2247
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationTreeNodeFactory
	{
		// Token: 0x06005375 RID: 21365 RVA: 0x0015B7E8 File Offset: 0x001599E8
		public ConversationTreeNodeFactory() : this(ConversationTreeNodeFactory.DefaultTreeNodeIndexPropertyDefinition)
		{
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x0015B7F5 File Offset: 0x001599F5
		public ConversationTreeNodeFactory(PropertyDefinition indexPropertyDefinition)
		{
			this.indexPropertyDefinition = indexPropertyDefinition;
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x0015B804 File Offset: 0x00159A04
		public ConversationTreeNode CreateInstance(List<IStorePropertyBag> storePropertyBags)
		{
			return new ConversationTreeNode(this.indexPropertyDefinition, storePropertyBags, TraversalChronologicalNodeSorter.Instance);
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x0015B817 File Offset: 0x00159A17
		public ConversationTreeRootNode CreateRootNode()
		{
			return new ConversationTreeRootNode(TraversalChronologicalNodeSorter.Instance);
		}

		// Token: 0x04002D5D RID: 11613
		public static PropertyDefinition DefaultTreeNodeIndexPropertyDefinition = ItemSchema.ConversationIndex;

		// Token: 0x04002D5E RID: 11614
		public static PropertyDefinition ConversationFamilyTreeNodeIndexPropertyDefinition = ItemSchema.ConversationFamilyIndex;

		// Token: 0x04002D5F RID: 11615
		private readonly PropertyDefinition indexPropertyDefinition;
	}
}

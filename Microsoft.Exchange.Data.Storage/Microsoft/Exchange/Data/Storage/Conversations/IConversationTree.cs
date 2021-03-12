using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008BB RID: 2235
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationTree : ICollection<IConversationTreeNode>, IEnumerable<IConversationTreeNode>, IEnumerable
	{
		// Token: 0x06005309 RID: 21257
		bool TryGetConversationTreeNode(StoreObjectId storeObjectId, out IConversationTreeNode conversationTreeNode);

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x0600530A RID: 21258
		string Topic { get; }

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x0600530B RID: 21259
		byte[] ConversationCreatorSID { get; }

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x0600530C RID: 21260
		EffectiveRights EffectiveRights { get; }

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x0600530D RID: 21261
		IConversationTreeNode RootMessageNode { get; }

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x0600530E RID: 21262
		StoreObjectId RootMessageId { get; }

		// Token: 0x0600530F RID: 21263
		int GetNodeCount(bool includeSubmitted);

		// Token: 0x06005310 RID: 21264
		void Sort(ConversationTreeSortOrder sortOrder);

		// Token: 0x06005311 RID: 21265
		void ExecuteSortedAction(ConversationTreeSortOrder sortOrder, SortedActionDelegate action);

		// Token: 0x06005312 RID: 21266
		bool IsPropertyLoaded(PropertyDefinition propertyDefinition);

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06005313 RID: 21267
		IEnumerable<IStorePropertyBag> StorePropertyBags { get; }

		// Token: 0x06005314 RID: 21268
		Dictionary<IConversationTreeNode, IConversationTreeNode> BuildPreviousNodeGraph();

		// Token: 0x06005315 RID: 21269
		T GetValueOrDefault<T>(StoreObjectId itemId, PropertyDefinition propertyDefinition, T defaultValue = default(T));
	}
}

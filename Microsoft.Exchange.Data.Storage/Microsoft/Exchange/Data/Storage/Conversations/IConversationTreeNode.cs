using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008C8 RID: 2248
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationTreeNode : IEnumerable<IConversationTreeNode>, IEnumerable, IComparable<IConversationTreeNode>
	{
		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x0600537A RID: 21370
		IList<IConversationTreeNode> ChildNodes { get; }

		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x0600537B RID: 21371
		ExDateTime? ReceivedTime { get; }

		// Token: 0x0600537C RID: 21372
		bool TryAddChild(IConversationTreeNode node);

		// Token: 0x0600537D RID: 21373
		void AddChild(IConversationTreeNode node);

		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x0600537E RID: 21374
		byte[] Index { get; }

		// Token: 0x0600537F RID: 21375
		bool TryGetPropertyBag(StoreObjectId itemId, out IStorePropertyBag bag);

		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06005380 RID: 21376
		IStorePropertyBag MainPropertyBag { get; }

		// Token: 0x06005381 RID: 21377
		ConversationTreeNodeRelation GetRelationTo(IConversationTreeNode otherNode);

		// Token: 0x06005382 RID: 21378
		void SortChildNodes(ConversationTreeSortOrder sortOrder);

		// Token: 0x06005383 RID: 21379
		bool UpdatePropertyBag(StoreObjectId itemId, IStorePropertyBag bag);

		// Token: 0x06005384 RID: 21380
		T GetValueOrDefault<T>(StoreObjectId itemId, PropertyDefinition propertyDefinition, T defaultValue = default(T));

		// Token: 0x17001739 RID: 5945
		// (get) Token: 0x06005385 RID: 21381
		bool HasChildren { get; }

		// Token: 0x1700173A RID: 5946
		// (get) Token: 0x06005386 RID: 21382
		ConversationId ConversationId { get; }

		// Token: 0x1700173B RID: 5947
		// (get) Token: 0x06005387 RID: 21383
		ConversationId ConversationThreadId { get; }

		// Token: 0x1700173C RID: 5948
		// (get) Token: 0x06005388 RID: 21384
		// (set) Token: 0x06005389 RID: 21385
		IConversationTreeNode ParentNode { get; set; }

		// Token: 0x1700173D RID: 5949
		// (get) Token: 0x0600538A RID: 21386
		// (set) Token: 0x0600538B RID: 21387
		ConversationTreeSortOrder SortOrder { get; set; }

		// Token: 0x0600538C RID: 21388
		void ApplyActionToChild(Action<IConversationTreeNode> action);

		// Token: 0x1700173E RID: 5950
		// (get) Token: 0x0600538D RID: 21389
		bool IsSpecificMessageReplyStamped { get; }

		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x0600538E RID: 21390
		bool IsSpecificMessageReply { get; }

		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x0600538F RID: 21391
		bool HasBeenSubmitted { get; }

		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x06005390 RID: 21392
		StoreObjectId MainStoreObjectId { get; }

		// Token: 0x17001742 RID: 5954
		// (get) Token: 0x06005391 RID: 21393
		bool HasData { get; }

		// Token: 0x06005392 RID: 21394
		bool IsPartOf(StoreObjectId itemId);

		// Token: 0x06005393 RID: 21395
		List<StoreObjectId> ToListStoreObjectId();

		// Token: 0x06005394 RID: 21396
		T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue = default(T));

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x06005395 RID: 21397
		bool HasAttachments { get; }

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06005396 RID: 21398
		List<IStorePropertyBag> StorePropertyBags { get; }
	}
}

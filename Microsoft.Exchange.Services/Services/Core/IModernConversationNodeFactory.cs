using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020003BD RID: 957
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IModernConversationNodeFactory
	{
		// Token: 0x06001AEB RID: 6891
		ConversationNode CreateInstance(IConversationTreeNode treeNode);

		// Token: 0x06001AEC RID: 6892
		bool TryLoadRelatedItemInfo(IConversationTreeNode treeNode, out IRelatedItemInfo relatedItem);

		// Token: 0x06001AED RID: 6893
		bool TryLoadRelatedItemInfo(IStorePropertyBag storePropertyBag, out IRelatedItemInfo relatedItem);
	}
}

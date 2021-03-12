using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008BD RID: 2237
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IBreadcrumbsSource
	{
		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06005317 RID: 21271
		IStorePropertyBag BackwardMessagePropertyBag { get; }

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06005318 RID: 21272
		Dictionary<StoreObjectId, List<IStorePropertyBag>> ForwardConversationRootMessagePropertyBags { get; }
	}
}

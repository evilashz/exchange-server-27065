using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008D2 RID: 2258
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationMembersQuery
	{
		// Token: 0x06005412 RID: 21522
		List<IStorePropertyBag> Query(ConversationId conversationId, ICollection<PropertyDefinition> headerPropertyDefinition, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList);

		// Token: 0x06005413 RID: 21523
		Dictionary<object, List<IStorePropertyBag>> AggregateMembersPerField(PropertyDefinition field, object defaultValue, List<IStorePropertyBag> members);
	}
}

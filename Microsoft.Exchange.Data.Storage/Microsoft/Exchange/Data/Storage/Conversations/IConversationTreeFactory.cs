using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008C5 RID: 2245
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationTreeFactory
	{
		// Token: 0x06005367 RID: 21351
		IConversationTree Create(IEnumerable<IStorePropertyBag> queryResult, IEnumerable<PropertyDefinition> propertyDefinitions);

		// Token: 0x06005368 RID: 21352
		IConversationTree GetNewestSubTree(IConversationTree conversationTree, int count);

		// Token: 0x06005369 RID: 21353
		HashSet<PropertyDefinition> CalculatePropertyDefinitionsToBeLoaded(ICollection<PropertyDefinition> requestedProperties);
	}
}

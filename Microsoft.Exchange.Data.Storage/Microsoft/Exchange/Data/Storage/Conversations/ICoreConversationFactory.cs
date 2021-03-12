using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000877 RID: 2167
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICoreConversationFactory<out T> where T : ICoreConversation
	{
		// Token: 0x060051A4 RID: 20900
		T CreateConversation(ConversationId conversationId, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, bool isIrmEnabled, params PropertyDefinition[] propertyDefinitions);

		// Token: 0x060051A5 RID: 20901
		T CreateConversation(ConversationId conversationId, IList<StoreObjectId> folderIds, bool useFolderIdsAsExclusionList, bool isIrmEnabled, bool isSmimeSupported, string domainName, params PropertyDefinition[] propertyDefinitions);

		// Token: 0x060051A6 RID: 20902
		T CreateConversation(ConversationId conversationId, params PropertyDefinition[] requestedProperties);
	}
}

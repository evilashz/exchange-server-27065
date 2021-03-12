using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F63 RID: 3939
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationDataExtractorFactory
	{
		// Token: 0x060086CF RID: 34511
		ConversationDataExtractor Create(bool isIrmEnabled, HashSet<PropertyDefinition> propertiesLoadedForExtractedItems, ConversationId conversationId, IConversationTree tree);

		// Token: 0x060086D0 RID: 34512
		ConversationDataExtractor Create(bool isIrmEnabled, HashSet<PropertyDefinition> propertiesLoadedForExtractedItems, ConversationId conversationId, IConversationTree tree, bool isSmimeSupported, string domainName);
	}
}

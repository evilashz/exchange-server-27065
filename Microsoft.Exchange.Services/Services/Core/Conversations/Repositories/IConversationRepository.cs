using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Conversations.Repositories
{
	// Token: 0x020003A6 RID: 934
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IConversationRepository<out T> where T : ICoreConversation
	{
		// Token: 0x06001A3E RID: 6718
		T Load(ConversationId conversationId, IMailboxSession mailboxSession, BaseFolderId[] folderIds, bool useFolderIdsAsExclusionList = true, bool additionalPropertiesOnLoadItemParts = true, params PropertyDefinition[] requestedProperties);

		// Token: 0x06001A3F RID: 6719
		void PrefetchAndLoadItemParts(IMailboxSession mailboxSession, ICoreConversation conversation, HashSet<IConversationTreeNode> nodesToLoadItemPart, bool isSyncScenario);
	}
}

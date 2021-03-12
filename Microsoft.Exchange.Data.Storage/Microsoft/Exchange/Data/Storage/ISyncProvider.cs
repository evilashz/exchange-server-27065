using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000B1 RID: 177
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ISyncProvider : IDisposeTrackable, IDisposable
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000BF6 RID: 3062
		ISyncLogger SyncLogger { get; }

		// Token: 0x06000BF7 RID: 3063
		ISyncItem GetItem(ISyncItemId id, params PropertyDefinition[] prefetchProperties);

		// Token: 0x06000BF8 RID: 3064
		ISyncWatermark CreateNewWatermark();

		// Token: 0x06000BF9 RID: 3065
		void BindToFolderSync(FolderSync folderSync);

		// Token: 0x06000BFA RID: 3066
		bool GetNewOperations(ISyncWatermark minWatermark, ISyncWatermark maxSyncWatermark, bool enumerateDeletes, int numOperations, QueryFilter filter, Dictionary<ISyncItemId, ServerManifestEntry> newServerManifest);

		// Token: 0x06000BFB RID: 3067
		void DisposeNewOperationsCachedData();

		// Token: 0x06000BFC RID: 3068
		ISyncWatermark GetMaxItemWatermark(ISyncWatermark currentWatermark);

		// Token: 0x06000BFD RID: 3069
		OperationResult DeleteItems(params ISyncItemId[] syncIds);

		// Token: 0x06000BFE RID: 3070
		List<IConversationTreeNode> GetInFolderItemsForConversation(ConversationId conversationId);

		// Token: 0x06000BFF RID: 3071
		ISyncItemId CreateISyncItemIdForNewItem(StoreObjectId itemId);
	}
}

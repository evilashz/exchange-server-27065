using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008C1 RID: 2241
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICoreConversation
	{
		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x06005332 RID: 21298
		StoreObjectId RootMessageId { get; }

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x06005333 RID: 21299
		IConversationTreeNode RootMessageNode { get; }

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06005334 RID: 21300
		IConversationTree ConversationTree { get; }

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06005335 RID: 21301
		ConversationId ConversationId { get; }

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x06005336 RID: 21302
		IConversationStatistics ConversationStatistics { get; }

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x06005337 RID: 21303
		string Topic { get; }

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x06005338 RID: 21304
		byte[] SerializedTreeState { get; }

		// Token: 0x06005339 RID: 21305
		byte[] GetSerializedTreeStateWithNodesToExclude(ICollection<IConversationTreeNode> nodesToExclude);

		// Token: 0x0600533A RID: 21306
		void LoadBodySummaries();

		// Token: 0x0600533B RID: 21307
		void LoadItemParts(ICollection<IConversationTreeNode> nodes);

		// Token: 0x0600533C RID: 21308
		KeyValuePair<List<StoreObjectId>, List<StoreObjectId>> CalculateChanges(byte[] olderState);

		// Token: 0x0600533D RID: 21309
		ItemPart GetItemPart(StoreObjectId itemId);

		// Token: 0x0600533E RID: 21310
		ParticipantSet AllParticipants(ICollection<IConversationTreeNode> loadedNodes = null);

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600533F RID: 21311
		// (remove) Token: 0x06005340 RID: 21312
		event OnBeforeItemLoadEventDelegate OnBeforeItemLoad;

		// Token: 0x06005341 RID: 21313
		List<StoreObjectId> GetMessageIdsForPreread();
	}
}

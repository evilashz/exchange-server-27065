using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008BE RID: 2238
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationData
	{
		// Token: 0x06005319 RID: 21273
		int GetNodeCount(bool includeSubmitted);

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x0600531A RID: 21274
		IConversationTreeNode FirstNode { get; }

		// Token: 0x0600531B RID: 21275
		Dictionary<IConversationTreeNode, ParticipantSet> LoadAddedParticipants();

		// Token: 0x0600531C RID: 21276
		ParticipantTable LoadReplyAllParticipantsPerType();

		// Token: 0x0600531D RID: 21277
		ParticipantSet LoadReplyAllParticipants(IConversationTreeNode node);

		// Token: 0x0600531E RID: 21278
		Dictionary<IConversationTreeNode, IConversationTreeNode> BuildPreviousNodeGraph();

		// Token: 0x0600531F RID: 21279
		IConversationTree GetNewestSubTree(int count);
	}
}

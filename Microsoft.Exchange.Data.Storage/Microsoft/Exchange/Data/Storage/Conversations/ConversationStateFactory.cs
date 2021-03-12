using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008CF RID: 2255
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ConversationStateFactory
	{
		// Token: 0x060053FB RID: 21499 RVA: 0x0015C794 File Offset: 0x0015A994
		internal ConversationStateFactory(IMailboxSession session, IConversationTree conversationTree)
		{
			this.session = session;
			this.conversationTree = conversationTree;
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x0015C7AA File Offset: 0x0015A9AA
		public ConversationState Create(ICollection<IConversationTreeNode> nodesToExclude = null)
		{
			return new ConversationState(this.session, this.conversationTree, nodesToExclude);
		}

		// Token: 0x04002D72 RID: 11634
		private readonly IMailboxSession session;

		// Token: 0x04002D73 RID: 11635
		private readonly IConversationTree conversationTree;
	}
}

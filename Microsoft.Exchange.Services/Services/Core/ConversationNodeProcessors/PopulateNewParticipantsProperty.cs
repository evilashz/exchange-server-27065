using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.ConversationNodeProcessors
{
	// Token: 0x020003B5 RID: 949
	internal class PopulateNewParticipantsProperty : IConversationNodeProcessor
	{
		// Token: 0x06001AA6 RID: 6822 RVA: 0x00098614 File Offset: 0x00096814
		public PopulateNewParticipantsProperty(Dictionary<IConversationTreeNode, ParticipantSet> newParticipantsPerNode, IParticipantResolver resolver)
		{
			this.newParticipantsPerNode = newParticipantsPerNode;
			this.resolver = resolver;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0009862C File Offset: 0x0009682C
		public void ProcessNode(IConversationTreeNode node, ConversationNode serviceNode)
		{
			ParticipantSet participants;
			if (this.newParticipantsPerNode.TryGetValue(node, out participants))
			{
				serviceNode.NewParticipants = this.resolver.ResolveToEmailAddressWrapper(participants);
			}
		}

		// Token: 0x0400118D RID: 4493
		private readonly Dictionary<IConversationTreeNode, ParticipantSet> newParticipantsPerNode;

		// Token: 0x0400118E RID: 4494
		private readonly IParticipantResolver resolver;
	}
}

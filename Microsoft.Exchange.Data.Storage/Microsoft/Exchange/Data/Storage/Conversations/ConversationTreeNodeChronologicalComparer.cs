using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008CB RID: 2251
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationTreeNodeChronologicalComparer : IComparer<IConversationTreeNode>
	{
		// Token: 0x060053CB RID: 21451 RVA: 0x0015C0E0 File Offset: 0x0015A2E0
		public int Compare(IConversationTreeNode left, IConversationTreeNode right)
		{
			ExDateTime? receivedTime = left.ReceivedTime;
			ExDateTime? receivedTime2 = right.ReceivedTime;
			if (receivedTime == null && receivedTime2 == null)
			{
				return 0;
			}
			if (receivedTime == null)
			{
				return 1;
			}
			if (receivedTime2 == null)
			{
				return -1;
			}
			return receivedTime.Value.CompareTo(receivedTime2.Value);
		}

		// Token: 0x04002D68 RID: 11624
		public static ConversationTreeNodeChronologicalComparer Default = new ConversationTreeNodeChronologicalComparer();
	}
}

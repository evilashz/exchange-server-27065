using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008CA RID: 2250
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversationTreeNodeEqualityComparer : IEqualityComparer<IConversationTreeNode>
	{
		// Token: 0x060053C7 RID: 21447 RVA: 0x0015C07C File Offset: 0x0015A27C
		public int GetHashCode(IConversationTreeNode obj)
		{
			if (obj != null && obj.MainStoreObjectId != null)
			{
				return obj.MainStoreObjectId.GetHashCode();
			}
			return 0;
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x0015C096 File Offset: 0x0015A296
		public bool Equals(IConversationTreeNode x, IConversationTreeNode y)
		{
			return object.ReferenceEquals(x, y) || (x != null && y != null && x.MainStoreObjectId != null && y.MainStoreObjectId != null && x.MainStoreObjectId.Equals(y.MainStoreObjectId));
		}

		// Token: 0x04002D67 RID: 11623
		public static readonly ConversationTreeNodeEqualityComparer Default = new ConversationTreeNodeEqualityComparer();
	}
}

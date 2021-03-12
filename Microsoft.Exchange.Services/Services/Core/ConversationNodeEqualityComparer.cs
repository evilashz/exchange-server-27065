using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200009D RID: 157
	internal class ConversationNodeEqualityComparer : IEqualityComparer<ConversationNode>
	{
		// Token: 0x060003AD RID: 941 RVA: 0x00012A48 File Offset: 0x00010C48
		public bool Equals(ConversationNode x, ConversationNode y)
		{
			if (x.InternetMessageId == y.InternetMessageId)
			{
				return x.ParentInternetMessageId == y.ParentInternetMessageId;
			}
			return x.InternetMessageId != null && y.InternetMessageId != null && x.InternetMessageId == y.InternetMessageId;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00012A9D File Offset: 0x00010C9D
		public int GetHashCode(ConversationNode obj)
		{
			return obj.GetHashCode();
		}
	}
}

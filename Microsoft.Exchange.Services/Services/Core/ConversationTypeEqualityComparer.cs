using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200009C RID: 156
	internal class ConversationTypeEqualityComparer : IEqualityComparer<ConversationType>
	{
		// Token: 0x060003AA RID: 938 RVA: 0x000129FD File Offset: 0x00010BFD
		public bool Equals(ConversationType x, ConversationType y)
		{
			return x.ConversationId != null && y.ConversationId != null && IdConverter.EwsIdToConversationId(x.ConversationId.Id).Equals(IdConverter.EwsIdToConversationId(y.ConversationId.Id));
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00012A36 File Offset: 0x00010C36
		public int GetHashCode(ConversationType obj)
		{
			return obj.GetHashCode();
		}
	}
}

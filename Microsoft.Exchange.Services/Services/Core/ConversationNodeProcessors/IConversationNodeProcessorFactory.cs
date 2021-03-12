using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.ConversationNodeProcessors
{
	// Token: 0x020003B2 RID: 946
	internal interface IConversationNodeProcessorFactory
	{
		// Token: 0x06001A9E RID: 6814
		IEnumerable<IConversationNodeProcessor> CreateNormalNodeProcessors();

		// Token: 0x06001A9F RID: 6815
		IEnumerable<IConversationNodeProcessor> CreateRootNodeProcessors();
	}
}

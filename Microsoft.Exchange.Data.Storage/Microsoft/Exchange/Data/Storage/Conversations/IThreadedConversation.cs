using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008C2 RID: 2242
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IThreadedConversation : ICoreConversation
	{
		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x06005342 RID: 21314
		IEnumerable<IConversationThread> Threads { get; }
	}
}

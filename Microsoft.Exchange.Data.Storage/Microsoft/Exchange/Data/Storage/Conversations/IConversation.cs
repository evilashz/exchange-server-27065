using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008D5 RID: 2261
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversation : ICoreConversation, IConversationData
	{
		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x0600542B RID: 21547
		byte[] ConversationCreatorSID { get; }

		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x0600542C RID: 21548
		EffectiveRights EffectiveRights { get; }
	}
}

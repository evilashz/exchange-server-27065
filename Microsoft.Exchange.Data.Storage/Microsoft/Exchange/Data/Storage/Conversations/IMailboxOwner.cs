using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F6E RID: 3950
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMailboxOwner
	{
		// Token: 0x060086FF RID: 34559
		bool SentToMySelf(ICorePropertyBag item);

		// Token: 0x170023C0 RID: 9152
		// (get) Token: 0x06008700 RID: 34560
		bool SideConversationProcessingEnabled { get; }

		// Token: 0x170023C1 RID: 9153
		// (get) Token: 0x06008701 RID: 34561
		bool ThreadedConversationProcessingEnabled { get; }

		// Token: 0x170023C2 RID: 9154
		// (get) Token: 0x06008702 RID: 34562
		bool ModernConversationPreparationEnabled { get; }

		// Token: 0x170023C3 RID: 9155
		// (get) Token: 0x06008703 RID: 34563
		bool SearchDuplicatedMessagesEnabled { get; }

		// Token: 0x170023C4 RID: 9156
		// (get) Token: 0x06008704 RID: 34564
		bool RequestExtraPropertiesWhenSearching { get; }

		// Token: 0x170023C5 RID: 9157
		// (get) Token: 0x06008705 RID: 34565
		bool IsGroupMailbox { get; }
	}
}

using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008C0 RID: 2240
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConversationThread : IBreadcrumbsSource, IConversationData, IThreadAggregatedProperties
	{
		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x0600532F RID: 21295
		StoreObjectId RootMessageId { get; }

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06005330 RID: 21296
		IConversationTree Tree { get; }

		// Token: 0x06005331 RID: 21297
		void SyncThread();
	}
}

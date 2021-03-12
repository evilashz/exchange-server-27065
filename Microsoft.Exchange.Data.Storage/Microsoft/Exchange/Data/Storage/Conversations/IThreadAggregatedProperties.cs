using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x020008BF RID: 2239
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IThreadAggregatedProperties
	{
		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06005320 RID: 21280
		string Preview { get; }

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06005321 RID: 21281
		ConversationId ThreadId { get; }

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06005322 RID: 21282
		ExDateTime? LastDeliveryTime { get; }

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06005323 RID: 21283
		Participant[] UniqueSenders { get; }

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x06005324 RID: 21284
		StoreObjectId[] ItemIds { get; }

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x06005325 RID: 21285
		StoreObjectId[] DraftItemIds { get; }

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x06005326 RID: 21286
		int ItemCount { get; }

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06005327 RID: 21287
		bool HasAttachments { get; }

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x06005328 RID: 21288
		bool HasIrm { get; }

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x06005329 RID: 21289
		Importance Importance { get; }

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x0600532A RID: 21290
		IconIndex IconIndex { get; }

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x0600532B RID: 21291
		FlagStatus FlagStatus { get; }

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x0600532C RID: 21292
		int UnreadCount { get; }

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x0600532D RID: 21293
		short[] RichContent { get; }

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x0600532E RID: 21294
		string[] ItemClasses { get; }
	}
}

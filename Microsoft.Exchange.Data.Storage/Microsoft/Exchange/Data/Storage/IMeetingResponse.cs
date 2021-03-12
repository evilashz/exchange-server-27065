using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003C6 RID: 966
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMeetingResponse : IMeetingMessageInstance, IMeetingMessage, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06002BD3 RID: 11219
		ResponseType ResponseType { get; }

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06002BD4 RID: 11220
		StoreObjectId AssociatedMeetingRequestId { get; }

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06002BD5 RID: 11221
		ExDateTime AttendeeCriticalChangeTime { get; }

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06002BD6 RID: 11222
		// (set) Token: 0x06002BD7 RID: 11223
		string Location { get; set; }

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06002BD8 RID: 11224
		ExDateTime ProposedStart { get; }

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06002BD9 RID: 11225
		ExDateTime ProposedEnd { get; }
	}
}

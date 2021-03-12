using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001A6 RID: 422
	internal interface ILockableItem : IQueueItem
	{
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001212 RID: 4626
		AccessToken AccessToken { get; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001213 RID: 4627
		WaitCondition CurrentCondition { get; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001214 RID: 4628
		// (set) Token: 0x06001215 RID: 4629
		DateTimeOffset LockExpirationTime { get; set; }

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001216 RID: 4630
		// (set) Token: 0x06001217 RID: 4631
		ThrottlingContext ThrottlingContext { get; set; }

		// Token: 0x06001218 RID: 4632
		WaitCondition GetCondition();
	}
}

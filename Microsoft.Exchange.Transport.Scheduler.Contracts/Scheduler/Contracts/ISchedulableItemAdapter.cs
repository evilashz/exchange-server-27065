using System;

namespace Microsoft.Exchange.Transport.Scheduler.Contracts
{
	// Token: 0x02000009 RID: 9
	internal interface ISchedulableItemAdapter<TScheduluableMessage>
	{
		// Token: 0x06000015 RID: 21
		ISchedulableMessage FromItem(TScheduluableMessage item);

		// Token: 0x06000016 RID: 22
		TScheduluableMessage ToItem(ISchedulableMessage message);
	}
}

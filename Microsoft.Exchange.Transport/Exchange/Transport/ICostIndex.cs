using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000333 RID: 819
	internal interface ICostIndex
	{
		// Token: 0x06002366 RID: 9062
		void Add(WaitCondition waitCondition);

		// Token: 0x06002367 RID: 9063
		WaitCondition[] TryRemove(bool allowAboveThreshold, object state);
	}
}

using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200031A RID: 794
	// (Invoke) Token: 0x06002243 RID: 8771
	internal delegate void ItemRelocked(IQueueItem item, string lockReason, out WaitCondition condition);
}

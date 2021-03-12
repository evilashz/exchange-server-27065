using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000311 RID: 785
	internal interface IQueueVisitor
	{
		// Token: 0x0600221F RID: 8735
		void ForEach(Action<IQueueItem> action, bool includeDeferred);
	}
}

using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F51 RID: 3921
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IQueue<T> : IDisposable
	{
		// Token: 0x06008688 RID: 34440
		void Send(T data);

		// Token: 0x06008689 RID: 34441
		IQueueMessage<T> GetNext(int timeoutInMilliseconds, CancellationToken cancel);

		// Token: 0x0600868A RID: 34442
		void Commit(IQueueMessage<T> message);

		// Token: 0x0600868B RID: 34443
		void Rollback(IQueueMessage<T> message);
	}
}

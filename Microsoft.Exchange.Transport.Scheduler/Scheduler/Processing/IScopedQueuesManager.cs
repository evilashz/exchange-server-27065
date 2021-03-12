using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000015 RID: 21
	internal interface IScopedQueuesManager
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000056 RID: 86
		IDictionary<IMessageScope, ScopedQueue> ScopedQueue { get; }

		// Token: 0x06000057 RID: 87
		void Add(ISchedulableMessage message, IMessageScope throttledScope);

		// Token: 0x06000058 RID: 88
		bool IsAlreadyScoped(IEnumerable<IMessageScope> scopes, out IMessageScope throttledScope);

		// Token: 0x06000059 RID: 89
		bool TryGetNext(out ISchedulableMessage message);
	}
}

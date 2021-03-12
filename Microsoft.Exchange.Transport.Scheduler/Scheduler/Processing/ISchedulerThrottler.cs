using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000014 RID: 20
	internal interface ISchedulerThrottler
	{
		// Token: 0x06000053 RID: 83
		bool ShouldThrottle(IEnumerable<IMessageScope> scopes, out IMessageScope throttledScope);

		// Token: 0x06000054 RID: 84
		bool ShouldThrottle(IMessageScope scope);

		// Token: 0x06000055 RID: 85
		IEnumerable<IMessageScope> GetThrottlingScopes(IEnumerable<IMessageScope> candidateScopes);
	}
}

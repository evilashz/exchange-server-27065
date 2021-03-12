using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x02000020 RID: 32
	internal class NoOpThrottler : ISchedulerThrottler
	{
		// Token: 0x0600008F RID: 143 RVA: 0x000038DF File Offset: 0x00001ADF
		public bool ShouldThrottle(IEnumerable<IMessageScope> scopes, out IMessageScope throttledScope)
		{
			throttledScope = null;
			return false;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000038E5 File Offset: 0x00001AE5
		public bool ShouldThrottle(IMessageScope scope)
		{
			return false;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000038E8 File Offset: 0x00001AE8
		public IEnumerable<IMessageScope> GetThrottlingScopes(IEnumerable<IMessageScope> candidateScopes)
		{
			return candidateScopes;
		}
	}
}

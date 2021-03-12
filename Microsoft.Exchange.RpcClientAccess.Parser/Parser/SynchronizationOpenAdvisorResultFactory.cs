using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200013A RID: 314
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SynchronizationOpenAdvisorResultFactory : StandardResultFactory
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x00010F90 File Offset: 0x0000F190
		internal SynchronizationOpenAdvisorResultFactory() : base(RopId.SynchronizationOpenAdvisor)
		{
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00010F9D File Offset: 0x0000F19D
		public RopResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulSynchronizationOpenAdvisorResult(serverObject);
		}
	}
}

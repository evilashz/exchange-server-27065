using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200018E RID: 398
	internal class UnreachableSolution : NextHopSolution
	{
		// Token: 0x0600118D RID: 4493 RVA: 0x0004762D File Offset: 0x0004582D
		public UnreachableSolution(NextHopSolutionKey key) : base(key)
		{
			this.reasons = UnreachableReason.None;
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x0004763D File Offset: 0x0004583D
		public UnreachableReason Reasons
		{
			get
			{
				return this.reasons;
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00047645 File Offset: 0x00045845
		public void AddUnreachableReason(UnreachableReason reason)
		{
			this.reasons |= reason;
		}

		// Token: 0x0400094F RID: 2383
		private UnreachableReason reasons;
	}
}

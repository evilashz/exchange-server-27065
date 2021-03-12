using System;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Servicelets.UnifiedPolicySync
{
	// Token: 0x02000007 RID: 7
	public sealed class ExHostStateProvider : HostStateProvider
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002EC8 File Offset: 0x000010C8
		public override bool IsShuttingDown()
		{
			return this.shutDown == 1;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002ED5 File Offset: 0x000010D5
		public override bool ShouldWait(out TimeSpan waitInterval)
		{
			waitInterval = TimeSpan.Zero;
			return false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002EE3 File Offset: 0x000010E3
		internal void SignalShutdown()
		{
			this.shutDown = 1;
		}

		// Token: 0x04000059 RID: 89
		private volatile int shutDown;
	}
}

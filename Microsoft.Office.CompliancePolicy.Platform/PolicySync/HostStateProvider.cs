using System;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200011E RID: 286
	public class HostStateProvider
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x00017F6F File Offset: 0x0001616F
		public virtual bool IsShuttingDown()
		{
			return false;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00017F72 File Offset: 0x00016172
		public virtual bool ShouldWait(out TimeSpan waitInterval)
		{
			waitInterval = TimeSpan.Zero;
			return false;
		}
	}
}

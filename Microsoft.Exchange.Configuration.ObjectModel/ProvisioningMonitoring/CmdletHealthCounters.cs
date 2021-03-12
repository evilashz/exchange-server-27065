using System;
using System.Management.Automation;

namespace Microsoft.Exchange.ProvisioningMonitoring
{
	// Token: 0x02000208 RID: 520
	internal class CmdletHealthCounters
	{
		// Token: 0x06001221 RID: 4641 RVA: 0x000390CA File Offset: 0x000372CA
		internal virtual void IncrementInvocationCount()
		{
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x000390CC File Offset: 0x000372CC
		internal virtual void UpdateSuccessCount(ErrorRecord errorRecord)
		{
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x000390CE File Offset: 0x000372CE
		internal virtual void IncrementIterationInvocationCount()
		{
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x000390D0 File Offset: 0x000372D0
		internal virtual void UpdateIterationSuccessCount(ErrorRecord errorRecord)
		{
		}
	}
}

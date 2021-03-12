using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200019A RID: 410
	internal static class ServerThrottlingResource
	{
		// Token: 0x06000F51 RID: 3921 RVA: 0x00022E20 File Offset: 0x00021020
		internal static void InitializeServerThrottlingObjects(bool initializeRHM)
		{
			if (initializeRHM)
			{
				ThrottlingPerfCounterWrapper.Initialize(BudgetType.ResourceTracking, null, true);
				ResourceHealthMonitorManager.Initialize(ResourceHealthComponent.MRS);
			}
		}
	}
}

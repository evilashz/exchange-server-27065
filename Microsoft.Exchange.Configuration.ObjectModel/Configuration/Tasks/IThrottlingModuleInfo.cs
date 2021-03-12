using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000270 RID: 624
	internal interface IThrottlingModuleInfo
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001578 RID: 5496
		IPowerShellBudget PSBudget { get; }

		// Token: 0x06001579 RID: 5497
		WorkloadSettings GetWorkloadSettings();

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600157A RID: 5498
		ResourceKey[] ResourceKeys { get; }

		// Token: 0x0600157B RID: 5499
		bool OnBeforeDelay(DelayInfo delayInfo);
	}
}

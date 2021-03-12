using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008A5 RID: 2213
	public enum RepairUrgency
	{
		// Token: 0x04002E77 RID: 11895
		[LocDescription(Strings.IDs.RepairUrgencyNormal)]
		Normal,
		// Token: 0x04002E78 RID: 11896
		[LocDescription(Strings.IDs.RepairUrgencyHigh)]
		High,
		// Token: 0x04002E79 RID: 11897
		[LocDescription(Strings.IDs.RepairUrgencyCritical)]
		Critical,
		// Token: 0x04002E7A RID: 11898
		[LocDescription(Strings.IDs.RepairUrgencyProhibited)]
		Prohibited
	}
}

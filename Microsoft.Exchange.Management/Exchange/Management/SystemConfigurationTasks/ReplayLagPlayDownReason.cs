using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000892 RID: 2194
	public enum ReplayLagPlayDownReason
	{
		// Token: 0x04002DAF RID: 11695
		[LocDescription(Strings.IDs.ReplayLagPlayDownReasonNone)]
		None,
		// Token: 0x04002DB0 RID: 11696
		[LocDescription(Strings.IDs.ReplayLagPlayDownReasonLagDisabled)]
		LagDisabled,
		// Token: 0x04002DB1 RID: 11697
		[LocDescription(Strings.IDs.ReplayLagPlayDownReasonNotEnoughFreeSpace)]
		NotEnoughFreeSpace,
		// Token: 0x04002DB2 RID: 11698
		[LocDescription(Strings.IDs.ReplayLagPlayDownReasonLogsInRequiredRange)]
		LogsInRequiredRange
	}
}

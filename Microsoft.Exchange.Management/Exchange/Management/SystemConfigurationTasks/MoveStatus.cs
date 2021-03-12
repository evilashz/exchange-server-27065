using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008B0 RID: 2224
	public enum MoveStatus
	{
		// Token: 0x04002EC1 RID: 11969
		[LocDescription(Strings.IDs.MoveStatusUnknown)]
		Unknown,
		// Token: 0x04002EC2 RID: 11970
		[LocDescription(Strings.IDs.MoveStatusSucceeded)]
		Succeeded,
		// Token: 0x04002EC3 RID: 11971
		[LocDescription(Strings.IDs.MoveStatusWarning)]
		Warning,
		// Token: 0x04002EC4 RID: 11972
		[LocDescription(Strings.IDs.MoveStatusFailed)]
		Failed,
		// Token: 0x04002EC5 RID: 11973
		[LocDescription(Strings.IDs.MoveStatusSkipped)]
		Skipped
	}
}

using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000747 RID: 1863
	internal enum ResubmitRequestResponseCode
	{
		// Token: 0x04003CD8 RID: 15576
		Success = 1,
		// Token: 0x04003CD9 RID: 15577
		CannotModifyCompletedRequest,
		// Token: 0x04003CDA RID: 15578
		ResubmitRequestDoesNotExist,
		// Token: 0x04003CDB RID: 15579
		CannotRemoveRequestInRunningState,
		// Token: 0x04003CDC RID: 15580
		UnexpectedError
	}
}

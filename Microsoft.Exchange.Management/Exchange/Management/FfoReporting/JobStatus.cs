using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003EB RID: 1003
	public enum JobStatus
	{
		// Token: 0x04001C3C RID: 7228
		[LocDescription(CoreStrings.IDs.JobStatusNotStarted)]
		NotStarted,
		// Token: 0x04001C3D RID: 7229
		[LocDescription(CoreStrings.IDs.JobStatusInProgress)]
		InProgress,
		// Token: 0x04001C3E RID: 7230
		[LocDescription(CoreStrings.IDs.JobStatusCancelled)]
		Cancelled,
		// Token: 0x04001C3F RID: 7231
		[LocDescription(CoreStrings.IDs.JobStatusFailed)]
		Failed,
		// Token: 0x04001C40 RID: 7232
		[LocDescription(CoreStrings.IDs.JobStatusDone)]
		Done
	}
}

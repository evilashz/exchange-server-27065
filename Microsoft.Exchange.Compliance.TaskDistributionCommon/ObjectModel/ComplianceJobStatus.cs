using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x02000032 RID: 50
	public enum ComplianceJobStatus : byte
	{
		// Token: 0x040000DF RID: 223
		StatusUnknown,
		// Token: 0x040000E0 RID: 224
		NotStarted,
		// Token: 0x040000E1 RID: 225
		Starting,
		// Token: 0x040000E2 RID: 226
		InProgress,
		// Token: 0x040000E3 RID: 227
		Succeeded,
		// Token: 0x040000E4 RID: 228
		Failed,
		// Token: 0x040000E5 RID: 229
		PartiallySucceeded,
		// Token: 0x040000E6 RID: 230
		Stopped
	}
}

using System;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000D95 RID: 3477
	[Flags]
	public enum StoreIntegrityCheckRequestFlags : uint
	{
		// Token: 0x0400407D RID: 16509
		None = 0U,
		// Token: 0x0400407E RID: 16510
		DetectOnly = 1U,
		// Token: 0x0400407F RID: 16511
		Force = 2U,
		// Token: 0x04004080 RID: 16512
		SystemJob = 4U,
		// Token: 0x04004081 RID: 16513
		Verbose = 2147483648U
	}
}

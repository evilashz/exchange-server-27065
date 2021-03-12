using System;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000993 RID: 2451
	[Flags]
	internal enum AcquireLicenseFlags : uint
	{
		// Token: 0x04002D34 RID: 11572
		NonSilent = 1U,
		// Token: 0x04002D35 RID: 11573
		NoPersist = 2U,
		// Token: 0x04002D36 RID: 11574
		Cancel = 4U,
		// Token: 0x04002D37 RID: 11575
		FetchAdvisory = 8U,
		// Token: 0x04002D38 RID: 11576
		NoUI = 16U
	}
}

using System;

namespace Microsoft.Exchange.Management.Analysis.Features
{
	// Token: 0x02000065 RID: 101
	[Flags]
	public enum SetupMode
	{
		// Token: 0x04000153 RID: 339
		None = 0,
		// Token: 0x04000154 RID: 340
		Install = 1,
		// Token: 0x04000155 RID: 341
		Upgrade = 2,
		// Token: 0x04000156 RID: 342
		Uninstall = 4,
		// Token: 0x04000157 RID: 343
		DisasterRecovery = 8,
		// Token: 0x04000158 RID: 344
		All = 15
	}
}

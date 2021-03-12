using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200008A RID: 138
	[Flags]
	public enum LogOptions
	{
		// Token: 0x04000250 RID: 592
		None = 0,
		// Token: 0x04000251 RID: 593
		LogException = 1,
		// Token: 0x04000252 RID: 594
		LogStart = 2,
		// Token: 0x04000253 RID: 595
		LogSuccess = 4,
		// Token: 0x04000254 RID: 596
		LogPeriodic = 8,
		// Token: 0x04000255 RID: 597
		LogAll = 7,
		// Token: 0x04000256 RID: 598
		LogExceptionFull = 16,
		// Token: 0x04000257 RID: 599
		LogAlways = 32768,
		// Token: 0x04000258 RID: 600
		LogNever = 16384
	}
}

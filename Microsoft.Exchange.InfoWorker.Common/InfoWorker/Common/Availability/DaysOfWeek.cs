using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000F2 RID: 242
	[Flags]
	public enum DaysOfWeek
	{
		// Token: 0x040003DF RID: 991
		Sunday = 1,
		// Token: 0x040003E0 RID: 992
		Monday = 2,
		// Token: 0x040003E1 RID: 993
		Tuesday = 4,
		// Token: 0x040003E2 RID: 994
		Wednesday = 8,
		// Token: 0x040003E3 RID: 995
		Thursday = 16,
		// Token: 0x040003E4 RID: 996
		Friday = 32,
		// Token: 0x040003E5 RID: 997
		Saturday = 64
	}
}

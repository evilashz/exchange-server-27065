using System;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x0200000A RID: 10
	internal enum MeteringEvent
	{
		// Token: 0x04000002 RID: 2
		EntityAdded,
		// Token: 0x04000003 RID: 3
		EntityRemoved,
		// Token: 0x04000004 RID: 4
		MeasureAdded,
		// Token: 0x04000005 RID: 5
		MeasureRemoved,
		// Token: 0x04000006 RID: 6
		MeasurePromoted,
		// Token: 0x04000007 RID: 7
		MeasureExpired
	}
}

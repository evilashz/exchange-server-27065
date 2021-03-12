using System;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000035 RID: 53
	[Flags]
	internal enum TimeFieldToRepair
	{
		// Token: 0x04000134 RID: 308
		None = 0,
		// Token: 0x04000135 RID: 309
		StartTime = 1,
		// Token: 0x04000136 RID: 310
		EndTime = 2,
		// Token: 0x04000137 RID: 311
		StartAndEndTime = 3
	}
}

using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009D8 RID: 2520
	public enum FirstWeekRules
	{
		// Token: 0x04003348 RID: 13128
		[LocDescription(ServerStrings.IDs.FirstDay)]
		LegacyNotSet,
		// Token: 0x04003349 RID: 13129
		[LocDescription(ServerStrings.IDs.FirstDay)]
		FirstDay,
		// Token: 0x0400334A RID: 13130
		[LocDescription(ServerStrings.IDs.FirstFourDayWeek)]
		FirstFourDayWeek,
		// Token: 0x0400334B RID: 13131
		[LocDescription(ServerStrings.IDs.FirstFullWeek)]
		FirstFullWeek
	}
}

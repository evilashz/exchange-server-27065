using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009D6 RID: 2518
	public enum CalendarReminder
	{
		// Token: 0x0400332F RID: 13103
		[LocDescription(ServerStrings.IDs.ZeroMinutes)]
		ZeroMinutes,
		// Token: 0x04003330 RID: 13104
		[LocDescription(ServerStrings.IDs.FiveMinutes)]
		FiveMinutes = 5,
		// Token: 0x04003331 RID: 13105
		[LocDescription(ServerStrings.IDs.TenMinutes)]
		TenMinutes = 10,
		// Token: 0x04003332 RID: 13106
		[LocDescription(ServerStrings.IDs.FifteenMinutes)]
		FifteenMinutes = 15,
		// Token: 0x04003333 RID: 13107
		[LocDescription(ServerStrings.IDs.ThirtyMinutes)]
		ThirtyMinutes = 30,
		// Token: 0x04003334 RID: 13108
		[LocDescription(ServerStrings.IDs.OneHours)]
		OneHours = 60,
		// Token: 0x04003335 RID: 13109
		[LocDescription(ServerStrings.IDs.TwoHours)]
		TwoHours = 120,
		// Token: 0x04003336 RID: 13110
		[LocDescription(ServerStrings.IDs.ThreeHours)]
		ThreeHours = 180,
		// Token: 0x04003337 RID: 13111
		[LocDescription(ServerStrings.IDs.FourHours)]
		FourHours = 240,
		// Token: 0x04003338 RID: 13112
		[LocDescription(ServerStrings.IDs.EightHours)]
		EightHours = 480,
		// Token: 0x04003339 RID: 13113
		[LocDescription(ServerStrings.IDs.TwelveHours)]
		TwelveHours = 720,
		// Token: 0x0400333A RID: 13114
		[LocDescription(ServerStrings.IDs.OneDays)]
		OneDays = 1440,
		// Token: 0x0400333B RID: 13115
		[LocDescription(ServerStrings.IDs.TwoDays)]
		TwoDays = 2880,
		// Token: 0x0400333C RID: 13116
		[LocDescription(ServerStrings.IDs.ThreeDays)]
		ThreeDays = 4320,
		// Token: 0x0400333D RID: 13117
		[LocDescription(ServerStrings.IDs.OneWeeks)]
		OneWeeks = 10080,
		// Token: 0x0400333E RID: 13118
		[LocDescription(ServerStrings.IDs.TwoWeeks)]
		TwoWeeks = 20160
	}
}

using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003C8 RID: 968
	[Flags]
	internal enum DifferencesBetweenBlobAndAttach
	{
		// Token: 0x04001871 RID: 6257
		None = 0,
		// Token: 0x04001872 RID: 6258
		StartTime = 1,
		// Token: 0x04001873 RID: 6259
		EndTime = 2,
		// Token: 0x04001874 RID: 6260
		Subject = 4,
		// Token: 0x04001875 RID: 6261
		Location = 8,
		// Token: 0x04001876 RID: 6262
		AppointmentColor = 16,
		// Token: 0x04001877 RID: 6263
		IsAllDayEvent = 32,
		// Token: 0x04001878 RID: 6264
		HasAttachment = 64,
		// Token: 0x04001879 RID: 6265
		FreeBusyStatus = 128,
		// Token: 0x0400187A RID: 6266
		ReminderIsSet = 256,
		// Token: 0x0400187B RID: 6267
		ReminderMinutesBeforeStartInternal = 512,
		// Token: 0x0400187C RID: 6268
		AppointmentState = 1024
	}
}

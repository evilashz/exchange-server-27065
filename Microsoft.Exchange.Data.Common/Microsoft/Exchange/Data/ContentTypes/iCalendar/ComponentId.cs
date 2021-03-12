using System;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000A4 RID: 164
	[Flags]
	public enum ComponentId
	{
		// Token: 0x04000586 RID: 1414
		None = 0,
		// Token: 0x04000587 RID: 1415
		Unknown = 1,
		// Token: 0x04000588 RID: 1416
		VCalendar = 2,
		// Token: 0x04000589 RID: 1417
		VEvent = 4,
		// Token: 0x0400058A RID: 1418
		VTodo = 8,
		// Token: 0x0400058B RID: 1419
		VJournal = 16,
		// Token: 0x0400058C RID: 1420
		VFreeBusy = 32,
		// Token: 0x0400058D RID: 1421
		VTimeZone = 64,
		// Token: 0x0400058E RID: 1422
		VAlarm = 128,
		// Token: 0x0400058F RID: 1423
		Standard = 256,
		// Token: 0x04000590 RID: 1424
		Daylight = 512
	}
}

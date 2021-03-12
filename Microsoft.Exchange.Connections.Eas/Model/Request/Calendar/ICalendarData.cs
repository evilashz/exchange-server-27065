using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Eas.Model.Common.Email;
using Microsoft.Exchange.Connections.Eas.Model.Request.AirSyncBase;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.Calendar
{
	// Token: 0x02000091 RID: 145
	public interface ICalendarData
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002B2 RID: 690
		// (set) Token: 0x060002B3 RID: 691
		Body Body { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002B4 RID: 692
		// (set) Token: 0x060002B5 RID: 693
		byte? AllDayEvent { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002B6 RID: 694
		// (set) Token: 0x060002B7 RID: 695
		byte? BusyStatus { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002B8 RID: 696
		// (set) Token: 0x060002B9 RID: 697
		string DtStamp { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002BA RID: 698
		// (set) Token: 0x060002BB RID: 699
		string EndTime { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002BC RID: 700
		// (set) Token: 0x060002BD RID: 701
		string Location { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002BE RID: 702
		// (set) Token: 0x060002BF RID: 703
		uint? Reminder { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002C0 RID: 704
		// (set) Token: 0x060002C1 RID: 705
		byte? Sensitivity { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002C2 RID: 706
		// (set) Token: 0x060002C3 RID: 707
		string CalendarSubject { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002C4 RID: 708
		// (set) Token: 0x060002C5 RID: 709
		string StartTime { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002C6 RID: 710
		// (set) Token: 0x060002C7 RID: 711
		List<Attendee> Attendees { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002C8 RID: 712
		// (set) Token: 0x060002C9 RID: 713
		List<Category> CalendarCategories { get; set; }
	}
}

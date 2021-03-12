using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000047 RID: 71
	public interface IEvent : IItem, IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001A7 RID: 423
		// (set) Token: 0x060001A8 RID: 424
		IList<Attendee> Attendees { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001A9 RID: 425
		// (set) Token: 0x060001AA RID: 426
		Calendar Calendar { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001AB RID: 427
		// (set) Token: 0x060001AC RID: 428
		string ClientId { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001AD RID: 429
		// (set) Token: 0x060001AE RID: 430
		bool DisallowNewTimeProposal { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001AF RID: 431
		// (set) Token: 0x060001B0 RID: 432
		ExDateTime End { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001B1 RID: 433
		// (set) Token: 0x060001B2 RID: 434
		IList<string> ExceptionalProperties { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001B3 RID: 435
		// (set) Token: 0x060001B4 RID: 436
		bool HasAttendees { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001B5 RID: 437
		// (set) Token: 0x060001B6 RID: 438
		string IntendedEndTimeZoneId { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001B7 RID: 439
		// (set) Token: 0x060001B8 RID: 440
		string IntendedStartTimeZoneId { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001B9 RID: 441
		// (set) Token: 0x060001BA RID: 442
		FreeBusyStatus IntendedStatus { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001BB RID: 443
		// (set) Token: 0x060001BC RID: 444
		bool IsAllDay { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001BD RID: 445
		// (set) Token: 0x060001BE RID: 446
		bool IsCancelled { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001BF RID: 447
		// (set) Token: 0x060001C0 RID: 448
		bool IsDraft { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001C1 RID: 449
		// (set) Token: 0x060001C2 RID: 450
		bool IsOnlineMeeting { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001C3 RID: 451
		// (set) Token: 0x060001C4 RID: 452
		bool IsOrganizer { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001C5 RID: 453
		// (set) Token: 0x060001C6 RID: 454
		Location Location { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001C7 RID: 455
		// (set) Token: 0x060001C8 RID: 456
		IList<Event> Occurrences { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001C9 RID: 457
		// (set) Token: 0x060001CA RID: 458
		string OnlineMeetingConfLink { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001CB RID: 459
		// (set) Token: 0x060001CC RID: 460
		string OnlineMeetingExternalLink { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001CD RID: 461
		// (set) Token: 0x060001CE RID: 462
		Organizer Organizer { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001CF RID: 463
		// (set) Token: 0x060001D0 RID: 464
		PatternedRecurrence PatternedRecurrence { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001D1 RID: 465
		// (set) Token: 0x060001D2 RID: 466
		bool ResponseRequested { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001D3 RID: 467
		// (set) Token: 0x060001D4 RID: 468
		ResponseStatus ResponseStatus { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001D5 RID: 469
		// (set) Token: 0x060001D6 RID: 470
		string SeriesId { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001D7 RID: 471
		// (set) Token: 0x060001D8 RID: 472
		Event SeriesMaster { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001D9 RID: 473
		// (set) Token: 0x060001DA RID: 474
		string SeriesMasterId { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001DB RID: 475
		// (set) Token: 0x060001DC RID: 476
		FreeBusyStatus ShowAs { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001DD RID: 477
		// (set) Token: 0x060001DE RID: 478
		ExDateTime Start { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001DF RID: 479
		// (set) Token: 0x060001E0 RID: 480
		EventType Type { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001E1 RID: 481
		// (set) Token: 0x060001E2 RID: 482
		IList<EventPopupReminderSetting> PopupReminderSettings { get; set; }
	}
}

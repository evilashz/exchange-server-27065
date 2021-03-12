using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003D7 RID: 983
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LinkedCalendarGroupEntryInfo : CalendarGroupEntryInfo
	{
		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x000B58DD File Offset: 0x000B3ADD
		// (set) Token: 0x06002C61 RID: 11361 RVA: 0x000B58E5 File Offset: 0x000B3AE5
		public string CalendarOwner { get; private set; }

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06002C62 RID: 11362 RVA: 0x000B58EE File Offset: 0x000B3AEE
		// (set) Token: 0x06002C63 RID: 11363 RVA: 0x000B58F6 File Offset: 0x000B3AF6
		public bool IsGeneralScheduleCalendar { get; private set; }

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06002C64 RID: 11364 RVA: 0x000B58FF File Offset: 0x000B3AFF
		// (set) Token: 0x06002C65 RID: 11365 RVA: 0x000B5907 File Offset: 0x000B3B07
		public bool IsPublicCalendarFolder { get; private set; }

		// Token: 0x06002C66 RID: 11366 RVA: 0x000B5910 File Offset: 0x000B3B10
		public LinkedCalendarGroupEntryInfo(string calendarName, VersionedId id, LegacyCalendarColor color, StoreObjectId calendarId, string calendarOwner, Guid parentGroupId, byte[] calendarOrdinal, bool isGeneralScheduleCalendar, bool isPublicCalendarFolder, ExDateTime lastModifiedTime) : base(calendarName, id, color, calendarId, parentGroupId, calendarOrdinal, lastModifiedTime)
		{
			this.CalendarOwner = calendarOwner;
			this.IsGeneralScheduleCalendar = isGeneralScheduleCalendar;
			this.IsPublicCalendarFolder = isPublicCalendarFolder;
		}
	}
}

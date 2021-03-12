using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003D8 RID: 984
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LocalCalendarGroupEntryInfo : CalendarGroupEntryInfo
	{
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x000B593B File Offset: 0x000B3B3B
		// (set) Token: 0x06002C68 RID: 11368 RVA: 0x000B5943 File Offset: 0x000B3B43
		public bool IsInternetCalendar { get; private set; }

		// Token: 0x06002C69 RID: 11369 RVA: 0x000B594C File Offset: 0x000B3B4C
		public LocalCalendarGroupEntryInfo(string calendarName, VersionedId id, LegacyCalendarColor color, StoreObjectId calendarId, byte[] calendarOrdinal, Guid parentGroupId, bool isICal, ExDateTime lastModifiedTime) : base(calendarName, id, color, calendarId, parentGroupId, calendarOrdinal, lastModifiedTime)
		{
			this.IsInternetCalendar = isICal;
		}
	}
}

using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200039F RID: 927
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class CalendarGroupEntryInfo : FolderTreeDataInfo
	{
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06002934 RID: 10548 RVA: 0x000A407A File Offset: 0x000A227A
		// (set) Token: 0x06002935 RID: 10549 RVA: 0x000A4082 File Offset: 0x000A2282
		public string CalendarName { get; private set; }

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x000A408B File Offset: 0x000A228B
		// (set) Token: 0x06002937 RID: 10551 RVA: 0x000A4093 File Offset: 0x000A2293
		public StoreObjectId CalendarId { get; private set; }

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x000A409C File Offset: 0x000A229C
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x000A40A4 File Offset: 0x000A22A4
		public LegacyCalendarColor LegacyCalendarColor { get; private set; }

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x000A40AD File Offset: 0x000A22AD
		// (set) Token: 0x0600293B RID: 10555 RVA: 0x000A40B5 File Offset: 0x000A22B5
		public CalendarColor CalendarColor { get; private set; }

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x000A40BE File Offset: 0x000A22BE
		// (set) Token: 0x0600293D RID: 10557 RVA: 0x000A40C6 File Offset: 0x000A22C6
		public Guid ParentGroupClassId { get; private set; }

		// Token: 0x0600293E RID: 10558 RVA: 0x000A40D0 File Offset: 0x000A22D0
		public CalendarGroupEntryInfo(string calendarName, VersionedId id, LegacyCalendarColor color, StoreObjectId calendarId, Guid parentGroupId, byte[] calendarOrdinal, ExDateTime lastModifiedTime) : base(id, calendarOrdinal, lastModifiedTime)
		{
			Util.ThrowOnNullArgument(calendarName, "calendarName");
			EnumValidator.ThrowIfInvalid<LegacyCalendarColor>(color, "color");
			this.CalendarId = calendarId;
			this.CalendarName = calendarName;
			this.LegacyCalendarColor = color;
			this.ParentGroupClassId = parentGroupId;
			this.CalendarColor = LegacyCalendarColorConverter.FromLegacyCalendarColor(color);
		}
	}
}

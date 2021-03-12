using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003C5 RID: 965
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMeetingRequest : IMeetingMessage, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002BCF RID: 11215
		CalendarItemBase GetCorrelatedItem();

		// Token: 0x06002BD0 RID: 11216
		bool TryUpdateCalendarItem(ref CalendarItemBase originalCalendarItem, bool canUpdatePrincipalCalendar);

		// Token: 0x06002BD1 RID: 11217
		bool IsDelegated();

		// Token: 0x06002BD2 RID: 11218
		VersionedId FetchCorrelatedItemId(CalendarFolder calendarFolder, bool shouldDetectDuplicateIds, out IEnumerable<VersionedId> detectedDuplicatesIds);
	}
}

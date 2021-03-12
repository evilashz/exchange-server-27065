using System;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200039A RID: 922
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICalendarGroup : IFolderTreeData, IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x060028C0 RID: 10432
		Guid GroupClassId { get; }

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x060028C1 RID: 10433
		// (set) Token: 0x060028C2 RID: 10434
		string GroupName { get; set; }

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x060028C3 RID: 10435
		CalendarGroupType GroupType { get; }

		// Token: 0x060028C4 RID: 10436
		ReadOnlyCollection<CalendarGroupEntryInfo> GetChildCalendars();

		// Token: 0x060028C5 RID: 10437
		CalendarGroupInfo GetCalendarGroupInfo();

		// Token: 0x060028C6 RID: 10438
		CalendarGroupEntryInfo FindSharedGSCalendaryEntry(string sharerLegacyDN);

		// Token: 0x060028C7 RID: 10439
		CalendarGroupEntryInfo FindSharedCalendaryEntry(StoreObjectId folderId);
	}
}

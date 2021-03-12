using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200039C RID: 924
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICalendarGroupEntry : IMessageItem, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060028EF RID: 10479
		// (set) Token: 0x060028F0 RID: 10480
		string CalendarName { get; set; }

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060028F1 RID: 10481
		string ParentGroupName { get; }

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060028F2 RID: 10482
		// (set) Token: 0x060028F3 RID: 10483
		Guid ParentGroupClassId { get; set; }

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060028F4 RID: 10484
		// (set) Token: 0x060028F5 RID: 10485
		LegacyCalendarColor LegacyCalendarColor { get; set; }

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060028F6 RID: 10486
		// (set) Token: 0x060028F7 RID: 10487
		CalendarColor CalendarColor { get; set; }

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060028F8 RID: 10488
		// (set) Token: 0x060028F9 RID: 10489
		StoreObjectId CalendarId { get; set; }

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060028FA RID: 10490
		VersionedId CalendarGroupEntryId { get; }

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060028FB RID: 10491
		// (set) Token: 0x060028FC RID: 10492
		byte[] CalendarRecordKey { get; set; }

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060028FD RID: 10493
		bool IsLocalMailboxCalendar { get; }

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060028FE RID: 10494
		// (set) Token: 0x060028FF RID: 10495
		byte[] SharerAddressBookEntryId { get; set; }

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06002900 RID: 10496
		// (set) Token: 0x06002901 RID: 10497
		byte[] UserAddressBookStoreEntryId { get; set; }
	}
}

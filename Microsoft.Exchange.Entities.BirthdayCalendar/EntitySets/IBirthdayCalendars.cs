using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.DataProviders;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets
{
	// Token: 0x0200000D RID: 13
	internal interface IBirthdayCalendars : IEntitySet<IBirthdayCalendar>, IStorageEntitySetScope<IMailboxSession>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004D RID: 77
		CalendarFolderDataProvider CalendarFolderDataProvider { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004E RID: 78
		IBirthdaysContainer ParentScope { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004F RID: 79
		StoreId BirthdayCalendarFolderId { get; }
	}
}

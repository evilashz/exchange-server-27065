using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.BirthdayCalendar
{
	// Token: 0x0200000F RID: 15
	internal interface IBirthdaysContainer : IStorageEntitySetScope<IMailboxSession>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000055 RID: 85
		IBirthdayCalendars Calendars { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000056 RID: 86
		IBirthdayContacts Contacts { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000057 RID: 87
		IBirthdayEvents Events { get; }
	}
}

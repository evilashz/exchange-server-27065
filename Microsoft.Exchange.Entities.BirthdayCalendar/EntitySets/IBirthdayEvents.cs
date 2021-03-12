using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.BirthdayCalendar.DataProviders;
using Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets.BirthdayEventCommands;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets
{
	// Token: 0x0200000B RID: 11
	internal interface IBirthdayEvents : IEntitySet<IBirthdayEvent>, IStorageEntitySetScope<IStoreSession>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57
		// (set) Token: 0x0600003A RID: 58
		BirthdayEventDataProvider BirthdayEventDataProvider { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59
		IBirthdayCalendars ParentScope { get; }

		// Token: 0x0600003C RID: 60
		IEnumerable<BirthdayEvent> GetBirthdayCalendarView(ExDateTime rangeStart, ExDateTime rangeEnd);

		// Token: 0x0600003D RID: 61
		BirthdayEventCommandResult CreateBirthdayEventForContact(IBirthdayContact contact);

		// Token: 0x0600003E RID: 62
		BirthdayEventCommandResult DeleteBirthdayEventForContactId(StoreObjectId birthdayContactStoreObjectId);

		// Token: 0x0600003F RID: 63
		BirthdayEventCommandResult DeleteBirthdayEventForContact(IBirthdayContact birthdayContact);

		// Token: 0x06000040 RID: 64
		BirthdayEventCommandResult UpdateBirthdayEventForContact(IBirthdayEvent birthdayEvent, IBirthdayContact birthdayContact);

		// Token: 0x06000041 RID: 65
		BirthdayEventCommandResult UpdateBirthdaysForLinkedContacts(IEnumerable<IBirthdayContact> linkedContacts);
	}
}

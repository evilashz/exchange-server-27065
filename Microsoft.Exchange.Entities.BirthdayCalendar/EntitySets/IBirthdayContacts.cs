using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.BirthdayCalendar.DataProviders;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets
{
	// Token: 0x02000003 RID: 3
	internal interface IBirthdayContacts : IEntitySet<IBirthdayContact>, IStorageEntitySetScope<IStoreSession>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8
		// (set) Token: 0x06000009 RID: 9
		BirthdayContactDataProvider BirthdayContactDataProvider { get; set; }

		// Token: 0x0600000A RID: 10
		IEnumerable<IBirthdayContact> GetLinkedContacts(PersonId personId);
	}
}

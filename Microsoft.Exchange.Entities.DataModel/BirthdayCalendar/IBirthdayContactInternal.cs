using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.BirthdayCalendar
{
	// Token: 0x0200001D RID: 29
	internal interface IBirthdayContactInternal : IBirthdayContact, IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000061 RID: 97
		// (set) Token: 0x06000062 RID: 98
		PersonId PersonId { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000063 RID: 99
		// (set) Token: 0x06000064 RID: 100
		StoreId StoreId { get; set; }
	}
}

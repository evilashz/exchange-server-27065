using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.BirthdayCalendar
{
	// Token: 0x02000022 RID: 34
	internal interface IBirthdayEventInternal : IBirthdayEvent, IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000093 RID: 147
		// (set) Token: 0x06000094 RID: 148
		StoreObjectId ContactId { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000095 RID: 149
		// (set) Token: 0x06000096 RID: 150
		PersonId PersonId { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000097 RID: 151
		// (set) Token: 0x06000098 RID: 152
		StoreId StoreId { get; set; }
	}
}

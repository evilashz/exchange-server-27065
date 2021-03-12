using System;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.BirthdayCalendar
{
	// Token: 0x02000017 RID: 23
	public sealed class BirthdayCalendar : StorageEntity<BirthdayCalendarSchema>, IBirthdayCalendar, IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
	}
}

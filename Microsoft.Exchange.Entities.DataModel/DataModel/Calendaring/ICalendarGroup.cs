using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000030 RID: 48
	public interface ICalendarGroup : IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F5 RID: 245
		// (set) Token: 0x060000F6 RID: 246
		IEnumerable<Calendar> Calendars { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F7 RID: 247
		// (set) Token: 0x060000F8 RID: 248
		Guid ClassId { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F9 RID: 249
		// (set) Token: 0x060000FA RID: 250
		string Name { get; set; }
	}
}

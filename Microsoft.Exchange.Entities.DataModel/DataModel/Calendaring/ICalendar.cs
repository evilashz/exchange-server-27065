using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x0200002D RID: 45
	public interface ICalendar : IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DD RID: 221
		// (set) Token: 0x060000DE RID: 222
		string Name { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000DF RID: 223
		// (set) Token: 0x060000E0 RID: 224
		IEnumerable<Event> Events { get; set; }
	}
}

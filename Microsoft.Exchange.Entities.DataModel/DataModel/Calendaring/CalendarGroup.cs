using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000031 RID: 49
	public sealed class CalendarGroup : StorageEntity<CalendarGroupSchema>, ICalendarGroup, IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003651 File Offset: 0x00001851
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00003659 File Offset: 0x00001859
		public IEnumerable<Calendar> Calendars { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00003662 File Offset: 0x00001862
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00003675 File Offset: 0x00001875
		public Guid ClassId
		{
			get
			{
				return base.GetPropertyValueOrDefault<Guid>(base.Schema.ClassIdProperty);
			}
			set
			{
				base.SetPropertyValue<Guid>(base.Schema.ClassIdProperty, value);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00003689 File Offset: 0x00001889
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000369C File Offset: 0x0000189C
		public string Name
		{
			get
			{
				return base.GetPropertyValueOrDefault<string>(base.Schema.NameProperty);
			}
			set
			{
				base.SetPropertyValue<string>(base.Schema.NameProperty, value);
			}
		}

		// Token: 0x02000032 RID: 50
		public new static class Accessors
		{
			// Token: 0x04000068 RID: 104
			public static readonly EntityPropertyAccessor<CalendarGroup, Guid> ClassId = new EntityPropertyAccessor<CalendarGroup, Guid>(SchematizedObject<CalendarGroupSchema>.SchemaInstance.ClassIdProperty, (CalendarGroup calendarGroup) => calendarGroup.ClassId, delegate(CalendarGroup calendarGroup, Guid classId)
			{
				calendarGroup.ClassId = classId;
			});

			// Token: 0x04000069 RID: 105
			public static readonly EntityPropertyAccessor<CalendarGroup, string> Name = new EntityPropertyAccessor<CalendarGroup, string>(SchematizedObject<CalendarGroupSchema>.SchemaInstance.NameProperty, (CalendarGroup calendarGroup) => calendarGroup.Name, delegate(CalendarGroup calendarGroup, string name)
			{
				calendarGroup.Name = name;
			});
		}
	}
}

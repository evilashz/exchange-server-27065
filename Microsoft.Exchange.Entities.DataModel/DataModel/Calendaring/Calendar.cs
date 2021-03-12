using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x0200002E RID: 46
	public sealed class Calendar : StorageEntity<CalendarSchema>, ICalendar, IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003411 File Offset: 0x00001611
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00003424 File Offset: 0x00001624
		public CalendarColor Color
		{
			get
			{
				return base.GetPropertyValueOrDefault<CalendarColor>(base.Schema.ColorProperty);
			}
			set
			{
				base.SetPropertyValue<CalendarColor>(base.Schema.ColorProperty, value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003438 File Offset: 0x00001638
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00003440 File Offset: 0x00001640
		public IEnumerable<Event> Events { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003449 File Offset: 0x00001649
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000345C File Offset: 0x0000165C
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003470 File Offset: 0x00001670
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00003483 File Offset: 0x00001683
		internal StoreId CalendarFolderStoreId
		{
			get
			{
				return base.GetPropertyValueOrDefault<StoreId>(base.Schema.CalendarFolderStoreIdProperty);
			}
			set
			{
				base.SetPropertyValue<StoreId>(base.Schema.CalendarFolderStoreIdProperty, value);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003497 File Offset: 0x00001697
		// (set) Token: 0x060000EA RID: 234 RVA: 0x000034AA File Offset: 0x000016AA
		internal byte[] RecordKey
		{
			get
			{
				return base.GetPropertyValueOrDefault<byte[]>(base.Schema.RecordKeyProperty);
			}
			set
			{
				base.SetPropertyValue<byte[]>(base.Schema.RecordKeyProperty, value);
			}
		}

		// Token: 0x0200002F RID: 47
		public new static class Accessors
		{
			// Token: 0x0400005B RID: 91
			public static readonly EntityPropertyAccessor<Calendar, CalendarColor> Color = new EntityPropertyAccessor<Calendar, CalendarColor>(SchematizedObject<CalendarSchema>.SchemaInstance.ColorProperty, (Calendar calendar) => calendar.Color, delegate(Calendar calendar, CalendarColor color)
			{
				calendar.Color = color;
			});

			// Token: 0x0400005C RID: 92
			public static readonly EntityPropertyAccessor<Calendar, string> Name = new EntityPropertyAccessor<Calendar, string>(SchematizedObject<CalendarSchema>.SchemaInstance.NameProperty, (Calendar calendar) => calendar.Name, delegate(Calendar calendar, string name)
			{
				calendar.Name = name;
			});

			// Token: 0x0400005D RID: 93
			internal static readonly EntityPropertyAccessor<Calendar, StoreId> CalendarFolderStoreId = new EntityPropertyAccessor<Calendar, StoreId>(SchematizedObject<CalendarSchema>.SchemaInstance.CalendarFolderStoreIdProperty, (Calendar calendar) => calendar.CalendarFolderStoreId, delegate(Calendar calendar, StoreId id)
			{
				calendar.CalendarFolderStoreId = id;
			});

			// Token: 0x0400005E RID: 94
			internal static readonly EntityPropertyAccessor<Calendar, byte[]> RecordKey = new EntityPropertyAccessor<Calendar, byte[]>(SchematizedObject<CalendarSchema>.SchemaInstance.RecordKeyProperty, (Calendar calendar) => calendar.RecordKey, delegate(Calendar calendar, byte[] recordKey)
			{
				calendar.RecordKey = recordKey;
			});
		}
	}
}

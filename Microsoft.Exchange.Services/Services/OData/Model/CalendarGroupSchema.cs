using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E67 RID: 3687
	internal class CalendarGroupSchema : EntitySchema
	{
		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06005FC6 RID: 24518 RVA: 0x0012AD71 File Offset: 0x00128F71
		public new static CalendarGroupSchema SchemaInstance
		{
			get
			{
				return CalendarGroupSchema.CalendarGroupSchemaInstance.Member;
			}
		}

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06005FC7 RID: 24519 RVA: 0x0012AD7D File Offset: 0x00128F7D
		public override EdmEntityType EdmEntityType
		{
			get
			{
				return CalendarGroup.EdmEntityType;
			}
		}

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06005FC8 RID: 24520 RVA: 0x0012AD84 File Offset: 0x00128F84
		public override ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				return CalendarGroupSchema.DeclaredCalendarGroupProperties;
			}
		}

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06005FC9 RID: 24521 RVA: 0x0012AD8B File Offset: 0x00128F8B
		public override ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return CalendarGroupSchema.AllCalendarGroupProperties;
			}
		}

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06005FCA RID: 24522 RVA: 0x0012AD92 File Offset: 0x00128F92
		public override ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				return CalendarGroupSchema.DefaultCalendarGroupProperties;
			}
		}

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06005FCB RID: 24523 RVA: 0x0012AD99 File Offset: 0x00128F99
		public override ReadOnlyCollection<PropertyDefinition> MandatoryCreationProperties
		{
			get
			{
				return CalendarGroupSchema.MandatoryCalendarGroupCreationProperties;
			}
		}

		// Token: 0x06005FCD RID: 24525 RVA: 0x0012ADF4 File Offset: 0x00128FF4
		// Note: this type is marked as 'beforefieldinit'.
		static CalendarGroupSchema()
		{
			PropertyDefinition propertyDefinition = new PropertyDefinition("Name", typeof(string));
			propertyDefinition.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition2 = propertyDefinition;
			DataEntityPropertyProvider<CalendarGroup> dataEntityPropertyProvider = new DataEntityPropertyProvider<CalendarGroup>("Name");
			dataEntityPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, CalendarGroup d)
			{
				e[ep] = d.Name;
			};
			dataEntityPropertyProvider.Setter = delegate(Entity e, PropertyDefinition ep, CalendarGroup d)
			{
				d.Name = (string)e[ep];
			};
			propertyDefinition2.DataEntityPropertyProvider = dataEntityPropertyProvider;
			CalendarGroupSchema.Name = propertyDefinition;
			PropertyDefinition propertyDefinition3 = new PropertyDefinition("ClassId", typeof(Guid));
			propertyDefinition3.EdmType = EdmCoreModel.Instance.GetGuid(true);
			propertyDefinition3.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition4 = propertyDefinition3;
			DataEntityPropertyProvider<CalendarGroup> dataEntityPropertyProvider2 = new DataEntityPropertyProvider<CalendarGroup>("ClassId");
			dataEntityPropertyProvider2.Getter = delegate(Entity e, PropertyDefinition ep, CalendarGroup d)
			{
				e[ep] = d.ClassId;
			};
			dataEntityPropertyProvider2.Setter = delegate(Entity e, PropertyDefinition ep, CalendarGroup d)
			{
				d.ClassId = (Guid)e[ep];
			};
			propertyDefinition4.DataEntityPropertyProvider = dataEntityPropertyProvider2;
			CalendarGroupSchema.ClassId = propertyDefinition3;
			CalendarGroupSchema.ChangeKey = ItemSchema.ChangeKey;
			CalendarGroupSchema.Calendars = new PropertyDefinition("Calendars", typeof(IEnumerable<Calendar>))
			{
				NavigationTargetEntity = Calendar.EdmEntityType,
				Flags = PropertyDefinitionFlags.Navigation
			};
			CalendarGroupSchema.DeclaredCalendarGroupProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				CalendarGroupSchema.Name,
				CalendarGroupSchema.ChangeKey,
				CalendarGroupSchema.ClassId,
				CalendarGroupSchema.Calendars
			});
			CalendarGroupSchema.AllCalendarGroupProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(EntitySchema.AllEntityProperties.Union(CalendarGroupSchema.DeclaredCalendarGroupProperties)));
			CalendarGroupSchema.DefaultCalendarGroupProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(EntitySchema.DefaultEntityProperties)
			{
				CalendarGroupSchema.Name,
				CalendarGroupSchema.ChangeKey,
				CalendarGroupSchema.ClassId
			});
			CalendarGroupSchema.MandatoryCalendarGroupCreationProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				CalendarSchema.Name
			});
			CalendarGroupSchema.CalendarGroupSchemaInstance = new LazyMember<CalendarGroupSchema>(() => new CalendarGroupSchema());
		}

		// Token: 0x040033EE RID: 13294
		public static readonly PropertyDefinition Name;

		// Token: 0x040033EF RID: 13295
		public static readonly PropertyDefinition ClassId;

		// Token: 0x040033F0 RID: 13296
		public static readonly PropertyDefinition ChangeKey;

		// Token: 0x040033F1 RID: 13297
		public static readonly PropertyDefinition Calendars;

		// Token: 0x040033F2 RID: 13298
		public static readonly ReadOnlyCollection<PropertyDefinition> DeclaredCalendarGroupProperties;

		// Token: 0x040033F3 RID: 13299
		public static readonly ReadOnlyCollection<PropertyDefinition> AllCalendarGroupProperties;

		// Token: 0x040033F4 RID: 13300
		public static readonly ReadOnlyCollection<PropertyDefinition> DefaultCalendarGroupProperties;

		// Token: 0x040033F5 RID: 13301
		public static readonly ReadOnlyCollection<PropertyDefinition> MandatoryCalendarGroupCreationProperties;

		// Token: 0x040033F6 RID: 13302
		private static readonly LazyMember<CalendarGroupSchema> CalendarGroupSchemaInstance;
	}
}

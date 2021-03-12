using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E65 RID: 3685
	internal class CalendarSchema : EntitySchema
	{
		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x06005FB0 RID: 24496 RVA: 0x0012AAB5 File Offset: 0x00128CB5
		public new static CalendarSchema SchemaInstance
		{
			get
			{
				return CalendarSchema.CalendarSchemaInstance.Member;
			}
		}

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06005FB1 RID: 24497 RVA: 0x0012AAC1 File Offset: 0x00128CC1
		public override EdmEntityType EdmEntityType
		{
			get
			{
				return Calendar.EdmEntityType;
			}
		}

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06005FB2 RID: 24498 RVA: 0x0012AAC8 File Offset: 0x00128CC8
		public override ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				return CalendarSchema.DeclaredCalendarProperties;
			}
		}

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06005FB3 RID: 24499 RVA: 0x0012AACF File Offset: 0x00128CCF
		public override ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return CalendarSchema.AllCalendarProperties;
			}
		}

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06005FB4 RID: 24500 RVA: 0x0012AAD6 File Offset: 0x00128CD6
		public override ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				return CalendarSchema.DefaultCalendarProperties;
			}
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06005FB5 RID: 24501 RVA: 0x0012AADD File Offset: 0x00128CDD
		public override ReadOnlyCollection<PropertyDefinition> MandatoryCreationProperties
		{
			get
			{
				return CalendarSchema.MandatoryCalendarCreationProperties;
			}
		}

		// Token: 0x06005FB7 RID: 24503 RVA: 0x0012AB10 File Offset: 0x00128D10
		// Note: this type is marked as 'beforefieldinit'.
		static CalendarSchema()
		{
			PropertyDefinition propertyDefinition = new PropertyDefinition("Name", typeof(string));
			propertyDefinition.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition2 = propertyDefinition;
			DataEntityPropertyProvider<Calendar> dataEntityPropertyProvider = new DataEntityPropertyProvider<Calendar>("Name");
			dataEntityPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, Calendar d)
			{
				e[ep] = d.Name;
			};
			dataEntityPropertyProvider.Setter = delegate(Entity e, PropertyDefinition ep, Calendar d)
			{
				d.Name = (string)e[ep];
			};
			propertyDefinition2.DataEntityPropertyProvider = dataEntityPropertyProvider;
			CalendarSchema.Name = propertyDefinition;
			CalendarSchema.ChangeKey = ItemSchema.ChangeKey;
			CalendarSchema.Events = new PropertyDefinition("Events", typeof(IEnumerable<Event>))
			{
				NavigationTargetEntity = Event.EdmEntityType,
				Flags = PropertyDefinitionFlags.Navigation
			};
			CalendarSchema.DeclaredCalendarProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				CalendarSchema.Name,
				CalendarSchema.ChangeKey,
				CalendarSchema.Events
			});
			CalendarSchema.AllCalendarProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(EntitySchema.AllEntityProperties.Union(CalendarSchema.DeclaredCalendarProperties)));
			CalendarSchema.DefaultCalendarProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(EntitySchema.DefaultEntityProperties)
			{
				CalendarSchema.Name,
				CalendarSchema.ChangeKey
			});
			CalendarSchema.MandatoryCalendarCreationProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				CalendarSchema.Name
			});
			CalendarSchema.CalendarSchemaInstance = new LazyMember<CalendarSchema>(() => new CalendarSchema());
		}

		// Token: 0x040033E2 RID: 13282
		public static readonly PropertyDefinition Name;

		// Token: 0x040033E3 RID: 13283
		public static readonly PropertyDefinition ChangeKey;

		// Token: 0x040033E4 RID: 13284
		public static readonly PropertyDefinition Events;

		// Token: 0x040033E5 RID: 13285
		public static readonly ReadOnlyCollection<PropertyDefinition> DeclaredCalendarProperties;

		// Token: 0x040033E6 RID: 13286
		public static readonly ReadOnlyCollection<PropertyDefinition> AllCalendarProperties;

		// Token: 0x040033E7 RID: 13287
		public static readonly ReadOnlyCollection<PropertyDefinition> DefaultCalendarProperties;

		// Token: 0x040033E8 RID: 13288
		public static readonly ReadOnlyCollection<PropertyDefinition> MandatoryCalendarCreationProperties;

		// Token: 0x040033E9 RID: 13289
		private static readonly LazyMember<CalendarSchema> CalendarSchemaInstance;
	}
}

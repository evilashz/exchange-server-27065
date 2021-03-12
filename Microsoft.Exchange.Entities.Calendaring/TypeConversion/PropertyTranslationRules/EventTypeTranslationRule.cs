using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.TypeConversion;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyTranslationRules
{
	// Token: 0x02000099 RID: 153
	internal class EventTypeTranslationRule : IStorageTranslationRule<ICalendarItemBase, IEvent>, IPropertyValueCollectionTranslationRule<ICalendarItemBase, Microsoft.Exchange.Data.PropertyDefinition, IEvent>, ITranslationRule<ICalendarItemBase, IEvent>
	{
		// Token: 0x0600039A RID: 922 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		public EventTypeTranslationRule()
		{
			this.StorageDependencies = new Microsoft.Exchange.Data.PropertyDefinition[]
			{
				EventTypeTranslationRule.CalendarItemTypeProperty,
				EventTypeTranslationRule.NprSeriesId,
				EventTypeTranslationRule.ItemClass
			};
			this.StoragePropertyGroup = null;
			this.EntityProperties = new Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[]
			{
				SchematizedObject<EventSchema>.SchemaInstance.TypeProperty
			};
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000DA2F File Offset: 0x0000BC2F
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000DA37 File Offset: 0x0000BC37
		public IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> EntityProperties { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000DA40 File Offset: 0x0000BC40
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000DA48 File Offset: 0x0000BC48
		public IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> StorageDependencies { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000DA51 File Offset: 0x0000BC51
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000DA59 File Offset: 0x0000BC59
		public PropertyChangeMetadata.PropertyGroup StoragePropertyGroup { get; private set; }

		// Token: 0x060003A1 RID: 929 RVA: 0x0000DA94 File Offset: 0x0000BC94
		public void FromLeftToRightType(ICalendarItemBase left, IEvent right)
		{
			right.Type = EventTypeTranslationRule.GetEventType(() => left.CalendarItemType, () => left.ItemClass, () => left.SeriesId);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000DB80 File Offset: 0x0000BD80
		public void FromPropertyValues(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, IEvent right)
		{
			object calendarItemTypeValue;
			Func<CalendarItemType> getCalendarItemType = delegate()
			{
				if (!this.TryGetPropertyFromPropertyIndices(propertyIndices, values, EventTypeTranslationRule.CalendarItemTypeProperty, out calendarItemTypeValue))
				{
					return CalendarItemType.Single;
				}
				return (CalendarItemType)calendarItemTypeValue;
			};
			object itemClass;
			Func<string> getItemClass = delegate()
			{
				if (!this.TryGetPropertyFromPropertyIndices(propertyIndices, values, EventTypeTranslationRule.ItemClass, out itemClass))
				{
					return null;
				}
				return (string)itemClass;
			};
			object seriesId;
			Func<string> getSeriesId = delegate()
			{
				if (!this.TryGetPropertyFromPropertyIndices(propertyIndices, values, EventTypeTranslationRule.NprSeriesId, out seriesId))
				{
					return null;
				}
				return (string)seriesId;
			};
			right.Type = EventTypeTranslationRule.GetEventType(getCalendarItemType, getItemClass, getSeriesId);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000DBDD File Offset: 0x0000BDDD
		public void FromRightToLeftType(ICalendarItemBase left, IEvent right)
		{
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		internal static EventType GetEventType(Func<CalendarItemType> getCalendarItemType, Func<string> getItemClass, Func<string> getSeriesId)
		{
			EventType result;
			switch (getCalendarItemType())
			{
			case CalendarItemType.Single:
				if (ObjectClass.IsCalendarItemSeries(getItemClass()))
				{
					result = EventType.SeriesMaster;
				}
				else if (!string.IsNullOrEmpty(getSeriesId()))
				{
					result = EventType.Exception;
				}
				else
				{
					result = EventType.SingleInstance;
				}
				break;
			case CalendarItemType.Occurrence:
				result = EventType.Occurrence;
				break;
			case CalendarItemType.Exception:
				result = EventType.Exception;
				break;
			case CalendarItemType.RecurringMaster:
				result = EventType.SeriesMaster;
				break;
			default:
				throw new ArgumentOutOfRangeException("value");
			}
			return result;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000DC4C File Offset: 0x0000BE4C
		private bool TryGetPropertyFromPropertyIndices(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, Microsoft.Exchange.Data.PropertyDefinition property, out object value)
		{
			int index;
			object obj;
			if (propertyIndices.TryGetValue(property, out index) && (obj = values[index]) != null && obj.GetType() == property.Type)
			{
				value = obj;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x04000151 RID: 337
		private static readonly StorePropertyDefinition NprSeriesId = CalendarItemBaseSchema.SeriesId;

		// Token: 0x04000152 RID: 338
		private static readonly Microsoft.Exchange.Data.PropertyDefinition ItemClass = StoreObjectSchema.ItemClass;

		// Token: 0x04000153 RID: 339
		private static readonly StorePropertyDefinition CalendarItemTypeProperty = CalendarItemBaseSchema.CalendarItemType;
	}
}

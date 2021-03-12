using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.TypeConversion;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyTranslationRules
{
	// Token: 0x0200009F RID: 159
	internal class SeriesMasterIdTranslationRule : IStorageTranslationRule<ICalendarItemBase, Event>, ITranslationRule<ICalendarItemBase, Event>
	{
		// Token: 0x060003CA RID: 970 RVA: 0x0000E1E1 File Offset: 0x0000C3E1
		static SeriesMasterIdTranslationRule()
		{
			SeriesMasterIdTranslationRule.NprInstanceAccessor = new DefaultStoragePropertyAccessor<ICalendarItem, string>(SeriesMasterIdTranslationRule.NprSeriesMasterId, false);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000E214 File Offset: 0x0000C414
		public SeriesMasterIdTranslationRule(IdConverter idConverter = null)
		{
			this.StorageDependencies = new Microsoft.Exchange.Data.PropertyDefinition[]
			{
				SeriesMasterIdTranslationRule.ItemClass,
				SeriesMasterIdTranslationRule.ItemId,
				SeriesMasterIdTranslationRule.NprSeriesMasterId
			};
			this.StoragePropertyGroup = null;
			this.EntityProperties = new Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[]
			{
				SchematizedObject<EventSchema>.SchemaInstance.SeriesMasterIdProperty
			};
			this.IdConverter = (idConverter ?? IdConverter.Instance);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000E27F File Offset: 0x0000C47F
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0000E287 File Offset: 0x0000C487
		public IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> EntityProperties { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000E290 File Offset: 0x0000C490
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000E298 File Offset: 0x0000C498
		public IdConverter IdConverter { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000E2A1 File Offset: 0x0000C4A1
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0000E2A9 File Offset: 0x0000C4A9
		public IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> StorageDependencies { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000E2B2 File Offset: 0x0000C4B2
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000E2BA File Offset: 0x0000C4BA
		public PropertyChangeMetadata.PropertyGroup StoragePropertyGroup { get; private set; }

		// Token: 0x060003D4 RID: 980 RVA: 0x0000E2E0 File Offset: 0x0000C4E0
		public void FromLeftToRightType(ICalendarItemBase left, Event right)
		{
			this.FromLeftToRight(right, delegate(out string value)
			{
				return this.TryGetValue(left, out value);
			});
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		public void FromPropertyValues(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, IStoreSession session, Event right)
		{
			this.FromLeftToRight(right, delegate(out string value)
			{
				int index;
				object obj;
				if (propertyIndices.TryGetValue(SeriesMasterIdTranslationRule.ItemClass, out index) && (obj = values[index]) is string)
				{
					string itemClass = (string)obj;
					if (ObjectClass.IsCalendarItemOccurrence(itemClass) || ObjectClass.IsRecurrenceException(itemClass))
					{
						return this.TryGetSeriesMasterIdForPrInstance(propertyIndices, values, session, out value);
					}
					if (ObjectClass.IsCalendarItem(itemClass))
					{
						return this.TryGetSeriesMasterIdForNprInstance(propertyIndices, values, session, out value);
					}
				}
				value = null;
				return false;
			});
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
		public void FromRightToLeftType(ICalendarItemBase left, Event right)
		{
			ICalendarItem calendarItem = left as ICalendarItem;
			if (calendarItem != null && right.IsPropertySet(SchematizedObject<EventSchema>.SchemaInstance.SeriesMasterIdProperty))
			{
				SeriesMasterIdTranslationRule.NprInstanceAccessor.Set(calendarItem, right.SeriesMasterId);
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000E434 File Offset: 0x0000C634
		public bool TryGetValue(ICalendarItemBase container, out string value)
		{
			ICalendarItemOccurrence calendarItemOccurrence = container as ICalendarItemOccurrence;
			if (calendarItemOccurrence != null)
			{
				value = this.IdConverter.ToStringId(calendarItemOccurrence.MasterId, calendarItemOccurrence.Session);
				return true;
			}
			ICalendarItem calendarItem = container as ICalendarItem;
			if (calendarItem != null && !string.IsNullOrEmpty(calendarItem.SeriesId))
			{
				return SeriesMasterIdTranslationRule.NprInstanceAccessor.TryGetValue(calendarItem, out value);
			}
			value = null;
			return false;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000E490 File Offset: 0x0000C690
		protected virtual bool TryGetSeriesMasterIdForPrInstance(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, IStoreSession session, out string seriesMasterId)
		{
			int index;
			object obj;
			if (propertyIndices.TryGetValue(ItemSchema.Id, out index) && (obj = values[index]) is StoreId)
			{
				StoreObjectId storeObjectId = StoreId.GetStoreObjectId((StoreId)obj);
				StoreObjectId storeId = StoreObjectId.FromProviderSpecificId(storeObjectId.ProviderLevelItemId, StoreObjectType.CalendarItem);
				seriesMasterId = this.IdConverter.ToStringId(storeId, session);
				return true;
			}
			seriesMasterId = null;
			return false;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E4F0 File Offset: 0x0000C6F0
		protected virtual bool TryGetSeriesMasterIdForNprInstance(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, IStoreSession session, out string seriesMasterId)
		{
			int index;
			object obj;
			if (propertyIndices.TryGetValue(SeriesMasterIdTranslationRule.NprSeriesMasterId, out index) && (obj = values[index]) is string)
			{
				seriesMasterId = (string)obj;
				return true;
			}
			seriesMasterId = null;
			return false;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000E52C File Offset: 0x0000C72C
		private void FromLeftToRight(Event entity, SeriesMasterIdTranslationRule.TryGetValueFunc<string> tryGetMasterId)
		{
			string seriesMasterId;
			if (tryGetMasterId != null && tryGetMasterId(out seriesMasterId))
			{
				entity.SeriesMasterId = seriesMasterId;
			}
		}

		// Token: 0x04000163 RID: 355
		private static readonly Microsoft.Exchange.Data.PropertyDefinition ItemClass = StoreObjectSchema.ItemClass;

		// Token: 0x04000164 RID: 356
		private static readonly Microsoft.Exchange.Data.PropertyDefinition ItemId = ItemSchema.Id;

		// Token: 0x04000165 RID: 357
		private static readonly IStoragePropertyAccessor<ICalendarItem, string> NprInstanceAccessor;

		// Token: 0x04000166 RID: 358
		private static readonly StorePropertyDefinition NprSeriesMasterId = CalendarItemSchema.SeriesMasterId;

		// Token: 0x020000A0 RID: 160
		// (Invoke) Token: 0x060003DC RID: 988
		public delegate bool TryGetValueFunc<TValue>(out TValue value);
	}
}

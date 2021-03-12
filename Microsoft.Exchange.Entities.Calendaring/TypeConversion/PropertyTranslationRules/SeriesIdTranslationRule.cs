using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.TypeConversion;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyTranslationRules
{
	// Token: 0x0200009D RID: 157
	internal class SeriesIdTranslationRule : IStorageTranslationRule<ICalendarItemBase, IEvent>, IPropertyValueCollectionTranslationRule<ICalendarItemBase, Microsoft.Exchange.Data.PropertyDefinition, IEvent>, ITranslationRule<ICalendarItemBase, IEvent>
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x0000DF2E File Offset: 0x0000C12E
		static SeriesIdTranslationRule()
		{
			SeriesIdTranslationRule.NprInstanceAccessor = new DefaultStoragePropertyAccessor<ICalendarItemBase, string>(SeriesIdTranslationRule.NprSeriesId, false);
			SeriesIdTranslationRule.PrInstanceAccessor = new DefaultStoragePropertyAccessor<ICalendarItemBase, byte[]>(SeriesIdTranslationRule.CleanGlobalObjectId, false);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000DF70 File Offset: 0x0000C170
		public SeriesIdTranslationRule()
		{
			this.StorageDependencies = new StorePropertyDefinition[]
			{
				SeriesIdTranslationRule.CalendarItemTypeProperty,
				SeriesIdTranslationRule.NprSeriesId,
				SeriesIdTranslationRule.CleanGlobalObjectId
			};
			this.StoragePropertyGroup = null;
			this.EntityProperties = new Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition[]
			{
				SchematizedObject<EventSchema>.SchemaInstance.SeriesIdProperty
			};
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000DFCB File Offset: 0x0000C1CB
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000DFD3 File Offset: 0x0000C1D3
		public IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> EntityProperties { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		public IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> StorageDependencies { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000DFED File Offset: 0x0000C1ED
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000DFF5 File Offset: 0x0000C1F5
		public PropertyChangeMetadata.PropertyGroup StoragePropertyGroup { get; private set; }

		// Token: 0x060003C1 RID: 961 RVA: 0x0000E01C File Offset: 0x0000C21C
		public void FromLeftToRightType(ICalendarItemBase left, IEvent right)
		{
			SeriesIdTranslationRule.FromLeftToRight(right, delegate(out string value)
			{
				return this.TryGetValue(left, out value);
			});
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000E124 File Offset: 0x0000C324
		public void FromPropertyValues(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, IEvent right)
		{
			SeriesIdTranslationRule.FromLeftToRight(right, delegate(out string value)
			{
				int index;
				object obj;
				if (propertyIndices.TryGetValue(SeriesIdTranslationRule.CalendarItemTypeProperty, out index) && (obj = values[index]) is CalendarItemType)
				{
					if ((CalendarItemType)obj == CalendarItemType.Single)
					{
						if (propertyIndices.TryGetValue(SeriesIdTranslationRule.NprSeriesId, out index) && values[index] is string)
						{
							value = (string)values[index];
							return true;
						}
					}
					else if (propertyIndices.TryGetValue(SeriesIdTranslationRule.CleanGlobalObjectId, out index) && values[index] is byte[])
					{
						value = new GlobalObjectId((byte[])values[index]).ToString();
						return true;
					}
				}
				value = null;
				return false;
			});
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000E157 File Offset: 0x0000C357
		public void FromRightToLeftType(ICalendarItemBase left, IEvent right)
		{
			if (right.IsPropertySet(SchematizedObject<EventSchema>.SchemaInstance.SeriesIdProperty))
			{
				SeriesIdTranslationRule.NprInstanceAccessor.Set(left, right.SeriesId);
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000E17C File Offset: 0x0000C37C
		public bool TryGetValue(ICalendarItemBase container, out string value)
		{
			if (container.CalendarItemType == CalendarItemType.Single)
			{
				return SeriesIdTranslationRule.NprInstanceAccessor.TryGetValue(container, out value);
			}
			byte[] globalObjectIdBytes;
			if (SeriesIdTranslationRule.PrInstanceAccessor.TryGetValue(container, out globalObjectIdBytes))
			{
				value = new GlobalObjectId(globalObjectIdBytes).ToString();
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		private static void FromLeftToRight(IEvent entity, SeriesIdTranslationRule.TryGetValueFunc<string> tryGetSeriesId)
		{
			string seriesId;
			if (tryGetSeriesId != null && tryGetSeriesId(out seriesId))
			{
				entity.SeriesId = seriesId;
			}
		}

		// Token: 0x0400015B RID: 347
		private static readonly IStoragePropertyAccessor<ICalendarItemBase, string> NprInstanceAccessor;

		// Token: 0x0400015C RID: 348
		private static readonly IStoragePropertyAccessor<ICalendarItemBase, byte[]> PrInstanceAccessor;

		// Token: 0x0400015D RID: 349
		private static readonly StorePropertyDefinition NprSeriesId = CalendarItemBaseSchema.SeriesId;

		// Token: 0x0400015E RID: 350
		private static readonly StorePropertyDefinition CleanGlobalObjectId = CalendarItemBaseSchema.CleanGlobalObjectId;

		// Token: 0x0400015F RID: 351
		private static readonly StorePropertyDefinition CalendarItemTypeProperty = CalendarItemBaseSchema.CalendarItemType;

		// Token: 0x0200009E RID: 158
		// (Invoke) Token: 0x060003C7 RID: 967
		public delegate bool TryGetValueFunc<TValue>(out TValue value);
	}
}

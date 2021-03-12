﻿using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C2B RID: 3115
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class StartTimeProperty : SmartPropertyDefinition
	{
		// Token: 0x06006EA1 RID: 28321 RVA: 0x001DC094 File Offset: 0x001DA294
		internal StartTimeProperty() : base("StartTime", typeof(ExDateTime), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiStartTime, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.MapiPRStartDate, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.TimeZoneDefinitionStart, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.MapiEndTime, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.MapiPREndDate, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.ReminderNextTime, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.MapiIsAllDayEvent, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.AppointmentRecurring, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.ReminderDueByInternal, PropertyDependencyType.NeedToReadForWrite)
		})
		{
			this.legalTrackingDependencies = (from x in base.Dependencies
			where x.Property != InternalSchema.ReminderNextTime && x.Property != InternalSchema.ReminderDueByInternal
			select x).ToArray<PropertyDependency>();
		}

		// Token: 0x17001DF1 RID: 7665
		// (get) Token: 0x06006EA2 RID: 28322 RVA: 0x001DC176 File Offset: 0x001DA376
		internal override PropertyDependency[] LegalTrackingDependencies
		{
			get
			{
				return this.legalTrackingDependencies;
			}
		}

		// Token: 0x06006EA3 RID: 28323 RVA: 0x001DC17E File Offset: 0x001DA37E
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return StartTimeProperty.InternalTryGetDateTimeValue(propertyBag, this, InternalSchema.MapiStartTime, InternalSchema.MapiPRStartDate);
		}

		// Token: 0x06006EA4 RID: 28324 RVA: 0x001DC194 File Offset: 0x001DA394
		internal static object InternalTryGetDateTimeValue(PropertyBag.BasicPropertyStore propertyBag, StorePropertyDefinition property, GuidIdPropertyDefinition mapiUtcTimeProperty, PropertyTagPropertyDefinition mapiLegacyUtcTimeProperty)
		{
			ExDateTime? normalizedTime = StartTimeProperty.GetNormalizedTime(propertyBag, mapiUtcTimeProperty, mapiLegacyUtcTimeProperty);
			if (normalizedTime != null)
			{
				return normalizedTime.Value;
			}
			return new PropertyError(property, PropertyErrorCode.NotFound);
		}

		// Token: 0x06006EA5 RID: 28325 RVA: 0x001DC1C7 File Offset: 0x001DA3C7
		internal static ExDateTime? GetNormalizedTime(PropertyBag propertyBag, GuidIdPropertyDefinition utcTimeProperty, PropertyTagPropertyDefinition legacyUtcTimeProperty)
		{
			return StartTimeProperty.GetNormalizedTime((PropertyBag.BasicPropertyStore)propertyBag, utcTimeProperty, legacyUtcTimeProperty);
		}

		// Token: 0x06006EA6 RID: 28326 RVA: 0x001DC1D8 File Offset: 0x001DA3D8
		internal static ExDateTime? GetNormalizedTime(PropertyBag.BasicPropertyStore propertyBag, GuidIdPropertyDefinition utcTimeProperty, PropertyTagPropertyDefinition legacyUtcTimeProperty)
		{
			ExDateTime? valueAsNullable = propertyBag.GetValueAsNullable<ExDateTime>(utcTimeProperty);
			if (valueAsNullable == null)
			{
				if (legacyUtcTimeProperty != null)
				{
					valueAsNullable = propertyBag.GetValueAsNullable<ExDateTime>(legacyUtcTimeProperty);
				}
				if (valueAsNullable == null)
				{
					return null;
				}
			}
			ExDateTime exDateTime = ExTimeZone.UtcTimeZone.ConvertDateTime(valueAsNullable.Value);
			byte[] valueOrDefault = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.TimeZoneDefinitionStart);
			ExTimeZone legacyTimeZone;
			if (O12TimeZoneFormatter.TryParseTimeZoneBlob(valueOrDefault, string.Empty, out legacyTimeZone))
			{
				exDateTime = TimeZoneHelper.NormalizeUtcTime(exDateTime, legacyTimeZone);
			}
			exDateTime = StartTimeProperty.DynamicAdjustForAllDayEvent(propertyBag, exDateTime, utcTimeProperty == InternalSchema.MapiEndTime);
			return new ExDateTime?(propertyBag.TimeZone.ConvertDateTime(exDateTime));
		}

		// Token: 0x06006EA7 RID: 28327 RVA: 0x001DC270 File Offset: 0x001DA470
		private static ExDateTime DynamicAdjustForAllDayEvent(PropertyBag.BasicPropertyStore propertyBag, ExDateTime originalResult, bool isEndTime)
		{
			CalendarItemBase calendarItemBase = propertyBag.Context.StoreObject as CalendarItemBase;
			bool flag = false;
			if (calendarItemBase != null && calendarItemBase.IsAllDayEventCache != null)
			{
				flag = calendarItemBase.IsAllDayEventCache.Value;
			}
			if (!flag || propertyBag.TimeZone == null)
			{
				return originalResult;
			}
			if (isEndTime && originalResult != originalResult.Date)
			{
				return originalResult.Date.IncrementDays(1);
			}
			return originalResult.Date;
		}

		// Token: 0x06006EA8 RID: 28328 RVA: 0x001DC2EC File Offset: 0x001DA4EC
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			StartTimeProperty.SetCalendarTime(propertyBag, InternalSchema.MapiStartTime, InternalSchema.MapiPRStartDate, InternalSchema.TimeZoneDefinitionStart, false, value);
		}

		// Token: 0x06006EA9 RID: 28329 RVA: 0x001DC308 File Offset: 0x001DA508
		internal static void SetCalendarTime(PropertyBag.BasicPropertyStore propertyBag, GuidIdPropertyDefinition utcTimeProperty, PropertyTagPropertyDefinition legacyUtcTimeProperty, GuidIdPropertyDefinition timeZoneDefinition, bool isEndTime, object value)
		{
			StoreObject storeObject = propertyBag.Context.StoreObject;
			CalendarItemBase calendarItemBase = storeObject as CalendarItemBase;
			if (calendarItemBase != null && calendarItemBase.PropertyBag.ExTimeZone != null && calendarItemBase.IsAllDayEventCache == null)
			{
				object obj = IsAllDayEventProperty.CalculateIsAllDayEvent(propertyBag);
				if (obj is bool)
				{
					calendarItemBase.IsAllDayEventCache = new bool?((bool)obj);
				}
			}
			propertyBag.SetValueWithFixup(utcTimeProperty, value);
			propertyBag.SetValueWithFixup(legacyUtcTimeProperty, value);
			ExTimeZone timeZone = propertyBag.TimeZone;
			if (value is ExDateTime && ((ExDateTime)value).TimeZone != null && ((ExDateTime)value).TimeZone != ExTimeZone.UnspecifiedTimeZone)
			{
				timeZone = ((ExDateTime)value).TimeZone;
			}
			if (timeZone == ExTimeZone.UtcTimeZone)
			{
				if (!(storeObject is CalendarItemOccurrence))
				{
					propertyBag.Delete(timeZoneDefinition);
				}
			}
			else if (value is ExDateTime && timeZone != ExTimeZone.UnspecifiedTimeZone)
			{
				if (timeZoneDefinition == InternalSchema.TimeZoneDefinitionStart)
				{
					byte[] timeZoneBlob = O12TimeZoneFormatter.GetTimeZoneBlob(timeZone);
					StartTimeZoneProperty.RecalculateNormalizedTimeProperty(propertyBag, InternalSchema.MapiEndTime, InternalSchema.MapiPREndDate, timeZone);
					StartTimeZoneProperty.RecalculateNormalizedTimeProperty(propertyBag, InternalSchema.ReminderNextTime, null, timeZone);
					StartTimeZoneProperty.SyncRecurringTimeZoneProperties(propertyBag, timeZone, timeZoneBlob);
					propertyBag.SetValueWithFixup(timeZoneDefinition, timeZoneBlob);
				}
				else
				{
					byte[] timeZoneBlob2 = O12TimeZoneFormatter.GetTimeZoneBlob(timeZone, (ExDateTime)value);
					propertyBag.SetValueWithFixup(timeZoneDefinition, timeZoneBlob2);
				}
			}
			if (!isEndTime)
			{
				propertyBag.SetValueWithFixup(InternalSchema.ReminderDueByInternal, value);
			}
			if (storeObject != null)
			{
				Reminder.Adjust(storeObject);
			}
		}

		// Token: 0x06006EAA RID: 28330 RVA: 0x001DC46B File Offset: 0x001DA66B
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, InternalSchema.MapiStartTime);
		}

		// Token: 0x06006EAB RID: 28331 RVA: 0x001DC479 File Offset: 0x001DA679
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, InternalSchema.MapiStartTime);
		}

		// Token: 0x06006EAC RID: 28332 RVA: 0x001DC487 File Offset: 0x001DA687
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
		}

		// Token: 0x17001DF2 RID: 7666
		// (get) Token: 0x06006EAD RID: 28333 RVA: 0x001DC4A9 File Offset: 0x001DA6A9
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06006EAE RID: 28334 RVA: 0x001DC4AC File Offset: 0x001DA6AC
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.MapiStartTime;
		}

		// Token: 0x04004212 RID: 16914
		private readonly PropertyDependency[] legalTrackingDependencies;
	}
}

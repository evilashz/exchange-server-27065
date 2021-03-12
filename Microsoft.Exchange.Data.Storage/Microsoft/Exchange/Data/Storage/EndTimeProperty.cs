using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C2C RID: 3116
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class EndTimeProperty : SmartPropertyDefinition
	{
		// Token: 0x06006EB0 RID: 28336 RVA: 0x001DC4B4 File Offset: 0x001DA6B4
		internal EndTimeProperty() : base("EndTime", typeof(ExDateTime), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiEndTime, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.MapiPREndDate, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.TimeZoneDefinitionStart, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.TimeZoneDefinitionEnd, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.MapiStartTime, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.MapiPRStartDate, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.MapiIsAllDayEvent, PropertyDependencyType.NeedToReadForWrite)
		})
		{
		}

		// Token: 0x06006EB1 RID: 28337 RVA: 0x001DC546 File Offset: 0x001DA746
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return StartTimeProperty.InternalTryGetDateTimeValue(propertyBag, this, InternalSchema.MapiEndTime, InternalSchema.MapiPREndDate);
		}

		// Token: 0x06006EB2 RID: 28338 RVA: 0x001DC559 File Offset: 0x001DA759
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			StartTimeProperty.SetCalendarTime(propertyBag, InternalSchema.MapiEndTime, InternalSchema.MapiPREndDate, InternalSchema.TimeZoneDefinitionEnd, true, value);
			EndTimeProperty.DenormalizeTimeProperty(propertyBag, (ExDateTime)propertyBag.GetValue(InternalSchema.MapiEndTime), InternalSchema.MapiEndTime, InternalSchema.MapiPREndDate);
		}

		// Token: 0x06006EB3 RID: 28339 RVA: 0x001DC593 File Offset: 0x001DA793
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return base.SinglePropertySmartFilterToNativeFilter(filter, InternalSchema.MapiEndTime);
		}

		// Token: 0x06006EB4 RID: 28340 RVA: 0x001DC5A1 File Offset: 0x001DA7A1
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return base.SinglePropertyNativeFilterToSmartFilter(filter, InternalSchema.MapiEndTime);
		}

		// Token: 0x06006EB5 RID: 28341 RVA: 0x001DC5AF File Offset: 0x001DA7AF
		internal override void RegisterFilterTranslation()
		{
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ComparisonFilter));
			FilterRestrictionConverter.RegisterFilterTranslation(this, typeof(ExistsFilter));
		}

		// Token: 0x17001DF3 RID: 7667
		// (get) Token: 0x06006EB6 RID: 28342 RVA: 0x001DC5D1 File Offset: 0x001DA7D1
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x06006EB7 RID: 28343 RVA: 0x001DC5D4 File Offset: 0x001DA7D4
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return InternalSchema.MapiEndTime;
		}

		// Token: 0x06006EB8 RID: 28344 RVA: 0x001DC5DC File Offset: 0x001DA7DC
		private static void DenormalizeTimeProperty(PropertyBag.BasicPropertyStore propertyBag, ExDateTime newTime, GuidIdPropertyDefinition utcTimeProperty, PropertyTagPropertyDefinition legacyUtcTimeProperty)
		{
			byte[] valueOrDefault = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.TimeZoneDefinitionStart);
			ExTimeZone legacyTimeZone;
			if (O12TimeZoneFormatter.TryParseTimeZoneBlob(valueOrDefault, string.Empty, out legacyTimeZone))
			{
				ExDateTime exDateTime = TimeZoneHelper.DeNormalizeToUtcTime(newTime, legacyTimeZone);
				propertyBag.SetValueWithFixup(utcTimeProperty, exDateTime);
				if (legacyUtcTimeProperty != null)
				{
					propertyBag.SetValueWithFixup(legacyUtcTimeProperty, exDateTime);
				}
			}
		}

		// Token: 0x06006EB9 RID: 28345 RVA: 0x001DC62C File Offset: 0x001DA82C
		internal static void DenormalizeTimeProperty(PropertyBag propertyBag, ExDateTime newTime, GuidIdPropertyDefinition utcTimeProperty, PropertyTagPropertyDefinition legacyUtcTimeProperty)
		{
			byte[] valueOrDefault = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.TimeZoneDefinitionStart);
			ExTimeZone legacyTimeZone;
			if (O12TimeZoneFormatter.TryParseTimeZoneBlob(valueOrDefault, string.Empty, out legacyTimeZone))
			{
				ExDateTime exDateTime = TimeZoneHelper.DeNormalizeToUtcTime(newTime, legacyTimeZone);
				propertyBag.SetProperty(utcTimeProperty, exDateTime);
				if (legacyUtcTimeProperty != null)
				{
					propertyBag.SetProperty(legacyUtcTimeProperty, exDateTime);
				}
			}
		}
	}
}

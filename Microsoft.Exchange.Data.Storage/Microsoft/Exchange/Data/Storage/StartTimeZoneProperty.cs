using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C2D RID: 3117
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class StartTimeZoneProperty : SmartPropertyDefinition
	{
		// Token: 0x06006EBA RID: 28346 RVA: 0x001DC67C File Offset: 0x001DA87C
		internal StartTimeZoneProperty() : base("CreationTimeZone", typeof(ExTimeZone), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.TimeZoneDefinitionStart, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.MapiStartTime, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.MapiPRStartDate, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.MapiEndTime, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.MapiPREndDate, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.ReminderNextTime, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.AppointmentRecurring, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.MapiIsAllDayEvent, PropertyDependencyType.NeedToReadForWrite)
		})
		{
		}

		// Token: 0x06006EBB RID: 28347 RVA: 0x001DC71C File Offset: 0x001DA91C
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return StartTimeZoneProperty.GetExTimeZoneFromLegacyBlob(propertyBag, InternalSchema.TimeZoneDefinitionStart);
		}

		// Token: 0x06006EBC RID: 28348 RVA: 0x001DC729 File Offset: 0x001DA929
		internal static ExTimeZone GetExTimeZoneFromLegacyBlob(PropertyBag propertyBag, NativeStorePropertyDefinition propertyDefinition)
		{
			return StartTimeZoneProperty.GetExTimeZoneFromLegacyBlob((PropertyBag.BasicPropertyStore)propertyBag, propertyDefinition);
		}

		// Token: 0x06006EBD RID: 28349 RVA: 0x001DC738 File Offset: 0x001DA938
		internal static ExTimeZone GetExTimeZoneFromLegacyBlob(PropertyBag.BasicPropertyStore propertyBag, NativeStorePropertyDefinition propertyDefinition)
		{
			byte[] valueOrDefault = propertyBag.GetValueOrDefault<byte[]>(propertyDefinition);
			ExTimeZone exTimeZone;
			if (O12TimeZoneFormatter.TryParseTimeZoneBlob(valueOrDefault, string.Empty, out exTimeZone))
			{
				return TimeZoneHelper.PromoteCustomizedTimeZone(exTimeZone) ?? exTimeZone;
			}
			return ExTimeZone.UtcTimeZone;
		}

		// Token: 0x06006EBE RID: 28350 RVA: 0x001DC770 File Offset: 0x001DA970
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			ExTimeZone exTimeZone = value as ExTimeZone;
			if (exTimeZone == ExTimeZone.UnspecifiedTimeZone)
			{
				throw new InvalidOperationException("unspecified time zone is not allowed to set");
			}
			StartTimeZoneProperty.RecalculateNormalizedTimeProperty(propertyBag, InternalSchema.MapiStartTime, InternalSchema.MapiPRStartDate, exTimeZone);
			StartTimeZoneProperty.RecalculateNormalizedTimeProperty(propertyBag, InternalSchema.MapiEndTime, InternalSchema.MapiPREndDate, exTimeZone);
			StartTimeZoneProperty.RecalculateNormalizedTimeProperty(propertyBag, InternalSchema.ReminderNextTime, null, exTimeZone);
			if (exTimeZone == ExTimeZone.UtcTimeZone)
			{
				if (!(propertyBag.Context.StoreObject is CalendarItemOccurrence))
				{
					propertyBag.Delete(InternalSchema.TimeZoneDefinitionStart);
					return;
				}
			}
			else
			{
				byte[] timeZoneBlob = O12TimeZoneFormatter.GetTimeZoneBlob(exTimeZone);
				propertyBag.SetValueWithFixup(InternalSchema.TimeZoneDefinitionStart, timeZoneBlob);
				StartTimeZoneProperty.SyncRecurringTimeZoneProperties(propertyBag, exTimeZone, timeZoneBlob);
			}
		}

		// Token: 0x06006EBF RID: 28351 RVA: 0x001DC80C File Offset: 0x001DAA0C
		internal static void SyncRecurringTimeZoneProperties(PropertyBag.BasicPropertyStore propertyBag, ExTimeZone timeZone, byte[] o12TimeZoneBlob)
		{
			if (o12TimeZoneBlob == null)
			{
				o12TimeZoneBlob = O12TimeZoneFormatter.GetTimeZoneBlob(timeZone);
			}
			if (propertyBag.GetValueOrDefault<bool>(InternalSchema.AppointmentRecurring) && timeZone != ExTimeZone.UtcTimeZone && timeZone != ExTimeZone.UnspecifiedTimeZone)
			{
				propertyBag.SetValueWithFixup(InternalSchema.TimeZone, timeZone.LocalizableDisplayName.ToString());
				propertyBag.SetValueWithFixup(InternalSchema.TimeZoneBlob, O11TimeZoneFormatter.GetTimeZoneBlob(timeZone));
				propertyBag.SetValueWithFixup(InternalSchema.TimeZoneDefinitionRecurring, o12TimeZoneBlob);
			}
		}

		// Token: 0x06006EC0 RID: 28352 RVA: 0x001DC884 File Offset: 0x001DAA84
		internal static void RecalculateNormalizedTimeProperty(PropertyBag.BasicPropertyStore propertyBag, GuidIdPropertyDefinition utcTimeProperty, PropertyTagPropertyDefinition legacyUtcTimeProperty, ExTimeZone newTimeZone)
		{
			if (newTimeZone == null || newTimeZone == ExTimeZone.UtcTimeZone || newTimeZone == ExTimeZone.UnspecifiedTimeZone)
			{
				return;
			}
			ExDateTime? normalizedTime = StartTimeProperty.GetNormalizedTime(propertyBag, utcTimeProperty, legacyUtcTimeProperty);
			if (normalizedTime != null)
			{
				ExDateTime exDateTime = newTimeZone.ConvertDateTime(normalizedTime.Value);
				propertyBag.SetValueWithFixup(utcTimeProperty, exDateTime.ToUtc());
				if (legacyUtcTimeProperty != null)
				{
					propertyBag.SetValueWithFixup(legacyUtcTimeProperty, exDateTime.ToUtc());
				}
			}
		}
	}
}

using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C2E RID: 3118
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class EndTimeZoneProperty : SmartPropertyDefinition
	{
		// Token: 0x06006EC1 RID: 28353 RVA: 0x001DC8F0 File Offset: 0x001DAAF0
		internal EndTimeZoneProperty() : base("CreationTimeZone", typeof(ExTimeZone), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiEndTime, PropertyDependencyType.NeedToReadForWrite),
			new PropertyDependency(InternalSchema.TimeZoneDefinitionEnd, PropertyDependencyType.AllRead)
		})
		{
		}

		// Token: 0x06006EC2 RID: 28354 RVA: 0x001DC93C File Offset: 0x001DAB3C
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			return StartTimeZoneProperty.GetExTimeZoneFromLegacyBlob(propertyBag, InternalSchema.TimeZoneDefinitionEnd);
		}

		// Token: 0x06006EC3 RID: 28355 RVA: 0x001DC94C File Offset: 0x001DAB4C
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			ExTimeZone exTimeZone = value as ExTimeZone;
			if (exTimeZone == ExTimeZone.UnspecifiedTimeZone)
			{
				throw new InvalidOperationException("unspecified time zone is not allowed to set");
			}
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
				ExDateTime valueOrDefault = propertyBag.GetValueOrDefault<ExDateTime>(InternalSchema.MapiEndTime, ExDateTime.UtcNow);
				propertyBag.SetValueWithFixup(InternalSchema.TimeZoneDefinitionEnd, (exTimeZone != null) ? O12TimeZoneFormatter.GetTimeZoneBlob(exTimeZone, valueOrDefault) : null);
			}
		}
	}
}

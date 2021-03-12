using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C38 RID: 3128
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class EndTimeZoneIdProperty : SmartPropertyDefinition
	{
		// Token: 0x06006EDF RID: 28383 RVA: 0x001DD2D4 File Offset: 0x001DB4D4
		internal EndTimeZoneIdProperty() : base("EndTimeZoneId", typeof(string), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.TimeZoneDefinitionStart, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.TimeZoneDefinitionEnd, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.TimeZoneDefinitionRecurring, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006EE0 RID: 28384 RVA: 0x001DD330 File Offset: 0x001DB530
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			ExDateTime? wallClockTime = StartWallClockProperty.GetWallClockTime(propertyBag, InternalSchema.MapiEndTime, InternalSchema.MapiPREndDate, new NativeStorePropertyDefinition[]
			{
				InternalSchema.TimeZoneDefinitionEnd,
				InternalSchema.TimeZoneDefinitionStart,
				InternalSchema.TimeZoneDefinitionRecurring
			});
			if (wallClockTime == null)
			{
				return new PropertyError(this, PropertyErrorCode.NotFound);
			}
			return wallClockTime.Value.TimeZone.Id;
		}
	}
}

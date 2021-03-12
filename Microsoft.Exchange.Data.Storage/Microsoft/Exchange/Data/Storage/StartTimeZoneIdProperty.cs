using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C37 RID: 3127
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class StartTimeZoneIdProperty : SmartPropertyDefinition
	{
		// Token: 0x06006EDD RID: 28381 RVA: 0x001DD214 File Offset: 0x001DB414
		internal StartTimeZoneIdProperty() : base("StartTimeZoneId", typeof(string), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.TimeZoneDefinitionStart, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.TimeZoneDefinitionEnd, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.TimeZoneDefinitionRecurring, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006EDE RID: 28382 RVA: 0x001DD270 File Offset: 0x001DB470
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			ExDateTime? wallClockTime = StartWallClockProperty.GetWallClockTime(propertyBag, InternalSchema.MapiStartTime, InternalSchema.MapiPRStartDate, new NativeStorePropertyDefinition[]
			{
				InternalSchema.TimeZoneDefinitionStart,
				InternalSchema.TimeZoneDefinitionRecurring,
				InternalSchema.TimeZoneDefinitionEnd
			});
			if (wallClockTime == null)
			{
				return new PropertyError(this, PropertyErrorCode.NotFound);
			}
			return wallClockTime.Value.TimeZone.Id;
		}
	}
}

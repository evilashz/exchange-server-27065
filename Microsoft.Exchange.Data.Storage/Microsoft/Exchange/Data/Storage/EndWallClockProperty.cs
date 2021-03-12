using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C36 RID: 3126
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class EndWallClockProperty : SmartPropertyDefinition
	{
		// Token: 0x06006EDB RID: 28379 RVA: 0x001DD140 File Offset: 0x001DB340
		internal EndWallClockProperty() : base("EndWallClock", typeof(ExDateTime), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiEndTime, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.MapiPREndDate, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.TimeZoneDefinitionStart, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.TimeZoneDefinitionEnd, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.TimeZoneDefinitionRecurring, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006EDC RID: 28380 RVA: 0x001DD1B8 File Offset: 0x001DB3B8
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
			return wallClockTime.Value;
		}
	}
}

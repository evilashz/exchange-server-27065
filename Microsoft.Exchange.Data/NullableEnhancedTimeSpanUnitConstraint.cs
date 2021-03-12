using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000179 RID: 377
	[Serializable]
	internal class NullableEnhancedTimeSpanUnitConstraint : EnhancedTimeSpanUnitConstraint
	{
		// Token: 0x06000C6D RID: 3181 RVA: 0x00026768 File Offset: 0x00024968
		public NullableEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan unit) : base(unit)
		{
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00026771 File Offset: 0x00024971
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value != null)
			{
				return base.Validate(value, propertyDefinition, propertyBag);
			}
			return null;
		}
	}
}

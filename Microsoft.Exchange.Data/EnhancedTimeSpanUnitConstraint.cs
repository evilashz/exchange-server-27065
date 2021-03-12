using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000140 RID: 320
	[Serializable]
	internal class EnhancedTimeSpanUnitConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x000228AD File Offset: 0x00020AAD
		public EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan unit)
		{
			if (EnhancedTimeSpan.Zero > unit)
			{
				throw new ArgumentException(DataStrings.ExceptionNegativeUnit, "unit");
			}
			this.unit = unit;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x000228DE File Offset: 0x00020ADE
		public EnhancedTimeSpan Unit
		{
			get
			{
				return this.unit;
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x000228E8 File Offset: 0x00020AE8
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			EnhancedTimeSpan t = (value is TimeSpan) ? ((TimeSpan)value) : ((EnhancedTimeSpan)value);
			if (EnhancedTimeSpan.Zero != t % this.unit)
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationDontMatchUnit(this.unit.ToString(), t.ToString()), propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x040006A0 RID: 1696
		private EnhancedTimeSpan unit;
	}
}

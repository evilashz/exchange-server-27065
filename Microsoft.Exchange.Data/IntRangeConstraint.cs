using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200017F RID: 383
	[Serializable]
	internal sealed class IntRangeConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000C97 RID: 3223 RVA: 0x00026F33 File Offset: 0x00025133
		public IntRangeConstraint(int minValue, int maxValue)
		{
			this.minimumValue = minValue;
			this.maximumValue = maxValue;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00026F4C File Offset: 0x0002514C
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			IntRange intRange = value as IntRange;
			if (intRange != null && (intRange.LowerBound < this.minimumValue || intRange.UpperBound > this.maximumValue))
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationInvalidIntRange(this.minimumValue, this.maximumValue, intRange.ToString()), propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x0400078F RID: 1935
		private readonly int minimumValue;

		// Token: 0x04000790 RID: 1936
		private readonly int maximumValue;
	}
}

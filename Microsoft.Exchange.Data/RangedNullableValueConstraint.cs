using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000189 RID: 393
	[Serializable]
	internal class RangedNullableValueConstraint<T> : RangedValueConstraint<T> where T : struct, IComparable
	{
		// Token: 0x06000CBB RID: 3259 RVA: 0x000279FB File Offset: 0x00025BFB
		public RangedNullableValueConstraint(T minValue, T maxValue) : base(minValue, maxValue)
		{
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00027A05 File Offset: 0x00025C05
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				return null;
			}
			return base.Validate((T)((object)value), propertyDefinition, propertyBag);
		}
	}
}

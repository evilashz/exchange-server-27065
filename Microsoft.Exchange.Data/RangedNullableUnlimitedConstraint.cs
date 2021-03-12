using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000188 RID: 392
	[Serializable]
	internal class RangedNullableUnlimitedConstraint<T> : RangedUnlimitedConstraint<T> where T : struct, IComparable
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x000279D7 File Offset: 0x00025BD7
		public RangedNullableUnlimitedConstraint(T minValue, T maxValue) : base(minValue, maxValue)
		{
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x000279E1 File Offset: 0x00025BE1
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				return null;
			}
			return base.Validate((Unlimited<T>)value, propertyDefinition, propertyBag);
		}
	}
}

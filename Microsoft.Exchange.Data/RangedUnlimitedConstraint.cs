using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000187 RID: 391
	[Serializable]
	internal class RangedUnlimitedConstraint<T> : RangedValueConstraint<T> where T : struct, IComparable
	{
		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002799A File Offset: 0x00025B9A
		public RangedUnlimitedConstraint(T minValue, T maxValue) : base(minValue, maxValue)
		{
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000279A4 File Offset: 0x00025BA4
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			Unlimited<T> unlimited = (Unlimited<T>)value;
			if (unlimited.IsUnlimited)
			{
				return null;
			}
			return base.Validate(unlimited.Value, propertyDefinition, propertyBag);
		}
	}
}

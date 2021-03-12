using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200016F RID: 367
	[Serializable]
	internal class MandatoryStringLengthConstraint : StringLengthConstraint
	{
		// Token: 0x06000C2F RID: 3119 RVA: 0x00025A64 File Offset: 0x00023C64
		public MandatoryStringLengthConstraint(int minLength, int maxLength) : base(minLength, maxLength)
		{
			if (minLength < 1)
			{
				throw new ArgumentException("minLength < 1");
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00025A80 File Offset: 0x00023C80
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			int num;
			if (value == null)
			{
				num = 0;
			}
			else
			{
				num = value.ToString().Length;
			}
			if (num == 0)
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationStringLengthIsEmpty, propertyDefinition, value, this);
			}
			return base.Validate(value, propertyDefinition, propertyBag);
		}
	}
}

using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200015C RID: 348
	[Serializable]
	internal class Int32ParsableNullableEmptiableStringConstraint : Int32ParsableStringConstraint
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x00023526 File Offset: 0x00021726
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value == null || value.ToString().Length == 0)
			{
				return null;
			}
			return base.Validate(value, propertyDefinition, propertyBag);
		}
	}
}

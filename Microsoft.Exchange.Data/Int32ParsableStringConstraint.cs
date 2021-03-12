using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200015B RID: 347
	[Serializable]
	internal class Int32ParsableStringConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000B48 RID: 2888 RVA: 0x000234E0 File Offset: 0x000216E0
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			int num;
			if (value == null || !int.TryParse(value.ToString(), out num))
			{
				return new PropertyConstraintViolationError(DataStrings.Int32ParsableStringConstraintViolation((value == null) ? "null" : value.ToString()), propertyDefinition, value, this);
			}
			return null;
		}
	}
}

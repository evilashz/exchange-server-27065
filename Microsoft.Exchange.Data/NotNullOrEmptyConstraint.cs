using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000176 RID: 374
	[Serializable]
	internal class NotNullOrEmptyConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x000266B8 File Offset: 0x000248B8
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			string value2 = value as string;
			if (string.IsNullOrEmpty(value2))
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationValueIsNullOrEmpty, propertyDefinition, value, this);
			}
			return null;
		}
	}
}

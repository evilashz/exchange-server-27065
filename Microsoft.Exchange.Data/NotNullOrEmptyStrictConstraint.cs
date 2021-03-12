using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000177 RID: 375
	[Serializable]
	internal class NotNullOrEmptyStrictConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000C68 RID: 3176 RVA: 0x000266EB File Offset: 0x000248EB
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				return new PropertyConstraintViolationError(DataStrings.PropertyNotEmptyOrNull, propertyDefinition, value, this);
			}
			return null;
		}
	}
}

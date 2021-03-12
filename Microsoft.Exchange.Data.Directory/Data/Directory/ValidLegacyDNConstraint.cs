using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020001AD RID: 429
	[Serializable]
	internal class ValidLegacyDNConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x060011FE RID: 4606 RVA: 0x0005779C File Offset: 0x0005599C
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (!LegacyDN.IsValidLegacyDN((string)value))
			{
				return new PropertyConstraintViolationError(DirectoryStrings.ConstraintViolationNotValidLegacyDN, propertyDefinition, value, this);
			}
			return null;
		}
	}
}

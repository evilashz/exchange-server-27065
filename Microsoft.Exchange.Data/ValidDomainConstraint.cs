using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001DE RID: 478
	[Serializable]
	internal class ValidDomainConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x0600109E RID: 4254 RVA: 0x00032446 File Offset: 0x00030646
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (!SmtpAddress.IsValidDomain((string)value))
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationNotValidDomain((string)value), propertyDefinition, value, this);
			}
			return null;
		}
	}
}

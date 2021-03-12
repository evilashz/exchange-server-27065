using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001DF RID: 479
	[Serializable]
	internal class ValidSmtpAddressConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x0600109F RID: 4255 RVA: 0x0003246C File Offset: 0x0003066C
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			SmtpAddress value2 = SmtpAddress.Empty;
			if (value is SmtpAddress)
			{
				value2 = (SmtpAddress)value;
			}
			else if (value is string)
			{
				value2 = (SmtpAddress)((string)value);
			}
			if (value2 != SmtpAddress.Empty && !value2.IsValidAddress)
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationNotValidEmailAddress(value2.ToString()), propertyDefinition, value, this);
			}
			return null;
		}
	}
}

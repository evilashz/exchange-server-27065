using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000137 RID: 311
	[Serializable]
	internal class DisallowStarSmtpDomainWithSubdomainsConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x00021588 File Offset: 0x0001F788
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			SmtpDomainWithSubdomains smtpDomainWithSubdomains = value as SmtpDomainWithSubdomains;
			if (smtpDomainWithSubdomains != null && smtpDomainWithSubdomains.IsStar)
			{
				return new PropertyConstraintViolationError(DataStrings.StarDomainNotAllowed(propertyDefinition.Name), propertyDefinition, value, this);
			}
			return null;
		}
	}
}

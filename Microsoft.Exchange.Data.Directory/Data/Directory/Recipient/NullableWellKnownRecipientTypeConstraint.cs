using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000256 RID: 598
	[Serializable]
	internal class NullableWellKnownRecipientTypeConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06001D2B RID: 7467 RVA: 0x00078F44 File Offset: 0x00077144
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			WellKnownRecipientType? wellKnownRecipientType = (WellKnownRecipientType?)value;
			if (wellKnownRecipientType != null && wellKnownRecipientType != WellKnownRecipientType.None && WellKnownRecipientType.AllRecipients != wellKnownRecipientType && (~(WellKnownRecipientType.MailboxUsers | WellKnownRecipientType.Resources | WellKnownRecipientType.MailContacts | WellKnownRecipientType.MailGroups | WellKnownRecipientType.MailUsers) & wellKnownRecipientType) != WellKnownRecipientType.None)
			{
				return new PropertyConstraintViolationError(DirectoryStrings.ConstraintViolationInvalidRecipientType(propertyDefinition.Name, value.ToString()), propertyDefinition, value, this);
			}
			return null;
		}
	}
}

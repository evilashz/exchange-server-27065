using System;
using System.Collections;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200013F RID: 319
	[Serializable]
	internal class EnabledEmailAddressTemplatesConstraint : CollectionPropertyDefinitionConstraint
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x0002286C File Offset: 0x00020A6C
		public override PropertyConstraintViolationError Validate(IEnumerable value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			ProxyAddressTemplateCollection proxyAddressTemplateCollection = (ProxyAddressTemplateCollection)value;
			if (proxyAddressTemplateCollection == null || proxyAddressTemplateCollection.FindPrimary(ProxyAddressPrefix.Smtp) == null)
			{
				return new PropertyConstraintViolationError(DataStrings.EapMustHaveOneEnabledPrimarySmtpAddressTemplate, propertyDefinition, value, this);
			}
			return null;
		}
	}
}

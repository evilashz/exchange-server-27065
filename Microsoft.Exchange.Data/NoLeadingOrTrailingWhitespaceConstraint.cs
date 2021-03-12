using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000175 RID: 373
	[Serializable]
	internal class NoLeadingOrTrailingWhitespaceConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000C65 RID: 3173 RVA: 0x00026664 File Offset: 0x00024864
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			string text = (value != null) ? value.ToString() : null;
			if (!string.IsNullOrEmpty(text) && (char.IsWhiteSpace(text[0]) || char.IsWhiteSpace(text[text.Length - 1])))
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationNoLeadingOrTrailingWhitespace, propertyDefinition, value, this);
			}
			return null;
		}
	}
}

using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200012D RID: 301
	[Serializable]
	internal class ContainingNonWhitespaceConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000A92 RID: 2706 RVA: 0x00020F60 File Offset: 0x0001F160
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			string text = (string)value;
			if (!string.IsNullOrEmpty(text))
			{
				bool flag = true;
				for (int i = 0; i < text.Length; i++)
				{
					if (!char.IsWhiteSpace(text[i]))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationStringDoesNotContainNonWhitespaceCharacter(text), propertyDefinition, value, this);
				}
			}
			return null;
		}
	}
}

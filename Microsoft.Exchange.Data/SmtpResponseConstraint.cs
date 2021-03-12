using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001A9 RID: 425
	[Serializable]
	internal class SmtpResponseConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x0002CEA4 File Offset: 0x0002B0A4
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			string text = value.ToString();
			if (string.IsNullOrEmpty(text))
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationValueIsNullOrEmpty, propertyDefinition, value, this);
			}
			for (int i = 0; i < text.Length; i++)
			{
				if (SmtpResponseConstraint.IsIllegal(text[i]) || SmtpResponseConstraint.IsBareCR(text, i) || SmtpResponseConstraint.IsBareLF(text, i))
				{
					return new PropertyConstraintViolationError(DataStrings.SmtpResponseConstraintViolation(propertyDefinition.Name, text), propertyDefinition, value, this);
				}
			}
			return null;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0002CF18 File Offset: 0x0002B118
		private static bool IsIllegal(char ch)
		{
			return ch < '\0' || ch > 'Ā' || (char.IsControl(ch) && ch != '\r' && ch != '\n');
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0002CF4A File Offset: 0x0002B14A
		private static bool IsBareCR(string s, int index)
		{
			return s[index] == '\r' && index + 1 < s.Length && s[index + 1] != '\n';
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0002CF74 File Offset: 0x0002B174
		private static bool IsBareLF(string s, int index)
		{
			return s[index] == '\n' && index > 0 && s[index - 1] != '\r';
		}
	}
}

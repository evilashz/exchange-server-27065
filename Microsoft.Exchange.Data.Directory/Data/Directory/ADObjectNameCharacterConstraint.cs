using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	internal sealed class ADObjectNameCharacterConstraint : CharacterConstraint
	{
		// Token: 0x060003BA RID: 954 RVA: 0x000159EE File Offset: 0x00013BEE
		public ADObjectNameCharacterConstraint(char[] invalidCharacters) : base(invalidCharacters, false)
		{
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000159F8 File Offset: 0x00013BF8
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			string text = (string)value;
			int num = -1;
			if (!string.IsNullOrEmpty(text) && ADObjectNameHelper.CheckIsUnicodeStringWellFormed(text, out num))
			{
				if (ADObjectNameHelper.ReservedADNameStringRegex.IsMatch(text))
				{
					return null;
				}
				return base.Validate(value, propertyDefinition, propertyBag);
			}
			else
			{
				if (num == -1)
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationStringLengthIsEmpty, propertyDefinition, value, this);
				}
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationStringContainsInvalidCharacters(text.Substring(num, 1), text), propertyDefinition, value, this);
			}
		}
	}
}

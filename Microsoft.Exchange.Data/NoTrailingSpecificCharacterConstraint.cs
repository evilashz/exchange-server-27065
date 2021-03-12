using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000178 RID: 376
	[Serializable]
	internal class NoTrailingSpecificCharacterConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x00026707 File Offset: 0x00024907
		public NoTrailingSpecificCharacterConstraint(char c)
		{
			this.invalidChar = c;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00026716 File Offset: 0x00024916
		public char InvalidChar
		{
			get
			{
				return this.invalidChar;
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00026720 File Offset: 0x00024920
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			string text = (string)value;
			if (!string.IsNullOrEmpty(text) && text[text.Length - 1] == this.invalidChar)
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintNoTrailingSpecificCharacter(text, this.invalidChar), propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x04000775 RID: 1909
		private char invalidChar;
	}
}

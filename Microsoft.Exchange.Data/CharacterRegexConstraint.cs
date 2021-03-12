using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	internal abstract class CharacterRegexConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000CDBF File Offset: 0x0000AFBF
		protected CharacterRegexConstraint(string pattern)
		{
			if (string.IsNullOrEmpty(pattern))
			{
				throw new ArgumentNullException("pattern");
			}
			this.pattern = pattern;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000CDE1 File Offset: 0x0000AFE1
		public string Pattern
		{
			get
			{
				return this.pattern;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000CDEC File Offset: 0x0000AFEC
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			string text = value.ToString();
			if (!string.IsNullOrEmpty(text) && !Regex.IsMatch(text, "^" + this.Pattern + "+$"))
			{
				LocalizedString description = this.CustomErrorMessage(text, propertyDefinition);
				return new PropertyConstraintViolationError(description, propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000CE39 File Offset: 0x0000B039
		protected virtual LocalizedString CustomErrorMessage(string value, PropertyDefinition propertyDefinition)
		{
			return DataStrings.ConstraintViolationStringDoesNotMatchRegularExpression(this.Pattern, value);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000CE48 File Offset: 0x0000B048
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			CharacterRegexConstraint characterRegexConstraint = obj as CharacterRegexConstraint;
			return characterRegexConstraint.pattern == this.pattern;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000CE78 File Offset: 0x0000B078
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.pattern.GetHashCode();
		}

		// Token: 0x0400011D RID: 285
		private string pattern;
	}
}

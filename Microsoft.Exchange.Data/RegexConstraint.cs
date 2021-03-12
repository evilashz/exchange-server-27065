using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200018E RID: 398
	[Serializable]
	internal class RegexConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000CD3 RID: 3283 RVA: 0x00027D72 File Offset: 0x00025F72
		public RegexConstraint(string pattern, LocalizedString patternDescription) : this(pattern, RegexOptions.None, patternDescription)
		{
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00027D7D File Offset: 0x00025F7D
		public RegexConstraint(string pattern, RegexOptions options, LocalizedString patternDescription)
		{
			this.pattern = pattern;
			this.options = options;
			this.patternDescription = patternDescription;
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00027D9A File Offset: 0x00025F9A
		public string Pattern
		{
			get
			{
				return this.pattern;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00027DA2 File Offset: 0x00025FA2
		public RegexOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00027DAA File Offset: 0x00025FAA
		public LocalizedString PatternDescription
		{
			get
			{
				return this.patternDescription;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00027DB2 File Offset: 0x00025FB2
		private Regex Constraint
		{
			get
			{
				if (this.constraint == null)
				{
					this.constraint = new Regex(this.pattern, this.options);
				}
				return this.constraint;
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00027DDC File Offset: 0x00025FDC
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			try
			{
				string text = (string)value;
				if (text != null && !this.Constraint.IsMatch(text))
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationStringDoesNotMatchRegularExpression(this.patternDescription.ToString(), text), propertyDefinition, value, this);
				}
			}
			catch (OutOfMemoryException)
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationStringLengthCauseOutOfMemory, propertyDefinition, null, this);
			}
			return null;
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00027E4C File Offset: 0x0002604C
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			RegexConstraint regexConstraint = obj as RegexConstraint;
			return StringComparer.OrdinalIgnoreCase.Equals(regexConstraint.pattern, this.pattern) && regexConstraint.options == this.options;
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00027E93 File Offset: 0x00026093
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040007D9 RID: 2009
		private string pattern;

		// Token: 0x040007DA RID: 2010
		private RegexOptions options;

		// Token: 0x040007DB RID: 2011
		[NonSerialized]
		private Regex constraint;

		// Token: 0x040007DC RID: 2012
		private LocalizedString patternDescription;
	}
}

using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000097 RID: 151
	[Serializable]
	internal class UIImpactStringLengthConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x0000F9A3 File Offset: 0x0000DBA3
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			return null;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000F9A6 File Offset: 0x0000DBA6
		public UIImpactStringLengthConstraint(int minLength, int maxLength)
		{
			if (minLength > maxLength && maxLength != 0)
			{
				throw new ArgumentException("minLength > maxLength");
			}
			if (minLength < 0)
			{
				throw new ArgumentException("minLength < 0");
			}
			this.minLength = minLength;
			this.maxLength = maxLength;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000F9DD File Offset: 0x0000DBDD
		public int MinLength
		{
			get
			{
				return this.minLength;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000F9E5 File Offset: 0x0000DBE5
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			UIImpactStringLengthConstraint uiimpactStringLengthConstraint = obj as UIImpactStringLengthConstraint;
			return uiimpactStringLengthConstraint.MinLength == this.MinLength && uiimpactStringLengthConstraint.MaxLength == this.MaxLength;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000FA2D File Offset: 0x0000DC2D
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.MinLength ^ this.MaxLength;
		}

		// Token: 0x04000226 RID: 550
		private int minLength;

		// Token: 0x04000227 RID: 551
		private int maxLength;
	}
}

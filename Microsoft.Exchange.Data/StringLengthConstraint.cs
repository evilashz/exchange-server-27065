using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000096 RID: 150
	[Serializable]
	internal class StringLengthConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x0000F896 File Offset: 0x0000DA96
		public StringLengthConstraint(int minLength, int maxLength)
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

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000F8CD File Offset: 0x0000DACD
		public int MinLength
		{
			get
			{
				return this.minLength;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000F8D5 File Offset: 0x0000DAD5
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (value != null)
			{
				string text = value.ToString();
				if (text.Length < this.minLength)
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationStringLengthTooShort(this.minLength, text.Length), propertyDefinition, value, this);
				}
				if (this.maxLength != 0 && text.Length > this.maxLength)
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationStringLengthTooLong(this.maxLength, text.Length), propertyDefinition, value, this);
				}
			}
			return null;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000F950 File Offset: 0x0000DB50
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			StringLengthConstraint stringLengthConstraint = obj as StringLengthConstraint;
			return stringLengthConstraint.MinLength == this.MinLength && stringLengthConstraint.MaxLength == this.MaxLength;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000F98D File Offset: 0x0000DB8D
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.MinLength ^ this.MaxLength;
		}

		// Token: 0x04000224 RID: 548
		private int minLength;

		// Token: 0x04000225 RID: 549
		private int maxLength;
	}
}

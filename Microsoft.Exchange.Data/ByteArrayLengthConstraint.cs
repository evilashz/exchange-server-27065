using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	internal class ByteArrayLengthConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000BF0F File Offset: 0x0000A10F
		public int MinLength
		{
			get
			{
				return this.minLength;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000BF17 File Offset: 0x0000A117
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000BF20 File Offset: 0x0000A120
		public ByteArrayLengthConstraint(int minLength, int maxLength)
		{
			if (minLength > maxLength && maxLength != 0)
			{
				throw new ArgumentException(DataStrings.ConstraintViolationObjectIsBeyondRange(minLength.ToString()));
			}
			if (minLength < 0)
			{
				throw new ArgumentException(DataStrings.ConstraintViolationObjectIsBelowRange("0"));
			}
			this.minLength = minLength;
			this.maxLength = maxLength;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000BF78 File Offset: 0x0000A178
		public ByteArrayLengthConstraint(int length) : this(length, length)
		{
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000BF84 File Offset: 0x0000A184
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			byte[] array = (byte[])value;
			if (array != null)
			{
				if (array.Length < this.minLength)
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationByteArrayLengthTooShort(this.minLength, array.Length), propertyDefinition, value, this);
				}
				if (this.maxLength != 0 && array.Length > this.maxLength)
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationByteArrayLengthTooLong(this.maxLength, array.Length), propertyDefinition, value, this);
				}
			}
			return null;
		}

		// Token: 0x040000FF RID: 255
		private int minLength;

		// Token: 0x04000100 RID: 256
		private int maxLength;
	}
}

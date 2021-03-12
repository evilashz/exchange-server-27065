using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200018A RID: 394
	internal class RangedValuePerFileExtensionListConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06000CBD RID: 3261 RVA: 0x00027A1F File Offset: 0x00025C1F
		public RangedValuePerFileExtensionListConstraint(int minValue, int maxValue, int maxLength, char extensionValueDelimiter, char pairDelimiter)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentException("minValue > maxValue");
			}
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.maxLength = maxLength;
			this.extensionValueDelimiter = extensionValueDelimiter;
			this.pairDelimiter = pairDelimiter;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00027A5B File Offset: 0x00025C5B
		public int MinValue
		{
			get
			{
				return this.minValue;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x00027A63 File Offset: 0x00025C63
		public int MaxValue
		{
			get
			{
				return this.maxValue;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00027A6B File Offset: 0x00025C6B
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00027A73 File Offset: 0x00025C73
		public char ExtensionValueDelimiter
		{
			get
			{
				return this.extensionValueDelimiter;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00027A7B File Offset: 0x00025C7B
		public char PairDelimiter
		{
			get
			{
				return this.pairDelimiter;
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00027A84 File Offset: 0x00025C84
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			string text = (string)value;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			if (text.Length > this.maxLength)
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationObjectIsBeyondRange(this.maxLength.ToString()), propertyDefinition, value, this);
			}
			int num = 0;
			if (!int.TryParse(text, out num))
			{
				char[] separator = new char[]
				{
					this.pairDelimiter
				};
				char[] separator2 = new char[]
				{
					this.extensionValueDelimiter
				};
				string[] array = text.Split(separator);
				string[] array2 = array;
				int i = 0;
				while (i < array2.Length)
				{
					string text2 = array2[i];
					string[] array3 = text2.Split(separator2);
					PropertyConstraintViolationError result;
					if (array3.Length != 2)
					{
						result = new PropertyConstraintViolationError(DataStrings.ConstraintViolationMalformedExtensionValuePair(text2), propertyDefinition, value, this);
					}
					else if (array3[0].Length == 0)
					{
						result = new PropertyConstraintViolationError(DataStrings.ConstraintViolationMalformedExtensionValuePair(text2), propertyDefinition, value, this);
					}
					else
					{
						int num2 = 0;
						if (!int.TryParse(array3[1], out num2))
						{
							result = new PropertyConstraintViolationError(DataStrings.ConstraintViolationMalformedExtensionValuePair(text2), propertyDefinition, value, this);
						}
						else
						{
							if (num2 >= this.minValue && num2 <= this.maxValue)
							{
								i++;
								continue;
							}
							result = new PropertyConstraintViolationError(DataStrings.ConstraintViolationValueOutOfRange(this.minValue.ToString(), this.maxValue.ToString(), num2.ToString()), propertyDefinition, value, this);
						}
					}
					return result;
				}
				return null;
			}
			if (num < this.minValue)
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationObjectIsBelowRange(this.minValue.ToString()), propertyDefinition, value, this);
			}
			if (num > this.maxValue)
			{
				return new PropertyConstraintViolationError(DataStrings.ConstraintViolationObjectIsBeyondRange(this.maxValue.ToString()), propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x040007CD RID: 1997
		private int minValue;

		// Token: 0x040007CE RID: 1998
		private int maxValue;

		// Token: 0x040007CF RID: 1999
		private int maxLength;

		// Token: 0x040007D0 RID: 2000
		private char extensionValueDelimiter;

		// Token: 0x040007D1 RID: 2001
		private char pairDelimiter;
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000091 RID: 145
	[Serializable]
	internal class RangedValueConstraint<T> : PropertyDefinitionConstraint where T : struct, IComparable
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x0000E7E4 File Offset: 0x0000C9E4
		public RangedValueConstraint(T minValue, T maxValue)
		{
			if (minValue.CompareTo(maxValue) > 0)
			{
				throw new ArgumentException("minValue > maxValue");
			}
			this.minValue = minValue;
			this.maxValue = maxValue;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000E831 File Offset: 0x0000CA31
		public RangedValueConstraint(T minValue, T maxValue, LocalizedString customErrorMessage) : this(minValue, maxValue)
		{
			this.customErrorMessage = customErrorMessage;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000E842 File Offset: 0x0000CA42
		public T MinimumValue
		{
			get
			{
				return this.minValue;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000E84A File Offset: 0x0000CA4A
		public T MaximumValue
		{
			get
			{
				return this.maxValue;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000E854 File Offset: 0x0000CA54
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			if (Comparer<T>.Default.Compare(this.MinimumValue, (T)((object)value)) > 0 || Comparer<T>.Default.Compare(this.MaximumValue, (T)((object)value)) < 0)
			{
				string arg;
				string arg2;
				string text;
				if (typeof(T).GetTypeInfo().IsEnum)
				{
					if (Enum.IsDefined(typeof(T), this.MinimumValue))
					{
						arg = string.Format("{0}:{1}", (this.MinimumValue as Enum).ToString("G"), (this.MinimumValue as Enum).ToString("D"));
					}
					else
					{
						arg = (this.MinimumValue as Enum).ToString("D");
					}
					if (Enum.IsDefined(typeof(T), this.MaximumValue))
					{
						arg2 = string.Format("{0}:{1}", (this.MaximumValue as Enum).ToString("G"), (this.MaximumValue as Enum).ToString("D"));
					}
					else
					{
						arg2 = (this.MaximumValue as Enum).ToString("D");
					}
					if (Enum.IsDefined(typeof(T), value))
					{
						text = string.Format("{0}:{1}", (value as Enum).ToString("G"), (value as Enum).ToString("D"));
					}
					else
					{
						text = (((T)((object)value)) as Enum).ToString("D");
					}
				}
				else
				{
					T minimumValue = this.MinimumValue;
					arg = minimumValue.ToString();
					T maximumValue = this.MaximumValue;
					arg2 = maximumValue.ToString();
					text = value.ToString();
				}
				LocalizedString empty = LocalizedString.Empty;
				if (!this.customErrorMessage.IsEmpty)
				{
					empty = new LocalizedString(string.Format(this.customErrorMessage.ToString(), arg, arg2, text));
				}
				return new PropertyConstraintViolationError((empty == LocalizedString.Empty) ? DataStrings.ConstraintViolationValueOutOfRange(arg, arg2, text) : empty, propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000EA83 File Offset: 0x0000CC83
		public override string ToString()
		{
			return string.Format("[{0},{1}]", this.MinimumValue, this.MaximumValue);
		}

		// Token: 0x0400020C RID: 524
		private T minValue;

		// Token: 0x0400020D RID: 525
		private T maxValue;

		// Token: 0x0400020E RID: 526
		private LocalizedString customErrorMessage = LocalizedString.Empty;
	}
}

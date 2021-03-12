using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	internal class ComparisonFilter : SinglePropertyFilter
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x0000788C File Offset: 0x00005A8C
		public ComparisonFilter(ComparisonOperator comparisonOperator, PropertyDefinition property, object propertyValue) : base(property)
		{
			if (comparisonOperator != ComparisonOperator.Equal && ComparisonOperator.NotEqual != comparisonOperator)
			{
				Type type = Nullable.GetUnderlyingType(property.Type) ?? property.Type;
				if (!typeof(IComparable).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()) && !typeof(string[]).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()) && !typeof(byte[]).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
				{
					throw new ArgumentOutOfRangeException(DataStrings.ExceptionComparisonNotSupported(property.Name, property.Type, comparisonOperator));
				}
			}
			this.comparisonOperator = comparisonOperator;
			this.propertyValue = propertyValue;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00007942 File Offset: 0x00005B42
		public ComparisonOperator ComparisonOperator
		{
			get
			{
				return this.comparisonOperator;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000794A File Offset: 0x00005B4A
		public object PropertyValue
		{
			get
			{
				return this.propertyValue;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007952 File Offset: 0x00005B52
		public override SinglePropertyFilter CloneWithAnotherProperty(PropertyDefinition property)
		{
			base.CheckClonable(property);
			return new ComparisonFilter(this.comparisonOperator, property, this.propertyValue);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007970 File Offset: 0x00005B70
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(");
			sb.Append(base.Property.Name);
			sb.Append(" ");
			sb.Append(this.comparisonOperator.ToString());
			sb.Append(" ");
			IFormattable formattable = this.propertyValue as IFormattable;
			if (formattable != null)
			{
				sb.Append(formattable.ToString(null, CultureInfo.InvariantCulture));
			}
			else
			{
				sb.Append(this.propertyValue ?? "<null>");
			}
			sb.Append(")");
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007A10 File Offset: 0x00005C10
		public override bool Equals(object obj)
		{
			ComparisonFilter comparisonFilter = obj as ComparisonFilter;
			return comparisonFilter != null && this.comparisonOperator == comparisonFilter.comparisonOperator && comparisonFilter.GetType() == base.GetType() && ComparisonFilter.PropertyValueEquals(this.propertyValue, comparisonFilter.propertyValue) && base.Equals(obj);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007A64 File Offset: 0x00005C64
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ (int)this.comparisonOperator;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007A74 File Offset: 0x00005C74
		private static bool PropertyValueEquals(object value1, object value2)
		{
			if (object.Equals(value1, value2))
			{
				return true;
			}
			if (value1 is Enum || value2 is Enum)
			{
				IConvertible convertible = value1 as IConvertible;
				IConvertible convertible2 = value2 as IConvertible;
				if (convertible != null && convertible2 != null)
				{
					try
					{
						ulong num = convertible.ToUInt64(null);
						ulong num2 = convertible2.ToUInt64(null);
						return num == num2;
					}
					catch (OverflowException)
					{
					}
					catch (FormatException)
					{
						return false;
					}
					try
					{
						long num3 = convertible.ToInt64(null);
						long num4 = convertible2.ToInt64(null);
						return num3 == num4;
					}
					catch (OverflowException)
					{
						return false;
					}
					catch (FormatException)
					{
						return false;
					}
					return false;
				}
				return false;
			}
			IList list = value1 as IList;
			IList list2 = value2 as IList;
			if (list != null && list2 != null)
			{
				bool flag = list2.Count == list.Count;
				int num5 = 0;
				while (flag && num5 < list.Count)
				{
					flag = ComparisonFilter.PropertyValueEquals(list[num5], list2[num5]);
					num5++;
				}
				return flag;
			}
			return false;
		}

		// Token: 0x04000097 RID: 151
		private readonly ComparisonOperator comparisonOperator;

		// Token: 0x04000098 RID: 152
		private readonly object propertyValue;
	}
}

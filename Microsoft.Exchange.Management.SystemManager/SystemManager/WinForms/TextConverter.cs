using System;
using System.Collections;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200016F RID: 367
	internal class TextConverter : ICustomTextConverter, ICustomFormatter
	{
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0003A5E2 File Offset: 0x000387E2
		protected virtual string NullValueText
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0003A5EC File Offset: 0x000387EC
		string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider)
		{
			string result = this.NullValueText;
			arg = this.PreFormatObject(arg, format, formatProvider);
			if (arg is string)
			{
				result = (string)arg;
			}
			else if (arg != null && !DBNull.Value.Equals(arg))
			{
				result = this.FormatObject(format, arg, formatProvider);
			}
			return result;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0003A638 File Offset: 0x00038838
		object ICustomTextConverter.Parse(Type resultType, string s, IFormatProvider provider)
		{
			if (resultType == null)
			{
				throw new ArgumentNullException("resultType");
			}
			s = (s ?? string.Empty);
			object obj = this.PreParseObject(resultType, s, provider);
			if (obj != null && obj.GetType() != resultType)
			{
				if (obj.GetType() == typeof(string))
				{
					obj = this.ParseObject((string)obj, provider);
				}
				if (obj != null && obj.GetType() != resultType)
				{
					try
					{
						obj = ValueConvertor.ConvertValue(obj, resultType, provider);
					}
					catch (NotImplementedException)
					{
					}
				}
			}
			return obj;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0003A6D8 File Offset: 0x000388D8
		protected virtual string FormatObject(string format, object arg, IFormatProvider formatProvider)
		{
			string result = this.NullValueText;
			Type type = arg.GetType();
			if (type == typeof(DateTime) && format == null)
			{
				format = "F";
			}
			if (type.IsEnum)
			{
				result = LocalizedDescriptionAttribute.FromEnum(arg.GetType(), arg);
			}
			else if (typeof(IFormattable).IsAssignableFrom(type))
			{
				result = ((IFormattable)arg).ToString(format, formatProvider);
			}
			else if (type == typeof(bool))
			{
				result = (((bool)arg) ? Strings.TrueString : Strings.FalseString);
			}
			else if (type == typeof(double) || type == typeof(float))
			{
				result = ((double)arg).ToString(TextConverter.DoubleFormatString);
			}
			else
			{
				result = arg.ToString();
			}
			return result;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0003A7BB File Offset: 0x000389BB
		protected virtual object ParseObject(string s, IFormatProvider provider)
		{
			return s;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0003A7C0 File Offset: 0x000389C0
		private object PreFormatObject(object obj, string format, IFormatProvider formatProvider)
		{
			if (obj == null)
			{
				return null;
			}
			Type type = obj.GetType();
			if (typeof(IList).IsAssignableFrom(type))
			{
				obj = this.ConvertListToString(obj as IList, format, formatProvider);
			}
			else if (type.IsGenericType)
			{
				if (type.GetGenericTypeDefinition() == typeof(Unlimited<>))
				{
					if (!(bool)TextConverter.GetPropertyValue(obj.GetType(), obj, "IsUnlimited"))
					{
						obj = TextConverter.GetPropertyValue(obj.GetType(), obj, "Value");
					}
					else
					{
						obj = Strings.UnlimitedString.ToString();
					}
				}
				else if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					if (!(bool)TextConverter.GetPropertyValue(obj.GetType(), obj, "HasValue"))
					{
						obj = TextConverter.GetPropertyValue(obj.GetType(), obj, "Value");
					}
					else
					{
						obj = this.NullValueText;
					}
				}
			}
			return obj;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0003A8B4 File Offset: 0x00038AB4
		private object PreParseObject(Type resultType, string s, IFormatProvider formatProvider)
		{
			object result = s;
			if (typeof(IList).IsAssignableFrom(resultType))
			{
				throw new NotSupportedException();
			}
			if (resultType.IsGenericType)
			{
				Type type = resultType.GetGenericArguments()[0];
				if (resultType.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Unlimited<>))
					{
						resultType = type;
						type = resultType.GetGenericArguments()[0];
					}
					else if (string.IsNullOrEmpty(s))
					{
						result = null;
					}
					else
					{
						result = ValueConvertor.ConvertValue(this.Parse(type, s, formatProvider), resultType, formatProvider);
					}
				}
				if (resultType.GetGenericTypeDefinition() == typeof(Unlimited<>))
				{
					if (string.Compare(Strings.UnlimitedString.ToString(), s) == 0)
					{
						result = TextConverter.GetPropertyValue(resultType, null, "UnlimitedValue");
					}
					else
					{
						result = ValueConvertor.ConvertValue(this.Parse(type, s, formatProvider), resultType, formatProvider);
					}
				}
			}
			else if (resultType == typeof(bool) && s != null)
			{
				bool flag;
				if (bool.TryParse(s, out flag))
				{
					result = flag;
				}
				else if (string.Compare(s, Strings.TrueString) == 0)
				{
					result = true;
				}
				else if (string.Compare(s, Strings.FalseString) == 0)
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003AA04 File Offset: 0x00038C04
		private string ConvertListToString(IList list, string format, IFormatProvider formatProvider)
		{
			string result = string.Empty;
			if (list.Count > 0)
			{
				int num = Math.Min(list.Count, 10);
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < num; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(((ICustomFormatter)this).Format(format, list[i], formatProvider));
				}
				if (num < list.Count)
				{
					stringBuilder.Append(" [...]");
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0003AA82 File Offset: 0x00038C82
		private static object GetPropertyValue(Type t, object obj, string propertyName)
		{
			return t.GetProperty(propertyName).GetValue(obj, null);
		}

		// Token: 0x04000602 RID: 1538
		private const int maxEntriesInToString = 10;

		// Token: 0x04000603 RID: 1539
		public static string DoubleFormatString = "0.######";

		// Token: 0x04000604 RID: 1540
		public static readonly ICustomTextConverter DefaultConverter = new TextConverter();
	}
}

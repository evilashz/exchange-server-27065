using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x02000B02 RID: 2818
	internal static class Int32EnumFormatter<T>
	{
		// Token: 0x06003CA0 RID: 15520 RVA: 0x0009DE5C File Offset: 0x0009C05C
		private static Dictionary<int, string> CreateValueNameMap()
		{
			Int32EnumFormatter<T>.enumType = typeof(T);
			if (!Int32EnumFormatter<T>.enumType.IsEnum)
			{
				throw new InvalidOperationException("Type is not enum");
			}
			if (Enum.GetUnderlyingType(Int32EnumFormatter<T>.enumType) != typeof(int))
			{
				throw new InvalidOperationException("Enum underlying type is not int");
			}
			Array values = Enum.GetValues(Int32EnumFormatter<T>.enumType);
			Dictionary<int, string> dictionary = new Dictionary<int, string>(values.Length);
			foreach (object obj in values)
			{
				string name = Enum.GetName(Int32EnumFormatter<T>.enumType, obj);
				dictionary[(int)obj] = name;
			}
			return dictionary;
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x0009DF28 File Offset: 0x0009C128
		public static string Format(int value)
		{
			string result;
			if (Int32EnumFormatter<T>.dictionary.TryGetValue(value, out result))
			{
				return result;
			}
			return value.ToString();
		}

		// Token: 0x04003548 RID: 13640
		private static Type enumType;

		// Token: 0x04003549 RID: 13641
		private static Dictionary<int, string> dictionary = Int32EnumFormatter<T>.CreateValueNameMap();
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000073 RID: 115
	internal static class ExtensionMethods
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x0000EEA4 File Offset: 0x0000D0A4
		public static List<TOutput> ConvertAll<TInput, TOutput>(this ICollection<TInput> inputCollection, Converter<TInput, TOutput> converter)
		{
			List<TOutput> list = null;
			if (inputCollection != null)
			{
				list = new List<TOutput>(inputCollection.Count);
				foreach (TInput input in inputCollection)
				{
					list.Add(converter(input));
				}
			}
			return list;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000EF04 File Offset: 0x0000D104
		public static List<TOutput> ConvertAll<TInput, TOutput>(this IEnumerable<TInput> inputCollection, Converter<TInput, TOutput> converter)
		{
			List<TOutput> list = null;
			if (inputCollection != null)
			{
				list = new List<TOutput>();
				foreach (TInput input in inputCollection)
				{
					list.Add(converter(input));
				}
			}
			return list;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000EF60 File Offset: 0x0000D160
		public static string ToString<T>(this IList<T> list, string delimiter)
		{
			ValidateArgument.NotNull(delimiter, "delimiter");
			StringBuilder stringBuilder = new StringBuilder();
			foreach (T t in list)
			{
				object value = t;
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(delimiter);
				}
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000037 RID: 55
	internal class EnumConverter<T, S> where T : struct where S : struct
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00007C34 File Offset: 0x00005E34
		public static T Convert(S enumToConvert)
		{
			T result;
			if (!EnumConverter<T, S>.TryConvert(enumToConvert, out result))
			{
				throw new InvalidOperationException(string.Format("Enum {0} in type {1} cannot be converted to type {2}", enumToConvert, typeof(S), typeof(T)));
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007C78 File Offset: 0x00005E78
		public static S Convert(T enumToConvert)
		{
			S result;
			if (!EnumConverter<T, S>.TryConvert(enumToConvert, out result))
			{
				throw new InvalidOperationException(string.Format("Enum {0} in type {1} cannot be converted to type {2}", enumToConvert, typeof(T), typeof(S)));
			}
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007CBA File Offset: 0x00005EBA
		public static bool TryConvert(S enumToConvert, out T enumConverted)
		{
			EnumConverter<T, S>.InitializeIfNeccessary();
			return EnumConverter<T, S>.stMapping.TryGetValue(enumToConvert, out enumConverted);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007CCD File Offset: 0x00005ECD
		public static bool TryConvert(T enumToConvert, out S enumConverted)
		{
			EnumConverter<T, S>.InitializeIfNeccessary();
			return EnumConverter<T, S>.tsMapping.TryGetValue(enumToConvert, out enumConverted);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007CE0 File Offset: 0x00005EE0
		private static void InitializeIfNeccessary()
		{
			if (EnumConverter<T, S>.tsMapping == null)
			{
				lock (EnumConverter<T, S>.staticInitLock)
				{
					if (EnumConverter<T, S>.tsMapping == null)
					{
						string[] names = Enum.GetNames(typeof(T));
						string[] names2 = Enum.GetNames(typeof(S));
						EnumConverter<T, S>.tsMapping = EnumConverter<T, S>.Populate<T, S>(names, names2);
						EnumConverter<T, S>.stMapping = EnumConverter<T, S>.Populate<S, T>(names2, names);
					}
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00007D60 File Offset: 0x00005F60
		private static Dictionary<U, V> Populate<U, V>(string[] stringNamesOfU, string[] stringNamesOfV) where U : struct where V : struct
		{
			Dictionary<U, V> dictionary = new Dictionary<U, V>(stringNamesOfU.Length);
			foreach (string value in stringNamesOfU)
			{
				V value2;
				if (EnumValidator<V>.TryParse(value, EnumParseOptions.Default, out value2))
				{
					U key = EnumValidator<U>.Parse(value, EnumParseOptions.Default);
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, value2);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x040000DE RID: 222
		private static Dictionary<T, S> tsMapping;

		// Token: 0x040000DF RID: 223
		private static Dictionary<S, T> stMapping;

		// Token: 0x040000E0 RID: 224
		private static object staticInitLock = new object();
	}
}

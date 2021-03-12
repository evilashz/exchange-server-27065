using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000C5 RID: 197
	public static class ValueHelper
	{
		// Token: 0x060008E2 RID: 2274 RVA: 0x0001A82C File Offset: 0x00018A2C
		public static T[] ShallowCopyArray<T>(T[] original)
		{
			T[] array = null;
			if (original != null)
			{
				array = new T[original.Length];
				Array.Copy(original, 0, array, 0, original.Length);
			}
			return array;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001A874 File Offset: 0x00018A74
		public static void SortAndRemoveDuplicates(List<string> list, CompareInfo compareInfo, CompareOptions compareOptions)
		{
			if (list != null && list.Count > 0)
			{
				list.Sort((string x, string y) => ValueHelper.StringsCompare(x, y, compareInfo, compareOptions));
				int num = 0;
				for (int i = 1; i < list.Count; i++)
				{
					if (ValueHelper.StringsCompare(list[num], list[i], compareInfo, compareOptions) != 0)
					{
						num++;
						if (num < i)
						{
							list[num] = list[i];
						}
					}
				}
				int num2 = num + 1;
				if (num2 != list.Count)
				{
					list.RemoveRange(num2, list.Count - num2);
				}
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001A92C File Offset: 0x00018B2C
		public static void SortAndRemoveDuplicates<T>(List<T> list) where T : IComparable<T>, IEquatable<T>
		{
			ValueHelper.SortAndRemoveDuplicates<T>(list, Comparer<T>.Default);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001A93C File Offset: 0x00018B3C
		public static void SortAndRemoveDuplicates<T>(List<T> list, IComparer<T> comparer)
		{
			if (list != null && list.Count > 1)
			{
				list.Sort(comparer);
				int num = 0;
				for (int i = 1; i < list.Count; i++)
				{
					if (comparer.Compare(list[num], list[i]) != 0)
					{
						num++;
						if (num < i)
						{
							list[num] = list[i];
						}
					}
				}
				int num2 = num + 1;
				if (num2 != list.Count)
				{
					list.RemoveRange(num2, list.Count - num2);
				}
			}
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001A9B8 File Offset: 0x00018BB8
		public static object TruncateValueIfNecessary(object value, Type columnType, int maxLength, out bool valueTruncated)
		{
			valueTruncated = false;
			if (value != null)
			{
				if (columnType == typeof(string))
				{
					string text = (string)value;
					if (text.Length > maxLength)
					{
						value = text.Substring(0, maxLength);
						valueTruncated = true;
					}
				}
				else if (columnType == typeof(byte[]))
				{
					byte[] array = (byte[])value;
					if (array.Length > maxLength)
					{
						byte[] array2 = new byte[maxLength];
						Buffer.BlockCopy(array, 0, array2, 0, maxLength);
						value = array2;
						valueTruncated = true;
					}
				}
			}
			return value;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001AA33 File Offset: 0x00018C33
		public static int StringsCompare(string x, string y, CompareInfo compareInfo, CompareOptions compareOptions)
		{
			return compareInfo.Compare(x, y, compareOptions);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001AA3E File Offset: 0x00018C3E
		public static int ValuesCompare(object x, object y)
		{
			return ValueHelper.ValuesCompare(x, y, CultureInfo.InvariantCulture.CompareInfo, CompareOptions.Ordinal);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001AA58 File Offset: 0x00018C58
		public static int ValuesCompare(object x, object y, CompareInfo compareInfo, CompareOptions compareOptions)
		{
			if (object.ReferenceEquals(x, y))
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			Array array = x as Array;
			if (array != null)
			{
				if (x is byte[])
				{
					return ValueHelper.ArraysCompare<byte>((byte[])x, (byte[])y);
				}
				switch (ValueTypeHelper.GetExtendedTypeCodeNoAssert(x.GetType().GetElementType()))
				{
				case ExtendedTypeCode.Boolean:
					return ValueHelper.ArraysCompare<bool>((bool[])x, (bool[])y);
				case ExtendedTypeCode.Int16:
					return ValueHelper.ArraysCompare<short>((short[])x, (short[])y);
				case ExtendedTypeCode.Int32:
					return ValueHelper.ArraysCompare<int>((int[])x, (int[])y);
				case ExtendedTypeCode.Int64:
					return ValueHelper.ArraysCompare<long>((long[])x, (long[])y);
				case ExtendedTypeCode.Single:
					return ValueHelper.ArraysCompare<float>((float[])x, (float[])y);
				case ExtendedTypeCode.Double:
					return ValueHelper.ArraysCompare<double>((double[])x, (double[])y);
				case ExtendedTypeCode.DateTime:
					return ValueHelper.ArraysCompare<DateTime>((DateTime[])x, (DateTime[])y);
				case ExtendedTypeCode.Guid:
					return ValueHelper.ArraysCompare<Guid>((Guid[])x, (Guid[])y);
				case ExtendedTypeCode.String:
					return ValueHelper.ArraysCompare((string[])x, (string[])y, compareInfo, compareOptions);
				case ExtendedTypeCode.Binary:
					return ValueHelper.JaggedArraysCompare<byte>((byte[][])x, (byte[][])y);
				default:
					throw new ArgumentException(string.Format("arrays of {0} are not supported", x.GetType().GetElementType()));
				}
			}
			else
			{
				if (y is string)
				{
					return ValueHelper.StringsCompare((string)x, (string)y, compareInfo, compareOptions);
				}
				return ((IComparable)x).CompareTo(y);
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001ABE0 File Offset: 0x00018DE0
		public static bool ValuesEqual(object x, object y)
		{
			return ValueHelper.ValuesEqual(x, y, CultureInfo.InvariantCulture.CompareInfo, CompareOptions.Ordinal);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001ABF8 File Offset: 0x00018DF8
		public static bool ValuesEqual(object x, object y, CompareInfo compareInfo, CompareOptions compareOptions)
		{
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}
			if (x == null)
			{
				return false;
			}
			Array array = x as Array;
			if (array != null)
			{
				if (x is byte[])
				{
					return ValueHelper.ArraysEqual<byte>((byte[])x, y as byte[]);
				}
				Array array2 = y as Array;
				if (array2 == null)
				{
					return false;
				}
				if (array.Length != array2.Length)
				{
					return false;
				}
				switch (ValueTypeHelper.GetExtendedTypeCodeNoAssert(x.GetType().GetElementType()))
				{
				case ExtendedTypeCode.Boolean:
					return ValueHelper.ArraysEqual<bool>((bool[])x, y as bool[]);
				case ExtendedTypeCode.Int16:
					return ValueHelper.ArraysEqual<short>((short[])x, y as short[]);
				case ExtendedTypeCode.Int32:
					return ValueHelper.ArraysEqual<int>((int[])x, y as int[]);
				case ExtendedTypeCode.Int64:
					return ValueHelper.ArraysEqual<long>((long[])x, y as long[]);
				case ExtendedTypeCode.Single:
					return ValueHelper.ArraysEqual<float>((float[])x, y as float[]);
				case ExtendedTypeCode.Double:
					return ValueHelper.ArraysEqual<double>((double[])x, y as double[]);
				case ExtendedTypeCode.DateTime:
					return ValueHelper.ArraysEqual<DateTime>((DateTime[])x, y as DateTime[]);
				case ExtendedTypeCode.Guid:
					return ValueHelper.ArraysEqual<Guid>((Guid[])x, y as Guid[]);
				case ExtendedTypeCode.String:
					return ValueHelper.ArraysEqual((string[])x, y as string[], compareInfo, compareOptions);
				case ExtendedTypeCode.Binary:
					return ValueHelper.JaggedArraysEqual<byte>((byte[][])x, y as byte[][]);
				default:
					throw new ArgumentException(string.Format("arrays of {0} are not supported", x.GetType().GetElementType()));
				}
			}
			else
			{
				if (x is string)
				{
					int num = ValueHelper.StringsCompare((string)x, (string)y, compareInfo, compareOptions);
					return num == 0;
				}
				return x.Equals(y);
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001AD9C File Offset: 0x00018F9C
		public static bool ArraysEqual(object[] x, object[] y, CompareInfo compareInfo, CompareOptions compareOptions)
		{
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Length != y.Length)
			{
				return false;
			}
			for (int i = 0; i < x.Length; i++)
			{
				if (!ValueHelper.ValuesEqual(x[i], y[i], compareInfo, compareOptions))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001ADE8 File Offset: 0x00018FE8
		public static bool ArraysEqual<T>(T[] x, T[] y)
		{
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Length != y.Length)
			{
				return false;
			}
			if (x.Length != 0)
			{
				EqualityComparer<T> @default = EqualityComparer<T>.Default;
				for (int i = 0; i < x.Length; i++)
				{
					if (!@default.Equals(x[i], y[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001AE44 File Offset: 0x00019044
		public static bool ArraysEqual(string[] x, string[] y, CompareInfo compareInfo, CompareOptions compareOptions)
		{
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Length != y.Length)
			{
				return false;
			}
			for (int i = 0; i < x.Length; i++)
			{
				int num = ValueHelper.StringsCompare(x[i], y[i], compareInfo, compareOptions);
				if (num != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001AE90 File Offset: 0x00019090
		public static bool ListsEqual(IList<object> x, IList<object> y, CompareInfo compareInfo, CompareOptions compareOptions)
		{
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Count != y.Count)
			{
				return false;
			}
			for (int i = 0; i < x.Count; i++)
			{
				if (!ValueHelper.ValuesEqual(x[i], y[i], compareInfo, compareOptions))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001AEEC File Offset: 0x000190EC
		public static bool ListsEqual<T>(IList<T> x, IList<T> y)
		{
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Count != y.Count)
			{
				return false;
			}
			if (x.Count != 0)
			{
				EqualityComparer<T> @default = EqualityComparer<T>.Default;
				for (int i = 0; i < x.Count; i++)
				{
					if (!@default.Equals(x[i], y[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001AF54 File Offset: 0x00019154
		public static bool ListsEqual(IList<string> x, IList<string> y, CompareInfo compareInfo, CompareOptions compareOptions)
		{
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Count != y.Count)
			{
				return false;
			}
			for (int i = 0; i < x.Count; i++)
			{
				int num = ValueHelper.StringsCompare(x[i], y[i], compareInfo, compareOptions);
				if (num != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001AFB4 File Offset: 0x000191B4
		public static bool JaggedArraysEqual(string[][] x, string[][] y, CompareInfo compareInfo, CompareOptions compareOptions)
		{
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Length != y.Length)
			{
				return false;
			}
			if (x.Length != 0)
			{
				for (int i = 0; i < x.Length; i++)
				{
					if (!ValueHelper.ArraysEqual(x[i], y[i], compareInfo, compareOptions))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001B004 File Offset: 0x00019204
		public static bool JaggedArraysEqual<T>(T[][] x, T[][] y)
		{
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Length != y.Length)
			{
				return false;
			}
			if (x.Length != 0)
			{
				for (int i = 0; i < x.Length; i++)
				{
					if (!ValueHelper.ArraysEqual<T>(x[i], y[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001B054 File Offset: 0x00019254
		public static int ArraysCompare(string[] x, string[] y, CompareInfo compareInfo, CompareOptions compareOptions)
		{
			if (object.ReferenceEquals(x, y))
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			int i;
			for (i = 0; i < x.Length; i++)
			{
				if (i == y.Length)
				{
					return 1;
				}
				int num = ValueHelper.StringsCompare(x[i], y[i], compareInfo, compareOptions);
				if (num != 0)
				{
					return num;
				}
			}
			if (i < y.Length)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001B0A8 File Offset: 0x000192A8
		public static int ArraysCompare<T>(T[] x, T[] y)
		{
			if (object.ReferenceEquals(x, y))
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			Comparer<T> @default = Comparer<T>.Default;
			int i;
			for (i = 0; i < x.Length; i++)
			{
				if (i == y.Length)
				{
					return 1;
				}
				int num = @default.Compare(x[i], y[i]);
				if (num != 0)
				{
					return num;
				}
			}
			if (i < y.Length)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001B10C File Offset: 0x0001930C
		public static int JaggedArraysCompare(string[][] x, string[][] y, CompareInfo compareInfo, CompareOptions compareOptions)
		{
			if (object.ReferenceEquals(x, y))
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			int num = Math.Min(x.Length, y.Length);
			for (int i = 0; i < num; i++)
			{
				int num2 = ValueHelper.ArraysCompare(x[i], y[i], compareInfo, compareOptions);
				if (num2 != 0)
				{
					return num2;
				}
			}
			if (x.Length < y.Length)
			{
				return -1;
			}
			if (x.Length > y.Length)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001B170 File Offset: 0x00019370
		public static int JaggedArraysCompare<T>(T[][] x, T[][] y)
		{
			if (object.ReferenceEquals(x, y))
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			int num = Math.Min(x.Length, y.Length);
			for (int i = 0; i < num; i++)
			{
				int num2 = ValueHelper.ArraysCompare<T>(x[i], y[i]);
				if (num2 != 0)
				{
					return num2;
				}
			}
			if (x.Length < y.Length)
			{
				return -1;
			}
			if (x.Length > y.Length)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001B1D0 File Offset: 0x000193D0
		public static bool IsArraySorted(IList<string> array, CompareInfo compareInfo, CompareOptions compareOptions, bool dupesAllowed)
		{
			bool result = true;
			if (array != null && array.Count > 1)
			{
				for (int i = 1; i < array.Count; i++)
				{
					int num = ValueHelper.StringsCompare(array[i - 1], array[i], compareInfo, compareOptions);
					if (num > 0 || (num == 0 && !dupesAllowed))
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001B223 File Offset: 0x00019423
		public static bool IsArraySorted<T>(IList<T> array, bool dupesAllowed) where T : IComparable<T>
		{
			return ValueHelper.IsArraySorted<T>(array, Comparer<T>.Default, dupesAllowed);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001B234 File Offset: 0x00019434
		public static bool IsArraySorted<T>(IList<T> array, IComparer<T> comparer, bool dupesAllowed)
		{
			bool result = true;
			if (array != null)
			{
				for (int i = 1; i < array.Count; i++)
				{
					int num = comparer.Compare(array[i - 1], array[i]);
					if (num > 0 || (num == 0 && !dupesAllowed))
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001B280 File Offset: 0x00019480
		public static byte[] TruncateBinaryValue(byte[] input, int length)
		{
			byte[] array = input;
			if (input != null && input.Length > length)
			{
				array = new byte[length];
				Buffer.BlockCopy(input, 0, array, 0, length);
			}
			return array;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001B2AC File Offset: 0x000194AC
		public static string TruncateStringValue(string input, int length)
		{
			string result = input;
			if (input != null && input.Length > length)
			{
				result = input.Substring(0, length);
			}
			return result;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001B2D4 File Offset: 0x000194D4
		public static int GetHashCode(object value)
		{
			if (value == null)
			{
				return 0;
			}
			Array array = value as Array;
			if (array != null)
			{
				uint num = (uint)array.Length;
				byte[] array2 = array as byte[];
				if (array2 != null)
				{
					for (int i = 0; i < array2.Length; i++)
					{
						num ^= (uint)array2[i];
						num = (num << 1 | num >> 31);
					}
				}
				else
				{
					for (int j = 0; j < array.Length; j++)
					{
						num ^= (uint)ValueHelper.GetHashCode(array.GetValue(j));
						num = (num << 1 | num >> 31);
					}
				}
				return (int)num;
			}
			return value.GetHashCode();
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001A5 RID: 421
	internal static class CsvDecoder
	{
		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002A654 File Offset: 0x00028854
		public static CsvDecoderCallback GetDecoder(Type type)
		{
			CsvDecoderCallback result;
			if (CsvDecoder.decoders.TryGetValue(type, out result))
			{
				return result;
			}
			if (type.IsEnum)
			{
				return CsvDecoder.MapSafe(type, (byte[] src, int offset, int count) => CsvDecoder.DecodeEnum(type, src, offset, count));
			}
			if (type.IsArray)
			{
				return CsvDecoder.MapSafe(type, new CsvDecoderCallback(new CsvArrayDecoder(type.GetElementType()).Decode));
			}
			return null;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002A6E8 File Offset: 0x000288E8
		public static object DecodeKeyValuePair(byte[] src, int offset, int count)
		{
			string nextToken = CsvDecoder.GetNextToken(SpecialCharacters.ColonByte, src, ref offset, ref count);
			if (string.IsNullOrEmpty(nextToken))
			{
				return null;
			}
			string nextToken2 = CsvDecoder.GetNextToken(SpecialCharacters.EqualsByte, src, ref offset, ref count);
			if (string.IsNullOrEmpty(nextToken2) || !SpecialCharacters.IsValidKey(nextToken2))
			{
				return null;
			}
			object obj;
			if (nextToken.Equals("Dt", StringComparison.OrdinalIgnoreCase))
			{
				obj = CsvDecoder.DecodeDateTime(src, offset, count);
			}
			else if (nextToken.Equals("I32", StringComparison.OrdinalIgnoreCase))
			{
				obj = CsvDecoder.DecodeInt32(src, offset, count);
			}
			else if (nextToken.Equals("S", StringComparison.OrdinalIgnoreCase))
			{
				obj = CsvDecoder.DecodeString(src, offset, count);
			}
			else if (nextToken.Equals("F", StringComparison.OrdinalIgnoreCase))
			{
				obj = CsvDecoder.DecodeFloat(src, offset, count);
			}
			else
			{
				if (!nextToken.Equals("Dbl", StringComparison.OrdinalIgnoreCase))
				{
					return null;
				}
				obj = CsvDecoder.DecodeDouble(src, offset, count);
			}
			if (obj == null)
			{
				return null;
			}
			return new KeyValuePair<string, object>(nextToken2, obj);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002A7C4 File Offset: 0x000289C4
		private static CsvDecoderCallback MapSafe(Type type, CsvDecoderCallback callback)
		{
			CsvDecoderCallback result;
			lock (CsvDecoder.decodersMutex)
			{
				CsvDecoderCallback csvDecoderCallback;
				if (CsvDecoder.decoders.TryGetValue(type, out csvDecoderCallback))
				{
					result = csvDecoderCallback;
				}
				else
				{
					CsvDecoder.decoders = new Dictionary<Type, CsvDecoderCallback>(CsvDecoder.decoders)
					{
						{
							type,
							callback
						}
					};
					result = callback;
				}
			}
			return result;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002A834 File Offset: 0x00028A34
		private static object DecodeEnum(Type type, byte[] src, int offset, int count)
		{
			object result;
			try
			{
				string @string = Encoding.UTF8.GetString(src, offset, count);
				result = Enum.Parse(type, @string);
			}
			catch (ArgumentException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002A870 File Offset: 0x00028A70
		private static object DecodeGuid(byte[] src, int offset, int count)
		{
			string @string = Encoding.UTF8.GetString(src, offset, count);
			Guid guid;
			if (Guid.TryParse(@string, out guid))
			{
				return guid;
			}
			return null;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002A8A0 File Offset: 0x00028AA0
		private static object DecodeInt32(byte[] src, int offset, int count)
		{
			string @string = Encoding.UTF8.GetString(src, offset, count);
			int num;
			if (int.TryParse(@string, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
			{
				return num;
			}
			return null;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002A8D4 File Offset: 0x00028AD4
		private static object DecodeFloat(byte[] src, int offset, int count)
		{
			string @string = Encoding.UTF8.GetString(src, offset, count);
			float num;
			if (float.TryParse(@string, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num))
			{
				return num;
			}
			return null;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002A90C File Offset: 0x00028B0C
		private static object DecodeDouble(byte[] src, int offset, int count)
		{
			string @string = Encoding.UTF8.GetString(src, offset, count);
			double num;
			if (double.TryParse(@string, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num))
			{
				return num;
			}
			return null;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002A943 File Offset: 0x00028B43
		private static object DecodeString(byte[] src, int offset, int count)
		{
			return Encoding.UTF8.GetString(src, offset, count);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002A954 File Offset: 0x00028B54
		private static object DecodeDateTime(byte[] src, int offset, int count)
		{
			if (count != "yyyy-MM-ddThh:mm:ss.sssZ".Length && count != "yyyy-MM-ddThh:mm:ss.ssssssZ".Length)
			{
				return null;
			}
			int year;
			if (!CsvDecoder.TryDecodeDigits(src, offset, 4, out year))
			{
				return null;
			}
			offset += 4;
			if (src[offset++] != 45)
			{
				return null;
			}
			int month;
			if (!CsvDecoder.TryDecodeDigits(src, offset, 2, out month))
			{
				return null;
			}
			offset += 2;
			if (src[offset++] != 45)
			{
				return null;
			}
			int day;
			if (!CsvDecoder.TryDecodeDigits(src, offset, 2, out day))
			{
				return null;
			}
			offset += 2;
			if (src[offset++] != 84)
			{
				return null;
			}
			int hour;
			if (!CsvDecoder.TryDecodeDigits(src, offset, 2, out hour))
			{
				return null;
			}
			offset += 2;
			if (src[offset++] != 58)
			{
				return null;
			}
			int minute;
			if (!CsvDecoder.TryDecodeDigits(src, offset, 2, out minute))
			{
				return null;
			}
			offset += 2;
			if (src[offset++] != 58)
			{
				return null;
			}
			int second;
			if (!CsvDecoder.TryDecodeDigits(src, offset, 2, out second))
			{
				return null;
			}
			offset += 2;
			if (src[offset++] != 46)
			{
				return null;
			}
			int millisecond;
			if (!CsvDecoder.TryDecodeDigits(src, offset, 3, out millisecond))
			{
				return null;
			}
			offset += 3;
			int num = 0;
			if (count == "yyyy-MM-ddThh:mm:ss.ssssssZ".Length)
			{
				if (!CsvDecoder.TryDecodeDigits(src, offset, 3, out num))
				{
					return null;
				}
				offset += 3;
			}
			if (src[offset++] != 90)
			{
				return null;
			}
			object result;
			try
			{
				DateTime dateTime = new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Utc);
				dateTime += TimeSpan.FromTicks((long)(num * 10));
				result = dateTime;
			}
			catch (ArgumentException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002AAD0 File Offset: 0x00028CD0
		private static int IndexOf(byte ch, byte[] src, int offset, int count)
		{
			for (int i = offset; i < offset + count; i++)
			{
				if (src[i] == ch)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002AAF4 File Offset: 0x00028CF4
		private static string GetNextToken(byte terminalCh, byte[] src, ref int offset, ref int count)
		{
			int num = CsvDecoder.IndexOf(terminalCh, src, offset, count);
			if (num == -1)
			{
				return null;
			}
			int num2 = num - offset;
			string result = (string)CsvDecoder.DecodeString(src, offset, num2);
			int num3 = num2 + 1;
			offset += num3;
			count -= num3;
			return result;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002AB38 File Offset: 0x00028D38
		private static bool TryDecodeDigits(byte[] digits, int offset, int count, out int result)
		{
			result = 0;
			int i = offset;
			int num = offset + count;
			while (i < num)
			{
				byte b = digits[i++];
				if (b < 48 || b > 57)
				{
					return false;
				}
				result *= 10;
				result += (int)(b - 48);
			}
			return true;
		}

		// Token: 0x04000878 RID: 2168
		private static readonly object decodersMutex = new object();

		// Token: 0x04000879 RID: 2169
		private static volatile Dictionary<Type, CsvDecoderCallback> decoders = new Dictionary<Type, CsvDecoderCallback>
		{
			{
				typeof(Guid),
				new CsvDecoderCallback(CsvDecoder.DecodeGuid)
			},
			{
				typeof(DateTime),
				new CsvDecoderCallback(CsvDecoder.DecodeDateTime)
			},
			{
				typeof(string),
				new CsvDecoderCallback(CsvDecoder.DecodeString)
			},
			{
				typeof(int),
				new CsvDecoderCallback(CsvDecoder.DecodeInt32)
			},
			{
				typeof(KeyValuePair<string, object>),
				new CsvDecoderCallback(CsvDecoder.DecodeKeyValuePair)
			}
		};
	}
}

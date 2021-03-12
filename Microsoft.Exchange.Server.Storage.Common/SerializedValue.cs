using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000C6 RID: 198
	public static class SerializedValue
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x0001B356 File Offset: 0x00019556
		public static object BoxedTrue
		{
			get
			{
				return SerializedValue.boxedTrue;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0001B35D File Offset: 0x0001955D
		public static object BoxedFalse
		{
			get
			{
				return SerializedValue.boxedFalse;
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001B364 File Offset: 0x00019564
		public static bool TryParse(SerializedValue.ValueFormat format, byte[] buffer, int offset, out object value)
		{
			value = null;
			if (!ParseSerialize.CheckOffsetLength(buffer, offset, 1))
			{
				return false;
			}
			SerializedValue.ValueFormat valueFormat = (SerializedValue.ValueFormat)buffer[offset++];
			return (byte)(format & SerializedValue.ValueFormat.TypeMask) == (byte)(valueFormat & SerializedValue.ValueFormat.TypeMask) && SerializedValue.TryParseValue(valueFormat, buffer, ref offset, ref value);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0001B3A7 File Offset: 0x000195A7
		public static bool TryParse(byte[] buffer, int offset, out object value)
		{
			return SerializedValue.TryParse(buffer, ref offset, out value);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001B3B4 File Offset: 0x000195B4
		public static bool TryParse(byte[] buffer, ref int offset, out object value)
		{
			value = null;
			if (!ParseSerialize.CheckOffsetLength(buffer, offset, 1))
			{
				return false;
			}
			SerializedValue.ValueFormat format = (SerializedValue.ValueFormat)buffer[offset++];
			return SerializedValue.TryParseValue(format, buffer, ref offset, ref value);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001B3E8 File Offset: 0x000195E8
		public static bool TryGetSize(SerializedValue.ValueFormat format, byte[] buffer, int offset, out int size)
		{
			if (!ParseSerialize.CheckOffsetLength(buffer, offset, 1))
			{
				size = 0;
				return false;
			}
			SerializedValue.ValueFormat valueFormat = (SerializedValue.ValueFormat)buffer[offset++];
			if ((byte)(format & SerializedValue.ValueFormat.TypeMask) != (byte)(valueFormat & SerializedValue.ValueFormat.TypeMask))
			{
				size = 0;
				return false;
			}
			return SerializedValue.TryGetValueSize(valueFormat, buffer, offset, out size);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0001B42D File Offset: 0x0001962D
		public static bool TryGetSize(byte[] buffer, int offset, out int size)
		{
			return SerializedValue.TryGetSize(buffer, ref offset, out size);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0001B438 File Offset: 0x00019638
		public static bool TryGetSize(byte[] buffer, ref int offset, out int size)
		{
			if (!ParseSerialize.CheckOffsetLength(buffer, offset, 1))
			{
				size = 0;
				return false;
			}
			SerializedValue.ValueFormat format = (SerializedValue.ValueFormat)buffer[offset++];
			return SerializedValue.TryGetValueSize(format, buffer, offset, out size);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001B46B File Offset: 0x0001966B
		public static object Parse(byte[] buffer, int offset)
		{
			return SerializedValue.Parse(buffer, ref offset);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001B478 File Offset: 0x00019678
		public static object Parse(byte[] buffer, ref int offset)
		{
			object result;
			if (!SerializedValue.TryParse(buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0001B49C File Offset: 0x0001969C
		public static object Parse(byte[] buffer)
		{
			object result;
			if (!SerializedValue.TryParse(buffer, 0, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0001B4C0 File Offset: 0x000196C0
		public static bool TryParseList(byte[] buffer, ref int offset, out IList<object> valueList)
		{
			List<object> list = null;
			while (offset != buffer.Length)
			{
				object item;
				if (!SerializedValue.TryParse(buffer, ref offset, out item))
				{
					valueList = null;
					return false;
				}
				if (list == null)
				{
					list = new List<object>(4);
				}
				list.Add(item);
			}
			valueList = list;
			return true;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001B500 File Offset: 0x00019700
		public static IList<object> ParseList(byte[] buffer, ref int offset)
		{
			IList<object> result;
			if (!SerializedValue.TryParseList(buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001B524 File Offset: 0x00019724
		public static bool ParseBoolean(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat format = SerializedValue.ParseAndAssertFormat(buffer, ref offset, SerializedValue.ValueFormat.Boolean);
			bool result;
			if (!SerializedValue.TryParseBooleanValue(format, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0001B554 File Offset: 0x00019754
		public static short ParseInt16(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat format = SerializedValue.ParseAndAssertFormat(buffer, ref offset, SerializedValue.ValueFormat.Int16);
			short result;
			if (!SerializedValue.TryParseInt16Value(format, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001B584 File Offset: 0x00019784
		public static int ParseInt32(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat format = SerializedValue.ParseAndAssertFormat(buffer, ref offset, SerializedValue.ValueFormat.Int32);
			int result;
			if (!SerializedValue.TryParseInt32Value(format, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001B5B4 File Offset: 0x000197B4
		public static long ParseInt64(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat format = SerializedValue.ParseAndAssertFormat(buffer, ref offset, SerializedValue.ValueFormat.Int64);
			long result;
			if (!SerializedValue.TryParseInt64Value(format, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001B5E4 File Offset: 0x000197E4
		public static float ParseSingle(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat format = SerializedValue.ParseAndAssertFormat(buffer, ref offset, SerializedValue.ValueFormat.Single);
			float result;
			if (!SerializedValue.TryParseSingleValue(format, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0001B614 File Offset: 0x00019814
		public static double ParseDouble(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat format = SerializedValue.ParseAndAssertFormat(buffer, ref offset, SerializedValue.ValueFormat.Double);
			double result;
			if (!SerializedValue.TryParseDoubleValue(format, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001B644 File Offset: 0x00019844
		public static DateTime ParseDateTime(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat format = SerializedValue.ParseAndAssertFormat(buffer, ref offset, SerializedValue.ValueFormat.DateTime);
			DateTime result;
			if (!SerializedValue.TryParseDateTimeValue(format, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001B674 File Offset: 0x00019874
		public static Guid ParseGuid(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat format = SerializedValue.ParseAndAssertFormat(buffer, ref offset, SerializedValue.ValueFormat.Guid);
			Guid result;
			if (!SerializedValue.TryParseGuidValue(format, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001B6A4 File Offset: 0x000198A4
		public static byte[] ParseBinary(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat valueFormat = SerializedValue.ParseAndAssertFormatOrNull(buffer, ref offset, SerializedValue.ValueFormat.Binary);
			if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
			{
				return null;
			}
			byte[] result;
			if (!SerializedValue.TryParseBinaryValue(valueFormat, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001B6D8 File Offset: 0x000198D8
		public static string ParseString(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat valueFormat = SerializedValue.ParseAndAssertFormatOrNull(buffer, ref offset, SerializedValue.ValueFormat.String);
			if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
			{
				return null;
			}
			string result;
			if (!SerializedValue.TryParseStringValue(valueFormat, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001B70C File Offset: 0x0001990C
		public static short[] ParseMVInt16(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat valueFormat = SerializedValue.ParseAndAssertFormatOrNull(buffer, ref offset, SerializedValue.ValueFormat.MVInt16);
			if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
			{
				return null;
			}
			short[] result;
			if (!SerializedValue.TryParseMVInt16Value(valueFormat, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001B744 File Offset: 0x00019944
		public static int[] ParseMVInt32(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat valueFormat = SerializedValue.ParseAndAssertFormatOrNull(buffer, ref offset, SerializedValue.ValueFormat.MVInt32);
			if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
			{
				return null;
			}
			int[] result;
			if (!SerializedValue.TryParseMVInt32Value(valueFormat, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001B77C File Offset: 0x0001997C
		public static long[] ParseMVInt64(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat valueFormat = SerializedValue.ParseAndAssertFormatOrNull(buffer, ref offset, SerializedValue.ValueFormat.MVInt64);
			if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
			{
				return null;
			}
			long[] result;
			if (!SerializedValue.TryParseMVInt64Value(valueFormat, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001B7B4 File Offset: 0x000199B4
		public static float[] ParseMVSingle(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat valueFormat = SerializedValue.ParseAndAssertFormatOrNull(buffer, ref offset, SerializedValue.ValueFormat.MVSingle);
			if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
			{
				return null;
			}
			float[] result;
			if (!SerializedValue.TryParseMVSingleValue(valueFormat, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001B7EC File Offset: 0x000199EC
		public static double[] ParseMVDouble(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat valueFormat = SerializedValue.ParseAndAssertFormatOrNull(buffer, ref offset, SerializedValue.ValueFormat.MVDouble);
			if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
			{
				return null;
			}
			double[] result;
			if (!SerializedValue.TryParseMVDoubleValue(valueFormat, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001B824 File Offset: 0x00019A24
		public static DateTime[] ParseMVDateTime(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat valueFormat = SerializedValue.ParseAndAssertFormatOrNull(buffer, ref offset, SerializedValue.ValueFormat.MVDateTime);
			if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
			{
				return null;
			}
			DateTime[] result;
			if (!SerializedValue.TryParseMVDateTimeValue(valueFormat, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001B85C File Offset: 0x00019A5C
		public static Guid[] ParseMVGuid(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat valueFormat = SerializedValue.ParseAndAssertFormatOrNull(buffer, ref offset, SerializedValue.ValueFormat.MVGuid);
			if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
			{
				return null;
			}
			Guid[] result;
			if (!SerializedValue.TryParseMVGuidValue(valueFormat, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001B894 File Offset: 0x00019A94
		public static byte[][] ParseMVBinary(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat valueFormat = SerializedValue.ParseAndAssertFormatOrNull(buffer, ref offset, SerializedValue.ValueFormat.MVBinary);
			if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
			{
				return null;
			}
			byte[][] result;
			if (!SerializedValue.TryParseMVBinaryValue(valueFormat, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001B8CC File Offset: 0x00019ACC
		public static string[] ParseMVString(byte[] buffer, ref int offset)
		{
			SerializedValue.ValueFormat valueFormat = SerializedValue.ParseAndAssertFormatOrNull(buffer, ref offset, SerializedValue.ValueFormat.MVString);
			if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
			{
				return null;
			}
			string[] result;
			if (!SerializedValue.TryParseMVStringValue(valueFormat, buffer, ref offset, out result))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			return result;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001B903 File Offset: 0x00019B03
		public static object GetBoxedInt32(int value)
		{
			if (0 <= value && value < SerializedValue.boxedInts.Length)
			{
				if (SerializedValue.boxedInts[value] == null)
				{
					SerializedValue.boxedInts[value] = value;
				}
				return SerializedValue.boxedInts[value];
			}
			return value;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001B938 File Offset: 0x00019B38
		private static SerializedValue.ValueFormat ParseAndAssertFormatOrNull(byte[] buffer, ref int offset, SerializedValue.ValueFormat expectedFormat)
		{
			if (!ParseSerialize.CheckOffsetLength(buffer, offset, 1))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			SerializedValue.ValueFormat valueFormat = (SerializedValue.ValueFormat)buffer[offset++];
			if ((valueFormat & SerializedValue.ValueFormat.TypeMask) != expectedFormat && valueFormat != SerializedValue.ValueFormat.FormatModifierShift)
			{
				throw new InvalidSerializedFormatException("value parsing error - unexpeced value format");
			}
			return valueFormat;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001B984 File Offset: 0x00019B84
		private static SerializedValue.ValueFormat ParseAndAssertFormat(byte[] buffer, ref int offset, SerializedValue.ValueFormat expectedFormat)
		{
			if (!ParseSerialize.CheckOffsetLength(buffer, offset, 1))
			{
				throw new InvalidSerializedFormatException("value parsing error");
			}
			SerializedValue.ValueFormat valueFormat = (SerializedValue.ValueFormat)buffer[offset++];
			if ((valueFormat & SerializedValue.ValueFormat.TypeMask) != expectedFormat)
			{
				throw new InvalidSerializedFormatException("value parsing error - unexpeced value format");
			}
			return valueFormat;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001B9CC File Offset: 0x00019BCC
		private static bool TryParseValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, ref object value)
		{
			ValueReference valueReference;
			if (format <= SerializedValue.ValueFormat.Double)
			{
				if (format <= (SerializedValue.ValueFormat)9)
				{
					if (format == SerializedValue.ValueFormat.FormatModifierShift)
					{
						return true;
					}
					switch (format)
					{
					case SerializedValue.ValueFormat.Boolean:
					case (SerializedValue.ValueFormat)9:
					{
						bool flag;
						if (SerializedValue.TryParseBooleanValue(format, buffer, ref offset, out flag))
						{
							value = (flag ? SerializedValue.BoxedTrue : SerializedValue.BoxedFalse);
							return true;
						}
						break;
					}
					}
				}
				else
				{
					switch (format)
					{
					case SerializedValue.ValueFormat.Int16:
					case (SerializedValue.ValueFormat)17:
					case (SerializedValue.ValueFormat)18:
					{
						short num;
						if (SerializedValue.TryParseInt16Value(format, buffer, ref offset, out num))
						{
							value = num;
							return true;
						}
						break;
					}
					case (SerializedValue.ValueFormat)19:
					case (SerializedValue.ValueFormat)20:
					case (SerializedValue.ValueFormat)21:
					case (SerializedValue.ValueFormat)22:
					case (SerializedValue.ValueFormat)23:
					case (SerializedValue.ValueFormat)28:
					case (SerializedValue.ValueFormat)29:
					case (SerializedValue.ValueFormat)30:
					case (SerializedValue.ValueFormat)31:
					case (SerializedValue.ValueFormat)37:
					case (SerializedValue.ValueFormat)38:
					case (SerializedValue.ValueFormat)39:
						break;
					case SerializedValue.ValueFormat.Int32:
					case (SerializedValue.ValueFormat)25:
					case (SerializedValue.ValueFormat)26:
					case (SerializedValue.ValueFormat)27:
					{
						int num2;
						if (SerializedValue.TryParseInt32Value(format, buffer, ref offset, out num2))
						{
							value = num2;
							return true;
						}
						break;
					}
					case SerializedValue.ValueFormat.Int64:
					case (SerializedValue.ValueFormat)33:
					case (SerializedValue.ValueFormat)34:
					case (SerializedValue.ValueFormat)35:
					case (SerializedValue.ValueFormat)36:
					{
						long num3;
						if (SerializedValue.TryParseInt64Value(format, buffer, ref offset, out num3))
						{
							value = num3;
							return true;
						}
						break;
					}
					case SerializedValue.ValueFormat.Single:
					{
						float num4;
						if (SerializedValue.TryParseSingleValue(format, buffer, ref offset, out num4))
						{
							value = num4;
							return true;
						}
						break;
					}
					default:
						if (format == SerializedValue.ValueFormat.Double)
						{
							double num5;
							if (SerializedValue.TryParseDoubleValue(format, buffer, ref offset, out num5))
							{
								value = num5;
								return true;
							}
						}
						break;
					}
				}
			}
			else if (format <= (SerializedValue.ValueFormat)83)
			{
				DateTime dateTime;
				if (format != SerializedValue.ValueFormat.DateTime)
				{
					switch (format)
					{
					case SerializedValue.ValueFormat.Guid:
					{
						Guid guid;
						if (SerializedValue.TryParseGuidValue(format, buffer, ref offset, out guid))
						{
							value = guid;
							return true;
						}
						break;
					}
					case SerializedValue.ValueFormat.String:
					case (SerializedValue.ValueFormat)73:
					case (SerializedValue.ValueFormat)74:
					case (SerializedValue.ValueFormat)75:
					case (SerializedValue.ValueFormat)77:
					case (SerializedValue.ValueFormat)78:
					case (SerializedValue.ValueFormat)79:
					{
						string text;
						if (SerializedValue.TryParseStringValue(format, buffer, ref offset, out text))
						{
							value = text;
							return true;
						}
						break;
					}
					case SerializedValue.ValueFormat.Binary:
					case (SerializedValue.ValueFormat)81:
					case (SerializedValue.ValueFormat)82:
					case (SerializedValue.ValueFormat)83:
					{
						byte[] array;
						if (SerializedValue.TryParseBinaryValue(format, buffer, ref offset, out array))
						{
							value = array;
							return true;
						}
						break;
					}
					}
				}
				else if (SerializedValue.TryParseDateTimeValue(format, buffer, ref offset, out dateTime))
				{
					value = dateTime;
					return true;
				}
			}
			else if (format != SerializedValue.ValueFormat.Reference && format != (SerializedValue.ValueFormat)124)
			{
				switch (format)
				{
				case SerializedValue.ValueFormat.MVInt16:
				case (SerializedValue.ValueFormat)145:
				case (SerializedValue.ValueFormat)146:
				case (SerializedValue.ValueFormat)147:
				{
					short[] array2;
					if (SerializedValue.TryParseMVInt16Value(format, buffer, ref offset, out array2))
					{
						value = array2;
						return true;
					}
					break;
				}
				case SerializedValue.ValueFormat.MVInt32:
				case (SerializedValue.ValueFormat)153:
				case (SerializedValue.ValueFormat)154:
				case (SerializedValue.ValueFormat)155:
				{
					int[] array3;
					if (SerializedValue.TryParseMVInt32Value(format, buffer, ref offset, out array3))
					{
						value = array3;
						return true;
					}
					break;
				}
				case SerializedValue.ValueFormat.MVInt64:
				case (SerializedValue.ValueFormat)161:
				case (SerializedValue.ValueFormat)162:
				case (SerializedValue.ValueFormat)163:
				{
					long[] array4;
					if (SerializedValue.TryParseMVInt64Value(format, buffer, ref offset, out array4))
					{
						value = array4;
						return true;
					}
					break;
				}
				case SerializedValue.ValueFormat.MVSingle:
				case (SerializedValue.ValueFormat)169:
				case (SerializedValue.ValueFormat)170:
				case (SerializedValue.ValueFormat)171:
				{
					float[] array5;
					if (SerializedValue.TryParseMVSingleValue(format, buffer, ref offset, out array5))
					{
						value = array5;
						return true;
					}
					break;
				}
				case SerializedValue.ValueFormat.MVDouble:
				case (SerializedValue.ValueFormat)177:
				case (SerializedValue.ValueFormat)178:
				case (SerializedValue.ValueFormat)179:
				{
					double[] array6;
					if (SerializedValue.TryParseMVDoubleValue(format, buffer, ref offset, out array6))
					{
						value = array6;
						return true;
					}
					break;
				}
				case SerializedValue.ValueFormat.MVDateTime:
				case (SerializedValue.ValueFormat)185:
				case (SerializedValue.ValueFormat)186:
				case (SerializedValue.ValueFormat)187:
				{
					DateTime[] array7;
					if (SerializedValue.TryParseMVDateTimeValue(format, buffer, ref offset, out array7))
					{
						value = array7;
						return true;
					}
					break;
				}
				case SerializedValue.ValueFormat.MVGuid:
				case (SerializedValue.ValueFormat)193:
				case (SerializedValue.ValueFormat)194:
				case (SerializedValue.ValueFormat)195:
				{
					Guid[] array8;
					if (SerializedValue.TryParseMVGuidValue(format, buffer, ref offset, out array8))
					{
						value = array8;
						return true;
					}
					break;
				}
				case SerializedValue.ValueFormat.MVString:
				case (SerializedValue.ValueFormat)201:
				case (SerializedValue.ValueFormat)202:
				case (SerializedValue.ValueFormat)203:
				{
					string[] array9;
					if (SerializedValue.TryParseMVStringValue(format, buffer, ref offset, out array9))
					{
						value = array9;
						return true;
					}
					break;
				}
				case SerializedValue.ValueFormat.MVBinary:
				case (SerializedValue.ValueFormat)209:
				case (SerializedValue.ValueFormat)210:
				case (SerializedValue.ValueFormat)211:
				{
					byte[][] array10;
					if (SerializedValue.TryParseMVBinaryValue(format, buffer, ref offset, out array10))
					{
						value = array10;
						return true;
					}
					break;
				}
				}
			}
			else if (SerializedValue.TryParseReferenceValue(format, buffer, ref offset, out valueReference))
			{
				value = valueReference;
				return true;
			}
			return false;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001BDE8 File Offset: 0x00019FE8
		private static bool TryGetValueSize(SerializedValue.ValueFormat format, byte[] buffer, int offset, out int size)
		{
			if (format <= SerializedValue.ValueFormat.Double)
			{
				if (format <= (SerializedValue.ValueFormat)9)
				{
					if (format == SerializedValue.ValueFormat.FormatModifierShift)
					{
						size = 0;
						return true;
					}
					switch (format)
					{
					case SerializedValue.ValueFormat.Boolean:
					case (SerializedValue.ValueFormat)9:
						size = 1;
						return true;
					}
				}
				else
				{
					switch (format)
					{
					case SerializedValue.ValueFormat.Int16:
					case (SerializedValue.ValueFormat)17:
					case (SerializedValue.ValueFormat)18:
						size = 2;
						return true;
					case (SerializedValue.ValueFormat)19:
					case (SerializedValue.ValueFormat)20:
					case (SerializedValue.ValueFormat)21:
					case (SerializedValue.ValueFormat)22:
					case (SerializedValue.ValueFormat)23:
					case (SerializedValue.ValueFormat)28:
					case (SerializedValue.ValueFormat)29:
					case (SerializedValue.ValueFormat)30:
					case (SerializedValue.ValueFormat)31:
					case (SerializedValue.ValueFormat)37:
					case (SerializedValue.ValueFormat)38:
					case (SerializedValue.ValueFormat)39:
						break;
					case SerializedValue.ValueFormat.Int32:
					case (SerializedValue.ValueFormat)25:
					case (SerializedValue.ValueFormat)26:
					case (SerializedValue.ValueFormat)27:
						size = 4;
						return true;
					case SerializedValue.ValueFormat.Int64:
					case (SerializedValue.ValueFormat)33:
					case (SerializedValue.ValueFormat)34:
					case (SerializedValue.ValueFormat)35:
					case (SerializedValue.ValueFormat)36:
						size = 8;
						return true;
					case SerializedValue.ValueFormat.Single:
						size = 4;
						return true;
					default:
						if (format == SerializedValue.ValueFormat.Double)
						{
							size = 8;
							return true;
						}
						break;
					}
				}
			}
			else
			{
				int num;
				if (format <= (SerializedValue.ValueFormat)83)
				{
					if (format == SerializedValue.ValueFormat.DateTime)
					{
						size = 8;
						return true;
					}
					switch (format)
					{
					case SerializedValue.ValueFormat.Guid:
						size = 16;
						return true;
					case (SerializedValue.ValueFormat)65:
					case (SerializedValue.ValueFormat)66:
					case (SerializedValue.ValueFormat)67:
					case (SerializedValue.ValueFormat)68:
					case (SerializedValue.ValueFormat)69:
					case (SerializedValue.ValueFormat)70:
					case (SerializedValue.ValueFormat)71:
					case (SerializedValue.ValueFormat)76:
						goto IL_2DB;
					case SerializedValue.ValueFormat.String:
					case (SerializedValue.ValueFormat)73:
					case (SerializedValue.ValueFormat)74:
					case (SerializedValue.ValueFormat)75:
					case (SerializedValue.ValueFormat)77:
					case (SerializedValue.ValueFormat)78:
					case (SerializedValue.ValueFormat)79:
						if (SerializedValue.TryGetStringValueSize(format, buffer, ref offset, out size))
						{
							return true;
						}
						goto IL_2DB;
					case SerializedValue.ValueFormat.Binary:
					case (SerializedValue.ValueFormat)81:
					case (SerializedValue.ValueFormat)82:
					case (SerializedValue.ValueFormat)83:
						num = 1;
						break;
					default:
						goto IL_2DB;
					}
				}
				else
				{
					if (format == SerializedValue.ValueFormat.Reference || format == (SerializedValue.ValueFormat)124)
					{
						size = 0;
						return true;
					}
					switch (format)
					{
					case SerializedValue.ValueFormat.MVInt16:
					case (SerializedValue.ValueFormat)145:
					case (SerializedValue.ValueFormat)146:
					case (SerializedValue.ValueFormat)147:
						num = 2;
						break;
					case (SerializedValue.ValueFormat)148:
					case (SerializedValue.ValueFormat)149:
					case (SerializedValue.ValueFormat)150:
					case (SerializedValue.ValueFormat)151:
					case (SerializedValue.ValueFormat)156:
					case (SerializedValue.ValueFormat)157:
					case (SerializedValue.ValueFormat)158:
					case (SerializedValue.ValueFormat)159:
					case (SerializedValue.ValueFormat)164:
					case (SerializedValue.ValueFormat)165:
					case (SerializedValue.ValueFormat)166:
					case (SerializedValue.ValueFormat)167:
					case (SerializedValue.ValueFormat)172:
					case (SerializedValue.ValueFormat)173:
					case (SerializedValue.ValueFormat)174:
					case (SerializedValue.ValueFormat)175:
					case (SerializedValue.ValueFormat)180:
					case (SerializedValue.ValueFormat)181:
					case (SerializedValue.ValueFormat)182:
					case (SerializedValue.ValueFormat)183:
					case (SerializedValue.ValueFormat)188:
					case (SerializedValue.ValueFormat)189:
					case (SerializedValue.ValueFormat)190:
					case (SerializedValue.ValueFormat)191:
					case (SerializedValue.ValueFormat)196:
					case (SerializedValue.ValueFormat)197:
					case (SerializedValue.ValueFormat)198:
					case (SerializedValue.ValueFormat)199:
					case (SerializedValue.ValueFormat)204:
					case (SerializedValue.ValueFormat)205:
					case (SerializedValue.ValueFormat)206:
					case (SerializedValue.ValueFormat)207:
						goto IL_2DB;
					case SerializedValue.ValueFormat.MVInt32:
					case (SerializedValue.ValueFormat)153:
					case (SerializedValue.ValueFormat)154:
					case (SerializedValue.ValueFormat)155:
						num = 4;
						break;
					case SerializedValue.ValueFormat.MVInt64:
					case (SerializedValue.ValueFormat)161:
					case (SerializedValue.ValueFormat)162:
					case (SerializedValue.ValueFormat)163:
						num = 8;
						break;
					case SerializedValue.ValueFormat.MVSingle:
					case (SerializedValue.ValueFormat)169:
					case (SerializedValue.ValueFormat)170:
					case (SerializedValue.ValueFormat)171:
						num = 4;
						break;
					case SerializedValue.ValueFormat.MVDouble:
					case (SerializedValue.ValueFormat)177:
					case (SerializedValue.ValueFormat)178:
					case (SerializedValue.ValueFormat)179:
						num = 8;
						break;
					case SerializedValue.ValueFormat.MVDateTime:
					case (SerializedValue.ValueFormat)185:
					case (SerializedValue.ValueFormat)186:
					case (SerializedValue.ValueFormat)187:
						num = 8;
						break;
					case SerializedValue.ValueFormat.MVGuid:
					case (SerializedValue.ValueFormat)193:
					case (SerializedValue.ValueFormat)194:
					case (SerializedValue.ValueFormat)195:
						num = 16;
						break;
					case SerializedValue.ValueFormat.MVString:
					case (SerializedValue.ValueFormat)201:
					case (SerializedValue.ValueFormat)202:
					case (SerializedValue.ValueFormat)203:
						if (SerializedValue.TryGetMVStringValueSize(format, buffer, ref offset, out size))
						{
							return true;
						}
						goto IL_2DB;
					case SerializedValue.ValueFormat.MVBinary:
					case (SerializedValue.ValueFormat)209:
					case (SerializedValue.ValueFormat)210:
					case (SerializedValue.ValueFormat)211:
						if (SerializedValue.TryGetMVBinaryValueSize(format, buffer, ref offset, out size))
						{
							return true;
						}
						goto IL_2DB;
					default:
						goto IL_2DB;
					}
				}
				int num2;
				if (SerializedValue.TryParseLength(format, buffer, ref offset, out num2) && ParseSerialize.CheckOffsetLength(buffer, offset, num2))
				{
					size = num2 * num;
					return true;
				}
			}
			IL_2DB:
			size = 0;
			return false;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
		private static bool TryParseBooleanValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out bool value)
		{
			value = (format == (SerializedValue.ValueFormat)9);
			return true;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
		private static bool TryParseInt16Value(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out short value)
		{
			switch ((byte)(format & SerializedValue.ValueFormat.FormatModifierMask))
			{
			case 0:
				value = 0;
				return true;
			case 1:
				if (ParseSerialize.CheckOffsetLength(buffer, offset, 1))
				{
					value = (short)((sbyte)buffer[offset++]);
					return true;
				}
				break;
			case 2:
				if (ParseSerialize.CheckOffsetLength(buffer, offset, 2))
				{
					value = ParseSerialize.ParseInt16(buffer, offset);
					offset += 2;
					return true;
				}
				break;
			}
			value = 0;
			return false;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001C148 File Offset: 0x0001A348
		private static bool TryParseInt32Value(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out int value)
		{
			switch ((byte)(format & SerializedValue.ValueFormat.FormatModifierMask))
			{
			case 0:
				value = 0;
				return true;
			case 1:
				if (ParseSerialize.CheckOffsetLength(buffer, offset, 1))
				{
					value = (int)((sbyte)buffer[offset++]);
					return true;
				}
				break;
			case 2:
				if (ParseSerialize.CheckOffsetLength(buffer, offset, 2))
				{
					value = (int)ParseSerialize.ParseInt16(buffer, offset);
					offset += 2;
					return true;
				}
				break;
			case 3:
				if (ParseSerialize.CheckOffsetLength(buffer, offset, 4))
				{
					value = ParseSerialize.ParseInt32(buffer, offset);
					offset += 4;
					return true;
				}
				break;
			}
			value = 0;
			return false;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001C1D0 File Offset: 0x0001A3D0
		private static bool TryParseInt64Value(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out long value)
		{
			switch ((byte)(format & SerializedValue.ValueFormat.FormatModifierMask))
			{
			case 0:
				value = 0L;
				return true;
			case 1:
				if (ParseSerialize.CheckOffsetLength(buffer, offset, 1))
				{
					value = (long)((sbyte)buffer[offset++]);
					return true;
				}
				break;
			case 2:
				if (ParseSerialize.CheckOffsetLength(buffer, offset, 2))
				{
					value = (long)ParseSerialize.ParseInt16(buffer, offset);
					offset += 2;
					return true;
				}
				break;
			case 3:
				if (ParseSerialize.CheckOffsetLength(buffer, offset, 4))
				{
					value = (long)ParseSerialize.ParseInt32(buffer, offset);
					offset += 4;
					return true;
				}
				break;
			case 4:
				if (ParseSerialize.CheckOffsetLength(buffer, offset, 8))
				{
					value = ParseSerialize.ParseInt64(buffer, offset);
					offset += 8;
					return true;
				}
				break;
			}
			value = 0L;
			return false;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001C27E File Offset: 0x0001A47E
		private static bool TryParseSingleValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out float value)
		{
			if (ParseSerialize.CheckOffsetLength(buffer, offset, 4))
			{
				value = ParseSerialize.ParseSingle(buffer, offset);
				offset += 4;
				return true;
			}
			value = 0f;
			return false;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001C2A5 File Offset: 0x0001A4A5
		private static bool TryParseDoubleValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out double value)
		{
			if (ParseSerialize.CheckOffsetLength(buffer, offset, 8))
			{
				value = ParseSerialize.ParseDouble(buffer, offset);
				offset += 8;
				return true;
			}
			value = 0.0;
			return false;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001C2D0 File Offset: 0x0001A4D0
		private static bool TryParseDateTimeValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out DateTime value)
		{
			if (ParseSerialize.CheckOffsetLength(buffer, offset, 8) && ParseSerialize.TryParseFileTime(buffer, offset, out value))
			{
				offset += 8;
				return true;
			}
			value = default(DateTime);
			return false;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001C2F8 File Offset: 0x0001A4F8
		private static bool TryParseGuidValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out Guid value)
		{
			if (ParseSerialize.CheckOffsetLength(buffer, offset, 16))
			{
				value = ParseSerialize.ParseGuid(buffer, offset);
				offset += 16;
				return true;
			}
			value = default(Guid);
			return false;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001C328 File Offset: 0x0001A528
		private static bool TryParseBinaryValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out byte[] value)
		{
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && ParseSerialize.CheckOffsetLength(buffer, offset, num))
			{
				value = ParseSerialize.ParseBinary(buffer, offset, num);
				offset += num;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001C364 File Offset: 0x0001A564
		private static bool TryParseStringValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out string value)
		{
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && ParseSerialize.CheckOffsetLength(buffer, offset, num))
			{
				if ((byte)(format & SerializedValue.ValueFormat.StringEncodingMask) != 0)
				{
					value = ParseSerialize.ParseUtf8String(buffer, offset, num);
					offset += num;
					return true;
				}
				if ((num & 1) == 0)
				{
					value = ParseSerialize.ParseUcs16String(buffer, offset, num);
					offset += num;
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
		private static bool TryGetStringValueSize(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out int size)
		{
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && ParseSerialize.CheckOffsetLength(buffer, offset, num))
			{
				if ((byte)(format & SerializedValue.ValueFormat.StringEncodingMask) != 0)
				{
					size = ParseSerialize.GetLengthOfUtf8String(buffer, offset, num) * 2;
					offset += num;
					return true;
				}
				if ((num & 1) == 0)
				{
					size = num;
					offset += num;
					return true;
				}
			}
			size = 0;
			return false;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001C414 File Offset: 0x0001A614
		private static bool TryParseMVInt16Value(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out short[] value)
		{
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && num >= 0 && num <= (buffer.Length - offset) / 2 && ParseSerialize.CheckOffsetLength(buffer, offset, num * 2))
			{
				value = new short[num];
				for (int i = 0; i < num; i++)
				{
					value[i] = ParseSerialize.ParseInt16(buffer, offset);
					offset += 2;
				}
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001C478 File Offset: 0x0001A678
		private static bool TryParseMVInt32Value(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out int[] value)
		{
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && num >= 0 && num <= (buffer.Length - offset) / 4 && ParseSerialize.CheckOffsetLength(buffer, offset, num * 4))
			{
				value = new int[num];
				for (int i = 0; i < num; i++)
				{
					value[i] = ParseSerialize.ParseInt32(buffer, offset);
					offset += 4;
				}
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001C4DC File Offset: 0x0001A6DC
		private static bool TryParseMVInt64Value(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out long[] value)
		{
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && num >= 0 && num <= (buffer.Length - offset) / 8 && ParseSerialize.CheckOffsetLength(buffer, offset, num * 8))
			{
				value = new long[num];
				for (int i = 0; i < num; i++)
				{
					value[i] = ParseSerialize.ParseInt64(buffer, offset);
					offset += 8;
				}
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001C540 File Offset: 0x0001A740
		private static bool TryParseMVSingleValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out float[] value)
		{
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && num >= 0 && num <= (buffer.Length - offset) / 4 && ParseSerialize.CheckOffsetLength(buffer, offset, num * 4))
			{
				value = new float[num];
				for (int i = 0; i < num; i++)
				{
					value[i] = ParseSerialize.ParseSingle(buffer, offset);
					offset += 4;
				}
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001C5A4 File Offset: 0x0001A7A4
		private static bool TryParseMVDoubleValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out double[] value)
		{
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && num >= 0 && num <= (buffer.Length - offset) / 8 && ParseSerialize.CheckOffsetLength(buffer, offset, num * 8))
			{
				value = new double[num];
				for (int i = 0; i < num; i++)
				{
					value[i] = ParseSerialize.ParseDouble(buffer, offset);
					offset += 8;
				}
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001C608 File Offset: 0x0001A808
		private static bool TryParseMVDateTimeValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out DateTime[] value)
		{
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && num >= 0 && num <= (buffer.Length - offset) / 8 && ParseSerialize.CheckOffsetLength(buffer, offset, num * 8))
			{
				value = new DateTime[num];
				for (int i = 0; i < num; i++)
				{
					if (!ParseSerialize.TryParseFileTime(buffer, offset, out value[i]))
					{
						value = null;
						return false;
					}
					offset += 8;
				}
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001C674 File Offset: 0x0001A874
		private static bool TryParseMVGuidValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out Guid[] value)
		{
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && num >= 0 && num <= (buffer.Length - offset) / 16 && ParseSerialize.CheckOffsetLength(buffer, offset, num * 16))
			{
				value = new Guid[num];
				for (int i = 0; i < num; i++)
				{
					value[i] = ParseSerialize.ParseGuid(buffer, offset);
					offset += 16;
				}
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001C6E4 File Offset: 0x0001A8E4
		private static bool TryParseMVBinaryValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out byte[][] value)
		{
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && num >= 0 && num <= (buffer.Length - offset) / 1 && ParseSerialize.CheckOffsetLength(buffer, offset, num))
			{
				value = new byte[num][];
				for (int i = 0; i < num; i++)
				{
					if (!ParseSerialize.CheckOffsetLength(buffer, offset, 1))
					{
						value = null;
						return false;
					}
					SerializedValue.ValueFormat valueFormat = (SerializedValue.ValueFormat)buffer[offset++];
					if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
					{
						value[i] = null;
					}
					else
					{
						if ((byte)(valueFormat & SerializedValue.ValueFormat.TypeMask) != 80)
						{
							value = null;
							return false;
						}
						if (!SerializedValue.TryParseBinaryValue(valueFormat, buffer, ref offset, out value[i]))
						{
							value = null;
							return false;
						}
					}
				}
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001C780 File Offset: 0x0001A980
		private static bool TryGetMVBinaryValueSize(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out int size)
		{
			size = 0;
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && num >= 0 && num <= (buffer.Length - offset) / 1 && ParseSerialize.CheckOffsetLength(buffer, offset, num))
			{
				for (int i = 0; i < num; i++)
				{
					if (!ParseSerialize.CheckOffsetLength(buffer, offset, 1))
					{
						return false;
					}
					SerializedValue.ValueFormat valueFormat = (SerializedValue.ValueFormat)buffer[offset++];
					if (valueFormat != SerializedValue.ValueFormat.FormatModifierShift)
					{
						if ((byte)(valueFormat & SerializedValue.ValueFormat.TypeMask) != 80)
						{
							return false;
						}
						int num2;
						if (!SerializedValue.TryParseLength(valueFormat, buffer, ref offset, out num2) || !ParseSerialize.CheckOffsetLength(buffer, offset, num2))
						{
							return false;
						}
						size += num2;
						offset += num2;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001C818 File Offset: 0x0001AA18
		private static bool TryParseMVStringValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out string[] value)
		{
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && num >= 0 && num <= (buffer.Length - offset) / 1 && ParseSerialize.CheckOffsetLength(buffer, offset, num))
			{
				value = new string[num];
				for (int i = 0; i < num; i++)
				{
					if (!ParseSerialize.CheckOffsetLength(buffer, offset, 1))
					{
						value = null;
						return false;
					}
					SerializedValue.ValueFormat valueFormat = (SerializedValue.ValueFormat)buffer[offset++];
					if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
					{
						value[i] = null;
					}
					else
					{
						if ((byte)(valueFormat & SerializedValue.ValueFormat.TypeMask) != 72)
						{
							value = null;
							return false;
						}
						if (!SerializedValue.TryParseStringValue(valueFormat, buffer, ref offset, out value[i]))
						{
							value = null;
							return false;
						}
					}
				}
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001C8B4 File Offset: 0x0001AAB4
		private static bool TryGetMVStringValueSize(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out int size)
		{
			size = 0;
			int num;
			if (SerializedValue.TryParseLength(format, buffer, ref offset, out num) && num >= 0 && num <= (buffer.Length - offset) / 1 && ParseSerialize.CheckOffsetLength(buffer, offset, num))
			{
				for (int i = 0; i < num; i++)
				{
					if (!ParseSerialize.CheckOffsetLength(buffer, offset, 1))
					{
						return false;
					}
					SerializedValue.ValueFormat valueFormat = (SerializedValue.ValueFormat)buffer[offset++];
					if (valueFormat != SerializedValue.ValueFormat.FormatModifierShift)
					{
						if ((byte)(valueFormat & SerializedValue.ValueFormat.TypeMask) != 72)
						{
							return false;
						}
						int num2;
						if (!SerializedValue.TryGetStringValueSize(valueFormat, buffer, ref offset, out num2))
						{
							return false;
						}
						size += num2;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001C939 File Offset: 0x0001AB39
		private static bool TryParseReferenceValue(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out ValueReference value)
		{
			if ((byte)(format & SerializedValue.ValueFormat.StringEncodingMask) != 0)
			{
				value = ValueReference.Zero;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001C950 File Offset: 0x0001AB50
		private static bool TryParseLength(SerializedValue.ValueFormat format, byte[] buffer, ref int offset, out int length)
		{
			switch ((byte)(format & SerializedValue.ValueFormat.TypeShift))
			{
			case 0:
				length = 0;
				return true;
			case 1:
				if (ParseSerialize.CheckOffsetLength(buffer, offset, 1))
				{
					length = (int)buffer[offset++];
					return true;
				}
				break;
			case 2:
				if (ParseSerialize.CheckOffsetLength(buffer, offset, 2))
				{
					length = (int)((ushort)ParseSerialize.ParseInt16(buffer, offset));
					offset += 2;
					return true;
				}
				break;
			case 3:
				if (ParseSerialize.CheckOffsetLength(buffer, offset, 4))
				{
					length = ParseSerialize.ParseInt32(buffer, offset);
					offset += 4;
					return true;
				}
				break;
			}
			length = 0;
			return false;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001C9D8 File Offset: 0x0001ABD8
		public static int ComputeSize(object value)
		{
			return SerializedValue.SerializeImpl(value, null, 0);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001C9E2 File Offset: 0x0001ABE2
		public static int ComputeSize(SerializedValue.ValueFormat format, object value)
		{
			return SerializedValue.SerializeImpl(format, value, null, 0);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001C9F0 File Offset: 0x0001ABF0
		public static int ComputeSize(IList<object> valueList)
		{
			int num = 0;
			foreach (object value in valueList)
			{
				num += SerializedValue.SerializeImpl(value, null, 0);
			}
			return num;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001CA40 File Offset: 0x0001AC40
		public static byte[] Serialize(object value)
		{
			int num = SerializedValue.ComputeSize(value);
			byte[] array = new byte[num];
			int num2 = 0;
			SerializedValue.Serialize(value, array, ref num2);
			return array;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001CA68 File Offset: 0x0001AC68
		public static byte[] Serialize(IList<object> valueList)
		{
			int num = SerializedValue.ComputeSize(valueList);
			byte[] array = new byte[num];
			int num2 = 0;
			SerializedValue.Serialize(valueList, array, ref num2);
			return array;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001CA90 File Offset: 0x0001AC90
		public static byte[] Serialize(params object[] valueList)
		{
			int num = SerializedValue.ComputeSize(valueList);
			byte[] array = new byte[num];
			int num2 = 0;
			SerializedValue.Serialize(valueList, array, ref num2);
			return array;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001CAB7 File Offset: 0x0001ACB7
		public static void Serialize(object value, byte[] buffer, ref int offset)
		{
			offset += SerializedValue.SerializeImpl(value, buffer, offset);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001CAC7 File Offset: 0x0001ACC7
		public static void Serialize(SerializedValue.ValueFormat format, object value, byte[] buffer, ref int offset)
		{
			offset += SerializedValue.SerializeImpl(format, value, buffer, offset);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001CAD8 File Offset: 0x0001ACD8
		public static void Serialize(IList<object> valueList, byte[] buffer, ref int offset)
		{
			foreach (object value in valueList)
			{
				offset += SerializedValue.SerializeImpl(value, buffer, offset);
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0001CB28 File Offset: 0x0001AD28
		private static int SerializeImpl(object value, byte[] buffer, int offset)
		{
			SerializedValue.ValueFormat valueFormatType = SerializedValue.ValueFormat.FormatModifierShift;
			if (value != null && !(value is ValueReference))
			{
				switch (ValueTypeHelper.GetExtendedTypeCode(value.GetType()))
				{
				case ExtendedTypeCode.Boolean:
					valueFormatType = SerializedValue.ValueFormat.Boolean;
					break;
				case ExtendedTypeCode.Int16:
					valueFormatType = SerializedValue.ValueFormat.Int16;
					break;
				case ExtendedTypeCode.Int32:
					valueFormatType = SerializedValue.ValueFormat.Int32;
					break;
				case ExtendedTypeCode.Int64:
					valueFormatType = SerializedValue.ValueFormat.Int64;
					break;
				case ExtendedTypeCode.Single:
					valueFormatType = SerializedValue.ValueFormat.Single;
					break;
				case ExtendedTypeCode.Double:
					valueFormatType = SerializedValue.ValueFormat.Double;
					break;
				case ExtendedTypeCode.DateTime:
					valueFormatType = SerializedValue.ValueFormat.DateTime;
					break;
				case ExtendedTypeCode.Guid:
					valueFormatType = SerializedValue.ValueFormat.Guid;
					break;
				case ExtendedTypeCode.String:
					valueFormatType = SerializedValue.ValueFormat.String;
					break;
				case ExtendedTypeCode.Binary:
					valueFormatType = SerializedValue.ValueFormat.Binary;
					break;
				case ExtendedTypeCode.MVInt16:
					valueFormatType = SerializedValue.ValueFormat.MVInt16;
					break;
				case ExtendedTypeCode.MVInt32:
					valueFormatType = SerializedValue.ValueFormat.MVInt32;
					break;
				case ExtendedTypeCode.MVInt64:
					valueFormatType = SerializedValue.ValueFormat.MVInt64;
					break;
				case ExtendedTypeCode.MVSingle:
					valueFormatType = SerializedValue.ValueFormat.MVSingle;
					break;
				case ExtendedTypeCode.MVDouble:
					valueFormatType = SerializedValue.ValueFormat.MVDouble;
					break;
				case ExtendedTypeCode.MVDateTime:
					valueFormatType = SerializedValue.ValueFormat.MVDateTime;
					break;
				case ExtendedTypeCode.MVGuid:
					valueFormatType = SerializedValue.ValueFormat.MVGuid;
					break;
				case ExtendedTypeCode.MVString:
					valueFormatType = SerializedValue.ValueFormat.MVString;
					break;
				case ExtendedTypeCode.MVBinary:
					valueFormatType = SerializedValue.ValueFormat.MVBinary;
					break;
				}
			}
			return SerializedValue.SerializeImpl(valueFormatType, value, buffer, offset);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0001CC48 File Offset: 0x0001AE48
		private static int SerializeImpl(SerializedValue.ValueFormat valueFormatType, object value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				valueFormatType = SerializedValue.ValueFormat.FormatModifierShift;
			}
			else if (value is ValueReference)
			{
				valueFormatType = SerializedValue.ValueFormat.Reference;
			}
			SerializedValue.ValueFormat valueFormat = valueFormatType;
			if (valueFormat <= SerializedValue.ValueFormat.String)
			{
				if (valueFormat <= SerializedValue.ValueFormat.Int64)
				{
					if (valueFormat <= SerializedValue.ValueFormat.Boolean)
					{
						if (valueFormat == SerializedValue.ValueFormat.FormatModifierShift)
						{
							return SerializedValue.SerializeNull(buffer, offset);
						}
						if (valueFormat == SerializedValue.ValueFormat.Boolean)
						{
							return SerializedValue.SerializeBoolean((bool)value, buffer, offset);
						}
					}
					else
					{
						if (valueFormat == SerializedValue.ValueFormat.Int16)
						{
							return SerializedValue.SerializeInt16((short)value, buffer, offset);
						}
						if (valueFormat == SerializedValue.ValueFormat.Int32)
						{
							return SerializedValue.SerializeInt32((int)value, buffer, offset);
						}
						if (valueFormat == SerializedValue.ValueFormat.Int64)
						{
							return SerializedValue.SerializeInt64((long)value, buffer, offset);
						}
					}
				}
				else if (valueFormat <= SerializedValue.ValueFormat.Double)
				{
					if (valueFormat == SerializedValue.ValueFormat.Single)
					{
						return SerializedValue.SerializeSingle((float)value, buffer, offset);
					}
					if (valueFormat == SerializedValue.ValueFormat.Double)
					{
						return SerializedValue.SerializeDouble((double)value, buffer, offset);
					}
				}
				else
				{
					if (valueFormat == SerializedValue.ValueFormat.DateTime)
					{
						return SerializedValue.SerializeDateTime((DateTime)value, buffer, offset);
					}
					if (valueFormat == SerializedValue.ValueFormat.Guid)
					{
						return SerializedValue.SerializeGuid((Guid)value, buffer, offset);
					}
					if (valueFormat == SerializedValue.ValueFormat.String)
					{
						return SerializedValue.SerializeString((string)value, buffer, offset);
					}
				}
			}
			else if (valueFormat <= SerializedValue.ValueFormat.MVInt64)
			{
				if (valueFormat <= SerializedValue.ValueFormat.Reference)
				{
					if (valueFormat == SerializedValue.ValueFormat.Binary)
					{
						return SerializedValue.SerializeBinary((byte[])value, buffer, offset);
					}
					if (valueFormat == SerializedValue.ValueFormat.Reference)
					{
						return SerializedValue.SerializeReference((ValueReference)value, buffer, offset);
					}
				}
				else
				{
					if (valueFormat == SerializedValue.ValueFormat.MVInt16)
					{
						return SerializedValue.SerializeMVInt16((short[])value, buffer, offset);
					}
					if (valueFormat == SerializedValue.ValueFormat.MVInt32)
					{
						return SerializedValue.SerializeMVInt32((int[])value, buffer, offset);
					}
					if (valueFormat == SerializedValue.ValueFormat.MVInt64)
					{
						return SerializedValue.SerializeMVInt64((long[])value, buffer, offset);
					}
				}
			}
			else if (valueFormat <= SerializedValue.ValueFormat.MVDateTime)
			{
				if (valueFormat == SerializedValue.ValueFormat.MVSingle)
				{
					return SerializedValue.SerializeMVSingle((float[])value, buffer, offset);
				}
				if (valueFormat == SerializedValue.ValueFormat.MVDouble)
				{
					return SerializedValue.SerializeMVDouble((double[])value, buffer, offset);
				}
				if (valueFormat == SerializedValue.ValueFormat.MVDateTime)
				{
					return SerializedValue.SerializeMVDateTime((DateTime[])value, buffer, offset);
				}
			}
			else
			{
				if (valueFormat == SerializedValue.ValueFormat.MVGuid)
				{
					return SerializedValue.SerializeMVGuid((Guid[])value, buffer, offset);
				}
				if (valueFormat == SerializedValue.ValueFormat.MVString)
				{
					return SerializedValue.SerializeMVString((string[])value, buffer, offset);
				}
				if (valueFormat == SerializedValue.ValueFormat.MVBinary)
				{
					return SerializedValue.SerializeMVBinary((byte[][])value, buffer, offset);
				}
			}
			return 0;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001CE9D File Offset: 0x0001B09D
		public static int SerializeNull(byte[] buffer, int offset)
		{
			if (buffer != null)
			{
				buffer[offset] = 0;
			}
			return 1;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001CEA7 File Offset: 0x0001B0A7
		public static int SerializeBoolean(bool value, byte[] buffer, int offset)
		{
			if (buffer != null)
			{
				buffer[offset] = (byte)(8 | (value ? 1 : 0));
			}
			return 1;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001CEBA File Offset: 0x0001B0BA
		public static int SerializeInt16(short value, byte[] buffer, int offset)
		{
			if (value == 0)
			{
				if (buffer != null)
				{
					buffer[offset] = 16;
				}
				return 1;
			}
			if (-128 <= value && value <= 127)
			{
				if (buffer != null)
				{
					buffer[offset] = 17;
					buffer[offset + 1] = (byte)value;
				}
				return 2;
			}
			if (buffer != null)
			{
				buffer[offset] = 18;
				ParseSerialize.SerializeInt16(value, buffer, offset + 1);
			}
			return 3;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001CEF8 File Offset: 0x0001B0F8
		public static int SerializeInt32(int value, byte[] buffer, int offset)
		{
			if (value == 0)
			{
				if (buffer != null)
				{
					buffer[offset] = 24;
				}
				return 1;
			}
			if (-128 <= value && value <= 127)
			{
				if (buffer != null)
				{
					buffer[offset] = 25;
					buffer[offset + 1] = (byte)value;
				}
				return 2;
			}
			if (-32768 <= value && value <= 32767)
			{
				if (buffer != null)
				{
					buffer[offset] = 26;
					ParseSerialize.SerializeInt16((short)value, buffer, offset + 1);
				}
				return 3;
			}
			if (buffer != null)
			{
				buffer[offset] = 27;
				ParseSerialize.SerializeInt32(value, buffer, offset + 1);
			}
			return 5;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001CF68 File Offset: 0x0001B168
		public static int SerializeInt64(long value, byte[] buffer, int offset)
		{
			if (value == 0L)
			{
				if (buffer != null)
				{
					buffer[offset] = 32;
				}
				return 1;
			}
			if (-128L <= value && value <= 127L)
			{
				if (buffer != null)
				{
					buffer[offset] = 33;
					buffer[offset + 1] = (byte)value;
				}
				return 2;
			}
			if (-32768L <= value && value <= 32767L)
			{
				if (buffer != null)
				{
					buffer[offset] = 34;
					ParseSerialize.SerializeInt16((short)value, buffer, offset + 1);
				}
				return 3;
			}
			if (-2147483648L <= value && value <= 2147483647L)
			{
				if (buffer != null)
				{
					buffer[offset] = 35;
					ParseSerialize.SerializeInt32((int)value, buffer, offset + 1);
				}
				return 5;
			}
			if (buffer != null)
			{
				buffer[offset] = 36;
				ParseSerialize.SerializeInt64(value, buffer, offset + 1);
			}
			return 9;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001D006 File Offset: 0x0001B206
		public static int SerializeSingle(float value, byte[] buffer, int offset)
		{
			if (buffer != null)
			{
				buffer[offset] = 40;
				ParseSerialize.SerializeSingle(value, buffer, offset + 1);
			}
			return 5;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001D01C File Offset: 0x0001B21C
		public static int SerializeDouble(double value, byte[] buffer, int offset)
		{
			if (buffer != null)
			{
				buffer[offset] = 48;
				ParseSerialize.SerializeDouble(value, buffer, offset + 1);
			}
			return 9;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001D033 File Offset: 0x0001B233
		public static int SerializeDateTime(DateTime value, byte[] buffer, int offset)
		{
			if (buffer != null)
			{
				buffer[offset] = 56;
				ParseSerialize.SerializeFileTime(value, buffer, offset + 1);
			}
			return 9;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001D04A File Offset: 0x0001B24A
		public static int SerializeGuid(Guid value, byte[] buffer, int offset)
		{
			if (buffer != null)
			{
				buffer[offset] = 64;
				ParseSerialize.SerializeGuid(value, buffer, offset + 1);
			}
			return 17;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001D064 File Offset: 0x0001B264
		public static int SerializeString(string value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				return SerializedValue.SerializeNull(buffer, offset);
			}
			SerializedValue.ValueFormat valueFormat;
			int num;
			if (value.Length == 0)
			{
				valueFormat = SerializedValue.ValueFormat.String;
				num = 0;
			}
			else
			{
				int byteCount = Encoding.Unicode.GetByteCount(value);
				int byteCount2 = Encoding.UTF8.GetByteCount(value);
				if (byteCount <= byteCount2)
				{
					valueFormat = SerializedValue.ValueFormat.String;
					num = byteCount;
				}
				else
				{
					valueFormat = (SerializedValue.ValueFormat)76;
					num = byteCount2;
				}
			}
			int num2 = SerializedValue.SerializeHeader(valueFormat, num, buffer, offset);
			if (buffer != null && value.Length != 0)
			{
				if (valueFormat == (SerializedValue.ValueFormat)76)
				{
					Encoding.UTF8.GetBytes(value, 0, value.Length, buffer, offset + num2);
				}
				else
				{
					Encoding.Unicode.GetBytes(value, 0, value.Length, buffer, offset + num2);
				}
			}
			return num2 + num;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001D104 File Offset: 0x0001B304
		public static int SerializeBinary(byte[] value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				return SerializedValue.SerializeNull(buffer, offset);
			}
			int num = SerializedValue.SerializeHeader(SerializedValue.ValueFormat.Binary, value.Length, buffer, offset);
			if (buffer != null)
			{
				Buffer.BlockCopy(value, 0, buffer, offset + num, value.Length);
			}
			return num + value.Length;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001D140 File Offset: 0x0001B340
		public static int SerializeMVInt16(short[] value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				return SerializedValue.SerializeNull(buffer, offset);
			}
			int num = SerializedValue.SerializeHeader(SerializedValue.ValueFormat.MVInt16, value.Length, buffer, offset);
			if (buffer != null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					ParseSerialize.SerializeInt16(value[i], buffer, offset + num + i * 2);
				}
			}
			return num + value.Length * 2;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001D194 File Offset: 0x0001B394
		public static int SerializeMVInt32(int[] value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				return SerializedValue.SerializeNull(buffer, offset);
			}
			int num = SerializedValue.SerializeHeader(SerializedValue.ValueFormat.MVInt32, value.Length, buffer, offset);
			if (buffer != null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					ParseSerialize.SerializeInt32(value[i], buffer, offset + num + i * 4);
				}
			}
			return num + value.Length * 4;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
		public static int SerializeMVInt64(long[] value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				return SerializedValue.SerializeNull(buffer, offset);
			}
			int num = SerializedValue.SerializeHeader(SerializedValue.ValueFormat.MVInt64, value.Length, buffer, offset);
			if (buffer != null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					ParseSerialize.SerializeInt64(value[i], buffer, offset + num + i * 8);
				}
			}
			return num + value.Length * 8;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001D23C File Offset: 0x0001B43C
		public static int SerializeMVSingle(float[] value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				return SerializedValue.SerializeNull(buffer, offset);
			}
			int num = SerializedValue.SerializeHeader(SerializedValue.ValueFormat.MVSingle, value.Length, buffer, offset);
			if (buffer != null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					ParseSerialize.SerializeSingle(value[i], buffer, offset + num + i * 4);
				}
			}
			return num + value.Length * 4;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001D290 File Offset: 0x0001B490
		public static int SerializeMVDouble(double[] value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				return SerializedValue.SerializeNull(buffer, offset);
			}
			int num = SerializedValue.SerializeHeader(SerializedValue.ValueFormat.MVDouble, value.Length, buffer, offset);
			if (buffer != null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					ParseSerialize.SerializeDouble(value[i], buffer, offset + num + i * 8);
				}
			}
			return num + value.Length * 8;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001D2E4 File Offset: 0x0001B4E4
		public static int SerializeMVDateTime(DateTime[] value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				return SerializedValue.SerializeNull(buffer, offset);
			}
			int num = SerializedValue.SerializeHeader(SerializedValue.ValueFormat.MVDateTime, value.Length, buffer, offset);
			if (buffer != null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					ParseSerialize.SerializeFileTime(value[i], buffer, offset + num + i * 8);
				}
			}
			return num + value.Length * 8;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001D340 File Offset: 0x0001B540
		public static int SerializeMVGuid(Guid[] value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				return SerializedValue.SerializeNull(buffer, offset);
			}
			int num = SerializedValue.SerializeHeader(SerializedValue.ValueFormat.MVGuid, value.Length, buffer, offset);
			if (buffer != null)
			{
				for (int i = 0; i < value.Length; i++)
				{
					ParseSerialize.SerializeGuid(value[i], buffer, offset + num + i * 16);
				}
			}
			return num + value.Length * 16;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0001D39C File Offset: 0x0001B59C
		public static int SerializeMVString(string[] value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				return SerializedValue.SerializeNull(buffer, offset);
			}
			int num = SerializedValue.SerializeHeader(SerializedValue.ValueFormat.MVString, value.Length, buffer, offset);
			offset += num;
			for (int i = 0; i < value.Length; i++)
			{
				int num2;
				if (value[i] == null)
				{
					num2 = SerializedValue.SerializeNull(buffer, offset);
				}
				else
				{
					num2 = SerializedValue.SerializeString(value[i], buffer, offset);
				}
				num += num2;
				offset += num2;
			}
			return num;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0001D3FC File Offset: 0x0001B5FC
		public static int SerializeMVBinary(byte[][] value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				return SerializedValue.SerializeNull(buffer, offset);
			}
			int num = SerializedValue.SerializeHeader(SerializedValue.ValueFormat.MVBinary, value.Length, buffer, offset);
			offset += num;
			for (int i = 0; i < value.Length; i++)
			{
				int num2;
				if (value[i] == null)
				{
					num2 = SerializedValue.SerializeNull(buffer, offset);
				}
				else
				{
					num2 = SerializedValue.SerializeBinary(value[i], buffer, offset);
				}
				num += num2;
				offset += num2;
			}
			return num;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0001D45B File Offset: 0x0001B65B
		public static int SerializeReference(ValueReference value, byte[] buffer, int offset)
		{
			if (value == null)
			{
				return SerializedValue.SerializeNull(buffer, offset);
			}
			if (value.IsZero)
			{
				if (buffer != null)
				{
					buffer[offset] = 124;
				}
				return 1;
			}
			Globals.AssertRetail(false, "unexpected reference value");
			return 0;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001D488 File Offset: 0x0001B688
		private static int SerializeHeader(SerializedValue.ValueFormat format, int length, byte[] buffer, int offset)
		{
			int num;
			if (length == 0)
			{
				num = 0;
				format = format;
			}
			else if (length <= 255)
			{
				num = 1;
				format |= SerializedValue.ValueFormat.LengthSizeOneByte;
			}
			else if (length <= 65535)
			{
				num = 2;
				format |= SerializedValue.ValueFormat.LengthSizeTwoBytes;
			}
			else
			{
				num = 4;
				format |= SerializedValue.ValueFormat.TypeShift;
			}
			if (buffer != null)
			{
				buffer[offset] = (byte)format;
				switch ((byte)(format & SerializedValue.ValueFormat.TypeShift))
				{
				case 1:
					buffer[offset + 1] = (byte)length;
					break;
				case 2:
					ParseSerialize.SerializeInt16((short)length, buffer, offset + 1);
					break;
				case 3:
					ParseSerialize.SerializeInt32(length, buffer, offset + 1);
					break;
				}
			}
			return 1 + num;
		}

		// Token: 0x04000741 RID: 1857
		private const int MaximumInt32ToCache = 100;

		// Token: 0x04000742 RID: 1858
		private static readonly object boxedTrue = true;

		// Token: 0x04000743 RID: 1859
		private static readonly object boxedFalse = false;

		// Token: 0x04000744 RID: 1860
		private static readonly object[] boxedInts = new object[100];

		// Token: 0x020000C7 RID: 199
		public enum ValueFormat : byte
		{
			// Token: 0x04000746 RID: 1862
			TypeShift = 3,
			// Token: 0x04000747 RID: 1863
			TypeMask = 248,
			// Token: 0x04000748 RID: 1864
			FormatModifierMask = 7,
			// Token: 0x04000749 RID: 1865
			FormatModifierShift = 0,
			// Token: 0x0400074A RID: 1866
			Null = 0,
			// Token: 0x0400074B RID: 1867
			Boolean = 8,
			// Token: 0x0400074C RID: 1868
			Int16 = 16,
			// Token: 0x0400074D RID: 1869
			Int32 = 24,
			// Token: 0x0400074E RID: 1870
			Int64 = 32,
			// Token: 0x0400074F RID: 1871
			Single = 40,
			// Token: 0x04000750 RID: 1872
			Double = 48,
			// Token: 0x04000751 RID: 1873
			DateTime = 56,
			// Token: 0x04000752 RID: 1874
			Guid = 64,
			// Token: 0x04000753 RID: 1875
			String = 72,
			// Token: 0x04000754 RID: 1876
			Binary = 80,
			// Token: 0x04000755 RID: 1877
			Reserved2 = 104,
			// Token: 0x04000756 RID: 1878
			Reserved1 = 112,
			// Token: 0x04000757 RID: 1879
			Reference = 120,
			// Token: 0x04000758 RID: 1880
			MVFlag = 128,
			// Token: 0x04000759 RID: 1881
			MVInt16 = 144,
			// Token: 0x0400075A RID: 1882
			MVInt32 = 152,
			// Token: 0x0400075B RID: 1883
			MVInt64 = 160,
			// Token: 0x0400075C RID: 1884
			MVSingle = 168,
			// Token: 0x0400075D RID: 1885
			MVDouble = 176,
			// Token: 0x0400075E RID: 1886
			MVDateTime = 184,
			// Token: 0x0400075F RID: 1887
			MVGuid = 192,
			// Token: 0x04000760 RID: 1888
			MVString = 200,
			// Token: 0x04000761 RID: 1889
			MVBinary = 208,
			// Token: 0x04000762 RID: 1890
			LengthMask = 3,
			// Token: 0x04000763 RID: 1891
			LengthZero = 0,
			// Token: 0x04000764 RID: 1892
			LengthSizeOneByte,
			// Token: 0x04000765 RID: 1893
			LengthSizeTwoBytes,
			// Token: 0x04000766 RID: 1894
			LengthSizeFourBytes,
			// Token: 0x04000767 RID: 1895
			StringEncodingMask,
			// Token: 0x04000768 RID: 1896
			StringEncodingUcs16 = 0,
			// Token: 0x04000769 RID: 1897
			StringEncodingUtf8 = 4,
			// Token: 0x0400076A RID: 1898
			IntegerSizeMask = 7,
			// Token: 0x0400076B RID: 1899
			IntegerSizeZero = 0,
			// Token: 0x0400076C RID: 1900
			IntegerSizeOneByte,
			// Token: 0x0400076D RID: 1901
			IntegerSizeTwoBytes,
			// Token: 0x0400076E RID: 1902
			IntegerSizeFourBytes,
			// Token: 0x0400076F RID: 1903
			IntegerSizeEightBytes,
			// Token: 0x04000770 RID: 1904
			BooleanFalse = 0,
			// Token: 0x04000771 RID: 1905
			BooleanTrue,
			// Token: 0x04000772 RID: 1906
			ReferenceZero = 4
		}
	}
}

using System;
using System.Text;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000173 RID: 371
	public static class ParseSerialize
	{
		// Token: 0x06000FA0 RID: 4000 RVA: 0x0004A862 File Offset: 0x00048A62
		public static bool CheckOffsetLength(int maxOffset, int offset, int length)
		{
			return offset >= 0 && length >= 0 && offset <= maxOffset && length <= maxOffset - offset;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0004A87B File Offset: 0x00048A7B
		public static bool CheckOffsetLength(byte[] buffer, int offset, int length)
		{
			return ParseSerialize.CheckOffsetLength(buffer.Length, offset, length);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0004A887 File Offset: 0x00048A87
		public static short ParseInt16(byte[] buffer, int offset)
		{
			return BitConverter.ToInt16(buffer, offset);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0004A890 File Offset: 0x00048A90
		public static int ParseInt32(byte[] buffer, int offset)
		{
			return BitConverter.ToInt32(buffer, offset);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0004A899 File Offset: 0x00048A99
		public static long ParseInt64(byte[] buffer, int offset)
		{
			return BitConverter.ToInt64(buffer, offset);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0004A8A2 File Offset: 0x00048AA2
		public static float ParseSingle(byte[] buffer, int offset)
		{
			return BitConverter.ToSingle(buffer, offset);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0004A8AB File Offset: 0x00048AAB
		public static double ParseDouble(byte[] buffer, int offset)
		{
			return BitConverter.ToDouble(buffer, offset);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0004A8B4 File Offset: 0x00048AB4
		public static Guid ParseGuid(byte[] buffer, int offset)
		{
			return ExBitConverter.ReadGuid(buffer, offset);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0004A8C0 File Offset: 0x00048AC0
		public static byte[] ParseBinary(byte[] buffer, int offset, int length)
		{
			if (length == 0)
			{
				return ParseSerialize.emptyByteArray;
			}
			byte[] array = new byte[length];
			Buffer.BlockCopy(buffer, offset, array, 0, length);
			return array;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0004A8E8 File Offset: 0x00048AE8
		public static string ParseUcs16String(byte[] buffer, int offset, int length)
		{
			if (length != 0)
			{
				return Encoding.Unicode.GetString(buffer, offset, length);
			}
			return string.Empty;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0004A900 File Offset: 0x00048B00
		public static string ParseUtf8String(byte[] buffer, int offset, int length)
		{
			if (length != 0)
			{
				return Encoding.UTF8.GetString(buffer, offset, length);
			}
			return string.Empty;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0004A918 File Offset: 0x00048B18
		public static int GetLengthOfUtf8String(byte[] buffer, int offset, int length)
		{
			if (length != 0)
			{
				return Encoding.UTF8.GetCharCount(buffer, offset, length);
			}
			return 0;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0004A92C File Offset: 0x00048B2C
		public static string ParseAsciiString(byte[] buffer, int offset, int length)
		{
			if (length != 0)
			{
				return CTSGlobals.AsciiEncoding.GetString(buffer, offset, length);
			}
			return string.Empty;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0004A944 File Offset: 0x00048B44
		public static DateTime ParseFileTime(byte[] buffer, int offset)
		{
			DateTime result;
			ParseSerialize.TryParseFileTime(buffer, offset, out result);
			return result;
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0004A95C File Offset: 0x00048B5C
		public static bool TryParseFileTime(byte[] buffer, int offset, out DateTime dateTime)
		{
			long fileTime = ParseSerialize.ParseInt64(buffer, offset);
			return ParseSerialize.TryConvertFileTime(fileTime, out dateTime);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0004A978 File Offset: 0x00048B78
		public static bool TryConvertFileTime(long fileTime, out DateTime dateTime)
		{
			bool result;
			if (fileTime < ParseSerialize.MinFileTime || fileTime >= ParseSerialize.MaxFileTime)
			{
				dateTime = DateTime.MaxValue;
				result = (fileTime == long.MaxValue);
			}
			else if (fileTime == 0L)
			{
				dateTime = DateTime.MinValue;
				result = true;
			}
			else
			{
				dateTime = DateTime.FromFileTimeUtc(fileTime);
				result = true;
			}
			return result;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0004A9D4 File Offset: 0x00048BD4
		public static int SerializeInt16(short value, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)value;
			buffer[offset + 1] = (byte)(value >> 8);
			return 2;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0004A9E5 File Offset: 0x00048BE5
		public static int SerializeInt32(int value, byte[] buffer, int offset)
		{
			ExBitConverter.Write(value, buffer, offset);
			return 4;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0004A9F1 File Offset: 0x00048BF1
		public static int SerializeInt64(long value, byte[] buffer, int offset)
		{
			ExBitConverter.Write(value, buffer, offset);
			return 8;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0004A9FD File Offset: 0x00048BFD
		public static int SerializeSingle(float value, byte[] buffer, int offset)
		{
			ExBitConverter.Write(value, buffer, offset);
			return 4;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0004AA0A File Offset: 0x00048C0A
		public static int SerializeDouble(double value, byte[] buffer, int offset)
		{
			ExBitConverter.Write(value, buffer, offset);
			return 8;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0004AA17 File Offset: 0x00048C17
		public static int SerializeGuid(Guid value, byte[] buffer, int offset)
		{
			ExBitConverter.Write(value, buffer, offset);
			return 16;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0004AA24 File Offset: 0x00048C24
		public static int SerializeFileTime(DateTime dateTime, byte[] buffer, int offset)
		{
			long value;
			if (dateTime < ParseSerialize.MinFileTimeDateTime)
			{
				value = 0L;
			}
			else if (dateTime == DateTime.MaxValue)
			{
				value = long.MaxValue;
			}
			else
			{
				value = dateTime.ToFileTimeUtc();
			}
			ParseSerialize.SerializeInt64(value, buffer, offset);
			return 8;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0004AA6E File Offset: 0x00048C6E
		public static int SerializeAsciiString(string value, byte[] buffer, int offset)
		{
			CTSGlobals.AsciiEncoding.GetBytes(value, 0, value.Length, buffer, offset);
			return value.Length;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0004AA8B File Offset: 0x00048C8B
		public static void SetWord(byte[] buff, ref int pos, ushort w)
		{
			ParseSerialize.SetWord(buff, ref pos, (short)w);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0004AA96 File Offset: 0x00048C96
		public static void SetWord(byte[] buff, ref int pos, short w)
		{
			ParseSerialize.CheckBounds(pos, buff, 2);
			if (buff != null)
			{
				ParseSerialize.SerializeInt16(w, buff, pos);
			}
			pos += 2;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0004AAB4 File Offset: 0x00048CB4
		public static void SetDword(byte[] buff, ref int pos, uint dw)
		{
			ParseSerialize.SetDword(buff, ref pos, (int)dw);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0004AABE File Offset: 0x00048CBE
		public static void SetDword(byte[] buff, ref int pos, int dw)
		{
			ParseSerialize.CheckBounds(pos, buff, 4);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(dw, buff, pos);
			}
			pos += 4;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0004AADC File Offset: 0x00048CDC
		public static void SetQword(byte[] buff, ref int pos, ulong qw)
		{
			ParseSerialize.SetQword(buff, ref pos, (long)qw);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0004AAE6 File Offset: 0x00048CE6
		public static void SetQword(byte[] buff, ref int pos, long qw)
		{
			ParseSerialize.CheckBounds(pos, buff, 8);
			if (buff != null)
			{
				ParseSerialize.SerializeInt64(qw, buff, pos);
			}
			pos += 8;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0004AB04 File Offset: 0x00048D04
		public static void SetSysTime(byte[] buff, ref int pos, DateTime value)
		{
			ParseSerialize.CheckBounds(pos, buff, 8);
			if (buff != null)
			{
				ParseSerialize.SerializeFileTime(value, buff, pos);
			}
			pos += 8;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0004AB22 File Offset: 0x00048D22
		public static void SetBoolean(byte[] buff, ref int pos, bool value)
		{
			ParseSerialize.SetByte(buff, ref pos, value ? 1 : 0);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0004AB32 File Offset: 0x00048D32
		public static void SetByte(byte[] buff, ref int pos, byte b)
		{
			ParseSerialize.CheckBounds(pos, buff, 1);
			if (buff != null)
			{
				buff[pos] = b;
			}
			pos++;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0004AB4C File Offset: 0x00048D4C
		public static void SetUnicodeString(byte[] buff, ref int pos, string str)
		{
			ParseSerialize.CheckBounds(pos, buff, (str.Length + 1) * 2);
			if (buff != null)
			{
				Encoding.Unicode.GetBytes(str, 0, str.Length, buff, pos);
				buff[pos + str.Length * 2] = 0;
				buff[pos + str.Length * 2 + 1] = 0;
			}
			pos += (str.Length + 1) * 2;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0004ABB4 File Offset: 0x00048DB4
		public static void SetASCIIString(byte[] buff, ref int pos, string str)
		{
			ParseSerialize.CheckBounds(pos, buff, str.Length + 1);
			if (buff != null)
			{
				CTSGlobals.AsciiEncoding.GetBytes(str, 0, str.Length, buff, pos);
				buff[pos + str.Length] = 0;
			}
			pos += str.Length + 1;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0004AC03 File Offset: 0x00048E03
		public static void SetByteArray(byte[] buff, ref int pos, byte[] byteArray)
		{
			ParseSerialize.CheckBounds(pos, buff, 2 + byteArray.Length);
			if (buff != null)
			{
				ParseSerialize.SerializeInt16((short)byteArray.Length, buff, pos);
				Buffer.BlockCopy(byteArray, 0, buff, pos + 2, byteArray.Length);
			}
			pos += 2 + byteArray.Length;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0004AC3B File Offset: 0x00048E3B
		public static void SetFloat(byte[] buff, ref int pos, float fl)
		{
			ParseSerialize.CheckBounds(pos, buff, 4);
			if (buff != null)
			{
				ParseSerialize.SerializeSingle(fl, buff, pos);
			}
			pos += 4;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0004AC59 File Offset: 0x00048E59
		public static void SetDouble(byte[] buff, ref int pos, double dbl)
		{
			ParseSerialize.CheckBounds(pos, buff, 8);
			if (buff != null)
			{
				ParseSerialize.SerializeDouble(dbl, buff, pos);
			}
			pos += 8;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0004AC77 File Offset: 0x00048E77
		public static void SetGuid(byte[] buff, ref int pos, Guid guid)
		{
			ParseSerialize.CheckBounds(pos, buff, 16);
			if (buff != null)
			{
				ParseSerialize.SerializeGuid(guid, buff, pos);
			}
			pos += 16;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0004AC98 File Offset: 0x00048E98
		public static void SetMVInt16(byte[] buff, ref int pos, short[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 2);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeInt16(values[i], buff, pos + 4 + i * 2);
				}
			}
			pos += 4 + values.Length * 2;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0004ACF0 File Offset: 0x00048EF0
		public static void SetMVInt32(byte[] buff, ref int pos, int[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 4);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeInt32(values[i], buff, pos + 4 + i * 4);
				}
			}
			pos += 4 + values.Length * 4;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0004AD48 File Offset: 0x00048F48
		public static void SetMVInt64(byte[] buff, ref int pos, long[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 8);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeInt64(values[i], buff, pos + 4 + i * 8);
				}
			}
			pos += 4 + values.Length * 8;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0004ADA0 File Offset: 0x00048FA0
		public static void SetMVReal32(byte[] buff, ref int pos, float[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 4);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeSingle(values[i], buff, pos + 4 + i * 4);
				}
			}
			pos += 4 + values.Length * 4;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0004ADF8 File Offset: 0x00048FF8
		public static void SetMVReal64(byte[] buff, ref int pos, double[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 8);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeDouble(values[i], buff, pos + 4 + i * 8);
				}
			}
			pos += 4 + values.Length * 8;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0004AE50 File Offset: 0x00049050
		public static void SetMVGuid(byte[] buff, ref int pos, Guid[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 16);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeGuid(values[i], buff, pos + 4 + i * 16);
				}
			}
			pos += 4 + values.Length * 16;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0004AEB4 File Offset: 0x000490B4
		public static void SetMVSystime(byte[] buff, ref int pos, DateTime[] values)
		{
			ParseSerialize.CheckBounds(pos, buff, 4 + values.Length * 8);
			if (buff != null)
			{
				ParseSerialize.SerializeInt32(values.Length, buff, pos);
				for (int i = 0; i < values.Length; i++)
				{
					ParseSerialize.SerializeFileTime(values[i], buff, pos + 4 + i * 8);
				}
			}
			pos += 4 + values.Length * 8;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0004AF14 File Offset: 0x00049114
		public static void SetMVUnicode(byte[] buff, ref int pos, string[] values)
		{
			ParseSerialize.SetDword(buff, ref pos, values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				ParseSerialize.SetUnicodeString(buff, ref pos, values[i]);
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0004AF44 File Offset: 0x00049144
		public static void SetMVBinary(byte[] buff, ref int pos, byte[][] values)
		{
			ParseSerialize.SetDword(buff, ref pos, values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				ParseSerialize.SetByteArray(buff, ref pos, values[i]);
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0004AF73 File Offset: 0x00049173
		public static void CheckBounds(int pos, int posMax, int sizeNeeded)
		{
			if (!ParseSerialize.CheckOffsetLength(posMax, pos, sizeNeeded))
			{
				throw new BufferTooSmallException("Request would overflow buffer");
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0004AF8A File Offset: 0x0004918A
		public static void CheckBounds(int pos, byte[] buffer, int sizeNeeded)
		{
			if (buffer != null)
			{
				ParseSerialize.CheckBounds(pos, buffer.Length, sizeNeeded);
			}
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0004AF99 File Offset: 0x00049199
		internal static void CheckCount(uint count, int elementSize, int availableSize)
		{
			if (count < 0U)
			{
				throw new BufferTooSmallException("value count is negative");
			}
			if ((ulong)count * (ulong)((long)elementSize) > (ulong)((long)availableSize))
			{
				throw new BufferTooSmallException("overflow available size");
			}
			if ((ulong)count * (ulong)((long)elementSize) > 536870911UL)
			{
				throw new BufferTooSmallException("value count is too large");
			}
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0004AFD8 File Offset: 0x000491D8
		public static byte GetByte(byte[] buff, ref int pos, int posMax)
		{
			ParseSerialize.CheckBounds(pos, posMax, 1);
			byte result = buff[pos];
			pos++;
			return result;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0004AFFC File Offset: 0x000491FC
		public static uint GetDword(byte[] buff, ref int pos, int posMax)
		{
			ParseSerialize.CheckBounds(pos, posMax, 4);
			uint result = (uint)ParseSerialize.ParseInt32(buff, pos);
			pos += 4;
			return result;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0004B024 File Offset: 0x00049224
		public static ushort GetWord(byte[] buff, ref int pos, int posMax)
		{
			ParseSerialize.CheckBounds(pos, posMax, 2);
			ushort result = (ushort)ParseSerialize.ParseInt16(buff, pos);
			pos += 2;
			return result;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0004B04C File Offset: 0x0004924C
		public static float GetFloat(byte[] buff, ref int pos, int posMax)
		{
			ParseSerialize.CheckBounds(pos, posMax, 4);
			float result = ParseSerialize.ParseSingle(buff, pos);
			pos += 4;
			return result;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0004B074 File Offset: 0x00049274
		public static ulong GetQword(byte[] buff, ref int pos, int posMax)
		{
			ParseSerialize.CheckBounds(pos, posMax, 8);
			ulong result = (ulong)ParseSerialize.ParseInt64(buff, pos);
			pos += 8;
			return result;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0004B09C File Offset: 0x0004929C
		public static double GetDouble(byte[] buff, ref int pos, int posMax)
		{
			ParseSerialize.CheckBounds(pos, posMax, 8);
			double result = ParseSerialize.ParseDouble(buff, pos);
			pos += 8;
			return result;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0004B0C4 File Offset: 0x000492C4
		public static DateTime GetSysTime(byte[] buff, ref int pos, int posMax)
		{
			ParseSerialize.CheckBounds(pos, posMax, 8);
			DateTime result = ParseSerialize.ParseFileTime(buff, pos);
			pos += 8;
			return result;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0004B0EA File Offset: 0x000492EA
		public static bool GetBoolean(byte[] buff, ref int pos, int posMax)
		{
			return 0 != ParseSerialize.GetByte(buff, ref pos, posMax);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0004B0FC File Offset: 0x000492FC
		public static Guid GetGuid(byte[] buff, ref int pos, int posMax)
		{
			ParseSerialize.CheckBounds(pos, posMax, 16);
			Guid result = ParseSerialize.ParseGuid(buff, pos);
			pos += 16;
			return result;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0004B124 File Offset: 0x00049324
		public static byte[][] GetMVBinary(byte[] buff, ref int pos, int posMax)
		{
			uint dword = ParseSerialize.GetDword(buff, ref pos, posMax);
			ParseSerialize.CheckCount(dword, 2, posMax - pos);
			byte[][] array = new byte[dword][];
			int num = 0;
			while ((long)num < (long)((ulong)dword))
			{
				array[num] = ParseSerialize.GetByteArray(buff, ref pos, posMax);
				num++;
			}
			return array;
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0004B168 File Offset: 0x00049368
		public static short[] GetMVInt16(byte[] buff, ref int pos, int posMax)
		{
			uint dword = ParseSerialize.GetDword(buff, ref pos, posMax);
			ParseSerialize.CheckCount(dword, 2, posMax - pos);
			short[] array = new short[dword];
			int num = 0;
			while ((long)num < (long)((ulong)dword))
			{
				array[num] = (short)ParseSerialize.GetWord(buff, ref pos, posMax);
				num++;
			}
			return array;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0004B1AC File Offset: 0x000493AC
		public static int[] GetMVInt32(byte[] buff, ref int pos, int posMax)
		{
			uint dword = ParseSerialize.GetDword(buff, ref pos, posMax);
			ParseSerialize.CheckCount(dword, 4, posMax - pos);
			int[] array = new int[dword];
			int num = 0;
			while ((long)num < (long)((ulong)dword))
			{
				array[num] = (int)ParseSerialize.GetDword(buff, ref pos, posMax);
				num++;
			}
			return array;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0004B1F0 File Offset: 0x000493F0
		public static float[] GetMVReal32(byte[] buff, ref int pos, int posMax)
		{
			uint dword = ParseSerialize.GetDword(buff, ref pos, posMax);
			ParseSerialize.CheckCount(dword, 4, posMax - pos);
			float[] array = new float[dword];
			int num = 0;
			while ((long)num < (long)((ulong)dword))
			{
				array[num] = ParseSerialize.GetFloat(buff, ref pos, posMax);
				num++;
			}
			return array;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0004B234 File Offset: 0x00049434
		public static double[] GetMVR8(byte[] buff, ref int pos, int posMax)
		{
			uint dword = ParseSerialize.GetDword(buff, ref pos, posMax);
			ParseSerialize.CheckCount(dword, 8, posMax - pos);
			double[] array = new double[dword];
			int num = 0;
			while ((long)num < (long)((ulong)dword))
			{
				array[num] = ParseSerialize.GetDouble(buff, ref pos, posMax);
				num++;
			}
			return array;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0004B278 File Offset: 0x00049478
		public static long[] GetMVInt64(byte[] buff, ref int pos, int posMax)
		{
			uint dword = ParseSerialize.GetDword(buff, ref pos, posMax);
			ParseSerialize.CheckCount(dword, 8, posMax - pos);
			long[] array = new long[dword];
			int num = 0;
			while ((long)num < (long)((ulong)dword))
			{
				array[num] = (long)ParseSerialize.GetQword(buff, ref pos, posMax);
				num++;
			}
			return array;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0004B2BC File Offset: 0x000494BC
		public static DateTime[] GetMVSysTime(byte[] buff, ref int pos, int posMax)
		{
			uint dword = ParseSerialize.GetDword(buff, ref pos, posMax);
			ParseSerialize.CheckCount(dword, 8, posMax - pos);
			DateTime[] array = new DateTime[dword];
			int num = 0;
			while ((long)num < (long)((ulong)dword))
			{
				array[num] = ParseSerialize.GetSysTime(buff, ref pos, posMax);
				num++;
			}
			return array;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0004B308 File Offset: 0x00049508
		public static Guid[] GetMVGuid(byte[] buff, ref int pos, int posMax)
		{
			uint dword = ParseSerialize.GetDword(buff, ref pos, posMax);
			ParseSerialize.CheckCount(dword, 16, posMax - pos);
			Guid[] array = new Guid[dword];
			int num = 0;
			while ((long)num < (long)((ulong)dword))
			{
				array[num] = ParseSerialize.GetGuid(buff, ref pos, posMax);
				num++;
			}
			return array;
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0004B358 File Offset: 0x00049558
		public static string GetStringFromUnicode(byte[] buff, ref int pos, int posMax)
		{
			int num = 0;
			ParseSerialize.CheckBounds(pos, posMax, 2);
			while (buff[pos + num] != 0 || buff[pos + num + 1] != 0)
			{
				num += 2;
				ParseSerialize.CheckBounds(pos + num, posMax, 2);
			}
			return ParseSerialize.GetStringFromUnicode(buff, ref pos, posMax, num + 2);
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0004B3A0 File Offset: 0x000495A0
		public static string GetStringFromUnicode(byte[] buff, ref int pos, int posMax, int byteCount)
		{
			ParseSerialize.CheckBounds(pos, posMax, byteCount);
			string @string = Encoding.Unicode.GetString(buff, pos, byteCount - 2);
			pos += byteCount;
			return @string;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0004B3D0 File Offset: 0x000495D0
		public static byte PeekByte(byte[] buff, int pos, int posMax)
		{
			ParseSerialize.CheckBounds(pos, posMax, 1);
			return buff[pos];
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0004B3EC File Offset: 0x000495EC
		public static string GetStringFromASCII(byte[] buff, ref int pos, int posMax)
		{
			int num = 0;
			while (pos + num < posMax && buff[pos + num] != 0)
			{
				num++;
			}
			if (pos + num >= posMax)
			{
				throw new BufferTooSmallException("Request would overflow buffer");
			}
			return ParseSerialize.GetStringFromASCII(buff, ref pos, posMax, num + 1);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0004B430 File Offset: 0x00049630
		public static string GetStringFromASCII(byte[] buff, ref int pos, int posMax, int charCount)
		{
			ParseSerialize.CheckBounds(pos, posMax, charCount);
			string @string = CTSGlobals.AsciiEncoding.GetString(buff, pos, charCount - 1);
			pos += charCount;
			return @string;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0004B460 File Offset: 0x00049660
		public static string GetStringFromASCIINoNull(byte[] buff, ref int pos, int posMax, int charCount)
		{
			ParseSerialize.CheckBounds(pos, posMax, charCount);
			string @string = CTSGlobals.AsciiEncoding.GetString(buff, pos, charCount);
			pos += charCount;
			return @string;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0004B48C File Offset: 0x0004968C
		public static string[] GetMVUnicode(byte[] buff, ref int pos, int posMax)
		{
			uint dword = ParseSerialize.GetDword(buff, ref pos, posMax);
			ParseSerialize.CheckCount(dword, 2, posMax - pos);
			string[] array = new string[dword];
			int num = 0;
			while ((long)num < (long)((ulong)dword))
			{
				array[num] = ParseSerialize.GetStringFromUnicode(buff, ref pos, posMax);
				num++;
			}
			return array;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0004B4D0 File Offset: 0x000496D0
		public static byte[] GetByteArray(byte[] buff, ref int pos, int posMax)
		{
			int word = (int)ParseSerialize.GetWord(buff, ref pos, posMax);
			ParseSerialize.CheckBounds(pos, posMax, word);
			byte[] result = ParseSerialize.ParseBinary(buff, pos, word);
			pos += word;
			return result;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0004B500 File Offset: 0x00049700
		public static string[] GetMVString8(byte[] buff, ref int pos, int posMax)
		{
			uint dword = ParseSerialize.GetDword(buff, ref pos, posMax);
			ParseSerialize.CheckCount(dword, 1, posMax - pos);
			string[] array = new string[dword];
			int num = 0;
			while ((long)num < (long)((ulong)dword))
			{
				array[num] = ParseSerialize.GetStringFromASCII(buff, ref pos, posMax);
				num++;
			}
			return array;
		}

		// Token: 0x04000923 RID: 2339
		public const int SizeOfByte = 1;

		// Token: 0x04000924 RID: 2340
		public const int SizeOfInt16 = 2;

		// Token: 0x04000925 RID: 2341
		public const int SizeOfInt32 = 4;

		// Token: 0x04000926 RID: 2342
		public const int SizeOfInt64 = 8;

		// Token: 0x04000927 RID: 2343
		public const int SizeOfSingle = 4;

		// Token: 0x04000928 RID: 2344
		public const int SizeOfDouble = 8;

		// Token: 0x04000929 RID: 2345
		public const int SizeOfGuid = 16;

		// Token: 0x0400092A RID: 2346
		public const int SizeOfFileTime = 8;

		// Token: 0x0400092B RID: 2347
		public const int SizeOfUnicodeChar = 2;

		// Token: 0x0400092C RID: 2348
		public static readonly long MinFileTime = 0L;

		// Token: 0x0400092D RID: 2349
		public static readonly long MaxFileTime = DateTime.MaxValue.ToFileTimeUtc();

		// Token: 0x0400092E RID: 2350
		public static readonly DateTime MinFileTimeDateTime = DateTime.FromFileTimeUtc(ParseSerialize.MinFileTime);

		// Token: 0x0400092F RID: 2351
		private static readonly byte[] emptyByteArray = new byte[0];
	}
}

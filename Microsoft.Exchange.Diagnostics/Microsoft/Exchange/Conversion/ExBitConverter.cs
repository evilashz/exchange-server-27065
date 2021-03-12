using System;
using System.Text;

namespace Microsoft.Exchange.Conversion
{
	// Token: 0x02000004 RID: 4
	internal static class ExBitConverter
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000226E File Offset: 0x0000046E
		public static int Write(short value, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)value;
			buffer[offset + 1] = (byte)(value >> 8);
			return 2;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000227F File Offset: 0x0000047F
		public static int Write(ushort value, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)value;
			buffer[offset + 1] = (byte)(value >> 8);
			return 2;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002290 File Offset: 0x00000490
		public static int Write(int value, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)value;
			buffer[offset + 1] = (byte)(value >> 8);
			buffer[offset + 2] = (byte)(value >> 16);
			buffer[offset + 3] = (byte)(value >> 24);
			return 4;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022B5 File Offset: 0x000004B5
		public static int Write(uint value, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)value;
			buffer[offset + 1] = (byte)(value >> 8);
			buffer[offset + 2] = (byte)(value >> 16);
			buffer[offset + 3] = (byte)(value >> 24);
			return 4;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022DA File Offset: 0x000004DA
		public static int Write(long value, byte[] buffer, int offset)
		{
			ExBitConverter.Write((int)value, buffer, offset);
			ExBitConverter.Write((int)(value >> 32), buffer, offset + 4);
			return 8;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022F6 File Offset: 0x000004F6
		public static int Write(ulong value, byte[] buffer, int offset)
		{
			return ExBitConverter.Write((long)value, buffer, offset);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002300 File Offset: 0x00000500
		public unsafe static int Write(float value, byte[] buffer, int offset)
		{
			return ExBitConverter.Write(*(int*)(&value), buffer, offset);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000230D File Offset: 0x0000050D
		public unsafe static int Write(double value, byte[] buffer, int offset)
		{
			return ExBitConverter.Write(*(long*)(&value), buffer, offset);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000231A File Offset: 0x0000051A
		public static int Write(char value, byte[] buffer, int offset)
		{
			return ExBitConverter.Write((short)value, buffer, offset);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002328 File Offset: 0x00000528
		public unsafe static int Write(Guid value, byte[] buffer, int offset)
		{
			byte* ptr = (byte*)(&value);
			for (int i = 0; i < 16; i += 4)
			{
				buffer[offset] = ptr[i];
				buffer[offset + 1] = ptr[i + 1];
				buffer[offset + 2] = ptr[i + 2];
				buffer[offset + 3] = ptr[i + 3];
				offset += 4;
			}
			return 16;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002378 File Offset: 0x00000578
		public static int Write(string value, int maxCharCount, bool unicode, byte[] buffer, int offset)
		{
			int num = Math.Min(value.Length, maxCharCount);
			if (!unicode)
			{
				for (int i = 0; i < num; i++)
				{
					buffer[offset++] = ((value[i] < '\u0080') ? ((byte)value[i]) : 63);
				}
				buffer[offset++] = 0;
				return num + 1;
			}
			int bytes = CTSGlobals.UnicodeEncoding.GetBytes(value, 0, num, buffer, offset);
			return bytes + ExBitConverter.Write(0, buffer, offset + bytes);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023F2 File Offset: 0x000005F2
		public static int Write(string value, bool unicode, byte[] buffer, int offset)
		{
			return ExBitConverter.Write(value, value.Length, unicode, buffer, offset);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002404 File Offset: 0x00000604
		public unsafe static Guid ReadGuid(byte[] buffer, int offset)
		{
			if (offset < 0 || offset > buffer.Length - sizeof(Guid))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			Guid result;
			byte* ptr = (byte*)(&result);
			for (int i = 0; i < sizeof(Guid); i += 4)
			{
				ptr[i] = buffer[offset];
				ptr[i + 1] = buffer[offset + 1];
				ptr[i + 2] = buffer[offset + 2];
				ptr[i + 3] = buffer[offset + 3];
				offset += 4;
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002470 File Offset: 0x00000670
		public static byte[] ReadByteArray(byte[] buffer, int offset, int length)
		{
			byte[] array = new byte[length];
			Array.Copy(buffer, offset, array, 0, length);
			return array;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002490 File Offset: 0x00000690
		public static string ReadAsciiString(byte[] buffer, int offset)
		{
			int num = offset;
			while (num < buffer.Length && buffer[num] != 0)
			{
				num++;
			}
			StringBuilder stringBuilder = new StringBuilder(num - offset);
			for (int i = offset; i < num; i++)
			{
				stringBuilder.Append((char)((buffer[i] < 128) ? buffer[i] : 63));
			}
			return stringBuilder.ToString();
		}
	}
}

using System;
using System.Text;

namespace Microsoft.Exchange.Conversion
{
	// Token: 0x0200005A RID: 90
	public static class HexConverter
	{
		// Token: 0x06000283 RID: 643 RVA: 0x0000C153 File Offset: 0x0000A353
		public static byte[] HexStringToByteArray(string value)
		{
			return HexConverter.HexStringToByteArray(value, 0, value.Length);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C164 File Offset: 0x0000A364
		public static byte[] HexStringToByteArray(string value, int offset, int length)
		{
			if (length % 2 != 0)
			{
				throw new FormatException("Invalid Hex Data");
			}
			int num = length / 2;
			byte[] array = new byte[num];
			int num2 = offset;
			for (int i = 0; i < num; i++)
			{
				array[i] = HexConverter.ByteFromHexPair(value[num2++], value[num2++]);
			}
			return array;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		public static byte ByteFromHexPair(char firstChar, char secondChar)
		{
			byte b = HexConverter.NumFromHex(firstChar);
			byte b2 = HexConverter.NumFromHex(secondChar);
			return (byte)((int)b << 4 | (int)b2);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
		public static byte NumFromHex(char ch)
		{
			byte b = (ch < '\u0080') ? HexConverter.HexCharToNum[(int)ch] : byte.MaxValue;
			if (b != 255)
			{
				return b;
			}
			throw new FormatException("Invalid Hex Data");
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C218 File Offset: 0x0000A418
		public static byte NumFromHex(byte ch)
		{
			return HexConverter.HexCharToNum[(int)ch];
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000C221 File Offset: 0x0000A421
		public static string ByteArrayToHexString(byte[] array)
		{
			if (array == null)
			{
				return null;
			}
			return HexConverter.ByteArrayToHexString(array, 0, array.Length);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000C234 File Offset: 0x0000A434
		public static string ByteArrayToHexString(byte[] array, int start, int count)
		{
			if (array == null)
			{
				return null;
			}
			byte[] array2 = new byte[count * 2];
			int num = 0;
			for (int i = start; i < start + count; i++)
			{
				array2[num++] = HexConverter.NibbleToHex[array[i] >> 4];
				array2[num++] = HexConverter.NibbleToHex[(int)(array[i] & 15)];
			}
			return HexConverter.asciiEncoding.GetString(array2, 0, array2.Length);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C294 File Offset: 0x0000A494
		internal static string ByteArrayToEscapedHexString(byte[] array, byte escapeByte, int start, int count)
		{
			if (array == null)
			{
				return null;
			}
			byte[] array2 = new byte[count * 3];
			int num = 0;
			for (int i = start; i < start + count; i++)
			{
				array2[num++] = escapeByte;
				array2[num++] = HexConverter.NibbleToHex[array[i] >> 4];
				array2[num++] = HexConverter.NibbleToHex[(int)(array[i] & 15)];
			}
			return HexConverter.asciiEncoding.GetString(array2, 0, array2.Length);
		}

		// Token: 0x0400018B RID: 395
		private static readonly Encoding asciiEncoding = Encoding.GetEncoding("us-ascii");

		// Token: 0x0400018C RID: 396
		private static readonly byte[] NibbleToHex = HexConverter.asciiEncoding.GetBytes("0123456789ABCDEF");

		// Token: 0x0400018D RID: 397
		private static readonly byte[] HexCharToNum = new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			10,
			11,
			12,
			13,
			14,
			15,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			10,
			11,
			12,
			13,
			14,
			15,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};
	}
}

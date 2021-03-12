using System;
using System.Text;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200044B RID: 1099
	internal static class TransportProperty
	{
		// Token: 0x060032AB RID: 12971 RVA: 0x000C7678 File Offset: 0x000C5878
		public static int GetDword(byte[] value)
		{
			if (value.Length != 4)
			{
				throw new TransportPropertyException("invalid length");
			}
			return BitConverter.ToInt32(value, 0);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000C7692 File Offset: 0x000C5892
		public static byte[] PutDword(int value)
		{
			return BitConverter.GetBytes(value);
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000C769A File Offset: 0x000C589A
		public static bool GetBool(byte[] value)
		{
			return TransportProperty.GetDword(value) != 0;
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000C76A8 File Offset: 0x000C58A8
		public static byte[] PutBool(bool value)
		{
			return TransportProperty.PutDword(value ? 1 : 0);
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000C76B8 File Offset: 0x000C58B8
		public static string GetASCIIString(byte[] value)
		{
			int num = Array.IndexOf<byte>(value, 0);
			if (num == -1)
			{
				throw new TransportPropertyException("missing null terminator");
			}
			string @string;
			try
			{
				@string = TransportProperty.CheckedASCII.GetString(value, 0, num);
			}
			catch (DecoderFallbackException innerException)
			{
				throw new TransportPropertyException("invalid encoding", innerException);
			}
			return @string;
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000C770C File Offset: 0x000C590C
		public static byte[] PutASCIIString(string value)
		{
			byte[] array = new byte[TransportProperty.CheckedASCII.GetByteCount(value) + 1];
			TransportProperty.CheckedASCII.GetBytes(value, 0, value.Length, array, 0);
			array[array.Length - 1] = 0;
			return array;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000C774C File Offset: 0x000C594C
		public static string GetUTF8String(byte[] value)
		{
			int num = Array.IndexOf<byte>(value, 0);
			if (num == -1)
			{
				throw new TransportPropertyException("missing null terminator");
			}
			string @string;
			try
			{
				@string = TransportProperty.CheckedUTF8.GetString(value, 0, num);
			}
			catch (DecoderFallbackException innerException)
			{
				throw new TransportPropertyException("invalid encoding", innerException);
			}
			return @string;
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000C77A0 File Offset: 0x000C59A0
		public static byte[] PutUTF8String(string value)
		{
			byte[] array = new byte[TransportProperty.CheckedUTF8.GetByteCount(value) + 1];
			TransportProperty.CheckedUTF8.GetBytes(value, 0, value.Length, array, 0);
			array[array.Length - 1] = 0;
			return array;
		}

		// Token: 0x040019AF RID: 6575
		private static readonly Encoding CheckedASCII = Encoding.GetEncoding("us-ascii", new EncoderExceptionFallback(), new DecoderExceptionFallback());

		// Token: 0x040019B0 RID: 6576
		private static readonly Encoding CheckedUTF8 = new UTF8Encoding(false, true);
	}
}

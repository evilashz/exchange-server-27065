using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000189 RID: 393
	internal static class HexStringConverter
	{
		// Token: 0x0600110D RID: 4365 RVA: 0x0005DF30 File Offset: 0x0005C130
		public static byte[] GetBytes(string str, bool allowOddLength = false)
		{
			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentNullException("Null HexCodedString");
			}
			if (str.Length % 2 == 1)
			{
				if (!allowOddLength)
				{
					throw new ArgumentException("Bad Hexcoded string");
				}
				str = "0" + str;
			}
			byte[] array = new byte[str.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = byte.Parse(str.Substring(i * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			return array;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0005DFB4 File Offset: 0x0005C1B4
		public static byte[] GetBytes(string str, int length)
		{
			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentNullException("Null HexCodedString");
			}
			if (length > str.Length)
			{
				length = str.Length;
			}
			if (length % 2 == 1)
			{
				throw new ArgumentException("Bad Hexcoded string substring length");
			}
			byte[] array = new byte[length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				if (!byte.TryParse(str.Substring(i * 2, 2), NumberStyles.HexNumber, null, out array[i]))
				{
					return null;
				}
			}
			return array;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0005E030 File Offset: 0x0005C230
		public static string GetString(byte[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}
	}
}

﻿using System;
using System.Text;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200001A RID: 26
	public static class Imap4UTF7Encoding
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00005994 File Offset: 0x00003B94
		public static bool TryDecode(string encodedString, out string decodedString)
		{
			decodedString = string.Empty;
			if (string.IsNullOrEmpty(encodedString))
			{
				return true;
			}
			StringBuilder stringBuilder = new StringBuilder(encodedString.Length);
			byte[] array = null;
			int num = 0;
			int i = encodedString.IndexOf(Imap4UTF7Encoding.cShiftUTF7);
			while (i > -1)
			{
				if (i > encodedString.Length - 1)
				{
					return false;
				}
				if (num < i)
				{
					stringBuilder.Append(encodedString.Substring(num, i - num));
				}
				num = i;
				i = encodedString.IndexOf(Imap4UTF7Encoding.cShiftASCII, num);
				if (i == -1)
				{
					return false;
				}
				if (num + 1 == i)
				{
					stringBuilder.Append(Imap4UTF7Encoding.cShiftUTF7);
					num = i + 1;
					i = encodedString.IndexOf(Imap4UTF7Encoding.cShiftUTF7, num);
				}
				else
				{
					if (array == null)
					{
						array = Encoding.ASCII.GetBytes(encodedString);
					}
					array[num] = (byte)Imap4UTF7Encoding.cOriginalShiftUTF7;
					for (int j = num; j < i; j++)
					{
						if (array[j] == Imap4UTF7Encoding.bComma)
						{
							array[j] = Imap4UTF7Encoding.bSlash;
						}
					}
					string @string = Encoding.UTF7.GetString(array, num, i - num);
					if (@string.Length == 0)
					{
						return false;
					}
					stringBuilder.Append(@string);
					num = i + 1;
					i = encodedString.IndexOf(Imap4UTF7Encoding.cShiftUTF7, num);
				}
			}
			if (num == 0)
			{
				decodedString = encodedString;
				return true;
			}
			if (num < encodedString.Length)
			{
				stringBuilder.Append(encodedString.Substring(num));
			}
			decodedString = stringBuilder.ToString();
			return true;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005AD0 File Offset: 0x00003CD0
		public static string Encode(string decodedString)
		{
			if (string.IsNullOrEmpty(decodedString))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(decodedString.Length * 3);
			char[] array = null;
			for (int i = 0; i < decodedString.Length; i++)
			{
				if (decodedString[i] <= '\u007f' && decodedString[i] != Imap4UTF7Encoding.cShiftUTF7)
				{
					int num = i;
					while (i < decodedString.Length && decodedString[i] <= '\u007f' && decodedString[i] != Imap4UTF7Encoding.cShiftUTF7)
					{
						i++;
					}
					if (num == 0 && i == decodedString.Length)
					{
						return decodedString;
					}
					stringBuilder.Append(decodedString.Substring(num, i - num));
					i--;
				}
				else if (decodedString[i] == Imap4UTF7Encoding.cShiftUTF7)
				{
					stringBuilder.Append(Imap4UTF7Encoding.cShiftUTF7);
					stringBuilder.Append(Imap4UTF7Encoding.cShiftASCII);
				}
				else
				{
					if (array == null)
					{
						array = decodedString.ToCharArray();
					}
					int num = i;
					while (i < decodedString.Length && decodedString[i] > '\u007f')
					{
						i++;
					}
					byte[] bytes = Encoding.UTF7.GetBytes(array, num, i - num);
					bytes[0] = (byte)Imap4UTF7Encoding.cShiftUTF7;
					for (int j = 0; j < bytes.Length; j++)
					{
						if (bytes[j] == Imap4UTF7Encoding.bSlash)
						{
							stringBuilder.Append(Imap4UTF7Encoding.cComma);
						}
						else
						{
							stringBuilder.Append((char)bytes[j]);
						}
					}
					i--;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000BD RID: 189
		private static char cShiftUTF7 = '&';

		// Token: 0x040000BE RID: 190
		private static char cOriginalShiftUTF7 = '+';

		// Token: 0x040000BF RID: 191
		private static char cShiftASCII = '-';

		// Token: 0x040000C0 RID: 192
		private static char cSlash = '/';

		// Token: 0x040000C1 RID: 193
		private static byte bSlash = (byte)Imap4UTF7Encoding.cSlash;

		// Token: 0x040000C2 RID: 194
		private static char cComma = ',';

		// Token: 0x040000C3 RID: 195
		private static byte bComma = (byte)Imap4UTF7Encoding.cComma;
	}
}

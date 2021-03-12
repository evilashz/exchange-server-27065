using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Clients.Common.FBL
{
	// Token: 0x0200003A RID: 58
	internal class XssEncode
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0000C059 File Offset: 0x0000A259
		public static string UrlEncode(string input)
		{
			return XssEncode.UrlEncode(input, Encoding.UTF8);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000C068 File Offset: 0x0000A268
		public static string UrlEncode(string input, Encoding outputEncoding)
		{
			string result;
			if (string.IsNullOrEmpty(input))
			{
				result = input;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder(input.Length * 2);
				int length = input.Length;
				for (int i = 0; i < length; i++)
				{
					char c = input[i];
					uint num = (uint)c;
					bool flag = (num > 96U && num < 123U) || (num > 64U && num < 91U) || (num > 47U && num < 58U) || num == 46U || num == 45U || num == 95U;
					if (flag)
					{
						stringBuilder.Append(c);
					}
					else
					{
						byte[] array = null;
						if (char.IsSurrogatePair(input, i))
						{
							array = outputEncoding.GetBytes(new char[]
							{
								c,
								input[i + 1]
							});
							i++;
						}
						else if (char.IsSurrogate(c))
						{
							c = XssEncode.UndefinedChar;
						}
						if (array == null)
						{
							array = outputEncoding.GetBytes(c.ToString());
						}
						int num2 = array.Length;
						for (int j = 0; j < num2; j++)
						{
							stringBuilder.Append("%" + array[j].ToString("x", CultureInfo.InvariantCulture).PadLeft(2, '0'));
						}
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0400030E RID: 782
		private const uint UndefinedCharUint = 65533U;

		// Token: 0x0400030F RID: 783
		private static readonly char UndefinedChar = Convert.ToChar(65533U);
	}
}

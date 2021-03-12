using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000E6 RID: 230
	internal static class DNConvertor
	{
		// Token: 0x06000B4D RID: 2893 RVA: 0x00034008 File Offset: 0x00032208
		public static string FqdnFromDomainDistinguishedName(string distinguishedName)
		{
			if (distinguishedName == null)
			{
				throw new ArgumentNullException("distinguishedName");
			}
			return distinguishedName.Substring(3).ToLowerInvariant().Replace(",dc=", ".");
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00034034 File Offset: 0x00032234
		public static string[] SplitDistinguishedName(string distinguishedName, char separator)
		{
			if (string.IsNullOrEmpty(distinguishedName))
			{
				throw new ArgumentNullException("distinguishedName");
			}
			List<string> list = new List<string>(distinguishedName.Length / 4);
			int num = 0;
			do
			{
				int num2 = DNConvertor.IndexOfUnescapedChar(distinguishedName, num, separator);
				if (num2 == -1)
				{
					num2 = distinguishedName.Length;
				}
				string text = distinguishedName.Substring(num, num2 - num);
				if (!string.IsNullOrEmpty(text))
				{
					list.Add(text);
				}
				num = num2 + 1;
			}
			while (num < distinguishedName.Length);
			return list.ToArray();
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x000340A8 File Offset: 0x000322A8
		internal static string ServerNameFromServerLegacyDN(string serverLegacyDN)
		{
			int num = serverLegacyDN.LastIndexOf("cn=", StringComparison.OrdinalIgnoreCase);
			if (num < 0 || num + 3 >= serverLegacyDN.Length)
			{
				return string.Empty;
			}
			int num2 = num + 3;
			StringBuilder stringBuilder = new StringBuilder(serverLegacyDN.Length - num2);
			for (int i = num2; i < serverLegacyDN.Length; i++)
			{
				stringBuilder.Append(char.ToLowerInvariant(serverLegacyDN[i]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00034114 File Offset: 0x00032314
		internal static int LastIndexOfUnescapedChar(string input, int startIndex, int length, char ch)
		{
			int num = -1;
			bool flag = false;
			for (int i = startIndex; i > startIndex - length; i--)
			{
				if (input[i] == '\\')
				{
					flag = !flag;
				}
				else
				{
					if (num != -1)
					{
						if (!flag)
						{
							return num;
						}
						num = -1;
					}
					if (input[i] == ch)
					{
						num = i;
					}
					flag = false;
				}
			}
			if (num == -1 || flag)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0003416A File Offset: 0x0003236A
		public static int LastIndexOfUnescapedChar(string input, int startIndex, char ch)
		{
			return DNConvertor.LastIndexOfUnescapedChar(input, startIndex, startIndex + 1, ch);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00034178 File Offset: 0x00032378
		internal static int IndexOfUnescapedChar(string input, int startIndex, int length, char ch)
		{
			bool flag = false;
			for (int i = startIndex; i < startIndex + length; i++)
			{
				if (input[i] == '\\')
				{
					flag = !flag;
				}
				else
				{
					if (input[i] == ch && !flag)
					{
						return i;
					}
					flag = false;
				}
			}
			return -1;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000341B9 File Offset: 0x000323B9
		public static int IndexOfUnescapedChar(string input, int startIndex, char ch)
		{
			return DNConvertor.IndexOfUnescapedChar(input, startIndex, input.Length - startIndex, ch);
		}
	}
}

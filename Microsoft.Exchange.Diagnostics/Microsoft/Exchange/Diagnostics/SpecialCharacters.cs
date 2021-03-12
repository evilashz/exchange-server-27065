using System;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001A3 RID: 419
	internal static class SpecialCharacters
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x0002A498 File Offset: 0x00028698
		public static bool IsValidKey(string key)
		{
			foreach (char ch in key)
			{
				if (!SpecialCharacters.IsValidKeyChar(ch))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002A4CF File Offset: 0x000286CF
		public static bool IsValidKeyChar(char ch)
		{
			return char.IsLetterOrDigit(ch) || SpecialCharacters.allowedSpecialCharacters.Contains(ch);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002A4EC File Offset: 0x000286EC
		public static string SanitizeForLogging(string key)
		{
			StringBuilder stringBuilder = new StringBuilder(string.Empty, key.Length);
			foreach (char c in key)
			{
				if (!char.IsLetterOrDigit(c) && !SpecialCharacters.allowedSpecialCharacters.Contains(c))
				{
					stringBuilder.Append('-');
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000875 RID: 2165
		public static readonly byte ColonByte = 58;

		// Token: 0x04000876 RID: 2166
		public static readonly byte EqualsByte = 61;

		// Token: 0x04000877 RID: 2167
		private static char[] allowedSpecialCharacters = new char[]
		{
			'.',
			'-',
			'[',
			']'
		};
	}
}

using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200004A RID: 74
	internal static class ADObjectNameHelper
	{
		// Token: 0x060003BC RID: 956 RVA: 0x00015A64 File Offset: 0x00013C64
		internal static bool CheckIsUnicodeStringWellFormed(string chars, out int position)
		{
			for (int i = 0; i < chars.Length; i++)
			{
				char c = chars[i];
				position = i;
				if (c == '￾' || c == '￿')
				{
					return false;
				}
				if (char.IsHighSurrogate(c))
				{
					if (i + 1 >= chars.Length || !char.IsLowSurrogate(chars[i + 1]))
					{
						return false;
					}
					i++;
				}
				else if (char.IsLowSurrogate(c))
				{
					return false;
				}
			}
			position = -1;
			return true;
		}

		// Token: 0x0400013F RID: 319
		private static string reservedStringPattern = "^[^\\x0a\\x00]{1,64}\\x0a(CNF|DEL):([0-9a-f]){8}-(([0-9a-f]){4}-){3}([0-9a-f]){12}$";

		// Token: 0x04000140 RID: 320
		public static Regex ReservedADNameStringRegex = new Regex(ADObjectNameHelper.reservedStringPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
	}
}

using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000030 RID: 48
	internal static class StringNormalizer
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x000062B8 File Offset: 0x000044B8
		public static string NormalizeEndOfLines(string original)
		{
			foreach (string oldValue in StringNormalizer.EndOfLinesOtherThanLf)
			{
				original = original.Replace(oldValue, '\n'.ToString());
			}
			return original;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000062F4 File Offset: 0x000044F4
		public static string TrimTrailingEndOfLines(string original)
		{
			return original.TrimEnd(new char[]
			{
				'\r',
				'\n',
				'\u0085',
				'\u2028'
			});
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006329 File Offset: 0x00004529
		public static string TrimTrailingAndNormalizeEol(string original)
		{
			return StringNormalizer.NormalizeEndOfLines(StringNormalizer.TrimTrailingEndOfLines(original));
		}

		// Token: 0x0400009C RID: 156
		private const char Cr = '\r';

		// Token: 0x0400009D RID: 157
		private const char Lf = '\n';

		// Token: 0x0400009E RID: 158
		private const char Nel = '\u0085';

		// Token: 0x0400009F RID: 159
		private const char LSep = '\u2028';

		// Token: 0x040000A0 RID: 160
		private const string CrStr = "\r";

		// Token: 0x040000A1 RID: 161
		private const string LfStr = "\n";

		// Token: 0x040000A2 RID: 162
		private const string NelStr = "\u0085";

		// Token: 0x040000A3 RID: 163
		private const string Crlf = "\r\n";

		// Token: 0x040000A4 RID: 164
		private const string CrNel = "\r\u0085";

		// Token: 0x040000A5 RID: 165
		private static readonly string[] EndOfLinesOtherThanLf = new string[]
		{
			"\r\n",
			"\r\u0085",
			'\u0085'.ToString(),
			'\u2028'.ToString(),
			'\r'.ToString()
		};
	}
}

using System;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x02000044 RID: 68
	internal sealed class RegexCharacterClassRuntime
	{
		// Token: 0x060001CE RID: 462 RVA: 0x00007CF3 File Offset: 0x00005EF3
		private RegexCharacterClassRuntime()
		{
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007CFB File Offset: 0x00005EFB
		public static bool IsSpace(int ch)
		{
			return char.IsWhiteSpace((char)ch);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007D04 File Offset: 0x00005F04
		public static bool IsNonSpace(int ch)
		{
			return !RegexCharacterClassRuntime.IsEOF(ch) && !char.IsWhiteSpace((char)ch);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00007D1A File Offset: 0x00005F1A
		public static bool IsDigit(int ch)
		{
			return char.IsDigit((char)ch);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007D23 File Offset: 0x00005F23
		public static bool IsNonDigit(int ch)
		{
			return !RegexCharacterClassRuntime.IsEOF(ch) && !char.IsDigit((char)ch);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00007D39 File Offset: 0x00005F39
		public static bool IsWord(int ch)
		{
			return char.IsLetterOrDigit((char)ch);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007D42 File Offset: 0x00005F42
		public static bool IsNonWord(int ch)
		{
			return !RegexCharacterClassRuntime.IsWord(ch);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007D4D File Offset: 0x00005F4D
		public static bool IsBegin(int ch)
		{
			return ch == 1;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007D53 File Offset: 0x00005F53
		public static bool IsEnd(int ch)
		{
			return RegexCharacterClassRuntime.IsEOF(ch);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007D5B File Offset: 0x00005F5B
		private static bool IsEOF(int ch)
		{
			return ch == -1;
		}
	}
}

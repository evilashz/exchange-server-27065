using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000023 RID: 35
	internal static class RegexUtils
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00003994 File Offset: 0x00001B94
		public static string ConvertLegacyRegexToTpl(string legacyPattern)
		{
			string text = legacyPattern;
			foreach (string text2 in RegexUtils.escapableItems)
			{
				text = text.Replace(text2, RegexUtils.escapeChar + text2);
			}
			return text;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000039D4 File Offset: 0x00001BD4
		public static ShortList<string> ConvertLegacyRegexToTpl(ShortList<string> legacyPatterns)
		{
			ShortList<string> shortList = new ShortList<string>();
			foreach (string legacyPattern in legacyPatterns)
			{
				shortList.Add(RegexUtils.ConvertLegacyRegexToTpl(legacyPattern));
			}
			return shortList;
		}

		// Token: 0x04000030 RID: 48
		private static readonly string[] escapableItems = new string[]
		{
			"[",
			"]",
			".",
			"?",
			"+",
			"{",
			"}"
		};

		// Token: 0x04000031 RID: 49
		private static readonly char escapeChar = '\\';
	}
}

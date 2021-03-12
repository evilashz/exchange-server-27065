using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000057 RID: 87
	internal static class TextMatchingStrings
	{
		// Token: 0x06000288 RID: 648 RVA: 0x0000A9EC File Offset: 0x00008BEC
		static TextMatchingStrings()
		{
			TextMatchingStrings.stringIDs.Add(1233043159U, "RegexSyntaxForStar");
			TextMatchingStrings.stringIDs.Add(3196275601U, "RegexMismatchingParenthesis");
			TextMatchingStrings.stringIDs.Add(3281782444U, "RegexUnSupportedMetaCharacter");
			TextMatchingStrings.stringIDs.Add(1519177102U, "KeywordInternalParsingError");
			TextMatchingStrings.stringIDs.Add(1668465210U, "RegexInternalParsingError");
			TextMatchingStrings.stringIDs.Add(688185054U, "RegexSyntaxForBar");
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000AA9F File Offset: 0x00008C9F
		public static LocalizedString RegexSyntaxForStar
		{
			get
			{
				return new LocalizedString("RegexSyntaxForStar", "", false, false, TextMatchingStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		public static LocalizedString RegexPatternParsingError(string diagnostic)
		{
			return new LocalizedString("RegexPatternParsingError", "", false, false, TextMatchingStrings.ResourceManager, new object[]
			{
				diagnostic
			});
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000AAEF File Offset: 0x00008CEF
		public static LocalizedString RegexMismatchingParenthesis
		{
			get
			{
				return new LocalizedString("RegexMismatchingParenthesis", "", false, false, TextMatchingStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000AB0D File Offset: 0x00008D0D
		public static LocalizedString RegexUnSupportedMetaCharacter
		{
			get
			{
				return new LocalizedString("RegexUnSupportedMetaCharacter", "", false, false, TextMatchingStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000AB2B File Offset: 0x00008D2B
		public static LocalizedString KeywordInternalParsingError
		{
			get
			{
				return new LocalizedString("KeywordInternalParsingError", "", false, false, TextMatchingStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000AB49 File Offset: 0x00008D49
		public static LocalizedString RegexInternalParsingError
		{
			get
			{
				return new LocalizedString("RegexInternalParsingError", "", false, false, TextMatchingStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000AB67 File Offset: 0x00008D67
		public static LocalizedString RegexSyntaxForBar
		{
			get
			{
				return new LocalizedString("RegexSyntaxForBar", "", false, false, TextMatchingStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000AB85 File Offset: 0x00008D85
		public static LocalizedString GetLocalizedString(TextMatchingStrings.IDs key)
		{
			return new LocalizedString(TextMatchingStrings.stringIDs[(uint)key], TextMatchingStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000143 RID: 323
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(6);

		// Token: 0x04000144 RID: 324
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MessagingPolicies.Rules.TextMatchingStrings", typeof(TextMatchingStrings).GetTypeInfo().Assembly);

		// Token: 0x02000058 RID: 88
		public enum IDs : uint
		{
			// Token: 0x04000146 RID: 326
			RegexSyntaxForStar = 1233043159U,
			// Token: 0x04000147 RID: 327
			RegexMismatchingParenthesis = 3196275601U,
			// Token: 0x04000148 RID: 328
			RegexUnSupportedMetaCharacter = 3281782444U,
			// Token: 0x04000149 RID: 329
			KeywordInternalParsingError = 1519177102U,
			// Token: 0x0400014A RID: 330
			RegexInternalParsingError = 1668465210U,
			// Token: 0x0400014B RID: 331
			RegexSyntaxForBar = 688185054U
		}

		// Token: 0x02000059 RID: 89
		private enum ParamIDs
		{
			// Token: 0x0400014D RID: 333
			RegexPatternParsingError
		}
	}
}

using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002BA RID: 698
	[ImmutableObject(true)]
	[Serializable]
	internal static class Wildcard
	{
		// Token: 0x0600191A RID: 6426 RVA: 0x0004F0B8 File Offset: 0x0004D2B8
		public static string ConvertToRegexPattern(string wildcardString)
		{
			if (string.IsNullOrEmpty(wildcardString))
			{
				throw new ArgumentNullException("wildcardString");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("^");
			stringBuilder.Append(Regex.Escape(wildcardString));
			stringBuilder.Replace(Wildcard.escapedAnyCharacterWildcardString, ".*");
			stringBuilder.Replace(Wildcard.escapedOneCharacterWildcardString, ".");
			stringBuilder.Append("$");
			return stringBuilder.ToString();
		}

		// Token: 0x04000ECD RID: 3789
		public const char AnyCharacterWildcard = '*';

		// Token: 0x04000ECE RID: 3790
		public const string AnyCharacterWildcardString = "*";

		// Token: 0x04000ECF RID: 3791
		public const char OneCharacterWildcard = '?';

		// Token: 0x04000ED0 RID: 3792
		public const string OneCharacterWildcardString = "?";

		// Token: 0x04000ED1 RID: 3793
		private static readonly string escapedAnyCharacterWildcardString = Regex.Escape("*");

		// Token: 0x04000ED2 RID: 3794
		private static readonly string escapedOneCharacterWildcardString = Regex.Escape("?");
	}
}

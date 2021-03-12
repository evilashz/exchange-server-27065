using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000040 RID: 64
	internal class RegexTermMatch : IMatch
	{
		// Token: 0x06000212 RID: 530 RVA: 0x0000E798 File Offset: 0x0000C998
		internal RegexTermMatch(IEnumerable<string> terms)
		{
			if (this.IsEmpty(terms))
			{
				throw new ArgumentException(Strings.EmptyTermSet);
			}
			if (terms.Any(new Func<string, bool>(string.IsNullOrEmpty)))
			{
				throw new ArgumentException(Strings.InvalidTerm);
			}
			this.regex = new Regex(this.GetPattern(terms), RegexOptions.ExplicitCapture);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000E7FB File Offset: 0x0000C9FB
		public bool IsMatch(TextScanContext data)
		{
			return this.regex.IsMatch(data.NormalizedData);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000E80E File Offset: 0x0000CA0E
		private bool IsEmpty(IEnumerable<string> collection)
		{
			return collection == null || !collection.GetEnumerator().MoveNext();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000E824 File Offset: 0x0000CA24
		private string GetPattern(IEnumerable<string> terms)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			stringBuilder.Append("(^|\\W)(");
			foreach (string text in terms)
			{
				if (!flag)
				{
					stringBuilder.Append("|");
				}
				foreach (char c in text)
				{
					if (".$^{}[](|)*+?\\".Contains(c))
					{
						stringBuilder.Append('\\');
						stringBuilder.Append(c);
					}
					else
					{
						stringBuilder.Append(char.ToUpperInvariant(c));
					}
				}
				flag = false;
			}
			stringBuilder.Append(")(\\W|$)");
			return stringBuilder.ToString();
		}

		// Token: 0x04000152 RID: 338
		private const string SpecialCharacterString = ".$^{}[](|)*+?\\";

		// Token: 0x04000153 RID: 339
		private const string WordBoundaryRegexFragment = "\\b";

		// Token: 0x04000154 RID: 340
		private const string AlternationFragment = "|";

		// Token: 0x04000155 RID: 341
		private const char EscapeCharacter = '\\';

		// Token: 0x04000156 RID: 342
		private const string PatternPrefix = "(^|\\W)(";

		// Token: 0x04000157 RID: 343
		private const string PatternSuffix = ")(\\W|$)";

		// Token: 0x04000158 RID: 344
		private Regex regex;
	}
}

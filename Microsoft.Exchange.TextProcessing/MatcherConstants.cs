using System;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200003A RID: 58
	internal struct MatcherConstants
	{
		// Token: 0x04000137 RID: 311
		public const int MatchedTermsDictionaryInitialCapacity = 256;

		// Token: 0x04000138 RID: 312
		public const int TrieIDDictionaryInitialCapacity = 8;

		// Token: 0x04000139 RID: 313
		public static readonly double MinimumCoefficient = 0.5;

		// Token: 0x0400013A RID: 314
		public static readonly double MaximumCoefficient = 1.0;

		// Token: 0x0400013B RID: 315
		public static readonly TimeSpan DefaultRegexMatchTimeout = Regex.InfiniteMatchTimeout;
	}
}

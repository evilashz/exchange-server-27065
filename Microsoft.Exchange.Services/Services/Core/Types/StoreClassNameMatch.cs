using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000892 RID: 2194
	internal class StoreClassNameMatch
	{
		// Token: 0x06003EC1 RID: 16065 RVA: 0x000D987C File Offset: 0x000D7A7C
		public static StoreClassNameMatch CreatePrefixMatch(string prefix)
		{
			return StoreClassNameMatch.CreateAndInitialize(StoreClassNameMatchType.Prefix, prefix, null);
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x000D9886 File Offset: 0x000D7A86
		public static StoreClassNameMatch CreatePrefixSuffixMatch(string prefix, string suffix)
		{
			return StoreClassNameMatch.CreateAndInitialize(StoreClassNameMatchType.PrefixSuffix, prefix, suffix);
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x000D9890 File Offset: 0x000D7A90
		private static StoreClassNameMatch CreateAndInitialize(StoreClassNameMatchType matchType, string prefix, string suffix)
		{
			return new StoreClassNameMatch
			{
				matchType = matchType,
				prefix = prefix,
				suffix = suffix
			};
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x000D98BC File Offset: 0x000D7ABC
		public bool Match(string stringToTest)
		{
			switch (this.matchType)
			{
			case StoreClassNameMatchType.Prefix:
				return stringToTest.StartsWith(this.prefix, StringComparison.OrdinalIgnoreCase);
			case StoreClassNameMatchType.PrefixSuffix:
				return stringToTest.StartsWith(this.prefix, StringComparison.OrdinalIgnoreCase) && stringToTest.EndsWith(this.suffix, StringComparison.OrdinalIgnoreCase);
			default:
				return false;
			}
		}

		// Token: 0x04002403 RID: 9219
		private string prefix;

		// Token: 0x04002404 RID: 9220
		private string suffix;

		// Token: 0x04002405 RID: 9221
		private StoreClassNameMatchType matchType;
	}
}

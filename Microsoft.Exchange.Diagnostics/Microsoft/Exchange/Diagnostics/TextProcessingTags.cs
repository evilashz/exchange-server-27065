using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002CB RID: 715
	public struct TextProcessingTags
	{
		// Token: 0x0400131E RID: 4894
		public const int SmartTrie = 0;

		// Token: 0x0400131F RID: 4895
		public const int Matcher = 1;

		// Token: 0x04001320 RID: 4896
		public const int Fingerprint = 2;

		// Token: 0x04001321 RID: 4897
		public const int Boomerang = 3;

		// Token: 0x04001322 RID: 4898
		public static Guid guid = new Guid("B15C3C00-9FF8-47B7-A975-70F1278017EF");
	}
}

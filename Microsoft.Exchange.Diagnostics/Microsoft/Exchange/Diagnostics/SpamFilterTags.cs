using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002C9 RID: 713
	public struct SpamFilterTags
	{
		// Token: 0x04001310 RID: 4880
		public const int Agent = 0;

		// Token: 0x04001311 RID: 4881
		public const int BlockSenders = 1;

		// Token: 0x04001312 RID: 4882
		public const int SafeSenders = 2;

		// Token: 0x04001313 RID: 4883
		public const int BypassCheck = 3;

		// Token: 0x04001314 RID: 4884
		public static Guid guid = new Guid("175562D6-54D7-4C59-A421-598E03755639");
	}
}

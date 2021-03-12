using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002EF RID: 751
	public struct ForefrontQuarantineTags
	{
		// Token: 0x04001405 RID: 5125
		public const int Agent = 0;

		// Token: 0x04001406 RID: 5126
		public const int Store = 1;

		// Token: 0x04001407 RID: 5127
		public const int Manager = 2;

		// Token: 0x04001408 RID: 5128
		public const int Cleanup = 3;

		// Token: 0x04001409 RID: 5129
		public const int SpamDigestWS = 4;

		// Token: 0x0400140A RID: 5130
		public const int SpamDigestCommon = 5;

		// Token: 0x0400140B RID: 5131
		public const int SpamDigestGenerator = 6;

		// Token: 0x0400140C RID: 5132
		public const int SpamDigestBackgroundJob = 7;

		// Token: 0x0400140D RID: 5133
		public const int Common = 8;

		// Token: 0x0400140E RID: 5134
		public static Guid guid = new Guid("10B884FD-372F-490D-A233-7C2C4CB8F104");
	}
}

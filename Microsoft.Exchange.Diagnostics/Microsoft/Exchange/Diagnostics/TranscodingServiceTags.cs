using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200028D RID: 653
	public struct TranscodingServiceTags
	{
		// Token: 0x0400116E RID: 4462
		public const int tagTranscoder = 0;

		// Token: 0x0400116F RID: 4463
		public const int tagConverter = 1;

		// Token: 0x04001170 RID: 4464
		public const int tagRedirectIO = 2;

		// Token: 0x04001171 RID: 4465
		public const int tagFunction = 3;

		// Token: 0x04001172 RID: 4466
		public const int tagProgramFlow = 4;

		// Token: 0x04001173 RID: 4467
		public static Guid guid = new Guid("2DEAC164-DDB1-4A89-9110-8258F5018258");
	}
}

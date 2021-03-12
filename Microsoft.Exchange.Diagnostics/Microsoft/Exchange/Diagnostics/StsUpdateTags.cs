using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200028C RID: 652
	public struct StsUpdateTags
	{
		// Token: 0x04001168 RID: 4456
		public const int Factory = 0;

		// Token: 0x04001169 RID: 4457
		public const int Database = 1;

		// Token: 0x0400116A RID: 4458
		public const int Agent = 2;

		// Token: 0x0400116B RID: 4459
		public const int OnDownload = 3;

		// Token: 0x0400116C RID: 4460
		public const int OnRequest = 4;

		// Token: 0x0400116D RID: 4461
		public static Guid guid = new Guid("C5F72F2A-EF44-4286-9AB2-14D106DFB8F1");
	}
}

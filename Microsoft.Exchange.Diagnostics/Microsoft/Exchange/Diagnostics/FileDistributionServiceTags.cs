using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200023F RID: 575
	public struct FileDistributionServiceTags
	{
		// Token: 0x04000F03 RID: 3843
		public const int CustomCommand = 0;

		// Token: 0x04000F04 RID: 3844
		public const int FileReplication = 1;

		// Token: 0x04000F05 RID: 3845
		public const int ADRequests = 2;

		// Token: 0x04000F06 RID: 3846
		public const int FaultInjection = 3;

		// Token: 0x04000F07 RID: 3847
		public static Guid guid = new Guid("0f0a52f9-4d72-460d-9928-1da8215066d4");
	}
}

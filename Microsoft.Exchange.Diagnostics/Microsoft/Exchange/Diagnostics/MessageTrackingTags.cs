using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002A5 RID: 677
	public struct MessageTrackingTags
	{
		// Token: 0x0400125C RID: 4700
		public const int Task = 0;

		// Token: 0x0400125D RID: 4701
		public const int ServerStatus = 1;

		// Token: 0x0400125E RID: 4702
		public const int LogAnalysis = 2;

		// Token: 0x0400125F RID: 4703
		public const int SearchLibrary = 3;

		// Token: 0x04001260 RID: 4704
		public const int WebService = 4;

		// Token: 0x04001261 RID: 4705
		public static Guid guid = new Guid("0B7BA732-EF67-4e7c-A68F-3D8593D9DC06");
	}
}

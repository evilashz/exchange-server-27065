using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002BE RID: 702
	public struct BackSyncTags
	{
		// Token: 0x040012DC RID: 4828
		public const int BackSync = 0;

		// Token: 0x040012DD RID: 4829
		public const int ActiveDirectory = 1;

		// Token: 0x040012DE RID: 4830
		public const int TenantFullSync = 2;

		// Token: 0x040012DF RID: 4831
		public const int Merge = 3;

		// Token: 0x040012E0 RID: 4832
		public static Guid guid = new Guid("3C237538-546C-4659-AED9-F445236DFB91");
	}
}

using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200021A RID: 538
	public struct EdgeSyncTags
	{
		// Token: 0x04000C04 RID: 3076
		public const int Process = 0;

		// Token: 0x04000C05 RID: 3077
		public const int Connection = 1;

		// Token: 0x04000C06 RID: 3078
		public const int Scheduler = 2;

		// Token: 0x04000C07 RID: 3079
		public const int SyncNow = 3;

		// Token: 0x04000C08 RID: 3080
		public const int Topology = 4;

		// Token: 0x04000C09 RID: 3081
		public const int SynchronizationJob = 5;

		// Token: 0x04000C0A RID: 3082
		public const int Subscription = 6;

		// Token: 0x04000C0B RID: 3083
		public static Guid guid = new Guid("AB9C28FE-50E0-4907-BB41-8F82D8E0C068");
	}
}

using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002A1 RID: 673
	public struct ManagedStore_StoreIntegrityCheckTags
	{
		// Token: 0x04001244 RID: 4676
		public const int StartupShutdown = 0;

		// Token: 0x04001245 RID: 4677
		public const int OnlineIsinteg = 1;

		// Token: 0x04001246 RID: 4678
		public const int FaultInjection = 20;

		// Token: 0x04001247 RID: 4679
		public static Guid guid = new Guid("856DA9F3-E7F6-4565-84F6-71A96AF18D92");
	}
}

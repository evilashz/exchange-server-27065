using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000221 RID: 545
	public struct MExRuntimeTags
	{
		// Token: 0x04000C1E RID: 3102
		public const int Initialize = 0;

		// Token: 0x04000C1F RID: 3103
		public const int Dispatch = 1;

		// Token: 0x04000C20 RID: 3104
		public const int Shutdown = 2;

		// Token: 0x04000C21 RID: 3105
		public static Guid guid = new Guid("b7916055-456d-46f6-bdd2-42ac88ccb655");
	}
}

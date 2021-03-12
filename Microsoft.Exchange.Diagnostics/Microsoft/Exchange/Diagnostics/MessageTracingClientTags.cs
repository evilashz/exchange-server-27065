using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002CD RID: 717
	public struct MessageTracingClientTags
	{
		// Token: 0x04001327 RID: 4903
		public const int Parser = 0;

		// Token: 0x04001328 RID: 4904
		public const int Writer = 1;

		// Token: 0x04001329 RID: 4905
		public const int Reader = 2;

		// Token: 0x0400132A RID: 4906
		public const int LogMonitor = 3;

		// Token: 0x0400132B RID: 4907
		public const int General = 4;

		// Token: 0x0400132C RID: 4908
		public const int TransportQueue = 5;

		// Token: 0x0400132D RID: 4909
		public static Guid guid = new Guid("0402AB9A-3D53-4353-AC55-9A9491E5A22A");
	}
}

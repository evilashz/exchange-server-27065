using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002FE RID: 766
	public struct DirectoryCacheTags
	{
		// Token: 0x04001450 RID: 5200
		public const int Session = 0;

		// Token: 0x04001451 RID: 5201
		public const int CacheSession = 1;

		// Token: 0x04001452 RID: 5202
		public const int WCFServiceEndpoint = 2;

		// Token: 0x04001453 RID: 5203
		public const int WCFClientEndpoint = 3;

		// Token: 0x04001454 RID: 5204
		public static Guid guid = new Guid("2550C2A5-C4F4-4358-83E4-894A370B5A20");
	}
}

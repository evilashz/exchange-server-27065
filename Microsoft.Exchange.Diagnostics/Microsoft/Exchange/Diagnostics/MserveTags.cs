using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200021B RID: 539
	public struct MserveTags
	{
		// Token: 0x04000C0C RID: 3084
		public const int Provider = 0;

		// Token: 0x04000C0D RID: 3085
		public const int TargetConnection = 1;

		// Token: 0x04000C0E RID: 3086
		public const int Config = 2;

		// Token: 0x04000C0F RID: 3087
		public const int DeltaSyncAPI = 3;

		// Token: 0x04000C10 RID: 3088
		public const int MserveCacheService = 5;

		// Token: 0x04000C11 RID: 3089
		public static Guid guid = new Guid("86790e72-3e66-4b27-b3e1-66faaa21840f");
	}
}

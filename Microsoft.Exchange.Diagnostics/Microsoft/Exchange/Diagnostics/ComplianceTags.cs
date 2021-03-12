using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200030E RID: 782
	public struct ComplianceTags
	{
		// Token: 0x040014DC RID: 5340
		public const int General = 0;

		// Token: 0x040014DD RID: 5341
		public const int Configuration = 1;

		// Token: 0x040014DE RID: 5342
		public const int ViewProvider = 2;

		// Token: 0x040014DF RID: 5343
		public const int DataProvider = 3;

		// Token: 0x040014E0 RID: 5344
		public const int View = 4;

		// Token: 0x040014E1 RID: 5345
		public const int FaultInjection = 5;

		// Token: 0x040014E2 RID: 5346
		public const int ComplianceService = 6;

		// Token: 0x040014E3 RID: 5347
		public const int TaskDistributionSystem = 7;

		// Token: 0x040014E4 RID: 5348
		public static Guid guid = new Guid("3719A9EF-E0BD-45DF-9B58-B36C0C2ECF0E");
	}
}

using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000215 RID: 533
	public struct MSExchangeAutodiscoverTags
	{
		// Token: 0x04000B8F RID: 2959
		public const int Framework = 0;

		// Token: 0x04000B90 RID: 2960
		public const int OutlookProvider = 1;

		// Token: 0x04000B91 RID: 2961
		public const int MobileSyncProvider = 2;

		// Token: 0x04000B92 RID: 2962
		public const int FaultInjection = 3;

		// Token: 0x04000B93 RID: 2963
		public const int AuthMetadata = 4;

		// Token: 0x04000B94 RID: 2964
		public static Guid guid = new Guid("B3E33516-3A9E-4fba-8469-A88ECCCCDCD1");
	}
}

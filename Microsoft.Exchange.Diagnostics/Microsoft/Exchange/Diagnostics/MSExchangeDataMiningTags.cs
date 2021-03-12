using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002C3 RID: 707
	public struct MSExchangeDataMiningTags
	{
		// Token: 0x040012EB RID: 4843
		public const int Events = 0;

		// Token: 0x040012EC RID: 4844
		public const int General = 1;

		// Token: 0x040012ED RID: 4845
		public const int Configuration = 2;

		// Token: 0x040012EE RID: 4846
		public const int ConfigurationService = 3;

		// Token: 0x040012EF RID: 4847
		public const int Scheduler = 4;

		// Token: 0x040012F0 RID: 4848
		public const int Pumper = 5;

		// Token: 0x040012F1 RID: 4849
		public const int Uploader = 6;

		// Token: 0x040012F2 RID: 4850
		public static Guid guid = new Guid("{54300D03-CEA2-43CB-9522-2F6B1CD5DAA4}");
	}
}

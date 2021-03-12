using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000282 RID: 642
	public struct ConnectionFilteringTags
	{
		// Token: 0x04001131 RID: 4401
		public const int Error = 0;

		// Token: 0x04001132 RID: 4402
		public const int Factory = 1;

		// Token: 0x04001133 RID: 4403
		public const int OnConnect = 2;

		// Token: 0x04001134 RID: 4404
		public const int OnMailFrom = 3;

		// Token: 0x04001135 RID: 4405
		public const int OnRcptTo = 4;

		// Token: 0x04001136 RID: 4406
		public const int OnEOH = 5;

		// Token: 0x04001137 RID: 4407
		public const int DNS = 6;

		// Token: 0x04001138 RID: 4408
		public const int IPAllowDeny = 7;

		// Token: 0x04001139 RID: 4409
		public static Guid guid = new Guid("F0A7EB4B-2EE5-478f-A589-5273CAC4E5EE");
	}
}

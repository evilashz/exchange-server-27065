using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002E3 RID: 739
	public struct FfoAuthorizationTags
	{
		// Token: 0x0400136C RID: 4972
		public const int FfoRunspace = 0;

		// Token: 0x0400136D RID: 4973
		public const int PartnerConfig = 1;

		// Token: 0x0400136E RID: 4974
		public const int FfoRps = 2;

		// Token: 0x0400136F RID: 4975
		public const int FfoRpsBudget = 3;

		// Token: 0x04001370 RID: 4976
		public const int FaultInjection = 4;

		// Token: 0x04001371 RID: 4977
		public const int FfoServicePlans = 5;

		// Token: 0x04001372 RID: 4978
		public static Guid guid = new Guid("2AEBD40A-8FA5-4159-A644-54F41B37D965");
	}
}

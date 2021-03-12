using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200026F RID: 623
	public struct LiveIdRedirectionTags
	{
		// Token: 0x04001072 RID: 4210
		public const int Redirection = 0;

		// Token: 0x04001073 RID: 4211
		public const int ErrorReporting = 1;

		// Token: 0x04001074 RID: 4212
		public const int FaultInjection = 2;

		// Token: 0x04001075 RID: 4213
		public const int TenantMonitoring = 3;

		// Token: 0x04001076 RID: 4214
		public static Guid guid = new Guid("62a46310-1793-40b2-9f61-74bf8637fce6");
	}
}

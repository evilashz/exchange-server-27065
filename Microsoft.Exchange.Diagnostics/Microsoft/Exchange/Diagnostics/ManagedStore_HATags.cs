using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000293 RID: 659
	public struct ManagedStore_HATags
	{
		// Token: 0x0400118B RID: 4491
		public const int Eseback = 0;

		// Token: 0x0400118C RID: 4492
		public const int LogReplayStatus = 1;

		// Token: 0x0400118D RID: 4493
		public const int BlockModeCollector = 2;

		// Token: 0x0400118E RID: 4494
		public const int BlockModeMessageStream = 3;

		// Token: 0x0400118F RID: 4495
		public const int BlockModeSender = 4;

		// Token: 0x04001190 RID: 4496
		public const int JetHADatabase = 5;

		// Token: 0x04001191 RID: 4497
		public const int LastLogWriter = 6;

		// Token: 0x04001192 RID: 4498
		public const int FaultInjection = 20;

		// Token: 0x04001193 RID: 4499
		public static Guid guid = new Guid("0081576A-CB7C-4aba-9BB4-7D0B44290C2C");
	}
}

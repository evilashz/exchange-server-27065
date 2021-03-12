using System;

namespace Microsoft.Exchange.Management.Deployment.PrereqAnalysisSample
{
	// Token: 0x02000079 RID: 121
	[Flags]
	public enum SetupRole
	{
		// Token: 0x040005D0 RID: 1488
		None = 0,
		// Token: 0x040005D1 RID: 1489
		AdminTools = 1,
		// Token: 0x040005D2 RID: 1490
		Mailbox = 2,
		// Token: 0x040005D3 RID: 1491
		Bridgehead = 4,
		// Token: 0x040005D4 RID: 1492
		ClientAccess = 8,
		// Token: 0x040005D5 RID: 1493
		UnifiedMessaging = 16,
		// Token: 0x040005D6 RID: 1494
		Gateway = 32,
		// Token: 0x040005D7 RID: 1495
		Cafe = 64,
		// Token: 0x040005D8 RID: 1496
		Global = 128,
		// Token: 0x040005D9 RID: 1497
		LanguagePacks = 256,
		// Token: 0x040005DA RID: 1498
		UmLanguagePack = 512,
		// Token: 0x040005DB RID: 1499
		FrontendTransport = 1024,
		// Token: 0x040005DC RID: 1500
		All = 2047
	}
}

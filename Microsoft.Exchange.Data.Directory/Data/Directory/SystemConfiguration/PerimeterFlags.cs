using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000539 RID: 1337
	[Flags]
	internal enum PerimeterFlags
	{
		// Token: 0x04002898 RID: 10392
		Empty = 0,
		// Token: 0x04002899 RID: 10393
		SyncToHotmailEnabled = 1,
		// Token: 0x0400289A RID: 10394
		RouteOutboundViaEhfEnabled = 2,
		// Token: 0x0400289B RID: 10395
		IPSkiplistingEnabled = 4,
		// Token: 0x0400289C RID: 10396
		[Obsolete("This flag is deprecated. Use SafelistingUIMode flag instead.")]
		LinkToEhfEnabled = 8,
		// Token: 0x0400289D RID: 10397
		EhfConfigSyncDisabled = 16,
		// Token: 0x0400289E RID: 10398
		EhfAdminAccountSyncDisabled = 32,
		// Token: 0x0400289F RID: 10399
		IPSafelistingSyncEnabled = 64,
		// Token: 0x040028A0 RID: 10400
		RouteOutboundViaFfoFrontendEnabled = 512,
		// Token: 0x040028A1 RID: 10401
		MigrationInProgress = 1024,
		// Token: 0x040028A2 RID: 10402
		EheEnabled = 2048,
		// Token: 0x040028A3 RID: 10403
		RMSOFwdSyncDisabled = 4096,
		// Token: 0x040028A4 RID: 10404
		EheDecryptEnabled = 8192,
		// Token: 0x040028A5 RID: 10405
		All = 15988
	}
}

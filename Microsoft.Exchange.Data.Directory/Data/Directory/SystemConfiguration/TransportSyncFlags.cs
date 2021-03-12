using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000582 RID: 1410
	[Flags]
	internal enum TransportSyncFlags
	{
		// Token: 0x04002C84 RID: 11396
		None = 0,
		// Token: 0x04002C85 RID: 11397
		TransportSyncEnabled = 1,
		// Token: 0x04002C86 RID: 11398
		TransportSyncHubHealthLogEnabled = 2,
		// Token: 0x04002C87 RID: 11399
		TransportSyncPopEnabled = 4,
		// Token: 0x04002C88 RID: 11400
		WindowsLiveHotmailTransportSyncEnabled = 8,
		// Token: 0x04002C89 RID: 11401
		TransportSyncExchangeEnabled = 32,
		// Token: 0x04002C8A RID: 11402
		TransportSyncImapEnabled = 64,
		// Token: 0x04002C8B RID: 11403
		HttpProtocolLogEnabled = 128,
		// Token: 0x04002C8C RID: 11404
		TransportSyncLogEnabled = 256,
		// Token: 0x04002C8D RID: 11405
		TransportSyncAccountsPoisonDetectionEnabled = 1024,
		// Token: 0x04002C8E RID: 11406
		TransportSyncDispatchEnabled = 512,
		// Token: 0x04002C8F RID: 11407
		TransportSyncMailboxLogEnabled = 2048,
		// Token: 0x04002C90 RID: 11408
		TransportSyncMailboxHealthLogEnabled = 4096,
		// Token: 0x04002C91 RID: 11409
		TransportSyncFacebookEnabled = 8192,
		// Token: 0x04002C92 RID: 11410
		TransportSyncLinkedInEnabled = 16384,
		// Token: 0x04002C93 RID: 11411
		All = 32751
	}
}

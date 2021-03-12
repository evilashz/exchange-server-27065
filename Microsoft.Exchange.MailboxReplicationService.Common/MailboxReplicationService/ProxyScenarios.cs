using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000D7 RID: 215
	internal enum ProxyScenarios
	{
		// Token: 0x040004E4 RID: 1252
		None,
		// Token: 0x040004E5 RID: 1253
		LocalMdbAndProxy,
		// Token: 0x040004E6 RID: 1254
		RemoteMdbAndProxy = 16,
		// Token: 0x040004E7 RID: 1255
		LocalProxyRemoteMdb = 32
	}
}

using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005BA RID: 1466
	[Flags]
	internal enum SmtpSendFlags
	{
		// Token: 0x04002DD2 RID: 11730
		UseExternalDNSServers = 2,
		// Token: 0x04002DD3 RID: 11731
		DomainSecureEnabled = 4,
		// Token: 0x04002DD4 RID: 11732
		RequireOorg = 8,
		// Token: 0x04002DD5 RID: 11733
		TransportRuleScoped = 4096,
		// Token: 0x04002DD6 RID: 11734
		FrontendProxyEnabled = 8192,
		// Token: 0x04002DD7 RID: 11735
		EdgeSynced = 65536,
		// Token: 0x04002DD8 RID: 11736
		CloudServicesMailEnabled = 131072
	}
}

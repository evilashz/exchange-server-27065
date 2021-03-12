using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000186 RID: 390
	[Flags]
	internal enum ProxyControlFlags
	{
		// Token: 0x0400082F RID: 2095
		None = 0,
		// Token: 0x04000830 RID: 2096
		DoNotApplyProxyThrottling = 1,
		// Token: 0x04000831 RID: 2097
		DoNotCompress = 2,
		// Token: 0x04000832 RID: 2098
		DoNotBuffer = 4,
		// Token: 0x04000833 RID: 2099
		StripLargeRulesForDownlevelTargets = 8,
		// Token: 0x04000834 RID: 2100
		DoNotAddIdentifyingCafeHeaders = 16,
		// Token: 0x04000835 RID: 2101
		SkipWLMThrottling = 32,
		// Token: 0x04000836 RID: 2102
		ResolveServerName = 64,
		// Token: 0x04000837 RID: 2103
		UseTcp = 128,
		// Token: 0x04000838 RID: 2104
		UseCertificateToAuthenticate = 256,
		// Token: 0x04000839 RID: 2105
		Olc = 512
	}
}

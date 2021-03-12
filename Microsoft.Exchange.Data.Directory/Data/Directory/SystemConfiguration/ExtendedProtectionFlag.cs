using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000466 RID: 1126
	[Flags]
	public enum ExtendedProtectionFlag
	{
		// Token: 0x04002277 RID: 8823
		None = 0,
		// Token: 0x04002278 RID: 8824
		Proxy = 1,
		// Token: 0x04002279 RID: 8825
		NoServiceNameCheck = 2,
		// Token: 0x0400227A RID: 8826
		AllowDotlessSpn = 4,
		// Token: 0x0400227B RID: 8827
		ProxyCohosting = 32
	}
}

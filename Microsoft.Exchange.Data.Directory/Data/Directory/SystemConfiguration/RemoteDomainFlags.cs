using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003FE RID: 1022
	[Flags]
	internal enum RemoteDomainFlags
	{
		// Token: 0x04001F4A RID: 8010
		None = 0,
		// Token: 0x04001F4B RID: 8011
		TrustedMailOutboundEnabled = 1,
		// Token: 0x04001F4C RID: 8012
		TrustedMailInboundEnabled = 2
	}
}

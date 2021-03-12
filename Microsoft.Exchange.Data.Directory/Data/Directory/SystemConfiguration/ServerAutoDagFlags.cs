using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000587 RID: 1415
	[Flags]
	internal enum ServerAutoDagFlags
	{
		// Token: 0x04002CC1 RID: 11457
		None = 0,
		// Token: 0x04002CC2 RID: 11458
		DatabaseCopyLocationAgilityDisabled = 1,
		// Token: 0x04002CC3 RID: 11459
		ActivationDisabled = 2,
		// Token: 0x04002CC4 RID: 11460
		ServerConfigured = 4
	}
}

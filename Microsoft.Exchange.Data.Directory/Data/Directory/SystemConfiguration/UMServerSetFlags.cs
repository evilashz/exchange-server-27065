using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200058C RID: 1420
	[Flags]
	internal enum UMServerSetFlags
	{
		// Token: 0x04002CEB RID: 11499
		IPAddressFamilyConfigurable = 1,
		// Token: 0x04002CEC RID: 11500
		IPv4Enabled = 2,
		// Token: 0x04002CED RID: 11501
		IPv6Enabled = 4,
		// Token: 0x04002CEE RID: 11502
		Default = 7
	}
}

using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200060D RID: 1549
	public enum GatewayStatus
	{
		// Token: 0x040032D5 RID: 13013
		[LocDescription(DirectoryStrings.IDs.Enabled)]
		Enabled,
		// Token: 0x040032D6 RID: 13014
		[LocDescription(DirectoryStrings.IDs.Disabled)]
		Disabled,
		// Token: 0x040032D7 RID: 13015
		[LocDescription(DirectoryStrings.IDs.NoNewCalls)]
		NoNewCalls
	}
}

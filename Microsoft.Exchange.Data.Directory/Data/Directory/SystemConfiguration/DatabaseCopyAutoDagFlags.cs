using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003D8 RID: 984
	[Flags]
	internal enum DatabaseCopyAutoDagFlags
	{
		// Token: 0x04001E4E RID: 7758
		None = 0,
		// Token: 0x04001E4F RID: 7759
		BeingRelocated = 1,
		// Token: 0x04001E50 RID: 7760
		HostServerUnlinked = 2
	}
}

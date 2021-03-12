using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003D3 RID: 979
	[Flags]
	internal enum DatabaseAutoDagFlags
	{
		// Token: 0x04001E1C RID: 7708
		None = 0,
		// Token: 0x04001E1D RID: 7709
		ExcludeFromMonitoring = 1,
		// Token: 0x04001E1E RID: 7710
		ExcludeFromDatabaseCopyLocationAgility = 2
	}
}

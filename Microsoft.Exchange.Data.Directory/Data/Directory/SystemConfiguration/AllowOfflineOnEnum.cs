using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000538 RID: 1336
	public enum AllowOfflineOnEnum
	{
		// Token: 0x04002894 RID: 10388
		[LocDescription(DirectoryStrings.IDs.PrivateComputersOnly)]
		PrivateComputersOnly = 1,
		// Token: 0x04002895 RID: 10389
		[LocDescription(DirectoryStrings.IDs.NoComputers)]
		NoComputers,
		// Token: 0x04002896 RID: 10390
		[LocDescription(DirectoryStrings.IDs.AllComputers)]
		AllComputers
	}
}

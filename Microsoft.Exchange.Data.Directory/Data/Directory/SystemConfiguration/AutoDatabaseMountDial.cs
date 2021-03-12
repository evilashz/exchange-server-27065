using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000584 RID: 1412
	public enum AutoDatabaseMountDial
	{
		// Token: 0x04002CB0 RID: 11440
		[LocDescription(DirectoryStrings.IDs.AutoDatabaseMountDialLossless)]
		Lossless,
		// Token: 0x04002CB1 RID: 11441
		[LocDescription(DirectoryStrings.IDs.AutoDatabaseMountDialGoodAvailability)]
		GoodAvailability = 6,
		// Token: 0x04002CB2 RID: 11442
		[LocDescription(DirectoryStrings.IDs.AutoDatabaseMountDialBestAvailability)]
		BestAvailability = 12
	}
}

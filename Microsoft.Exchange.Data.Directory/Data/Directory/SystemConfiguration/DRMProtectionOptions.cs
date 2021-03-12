using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000611 RID: 1553
	public enum DRMProtectionOptions
	{
		// Token: 0x04003315 RID: 13077
		[LocDescription(DirectoryStrings.IDs.None)]
		None,
		// Token: 0x04003316 RID: 13078
		[LocDescription(DirectoryStrings.IDs.Private)]
		Private,
		// Token: 0x04003317 RID: 13079
		[LocDescription(DirectoryStrings.IDs.AllUsers)]
		All = -1
	}
}

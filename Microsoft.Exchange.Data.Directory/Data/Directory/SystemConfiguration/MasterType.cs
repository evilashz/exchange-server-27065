using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003D2 RID: 978
	public enum MasterType
	{
		// Token: 0x04001E18 RID: 7704
		[LocDescription(DirectoryStrings.IDs.DatabaseMasterTypeServer)]
		Server,
		// Token: 0x04001E19 RID: 7705
		[LocDescription(DirectoryStrings.IDs.DatabaseMasterTypeDag)]
		DatabaseAvailabilityGroup,
		// Token: 0x04001E1A RID: 7706
		[LocDescription(DirectoryStrings.IDs.DatabaseMasterTypeUnknown)]
		Unknown
	}
}

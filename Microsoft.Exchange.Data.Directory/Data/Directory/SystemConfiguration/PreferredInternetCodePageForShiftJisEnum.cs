using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003F9 RID: 1017
	public enum PreferredInternetCodePageForShiftJisEnum
	{
		// Token: 0x04001F26 RID: 7974
		[LocDescription(DirectoryStrings.IDs.PreferredInternetCodePageUndefined)]
		Undefined,
		// Token: 0x04001F27 RID: 7975
		[LocDescription(DirectoryStrings.IDs.PreferredInternetCodePageIso2022Jp)]
		Iso2022Jp = 50220,
		// Token: 0x04001F28 RID: 7976
		[LocDescription(DirectoryStrings.IDs.PreferredInternetCodePageEsc2022Jp)]
		Esc2022Jp,
		// Token: 0x04001F29 RID: 7977
		[LocDescription(DirectoryStrings.IDs.PreferredInternetCodePageSio2022Jp)]
		Sio2022Jp
	}
}

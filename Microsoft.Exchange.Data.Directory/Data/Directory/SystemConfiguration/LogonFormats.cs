using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200036E RID: 878
	public enum LogonFormats
	{
		// Token: 0x04001907 RID: 6407
		[LocDescription(DirectoryStrings.IDs.FullDomain)]
		FullDomain,
		// Token: 0x04001908 RID: 6408
		[LocDescription(DirectoryStrings.IDs.PrincipalName)]
		PrincipalName,
		// Token: 0x04001909 RID: 6409
		[LocDescription(DirectoryStrings.IDs.UserName)]
		UserName
	}
}

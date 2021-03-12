using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005FF RID: 1535
	public enum UMVoIPSecurityType
	{
		// Token: 0x0400328E RID: 12942
		[LocDescription(DirectoryStrings.IDs.SIPSecured)]
		SIPSecured = 1,
		// Token: 0x0400328F RID: 12943
		[LocDescription(DirectoryStrings.IDs.Unsecured)]
		Unsecured,
		// Token: 0x04003290 RID: 12944
		[LocDescription(DirectoryStrings.IDs.Secured)]
		Secured
	}
}

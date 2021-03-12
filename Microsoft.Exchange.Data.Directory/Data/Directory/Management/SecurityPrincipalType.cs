using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000709 RID: 1801
	public enum SecurityPrincipalType
	{
		// Token: 0x040038F3 RID: 14579
		[LocDescription(DirectoryStrings.IDs.SecurityPrincipalTypeNone)]
		None,
		// Token: 0x040038F4 RID: 14580
		[LocDescription(DirectoryStrings.IDs.SecurityPrincipalTypeUser)]
		User,
		// Token: 0x040038F5 RID: 14581
		[LocDescription(DirectoryStrings.IDs.SecurityPrincipalTypeGroup)]
		Group,
		// Token: 0x040038F6 RID: 14582
		[LocDescription(DirectoryStrings.IDs.SecurityPrincipalTypeComputer)]
		Computer,
		// Token: 0x040038F7 RID: 14583
		[LocDescription(DirectoryStrings.IDs.SecurityPrincipalTypeWellknownSecurityPrincipal)]
		WellknownSecurityPrincipal,
		// Token: 0x040038F8 RID: 14584
		[LocDescription(DirectoryStrings.IDs.SecurityPrincipalTypeUniversalSecurityGroup)]
		UniversalSecurityGroup,
		// Token: 0x040038F9 RID: 14585
		[LocDescription(DirectoryStrings.IDs.SecurityPrincipalTypeGlobalSecurityGroup)]
		GlobalSecurityGroup
	}
}

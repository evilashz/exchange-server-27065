using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000221 RID: 545
	[Flags]
	public enum GroupTypeFlags
	{
		// Token: 0x04000C7E RID: 3198
		[LocDescription(DirectoryStrings.IDs.GroupTypeFlagsNone)]
		None = 0,
		// Token: 0x04000C7F RID: 3199
		[LocDescription(DirectoryStrings.IDs.GroupTypeFlagsGlobal)]
		Global = 2,
		// Token: 0x04000C80 RID: 3200
		[LocDescription(DirectoryStrings.IDs.GroupTypeFlagsDomainLocal)]
		DomainLocal = 4,
		// Token: 0x04000C81 RID: 3201
		[LocDescription(DirectoryStrings.IDs.GroupTypeFlagsBuiltinLocal)]
		BuiltinLocal = 5,
		// Token: 0x04000C82 RID: 3202
		[LocDescription(DirectoryStrings.IDs.GroupTypeFlagsUniversal)]
		Universal = 8,
		// Token: 0x04000C83 RID: 3203
		[LocDescription(DirectoryStrings.IDs.GroupTypeFlagsSecurityEnabled)]
		SecurityEnabled = -2147483648
	}
}

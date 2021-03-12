using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000031 RID: 49
	internal enum CopyPropertiesFlags
	{
		// Token: 0x04000339 RID: 825
		None,
		// Token: 0x0400033A RID: 826
		Move,
		// Token: 0x0400033B RID: 827
		NoReplace,
		// Token: 0x0400033C RID: 828
		CopyMailboxPerUserData = 8,
		// Token: 0x0400033D RID: 829
		CopyFolderPerUserData = 16,
		// Token: 0x0400033E RID: 830
		StripLargeRulesForDownlevelTargets = 2048
	}
}

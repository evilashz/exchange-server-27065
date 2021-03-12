using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000015 RID: 21
	[Flags]
	internal enum EnumHierarchyFlags
	{
		// Token: 0x0400003A RID: 58
		None = 0,
		// Token: 0x0400003B RID: 59
		NormalFolders = 1,
		// Token: 0x0400003C RID: 60
		SearchFolders = 2,
		// Token: 0x0400003D RID: 61
		RootFolder = 4,
		// Token: 0x0400003E RID: 62
		AllFolders = 7
	}
}

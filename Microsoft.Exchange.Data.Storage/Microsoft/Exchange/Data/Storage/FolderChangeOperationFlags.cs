using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000284 RID: 644
	[Flags]
	internal enum FolderChangeOperationFlags
	{
		// Token: 0x040012D8 RID: 4824
		None = 0,
		// Token: 0x040012D9 RID: 4825
		IncludeAssociated = 1,
		// Token: 0x040012DA RID: 4826
		IncludeItems = 2,
		// Token: 0x040012DB RID: 4827
		IncludeSubFolders = 4,
		// Token: 0x040012DC RID: 4828
		IncludeAll = 7,
		// Token: 0x040012DD RID: 4829
		DeclineCalendarItemWithResponse = 16,
		// Token: 0x040012DE RID: 4830
		DeclineCalendarItemWithoutResponse = 32,
		// Token: 0x040012DF RID: 4831
		CancelCalendarItem = 64,
		// Token: 0x040012E0 RID: 4832
		EmptyFolder = 256,
		// Token: 0x040012E1 RID: 4833
		DeleteAllClutter = 512,
		// Token: 0x040012E2 RID: 4834
		ClutterActionByUserOverride = 1024
	}
}

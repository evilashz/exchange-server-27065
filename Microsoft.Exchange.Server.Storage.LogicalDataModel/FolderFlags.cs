using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200002E RID: 46
	[Flags]
	public enum FolderFlags
	{
		// Token: 0x040002C8 RID: 712
		Ipm = 1,
		// Token: 0x040002C9 RID: 713
		Search = 2,
		// Token: 0x040002CA RID: 714
		Normal = 4,
		// Token: 0x040002CB RID: 715
		Rules = 8
	}
}

using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000039 RID: 57
	[Flags]
	public enum FolderAdminFlags
	{
		// Token: 0x0400031E RID: 798
		ProvisionedFolder = 1,
		// Token: 0x0400031F RID: 799
		ProtectedFolder = 2,
		// Token: 0x04000320 RID: 800
		DisplayComment = 4,
		// Token: 0x04000321 RID: 801
		HasQuota = 8,
		// Token: 0x04000322 RID: 802
		RootFolder = 16,
		// Token: 0x04000323 RID: 803
		TrackFolderSize = 32,
		// Token: 0x04000324 RID: 804
		DumpsterFolder = 64
	}
}

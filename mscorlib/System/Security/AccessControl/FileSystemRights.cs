using System;

namespace System.Security.AccessControl
{
	// Token: 0x02000218 RID: 536
	[Flags]
	public enum FileSystemRights
	{
		// Token: 0x04000B2F RID: 2863
		ReadData = 1,
		// Token: 0x04000B30 RID: 2864
		ListDirectory = 1,
		// Token: 0x04000B31 RID: 2865
		WriteData = 2,
		// Token: 0x04000B32 RID: 2866
		CreateFiles = 2,
		// Token: 0x04000B33 RID: 2867
		AppendData = 4,
		// Token: 0x04000B34 RID: 2868
		CreateDirectories = 4,
		// Token: 0x04000B35 RID: 2869
		ReadExtendedAttributes = 8,
		// Token: 0x04000B36 RID: 2870
		WriteExtendedAttributes = 16,
		// Token: 0x04000B37 RID: 2871
		ExecuteFile = 32,
		// Token: 0x04000B38 RID: 2872
		Traverse = 32,
		// Token: 0x04000B39 RID: 2873
		DeleteSubdirectoriesAndFiles = 64,
		// Token: 0x04000B3A RID: 2874
		ReadAttributes = 128,
		// Token: 0x04000B3B RID: 2875
		WriteAttributes = 256,
		// Token: 0x04000B3C RID: 2876
		Delete = 65536,
		// Token: 0x04000B3D RID: 2877
		ReadPermissions = 131072,
		// Token: 0x04000B3E RID: 2878
		ChangePermissions = 262144,
		// Token: 0x04000B3F RID: 2879
		TakeOwnership = 524288,
		// Token: 0x04000B40 RID: 2880
		Synchronize = 1048576,
		// Token: 0x04000B41 RID: 2881
		FullControl = 2032127,
		// Token: 0x04000B42 RID: 2882
		Read = 131209,
		// Token: 0x04000B43 RID: 2883
		ReadAndExecute = 131241,
		// Token: 0x04000B44 RID: 2884
		Write = 278,
		// Token: 0x04000B45 RID: 2885
		Modify = 197055
	}
}

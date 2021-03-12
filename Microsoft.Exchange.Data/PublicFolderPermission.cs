using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000182 RID: 386
	[Flags]
	public enum PublicFolderPermission
	{
		// Token: 0x04000798 RID: 1944
		None = 0,
		// Token: 0x04000799 RID: 1945
		ReadItems = 1,
		// Token: 0x0400079A RID: 1946
		CreateItems = 2,
		// Token: 0x0400079B RID: 1947
		EditOwnedItems = 8,
		// Token: 0x0400079C RID: 1948
		DeleteOwnedItems = 16,
		// Token: 0x0400079D RID: 1949
		EditAllItems = 32,
		// Token: 0x0400079E RID: 1950
		DeleteAllItems = 64,
		// Token: 0x0400079F RID: 1951
		CreateSubfolders = 128,
		// Token: 0x040007A0 RID: 1952
		FolderOwner = 256,
		// Token: 0x040007A1 RID: 1953
		FolderContact = 512,
		// Token: 0x040007A2 RID: 1954
		FolderVisible = 1024
	}
}

using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007AD RID: 1965
	[Flags]
	public enum MailboxFolderMemberRights
	{
		// Token: 0x04002AB2 RID: 10930
		[LocDescription(Strings.IDs.MalboxFolderRightsNone)]
		None = 0,
		// Token: 0x04002AB3 RID: 10931
		[LocDescription(Strings.IDs.MalboxFolderRightsReadItems)]
		ReadItems = 1,
		// Token: 0x04002AB4 RID: 10932
		[LocDescription(Strings.IDs.MalboxFolderRightsCreateItems)]
		CreateItems = 2,
		// Token: 0x04002AB5 RID: 10933
		[LocDescription(Strings.IDs.MalboxFolderRightsEditOwnedItems)]
		EditOwnedItems = 8,
		// Token: 0x04002AB6 RID: 10934
		[LocDescription(Strings.IDs.MalboxFolderRightsDeleteOwnedItems)]
		DeleteOwnedItems = 16,
		// Token: 0x04002AB7 RID: 10935
		[LocDescription(Strings.IDs.MalboxFolderRightsEditAllItems)]
		EditAllItems = 32,
		// Token: 0x04002AB8 RID: 10936
		[LocDescription(Strings.IDs.MalboxFolderRightsDeleteAllItems)]
		DeleteAllItems = 64,
		// Token: 0x04002AB9 RID: 10937
		[LocDescription(Strings.IDs.MalboxFolderRightsCreateSubfolders)]
		CreateSubfolders = 128,
		// Token: 0x04002ABA RID: 10938
		[LocDescription(Strings.IDs.MalboxFolderRightsFolderOwner)]
		FolderOwner = 256,
		// Token: 0x04002ABB RID: 10939
		[LocDescription(Strings.IDs.MalboxFolderRightsFolderContact)]
		FolderContact = 512,
		// Token: 0x04002ABC RID: 10940
		[LocDescription(Strings.IDs.MalboxFolderRightsFolderVisible)]
		FolderVisible = 1024
	}
}

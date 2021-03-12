using System;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200038C RID: 908
	[Flags]
	public enum FolderTreeRenderType
	{
		// Token: 0x04001835 RID: 6197
		None = 0,
		// Token: 0x04001836 RID: 6198
		HideSearchFolders = 1,
		// Token: 0x04001837 RID: 6199
		HideGeekFoldersWithSpecificOrder = 2,
		// Token: 0x04001838 RID: 6200
		MailFoldersOnly = 4,
		// Token: 0x04001839 RID: 6201
		MailFolderWithoutSearchFolders = 5
	}
}

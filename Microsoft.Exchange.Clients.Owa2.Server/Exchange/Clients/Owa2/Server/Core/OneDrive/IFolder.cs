using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000014 RID: 20
	public interface IFolder : IClientObject<Folder>
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006C RID: 108
		int ItemCount { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006D RID: 109
		string ServerRelativeUrl { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006E RID: 110
		IListItem ListItemAllFields { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006F RID: 111
		IFolderCollection Folders { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000070 RID: 112
		IFileCollection Files { get; }

		// Token: 0x06000071 RID: 113
		void DeleteObject();
	}
}

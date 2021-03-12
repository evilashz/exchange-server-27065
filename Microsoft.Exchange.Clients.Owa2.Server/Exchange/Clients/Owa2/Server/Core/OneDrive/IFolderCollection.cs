using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000017 RID: 23
	public interface IFolderCollection : IClientObject<FolderCollection>
	{
		// Token: 0x06000085 RID: 133
		IFolder Add(string url);

		// Token: 0x06000086 RID: 134
		IFolder GetByUrl(string url);
	}
}

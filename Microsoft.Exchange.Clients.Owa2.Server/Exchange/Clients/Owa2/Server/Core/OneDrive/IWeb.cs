using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000029 RID: 41
	public interface IWeb : IClientObject<Web>
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000DC RID: 220
		IListCollection Lists { get; }

		// Token: 0x060000DD RID: 221
		IFolder GetFolderByServerRelativeUrl(string serverRelativeUrl);

		// Token: 0x060000DE RID: 222
		IFile GetFileByServerRelativeUrl(string relativeLocation);

		// Token: 0x060000DF RID: 223
		IList GetList(string url);
	}
}

using System;
using System.IO;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200000E RID: 14
	public interface IFile : IClientObject<File>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71
		string Name { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000048 RID: 72
		string ServerRelativeUrl { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000049 RID: 73
		long Length { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74
		IListItem ListItemAllFields { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004B RID: 75
		bool Exists { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004C RID: 76
		string LinkingUrl { get; }

		// Token: 0x0600004D RID: 77
		IClientResult<Stream> OpenBinaryStream();
	}
}

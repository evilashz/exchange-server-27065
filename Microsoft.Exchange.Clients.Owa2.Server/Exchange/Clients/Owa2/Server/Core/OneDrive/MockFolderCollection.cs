using System;
using System.IO;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000019 RID: 25
	public class MockFolderCollection : MockClientObject<FolderCollection>, IFolderCollection, IClientObject<FolderCollection>
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public MockFolderCollection(string serverRelativeUrl, MockClientContext context)
		{
			this.serverRelativeUrl = serverRelativeUrl;
			this.context = context;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002CFE File Offset: 0x00000EFE
		public override void LoadMockData()
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002D00 File Offset: 0x00000F00
		public IFolder Add(string url)
		{
			return new MockFolder(Path.Combine(this.serverRelativeUrl, url), this.context);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002D1C File Offset: 0x00000F1C
		public IFolder GetByUrl(string url)
		{
			string path = Path.Combine(MockClientContext.MockAttachmentDataProviderFilePath, Path.Combine(this.serverRelativeUrl, url));
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			if (!directoryInfo.Exists)
			{
				throw new MockServerException();
			}
			return new MockFolder(Path.Combine(this.serverRelativeUrl, url), this.context);
		}

		// Token: 0x0400002C RID: 44
		private readonly string serverRelativeUrl;

		// Token: 0x0400002D RID: 45
		private MockClientContext context;
	}
}

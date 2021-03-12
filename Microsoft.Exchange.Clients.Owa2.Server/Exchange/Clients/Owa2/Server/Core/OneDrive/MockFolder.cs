using System;
using System.IO;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000016 RID: 22
	public class MockFolder : MockClientObject<Folder>, IFolder, IClientObject<Folder>
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002B05 File Offset: 0x00000D05
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002B0D File Offset: 0x00000D0D
		public int ItemCount { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002B16 File Offset: 0x00000D16
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002B1E File Offset: 0x00000D1E
		public string ServerRelativeUrl { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002B28 File Offset: 0x00000D28
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002B7D File Offset: 0x00000D7D
		public IListItem ListItemAllFields
		{
			get
			{
				if (this.listItem == null)
				{
					string path = Path.Combine(MockClientContext.MockAttachmentDataProviderFilePath, this.ServerRelativeUrl);
					DirectoryInfo dirInfo = new DirectoryInfo(path);
					this.listItem = new MockListItem(dirInfo, new DirectoryInfo(path).Parent.FullName, this.context);
				}
				return this.listItem;
			}
			private set
			{
				this.listItem = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002B88 File Offset: 0x00000D88
		public IFolderCollection Folders
		{
			get
			{
				MockFolderCollection result;
				if ((result = this.folders) == null)
				{
					result = (this.folders = new MockFolderCollection(this.ServerRelativeUrl, this.context));
				}
				return result;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002BBC File Offset: 0x00000DBC
		public IFileCollection Files
		{
			get
			{
				MockFileCollection result;
				if ((result = this.files) == null)
				{
					result = (this.files = new MockFileCollection(this.ServerRelativeUrl, this.context));
				}
				return result;
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public void DeleteObject()
		{
			string path = Path.Combine(MockClientContext.MockAttachmentDataProviderFilePath, this.ServerRelativeUrl);
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			if (directoryInfo.Exists)
			{
				directoryInfo.Delete();
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002C23 File Offset: 0x00000E23
		public MockFolder(string serverRelativeUrl, MockClientContext context)
		{
			this.context = context;
			this.ServerRelativeUrl = serverRelativeUrl;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002C39 File Offset: 0x00000E39
		public MockFolder(MockListItem mockListItem, MockClientContext context)
		{
			this.ListItemAllFields = mockListItem;
			this.ServerRelativeUrl = (string)mockListItem["FileRef"];
			this.context = context;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002C68 File Offset: 0x00000E68
		public override void LoadMockData()
		{
			string path = Path.Combine(MockClientContext.MockAttachmentDataProviderFilePath, this.ServerRelativeUrl);
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
			this.ItemCount = directoryInfo.GetDirectories().Length + directoryInfo.GetFiles().Length;
		}

		// Token: 0x04000025 RID: 37
		private MockClientContext context;

		// Token: 0x04000026 RID: 38
		private MockFolderCollection folders;

		// Token: 0x04000027 RID: 39
		private MockFileCollection files;

		// Token: 0x04000028 RID: 40
		private IListItem listItem;
	}
}

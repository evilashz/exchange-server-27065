using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000015 RID: 21
	public class FolderWrapper : ClientObjectWrapper<Folder>, IFolder, IClientObject<Folder>
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002A3E File Offset: 0x00000C3E
		public int ItemCount
		{
			get
			{
				return this.backingFolder.ItemCount;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002A4B File Offset: 0x00000C4B
		public string ServerRelativeUrl
		{
			get
			{
				return this.backingFolder.ServerRelativeUrl;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002A58 File Offset: 0x00000C58
		public IListItem ListItemAllFields
		{
			get
			{
				IListItem result;
				if ((result = this.listItem) == null)
				{
					result = (this.listItem = new ListItemWrapper(this.backingFolder.ListItemAllFields));
				}
				return result;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002A88 File Offset: 0x00000C88
		public IFolderCollection Folders
		{
			get
			{
				FolderCollectionWrapper result;
				if ((result = this.folders) == null)
				{
					result = (this.folders = new FolderCollectionWrapper(this.backingFolder.Folders));
				}
				return result;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public IFileCollection Files
		{
			get
			{
				FileCollectionWrapper result;
				if ((result = this.files) == null)
				{
					result = (this.files = new FileCollectionWrapper(this.backingFolder.Files));
				}
				return result;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public void DeleteObject()
		{
			this.backingFolder.DeleteObject();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002AF5 File Offset: 0x00000CF5
		public FolderWrapper(Folder folder) : base(folder)
		{
			this.backingFolder = folder;
		}

		// Token: 0x04000021 RID: 33
		private Folder backingFolder;

		// Token: 0x04000022 RID: 34
		private FolderCollectionWrapper folders;

		// Token: 0x04000023 RID: 35
		private FileCollectionWrapper files;

		// Token: 0x04000024 RID: 36
		private IListItem listItem;
	}
}

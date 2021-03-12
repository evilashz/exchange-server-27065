using System;
using System.IO;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200000F RID: 15
	public class FileWrapper : ClientObjectWrapper<File>, IFile, IClientObject<File>
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002658 File Offset: 0x00000858
		public FileWrapper(File file) : base(file)
		{
			this.backingFile = file;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002668 File Offset: 0x00000868
		public string Name
		{
			get
			{
				return this.backingFile.Name;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002675 File Offset: 0x00000875
		public string ServerRelativeUrl
		{
			get
			{
				return this.backingFile.ServerRelativeUrl;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002682 File Offset: 0x00000882
		public long Length
		{
			get
			{
				return this.backingFile.Length;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002690 File Offset: 0x00000890
		public IListItem ListItemAllFields
		{
			get
			{
				IListItem result;
				if ((result = this.listItem) == null)
				{
					result = (this.listItem = new ListItemWrapper(this.backingFile.ListItemAllFields));
				}
				return result;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000026C0 File Offset: 0x000008C0
		public bool Exists
		{
			get
			{
				return this.backingFile.Exists;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000026CD File Offset: 0x000008CD
		public string LinkingUrl
		{
			get
			{
				return this.backingFile.LinkingUrl;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000026DA File Offset: 0x000008DA
		public IClientResult<Stream> OpenBinaryStream()
		{
			return new ClientResultWrapper<Stream>(this.backingFile.OpenBinaryStream());
		}

		// Token: 0x04000012 RID: 18
		private File backingFile;

		// Token: 0x04000013 RID: 19
		private IListItem listItem;
	}
}

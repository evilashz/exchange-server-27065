using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000018 RID: 24
	public class FolderCollectionWrapper : ClientObjectWrapper<FolderCollection>, IFolderCollection, IClientObject<FolderCollection>
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00002CB2 File Offset: 0x00000EB2
		public FolderCollectionWrapper(FolderCollection folders) : base(folders)
		{
			this.backingFolderCollection = folders;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002CC2 File Offset: 0x00000EC2
		public IFolder Add(string url)
		{
			return new FolderWrapper(this.backingFolderCollection.Add(url));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002CD5 File Offset: 0x00000ED5
		public IFolder GetByUrl(string url)
		{
			return new FolderWrapper(this.backingFolderCollection.GetByUrl(url));
		}

		// Token: 0x0400002B RID: 43
		private FolderCollection backingFolderCollection;
	}
}

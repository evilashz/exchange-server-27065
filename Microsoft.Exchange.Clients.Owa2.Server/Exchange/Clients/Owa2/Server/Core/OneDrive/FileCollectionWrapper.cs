using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000012 RID: 18
	public class FileCollectionWrapper : ClientObjectWrapper<FileCollection>, IFileCollection, IClientObject<FileCollection>
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000029EF File Offset: 0x00000BEF
		public FileCollectionWrapper(FileCollection files) : base(files)
		{
			this.backingFileCollection = files;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000029FF File Offset: 0x00000BFF
		public IFile Add(FileCreationInformation parameters)
		{
			return new FileWrapper(this.backingFileCollection.Add(parameters));
		}

		// Token: 0x0400001E RID: 30
		private FileCollection backingFileCollection;
	}
}

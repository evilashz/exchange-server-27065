using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000011 RID: 17
	public interface IFileCollection : IClientObject<FileCollection>
	{
		// Token: 0x06000066 RID: 102
		IFile Add(FileCreationInformation parameters);
	}
}

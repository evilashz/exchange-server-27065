using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000013 RID: 19
	public class MockFileCollection : MockClientObject<FileCollection>, IFileCollection, IClientObject<FileCollection>
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002A12 File Offset: 0x00000C12
		public MockFileCollection(string serverRelativeUrl, MockClientContext context)
		{
			this.serverRelativeUrl = serverRelativeUrl;
			this.context = context;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002A28 File Offset: 0x00000C28
		public override void LoadMockData()
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002A2A File Offset: 0x00000C2A
		public IFile Add(FileCreationInformation parameters)
		{
			return new MockFile(parameters, this.serverRelativeUrl, this.context);
		}

		// Token: 0x0400001F RID: 31
		private readonly string serverRelativeUrl;

		// Token: 0x04000020 RID: 32
		private MockClientContext context;
	}
}

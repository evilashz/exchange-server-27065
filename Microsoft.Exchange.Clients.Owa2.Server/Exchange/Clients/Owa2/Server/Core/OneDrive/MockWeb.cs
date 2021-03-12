using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200002B RID: 43
	public class MockWeb : MockClientObject<Web>, IWeb, IClientObject<Web>
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003998 File Offset: 0x00001B98
		public IListCollection Lists
		{
			get
			{
				MockListCollection result;
				if ((result = this.lists) == null)
				{
					result = (this.lists = new MockListCollection(this.context));
				}
				return result;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000039C3 File Offset: 0x00001BC3
		public MockWeb(MockClientContext context)
		{
			this.context = context;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000039D2 File Offset: 0x00001BD2
		public IFolder GetFolderByServerRelativeUrl(string serverRelativeUrl)
		{
			return new MockFolder(serverRelativeUrl, this.context);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000039E0 File Offset: 0x00001BE0
		public IFile GetFileByServerRelativeUrl(string relativeLocation)
		{
			return new MockFile(relativeLocation, this.context);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000039EE File Offset: 0x00001BEE
		public IList GetList(string url)
		{
			return new MockList(this.context);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000039FB File Offset: 0x00001BFB
		public override void LoadMockData()
		{
		}

		// Token: 0x04000055 RID: 85
		private MockClientContext context;

		// Token: 0x04000056 RID: 86
		private MockListCollection lists;
	}
}

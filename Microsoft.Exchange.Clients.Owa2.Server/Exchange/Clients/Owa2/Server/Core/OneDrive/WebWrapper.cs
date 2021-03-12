using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200002A RID: 42
	public class WebWrapper : ClientObjectWrapper<Web>, IWeb, IClientObject<Web>
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000391C File Offset: 0x00001B1C
		public IListCollection Lists
		{
			get
			{
				ListCollectionWrapper result;
				if ((result = this.lists) == null)
				{
					result = (this.lists = new ListCollectionWrapper(this.backingWeb.Lists));
				}
				return result;
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000394C File Offset: 0x00001B4C
		public WebWrapper(Web web) : base(web)
		{
			this.backingWeb = web;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000395C File Offset: 0x00001B5C
		public IFolder GetFolderByServerRelativeUrl(string serverRelativeUrl)
		{
			return new FolderWrapper(this.backingWeb.GetFolderByServerRelativeUrl(serverRelativeUrl));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000396F File Offset: 0x00001B6F
		public IFile GetFileByServerRelativeUrl(string relativeLocation)
		{
			return new FileWrapper(this.backingWeb.GetFileByServerRelativeUrl(relativeLocation));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003982 File Offset: 0x00001B82
		public IList GetList(string url)
		{
			return new ListWrapper(this.backingWeb.GetList(url));
		}

		// Token: 0x04000053 RID: 83
		private Web backingWeb;

		// Token: 0x04000054 RID: 84
		private ListCollectionWrapper lists;
	}
}

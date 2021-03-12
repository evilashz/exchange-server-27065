using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200001B RID: 27
	public class ListWrapper : ClientObjectWrapper<List>, IList, IClientObject<List>
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002D6C File Offset: 0x00000F6C
		public IFolder RootFolder
		{
			get
			{
				return new FolderWrapper(this.backingList.RootFolder);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002D7E File Offset: 0x00000F7E
		public ListWrapper(List list) : base(list)
		{
			this.backingList = list;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002D8E File Offset: 0x00000F8E
		public IListItemCollection GetItems(CamlQuery query)
		{
			return new ListItemCollectionWrapper(this.backingList.GetItems(query));
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002DA1 File Offset: 0x00000FA1
		public IListItem GetItemById(string id)
		{
			return new ListItemWrapper(this.backingList.GetItemById(id));
		}

		// Token: 0x0400002E RID: 46
		private List backingList;
	}
}

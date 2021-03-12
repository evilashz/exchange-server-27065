using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200001E RID: 30
	public class ListCollectionWrapper : ClientObjectWrapper<ListCollection>, IListCollection, IClientObject<ListCollection>
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00002FD4 File Offset: 0x000011D4
		public ListCollectionWrapper(ListCollection lists) : base(lists)
		{
			this.backingLists = lists;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002FE4 File Offset: 0x000011E4
		public IList GetByTitle(string title)
		{
			return new ListWrapper(this.backingLists.GetByTitle(title));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002FF7 File Offset: 0x000011F7
		public IList GetById(Guid guid)
		{
			return new ListWrapper(this.backingLists.GetById(guid));
		}

		// Token: 0x04000039 RID: 57
		private ListCollection backingLists;
	}
}

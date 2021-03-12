using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000024 RID: 36
	public class ListItemCollectionWrapper : ClientObjectWrapper<ListItemCollection>, IListItemCollection, IClientObjectCollection<IListItem, ListItemCollection>, IClientObject<ListItemCollection>, IEnumerable<IListItem>, IEnumerable
	{
		// Token: 0x17000048 RID: 72
		public IListItem this[int index]
		{
			get
			{
				return new ListItemWrapper(this.backingCollection[index]);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000034A2 File Offset: 0x000016A2
		public ListItemCollectionWrapper(ListItemCollection collection) : base(collection)
		{
			this.backingCollection = collection;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000034B2 File Offset: 0x000016B2
		public int Count()
		{
			return this.backingCollection.Count<ListItem>();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000035F8 File Offset: 0x000017F8
		public IEnumerator<IListItem> GetEnumerator()
		{
			foreach (ListItem item in this.backingCollection)
			{
				yield return new ListItemWrapper(item);
			}
			yield break;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003614 File Offset: 0x00001814
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000046 RID: 70
		private ListItemCollection backingCollection;
	}
}

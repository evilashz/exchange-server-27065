using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000023 RID: 35
	public interface IListItemCollection : IClientObjectCollection<IListItem, ListItemCollection>, IClientObject<ListItemCollection>, IEnumerable<IListItem>, IEnumerable
	{
	}
}

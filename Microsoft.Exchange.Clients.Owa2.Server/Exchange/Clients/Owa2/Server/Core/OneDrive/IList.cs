using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200001A RID: 26
	public interface IList : IClientObject<List>
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008E RID: 142
		IFolder RootFolder { get; }

		// Token: 0x0600008F RID: 143
		IListItemCollection GetItems(CamlQuery query);

		// Token: 0x06000090 RID: 144
		IListItem GetItemById(string id);
	}
}

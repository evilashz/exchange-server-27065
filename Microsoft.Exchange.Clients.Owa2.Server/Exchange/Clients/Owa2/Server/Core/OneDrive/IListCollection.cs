using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200001D RID: 29
	public interface IListCollection : IClientObject<ListCollection>
	{
		// Token: 0x0600009B RID: 155
		IList GetByTitle(string title);

		// Token: 0x0600009C RID: 156
		IList GetById(Guid guid);
	}
}

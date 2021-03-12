using System;
using Microsoft.Exchange.Clients.Owa.Core.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000379 RID: 889
	public interface IGroupRange
	{
		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x0600213E RID: 8510
		string Header { get; }

		// Token: 0x0600213F RID: 8511
		bool IsInGroup(IListViewDataSource dataSource, Column column);
	}
}

using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000C8 RID: 200
	internal interface IItemIdMapping : IIdMapping
	{
		// Token: 0x06000BB1 RID: 2993
		string Add(ISyncItemId mailboxItemId);

		// Token: 0x06000BB2 RID: 2994
		void Add(ISyncItemId itemId, string syncId);
	}
}

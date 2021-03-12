using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007D8 RID: 2008
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IUnseenItem
	{
		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x06004B40 RID: 19264
		StoreId Id { get; }

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x06004B41 RID: 19265
		ExDateTime ReceivedTime { get; }
	}
}

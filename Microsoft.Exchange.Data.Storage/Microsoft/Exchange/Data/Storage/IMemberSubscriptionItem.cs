using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007D7 RID: 2007
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMemberSubscriptionItem
	{
		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x06004B3E RID: 19262
		string SubscriptionId { get; }

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x06004B3F RID: 19263
		ExDateTime LastUpdateTimeUTC { get; }
	}
}

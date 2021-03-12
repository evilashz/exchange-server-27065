using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002DF RID: 735
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPushNotificationSubscriptionItem : IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06001F6E RID: 8046
		// (set) Token: 0x06001F6F RID: 8047
		string SubscriptionId { get; set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06001F70 RID: 8048
		// (set) Token: 0x06001F71 RID: 8049
		ExDateTime LastUpdateTimeUTC { get; set; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06001F72 RID: 8050
		// (set) Token: 0x06001F73 RID: 8051
		string SerializedNotificationSubscription { get; set; }
	}
}

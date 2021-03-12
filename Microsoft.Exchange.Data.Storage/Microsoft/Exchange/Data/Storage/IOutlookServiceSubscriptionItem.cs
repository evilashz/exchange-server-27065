using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002DA RID: 730
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IOutlookServiceSubscriptionItem : IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001F38 RID: 7992
		// (set) Token: 0x06001F39 RID: 7993
		string SubscriptionId { get; set; }

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06001F3A RID: 7994
		// (set) Token: 0x06001F3B RID: 7995
		ExDateTime LastUpdateTimeUTC { get; set; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06001F3C RID: 7996
		// (set) Token: 0x06001F3D RID: 7997
		string PackageId { get; set; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06001F3E RID: 7998
		// (set) Token: 0x06001F3F RID: 7999
		string AppId { get; set; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06001F40 RID: 8000
		// (set) Token: 0x06001F41 RID: 8001
		string DeviceNotificationId { get; set; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06001F42 RID: 8002
		// (set) Token: 0x06001F43 RID: 8003
		ExDateTime ExpirationTime { get; set; }

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001F44 RID: 8004
		// (set) Token: 0x06001F45 RID: 8005
		bool LockScreen { get; set; }
	}
}

using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000037 RID: 55
	internal interface IDeliveryConfiguration
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600027B RID: 635
		IAppConfiguration App { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600027C RID: 636
		DeliveryPoisonHandler PoisonHandler { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600027D RID: 637
		IThrottlingConfig Throttling { get; }

		// Token: 0x0600027E RID: 638
		void Load(IMbxDeliveryListener submitHandler);

		// Token: 0x0600027F RID: 639
		void Unload();

		// Token: 0x06000280 RID: 640
		void ConfigUpdate();
	}
}

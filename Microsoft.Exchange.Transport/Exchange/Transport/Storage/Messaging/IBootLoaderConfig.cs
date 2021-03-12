using System;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000EA RID: 234
	internal interface IBootLoaderConfig
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600087A RID: 2170
		bool BootLoaderMessageTrackingEnabled { get; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600087B RID: 2171
		TimeSpan MessageDropTimeout { get; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600087C RID: 2172
		TimeSpan MessageExpirationGracePeriod { get; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600087D RID: 2173
		TimeSpan PoisonMessageRetentionPeriod { get; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600087E RID: 2174
		bool PoisonCountPublishingEnabled { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600087F RID: 2175
		int PoisonCountLookbackHours { get; }
	}
}

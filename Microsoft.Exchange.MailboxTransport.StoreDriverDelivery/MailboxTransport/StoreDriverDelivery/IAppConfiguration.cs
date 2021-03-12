using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000039 RID: 57
	internal interface IAppConfiguration
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600028E RID: 654
		bool IsFolderPickupEnabled { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600028F RID: 655
		int PoisonRegistryEntryMaxCount { get; }

		// Token: 0x06000290 RID: 656
		void Load();
	}
}

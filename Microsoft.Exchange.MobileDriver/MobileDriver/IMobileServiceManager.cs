using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200001C RID: 28
	internal interface IMobileServiceManager
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000097 RID: 151
		IMobileServiceSelector Selector { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000098 RID: 152
		bool CapabilityPerRecipientSupported { get; }

		// Token: 0x06000099 RID: 153
		MobileServiceCapability GetCapabilityForRecipient(MobileRecipient recipient);
	}
}

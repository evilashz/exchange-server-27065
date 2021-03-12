using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000069 RID: 105
	internal enum AzureDeviceRegistrationChannelState
	{
		// Token: 0x040001B1 RID: 433
		Init,
		// Token: 0x040001B2 RID: 434
		ReadRegistration,
		// Token: 0x040001B3 RID: 435
		CreateRegistrationId,
		// Token: 0x040001B4 RID: 436
		Sending,
		// Token: 0x040001B5 RID: 437
		Discarding
	}
}

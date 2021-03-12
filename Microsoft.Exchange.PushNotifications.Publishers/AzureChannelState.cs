using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000041 RID: 65
	internal enum AzureChannelState
	{
		// Token: 0x04000102 RID: 258
		Init,
		// Token: 0x04000103 RID: 259
		ReadRegistration,
		// Token: 0x04000104 RID: 260
		NewRegistration,
		// Token: 0x04000105 RID: 261
		Sending,
		// Token: 0x04000106 RID: 262
		Discarding
	}
}

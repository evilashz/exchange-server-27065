using System;

namespace Microsoft.Exchange.Data.PushNotifications
{
	// Token: 0x0200026E RID: 622
	[Flags]
	public enum PushNotificationSubscriptionOption
	{
		// Token: 0x04000C2E RID: 3118
		NoSubscription = 0,
		// Token: 0x04000C2F RID: 3119
		Email = 1,
		// Token: 0x04000C30 RID: 3120
		Calendar = 2,
		// Token: 0x04000C31 RID: 3121
		VoiceMail = 4,
		// Token: 0x04000C32 RID: 3122
		MissedCall = 8,
		// Token: 0x04000C33 RID: 3123
		SuppressNotificationsWhenOof = 16,
		// Token: 0x04000C34 RID: 3124
		BackgroundSync = 32
	}
}

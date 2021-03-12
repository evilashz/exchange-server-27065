using System;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public enum NotificationType
	{
		// Token: 0x04000029 RID: 41
		Dropped,
		// Token: 0x0400002A RID: 42
		Missed,
		// Token: 0x0400002B RID: 43
		NewMail,
		// Token: 0x0400002C RID: 44
		Conversation,
		// Token: 0x0400002D RID: 45
		MessageItem,
		// Token: 0x0400002E RID: 46
		CalendarItem,
		// Token: 0x0400002F RID: 47
		PeopleIKnow,
		// Token: 0x04000030 RID: 48
		UnseenCount,
		// Token: 0x04000031 RID: 49
		ConnectionDropped
	}
}

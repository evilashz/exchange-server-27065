using System;

namespace Microsoft.Exchange.Configuration.SQM
{
	// Token: 0x0200026B RID: 619
	public enum SMSNotificationType
	{
		// Token: 0x04000678 RID: 1656
		None,
		// Token: 0x04000679 RID: 1657
		Email = 10,
		// Token: 0x0400067A RID: 1658
		VoiceMail = 20,
		// Token: 0x0400067B RID: 1659
		VoiceMailAndMissedCalls,
		// Token: 0x0400067C RID: 1660
		CalendarUpdate = 30,
		// Token: 0x0400067D RID: 1661
		CalendarReminder,
		// Token: 0x0400067E RID: 1662
		CalendarAgenda,
		// Token: 0x0400067F RID: 1663
		System = 40
	}
}

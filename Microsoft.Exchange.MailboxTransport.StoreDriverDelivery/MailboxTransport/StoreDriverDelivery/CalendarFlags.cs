using System;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000096 RID: 150
	[Flags]
	internal enum CalendarFlags
	{
		// Token: 0x040002C8 RID: 712
		None = 0,
		// Token: 0x040002C9 RID: 713
		AutoBooking = 1,
		// Token: 0x040002CA RID: 714
		CalendarAssistantActive = 2,
		// Token: 0x040002CB RID: 715
		CalendarAssistantNoiseReduction = 4,
		// Token: 0x040002CC RID: 716
		CalendarAssistantAddNewItems = 8,
		// Token: 0x040002CD RID: 717
		CalendarAssistantProcessExternal = 16,
		// Token: 0x040002CE RID: 718
		SkipProcessing = 32,
		// Token: 0x040002CF RID: 719
		CalAssistantDefaults = 14
	}
}

using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003C9 RID: 969
	[Flags]
	internal enum ModificationType
	{
		// Token: 0x0400187E RID: 6270
		Subject = 1,
		// Token: 0x0400187F RID: 6271
		MeetingType = 2,
		// Token: 0x04001880 RID: 6272
		ReminderDelta = 4,
		// Token: 0x04001881 RID: 6273
		Reminder = 8,
		// Token: 0x04001882 RID: 6274
		Location = 16,
		// Token: 0x04001883 RID: 6275
		BusyStatus = 32,
		// Token: 0x04001884 RID: 6276
		Attachment = 64,
		// Token: 0x04001885 RID: 6277
		SubType = 128,
		// Token: 0x04001886 RID: 6278
		Color = 256,
		// Token: 0x04001887 RID: 6279
		Body = 512
	}
}
